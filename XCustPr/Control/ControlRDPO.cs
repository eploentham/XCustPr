using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class ControlRDPO
    {
        static String fontName = "Microsoft Sans Serif";
        public String backColor1 = "#1E1E1E";
        public String backColor2 = "#2D2D30";
        public String foreColor1 = "#fff";
        static float fontSize9 = 9.75f;
        static float fontSize8 = 8.25f;
        public Font fV1B, fV1;
        public int tcW = 0, tcH = 0, tcWMinus = 25, tcHMinus = 70, formFirstLineX = 5, formFirstLineY = 5;

        public ConnectDB conn;

        private IniFile iniFile;
        public InitC initC;
        public XcustLinfoxPrTblDB xCLFPTDB;
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

        public ValidatePrPo vPrPo;

        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();
        public ControlRDPO()
        {
            iniFile = new IniFile(Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini");
            initC = new InitC();
            vPrPo = new ValidatePrPo();

            GetConfig();

            conn = new ConnectDB("kfc_po", initC);
            xCLFPTDB = new XcustLinfoxPrTblDB(conn, initC);
            xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
            xCPRLIADB = new XcustPorReqLineIntAllDB(conn);
            xCPRDIADB = new XcustPorReqDistIntAllDB(conn);
            xCBMTDB = new XcustBuMstTblDB(conn, initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn,initC);
            xCIMTDB = new XcustItemMstTblDB(conn, initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, initC);
            xCUMTDB = new XcustUomMstTblDB(conn, initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, initC);

            fontSize9 = 9.75f;
            fontSize8 = 8.25f;
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);
            
        }
        public void GetConfig()
        {
            initC.PathArchive = iniFile.Read("PathArchive");    //bit
            initC.PathError = iniFile.Read("PathError");
            initC.PathInitial = iniFile.Read("PathInitial");
            initC.PathProcess = iniFile.Read("PathProcess");
            initC.portDBBIT = iniFile.Read("portDBBIT");

            initC.APPROVER_EMAIL = iniFile.Read("APPROVER_EMAIL");    //bit demo
            initC.BU_NAME = iniFile.Read("BU_NAME");
            initC.Requester = iniFile.Read("Requester");
            initC.ImportSource = iniFile.Read("ImportSource");
            initC.Company = iniFile.Read("Company");
            initC.DELIVER_TO_LOCATTION = iniFile.Read("DELIVER_TO_LOCATTION");
            initC.ORGANIZATION_code = iniFile.Read("ORGANIZATION_code");
            initC.Locator = iniFile.Read("Locator");
            initC.Subinventory_Code = iniFile.Read("Subinventory_Code");
            initC.CURRENCY_CODE = iniFile.Read("CURRENCY_CODE");

            initC.EmailPort = iniFile.Read("EmailPort");

            initC.EmailCharset = iniFile.Read("EmailCharset");      //orc master
            initC.EmailUsername = iniFile.Read("EmailUsername");
            initC.EmailPassword = iniFile.Read("EmailPassword");
            initC.EmailSMTPSecure = iniFile.Read("EmailSMTPSecure");
            initC.PathLinfox = iniFile.Read("PathLinfox");

            initC.EmailHost = iniFile.Read("EmailHost");        // orc backoffice
            initC.EmailSender = iniFile.Read("EmailSender");
            initC.FTPServer = iniFile.Read("FTPServer");
            initC.PathZipExtract = iniFile.Read("PathZipExtract");
            initC.PathZip = iniFile.Read("PathZip");

            initC.databaseDBKFCPO = iniFile.Read("databaseDBKFCPO");        // orc BIT
            initC.hostDBKFCPO = iniFile.Read("hostDBKFCPO");
            initC.userDBKFCPO = iniFile.Read("userDBKFCPO");
            initC.passDBKFCPO = iniFile.Read("passDBKFCPO");
            initC.portDBKFCPO = iniFile.Read("portDBKFCPO");
            //initC.quoLine6 = iniFile.Read("quotationline6");

            //initC.grdQuoColor = iniFile.Read("gridquotationcolor");

            //initC.HideCostQuotation = iniFile.Read("hidecostquotation");
            //if (initC.grdQuoColor.Equals(""))
            //{
            //    initC.grdQuoColor = "#b7e1cd";
            //}
            //initC.Password = regE.getPassword();
        }
        public void CreateIfMissing(String path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public String[] getFileinFolder(String path)
        {
            string[] filePaths = Directory.GetFiles(@path);
            return filePaths;
        }
        public void moveFile(String sourceFile, String destinationFile)
        {
            System.IO.File.Move(@sourceFile, @destinationFile);
        }
        /*
         * check แค่ format ว่า เป็น yyyymmdd เท่านั้น
         * Error PO001-002 : Date Format not correct 
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
                    sYear.Append(date.Substring(0,4));
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
                catch(Exception ex)
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
         * กรณีที Order Date ย้อนหลังจากวันที่ปัจจุบัน
         * Error PO001-028 : Order date is less than current date
         */
        public Boolean validateOrderDateMinCurrDate(String orderdate, DateTime currdate)
        {
            Boolean chk = false;
            DateTime orderDate = new DateTime();
            if(DateTime.TryParse(orderdate,out orderDate))
            {
                if(DateTime.Compare(orderDate, currdate)>=0)// check แบบนี้ เพราะจะได้ return false เพื่อในการ gen log และ return false จะได้ออกจาก loop ง่ายดี
                {
                    chk = true;
                }
                else
                {
                    chk = false;
                }
            }
            else
            {
                chk = false;
            }
            

            return chk;
        }
        /*
         * check qty ว่า data type ถูกต้องไหม
         * ที่ใช้ int.tryparse เพราะ ใน database เป็น decimal(18,0)
         * Error PO001-006 : Invalid data type
         */
        public Boolean validateQTY(String qty)
        {
            Boolean chk = false;
            int i = 0;
            chk = int.TryParse(qty,out i);
            return chk;
        }

        public Boolean validateLinfox(DataRow row)
        {
            //row[dc].ToString().Trim()
            return true;
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
        public void processGetBlanketAgreement(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("หา Blanket Agreement " + initC.PathProcess, "Blanket", lv1, form1);

        }
        /*
         *d. จากนั้น Program จะเอาข้อมูลจาก Table XCUST_LINFOX_PR_TBL มาทำการ Validate 
         *e. กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL, XCUST_POR_REQ_LINE_INT_ALL, XCUST_POR_REQ_DIST_INT_ALL
         * Validate ไม่ผ่าน ลบ temp where ตาม filename
         */
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("อ่าน file จาก " + initC.PathProcess, "Validate", lv1, form1);
            pB1.Visible = true;
            Boolean chk = false;
            DataTable dtGroupBy = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            String buCode = "", locator="", Org="", subInv_code="", currencyCode="";
            ValidatePrPo vPP = new ValidatePrPo();
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();
            int row1 = 0;

            buCode = xCBMTDB.selectActive1();
            //Error PO001-004 : Invalid Requisitioning BU
            if (!buCode.Equals(initC.BU_NAME.Trim()))
            {
                chk = false;
            }
            //Error PO001-008 : Invalid Deliver To Location
            locator = xCDLMTDB.selectLocator1();
            if (!locator.Equals(initC.Locator.Trim()))
            {
                chk = false;
            }
            //Error PO001-009 : Invalid Deliver-to Organization
            Org = xCDOMTDB.selectActive1();
            if (!Org.Equals(initC.ORGANIZATION_code.Trim()))
            {
                chk = false;
            }

            //Error PO001-013 : Invalid Currency Code
            if (!xCMTDB.validateCurrencyCodeBycurrCode(initC.CURRENCY_CODE))
            {
                chk = false;
            }
            //Error PO001-014 : Invalid Procurement BU
            if (!buCode.Equals(initC.BU_NAME.Trim()))
            {
                chk = false;
            }

            //StringBuilder filename = new StringBuilder();
            dtGroupBy = xCLFPTDB.selectLinfoxGroupByFilename();//   ดึง filename
            foreach(DataRow rowG in dtGroupBy.Rows)
            {
                addListView("ดึงข้อมูล  " + rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim(), "Validate", lv1, form1);
                //filename.Clear();
                //filename.Append(row[xCLFPTDB.xCLFPT.file_name].ToString().Trim());
                //dt = xCLFPTDB.selectLinfoxByFilename(filename.ToString());        // for test
                //pB1.Value = 0;
                dt = xCLFPTDB.selectLinfoxByFilename(rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim());    // ข้อมูลใน file
                row1 = 0;
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {
                    row1++;
                    pB1.Value = row1;
                    //Error PO001-006 : Invalid data type
                    chk = validateQTY(row[xCLFPTDB.xCLFPT.QTY].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-006 ";
                        vPP.Validate = "row "+ row1 + " QTY=" + row[xCLFPTDB.xCLFPT.QTY].ToString();
                        lVPr.Add(vPP);
                    }
                    chk = validateDate(row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " ORDER_DATE=" + row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString();
                        lVPr.Add(vPP);
                    }
                    chk = validateDate(row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString());//ต้องแก้ไข เพราะ agreement เข้า method มีค่าเป็น date
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " REQUEST_DATE=" + row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString();
                        lVPr.Add(vPP);
                    }
                    //Error PO001-010 : Invalid Subinventory Code
                    subInv_code = xCSIMTDB.validateSubInventoryCode1(initC.ORGANIZATION_code.Trim(), row[xCLFPTDB.xCLFPT.store_code].ToString().Trim());
                    if (subInv_code.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-010 ";
                        vPP.Validate = "row " + row1 + " store_code =" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim()+ " ORGANIZATION_code "+ initC.ORGANIZATION_code.Trim();
                        lVPr.Add(vPP);
                    }
                    // Error PO001 - 011 : Invalid Item Number
                    if (!xCIMTDB.validateItemCodeByOrgRef1("300000000949654", row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim()))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-011 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " item_code " + row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim();
                        lVPr.Add(vPP);
                    }
                    // Error PO001-015 : Invalid Supplier
                    if (!xCSMTDB.validateSupplierBySupplierCode(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-015 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " supplier_code " + row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim();
                        lVPr.Add(vPP);
                    }
                    // Error PO001-016 :  Invalid UOM
                    if (!xCUMTDB.validateUOMCodeByUOMCode(row[xCLFPTDB.xCLFPT.UOMCODE].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-016 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " uom_code " + row[xCLFPTDB.xCLFPT.UOMCODE].ToString().Trim();
                        lVPr.Add(vPP);
                    }
                    // Error PO001-017 : Invalid CHARGE_ACCOUNT_SEGMENT1
                    if (!xCVSMTDB.validateValueBySegment1("COMPANY RD CLOUD","Y", "11"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-017 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT1 " ;
                        lVPr.Add(vPP);
                    }
                    // Error PO001-018 : Invalid CHARGE_ACCOUNT_SEGMENT2
                    if (!xCVSMTDB.validateValueBySegment2("STORE RD CLOUD", "Y", "00000"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-018 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT2 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-019 : Invalid CHARGE_ACCOUNT_SEGMENT3
                    if (!xCVSMTDB.validateValueBySegment3("ACCOUNT RD CLOUD", "Y", "117101"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-019 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT3 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-020 : Invalid CHARGE_ACCOUNT_SEGMENT4
                    if (!xCVSMTDB.validateValueBySegment4("PROJECT RD CLOUD", "Y", subInv_code))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-020 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT4 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-021 : Invalid CHARGE_ACCOUNT_SEGMENT5
                    if (!xCVSMTDB.validateValueBySegment5("FUTURE1 RD CLOUD", "Y", "00"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-021 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT5 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-022 : Invalid CHARGE_ACCOUNT_SEGMENT6
                    if (!xCVSMTDB.validateValueBySegment6("FUTURE2 RD CLOUD", "Y", "0000"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-022 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT6 ";
                        lVPr.Add(vPP);
                    }



                    //dt.Rows[0][xc xCBMT.BU_ID].ToString();xCVSMTDB
                }
            }
            pB1.Visible = false;
        }
        public void processLinfoxPOtoErpPR(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            ReadText rd = new ReadText();
            String[] filePOProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;

            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process
            addListView("อ่าน fileจาก" + initC.PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                moveFile(aa, initC.PathProcess + aa.Replace(initC.PathInitial, ""));
            }
            addListView("Clear temp table", "", lv1, form1);
            xCLFPTDB.DeleteLinfoxTemp();//  clear temp table
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_LINFOX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert XCUST_LINFOX_PR_TBL
            filePOProcess = getFileinFolder(initC.PathProcess);
            addListView("อ่าน file จาก " + initC.PathProcess, "", lv1, form1);
            foreach (string aa in filePOProcess)
            {
                List<String> linfox = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL
                pB1.Visible = true;
                xCLFPTDB.insertBluk(linfox, aa, "kfc_po", pB1);
                pB1.Visible = false;
            }
            
        }
        private void addListView(String col1, String col2, MaterialListView lv1, Form form1)
        {
            lv1.Items.Add(AddToList((lv1.Items.Count + 1), col1, col2));
            form1.Refresh();
        }
        private void genFilePR()
        {

        }
        private void insertXcustPorReqHeaderIntAll(DataRow row, String date, String time)
        {//row[dc].ToString().Trim().
            XcustPorReqHeaderIntAll xCPRHIA = new XcustPorReqHeaderIntAll();
            xCPRHIA.ATTRIBUTE1 = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();

            xCPRHIA.ATTRIBUTE_DATE1 = date;
            xCPRHIA.ATTRIBUTE_TIMESTAMP1 = date+" "+ time;
            xCPRHIA.BATCH_ID = "";
            xCPRHIA.DESCRIPTIONS = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRHIA.REQUESTER_EMAIL_ADDR = "";
            xCPRHIA.INTERFACE_SOURCE_CODE = "";
            xCPRHIA.ATTRIBUTE_CATEGORY = "";
            xCPRHIA.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
            xCPRHIA.PROCESS_FLAG = "N";
            xCPRHIA.APPROVER_EMAIL_ADDR = "";
            xCPRHIA.STATUS_CODE = "";
            xCPRHIA.REQ_BU_NAME = "";
            xCPRHIA.REQUITITION_NUMBER = "";
            xCPRHIADB.insert(xCPRHIA);
        }
        private void insertXcustPorReqLineIntAll(DataRow row, String date, String time)
        {
            XcustPorReqLineIntAll xCPRLIA = new XcustPorReqLineIntAll();
            xCPRLIA.ATTRIBUTE1 = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRLIA.ATTRIBUTE_DATE1 = date;
            xCPRLIA.ATTRIBUTE_NUMBER1 = "";
            xCPRLIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
            xCPRLIA.CATEGORY_NAME = "";
            xCPRLIA.CURRENCY_CODE = "";
            xCPRLIA.DELIVER_TO_LOCATION_CODE = "";
            xCPRLIA.DELIVER_TO_ORGANIZATION_CODE = "";
            xCPRLIA.Goods = "";
            xCPRLIA.DESTINATION_TYPE_CODE = "";
            xCPRLIA.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim();
            xCPRLIA.LINFOX_PR = "";
            xCPRLIA.NEED_BY_DATE = "";
            xCPRLIA.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRLIA.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
            xCPRLIA.Price = "";
            xCPRLIA.PROCESS_FLAG = "N";
            xCPRLIA.PRC_BU_NAME = "";
            xCPRLIA.PR_APPROVER = "";
            xCPRLIA.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString().Trim();
            xCPRLIA.REQUESTER_EMAIL_ADDR = initC.Requester;
            xCPRLIA.Requisitioning_BU = initC.BU_NAME;
            xCPRLIA.DESTINATION_SUBINVENTORY = "";
            xCPRLIA.SUGGESTED_VENDOR_NAME = row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim();
            xCPRLIA.SUGGESTED_VENDOR_SITE = "";
            xCPRLIADB.insert(xCPRLIA);
        }
        private void insertXcustPorReqDistIntAll(DataRow row, String date, String time)
        {
            XcustPorReqDistIntAll xCPRDIA = new XcustPorReqDistIntAll();
            xCPRDIA.ATTRIBUTE1 = "";
            xCPRDIA.ATTRIBUTE_CATEGORY = "";
            xCPRDIA.ATTRIBUTE_DATE1 = date;
            xCPRDIA.ATTRIBUTE_NUMBER1 = "";
            xCPRDIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 = "";
            xCPRDIA.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRDIA.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
            xCPRDIA.PROCESS_FLAG = "N";
            xCPRDIA.REQ_DIST_INTERFACE_ID = "";
            xCPRDIA.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString().Trim();
            xCPRDIADB.insert(xCPRDIA);
        }
    }
}
