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
    public class ControlPO004
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

        public ControlPO004(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            conn = new ConnectDB("kfc_po", Cm.initC);        //standard

            vPrPo = new ValidatePrPo();
            Cm.createFolderPO004();
            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard

            
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
         * 1.5.1	File DRT MMX ที่ส่งเข้าจะแยก File ตาม Store Code
         * 1.5.2	ระบบจะวางชุดไฟล์ MX Interface ไว้ที่ Folder MX (.zip) ไว้ที่ Folder MX Interface Ex. KFCSDC-2017-08-28.zip
         * 1.5.3	โปรแกรมจะต้องทำการแตก Zip File และนำ File (01202drt.507) โดยเลือกประเภทไฟล์ที่ต้องการไปวางที่ Folder Initial เอง ความหมายของชื่อไฟล์ตามด้านล่าง
         * -	01202 คือ รหัสสาขา
         * -	drt คือ ประเภทไฟล์
         * -	5 คือ เดือน กรณีที่เป็น a=October, b=November, c=December
         * -	07 คือ วันที่
         * 1.	ระบบ ERP จะเข้าไป SFTP file จากระบบงาน MMX และนำ File มาวางไว้ที่ Server ตาม Path Parameter Path Initial
         * 2.	อ่านเฉพาะ File Type ที่ระบุตาม parameter file type
         * 3.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process
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

            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
            addListView("อ่าน fileจาก" + Cm.initC.PO004PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.PO004PathProcess + aa.Replace(Cm.initC.PO004PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            //xCLPRITDB.DeleteMmxTemp();//  clear temp table     
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_MMX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert xcust_mmx_pr_int_tbl
            filePOProcess = Cm.getFileinFolder(Cm.initC.PO004PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.PO004PathProcess, "", lv1, form1);
            foreach (string aa in filePOProcess)
            {
                List<String> rcv = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL   
                pB1.Visible = true;
                //xCLPRITDB.insertBluk(rcv, aa, "kfc_po", pB1);
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
    }
}
