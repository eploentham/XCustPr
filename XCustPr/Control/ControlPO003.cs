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

        public XcustPorReqHeaderIntAllDB xCPRHIADB;
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

        public XcustRcvHeadersIntAllDB xCRHIADB;
        public XcustRcvTransactionsIntAllDB xCRTIADB;
        public XcustInvTransactionLostsIntTblDB xITLITDB;

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

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard

            xCLPRITDB = new XcustLinfoxPoRcpIntTblDB(conn, Cm.initC);
            xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
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

            xCRHIADB = new XcustRcvHeadersIntAllDB(conn);
            xCRTIADB = new XcustRcvTransactionsIntAllDB(conn);
            xITLITDB = new XcustInvTransactionLostsIntTblDB(conn);
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
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " conf_delivery_date=" + row[xCLPRITDB.xCLPRIT.lot_expire_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO003-0054 : Invalid  Supplier Code
                    if (Cm.validateSupplierBySupplierCode(row[xCLPRITDB.xCLPRIT.supplier_code].ToString().Trim(), listXcSMT))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-015 ";
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
                        vPP.Message = "Error PO005-006 ";
                        vPP.Validate = "row " + row1 + " order_qty=" + row[xCLPRITDB.xCLPRIT.qty_receipt].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO003-0110 : Item No. not found in PO No.
                    if (Cm.validateItemCodeByOrgRef("300000000949654", row[xCLPRITDB.xCLPRIT.item_code].ToString().Trim(), listXcIMT))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLPRITDB.xCLPRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-011 ";
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

    }
}
