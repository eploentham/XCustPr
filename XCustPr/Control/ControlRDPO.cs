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
        public XcustBlanketAgreementHeaderTblDB xCBAHTDB;
        public XcustBlanketAgreementLinesTblDB xCBALTDB;

        public ValidatePrPo vPrPo;

        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();

        private List<XcustPorReqHeaderIntAll> listXcustPRHIA;
        private List<XcustPorReqLineIntAll> listXcustPRLIA;
        private List<XcustPorReqDistIntAll> listXcustPRDIA;

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;

        public ControlRDPO()
        {
            iniFile = new IniFile(Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini");
            initC = new InitC();
            vPrPo = new ValidatePrPo();

            GetConfig();

            conn = new ConnectDB("kfc_po", initC);
            xCLFPTDB = new XcustLinfoxPrTblDB(conn, initC);
            xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
            xCPRLIADB = new XcustPorReqLineIntAllDB(conn, initC);
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
            xCBAHTDB = new XcustBlanketAgreementHeaderTblDB(conn, initC);
            xCBALTDB = new XcustBlanketAgreementLinesTblDB(conn, initC);

            fontSize9 = 9.75f;
            fontSize8 = 8.25f;
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);

            listXcustPRHIA = new List<XcustPorReqHeaderIntAll>();
            listXcustPRLIA = new List<XcustPorReqLineIntAll>();
            listXcustPRDIA = new List<XcustPorReqDistIntAll>();

            listXcSIMT = new List<XcustSubInventoryMstTbl>();
            listXcIMT = new List<XcustItemMstTbl>();
            listXcSMT = new List<XcustSupplierMstTbl>();
            listXcVSMT = new List<XcustValueSetMstTbl>();
            listXcUMT = new List<XcustUomMstTbl>();

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
            initC.PR_STATAUS = iniFile.Read("PR_STATAUS");

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
        private void getListXcSIMT()
        {
            listXcSIMT.Clear();
            DataTable dt = xCSIMTDB.selectAll();
            foreach(DataRow row in dt.Rows)
            {
                XcustSubInventoryMstTbl item = new XcustSubInventoryMstTbl();
                item = xCSIMTDB.setData(row);
                listXcSIMT.Add(item);
            }
        }
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
         * เตรียมข้อมูล เพื่อที่จะลง table XcustPorReqHeaderIntAll
         * เพราะในเอกสาร ต้องลงตาม group by po_number
         */
        private void addXcustPRHIA(String po_number, String curr_date, String filename)
        {
            Boolean chk = true;
            foreach(XcustPorReqHeaderIntAll xcprhia in listXcustPRHIA)
            {
                if (xcprhia.ATTRIBUTE1.Equals(po_number))
                {
                    chk = false;
                }
            }
            if (chk)
            {
                XcustPorReqHeaderIntAll xcprhia1 = new XcustPorReqHeaderIntAll();
                xcprhia1.ATTRIBUTE1 = po_number;
                xcprhia1.IMPORT_SOURCE = initC.ImportSource;
                xcprhia1.REQ_BU_NAME = initC.BU_NAME;
                xcprhia1.STATUS_CODE = initC.PR_STATAUS;
                xcprhia1.REQ_HEADER_INTERFACE_ID = po_number;
                xcprhia1.BATCH_ID = curr_date + String.Concat("00" + listXcustPRHIA.Count).Substring(2);
                xcprhia1.REQUITITION_NUMBER = "PR"+curr_date.Substring(2,2);
                xcprhia1.DESCRIPTIONS = "LINFOX_"+ po_number+"_"+ filename;
                xcprhia1.ATTRIBUTE_CATEGORY = "LINFOX_PR’";
                xcprhia1.ATTRIBUTE2 = po_number;
                listXcustPRHIA.Add(xcprhia1);
            }
        }
        private void addXcustPRLIAFromxCLFPT(DataRow row)
        {
            XcustPorReqLineIntAll item = new XcustPorReqLineIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.DESTINATION_TYPE_CODE = "";
            item.PRC_BU_NAME = "";
            item.DELIVER_TO_ORGANIZATION_CODE = initC.ORGANIZATION_code;
            item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            item.LINE_TYPE = "";

            item.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString();
            item.CURRENCY_CODE = initC.CURRENCY_CODE;
            item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";

            listXcustPRLIA.Add(item);
        }
        private void addXcustPRDIAFromxCLFPT(DataRow row)
        {
            XcustPorReqDistIntAll item = new XcustPorReqDistIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            //item.DESTINATION_TYPE_CODE = "";
            //item.PRC_BU_NAME = "";
            //item.DELIVER_TO_ORGANIZATION_CODE = initC.ORGANIZATION_code;
            //item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            //item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            //item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            //item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            //item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            //item.LINE_TYPE = "";

            item.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString();
            //item.CURRENCY_CODE = initC.CURRENCY_CODE;
            //item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";

            listXcustPRDIA.Add(item);
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
        private String validateSubInventoryCode(String ordId, String StoreCode)
        {
            String chk = "";
            foreach(XcustSubInventoryMstTbl item in listXcSIMT)
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
        private Boolean validateUOMCodeByUOMCode(String uomCode)
        {
            Boolean chk = false;
            foreach (XcustUomMstTbl item in listXcUMT)
            {
                if (item.UOM_CODE.Equals(uomCode.Trim()))
                {
                    chk = true;
                    break;
                }
            }
            return chk;
        }
        private Boolean validateValueBySegment1(String valuesetcode, String enableflag, String value)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        private Boolean validateValueBySegment2(String valuesetcode, String enableflag, String value)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        private Boolean validateValueBySegment3(String valuesetcode, String enableflag, String value)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        private Boolean validateValueBySegment4(String valuesetcode, String enableflag, String value)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        private Boolean validateValueBySegment5(String valuesetcode, String enableflag, String value)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        private Boolean validateValueBySegment6(String valuesetcode, String enableflag, String value)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
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
        public void processGenCSV()
        {
            processGenCSVxCPRHIA();
            processGenCSVxCPRLIA();
            processGenCSVxCPRDIA();
            processGenZIP();
        }
        public void processGenCSVxCPRHIA()
        {
            var file = initC.PathArchive+ "PorReqHeadersInterfaceAl.csv";
            DataTable dt = xCPRHIADB.selectAll();
            using (var stream = File.CreateText(file))
            {
                foreach(DataRow row in dt.Rows)
                {
                    string col01 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col02 = "col02";
                    string col03 = "col03";
                    string col04 = "col04";
                    string col05 = "col05";
                    string col06 = "col06";
                    string col07 = "col07";
                    string col08 = "col08";
                    string col09 = "col09";
                    string col10 = "col10";

                    string col11 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col12 = "col12";
                    string col13 = "col13";
                    string col14 = "col14";
                    string col15 = "col15";
                    string col16 = "col16";
                    string col17 = "col17";
                    string col18 = "col18";
                    string col19 = "col19";
                    string col20 = "col20";

                    string col21 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col22 = "col22";
                    string col23 = "col23";
                    string col24 = "col24";
                    string col25 = "col25";
                    string col26 = "col26";
                    string col27 = "col27";
                    string col28 = "col28";
                    string col29 = "col29";
                    string col30 = "col30";

                    string col31 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col32 = "col32";
                    string col33 = "col33";
                    string col34 = "col34";
                    string col35 = "col35";
                    string col36 = "col36";
                    string col37 = "col37";
                    string col38 = "col38";
                    string col39 = "col39";
                    string col40 = "col40";

                    string col41 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col42 = "col42";
                    string col43 = "col43";
                    string col44 = "col44";
                    string col45 = "col45";
                    string col46 = "col46";
                    string col47 = "col47";
                    string col48 = "col48";
                    string col49 = "col49";
                    string col50 = "col50";

                    string col51 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col52 = "col52";
                    string col53 = "col53";
                    string col54 = "col54";
                    string col55 = "col55";
                    string col56 = "col56";
                    string col57 = "col57";
                    string col58 = "col58";
                    string col59 = "col59";
                    string col60 = "col60";

                    string col61 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col62 = "col62";
                    string col63 = "col63";
                    string col64 = "col64";
                    string col65 = "col65";
                    string col66 = "col66";
                    string col67 = "col67";
                    string col68 = "col68";
                    string col69 = "col69";
                    string col70 = "col70";
                    string col71 = "col71";

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

                    string csvRow = col01+","+col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 +"," + col08 +"," + col09 +"," + col10;

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenCSVxCPRLIA()
        {
            var file = initC.PathArchive + "PorReqLinesInterfaceAl.csv";
            DataTable dt = xCPRHIADB.selectAll();
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col02 = "col02";
                    string col03 = "col03";
                    string col04 = "col04";
                    string col05 = "col05";
                    string col06 = "col06";
                    string col07 = "col07";
                    string col08 = "col08";
                    string col09 = "col09";
                    string col10 = "col10";

                    string col11 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col12 = "col12";
                    string col13 = "col13";
                    string col14 = "col14";
                    string col15 = "col15";
                    string col16 = "col16";
                    string col17 = "col17";
                    string col18 = "col18";
                    string col19 = "col19";
                    string col20 = "col20";

                    string col21 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col22 = "col22";
                    string col23 = "col23";
                    string col24 = "col24";
                    string col25 = "col25";
                    string col26 = "col26";
                    string col27 = "col27";
                    string col28 = "col28";
                    string col29 = "col29";
                    string col30 = "col30";

                    string col31 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col32 = "col32";
                    string col33 = "col33";
                    string col34 = "col34";
                    string col35 = "col35";
                    string col36 = "col36";
                    string col37 = "col37";
                    string col38 = "col38";
                    string col39 = "col39";
                    string col40 = "col40";

                    string col41 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col42 = "col42";
                    string col43 = "col43";
                    string col44 = "col44";
                    string col45 = "col45";
                    string col46 = "col46";
                    string col47 = "col47";
                    string col48 = "col48";
                    string col49 = "col49";
                    string col50 = "col50";

                    string col51 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col52 = "col52";
                    string col53 = "col53";
                    string col54 = "col54";
                    string col55 = "col55";
                    string col56 = "col56";
                    string col57 = "col57";
                    string col58 = "col58";
                    string col59 = "col59";
                    string col60 = "col60";

                    string col61 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col62 = "col62";
                    string col63 = "col63";
                    string col64 = "col64";
                    string col65 = "col65";
                    string col66 = "col66";
                    string col67 = "col67";
                    string col68 = "col68";
                    string col69 = "col69";
                    string col70 = "col70";
                    string col71 = "col71";

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

                    string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10;

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenCSVxCPRDIA()
        {
            var file = initC.PathArchive + "PorReqDistInterfaceAl.csv";
            DataTable dt = xCPRHIADB.selectAll();
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col02 = "col02";
                    string col03 = "col03";
                    string col04 = "col04";
                    string col05 = "col05";
                    string col06 = "col06";
                    string col07 = "col07";
                    string col08 = "col08";
                    string col09 = "col09";
                    string col10 = "col10";

                    string col11 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col12 = "col12";
                    string col13 = "col13";
                    string col14 = "col14";
                    string col15 = "col15";
                    string col16 = "col16";
                    string col17 = "col17";
                    string col18 = "col18";
                    string col19 = "col19";
                    string col20 = "col20";

                    string col21 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col22 = "col22";
                    string col23 = "col23";
                    string col24 = "col24";
                    string col25 = "col25";
                    string col26 = "col26";
                    string col27 = "col27";
                    string col28 = "col28";
                    string col29 = "col29";
                    string col30 = "col30";

                    string col31 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col32 = "col32";
                    string col33 = "col33";
                    string col34 = "col34";
                    string col35 = "col35";
                    string col36 = "col36";
                    string col37 = "col37";
                    string col38 = "col38";
                    string col39 = "col39";
                    string col40 = "col40";

                    string col41 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col42 = "col42";
                    string col43 = "col43";
                    string col44 = "col44";
                    string col45 = "col45";
                    string col46 = "col46";
                    string col47 = "col47";
                    string col48 = "col48";
                    string col49 = "col49";
                    string col50 = "col50";

                    string col51 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col52 = "col52";
                    string col53 = "col53";
                    string col54 = "col54";
                    string col55 = "col55";
                    string col56 = "col56";
                    string col57 = "col57";
                    string col58 = "col58";
                    string col59 = "col59";
                    string col60 = "col60";

                    string col61 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col62 = "col62";
                    string col63 = "col63";
                    string col64 = "col64";
                    string col65 = "col65";
                    string col66 = "col66";
                    string col67 = "col67";
                    string col68 = "col68";
                    string col69 = "col69";
                    string col70 = "col70";
                    string col71 = "col71";

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

                    string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10;

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenZIP()
        {
            var allFiles = Directory.GetFiles(@initC.PathArchive, "*.*", SearchOption.AllDirectories);
            foreach (String file in allFiles)
            {
                //using (ZipArchive archive = ZipFile.OpenRead(file))
                //{
                //    foreach (ZipArchiveEntry entry in archive.Entries)
                //    {
                //        if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                //        {
                //            entry.ExtractToFile(Path.Combine(@initC.PathZipExtract, entry.FullName));
                //        }
                //    }
                //}
                using (FileStream zipToOpen = new FileStream(@file, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        ZipArchiveEntry readmeEntry = archive.CreateEntryFromFile(file, file.Replace(initC.PathArchive,""));
                    }
                }
            }
        }
        /*
         * g.	กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL
         * ,XCUST_POR_REQ_LINE_INT_ALL ,XCUST_POR_REQ_DIST_INT_ALLและ Update Validate_flag = ‘Y’
         */
        public void processInsertTable(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + initC.PathProcess, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            foreach (XcustPorReqHeaderIntAll xcprhia in listXcustPRHIA)
            {
                if(insertXcustPorReqHeaderIntAll(xcprhia, date, time).Equals("1"))
                {
                    foreach(XcustPorReqLineIntAll xcprlia in listXcustPRLIA)
                    {
                        //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                        String chk = xCPRLIADB.insert(xcprlia);
                    }
                    foreach (XcustPorReqDistIntAll xcprdia in listXcustPRDIA)
                    {
                        //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                        String chk = xCPRDIADB.insert(xcprdia);
                    }
                }
            }
        }
        /*
         *d. จากนั้น Program จะเอาข้อมูลจาก Table XCUST_LINFOX_PR_TBL มาทำการ Validate 
         * e.	ทำการหา Blanket Agreement Number โดยใช้ Supplier Code กับ Item Code หาค่า Blanket Agreement ที่ Active อยู่ ณ เวลานั้น มี Status เป็น Approved  กรณีไม่เจอ หรือเจอมากกว่า 1 ค่าให้ Validatte ไม่ผ่าน 
         * f.	กรณีที่มี Blanket Agreement และพบว่า Agreement นั้นมีการ Setup ค่า Minimum Relese 
         * และ Amount Limit ต้องทำการตรวจสอบว่าห้ามน้อยกว่า หรือมากกว่าค่าที่กำหนด  หากมากกว่าหรือน้อยกว่าต้อง Validate ไม่ผ่าน
         *g. กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL, XCUST_POR_REQ_LINE_INT_ALL, XCUST_POR_REQ_DIST_INT_ALL
         * Validate ไม่ผ่าน ลบ temp where ตาม filename
         * h.	กรณีที่ Validate ไม่ผ่าน จะะ Update Validate_flag = ‘E’ พร้อมระบุ Error Message
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
            String buCode = "", locator="", Org="", subInv_code="", currencyCode="", blanketAgreement="";
            ValidatePrPo vPP = new ValidatePrPo();
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();
            listXcustPRHIA.Clear();
            listXcustPRLIA.Clear();
            getListXcSIMT();
            getListXcIMT();
            getListXcSMT();
            getListXcVSMT();
            getListXcUMT();
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
                    //subInv_code = xCSIMTDB.validateSubInventoryCode1(initC.ORGANIZATION_code.Trim(), row[xCLFPTDB.xCLFPT.store_code].ToString().Trim());
                    subInv_code = validateSubInventoryCode(initC.ORGANIZATION_code.Trim(), row[xCLFPTDB.xCLFPT.store_code].ToString().Trim());
                    if (subInv_code.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-010 ";
                        vPP.Validate = "row " + row1 + " store_code =" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " ORGANIZATION_code " + initC.ORGANIZATION_code.Trim();
                        lVPr.Add(vPP);
                    }

                    // Error PO001 - 011 : Invalid Item Number
                    //if (!xCIMTDB.validateItemCodeByOrgRef1("300000000949654", row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim()))// ต้องแก้ Fix code อยู่
                    if (validateItemCodeByOrgRef("300000000949654", row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim()))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-011 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " item_code " + row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim();
                        lVPr.Add(vPP);
                    }
                    // Error PO001-015 : Invalid Supplier
                    //if (!xCSMTDB.validateSupplierBySupplierCode(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim()))
                    if (validateSupplierBySupplierCode(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-015 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " supplier_code " + row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim();
                        lVPr.Add(vPP);
                    }
                    // Error PO001-016 :  Invalid UOM
                    if (validateUOMCodeByUOMCode(row[xCLFPTDB.xCLFPT.UOMCODE].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-016 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " uom_code " + row[xCLFPTDB.xCLFPT.UOMCODE].ToString().Trim();
                        lVPr.Add(vPP);
                    }
                    // Error PO001-017 : Invalid CHARGE_ACCOUNT_SEGMENT1
                    //if (!xCVSMTDB.validateValueBySegment1("COMPANY RD CLOUD","Y", "11"))// ต้องแก้ Fix code อยู่
                    if (validateValueBySegment1("COMPANY RD CLOUD", "Y", "11"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-017 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT1 " ;
                        lVPr.Add(vPP);
                    }
                    // Error PO001-018 : Invalid CHARGE_ACCOUNT_SEGMENT2
                    //if (!xCVSMTDB.validateValueBySegment2("STORE RD CLOUD", "Y", "00000"))// ต้องแก้ Fix code อยู่
                    if (validateValueBySegment2("STORE RD CLOUD", "Y", "00000"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-018 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT2 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-019 : Invalid CHARGE_ACCOUNT_SEGMENT3
                    //if (!xCVSMTDB.validateValueBySegment3("ACCOUNT RD CLOUD", "Y", "117101"))// ต้องแก้ Fix code อยู่
                    if (validateValueBySegment3("ACCOUNT RD CLOUD", "Y", "117101"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-019 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT3 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-020 : Invalid CHARGE_ACCOUNT_SEGMENT4
                    //if (!xCVSMTDB.validateValueBySegment4("PROJECT RD CLOUD", "Y", subInv_code))// ต้องแก้ Fix code อยู่
                    if (validateValueBySegment4("PROJECT RD CLOUD", "Y", subInv_code))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-020 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT4 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-021 : Invalid CHARGE_ACCOUNT_SEGMENT5
                    //if (!xCVSMTDB.validateValueBySegment5("FUTURE1 RD CLOUD", "Y", "00"))// ต้องแก้ Fix code อยู่
                    if (validateValueBySegment5("FUTURE1 RD CLOUD", "Y", "00"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-021 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT5 ";
                        lVPr.Add(vPP);
                    }
                    // Error PO001-022 : Invalid CHARGE_ACCOUNT_SEGMENT6
                    //if (!xCVSMTDB.validateValueBySegment6("FUTURE2 RD CLOUD", "Y", "0000"))// ต้องแก้ Fix code อยู่
                    if (validateValueBySegment6("FUTURE2 RD CLOUD", "Y", "0000"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-022 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT6 ";
                        lVPr.Add(vPP);
                    }
                    
                    blanketAgreement = getBlanketAgreement(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim(), 
                        row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim(), row[xCLFPTDB.xCLFPT.QTY].ToString().Trim());
                    String[] aa = blanketAgreement.Split(',');
                    String price = "";
                    if (aa.Length > 0)
                    {
                        blanketAgreement = aa[0];
                        price = aa[1];
                    }
                    if (blanketAgreement.IndexOf("false")>=0)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-"+ blanketAgreement.Replace("flase","");
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT6 ";
                        lVPr.Add(vPP);
                        xCLFPTDB.updateValidateFlag(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim(), "E","", "kfc_po");
                    }
                    else
                    {
                        addXcustPRHIA(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim(), currDate, rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim());//ทำ รอไว้ เพื่อ process ช้า
                        row.BeginEdit();
                        row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER] = blanketAgreement;
                        row[xCLFPTDB.xCLFPT.PRICE] = price;
                        row.EndEdit();
                        addXcustPRLIAFromxCLFPT(row);
                        addXcustPRDIAFromxCLFPT(row);
                        xCLFPTDB.updateValidateFlag(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim(),"Y", blanketAgreement, "kfc_po");
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
        private String insertXcustPorReqHeaderIntAll(XcustPorReqHeaderIntAll xcprhia, String date, String time)
        {//row[dc].ToString().Trim().
            String chk = "";
            XcustPorReqHeaderIntAll xCPRHIA = new XcustPorReqHeaderIntAll();
            xCPRHIA.ATTRIBUTE1 = xcprhia.ATTRIBUTE1.Trim();

            xCPRHIA.ATTRIBUTE_DATE1 = date;
            xCPRHIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
            xCPRHIA.BATCH_ID = "";
            xCPRHIA.DESCRIPTIONS = xcprhia.DESCRIPTIONS.Trim();
            xCPRHIA.REQUESTER_EMAIL_ADDR = "";
            xCPRHIA.INTERFACE_SOURCE_CODE = "";
            xCPRHIA.ATTRIBUTE_CATEGORY = "";
            xCPRHIA.REQ_HEADER_INTERFACE_ID = xcprhia.REQ_HEADER_INTERFACE_ID.Trim();
            xCPRHIA.PROCESS_FLAG = "N";
            xCPRHIA.APPROVER_EMAIL_ADDR = "";
            xCPRHIA.REQ_BU_NAME = "";
            xCPRHIA.REQUITITION_NUMBER = "";
            xCPRHIA.IMPORT_SOURCE = xcprhia.IMPORT_SOURCE;
            xCPRHIA.ATTRIBUTE1 = xcprhia.ATTRIBUTE1;
            xCPRHIA.REQ_BU_NAME = xcprhia.REQ_BU_NAME;
            xCPRHIA.STATUS_CODE = xcprhia.STATUS_CODE;
            chk = xCPRHIADB.insert(xCPRHIA);
            return chk;
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
        /*
         * e.	ทำการหา Blanket Agreement Number โดยใช้ Supplier Code กับ Item Code หาค่า Blanket Agreement ที่ Active อยู่ ณ เวลานั้น 
         * มี Status เป็น Approved -> OPEN  กรณีไม่เจอ หรือเจอมากกว่า 1 ค่าให้ Validatte ไม่ผ่าน -> return false
         */
        public String selectBlanketAgreement(String supp_code, String item_code)
        {
            DataTable dt = new DataTable();
            String chk = "false", sql="";
            sql = "Select xcbaht.* From " + xCBAHTDB.xCBAHT.table +
                " xcbaht " +
                "Where " + xCBAHTDB.xCBAHT.SUPPLIER_CODE + "  = '"+supp_code+"' and "+xCBALTDB.xCBALT.ITEM_CODE+"='"+item_code+"' and "+
                xCBAHTDB.xCBAHT.STATUS+"='OPEN'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                if(dt.Rows.Count == 1)
                {
                    chk = dt.Rows[0][xCBAHTDB.xCBAHT.AGREEMENT_NUMBER].ToString().Trim();
                }
            }
            return chk;
        }
        /*
         * 
         * e.	ทำการหา Blanket Agreement Number โดยใช้ Supplier Code กับ Item Code หาค่า Blanket Agreement ที่ Active อยู่ ณ เวลานั้น 
         * มี Status เป็น Approved -> OPEN  กรณีไม่เจอ หรือเจอมากกว่า 1 ค่าให้ Validatte ไม่ผ่าน -> return false
         * f.	กรณีที่มี Blanket Agreement และพบว่า Agreement นั้นมีการ Setup ค่า Minimum Relese 
         * และ Amount Limit ต้องทำการตรวจสอบว่าห้ามน้อยกว่า หรือมากกว่าค่าที่กำหนด  หากมากกว่าหรือน้อยกว่าต้อง Validate ไม่ผ่าน
         */
        public String getBlanketAgreement(String supp_code, String item_code, String qty)
        {
            DataTable dt = new DataTable();
            String chk = "false,", sql = "";
            int min = 0, amt = 0;
            double qty1 = 0, price1 = 0;
            if (!double.TryParse(qty, out qty1))
            {
                return "false,";
            }
            //if (!double.TryParse(price, out price1))
            //{
            //    return "false";
            //}
            sql = "Select xcbaht.*, xcblt."+xCBALTDB.xCBALT.PRICE+" From " + xCBAHTDB.xCBAHT.table +
                " xcbaht " +
                "left join "+xCBALTDB.xCBALT.table+ " xcblt ON xcblt." + xCBALTDB.xCBALT.PO_HEADER_ID+ "=xcbaht."+xCBAHTDB.xCBAHT.PO_HEADER_ID+" "+
                "Where xcbaht." + xCBAHTDB.xCBAHT.SUPPLIER_CODE + "  = '" + supp_code + "' " +
                "and xcblt." + xCBALTDB.xCBALT.ITEM_CODE + "='" + item_code + "' and xcbaht." +
            xCBAHTDB.xCBAHT.STATUS + "='OPEN'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if(dt.Rows[0][xCBAHTDB.xCBAHT.MIN_RELEASE_AMT] != null)
                    {
                        int.TryParse(dt.Rows[0][xCBAHTDB.xCBAHT.MIN_RELEASE_AMT].ToString(), out min);
                        if (dt.Rows[0][xCBAHTDB.xCBAHT.AGREEMENT_AMT] != null)
                        {
                            int.TryParse(dt.Rows[0][xCBAHTDB.xCBAHT.AGREEMENT_AMT].ToString(), out amt);
                            if (dt.Rows[0]["price"] != null)
                            {
                                double.TryParse(dt.Rows[0]["price"].ToString(), out price1);
                                if ((price1 * qty1) <= amt)
                                {
                                    if ((price1 * qty1) >= min)
                                    {
                                        chk = dt.Rows[0][xCBAHTDB.xCBAHT.AGREEMENT_NUMBER].ToString().Trim()+","+ price1;
                                    }
                                    else
                                    {
                                        chk = "false,026";
                                    }
                                }
                                else
                                {
                                    chk = "false,025";
                                }
                            }
                            else
                            {
                                chk = "false,";
                            }
                        }
                    }
                }
                else
                {
                    chk = "false,024";
                }
            }
            else
            {
                chk = "false,023";
            }
            return chk;
        }
    }
}
