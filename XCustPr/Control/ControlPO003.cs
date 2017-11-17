using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class ControlPO003
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

        public XcustLinfoxPoRcpIntTblDB xCLPRITDB;

        //public XcustPorReqHeaderIntAllDB xCPRHIADB;
        public XcustPorReqLineIntAllDB xCPRLIADB;
        public XcustPorReqDistIntAllDB xCPRDIADB;
        public XcustBuMstTblDB xCBMTDB;
        public XcustDeriverLocatorMstTblDB xCDLMTDB;
        public XcustDeriverOrganizationMstTblDB xCDOMTDB;
        public XcustSubInventoryMstTblDB xCSIMTDB;
        public XcustItemMstTblDB xCIMTDB;
        public XcustCurrencyMstTblDB xCMTDB;
        public XcustSupplierMstTblDB xCSMTDB;
        public XcustUomMstTblDB xCUMTDB;
        public XcustValueSetMstTblDB xCVSMTDB;

        public XcustRcvHeadersIntAllDB xCRHIADB;        //table จริง
        public XcustRcvTransactionsIntAllDB xCRTIADB;        //table จริง
        public XcustInvTransactionLostsIntTblDB xITLITDB;        //table จริง

        //public XcustBlanketAgreementHeaderTblDB xCBAHTDB;
        //public XcustBlanketAgreementLinesTblDB xCBALTDB;

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;

        private List<XcustRcvHeadersIntAll> listXcustRHIA;
        private List<XcustRcvTransactionsIntAll> listXcusTRTIA;
        private List<XcustInvTransactionLostsIntTbl> listXcusITLIT;

        private String dateStart = "";      //gen log

        public ControlPO003(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            vPrPo = new ValidatePrPo();

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard     XcustRcvHeadersIntAllDB

            xCLPRITDB = new XcustLinfoxPoRcpIntTblDB(conn, Cm.initC);
            //xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
            xCPRLIADB = new XcustPorReqLineIntAllDB(conn, Cm.initC);
            xCPRDIADB = new XcustPorReqDistIntAllDB(conn);
            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, Cm.initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, Cm.initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn, Cm.initC);
            xCIMTDB = new XcustItemMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCUMTDB = new XcustUomMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);

            xCRHIADB = new XcustRcvHeadersIntAllDB(conn);       //Table จริงๆ
            xCRTIADB = new XcustRcvTransactionsIntAllDB(conn);       //Table จริงๆ
            xITLITDB = new XcustInvTransactionLostsIntTblDB(conn);       //Table จริงๆ
            //xCBAHTDB = new XcustBlanketAgreementHeaderTblDB(conn, Cm.initC);
            //xCBALTDB = new XcustBlanketAgreementLinesTblDB(conn, Cm.initC);

            Cm.createFolderPO003();
            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard

            listXcSIMT = new List<XcustSubInventoryMstTbl>();

            listXcSIMT = new List<XcustSubInventoryMstTbl>();
            listXcIMT = new List<XcustItemMstTbl>();
            listXcSMT = new List<XcustSupplierMstTbl>();
            listXcVSMT = new List<XcustValueSetMstTbl>();
            listXcUMT = new List<XcustUomMstTbl>();

            listXcustRHIA = new List<XcustRcvHeadersIntAll>();
            listXcusTRTIA = new List<XcustRcvTransactionsIntAll>();
            listXcusITLIT = new List<XcustInvTransactionLostsIntTbl>();
        }
        /*
         * a.	ระบบ MMX จะ  SFTP file จากระบบงาน MMX และนำ File มาวางไว้ที่ Server ตาม Path Parameter Path Initial
         * b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
         */
        public void processLinfoxRCVPOtoErpPR(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
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
            addListView("อ่าน fileจาก" + Cm.initC.PO003PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.PO003PathProcess + aa.Replace(Cm.initC.PO003PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            xCLPRITDB.DeleteMmxTemp();//  clear temp table     
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_MMX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert xcust_mmx_pr_int_tbl
            filePOProcess = Cm.getFileinFolder(Cm.initC.PO003PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.PO003PathProcess, "", lv1, form1);
            foreach (string aa in filePOProcess)
            {
                List<String> rcv = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL   
                pB1.Visible = true;
                xCLPRITDB.insertBluk(rcv, aa, "kfc_po", pB1);
                pB1.Visible = false;
            }
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
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("อ่าน file จาก " + Cm.initC.PathProcess, "Validate", lv1, form1);
            pB1.Visible = true;
            Boolean chk = false;
            DataTable dtGroupBy = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            String buCode = "", locator = "", Org = "", subInv_code = "", currencyCode = "", blanketAgreement = "";
                        
            ValidatePrPo vPP = new ValidatePrPo();   // gen log
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();   // gen log
            List<ValidateFileName> lVfile = new List<ValidateFileName>();   // gen log

            listXcustRHIA.Clear();
            listXcusTRTIA.Clear();
            listXcusITLIT.Clear();

            getListXcSIMT();
            getListXcIMT();
            getListXcSMT();
            getListXcVSMT();
            getListXcUMT();
            //getListXcBAHT();
            //getListXcBALT();
            int row1 = 0;
            int cntErr = 0, cntFileErr = 0;   // gen log

            dtGroupBy = xCLPRITDB.selectMmxGroupByFilename();//   ดึง filename
            foreach (DataRow rowG in dtGroupBy.Rows)
            {
                addListView("ดึงข้อมูล  " + rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim(), "Validate", lv1, form1);
                dt = xCLPRITDB.selectMmxByFilename(rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim());    // ข้อมูลใน file
                row1 = 0;
                cntErr = 0;     //gen log
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;

                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();   // gen log
                vF.recordTotal = dt.Rows.Count.ToString();   // gen log

                foreach (DataRow row in dt.Rows)
                {
                    row1++;
                    pB1.Value = row1;
                    //Error PO003-002 : Date Format not correct 
                    chk = Cm.validateDate(row[xCLPRITDB.xCLPRIT.lot_expire_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO004-002 ";
                        vPP.Validate = "row " + row1 + " conf_delivery_date=" + row[xCLPRITDB.xCLPRIT.lot_expire_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO003-0054 : Invalid  Supplier Code
                    if (Cm.validateSupplierBySupplierCode(row[xCLPRITDB.xCLPRIT.supplier_code].ToString().Trim(), listXcSMT))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO004-015 ";
                        vPP.Validate = "row " + row1 + "  supplier_code " + row[xCLPRITDB.xCLPRIT.supplier_code].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO003-0065 : Invalid data type
                    chk = Cm.validateQTY(row[xCLPRITDB.xCLPRIT.qty_receipt].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO004-006 ";
                        vPP.Validate = "row " + row1 + " order_qty=" + row[xCLPRITDB.xCLPRIT.qty_receipt].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO003-0110 : Item No. not found in PO No.
                    if (Cm.validateItemCodeByOrgRef("300000000949654", row[xCLPRITDB.xCLPRIT.item_code].ToString().Trim(), listXcIMT))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO004-011 ";
                        vPP.Validate = "row " + row1 + "  item_code " + row[xCLPRITDB.xCLPRIT.item_code].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    if (cntErr > 0)   // gen log
                    {
                        cntFileErr++;
                    }
                    addXcustListHeader(row[xCLPRITDB.xCLPRIT.reference1].ToString().Trim(), row[xCLPRITDB.xCLPRIT.supplier_code].ToString().Trim()
                        , row[xCLPRITDB.xCLPRIT.SUPPLIER_SITE_CODE].ToString().Trim());//ทำ รอไว้ เพื่อ process ช้า
                    row.BeginEdit();
                    //row[xCMPITDB.xCMPIT.AGREEEMENT_NUMBER] = blanketAgreement;
                    //row[xCMPITDB.xCMPIT.PRICE] = price;
                    row.EndEdit();
                    addXcustListLine(row);
                    addXcustListDist(row);
                    xCLPRITDB.updateValidateFlag("", "", "Y","", "kfc_po");
                }
                vF.recordError = cntFileErr.ToString();   // gen log
                vF.totalError = cntErr.ToString();   // gen log
                lVfile.Add(vF);   // gen log
            }
            pB1.Visible = false;
            Cm.logProcess("xcustpo003", lVPr, dateStart, lVfile);   // gen log
        }
        private void addXcustListHeader(String ref1, String supplier_code, String SUPPLIER_SITE_CODE)
        {
            Boolean chk = true;
            foreach (XcustRcvHeadersIntAll xcprhia in listXcustRHIA)
            {
                if (xcprhia.ATTRIBUTE1.Equals(ref1))
                {
                    chk = false;
                }
            }
            if (chk)
            {
                String seq = String.Concat("00" + listXcustRHIA.Count);
                XcustRcvHeadersIntAll xcrhia1 = new XcustRcvHeadersIntAll();
                xcrhia1.ATTRIBUTE1 = ref1;
                xcrhia1.HEADER_INTERFACE_NUMBER = ref1;
                xcrhia1.RECEIPT_SOURCE_CODE = Cm.initC.PO003RECEIPT_SOURCE;
                xcrhia1.ASN_TYPE = "";//ถาม
                xcrhia1.TRANSACTION_TYPE = Cm.initC.PO003TRANSACTION_TYPE;
                xcrhia1.RECEIPT_NUM = "";//ถาม
                xcrhia1.VENDOR_NUM = supplier_code;//ถาม
                xcrhia1.VENDOR_SITE_CODE = SUPPLIER_SITE_CODE;//ถาม
                xcrhia1.SHIPTO_ORGANIZATION_CODE = SUPPLIER_SITE_CODE;//ถาม
                xcrhia1.TRANSACTION_DATE = "";//ถาม
                xcrhia1.BUSINESS_UNIT = "";//ถาม
                xcrhia1.ATTRIBUTE_CATEGORY = "";//ถาม
                xcrhia1.PROCESS_FLAG = "N";
                listXcustRHIA.Add(xcrhia1);
            }
        }
        private void addXcustListLine(DataRow row)
        {
            XcustRcvTransactionsIntAll item = new XcustRcvTransactionsIntAll();
            item.LINE_NUMBER = row[xCLPRITDB.xCLPRIT.line_number].ToString();
            item.TRANSACTION_TYPE = Cm.initC.PO003TRANSACTION_TYPE;
            item.TRANSACTION_DATE = "";
            item.SOURCE_DOCUMENT_CODE = "";
            item.RECEIPT_SOURCE_CODE = "";
            item.HEADER_INTERFACE_NUMBER = row[xCLPRITDB.xCLPRIT.reference1].ToString();
            item.ORGANIZATION_CODE = "";
            item.ITEM_CODE = row[xCLPRITDB.xCLPRIT.item_code].ToString();
            item.DOCUMENT_NUMBER = "";
            item.DOCUMENT_LINE_NUMBER = "";
            item.BUSINESS_UNIT = "";

            item.SUBINVENTORY_CODE = row[xCLPRITDB.xCLPRIT.subinventory_code].ToString();
            item.LOCATOR_CODE = row[xCLPRITDB.xCLPRIT.LOCATOR].ToString();
            item.QUANTITY = row[xCLPRITDB.xCLPRIT.qty_receipt].ToString();
            item.UOM_CODE = row[xCLPRITDB.xCLPRIT.uom_code].ToString();
            item.INTERFACE_SOURCE_CODE = "";
            item.PROCESS_FLAG = "Y";
            //item.UOM_CODE = row[xCMPITDB.xCMPIT.uom_code].ToString();

            listXcusTRTIA.Add(item);
        }
        private void addXcustListDist(DataRow row)
        {
            XcustInvTransactionLostsIntTbl item = new XcustInvTransactionLostsIntTbl();
            item.LINE_NUMBER = row[xCLPRITDB.xCLPRIT.line_number].ToString();
            item.LOT_NUMBER = row[xCLPRITDB.xCLPRIT.lot_number].ToString();
            item.LOT_EXPIRATION_DATE = row[xCLPRITDB.xCLPRIT.lot_expire_date].ToString();
            item.TRANSACTION_QUANTITY = row[xCLPRITDB.xCLPRIT.qty_receipt].ToString();
            item.PRIMARY_QUANTITY = row[xCLPRITDB.xCLPRIT.qty_receipt].ToString();
            //item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            //item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            //item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            //item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            //item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            //item.LINE_TYPE = "";

            //item.QTY = row[xCMPITDB.xCMPIT.confirm_qty].ToString();
            //item.CURRENCY_CODE = initC.CURRENCY_CODE;
            //item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";

            listXcusITLIT.Add(item);
        }
        /*
         * g.	กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL
         * ,XCUST_POR_REQ_LINE_INT_ALL ,XCUST_POR_REQ_DIST_INT_ALLและ Update Validate_flag = ‘Y’
         */
        public void processInsertTable(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + Cm.initC.PO005PathProcess, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            foreach (XcustRcvHeadersIntAll xcprhia in listXcustRHIA)
            {
                if (insertXcustPorReqHeaderIntAll(xcprhia, date, time).Equals("1"))
                {
                    foreach (XcustRcvTransactionsIntAll xcprlia in listXcusTRTIA)
                    {
                        //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                        String chk = xCRTIADB.insert(xcprlia);
                    }
                    foreach (XcustInvTransactionLostsIntTbl xcprdia in listXcusITLIT)
                    {
                        //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                        String chk = xITLITDB.insert(xcprdia);
                    }
                }
            }
        }
        private String insertXcustPorReqHeaderIntAll(XcustRcvHeadersIntAll xcprhia, String date, String time)
        {//row[dc].ToString().Trim().
            String chk = "";
            XcustRcvHeadersIntAll xCRHIA = xcprhia;
            //xCRHIA.ATTRIBUTE1 = xcprhia.ATTRIBUTE1.Trim();

            //xCRHIA.ATTRIBUTE_DATE1 = date;
            //xCRHIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
            //xCRHIA.BATCH_ID = xcprhia.BATCH_ID;
            //xCRHIA.DESCRIPTIONS = xcprhia.DESCRIPTIONS.Trim();
            //xCRHIA.REQUESTER_EMAIL_ADDR = "";
            //xCRHIA.INTERFACE_SOURCE_CODE = "";
            //xCRHIA.ATTRIBUTE_CATEGORY = xcprhia.ATTRIBUTE_CATEGORY;
            //xCRHIA.REQ_HEADER_INTERFACE_ID = xcprhia.REQ_HEADER_INTERFACE_ID.Trim();
            //xCRHIA.PROCESS_FLAG = "N";
            //xCRHIA.APPROVER_EMAIL_ADDR = "";
            //xCRHIA.ATTRIBUTE2 = xcprhia.ATTRIBUTE2;
            //xCRHIA.REQUITITION_NUMBER = xcprhia.REQUITITION_NUMBER;
            //xCRHIA.IMPORT_SOURCE = xcprhia.IMPORT_SOURCE;
            //xCRHIA.ATTRIBUTE1 = xcprhia.ATTRIBUTE1;
            //xCRHIA.REQ_BU_NAME = xcprhia.REQ_BU_NAME;
            //xCRHIA.STATUS_CODE = xcprhia.STATUS_CODE;
            chk = xCRHIADB.insert(xCRHIA);
            return chk;
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
        public void processGenCSV(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("processGenCSVxCPRHIA ", "CVS", lv1, form1);
            processGenCSVxCRHIA(lv1, form1, pB1, "PO003");
            addListView("processGenCSVxCPRLIA ", "CVS", lv1, form1);
            processGenCSVxCRTIA(lv1, form1, pB1, "PO003");
            addListView("processGenCSVxCPRDIA ", "CVS", lv1, form1);
            processGenCSVxITLIT(lv1, form1, pB1, "PO003");
            addListView("processGenZIP ", "CVS", lv1, form1);
            processGenZIP(lv1, form1, pB1, "PO003");
        }
        public void processGenCSVxCRHIA(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            var file = Cm.initC.PathArchive + "PorReqHeadersInterfaceAl.csv";
            DataTable dt;
            if (flag.Equals("PO003"))
            {
                dt = xCRHIADB.selectAll();
            }
            else
            {
                dt = xCRHIADB.selectAll();
            }

            addListView("processGenCSVxCPRHIA จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCRHIADB.xCRHIA.HEADER_INTERFACE_NUMBER].ToString();
                    string col02 = row[xCRHIADB.xCRHIA.RECEIPT_SOURCE_CODE].ToString();
                    string col03 = row[xCRHIADB.xCRHIA.ASN_TYPE].ToString();
                    string col04 = row[xCRHIADB.xCRHIA.TRANSACTION_TYPE].ToString();
                    string col05 = "col05";
                    string col06 = "col06";
                    string col07 = "col07";
                    string col08 = "col08";
                    string col09 = row[xCRHIADB.xCRHIA.VENDOR_NUM].ToString();
                    string col10 = row[xCRHIADB.xCRHIA.VENDOR_SITE_CODE].ToString();

                    string col11 = "col11";
                    string col12 = row[xCRHIADB.xCRHIA.SHIPTO_ORGANIZATION_CODE].ToString();
                    string col13 = "col13";
                    string col14 = "col14";
                    string col15 = "col15";
                    string col16 = "col16";
                    string col17 = "";
                    string col18 = "";
                    string col19 = "";
                    string col20 = "";

                    string col21 = "";
                    string col22 = "";
                    string col23 = "";
                    string col24 = "";
                    string col25 = "";
                    string col26 = "";
                    string col27 = "";
                    string col28 = "";
                    string col29 = "";
                    string col30 = "";

                    string col31 = "";
                    string col32 = "";
                    string col33 = "";
                    string col34 = "";
                    string col35 = "";
                    string col36 = "";
                    string col37 = "";
                    string col38 = "";
                    string col39 = "";
                    string col40 = "";

                    string col41 = "";
                    string col42 = "";
                    string col43 = "";
                    string col44 = "";
                    string col45 = "";
                    string col46 = "";
                    string col47 = "";
                    string col48 = "";
                    string col49 = row[xCRHIADB.xCRHIA.TRANSACTION_DATE].ToString();
                    string col50 = "";

                    string col51 = "";
                    string col52 = row[xCRHIADB.xCRHIA.BUSINESS_UNIT].ToString();
                    string col53 = "";
                    string col54 = "";
                    string col55 = "";
                    string col56 = "";
                    string col57 = "";
                    string col58 = "";
                    string col59 = "";
                    string col60 = "";

                    string col61 = "";
                    string col62 = "";
                    string col63 = "";
                    string col64 = "";
                    string col65 = row[xCRHIADB.xCRHIA.ATTRIBUTE_CATEGORY].ToString();
                    string col66 = row[xCRHIADB.xCRHIA.ATTRIBUTE1].ToString();
                    string col67 = row[xCRHIADB.xCRHIA.ATTRIBUTE2].ToString();
                    string col68 = row[xCRHIADB.xCRHIA.ATTRIBUTE3].ToString();
                    string col69 = row[xCRHIADB.xCRHIA.ATTRIBUTE4].ToString();
                    string col70 = row[xCRHIADB.xCRHIA.ATTRIBUTE5].ToString();

                    string col71 = row[xCRHIADB.xCRHIA.ATTRIBUTE6].ToString();
                    string col72 = row[xCRHIADB.xCRHIA.ATTRIBUTE7].ToString();
                    string col73 = row[xCRHIADB.xCRHIA.ATTRIBUTE8].ToString();
                    string col74 = row[xCRHIADB.xCRHIA.ATTRIBUTE9].ToString();
                    string col75 = row[xCRHIADB.xCRHIA.ATTRIBUTE10].ToString();
                    string col76 = row[xCRHIADB.xCRHIA.ATTRIBUTE11].ToString();
                    string col77 = row[xCRHIADB.xCRHIA.ATTRIBUTE12].ToString();
                    string col78 = row[xCRHIADB.xCRHIA.ATTRIBUTE13].ToString();
                    string col79 = row[xCRHIADB.xCRHIA.ATTRIBUTE14].ToString();
                    string col80 = row[xCRHIADB.xCRHIA.ATTRIBUTE15].ToString();

                    string col81 = row[xCRHIADB.xCRHIA.ATTRIBUTE16].ToString();
                    string col82 = row[xCRHIADB.xCRHIA.ATTRIBUTE17].ToString();
                    string col83 = row[xCRHIADB.xCRHIA.ATTRIBUTE18].ToString();
                    string col84 = row[xCRHIADB.xCRHIA.ATTRIBUTE19].ToString();
                    string col85 = row[xCRHIADB.xCRHIA.ATTRIBUTE20].ToString();
                    string col86 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER1].ToString();
                    string col87 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER2].ToString();
                    string col88 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER3].ToString();
                    string col89 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER4].ToString();
                    string col90 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER5].ToString();

                    string col91 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER6].ToString();
                    string col92 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER7].ToString();
                    string col93 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER8].ToString();
                    string col94 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER9].ToString();
                    string col95 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER10].ToString();
                    string col96 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE1].ToString();
                    string col97 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE2].ToString();
                    string col98 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE3].ToString();
                    string col99 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE4].ToString();
                    string col100 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE5].ToString();

                    string col101 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE6].ToString();
                    string col102 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE7].ToString();
                    string col103 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE8].ToString();
                    string col104 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE9].ToString();
                    string col105 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE10].ToString();
                    
                    //string col71 = "col71";

                    //string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}," +
                    //    "{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}," +
                    //    "{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},{60}," +
                    //    "{61},{62},{63},{64},{65},{66},{67},{68},{69},{70},{71}", 
                    //    col01, col02, col03, col04, col05, col06, col07, col08, col09, col10,
                    //    col11, col12, col13, col14, col15, col16, col17, col18, col19, col20,
                    //    col21, col22, col23, col24, col25, col26, col27, col28, col29, col30,
                    //    col41, col42, col43, col44, col45, col46, col47, col48, col49, col50,
                    //    col51, col52, col53, col54, col55, col56, col57, col58, col59, col60,
                    //    col61, col62, col63, col64, col65, col66, col67, col68, col69, col70, col71);

                    string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10
                        + "," + col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
                        + "," + col21 + "," + col22 + "," + col23 + "," + col24 + "," + col25 + "," + col26 + "," + col27 + "," + col28 + "," + col29 + "," + col30
                        + "," + col31 + "," + col32 + "," + col33 + "," + col34 + "," + col35 + "," + col36 + "," + col37 + "," + col38 + "," + col39 + "," + col40
                        + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
                        + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70
                        + "," + col71 + "," + col72 + "," + col73 + "," + col74 + "," + col75 + "," + col76 + "," + col77 + "," + col78 + "," + col79 + "," + col80
                        + "," + col81 + "," + col82 + "," + col83 + "," + col84 + "," + col85 + "," + col86 + "," + col87 + "," + col88 + "," + col89 + "," + col90
                        + "," + col91 + "," + col92 + "," + col93 + "," + col94 + "," + col95 + "," + col96 + "," + col97 + "," + col98 + "," + col99 + "," + col100
                        + "," + col101 + "," + col102 + "," + col103 + "," + col104 + "," + col105;

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenCSVxCRTIA(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            var file = Cm.initC.PathArchive + "PorReqHeadersInterfaceAl.csv";
            DataTable dt;
            if (flag.Equals("PO003"))
            {
                dt = xCRTIADB.selectAll();
            }
            else
            {
                dt = xCRTIADB.selectAll();
            }

            addListView("processGenCSVxCPRHIA จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCRTIADB.xCRTIA.LINE_NUMBER].ToString();
                    string col02 = row[xCRTIADB.xCRTIA.TRANSACTION_TYPE].ToString();
                    string col03 = "";      //ระบุ Receipt
                    string col04 = row[xCRTIADB.xCRTIA.TRANSACTION_DATE].ToString();
                    string col05 = row[xCRTIADB.xCRTIA.SOURCE_DOCUMENT_CODE].ToString();
                    string col06 = row[xCRTIADB.xCRTIA.RECEIPT_SOURCE_CODE].ToString();
                    string col07 = row[xCRTIADB.xCRTIA.HEADER_INTERFACE_NUMBER].ToString();
                    string col08 = "col08";
                    string col09 = "col08";
                    string col10 = row[xCRTIADB.xCRTIA.ORGANIZATION_CODE].ToString();

                    string col11 = row[xCRTIADB.xCRTIA.ITEM_CODE].ToString();
                    string col12 = "";
                    string col13 = "";
                    string col14 = row[xCRTIADB.xCRTIA.DOCUMENT_NUMBER].ToString();
                    string col15 = row[xCRTIADB.xCRTIA.DOCUMENT_LINE_NUMBER].ToString();
                    string col16 = "1";
                    string col17 = "";
                    string col18 = row[xCRTIADB.xCRTIA.BUSINESS_UNIT].ToString();
                    string col19 = "";
                    string col20 = "";

                    string col21 = row[xCRTIADB.xCRTIA.SUBINVENTORY_CODE].ToString();
                    string col22 = row[xCRTIADB.xCRTIA.LOCATOR_CODE].ToString();
                    string col23 = row[xCRTIADB.xCRTIA.QUANTITY].ToString();
                    string col24 = row[xCRTIADB.xCRTIA.UOM_CODE].ToString();
                    string col25 = "";
                    string col26 = "";
                    string col27 = "";
                    string col28 = "";
                    string col29 = "";
                    string col30 = "";

                    string col31 = "";
                    string col32 = "";
                    string col33 = "";
                    string col34 = "";
                    string col35 = "";
                    string col36 = "";
                    string col37 = "";
                    string col38 = "";
                    string col39 = "";
                    string col40 = "";

                    string col41 = "";
                    string col42 = "";      //
                    string col43 = row[xCRTIADB.xCRTIA.INTERFACE_SOURCE_CODE].ToString();
                    string col44 = "";
                    string col45 = "";
                    string col46 = "";
                    string col47 = "";
                    string col48 = "";
                    string col49 = "";
                    string col50 = "";

                    string col51 = "";
                    string col52 = "";
                    string col53 = "";
                    string col54 = "";
                    string col55 = "";
                    string col56 = "";
                    string col57 = "";
                    string col58 = "";
                    string col59 = "";
                    string col60 = "";

                    string col61 = "";
                    string col62 = "";
                    string col63 = "";
                    string col64 = "";
                    string col65 = "";
                    string col66 = "";
                    string col67 = "";
                    string col68 = "";
                    string col69 = "";
                    string col70 = "";

                    string col71 = "";
                    string col72 = "";
                    string col73 = "";
                    string col74 = "";
                    string col75 = "";
                    string col76 = "";
                    string col77 = "";
                    string col78 = "";
                    string col79 = "";
                    string col80 = "";

                    string col81 = "";
                    string col82 = "";
                    string col83 = "";
                    string col84 = "";
                    string col85 = "";
                    string col86 = row[xCRHIADB.xCRHIA.ATTRIBUTE_CATEGORY].ToString();
                    string col87 = row[xCRHIADB.xCRHIA.ATTRIBUTE1].ToString();
                    string col88 = row[xCRHIADB.xCRHIA.ATTRIBUTE2].ToString();
                    string col89 = row[xCRHIADB.xCRHIA.ATTRIBUTE3].ToString();
                    string col90 = row[xCRHIADB.xCRHIA.ATTRIBUTE4].ToString();

                    string col91 = row[xCRHIADB.xCRHIA.ATTRIBUTE5].ToString();
                    string col92 = row[xCRHIADB.xCRHIA.ATTRIBUTE6].ToString();
                    string col93 = row[xCRHIADB.xCRHIA.ATTRIBUTE7].ToString();
                    string col94 = row[xCRHIADB.xCRHIA.ATTRIBUTE8].ToString();
                    string col95 = row[xCRHIADB.xCRHIA.ATTRIBUTE9].ToString();
                    string col96 = row[xCRHIADB.xCRHIA.ATTRIBUTE10].ToString();
                    string col97 = row[xCRHIADB.xCRHIA.ATTRIBUTE11].ToString();
                    string col98 = row[xCRHIADB.xCRHIA.ATTRIBUTE12].ToString();
                    string col99 = row[xCRHIADB.xCRHIA.ATTRIBUTE13].ToString();
                    string col100 = row[xCRHIADB.xCRHIA.ATTRIBUTE14].ToString();

                    string col101 = row[xCRHIADB.xCRHIA.ATTRIBUTE15].ToString();
                    string col102 = row[xCRHIADB.xCRHIA.ATTRIBUTE16].ToString();
                    string col103 = row[xCRHIADB.xCRHIA.ATTRIBUTE17].ToString();
                    string col104 = row[xCRHIADB.xCRHIA.ATTRIBUTE18].ToString();
                    string col105 = row[xCRHIADB.xCRHIA.ATTRIBUTE19].ToString();
                    string col106 = row[xCRHIADB.xCRHIA.ATTRIBUTE20].ToString();
                    string col107 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER1].ToString();
                    string col108 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER2].ToString();
                    string col109 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER3].ToString();
                    string col110 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER4].ToString();

                    string col111 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER5].ToString();
                    string col112 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER6].ToString();
                    string col113 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER7].ToString();
                    string col114 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER8].ToString();
                    string col115 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER9].ToString();
                    string col116 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER10].ToString();
                    string col117 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE1].ToString();
                    string col118 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE2].ToString();
                    string col119 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE3].ToString();
                    string col120 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE4].ToString();

                    string col121 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE5].ToString();
                    string col122 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP1].ToString();
                    string col123 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP2].ToString();
                    string col124 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP3].ToString();
                    string col125 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP4].ToString();
                    string col126 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP5].ToString();
                    string col127 = "";
                    string col128 = "";
                    string col129 = "";
                    string col130 = "";

                    string col131 = "";
                    string col132 = "";
                    string col133 = "";
                    string col134 = "";
                    string col135 = "";
                    string col136 = "";
                    string col137 = "";
                    string col138 = "";
                    string col139 = "";
                    string col140 = "";

                    string col141 = "";
                    string col142 = "";
                    string col143 = "";
                    string col144 = "";
                    string col145 = "";                    
                    
                    //string col71 = "col71";

                    //string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}," +
                    //    "{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}," +
                    //    "{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},{60}," +
                    //    "{61},{62},{63},{64},{65},{66},{67},{68},{69},{70},{71}", 
                    //    col01, col02, col03, col04, col05, col06, col07, col08, col09, col10,
                    //    col11, col12, col13, col14, col15, col16, col17, col18, col19, col20,
                    //    col21, col22, col23, col24, col25, col26, col27, col28, col29, col30,
                    //    col41, col42, col43, col44, col45, col46, col47, col48, col49, col50,
                    //    col51, col52, col53, col54, col55, col56, col57, col58, col59, col60,
                    //    col61, col62, col63, col64, col65, col66, col67, col68, col69, col70, col71);

                    string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10
                        + "," + col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
                        + "," + col21 + "," + col22 + "," + col23 + "," + col24 + "," + col25 + "," + col26 + "," + col27 + "," + col28 + "," + col29 + "," + col30
                        + "," + col31 + "," + col32 + "," + col33 + "," + col34 + "," + col35 + "," + col36 + "," + col37 + "," + col38 + "," + col39 + "," + col40
                        + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
                        + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70
                        + "," + col71 + "," + col72 + "," + col73 + "," + col74 + "," + col75 + "," + col76 + "," + col77 + "," + col78 + "," + col79 + "," + col80
                        + "," + col81 + "," + col82 + "," + col83 + "," + col84 + "," + col85 + "," + col86 + "," + col87 + "," + col88 + "," + col89 + "," + col90
                        + "," + col91 + "," + col92 + "," + col93 + "," + col94 + "," + col95 + "," + col96 + "," + col97 + "," + col98 + "," + col99 + "," + col100
                        + "," + col101 + "," + col102 + "," + col103 + "," + col104 + "," + col105 + "," + col106 + "," + col107 + "," + col108 + "," + col109 + "," + col110
                        + "," + col111 + "," + col112 + "," + col113 + "," + col114 + "," + col115 + "," + col116 + "," + col117 + "," + col118 + "," + col119 + "," + col120
                        + "," + col121 + "," + col122 + "," + col123 + "," + col124 + "," + col125 + "," + col126 + "," + col127 + "," + col128 + "," + col129 + "," + col130
                        + "," + col131 + "," + col132 + "," + col133 + "," + col134 + "," + col135 + "," + col136 + "," + col137 + "," + col138 + "," + col139 + "," + col140
                        + "," + col141 + "," + col142 + "," + col143 + "," + col144 + "," + col145 ;

                    stream.WriteLine(csvRow);
                }
            }
        }
        /*
         * ในกรณีที่ต้นทางส่ง filed lot number มาด้วย
         */
        public void processGenCSVxITLIT(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            var file = Cm.initC.PathArchive + "PorReqHeadersInterfaceAl.csv";
            DataTable dt;
            if (flag.Equals("PO003"))
            {
                dt = xITLITDB.selectAll();
            }
            else
            {
                dt = xITLITDB.selectAll();
            }

            addListView("processGenCSVxCPRHIA จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xITLITDB.xCITLIT.HEADER_INTERFACE_NUMBER].ToString();
                    string col02 = row[xITLITDB.xCITLIT.LOT_NUMBER].ToString();
                    string col03 = row[xITLITDB.xCITLIT.LOT_EXPIRATION_DATE].ToString();
                    string col04 = row[xITLITDB.xCITLIT.TRANSACTION_QUANTITY].ToString();
                    string col05 = row[xITLITDB.xCITLIT.PRIMARY_QUANTITY].ToString();
                    string col06 = "";
                    string col07 = "";
                    string col08 = "col08";
                    string col09 = "col08";
                    string col10 = "";

                    string col11 = "";
                    string col12 = "";
                    string col13 = "";
                    string col14 = "";
                    string col15 = "";
                    string col16 = "1";
                    string col17 = "";
                    string col18 = "";
                    string col19 = "";
                    string col20 = "";

                    string col21 = "";
                    string col22 = "";
                    string col23 = "";
                    string col24 = "";
                    string col25 = "";
                    string col26 = "";
                    string col27 = "";
                    string col28 = "";
                    string col29 = "";
                    string col30 = "";

                    string col31 = "";
                    string col32 = "";
                    string col33 = "";
                    string col34 = "";
                    string col35 = "";
                    string col36 = "";
                    string col37 = "";
                    string col38 = "";
                    string col39 = "";
                    string col40 = "";

                    string col41 = "";
                    string col42 = "";      //
                    string col43 = "";
                    string col44 = "";
                    string col45 = "";
                    string col46 = "";
                    string col47 = "";
                    string col48 = "";
                    string col49 = "";
                    string col50 = "";

                    string col51 = "";
                    string col52 = "";
                    string col53 = "";
                    string col54 = "";
                    string col55 = "";
                    string col56 = "";
                    string col57 = "";
                    string col58 = "";
                    string col59 = "";
                    string col60 = "";

                    string col61 = "";
                    string col62 = "";
                    string col63 = "";
                    string col64 = "";
                    string col65 = "";
                    string col66 = "";
                    string col67 = "";
                    string col68 = "";
                    string col69 = "";
                    string col70 = "";

                    string col71 = "";
                    string col72 = "";
                    string col73 = "";
                    string col74 = "";
                    string col75 = "";
                    string col76 = "";
                    string col77 = "";
                    string col78 = "";
                    string col79 = "";
                    string col80 = "";

                    string col81 = "";
                    string col82 = "";
                    string col83 = "";
                    string col84 = "";
                    string col85 = "";
                    string col86 = "";
                    string col87 = "";
                    string col88 = "";
                    string col89 = "";
                    string col90 = "";

                    string col91 = "";
                    string col92 = "";
                    string col93 = "";
                    string col94 = "";
                    string col95 = "";
                    string col96 = "";
                    string col97 = "";
                    string col98 = "";
                    string col99 = "";
                    string col100 = "";

                    string col101 = "";
                    string col102 = "";
                    string col103 = "";
                    string col104 = "";
                    string col105 = "";
                    string col106 = "";
                    string col107 = "";
                    string col108 = "";
                    string col109 = "";
                    string col110 = "";

                    string col111 = "";
                    string col112 = "";
                    string col113 = "";
                    string col114 = "";
                    string col115 = "";
                    string col116 = "";
                    string col117 = "";
                    string col118 = "";
                    string col119 = "";
                    string col120 = "";

                    string col121 = "";
                    string col122 = "";
                    string col123 = "";
                    string col124 = "";
                    string col125 = "";
                    string col126 = "";
                    string col127 = "";
                    string col128 = "";
                    string col129 = "";
                    

                    //string col71 = "col71";

                    //string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}," +
                    //    "{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}," +
                    //    "{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},{60}," +
                    //    "{61},{62},{63},{64},{65},{66},{67},{68},{69},{70},{71}", 
                    //    col01, col02, col03, col04, col05, col06, col07, col08, col09, col10,
                    //    col11, col12, col13, col14, col15, col16, col17, col18, col19, col20,
                    //    col21, col22, col23, col24, col25, col26, col27, col28, col29, col30,
                    //    col41, col42, col43, col44, col45, col46, col47, col48, col49, col50,
                    //    col51, col52, col53, col54, col55, col56, col57, col58, col59, col60,
                    //    col61, col62, col63, col64, col65, col66, col67, col68, col69, col70, col71);

                    string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10
                        + "," + col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
                        + "," + col21 + "," + col22 + "," + col23 + "," + col24 + "," + col25 + "," + col26 + "," + col27 + "," + col28 + "," + col29 + "," + col30
                        + "," + col31 + "," + col32 + "," + col33 + "," + col34 + "," + col35 + "," + col36 + "," + col37 + "," + col38 + "," + col39 + "," + col40
                        + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
                        + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70
                        + "," + col71 + "," + col72 + "," + col73 + "," + col74 + "," + col75 + "," + col76 + "," + col77 + "," + col78 + "," + col79 + "," + col80
                        + "," + col81 + "," + col82 + "," + col83 + "," + col84 + "," + col85 + "," + col86 + "," + col87 + "," + col88 + "," + col89 + "," + col90
                        + "," + col91 + "," + col92 + "," + col93 + "," + col94 + "," + col95 + "," + col96 + "," + col97 + "," + col98 + "," + col99 + "," + col100
                        + "," + col101 + "," + col102 + "," + col103 + "," + col104 + "," + col105 + "," + col106 + "," + col107 + "," + col108 + "," + col109 + "," + col110
                        + "," + col111 + "," + col112 + "," + col113 + "," + col114 + "," + col115 + "," + col116 + "," + col117 + "," + col118 + "," + col119 + "," + col120
                        + "," + col121 + "," + col122 + "," + col123 + "," + col124 + "," + col125 + "," + col126 + "," + col127 + "," + col128 + "," + col129;

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenZIP(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            addListView("create zip file " + Cm.initC.PathProcess, "Validate", lv1, form1);
            String filenameZip = "", ilename2 = "", ilename3 = "", filename = "";
            if (flag.Equals("PO003"))
            {
                filenameZip = Cm.initC.PathZip + "\\xcustpr.zip";
                filename = @Cm.initC.PathArchive;
            }
            else
            {
                filenameZip = Cm.initC.PO005pathZip + "\\xcustpr.zip";
                filename = @Cm.initC.PO005PathArchive;
            }
            Cm.deleteFile(filenameZip);
            ZipArchive zip = ZipFile.Open(filenameZip, ZipArchiveMode.Create);

            var allFiles = Directory.GetFiles(filename, "*.*", SearchOption.AllDirectories);
            foreach (String file in allFiles)
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
            zip.Dispose();
        }
    }
}
