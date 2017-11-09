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
    public class ControlPO005
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
        public XcustMmxPrIntTblDB xCMPITDB;

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

        public ControlPO005(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            vPrPo = new ValidatePrPo();

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard

            xCMPITDB = new XcustMmxPrIntTblDB(conn, Cm.initC);
            Cm.createFolderPO005();
            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard

        }
        /*
         * a.	ระบบ MMX จะ  SFTP file จากระบบงาน MMX และนำ File มาวางไว้ที่ Server ตาม Path Parameter Path Initial
         * b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
         */
        public void processMMXPOtoErpPR(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            ReadText rd = new ReadText();
            String[] filePOProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;

            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
            addListView("อ่าน fileจาก" + Cm.initC.PO005PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.PO005PathProcess + aa.Replace(Cm.initC.PO005PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            xCMPITDB.DeleteMmxTemp();//  clear temp table
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_MMX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert xcust_mmx_pr_int_tbl
            filePOProcess = Cm.getFileinFolder(Cm.initC.PO005PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.PO005PathProcess, "", lv1, form1);
            foreach (string aa in filePOProcess)
            {
                List<String> mmx = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL
                pB1.Visible = true;
                xCMPITDB.insertBluk(mmx, aa, "kfc_po", pB1);
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
        /*
         * d.	จากนั้น Program จะเอาข้อมูลจาก Table XCUST_MMX_PR_TBL มาทำการ Validate 
         * e.	ทำการหา Blanket Agreement Number โดยใช้ Supplier Code กับ Item Code หาค่า Blanket Agreement ที่ Active อยู่ ณ เวลานั้น มี Status เป็น Approved  กรณีไม่เจอ หรือเจอมากกว่า 1 ค่าให้ Validatte ไม่ผ่าน 
         * f.	กรณีที่มี Blanket Agreement และพบว่า Agreement นั้นมีการ Setup ค่า Minimum Relese 
         * และ Amount Limit ต้องทำการตรวจสอบว่าห้ามน้อยกว่า หรือมากกว่าค่าที่กำหนด  หากมากกว่าหรือน้อยกว่าต้อง Validate ไม่ผ่าน
         * g.	กรณีที่ Validate ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL
         * ,XCUST_POR_REQ_LINE_INT_ALL ,XCUST_POR_REQ_DIST_INT_ALLและ Update Validate_flag = ‘Y’
         * h.	กรณีที่ Validate ไม่ผ่าน จะะ Update Validate_flag = ‘E’ พร้อมระบุ Error Message
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
            ValidatePrPo vPP = new ValidatePrPo();
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();

            listXcustPRHIA.Clear();
            listXcustPRLIA.Clear();
            listXcustPRDIA.Clear();
            getListXcSIMT();
            getListXcIMT();
            getListXcSMT();
            getListXcVSMT();
            getListXcUMT();
            int row1 = 0;

            buCode = xCBMTDB.selectActive1();
            //Error PO005-004 : Invalid Requisitioning BU
            if (!buCode.Equals(Cm.initC.BU_NAME.Trim()))
            {
                chk = false;
            }
            //Error PO005-008 : Invalid Deliver To Location  
            locator = xCDLMTDB.selectLocator1();
            if (!locator.Equals(Cm.initC.Locator.Trim()))
            {
                chk = false;
            }
            //Error PO005-009 : Invalid Deliver-to Organization
            Org = xCDOMTDB.selectActive1();
            if (!Org.Equals(Cm.initC.ORGANIZATION_code.Trim()))
            {
                chk = false;
            }
            //Error PO005-013 : Invalid Currency Code
            if (!xCMTDB.validateCurrencyCodeBycurrCode(Cm.initC.CURRENCY_CODE))
            {
                chk = false;
            }
            //กรณีที่ข้อมูล Procurement BUไม่ถูกต้องตรงกับค่า Setup ใน ERP
            if (!buCode.Equals(Cm.initC.BU_NAME.Trim()))
            {
                chk = false;
            }
            dtGroupBy = xCMPITDB.selectMmxGroupByFilename();//   ดึง filename
            foreach (DataRow rowG in dtGroupBy.Rows)
            {
                addListView("ดึงข้อมูล  " + rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim(), "Validate", lv1, form1);
                dt = xCMPITDB.selectMmxByFilename(rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim());    // ข้อมูลใน file
                row1 = 0;
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {
                    row1++;
                    pB1.Value = row1;
                    //Error PO001-006 : Invalid data type
                    chk = Cm.validateQTY(row[xCMPITDB.xCMPIT.order_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-006 ";
                        vPP.Validate = "row " + row1 + " order_date=" + row[xCMPITDB.xCMPIT.order_date].ToString();
                        lVPr.Add(vPP);
                    }
                    chk = Cm.validateQTY(row[xCMPITDB.xCMPIT.confirm_qty].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-006 ";
                        vPP.Validate = "row " + row1 + " confirm_qty=" + row[xCMPITDB.xCMPIT.confirm_qty].ToString();
                        lVPr.Add(vPP);
                    }
                    //Error PO005-002 : Date Format not correct 
                    chk = Cm.validateDate(row[xCMPITDB.xCMPIT.conf_delivery_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " conf_delivery_date=" + row[xCMPITDB.xCMPIT.conf_delivery_date].ToString();
                        lVPr.Add(vPP);
                    }
                    chk = Cm.validateDate(row[xCMPITDB.xCMPIT.delivery_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " delivery_date=" + row[xCMPITDB.xCMPIT.delivery_date].ToString();
                        lVPr.Add(vPP);
                    }
                    chk = Cm.validateDate(row[xCMPITDB.xCMPIT.order_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " order_date=" + row[xCMPITDB.xCMPIT.order_date].ToString();
                        lVPr.Add(vPP);
                    }
                    chk = Cm.validateDate(row[xCMPITDB.xCMPIT.request_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " request_date=" + row[xCMPITDB.xCMPIT.request_date].ToString();
                        lVPr.Add(vPP);
                    }
                    
                }
            }
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
