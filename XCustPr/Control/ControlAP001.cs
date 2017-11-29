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
    public class ControlAP001
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

        public XcustSupInvoiceIntTblDB xCSIITDB;        //table temp
        public XcustApInvIntTblDB xCApIITDB;        //table จริง
        public XcustApInvLinesIntTblDB xCApILITDB;        //table จริง

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

        private List<XcustApInvIntTbl> listXcApIIT;
        private List<XcustApInvLinesIntTbl> listXcApILIT;

        private String dateStart = "";      //gen log

        public ControlAP001(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            vPrPo = new ValidatePrPo();

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard     XcustRcvHeadersIntAllDB

            xCSIITDB = new XcustSupInvoiceIntTblDB(conn, Cm.initC);

            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, Cm.initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, Cm.initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn, Cm.initC);
            xCIMTDB = new XcustItemMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCUMTDB = new XcustUomMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);

            xCApIITDB = new XcustApInvIntTblDB(conn, Cm.initC);
            xCApILITDB = new XcustApInvLinesIntTblDB(conn, Cm.initC);

            Cm.createFolderAP001();

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
            listXcApIIT = new List<XcustApInvIntTbl>();
            listXcApILIT = new List<XcustApInvLinesIntTbl>();
        }
        public void processTextFileSupplier(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
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
            addListView("อ่าน fileจาก" + Cm.initC.AP001PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.AP001PathProcess + aa.Replace(Cm.initC.AP001PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            xCSIITDB.DeleteTemp();//  clear temp table     
            
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_MMX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert xcust_mmx_pr_int_tbl
            filePOProcess = Cm.getFileinFolder(Cm.initC.AP001PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.AP001PathProcess, "", lv1, form1);
            
            foreach (string aa in filePOProcess)
            {
                List<String> rcv = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL   
                pB1.Visible = true;

                xCSIITDB.insertBluk(rcv, aa, "kfc_po", pB1);
                
                pB1.Visible = false;
            }
        }
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("processGetTempTableToValidate ", "Validate", lv1, form1);
            pB1.Visible = true;
            Boolean chk = false;
            DataTable dtHeader = new DataTable();
            DataTable dtGroupBy = new DataTable();
            DataTable dt = new DataTable();
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
            
            dtGroupBy = xCSIITDB.selectGroupByFilename();//   ดึง filename
            foreach (DataRow rowG in dtGroupBy.Rows)
            {
                ValidateFileName vF = new ValidateFileName();   // gen log
                //vF.fileName = rowG[xCSIITDB.xCSIIT.FILE_NAME].ToString().Trim();   // gen log
                //vF.recordTotal = dt.Rows.Count.ToString();   // gen log
                dt = xCSIITDB.selectByFilename(rowG[xCSIITDB.xCSIIT.FILE_NAME].ToString());
                foreach(DataRow row in dt.Rows)
                {

                }
            }
        }
        private void addXcustApInv(String inv_number, String inv_date, String po_number, String filename)
        {
            Boolean chk = true;
            foreach (XcustApInvIntTbl xcprhia in listXcApIIT)
            {
                if (xcprhia.INVOICE_NUM.Equals(inv_number))
                {
                    chk = false;
                }
            }
            if (chk)
            {
                String seq = String.Concat("00" + listXcApIIT.Count);
                XcustApInvIntTbl xcprhia1 = new XcustApInvIntTbl();
                xcprhia1.INVOICE_NUM = inv_number;
                xcprhia1.INVOICE_ID = seq;
                xcprhia1.BUSINESS_UNIT = Cm.initC.BU_NAME;
                xcprhia1.SOURCE = Cm.initC.AP001ImportSource;
                xcprhia1.INVOICE_DATE = inv_number;
                //xcprhia1.VENDOR_NAME

                xcprhia1.INVOICE_CUR_CODE = po_number;
                xcprhia1.PAYMENT_CURR_CODE = po_number;
                xcprhia1.DESCRIPTION = inv_number;
                xcprhia1.INVOICE_TYPE_LOOKUP_CODE = Cm.initC.AP001INVOICE_TYPE;
                xcprhia1.LEGAL_ENTITY = Cm.initC.AP001LEGAL_ENTITY;
                xcprhia1.TERMS_DATE = inv_date;

                xcprhia1.SOURCE_FROM = "DIRECT_SUP’";
                listXcApIIT.Add(xcprhia1);
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
