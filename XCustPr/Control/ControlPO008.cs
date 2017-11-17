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
    public class ControlPO008
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
        private String dateStart = "";      //gen log
        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();

        public XcustBuMstTblDB xCBMTDB;
        public XcustDeriverLocatorMstTblDB xCDLMTDB;
        public XcustDeriverOrganizationMstTblDB xCDOMTDB;
        public XcustSubInventoryMstTblDB xCSIMTDB;
        public XcustItemMstTblDB xCIMTDB;
        public XcustCurrencyMstTblDB xCMTDB;
        public XcustSupplierMstTblDB xCSMTDB;
        public XcustUomMstTblDB xCUMTDB;
        public XcustValueSetMstTblDB xCVSMTDB;
        public XcustBlanketAgreementHeaderTblDB xCBAHTDB;
        public XcustBlanketAgreementLinesTblDB xCBALTDB;

        private List<XcustPorReqHeaderIntAll> listXcustPRHIA;
        private List<XcustPorReqLineIntAll> listXcustPRLIA;
        private List<XcustPorReqDistIntAll> listXcustPRDIA;

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;

        public XcustCedarPoIntTblDB xCCPITDB;


        public ControlPO008(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            vPrPo = new ValidatePrPo();

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard
            Cm.createFolderPO008();
            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard

            xCCPITDB = new XcustCedarPoIntTblDB(conn, Cm.initC);

            listXcSIMT = new List<XcustSubInventoryMstTbl>();
            listXcustPRHIA = new List<XcustPorReqHeaderIntAll>();
            listXcustPRLIA = new List<XcustPorReqLineIntAll>();
            listXcustPRDIA = new List<XcustPorReqDistIntAll>();

            listXcIMT = new List<XcustItemMstTbl>();
            listXcSMT = new List<XcustSupplierMstTbl>();
            listXcVSMT = new List<XcustValueSetMstTbl>();
            listXcUMT = new List<XcustUomMstTbl>();

        }
        /*
         * a.	USER จะเข้าไป Download File จากระบบงาน CEDAR และนำ File มาวางไว้ที่ Server ตาม Path Parameter Path Initial
         * b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
         * c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_CEDAR_PO_TBL ด้วย Validate Flag = ‘N’ ,PROCESS_FLAG = ‘N’
         */
        public void processCedarPOtoErpPR(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            ReadExcel re = new ReadExcel(Cm.initC);
            String[] filePOProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;

            dateStart = date + " " + time;       //gen log

            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
            addListView("อ่าน fileจาก" + Cm.initC.PO008PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.PO008PathProcess + aa.Replace(Cm.initC.PO008PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            xCCPITDB.DeleteCedarTemp();//  clear temp table
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_MMX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert xcust_mmx_pr_int_tbl
            filePOProcess = Cm.getFileinFolder(Cm.initC.PO008PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.PO008PathProcess, "", lv1, form1);
            foreach (string aa in filePOProcess)
            {
                List<String> mmx = re.ReadExcelPO008(aa, pB1);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL
                pB1.Visible = true;
                xCCPITDB.insertBluk(mmx, aa, "kfc_po", pB1);
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
        /*
         * check amount ว่า data type ถูกต้องไหม
         * ที่ใช้ decimal.tryparse เพราะ ใน database เป็น decimal(18,0)
         * Error PO008-006 : Invalid data type
         */
        public Boolean validateAmount(String qty)
        {
            Boolean chk = false;
            int i = 0;
            chk = int.TryParse(qty, out i);
            return chk;
        }
        private String validateSubInventoryCode(String ordId, String StoreCode)
        {
            String chk = "";
            foreach (XcustSubInventoryMstTbl item in listXcSIMT)
            {
                if (item.ORGAINZATION_ID.Equals(ordId.Trim()))
                {
                    if (item.attribute1.Equals(StoreCode.Trim()))
                    {
                        chk = item.SECONDARY_INVENTORY_NAME;
                        break;
                    }
                }
            }
            return chk;
        }
        private Boolean validateItemCodeByOrgRef(String ordId, String item_code)
        {
            Boolean chk = false;
            foreach (XcustItemMstTbl item in listXcIMT)
            {
                if (item.ORGAINZATION_ID.Equals(ordId.Trim()))
                {
                    if (item.ITEM_REFERENCE1.Equals(item_code.Trim()))
                    {
                        chk = true;
                        break;
                    }
                }
            }
            return chk;
        }
        private Boolean validateSupplierBySupplierCode(String supplier_code)
        {
            Boolean chk = false;
            foreach (XcustSupplierMstTbl item in listXcSMT)
            {
                if (item.SUPPLIER_NUMBER.Equals(supplier_code.Trim()))
                {
                    chk = true;
                    break;
                }
            }
            return chk;
        }
        /*
         * check แค่ format ว่า เป็น yyyymmdd เท่านั้น
         * Error PO008-002 : Date Format not correct 
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
                    sYear.Append(date.Substring(0, 4));
                    sMonth.Append(date.Substring(4, 2));
                    sDay.Append(date.Substring(6, 2));
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
         * d.	จากนั้น Program จะเอาข้อมูลจาก Table XCUST_CEDAR_PO_TBL มาทำการ Validate 
         * e.	กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_PO_HEADER_INT_ALL ,XCUST_PO_LINE_INT_ALL , XCUST_PO_LINE_LOC_INT_ALL ,XCUST_PO_DIST_INT_ALL และ Update Validate_flag = ‘Y’
         */
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

            listXcustPRHIA.Clear();
            listXcustPRLIA.Clear();
            listXcustPRDIA.Clear();
            getListXcSIMT();
            getListXcIMT();
            getListXcSMT();
            getListXcVSMT();
            getListXcUMT();
            int row1 = 0;
            int cntErr = 0, cntFileErr = 0;   // gen log

            buCode = xCBMTDB.selectActive1();
            //Error PO001-004 : Invalid Requisitioning BU
            if (!buCode.Equals(Cm.initC.BU_NAME.Trim()))
            {
                chk = false;
            }
            //Error PO001-008 : Invalid Deliver To Location
            locator = xCDLMTDB.selectLocator1();
            if (!locator.Equals(Cm.initC.Locator.Trim()))
            {
                chk = false;
            }
            //Error PO001-009 : Invalid Deliver-to Organization
            Org = xCDOMTDB.selectActive1();
            if (!Org.Equals(Cm.initC.ORGANIZATION_code.Trim()))
            {
                chk = false;
            }

            //Error PO001-013 : Invalid Currency Code
            if (!xCMTDB.validateCurrencyCodeBycurrCode(Cm.initC.CURRENCY_CODE))
            {
                chk = false;
            }
            //Error PO008-014 : Invalid Procurement BU
            if (!buCode.Equals(Cm.initC.BU_NAME.Trim()))
            {
                chk = false;
            }


            //StringBuilder filename = new StringBuilder();
            dtGroupBy = xCCPITDB.selectCedarGroupByFilename();//   ดึง filename
            foreach (DataRow rowG in dtGroupBy.Rows)
            {
                addListView("ดึงข้อมูล  " + rowG[xCCPITDB.xCCPIT.file_name].ToString().Trim(), "Validate", lv1, form1);
                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = rowG[xCCPITDB.xCCPIT.file_name].ToString().Trim();   // gen log
                vF.recordTotal = dt.Rows.Count.ToString();   // gen log
                dt = xCCPITDB.selectCedarByFilename(rowG[xCCPITDB.xCCPIT.file_name].ToString().Trim());    // ข้อมูลใน file

                row1 = 0;
                cntErr = 0;     //gen log
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;

                foreach (DataRow row in dt.Rows)
                {
                    row1++;
                    pB1.Value = row1;
                    //Error PO008-006 : Invalid data type
                    chk = validateAmount(row[xCCPITDB.xCCPIT.amt].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-006 ";
                        vPP.Validate = "row " + row1 + " amt=" + row[xCCPITDB.xCCPIT.amt].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    chk = validateAmount(row[xCCPITDB.xCCPIT.total].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-006 ";
                        vPP.Validate = "row " + row1 + " total=" + row[xCCPITDB.xCCPIT.total].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    chk = validateAmount(row[xCCPITDB.xCCPIT.vat].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-006 ";
                        vPP.Validate = "row " + row1 + " vat=" + row[xCCPITDB.xCCPIT.vat].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    chk = validateAmount(row[xCCPITDB.xCCPIT.amt].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-006 ";
                        vPP.Validate = "row " + row1 + " amt=" + row[xCCPITDB.xCCPIT.amt].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO008-002 : Date Format not correct 
                    chk = validateDate(row[xCCPITDB.xCCPIT.admin_receipt_doc_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-002 ";
                        vPP.Validate = "row " + row1 + " admin_receipt_doc_date=" + row[xCCPITDB.xCCPIT.admin_receipt_doc_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    chk = validateDate(row[xCCPITDB.xCCPIT.approve_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-002 ";
                        vPP.Validate = "row " + row1 + " approve_date=" + row[xCCPITDB.xCCPIT.approve_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    chk = validateDate(row[xCCPITDB.xCCPIT.cedar_close_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-002 ";
                        vPP.Validate = "row " + row1 + " cedar_close_date=" + row[xCCPITDB.xCCPIT.cedar_close_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO001-010 : Invalid Subinventory Code      //ไม่มี store code              
                    //subInv_code = validateSubInventoryCode(Cm.initC.ORGANIZATION_code.Trim(), row[xCCPITDB.xCCPIT.store_code].ToString().Trim());
                    //if (subInv_code.Equals(""))
                    //{
                    //    vPP = new ValidatePrPo();
                    //    vPP.Filename = rowG[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                    //    vPP.Message = "Error PO001-010 ";
                    //    vPP.Validate = "row " + row1 + " ORGANIZATION_code " + Cm.initC.ORGANIZATION_code.Trim();
                    //    lVPr.Add(vPP);
                    //    cntErr++;
                    //}
                    // Error PO008 - 011 : Invalid Item Number     //ในเอกสาร  Item Description    
                    //if (!xCIMTDB.validateItemCodeByOrgRef1("300000000949654", row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim()))// ต้องแก้ Fix code อยู่
                    //if (validateItemCodeByOrgRef("300000000949654", row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim()))// ต้องแก้ Fix code อยู่
                    //{
                    //    vPP = new ValidatePrPo();
                    //    vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                    //    vPP.Message = "Error PO001-011 ";
                    //    vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " item_code " + row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim();
                    //    lVPr.Add(vPP);
                    //    cntErr++;
                    //}
                    // Error PO001-015 : Invalid Supplier
                    //if (!xCSMTDB.validateSupplierBySupplierCode(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim()))
                    if (validateSupplierBySupplierCode(row[xCCPITDB.xCCPIT.supplier_code].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCCPITDB.xCCPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO008-015 ";
                        vPP.Validate = "row " + row1 + " supplier_code " + row[xCCPITDB.xCCPIT.supplier_code].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;
                    }
                }

            }
        }
        public void processGenCSVxCRHIADB(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            //var file = Cm.initC.PO003PathArchive + "PorReqHeadersInterfaceAl.csv";
            //DataTable dt;
            //if (flag.Equals("PO003"))
            //{
            //    dt = xCRHIADB.selectAll();
            //}
            //else
            //{
            //    dt = xCRHIADB.selectAll();
            //}

            //addListView("processGenCSVxCRHIADB จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            //using (var stream = File.CreateText(file))
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        string col01 = row[xCRHIADB.xCRHIA.HEADER_INTERFACE_NUMBER].ToString();
            //        string col02 = "col02";      //action        รอถาม  
            //        string col03 = "col03";      //batch_id      รอถาม  
            //        string col04 = row[xCRHIADB.xCRHIA.IMPORT_SOURCE].ToString();
            //        string col05 = "col05";//Approval Action       รอถาม  
            //        string col06 = "col06";      //Order       รอถาม  
            //        string col07 = "col07";//Document Type Code      รอถาม  
            //        string col08 = "col08";//Style       รอถาม 
            //        string col09 = "col09";//Procurement BU       รอถาม 
            //        string col10 = "col10";//Requisitioning BU       รอถาม 

            //        string col11 = "col11";//Sold-to Legal Entity รอถาม  
            //        string col12 = "col12";//Bill-to BU
            //        string col13 = "col13";//Buyer
            //        string col14 = "col14";//Currency Code
            //        string col15 = "col15";//Rate
            //        string col16 = "col16";//Rate Type
            //        string col17 = "col17";//Rate Date
            //        string col18 = "col18";//Description
            //        string col19 = "col19";//Bill-to Location
            //        string col20 = "col20";//Ship-to Location

            //        string col21 = "col21";//Supplier
            //        string col22 = "col22";//Supplier Number
            //        string col23 = "col23";//Supplier Site
            //        string col24 = "col24";//Supplier Contact
            //        string col25 = "col25";//Supplier Order
            //        string col26 = "col26";//FOB
            //        string col27 = "col27";//Carrier
            //        string col28 = "col28";//Freight Terms
            //        string col29 = "col29";//Pay On Code
            //        string col30 = "col30";//Payment Terms

            //        string col31 = "col31";//Initiating Party
            //        string col32 = "col32";//Change Order Description
            //        string col33 = "col33";//Required Acknowledgment
            //        string col34 = "col34";//Acknowledge Within (Days)
            //        string col35 = "col35";//Communication Method
            //        string col36 = "col36";//Fax
            //        string col37 = "col37";//E-mail
            //        string col38 = "col38";//Confirming order
            //        string col39 = "col39";//Note to Supplier
            //        string col40 = "col40";//Note to Receiver

            //        string col41 = "col41";//Default Taxation Country Code
            //        string col42 = "col42";//Tax Document Subtype Code
            //        string col43 = row[xCRHIADB.xCRHIA.ATTRIBUTE_CATEGORY].ToString();
            //        string col44 = row[xCRHIADB.xCRHIA.ATTRIBUTE1].ToString();
            //        string col45 = row[xCRHIADB.xCRHIA.ATTRIBUTE2].ToString();
            //        string col46 = row[xCRHIADB.xCRHIA.ATTRIBUTE3].ToString();
            //        string col47 = row[xCRHIADB.xCRHIA.ATTRIBUTE4].ToString();
            //        string col48 = row[xCRHIADB.xCRHIA.ATTRIBUTE5].ToString();
            //        string col49 = row[xCRHIADB.xCRHIA.ATTRIBUTE6].ToString();
            //        string col50 = row[xCRHIADB.xCRHIA.ATTRIBUTE7].ToString();

            //        string col51 = row[xCRHIADB.xCRHIA.ATTRIBUTE8].ToString();
            //        string col52 = row[xCRHIADB.xCRHIA.ATTRIBUTE9].ToString();
            //        string col53 = row[xCRHIADB.xCRHIA.ATTRIBUTE10].ToString();
            //        string col54 = row[xCRHIADB.xCRHIA.ATTRIBUTE11].ToString();
            //        string col55 = row[xCRHIADB.xCRHIA.ATTRIBUTE12].ToString();
            //        string col56 = row[xCRHIADB.xCRHIA.ATTRIBUTE13].ToString();
            //        string col57 = row[xCRHIADB.xCRHIA.ATTRIBUTE14].ToString();
            //        string col58 = row[xCRHIADB.xCRHIA.ATTRIBUTE15].ToString();
            //        string col59 = row[xCRHIADB.xCRHIA.ATTRIBUTE16].ToString();
            //        string col60 = row[xCRHIADB.xCRHIA.ATTRIBUTE17].ToString();

            //        string col61 = row[xCRHIADB.xCRHIA.ATTRIBUTE18].ToString();
            //        string col62 = row[xCRHIADB.xCRHIA.ATTRIBUTE19].ToString();
            //        string col63 = row[xCRHIADB.xCRHIA.ATTRIBUTE20].ToString();
            //        string col64 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE1].ToString();
            //        string col65 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE2].ToString();
            //        string col66 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE3].ToString();
            //        string col67 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE4].ToString();
            //        string col68 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE5].ToString();
            //        string col69 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE6].ToString();
            //        string col70 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE7].ToString();

            //        string col71 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE8].ToString();
            //        string col72 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE9].ToString();
            //        string col73 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE10].ToString();
            //        string col74 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER1].ToString();
            //        string col75 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER2].ToString();
            //        string col76 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER3].ToString();
            //        string col77 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER4].ToString();
            //        string col78 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER5].ToString();
            //        string col79 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER6].ToString();
            //        string col80 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER7].ToString();

            //        string col81 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER8].ToString();
            //        string col82 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER9].ToString();
            //        string col83 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER10].ToString();
            //        string col84 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP1].ToString();
            //        string col85 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP2].ToString();
            //        string col86 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP3].ToString();
            //        string col87 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP4].ToString();
            //        string col88 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP5].ToString();
            //        string col89 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP6].ToString();
            //        string col90 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP7].ToString();

            //        string col91 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP8].ToString();
            //        string col92 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP9].ToString();
            //        string col93 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP10].ToString();
            //        string col94 = "col94";       //Buyer E-mail
            //        string col95 = "col95";       //Mode of Transport
            //        string col96 = "col96";       //Service Level
            //        string col97 = "col97";       //First Party Tax Registration Number
            //        string col98 = "col98";       //Third Party Tax Registration Number
            //        string col99 = "col99";       //Buyer Managed Transportation

            //        //string col71 = "col71";

            //        //string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}," +
            //        //    "{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}," +
            //        //    "{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},{60}," +
            //        //    "{61},{62},{63},{64},{65},{66},{67},{68},{69},{70},{71}", 
            //        //    col01, col02, col03, col04, col05, col06, col07, col08, col09, col10,
            //        //    col11, col12, col13, col14, col15, col16, col17, col18, col19, col20,
            //        //    col21, col22, col23, col24, col25, col26, col27, col28, col29, col30,
            //        //    col41, col42, col43, col44, col45, col46, col47, col48, col49, col50,
            //        //    col51, col52, col53, col54, col55, col56, col57, col58, col59, col60,
            //        //    col61, col62, col63, col64, col65, col66, col67, col68, col69, col70, col71);

            //        string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10
            //            + "," + col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
            //            + "," + col21 + "," + col22 + "," + col23 + "," + col24 + "," + col25 + "," + col26 + "," + col27 + "," + col28 + "," + col29 + "," + col30
            //            + "," + col31 + "," + col32 + "," + col33 + "," + col34 + "," + col35 + "," + col36 + "," + col37 + "," + col38 + "," + col39 + "," + col40
            //            + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
            //            + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70
            //            + "," + col71 + "," + col72 + "," + col73 + "," + col74 + "," + col75 + "," + col76 + "," + col77 + "," + col78 + "," + col79 + "," + col80
            //            + "," + col81 + "," + col82 + "," + col83 + "," + col84 + "," + col85 + "," + col86 + "," + col87 + "," + col88 + "," + col89 + "," + col90
            //            + "," + col91 + "," + col92 + "," + col93 + "," + col94 + "," + col95 + "," + col96 + "," + col97 + "," + col98 + "," + col99;

            //        stream.WriteLine(csvRow);
            //    }
            //}
        }
        public void processGenCSVxCRTIADB(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            //var file = Cm.initC.PO003PathArchive + "PorReqHeadersInterfaceAl.csv";
            //DataTable dt;
            //if (flag.Equals("PO003"))
            //{
            //    dt = xCRTIADB.selectAll();
            //}
            //else
            //{
            //    dt = xCRTIADB.selectAll();
            //}

            //addListView("processGenCSVxCRTIADB จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            //using (var stream = File.CreateText(file))
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        string col01 = row[xCRTIADB.xCRTIA.HEADER_INTERFACE_NUMBER].ToString();     //Interface Line Key
            //        string col02 = row[xCRTIADB.xCRTIA.INTERFACE_SOURCE_CODE].ToString();      //Interface Header Key        รอถาม  
            //        string col03 = "col03";      //Action      รอถาม  
            //        string col04 = "col04";       //Line
            //        string col05 = "col05";//Line Type       รอถาม  
            //        string col06 = "col06";      //Item       รอถาม  
            //        string col07 = "col07";//Item Description      รอถาม  
            //        string col08 = "col08";//Item Revision       รอถาม 
            //        string col09 = "col09";//Category Name       รอถาม 
            //        string col10 = "col10";//Amount       รอถาม 

            //        string col11 = "col11";//Quantity รอถาม  
            //        string col12 = "col12";//UOM
            //        string col13 = "col13";//Price
            //        string col14 = "col14";//Secondary Quantity
            //        string col15 = "col15";//Secondary UOM
            //        string col16 = "col16";//Supplier Item
            //        string col17 = "col17";//Negotiated
            //        string col18 = "col18";//Hazard Class
            //        string col19 = "col19";//UN Number
            //        string col20 = "col20";//Note to Supplier 

            //        string col21 = "col21";//Note to Receiver
            //        string col22 = row[xCRHIADB.xCRHIA.ATTRIBUTE_CATEGORY].ToString();
            //        string col23 = row[xCRHIADB.xCRHIA.ATTRIBUTE1].ToString();
            //        string col24 = row[xCRHIADB.xCRHIA.ATTRIBUTE2].ToString();
            //        string col25 = row[xCRHIADB.xCRHIA.ATTRIBUTE3].ToString();
            //        string col26 = row[xCRHIADB.xCRHIA.ATTRIBUTE4].ToString();
            //        string col27 = row[xCRHIADB.xCRHIA.ATTRIBUTE5].ToString();
            //        string col28 = row[xCRHIADB.xCRHIA.ATTRIBUTE6].ToString();
            //        string col29 = row[xCRHIADB.xCRHIA.ATTRIBUTE7].ToString();
            //        string col30 = row[xCRHIADB.xCRHIA.ATTRIBUTE8].ToString();

            //        string col31 = row[xCRHIADB.xCRHIA.ATTRIBUTE9].ToString();
            //        string col32 = row[xCRHIADB.xCRHIA.ATTRIBUTE10].ToString();
            //        string col33 = row[xCRHIADB.xCRHIA.ATTRIBUTE11].ToString();
            //        string col34 = row[xCRHIADB.xCRHIA.ATTRIBUTE12].ToString();
            //        string col35 = row[xCRHIADB.xCRHIA.ATTRIBUTE13].ToString();
            //        string col36 = row[xCRHIADB.xCRHIA.ATTRIBUTE14].ToString();
            //        string col37 = row[xCRHIADB.xCRHIA.ATTRIBUTE15].ToString();
            //        string col38 = row[xCRHIADB.xCRHIA.ATTRIBUTE16].ToString();
            //        string col39 = row[xCRHIADB.xCRHIA.ATTRIBUTE17].ToString();
            //        string col40 = row[xCRHIADB.xCRHIA.ATTRIBUTE18].ToString();

            //        string col41 = row[xCRHIADB.xCRHIA.ATTRIBUTE19].ToString();
            //        string col42 = row[xCRHIADB.xCRHIA.ATTRIBUTE20].ToString();
            //        string col43 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE1].ToString();
            //        string col44 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE2].ToString();
            //        string col45 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE3].ToString();
            //        string col46 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE4].ToString();
            //        string col47 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE5].ToString();
            //        string col48 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE6].ToString();
            //        string col49 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE7].ToString();
            //        string col50 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE8].ToString();

            //        string col51 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE9].ToString();
            //        string col52 = row[xCRHIADB.xCRHIA.ATTRIBUTE_DATE10].ToString();
            //        string col53 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER1].ToString();
            //        string col54 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER2].ToString();
            //        string col55 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER3].ToString();
            //        string col56 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER4].ToString();
            //        string col57 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER5].ToString();
            //        string col58 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER6].ToString();
            //        string col59 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER7].ToString();
            //        string col60 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER8].ToString();

            //        string col61 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER9].ToString();
            //        string col62 = row[xCRHIADB.xCRHIA.ATTRIBUTE_NUMBER10].ToString();
            //        string col63 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP1].ToString();
            //        string col64 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP2].ToString();
            //        string col65 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP3].ToString();
            //        string col66 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP4].ToString();
            //        string col67 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP5].ToString();
            //        string col68 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP6].ToString();
            //        string col69 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP7].ToString();
            //        string col70 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP8].ToString();

            //        string col71 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP9].ToString();
            //        string col72 = row[xCRHIADB.xCRHIA.ATTRIBUTE_TIMESTAMP10].ToString();
            //        string col73 = "col73";       //Unit Weight
            //        string col74 = "col74";     //Weight UOM
            //        string col75 = "col75";     //Weight UOM Name
            //        string col76 = "col76";     //Unit Volumn
            //        string col77 = "col77";     //Volume UOM
            //        string col78 = "col78";     //Volume UOM Name
            //        string col79 = "col79";     //Template Name
            //        string col80 = "col80";     //ITEM_ATTRIBUTE_CATEGORY

            //        string col81 = "col81";     //ITEM_ATTRIBUTE1
            //        string col82 = "col82";     //ITEM_ATTRIBUTE2
            //        string col83 = "col83";     //ITEM_ATTRIBUTE3
            //        string col84 = "col84";     //ITEM_ATTRIBUTE4
            //        string col85 = "col85";     //ITEM_ATTRIBUTE5
            //        string col86 = "col86";     //ITEM_ATTRIBUTE6
            //        string col87 = "col87";     //ITEM_ATTRIBUTE7
            //        string col88 = "col88";     //ITEM_ATTRIBUTE8
            //        string col89 = "col89";     //ITEM_ATTRIBUTE9
            //        string col90 = "col90";     //ITEM_ATTRIBUTE10

            //        string col91 = "col91";     //ITEM_ATTRIBUTE11
            //        string col92 = "col92";     //ITEM_ATTRIBUTE12
            //        string col93 = "col93";     //ITEM_ATTRIBUTE13
            //        string col94 = "col94";       //ITEM_ATTRIBUTE14
            //        string col95 = "col95";       //ITEM_ATTRIBUTE15


            //        //string col71 = "col71";

            //        //string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}," +
            //        //    "{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}," +
            //        //    "{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},{60}," +
            //        //    "{61},{62},{63},{64},{65},{66},{67},{68},{69},{70},{71}", 
            //        //    col01, col02, col03, col04, col05, col06, col07, col08, col09, col10,
            //        //    col11, col12, col13, col14, col15, col16, col17, col18, col19, col20,
            //        //    col21, col22, col23, col24, col25, col26, col27, col28, col29, col30,
            //        //    col41, col42, col43, col44, col45, col46, col47, col48, col49, col50,
            //        //    col51, col52, col53, col54, col55, col56, col57, col58, col59, col60,
            //        //    col61, col62, col63, col64, col65, col66, col67, col68, col69, col70, col71);

            //        string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10
            //            + "," + col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
            //            + "," + col21 + "," + col22 + "," + col23 + "," + col24 + "," + col25 + "," + col26 + "," + col27 + "," + col28 + "," + col29 + "," + col30
            //            + "," + col31 + "," + col32 + "," + col33 + "," + col34 + "," + col35 + "," + col36 + "," + col37 + "," + col38 + "," + col39 + "," + col40
            //            + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
            //            + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70
            //            + "," + col71 + "," + col72 + "," + col73 + "," + col74 + "," + col75 + "," + col76 + "," + col77 + "," + col78 + "," + col79 + "," + col80
            //            + "," + col81 + "," + col82 + "," + col83 + "," + col84 + "," + col85 + "," + col86 + "," + col87 + "," + col88 + "," + col89 + "," + col90
            //            + "," + col91 + "," + col92 + "," + col93 + "," + col94 + "," + col95;

            //        stream.WriteLine(csvRow);
            //    }
            //}
        }
    }
}
