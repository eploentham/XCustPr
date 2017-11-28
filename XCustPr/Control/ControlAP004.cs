using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class ControlAP004
    {
        static String fontName = "Microsoft Sans Serif";        //standard
        public String backColor1 = "#1E1E1E";        //standard
        public String backColor2 = "#2D2D30";        //standard
        public String foreColor1 = "#fff";        //standard
        static float fontSize9 = 9.75f;        //standard
        static float fontSize8 = 8.25f;        //standard
        public Font fV1B, fV1;        //standard
        public int tcW = 0, tcH = 0, tcWMinus = 25, tcHMinus = 70, formFirstLineX = 5, formFirstLineY = 5;        //standard

        public ControlMain Cm;
        public ConnectDB conn;        //standard

        public ValidatePrPo vPrPo;

        public XcustUinfoInvoiceDetailTblDB xCUiIDTDB;
        public XcustUinfoInvoiceSumIntTblDB xCUiISITDB;
        public XcustPrTblDB xCPrTDB;

        public XcustApInvIntTblDB xCApIITDB;
        public XcustApInvLinesIntTblDB xCApILITDB;

        public XcustBuMstTblDB xCBMTDB;
        public XcustDeriverLocatorMstTblDB xCDLMTDB;
        public XcustDeriverOrganizationMstTblDB xCDOMTDB;
        public XcustSubInventoryMstTblDB xCSIMTDB;
        public XcustItemMstTblDB xCIMTDB;
        public XcustCurrencyMstTblDB xCMTDB;
        public XcustSupplierMstTblDB xCSMTDB;
        public XcustUomMstTblDB xCUMTDB;
        public XcustValueSetMstTblDB xCVSMTDB;

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;

        private List<XcustApInvIntTbl> listXcustApIIT;
        private List<XcustApInvLinesIntTbl> listXcustApILIT;

        private String dateStart = "";      //gen log

        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();

        public ControlAP004(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            vPrPo = new ValidatePrPo();

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard     XcustRcvHeadersIntAllDB

            xCUiIDTDB = new XcustUinfoInvoiceDetailTblDB(conn, Cm.initC);
            xCUiISITDB = new XcustUinfoInvoiceSumIntTblDB(conn, Cm.initC);
            xCPrTDB = new XcustPrTblDB(conn, Cm.initC);

            xCApIITDB = new XcustApInvIntTblDB(conn, Cm.initC);
            xCApILITDB = new XcustApInvLinesIntTblDB(conn, Cm.initC);

            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, Cm.initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, Cm.initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn, Cm.initC);
            xCIMTDB = new XcustItemMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCUMTDB = new XcustUomMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);

            Cm.createFolderAP004();

            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard

            listXcustApIIT = new List<XcustApInvIntTbl>();
            listXcustApILIT = new List<XcustApInvLinesIntTbl>();

            listXcSIMT = new List<XcustSubInventoryMstTbl>();

            listXcSIMT = new List<XcustSubInventoryMstTbl>();
            listXcIMT = new List<XcustItemMstTbl>();
            listXcSMT = new List<XcustSupplierMstTbl>();
            listXcVSMT = new List<XcustValueSetMstTbl>();
            listXcUMT = new List<XcustUomMstTbl>();
        }
        public void processTextFileUinfo(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            ReadText rd = new ReadText();
            String[] filePOProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;

            dateStart = date + " " + time;       //gen log

            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
            addListView("อ่าน fileจาก" + Cm.initC.AP004PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.AP004PathProcess + aa.Replace(Cm.initC.AP004PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            xCUiIDTDB.DeleteTemp();//  clear temp table     
            xCUiISITDB.DeleteTemp();//  clear temp table     
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_MMX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert xcust_mmx_pr_int_tbl
            filePOProcess = Cm.getFileinFolder(Cm.initC.AP004PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.AP004PathProcess, "", lv1, form1);
            DataTable dtFixLen = xCPrTDB.selectPO007FixLenLine();
            DataTable dtFixLenH = xCPrTDB.selectPO007FixLenHeader();
            foreach (string aa in filePOProcess)
            {
                List<String> rcv = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL   
                pB1.Visible = true;
                if (aa.Replace(Cm.initC.AP004PathProcess,"").ToLower().IndexOf("detail")>=0)
                {
                    xCUiIDTDB.insertBluk(rcv, dtFixLen, aa, "kfc_po", pB1);
                }
                else
                {
                    xCUiISITDB.insertBluk(rcv, dtFixLenH, aa, "kfc_po", pB1);
                }
                
                pB1.Visible = false;
            }
        }
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("processGetTempTableToValidate ", "Validate", lv1, form1);
            pB1.Visible = true;
            Boolean chk = false;
            DataTable dtHeader = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            String buCode = "", locator = "", Org = "", subInv_code = "", currencyCode = "", blanketAgreement = "";

            ValidatePrPo vPP = new ValidatePrPo();   // gen log
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();   // gen log
            List<ValidateFileName> lVfile = new List<ValidateFileName>();   // gen log

            getListXcSIMT();
            getListXcIMT();
            getListXcSMT();
            getListXcVSMT();
            getListXcUMT();
            //getListXcBAHT();
            //getListXcBALT();
            int row1 = 0;
            int cntErr = 0, cntFileErr = 0;   // gen log

            dtHeader = xCUiISITDB.selectAll();//   ดึง filename
            foreach (DataRow rowH in dtHeader.Rows)
            {
                addListView("ดึงข้อมูล  " + rowH[xCUiISITDB.xCUiISIT.FLIE_NAME].ToString().Trim(), "Validate", lv1, form1);
                dt = xCUiIDTDB.selectAll();    // ข้อมูลใน file
                row1 = 0;
                cntErr = 0;     //gen log
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;

                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = rowH[xCUiISITDB.xCUiISIT.FLIE_NAME].ToString().Trim();   // gen log
                vF.recordTotal = dt.Rows.Count.ToString();   // gen log

                //Error AP004-002 : Date Format not correct 
                chk = validateDate(rowH[xCUiISITDB.xCUiISIT.INVOICE_DATE].ToString());
                if (!chk)
                {
                    vPP = new ValidatePrPo();
                    vPP.Filename = rowH[xCUiISITDB.xCUiISIT.FLIE_NAME].ToString().Trim();
                    vPP.Message = "Error AP004-002 ";
                    vPP.Validate = "row " + row1 + " INVOICE_DATE=" + rowH[xCUiISITDB.xCUiISIT.INVOICE_DATE].ToString();
                    lVPr.Add(vPP);
                    cntErr++;       // gen log
                }
                //Error AP004-002 : Date Format not correct 
                chk = validateDate(rowH[xCUiISITDB.xCUiISIT.GL_DATE].ToString());
                if (!chk)
                {
                    vPP = new ValidatePrPo();
                    vPP.Filename = rowH[xCUiISITDB.xCUiISIT.FLIE_NAME].ToString().Trim();
                    vPP.Message = "Error AP004-002 ";
                    vPP.Validate = "row " + row1 + " GL_DATE=" + rowH[xCUiISITDB.xCUiISIT.GL_DATE].ToString();
                    lVPr.Add(vPP);
                    cntErr++;       // gen log
                }
                String invId = addXcustApIIT(rowH[xCUiISITDB.xCUiISIT.INVOICE_DATE].ToString().Trim(), rowH[xCUiISITDB.xCUiISIT.INVOICE_NUM].ToString().Trim()
                        , rowH[xCUiISITDB.xCUiISIT.SUPPLIER_NUM].ToString().Trim());//ทำ รอไว้ เพื่อ process ช้า
                foreach (DataRow row in dt.Rows)
                {
                    row1++;
                    pB1.Value = row1;
                    //Error PO003-002 : Date Format not correct
                    //chk = Cm.validateDate(row[xCLPRITDB.xCLPRIT.lot_expire_date].ToString());
                    //if (!chk)
                    //{
                    //    vPP = new ValidatePrPo();
                    //    vPP.Filename = rowH[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                    //    vPP.Message = "Error PO004-002 ";
                    //    vPP.Validate = "row " + row1 + " conf_delivery_date=" + row[xCLPRITDB.xCLPRIT.lot_expire_date].ToString();
                    //    lVPr.Add(vPP);
                    //    cntErr++;       // gen log
                    //}
                    //Error PO003-0054 : Invalid Supplier Code
                    //if (Cm.validateSupplierBySupplierCode(row[xCLPRITDB.xCLPRIT.supplier_code].ToString().Trim(), listXcSMT))
                    //{
                    //    vPP = new ValidatePrPo();
                    //    vPP.Filename = rowH[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                    //    vPP.Message = "Error PO004-015 ";
                    //    vPP.Validate = "row " + row1 + "  supplier_code " + row[xCLPRITDB.xCLPRIT.supplier_code].ToString().Trim();
                    //    lVPr.Add(vPP);
                    //    cntErr++;       // gen log
                    //}
                    //Error PO003-0065 : Invalid data type
                    //chk = Cm.validateQTY(row[xCLPRITDB.xCLPRIT.qty_receipt].ToString());
                    //if (!chk)
                    //{
                    //    vPP = new ValidatePrPo();
                    //    vPP.Filename = rowH[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                    //    vPP.Message = "Error PO004-006 ";
                    //    vPP.Validate = "row " + row1 + " order_qty=" + row[xCLPRITDB.xCLPRIT.qty_receipt].ToString();
                    //    lVPr.Add(vPP);
                    //    cntErr++;       // gen log
                    //}
                    //Error PO003-0110 : Item No. not found in PO No.
                    //if (Cm.validateItemCodeByOrgRef("300000000949654", row[xCLPRITDB.xCLPRIT.item_code].ToString().Trim(), listXcIMT))// ต้องแก้ Fix code อยู่
                    //{
                    //    vPP = new ValidatePrPo();
                    //    vPP.Filename = rowH[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                    //    vPP.Message = "Error PO004-011 ";
                    //    vPP.Validate = "row " + row1 + "  item_code " + row[xCLPRITDB.xCLPRIT.item_code].ToString().Trim();
                    //    lVPr.Add(vPP);
                    //    cntErr++;       // gen log
                    //}
                    //if (cntErr > 0)   // gen log
                    //{
                    //    cntFileErr++;
                    //}

                    //row.BeginEdit();
                    //row[xCMPITDB.xCMPIT.AGREEEMENT_NUMBER] = blanketAgreement;
                    //row[xCMPITDB.xCMPIT.PRICE] = price;
                    //row.EndEdit();
                    addXcustApILIT(invId,row);
                    //addXcustListDist(row);
                    //xCLPRITDB.updateValidateFlag("", "", "Y", "", "kfc_po");
                }
                vF.recordError = cntFileErr.ToString();   // gen log
                vF.totalError = cntErr.ToString();   // gen log
                lVfile.Add(vF);   // gen log
            }
            pB1.Visible = false;
            Cm.logProcess("xcustap004", lVPr, dateStart, lVfile);   // gen log
        }
        public void processInsertTable(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + Cm.initC.PO005PathProcess, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            foreach (XcustApInvIntTbl xcprhia in listXcustApIIT)
            {
                insertXcustApInvIntTbl(xcprhia, date, time);
            }
            foreach (XcustApInvLinesIntTbl xcprlia in listXcustApILIT)
            {
                //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                String chk = xCApILITDB.insert(xcprlia);
            }
        }
        private String insertXcustApInvIntTbl(XcustApInvIntTbl xcprhia, String date, String time)
        {
            String chk = "";
            XcustApInvIntTbl xCRHIA = xcprhia;

            chk = xCApIITDB.insert(xCRHIA);
            return chk;
        }
        /*
         * เตรียมข้อมูล เพื่อที่จะลง table XCUST_AP_INV_INT_TBL
         * 
         */
        private String addXcustApIIT(String inv_date, String inv_num, String vendor_num)
        {
            Boolean chk = true;
            String seq = "";
            if (chk)
            {
                seq = String.Concat("00" + listXcustApIIT.Count);
                XcustApInvIntTbl xcprhia1 = new XcustApInvIntTbl();
                xcprhia1.INVOICE_ID = seq;
                xcprhia1.BUSINESS_UNIT = Cm.initC.BU_NAME;
                xcprhia1.SOURCE = Cm.initC.AP004ImportSource;
                xcprhia1.INVOICE_DATE = inv_date;
                xcprhia1.INVOICE_NUM = inv_num;
                xcprhia1.VENDOR_NUMBER = vendor_num;
                xcprhia1.VENDOR_NAME = "";
                //xcprhia1.REQ_HEADER_INTERFACE_ID = po_number;
                //xcprhia1.BATCH_ID = curr_date.Replace("-", "") + seq.Substring(seq.Length - 2);
                //xcprhia1.REQUITITION_NUMBER = "PR" + curr_date.Substring(2, 2);
                //xcprhia1.DESCRIPTIONS = "LINFOX_" + po_number + "_" + filename;
                //xcprhia1.ATTRIBUTE_CATEGORY = "LINFOX_PR’";
                //xcprhia1.ATTRIBUTE2 = po_number;
                listXcustApIIT.Add(xcprhia1);
            }
            return seq;
        }
        private void addXcustApILIT(String inv_id, DataRow row)
        {
            XcustApInvLinesIntTbl item = new XcustApInvLinesIntTbl();
            item.INVOICE_ID = inv_id;
            item.LINE_NUMBER = row[xCUiIDTDB.xCUiIDT.invoice_line_number].ToString();
            item.INVOICE_TYPE_LOOKUP_CODE = "";
            item.INVOICE_AMOUNT = row[xCUiIDTDB.xCUiIDT.PO_ACT_TAX_AMT].ToString();
            item.QUANTITY = row[xCUiIDTDB.xCUiIDT.qty].ToString();
            item.PRICE = row[xCUiIDTDB.xCUiIDT.unit_price].ToString();
            item.DESCRIPTION = row[xCUiIDTDB.xCUiIDT.description2].ToString();
            item.PO_NUMBER = row[xCUiIDTDB.xCUiIDT.po_code].ToString();
            item.PO_LINE_NUMBER = row[xCUiIDTDB.xCUiIDT.po_line_no].ToString();
            item.RECEIPT_NUMBER = "";
            item.RECEIPT_LINE_NUMBER = "";
            item.TAX_REGIME_CODE = row[xCUiIDTDB.xCUiIDT.po_tax_code].ToString();
            item.AWT_GROUP_NAME = "";       // Withholding Tax Group
            //item.ATTRIBUTE1 = row[xCUiIDTDB.xCUiIDT.PO].ToString();
            //item.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
            //item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            //item.PROCESS_FLAG = "Y";
            //item.UOM_CODE = row[xCLFPTDB.xCLFPT.UOMCODE].ToString();

            listXcustApILIT.Add(item);
        }
        /*
         * check แค่ format ว่า เป็น DD-MM-YYYY เท่านั้น
         * Error AP004-002  : Date Format not correct 
         */
        public Boolean validateDate(String date)
        {
            Boolean chk = false;
            if (date.Length == 8)
            {
                sYear.Clear();
                sMonth.Clear();
                sDay.Clear();
                try
                {
                    sYear.Append(date.Substring(6, 4));
                    sMonth.Append(date.Substring(3, 2));
                    sDay.Append(date.Substring(0, 2));
                    if ((int.Parse(sYear.ToString()) > 2000) && (int.Parse(sYear.ToString()) < 2100))
                    {
                        if ((int.Parse(sMonth.ToString()) >= 1) && (int.Parse(sMonth.ToString()) <= 12))
                        {
                            if ((int.Parse(sDay.ToString()) >= 1) && (int.Parse(sDay.ToString()) <= 31))
                            {
                                chk = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    chk = false;
                }
                finally
                {

                }

            }
            else
            {
                chk = false;
            }
            return chk;
        }
        private void addListView(String col1, String col2, MaterialListView lv1, Form form1)
        {
            lv1.Items.Add(AddToList((lv1.Items.Count + 1), col1, col2));
            form1.Refresh();
        }
        private ListViewItem AddToList(int col1, string col2, string col3)
        {
            //int i = 0;
            string[] array = new string[3];
            array[0] = col1.ToString();
            //i = lv.Items.Count();
            //array[0] = lv.Items.Count();
            array[1] = col2;
            array[2] = col3;

            return (new ListViewItem(array));
        }
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
        private void getListXcSIMT()
        {
            listXcSIMT.Clear();
            DataTable dt = xCSIMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustSubInventoryMstTbl item = new XcustSubInventoryMstTbl();
                item = xCSIMTDB.setData(row);
                listXcSIMT.Add(item);
            }
        }
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
        private void getListXcIMT()
        {
            listXcIMT.Clear();
            DataTable dt = xCIMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustItemMstTbl item = new XcustItemMstTbl();
                item = xCIMTDB.setData(row);
                listXcIMT.Add(item);
            }
        }
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
        private void getListXcSMT()
        {
            listXcSMT.Clear();
            DataTable dt = xCSMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustSupplierMstTbl item = new XcustSupplierMstTbl();
                item = xCSMTDB.setData(row);
                listXcSMT.Add(item);
            }
        }
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
        private void getListXcVSMT()
        {
            listXcVSMT.Clear();
            DataTable dt = xCVSMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustValueSetMstTbl item = new XcustValueSetMstTbl();
                item = xCVSMTDB.setData(row);
                listXcVSMT.Add(item);
            }
        }
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
        private void getListXcUMT()
        {
            listXcUMT.Clear();
            DataTable dt = xCUMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustUomMstTbl item = new XcustUomMstTbl();
                item = xCUMTDB.setData(row);
                listXcUMT.Add(item);
            }
        }
    }
}
