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
    }
}
