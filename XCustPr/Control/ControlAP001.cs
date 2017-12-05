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

        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();

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
            xCSIITDB.DeleteTemp(Cm.initC.AP001PathLog);//  clear temp table     
            
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

                xCSIITDB.insertBluk(rcv, aa, "kfc_po", pB1, Cm.initC.AP001PathLog);
                
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
                dt = xCSIITDB.selectByFilename(rowG[xCSIITDB.xCSIIT.FILE_NAME].ToString());
                foreach(DataRow row in dt.Rows)
                {
                    row1 = 0;
                    cntErr = 0;     //gen log
                    pB1.Minimum = 0;
                    pB1.Maximum = dt.Rows.Count;

                    ValidateFileName vF = new ValidateFileName();   // gen log
                    vF.fileName = row[xCSIITDB.xCSIIT.FILE_NAME].ToString().Trim();   // gen log
                    vF.recordTotal = dt.Rows.Count.ToString();   // gen log

                    //Error AP004-002 : Date Format not correct 
                    chk = validateDate(row[xCSIITDB.xCSIIT.INVOICE_DATE].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCSIITDB.xCSIIT.FILE_NAME].ToString().Trim();
                        vPP.Message = "Error AP004-002 ";
                        vPP.Validate = "row " + row1 + " INVOICE_DATE=" + row[xCSIITDB.xCSIIT.INVOICE_DATE].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error AP001-003 : Not found Store Code
                    subInv_code = Cm.validateSubInventoryCode(Cm.initC.ORGANIZATION_code.Trim(), row[xCSIITDB.xCSIIT.STORE].ToString().Trim(), listXcSIMT);
                    if (subInv_code.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCSIITDB.xCSIIT.FILE_NAME].ToString().Trim();
                        vPP.Message = "Error AP001-003 ";
                        vPP.Validate = "row " + row1 + " STORE=" + row[xCSIITDB.xCSIIT.STORE].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    addXcustApInv(row[xCSIITDB.xCSIIT.INVOICE_NUM].ToString(), row[xCSIITDB.xCSIIT.INVOICE_DATE].ToString(), "", row[xCSIITDB.xCSIIT.FILE_NAME].ToString());
                    addXcustApILIT(row);

                    vF.recordError = cntFileErr.ToString();   // gen log
                    vF.totalError = cntErr.ToString();   // gen log
                    lVfile.Add(vF);   // gen log
                }
            }
        }
        public void processInsertTable(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String pathLog)
        {
            addListView("insert table " + Cm.initC.AP001PathProcess, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            pB1.Minimum = 0;
            pB1.Maximum = listXcApIIT.Count + listXcApILIT.Count+2;
            pB1.Value = 0;
            foreach (XcustApInvIntTbl xcprhia in listXcApIIT)
            {
                pB1.Value++;
                insertXcustApInvIntTbl(xcprhia, date, time);
            }
            foreach (XcustApInvLinesIntTbl xcprlia in listXcApILIT)
            {
                pB1.Value++;
                //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                String chk = xCApILITDB.insert(xcprlia, Cm.initC.AP001PathLog);
            }
        }
        /*
         * เตรียมข้อมูล เพื่อที่จะลง table XCUST_AP_INV_INT_TBL
         * 
         */
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
        private String insertXcustApInvIntTbl(XcustApInvIntTbl xcprhia, String date, String time)
        {
            String chk = "";
            XcustApInvIntTbl xCRHIA = xcprhia;

            chk = xCApIITDB.insert(xCRHIA, Cm.initC.AP001PathLog);
            return chk;
        }
        private void addXcustApILIT(DataRow row)
        {
            XcustApInvLinesIntTbl item = new XcustApInvLinesIntTbl();
            item.INVOICE_ID = row[xCSIITDB.xCSIIT.INVOICE_NUM].ToString();
            item.LINE_NUMBER = "0";
            
            item.INVOICE_TYPE_LOOKUP_CODE = (Double.Parse(row[xCSIITDB.xCSIIT.VAT_AMOUNT].ToString())>0) ? "ITEM":"VAT";     //Line Type
            item.INVOICE_AMOUNT = (Double.Parse(row[xCSIITDB.xCSIIT.VAT_AMOUNT].ToString()) > 0) ? row[xCSIITDB.xCSIIT.BASE_AMOUNT].ToString() : row[xCSIITDB.xCSIIT.VAT_AMOUNT].ToString();
            item.QUANTITY = row[xCSIITDB.xCSIIT.QTY].ToString();
            item.PRICE = row[xCSIITDB.xCSIIT.PRICE].ToString();
            item.DESCRIPTION = "DIRECT_SUP_" + row[xCSIITDB.xCSIIT.STORE].ToString();
            item.PO_NUMBER = row[xCSIITDB.xCSIIT.PO_NUMBER].ToString();
            item.TAX_CLASSIFICATION_CODE = "";
            item.TAX_RATE = "";
            item.AWT_GROUP_NAME = "";
            //item.TAX_REGIME_CODE = row[xCUiIDTDB.xCUiIDT.po_tax_code].ToString();
            //item.AWT_GROUP_NAME = "";       // Withholding Tax Group
            //item.ATTRIBUTE1 = row[xCUiIDTDB.xCUiIDT.PO].ToString();
            //item.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
            //item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            //item.PROCESS_FLAG = "Y";
            //item.UOM_CODE = row[xCLFPTDB.xCLFPT.UOMCODE].ToString();

            listXcApILIT.Add(item); 
        }
        public void processGenCSV(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("processGenCSVxCPRHIA ", "CVS", lv1, form1);
            processGenCSVxCApIIT(lv1, form1, pB1, "AP001");
            addListView("processGenCSVxCPRLIA ", "CVS", lv1, form1);
            processGenCSVxCApILIT(lv1, form1, pB1, "AP001");

            addListView("processGenZIP ", "CVS", lv1, form1);
            processGenZIP(lv1, form1, pB1, "AP001");
        }
        public void processGenCSVxCApIIT(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            var file = Cm.initC.AP001PathArchive + "processGenCSVxCApIIT.csv";
            DataTable dt;
            if (flag.Equals("AP001"))
            {
                dt = xCApIITDB.selectAll();
            }
            else
            {
                dt = xCApIITDB.selectAll();
            }

            addListView("processGenCSVxCApIIT จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCApIITDB.xCAIIT.INVOICE_ID].ToString();
                    string col02 = row[xCApIITDB.xCAIIT.BUSINESS_UNIT].ToString();
                    string col03 = row[xCApIITDB.xCAIIT.SOURCE].ToString();
                    string col04 = row[xCApIITDB.xCAIIT.INVOICE_NUM].ToString();
                    string col05 = "col05";              //*Invoice Amount
                    string col06 = row[xCApIITDB.xCAIIT.INVOICE_DATE].ToString();
                    string col07 = row[xCApIITDB.xCAIIT.VENDOR_NAME].ToString();
                    string col08 = row[xCApIITDB.xCAIIT.VENDOR_NUMBER].ToString();
                    string col09 = row[xCApIITDB.xCAIIT.VENDOR_SITE_CODE].ToString();
                    string col10 = "col10";

                    string col11 = row[xCApIITDB.xCAIIT.PAYMENT_CURR_CODE].ToString();
                    string col12 = row[xCApIITDB.xCAIIT.DESCRIPTION].ToString();
                    string col13 = "col13";     //Import Set
                    string col14 = row[xCApIITDB.xCAIIT.INVOICE_TYPE_LOOKUP_CODE].ToString();
                    string col15 = row[xCApIITDB.xCAIIT.LEGAL_ENTITY].ToString();
                    string col16 = "col16";     //Customer Tax Registration Number
                    string col17 = "";      //Customer Registration Code
                    string col18 = "";      //First-Party Tax Registration Number
                    string col19 = "";      //Supplier Tax Registration Number
                    string col20 = row[xCApIITDB.xCAIIT.PAYMENT_METHOD].ToString();     //*Payment Terms

                    string col21 = "";      //Terms Date
                    string col22 = "";      //Goods Received Date
                    string col23 = "";      //Invoice Received Date
                    string col24 = "";      //Accounting Date
                    string col25 = "";      //Payment Method
                    string col26 = "";      //Pay Group
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
                    string col49 = "";        //Settlement Priority
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
                    string col74 = "";      //Attribute Category
                    string col75 = row[xCApIITDB.xCAIIT.ATTRIBUTE1].ToString();
                    string col76 = row[xCApIITDB.xCAIIT.ATTRIBUTE2].ToString();
                    string col77 = row[xCApIITDB.xCAIIT.ATTRIBUTE3].ToString();
                    string col78 = row[xCApIITDB.xCAIIT.ATTRIBUTE4].ToString();
                    string col79 = row[xCApIITDB.xCAIIT.ATTRIBUTE5].ToString();
                    string col80 = row[xCApIITDB.xCAIIT.ATTRIBUTE6].ToString();

                    string col81 = row[xCApIITDB.xCAIIT.ATTRIBUTE7].ToString();
                    string col82 = row[xCApIITDB.xCAIIT.ATTRIBUTE8].ToString();
                    string col83 = row[xCApIITDB.xCAIIT.ATTRIBUTE9].ToString();
                    string col84 = row[xCApIITDB.xCAIIT.ATTRIBUTE10].ToString();
                    string col85 = row[xCApIITDB.xCAIIT.ATTRIBUTE11].ToString();
                    string col86 = row[xCApIITDB.xCAIIT.ATTRIBUTE12].ToString();
                    string col87 = row[xCApIITDB.xCAIIT.ATTRIBUTE13].ToString();
                    string col88 = row[xCApIITDB.xCAIIT.ATTRIBUTE14].ToString();
                    string col89 = row[xCApIITDB.xCAIIT.ATTRIBUTE15].ToString();
                    string col90 = "";      //Attribute Number 1

                    string col91 = "";
                    string col92 = "";
                    string col93 = "";
                    string col94 = "";      //Attribute Number 5
                    string col95 = "";      //Attribute Date 1
                    string col96 = "";
                    string col97 = "";
                    string col98 = "";
                    string col99 = "";
                    string col100 = "";     //Global Attribute Category

                    string col101 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE1].ToString();
                    string col102 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE2].ToString();
                    string col103 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE3].ToString();
                    string col104 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE4].ToString();
                    string col105 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE5].ToString();
                    string col106 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE6].ToString();
                    string col107 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE7].ToString();
                    string col108 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE8].ToString();
                    string col109 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE9].ToString();
                    string col110 = "";     //Global Attribute 10

                    string col111 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE11].ToString();
                    string col112 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE12].ToString();
                    string col113 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE13].ToString();
                    string col114 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE14].ToString();
                    string col115 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE15].ToString();
                    string col116 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE16].ToString();
                    string col117 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE17].ToString();
                    string col118 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE18].ToString();
                    string col119 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE19].ToString();
                    string col120 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE20].ToString();

                    string col121 = "";     //Global Attribute Number 1
                    string col122 = "";
                    string col123 = "";
                    string col124 = "";
                    string col125 = "";
                    string col126 = "";     //Global Attribute Date 1
                    string col127 = "";
                    string col128 = "";
                    string col129 = "";
                    string col130 = "";
                    
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
                        + "," + col41 + "," + col42 + "," + col43 + "," + col44 + "," + col45 + "," + col46 + "," + col47 + "," + col48 + "," + col49 + "," + col50
                        + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
                        + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70
                        + "," + col71 + "," + col72 + "," + col73 + "," + col74 + "," + col75 + "," + col76 + "," + col77 + "," + col78 + "," + col79 + "," + col80
                        + "," + col81 + "," + col82 + "," + col83 + "," + col84 + "," + col85 + "," + col86 + "," + col87 + "," + col88 + "," + col89 + "," + col90
                        + "," + col91 + "," + col92 + "," + col93 + "," + col94 + "," + col95 + "," + col96 + "," + col97 + "," + col98 + "," + col99 + "," + col100
                        + "," + col101 + "," + col102 + "," + col103 + "," + col104 + "," + col105 + "," + col106 + "," + col107 + "," + col108 + "," + col109 + "," + col110
                        + "," + col111 + "," + col112 + "," + col113 + "," + col114 + "," + col115 + "," + col116 + "," + col117 + "," + col118 + "," + col119 + "," + col120
                        + "," + col121 + "," + col122 + "," + col123 + "," + col124 + "," + col125 + "," + col126 + "," + col127 + "," + col128 + "," + col129 + "," + col130;

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenCSVxCApILIT(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            var file = Cm.initC.AP001PathArchive + "processGenCSVxCApILIT.csv";
            DataTable dt;
            if (flag.Equals("AP001"))
            {
                dt = xCApILITDB.selectAll();
            }
            else
            {
                dt = xCApILITDB.selectAll();
            }

            addListView("processGenCSVxCApILIT จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCApILITDB.xCAILIT.INVOICE_ID].ToString();
                    string col02 = row[xCApILITDB.xCAILIT.LINE_NUMBER].ToString();
                    string col03 = "col03";     //*Line Type
                    string col04 = row[xCApILITDB.xCAILIT.INVOICE_AMOUNT].ToString();     //*Amount
                    string col05 = row[xCApILITDB.xCAILIT.QUANTITY].ToString();
                    string col06 = row[xCApILITDB.xCAILIT.PRICE].ToString();
                    string col07 = "col07";     //UOM
                    string col08 = row[xCApILITDB.xCAILIT.DESCRIPTION].ToString();
                    string col09 = row[xCApILITDB.xCAILIT.PO_NUMBER].ToString();
                    string col10 = row[xCApILITDB.xCAILIT.PO_LINE_NUMBER].ToString();

                    string col11 = "col11";      //PO Schedule Number
                    string col12 = "col12";        //PO Distribution Number
                    string col13 = "col13";     //Item Description
                    string col14 = "col14";       //PO Release Number
                    string col15 = "col15";       //Purchasing Category
                    string col16 = row[xCApILITDB.xCAILIT.RECEIPT_NUMBER].ToString();     //Receipt Number
                    string col17 = row[xCApILITDB.xCAILIT.RECEIPT_LINE_NUMBER].ToString();      //
                    string col18 = "";      //Consumption Advice Number
                    string col19 = "";      //Consumption Advice Line Number
                    string col20 = "col20";     //Packing Slip

                    string col21 = "";      //Final Match
                    string col22 = "";      //Distribution Combination
                    string col23 = "";      //Distribution Set
                    string col24 = "";      //Accounting Date
                    string col25 = "";      //Overlay Account Segment
                    string col26 = "";      //Overlay Primary Balancing Segment
                    string col27 = "";      //Overlay Cost Center Segment
                    string col28 = "";      //Tax Classification Code
                    string col29 = "";      //Ship-to Location
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
                    string col49 = "";        //Settlement Priority
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
                    string col67 = row[xCApILITDB.xCAILIT.ATTRIBUTE1].ToString();
                    string col68 = row[xCApILITDB.xCAILIT.ATTRIBUTE2].ToString();
                    string col69 = row[xCApILITDB.xCAILIT.ATTRIBUTE3].ToString();
                    string col70 = row[xCApILITDB.xCAILIT.ATTRIBUTE4].ToString();

                    string col71 = row[xCApILITDB.xCAILIT.ATTRIBUTE5].ToString();
                    string col72 = row[xCApILITDB.xCAILIT.ATTRIBUTE6].ToString();
                    string col73 = row[xCApILITDB.xCAILIT.ATTRIBUTE7].ToString();
                    string col74 = row[xCApILITDB.xCAILIT.ATTRIBUTE8].ToString();
                    string col75 = row[xCApILITDB.xCAILIT.ATTRIBUTE9].ToString();
                    string col76 = row[xCApILITDB.xCAILIT.ATTRIBUTE10].ToString();
                    string col77 = row[xCApILITDB.xCAILIT.ATTRIBUTE11].ToString();
                    string col78 = row[xCApILITDB.xCAILIT.ATTRIBUTE12].ToString();
                    string col79 = row[xCApILITDB.xCAILIT.ATTRIBUTE13].ToString();
                    string col80 = row[xCApILITDB.xCAILIT.ATTRIBUTE14].ToString();

                    string col81 = row[xCApILITDB.xCAILIT.ATTRIBUTE15].ToString();
                    string col82 = "";       //Attribute Number 1
                    string col83 = "";
                    string col84 = "";
                    string col85 = "";
                    string col86 = "";
                    string col87 = "";       //Attribute Date 1
                    string col88 = "";
                    string col89 = "";
                    string col90 = "";      //

                    string col91 = "";
                    string col92 = "";      //Global Attribute Category
                    string col93 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE1].ToString();      //Global Attribute 1
                    string col94 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE2].ToString();
                    string col95 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE3].ToString();
                    string col96 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE4].ToString();
                    string col97 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE5].ToString();
                    string col98 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE6].ToString();
                    string col99 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE7].ToString();
                    string col100 = row[xCApILITDB.xCAILIT.GLOBAL_ATTRIBUTE8].ToString();

                    string col101 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE9].ToString();
                    string col102 = "";     //Global Attribute 10
                    string col103 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE11].ToString();
                    string col104 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE12].ToString();
                    string col105 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE13].ToString();
                    string col106 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE14].ToString();
                    string col107 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE15].ToString();
                    string col108 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE16].ToString();
                    string col109 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE17].ToString();
                    string col110 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE19].ToString();

                    string col111 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE20].ToString();
                    string col112 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE19].ToString();
                    string col113 = row[xCApIITDB.xCAIIT.GLOBAL_ATTRIBUTE20].ToString();
                    string col114 = "";     //Global Attribute Number 1
                    string col115 = "";
                    string col116 = "";
                    string col117 = "";
                    string col118 = "";     //Global Attribute Number 5
                    string col119 = "";     //Global Attribute Date 1
                    string col120 = "";

                    string col121 = "";     //
                    string col122 = "";     // Global Attribute Date 5
                    string col123 = "";
                    string col124 = "";
                    string col125 = "";
                    string col126 = "";     //Global Attribute Date 1
                    string col127 = "";
                    string col128 = "";
                    string col129 = "";
                    string col130 = "";

                    string col131 = "";     //
                    string col132 = "";     // 
                    string col133 = "";
                    string col134 = "";
                    string col135 = "";
                    string col136 = "";     //
                    string col137 = "";
                    string col138 = "";
                    string col139 = "";
                    string col140 = "";

                    string col141 = "";     //
                    string col142 = "";     // 
                    string col143 = "";
                    string col144 = "";
                    string col145 = "";
                    string col146 = "";     //
                    string col147 = "";
                    string col148 = "";
                    string col149 = "";
                    string col150 = "";

                    string col151 = "";     //
                    string col152 = "";     // 
                    string col153 = "";
                    string col154 = "";
                    string col155 = "";
                    string col156 = "";     //
                    string col157 = "";

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
                        + "," + col41 + "," + col42 + "," + col43 + "," + col44 + "," + col45 + "," + col46 + "," + col47 + "," + col48 + "," + col49 + "," + col50
                        + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
                        + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70
                        + "," + col71 + "," + col72 + "," + col73 + "," + col74 + "," + col75 + "," + col76 + "," + col77 + "," + col78 + "," + col79 + "," + col80
                        + "," + col81 + "," + col82 + "," + col83 + "," + col84 + "," + col85 + "," + col86 + "," + col87 + "," + col88 + "," + col89 + "," + col90
                        + "," + col91 + "," + col92 + "," + col93 + "," + col94 + "," + col95 + "," + col96 + "," + col97 + "," + col98 + "," + col99 + "," + col100
                        + "," + col101 + "," + col102 + "," + col103 + "," + col104 + "," + col105 + "," + col106 + "," + col107 + "," + col108 + "," + col109 + "," + col110
                        + "," + col111 + "," + col112 + "," + col113 + "," + col114 + "," + col115 + "," + col116 + "," + col117 + "," + col118 + "," + col119 + "," + col120
                        + "," + col121 + "," + col122 + "," + col123 + "," + col124 + "," + col125 + "," + col126 + "," + col127 + "," + col128 + "," + col129 + "," + col130
                        + "," + col131 + "," + col132 + "," + col133 + "," + col134 + "," + col135 + "," + col136 + "," + col137 + "," + col138 + "," + col139 + "," + col130
                        + "," + col141 + "," + col142 + "," + col143 + "," + col144 + "," + col145 + "," + col146 + "," + col147 + "," + col148 + "," + col149 + "," + col150
                        + "," + col151 + "," + col152 + "," + col153 + "," + col154 + "," + col155 + "," + col156 + "," + col157 ;

                    stream.WriteLine(csvRow);
                }
            }
        }

        public void processGenZIP(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            addListView("create zip file " + Cm.initC.PathProcess, "Validate", lv1, form1);
            String filenameZip = "", ilename2 = "", ilename3 = "", filename = "";
            if (flag.Equals("AP001"))
            {
                filenameZip = Cm.initC.PathFileCSV + "\\xcustpr.zip";
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
