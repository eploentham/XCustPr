using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XCustPr
{
    public class ControlRDPO
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

        private IniFile iniFile;        //standard
        //public InitC initC;        //standard

        public XcustLinfoxPrTblDB xCLFPTDB;          // table temp

        public XcustPorReqHeaderIntAllDB xCPRHIADB;     // table จริง
        public XcustPorReqLineIntAllDB xCPRLIADB;     // table จริง
        public XcustPorReqDistIntAllDB xCPRDIADB;     // table จริง

        public XcustLinfoxPrTblDB xCLPTDB;

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
        public XcustSupplierSiteMstTblDB xCSSMTDB;

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
        private String dateStart = "";      //gen log

        public ControlRDPO(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            iniFile = new IniFile(Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini");        //standard
            //initC = new InitC();        //standard
            vPrPo = new ValidatePrPo();
            Cm.createFolderPO001();
            //GetConfig();        //standard

            //Cm = new ControlMain();     //standard
            conn = new ConnectDB("kfc_po", Cm.initC);        //standard
            xCLFPTDB = new XcustLinfoxPrTblDB(conn, Cm.initC);
            xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
            xCPRLIADB = new XcustPorReqLineIntAllDB(conn, Cm.initC);
            xCPRDIADB = new XcustPorReqDistIntAllDB(conn);
            xCLPTDB = new XcustLinfoxPrTblDB(conn, Cm.initC);

            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, Cm.initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, Cm.initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn, Cm.initC);
            xCIMTDB = new XcustItemMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCUMTDB = new XcustUomMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);
            xCBAHTDB = new XcustBlanketAgreementHeaderTblDB(conn, Cm.initC);
            xCBALTDB = new XcustBlanketAgreementLinesTblDB(conn, Cm.initC);
            xCSSMTDB = new XcustSupplierSiteMstTblDB(conn, Cm.initC);

            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard

            listXcustPRHIA = new List<XcustPorReqHeaderIntAll>();
            listXcustPRLIA = new List<XcustPorReqLineIntAll>();
            listXcustPRDIA = new List<XcustPorReqDistIntAll>();

            listXcSIMT = new List<XcustSubInventoryMstTbl>();
            listXcIMT = new List<XcustItemMstTbl>();
            listXcSMT = new List<XcustSupplierMstTbl>();
            listXcVSMT = new List<XcustValueSetMstTbl>();
            listXcUMT = new List<XcustUomMstTbl>();
        }
        private void getConfig()
        {

        }
        //public void GetConfig()
        //{
        //    initC.PathArchive = iniFile.Read("PathArchive");    //bit
        //    initC.PathError = iniFile.Read("PathError");
        //    initC.PathInitial = iniFile.Read("PathInitial");
        //    initC.PathProcess = iniFile.Read("PathProcess");
        //    initC.portDBBIT = iniFile.Read("portDBBIT");

        //    initC.APPROVER_EMAIL = iniFile.Read("APPROVER_EMAIL");    //bit demo
        //    initC.BU_NAME = iniFile.Read("BU_NAME");
        //    initC.Requester = iniFile.Read("Requester");
        //    initC.ImportSource = iniFile.Read("ImportSource");
        //    initC.Company = iniFile.Read("Company");
        //    initC.DELIVER_TO_LOCATTION = iniFile.Read("DELIVER_TO_LOCATTION");
        //    initC.ORGANIZATION_code = iniFile.Read("ORGANIZATION_code");
        //    initC.Locator = iniFile.Read("Locator");
        //    initC.Subinventory_Code = iniFile.Read("Subinventory_Code");
        //    initC.CURRENCY_CODE = iniFile.Read("CURRENCY_CODE");
        //    initC.PR_STATAUS = iniFile.Read("PR_STATAUS");
        //    initC.LINE_TYPE = iniFile.Read("LINE_TYPE");

        //    initC.EmailPort = iniFile.Read("EmailPort");

        //    initC.EmailCharset = iniFile.Read("EmailCharset");      //orc master
        //    initC.EmailUsername = iniFile.Read("EmailUsername");
        //    initC.EmailPassword = iniFile.Read("EmailPassword");
        //    initC.EmailSMTPSecure = iniFile.Read("EmailSMTPSecure");
        //    initC.PathLinfox = iniFile.Read("PathLinfox");

        //    initC.EmailHost = iniFile.Read("EmailHost");        // orc backoffice
        //    initC.EmailSender = iniFile.Read("EmailSender");
        //    initC.FTPServer = iniFile.Read("FTPServer");
        //    initC.PathZipExtract = iniFile.Read("PathZipExtract");
        //    initC.PathZip = iniFile.Read("PathZip");

        //    initC.databaseDBKFCPO = iniFile.Read("databaseDBKFCPO");        // orc BIT
        //    initC.hostDBKFCPO = iniFile.Read("hostDBKFCPO");
        //    initC.userDBKFCPO = iniFile.Read("userDBKFCPO");
        //    initC.passDBKFCPO = iniFile.Read("passDBKFCPO");
        //    initC.portDBKFCPO = iniFile.Read("portDBKFCPO");

        //    initC.AutoRunPO001 = iniFile.Read("AutoRunPO001");

        //    //initC.grdQuoColor = iniFile.Read("gridquotationcolor");

        //    //initC.HideCostQuotation = iniFile.Read("hidecostquotation");
        //    //if (initC.grdQuoColor.Equals(""))
        //    //{
        //    //    initC.grdQuoColor = "#b7e1cd";
        //    //}
        //    //initC.Password = regE.getPassword();
        //}

        //public String[] getFileinFolder(String path)
        //{
        //    string[] filePaths = Directory.GetFiles(@path);
        //    return filePaths;
        //}
        //public void moveFile(String sourceFile, String destinationFile)
        //{
        //    System.IO.File.Move(@sourceFile, @destinationFile);
        //}
        //public void deleteFile(String sourceFile)
        //{
        //    if (System.IO.File.Exists(sourceFile))
        //    {
        //        System.IO.File.Delete(@sourceFile);
        //    }
        //}
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
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
         * เตรียมข้อมูล เพื่อที่จะลง table XcustPorReqHeaderIntAll
         * เพราะในเอกสาร ต้องลงตาม group by po_number
         */
        private void addXcustPRHIA(String po_number, String curr_date, String filename, String requestId)
        {
            Boolean chk = true;
            foreach(XcustPorReqHeaderIntAll xcprhia in listXcustPRHIA)
            {
                if (xcprhia.REQ_HEADER_INTERFACE_ID.Equals(po_number))
                {
                    chk = false;
                }
            }
            if (chk)
            {
                String seq = String.Concat("00" + listXcustPRHIA.Count);
                XcustPorReqHeaderIntAll xcprhia1 = new XcustPorReqHeaderIntAll();
                //xcprhia1.ATTRIBUTE1 = po_number;
                xcprhia1.IMPORT_SOURCE = Cm.initC.ImportSource;
                xcprhia1.REQ_BU_NAME = Cm.initC.BU_NAME;
                xcprhia1.STATUS_CODE = Cm.initC.PR_STATAUS;
                xcprhia1.REQ_HEADER_INTERFACE_ID = po_number;
                xcprhia1.BATCH_ID = curr_date.Replace("-","") + seq.Substring(seq.Length-2);
                xcprhia1.REQUITITION_NUMBER = "PR"+curr_date.Substring(2,2);
                xcprhia1.DESCRIPTIONS = "LINFOX_"+ po_number+"_"+ filename;
                xcprhia1.ATTRIBUTE_CATEGORY = "LINFOX_PR";
                xcprhia1.ATTRIBUTE1 = Cm.initC.ImportSource;
                xcprhia1.ATTRIBUTE2 = po_number;
                xcprhia1.request_id = requestId;
                listXcustPRHIA.Add(xcprhia1);
            }
        }
        private void addXcustPRLIAFromxCLFPT(DataRow row, String subInv_code, String price, String agreementnumber, String agreementLinbNumber, String supplierSiteCode, String suppName)
        {
            XcustPorReqLineIntAll item = new XcustPorReqLineIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.DESTINATION_TYPE_CODE = "";
            item.PRC_BU_NAME = Cm.initC.BU_NAME;
            item.DELIVER_TO_ORGANIZATION_CODE = Cm.initC.ORGANIZATION_code;
            item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            item.DESTINATION_SUBINVENTORY = subInv_code;
            item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            item.NEED_BY_DATE = item.dateYearToDB(row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString());
            item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            item.LINE_TYPE = "GOODS";

            item.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString();
            item.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
            item.AGREEMENT_NUMBER = agreementnumber;
            item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";
            item.UOM_CODE = row[xCLFPTDB.xCLFPT.UOMCODE].ToString();
            item.request_id = row[xCLFPTDB.xCLFPT.request_id].ToString();
            item.ATTRIBUTE2 = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.AGREEMENT_LINE_NUMBER = agreementLinbNumber;
            item.ATTRIBUTE_CATEGORY = "LINFOX_PR";
            item.SUGGESTED_VENDOR_NAME = suppName;
            item.SUGGESTED_VENDOR_SITE = supplierSiteCode;
            item.Price = price;
            item.delivery_date = row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString();

            listXcustPRLIA.Add(item);
        }
        private void addXcustPRDIAFromxCLFPT(DataRow row, String subInv_code, String price)
        {
            XcustPorReqDistIntAll item = new XcustPorReqDistIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.CHARGE_ACCOUNT_SEGMENT2 = subInv_code;
            item.CHARGE_ACCOUNT_SEGMENT1 = "11";
            item.CHARGE_ACCOUNT_SEGMENT3 = "216133";
            item.CHARGE_ACCOUNT_SEGMENT4 = "000000";
            item.CHARGE_ACCOUNT_SEGMENT5 = "00";
            item.CHARGE_ACCOUNT_SEGMENT6 = "0000";

            //item.DELIVER_TO_ORGANIZATION_CODE = initC.ORGANIZATION_code;
            //item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            //item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            //item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            //item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            //item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            //item.LINE_TYPE = "";

            item.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString();
            item.request_id = row[xCLFPTDB.xCLFPT.request_id].ToString();
            item.price = price;
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";

            listXcustPRDIA.Add(item);
        }
        private XcustPorReqHeaderIntAll addXcustPRHIA1(String po_number, String curr_date, String filename, String requestId, int rowH)
        {
            XcustPorReqHeaderIntAll item = new XcustPorReqHeaderIntAll();
            
            String seq = String.Concat("00" + rowH);
                
            //xcprhia1.ATTRIBUTE1 = po_number;
            item.IMPORT_SOURCE = Cm.initC.ImportSource;
            item.REQ_BU_NAME = Cm.initC.BU_NAME;
            item.STATUS_CODE = Cm.initC.PR_STATAUS;
            item.REQ_HEADER_INTERFACE_ID = po_number;
            item.BATCH_ID = curr_date.Replace("-", "") + seq.Substring(seq.Length - 2);
            item.REQUITITION_NUMBER = "PR" + curr_date.Substring(2, 2);
            item.DESCRIPTIONS = "LINFOX_" + po_number + "_" + filename;
            item.ATTRIBUTE_CATEGORY = "LINFOX_PR";
            item.ATTRIBUTE1 = Cm.initC.ImportSource;
            item.ATTRIBUTE2 = po_number;
            item.request_id = requestId;
            //listXcustPRHIA.Add(xcprhia1);
            return item;
        }
        private XcustPorReqLineIntAll addXcustPRLIAFromxCLFPT1(DataRow row)
        {
            XcustPorReqLineIntAll item = new XcustPorReqLineIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.DESTINATION_TYPE_CODE = "";
            item.PRC_BU_NAME = Cm.initC.BU_NAME;
            item.DELIVER_TO_ORGANIZATION_CODE = Cm.initC.ORGANIZATION_code;
            item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            item.NEED_BY_DATE = item.dateYearToDB(row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString());
            item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            item.LINE_TYPE = "GOODS";

            item.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString();
            item.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
            item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";
            item.UOM_CODE = row[xCLFPTDB.xCLFPT.UOMCODE].ToString();
            item.request_id = row[xCLFPTDB.xCLFPT.request_id].ToString();
            item.ATTRIBUTE2 = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.AGREEMENT_LINE_NUMBER = row[xCLFPTDB.xCLFPT.AGREEMENT_LINE_NUMBER].ToString();
            item.ATTRIBUTE_CATEGORY = "LINFOX_PR";
            item.SUGGESTED_VENDOR_NAME = row[xCLFPTDB.xCLFPT.supplier_name].ToString();
            item.SUGGESTED_VENDOR_SITE = row[xCLFPTDB.xCLFPT.SUPPLIER_SITE_CODE].ToString();
            item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.delivery_date = row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString();

            return item;
        }
        private XcustPorReqDistIntAll addXcustPRDIAFromxCLFPT1(DataRow row)
        {
            XcustPorReqDistIntAll item = new XcustPorReqDistIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.CHARGE_ACCOUNT_SEGMENT2 = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            item.CHARGE_ACCOUNT_SEGMENT1 = "11";
            item.CHARGE_ACCOUNT_SEGMENT3 = "216133";
            item.CHARGE_ACCOUNT_SEGMENT4 = "000000";
            item.CHARGE_ACCOUNT_SEGMENT5 = "00";
            item.CHARGE_ACCOUNT_SEGMENT6 = "0000";

            //item.DELIVER_TO_ORGANIZATION_CODE = initC.ORGANIZATION_code;
            //item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            //item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            //item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            //item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            //item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            //item.LINE_TYPE = "";

            item.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString();
            item.request_id = row[xCLFPTDB.xCLFPT.request_id].ToString();
            item.price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";

            return item;
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
            addListView("หา Blanket Agreement " + Cm.initC.PathProcess, "Blanket", lv1, form1);
        }
        public void processGenCSV(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String requestId)
        {
            addListView("processGenCSVxCPRHIA ", "CVS", lv1, form1);
            processGenCSVxCPRHIA(lv1, form1, pB1, "PO001", requestId);
            addListView("processGenCSVxCPRLIA ", "CVS", lv1, form1);
            processGenCSVxCPRLIA(lv1, form1, pB1, "PO001", requestId);
            addListView("processGenCSVxCPRDIA ", "CVS", lv1, form1);
            processGenCSVxCPRDIA(lv1, form1, pB1, "PO001", requestId);
            addListView("processGenZIP ", "CVS", lv1, form1);
            processGenZIP(lv1,form1, pB1, "PO001");
        }
        public void processGenCSVxCPRHIA(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag, String requestId)
        {
            var file = Cm.initC.PathFileCSV+ "PorReqHeadersInterfaceAll.csv";
            DataTable dt;
            if (flag.Equals("PO001"))
            {
                dt = xCPRHIADB.selectGenTextCSV(requestId);
            }
            else
            {
                dt = xCPRHIADB.selectAll();
            }
            
            addListView("processGenCSVxCPRHIA จำนวนข้อมูล "+dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach(DataRow row in dt.Rows)
                {
                    string col01 = row[xCPRHIADB.xCPRHIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col02 = row[xCPRHIADB.xCPRHIA.IMPORT_SOURCE].ToString();
                    string col03 = row[xCPRHIADB.xCPRHIA.REQ_BU_NAME].ToString();
                    string col04 = row[xCPRHIADB.xCPRHIA. request_id].ToString();
                    string col05 = "";// รอถาม  Interface Source Line ID
                    string col06 = row[xCPRHIADB.xCPRHIA.STATUS_CODE].ToString();
                    string col07 = Cm.initC.POAPPROVER;// รอถาม  Approver
                    string col08 = Cm.initC.POAPPROVER_EMAIL;
                    string col09 = Cm.initC.BU_NAME;// รอถาม  Entered By*
                    //string col10 = row[xCPRHIADB.xCPRHIA.REQUITITION_NUMBER].ToString();
                    String col10 = "";

                    string col11 = row[xCPRHIADB.xCPRHIA.DESCRIPTIONS].ToString();
                    string col12 = "";//Taxation Country
                    string col13 = "";//Taxation Territory
                    string col14 = "";//Document Fiscal Classification Code
                    string col15 = "";//Document Fiscal Classification
                    string col16 = "";//Justification
                    string col17 = "";
                    string col18 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE1].ToString();
                    string col19 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE2].ToString();
                    string col20 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE3].ToString();

                    string col21 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE4].ToString();
                    string col22 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE5].ToString();
                    string col23 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE6].ToString();
                    string col24 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE7].ToString();
                    string col25 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE8].ToString();
                    string col26 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE9].ToString();
                    string col27 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE10].ToString();
                    string col28 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE11].ToString();
                    string col29 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE12].ToString();
                    string col30 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE13].ToString();

                    string col31 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE14].ToString();
                    string col32 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE15].ToString();
                    string col33 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE16].ToString();
                    string col34 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE17].ToString();
                    string col35 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE18].ToString();
                    string col36 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE19].ToString();
                    string col37 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE20].ToString();
                    string col38 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE1].ToString();
                    string col39 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE2].ToString();
                    string col40 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE3].ToString();

                    string col41 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE4].ToString();
                    string col42 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE5].ToString();
                    string col43 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE6].ToString();
                    string col44 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE7].ToString();
                    string col45 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE8].ToString();
                    string col46 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE9].ToString();
                    string col47 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_DATE10].ToString();
                    string col48 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP1].ToString();
                    string col49 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP2].ToString();
                    string col50 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP3].ToString();

                    string col51 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP4].ToString();
                    string col52 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP5].ToString();
                    string col53 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP6].ToString();
                    string col54 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP7].ToString();
                    string col55 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP8].ToString();
                    string col56 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP9].ToString();
                    string col57 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP10].ToString();
                    string col58 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER1].ToString().Equals("0")?"": row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER1].ToString();
                    string col59 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER2].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER2].ToString();
                    string col60 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER3].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER3].ToString();

                    string col61 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER4].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER4].ToString();
                    string col62 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER5].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER5].ToString();
                    string col63 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER6].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER6].ToString();
                    string col64 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER7].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER7].ToString();
                    string col65 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER8].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER8].ToString();
                    string col66 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER9].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER9].ToString();
                    string col67 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER10].ToString().Equals("0") ? "" : row[xCPRHIADB.xCPRHIA.ATTRIBUTE_NUMBER10].ToString();
                    string col68 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_CATEGORY].ToString();
                    string col69 = "";
                    string col70 = "Y";
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

                    string csvRow = col01+","+col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 +"," + col08 +"," + col09 +"," + col10
                        +","+ col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
                        + "," + col21 + "," + col22 + "," + col23 + "," + col24 + "," + col25 + "," + col26 + "," + col27 + "," + col28 + "," + col29 + "," + col30
                        + "," + col31 + "," + col32 + "," + col33 + "," + col34 + "," + col35 + "," + col36 + "," + col37 + "," + col38 + "," + col39 + "," + col40
                        + "," + col41 + "," + col42 + "," + col43 + "," + col44 + "," + col45 + "," + col46 + "," + col47 + "," + col48 + "," + col49 + "," + col50
                        + "," + col51 + "," + col52 + "," + col53 + "," + col54 + "," + col55 + "," + col56 + "," + col57 + "," + col58 + "," + col59 + "," + col60
                        + "," + col61 + "," + col62 + "," + col63 + "," + col64 + "," + col65 + "," + col66 + "," + col67 + "," + col68 + "," + col69 + "," + col70 + ",END";

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenCSVxCPRLIA(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag, String requestId)
        {
            var file = Cm.initC.PathFileCSV + "PorReqLinesInterfaceAll.csv";
            DataTable dt;
            if (flag.Equals("PO001"))
            {
                dt = xCPRLIADB.selectGenTextCSV(requestId);
            }
            else
            {
                dt = xCPRLIADB.selectAll();
            }
                
            addListView("processGenCSVxCPRLIA จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCPRLIADB.xCPRLIA.REQ_LINE_INTERFACE_ID].ToString();
                    string col02 = row[xCPRLIADB.xCPRLIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col03 = "";
                    string col04 = Cm.initC.DESTINATION_TYPE_CODE;
                    string col05 = row[xCPRLIADB.xCPRLIA.DESTINATION_SUBINVENTORY].ToString();
                    string col06 = row[xCPRLIADB.xCPRLIA.DELIVER_TO_ORGANIZATION_CODE].ToString();
                    string col07 = row[xCPRLIADB.xCPRLIA.DESTINATION_SUBINVENTORY].ToString();
                    string col08 = Cm.initC.Requester;         //Requester
                    string col09 = "";         //Item Description
                    string col10 = row[xCPRLIADB.xCPRLIA.CATEGORY_NAME].ToString();     //Category Name

                    string col11 = row[xCPRLIADB.xCPRLIA.NEED_BY_DATE].ToString();     // Need - by Date        ok formet date

                    string col12 = row[xCPRLIADB.xCPRLIA.ITEM_CODE].ToString();
                    string col13 = "";         //Revision
                    string col14 = row[xCPRLIADB.xCPRLIA.UOM_CODE].ToString();         //UOM Code
                    string col15 = Cm.initC.LINE_TYPE;
                    string col16 = row[xCPRLIADB.xCPRLIA.QTY].ToString();
                    string col17 = row[xCPRLIADB.xCPRLIA.CURRENCY_CODE].ToString();
                    //string col18 = row[xCPRLIADB.xCPRLIA.Price].ToString();
                    string col18 = row[xCPRLIADB.xCPRLIA.Price].ToString();// ใน table ไม่มี filed price เลยเอามาฝาก ใส่ ATTRIBUTE_NUMBER10 ไปก่อน
                    string col19 = "";     //Conversion Rate
                    string col20 = "";     //Conversion Date

                    string col21 = "";       //Conversion Rate Type
                    string col22 = "";     //Secondary UOM Code
                    string col23 = "";     //Secondary Quantity
                    string col24 = "";     //amount
                    string col25 = "";     //UN Number
                    string col26 = "";     //Hazard Class
                    string col27 = row[xCPRLIADB.xCPRLIA.PRC_BU_NAME].ToString();
                    string col28 = row[xCPRLIADB.xCPRLIA.AGREEMENT_NUMBER].ToString();          //Agreement
                    string col29 = row[xCPRLIADB.xCPRLIA.AGREEMENT_LINE_NUMBER].ToString();     //Agreement Line 
                    string col30 = row[xCPRLIADB.xCPRLIA.SUGGESTED_VENDOR_NAME].ToString();             //Supplier

                    string col31 = row[xCPRLIADB.xCPRLIA.SUGGESTED_VENDOR_SITE].ToString();     //Supplier Site
                    string col32 = "";     //Supplier Contact
                    string col33 = "";     //phone
                    string col34 = "";     //fax
                    string col35 = "";     //email
                    string col36 = "";     //Supplier Item
                    string col37 = "";     //Suggested Buyer
                    string col38 = "";     //Autosource Flag
                    string col39 = "";     //Negotiated
                    string col40 = "";     //Negotiation Required

                    string col41 = "";       //Urgent
                    string col42 = "";     //New Supplier
                    string col43 = "";     //Note to Buyer
                    string col44 = "";     //Note to Receiver
                    string col45 = "";     //Transaction Business Category
                    string col46 = "";     //Transaction Business Category Name
                    string col47 = "";     //Product Type
                    string col48 = "";     //Product Type Name
                    string col49 = "";     //Product-Fiscal Classification
                    string col50 = "";     //Product-Fiscal Classification Name

                    string col51 = "";         //Product Category
                    string col52 = "";         //Product Category Name
                    string col53 = "";         //Intended Use
                    string col54 = "";         //Intended Use Name
                    string col55 = "";         //User-Defined Fiscal Classification
                    string col56 = "";         //User-Defined Fiscal Classification Name
                    string col57 = "";         //Tax Classification Code
                    string col58 = "";         //Tax Classification Name
                    string col59 = "";         //Assessable Value
                    string col60 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE1].ToString();

                    string col61 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE2].ToString();
                    string col62 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE3].ToString();
                    string col63 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE4].ToString();
                    string col64 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE5].ToString();
                    string col65 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE6].ToString();
                    string col66 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE7].ToString();
                    string col67 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE8].ToString();
                    string col68 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE9].ToString();
                    string col69 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE10].ToString();
                    string col70 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE11].ToString();

                    string col71 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE12].ToString();
                    string col72 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE13].ToString();
                    string col73 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE14].ToString();
                    string col74 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE15].ToString();
                    string col75 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE16].ToString();
                    string col76 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE17].ToString();
                    string col77 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE18].ToString();
                    string col78 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE19].ToString();
                    string col79 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE20].ToString();

                    string col80 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE1].ToString();

                    string col81 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE2].ToString();     //ATTRIBUTE_DATE1
                    string col82 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE3].ToString();
                    string col83 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE4].ToString();
                    string col84 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE5].ToString();
                    string col85 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE6].ToString();
                    string col86 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE7].ToString();
                    string col87 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE8].ToString();
                    string col88 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE9].ToString();
                    string col89 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_DATE10].ToString();
                    string col90 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP1].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP1].ToString();

                    string col91 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP2].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP2].ToString();     //ATTRIBUTE_TIMESTAMP1
                    string col92 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP3].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP3].ToString();
                    string col93 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP4].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP4].ToString();
                    string col94 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP5].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP5].ToString();
                    string col95 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP6].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP6].ToString();
                    string col96 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP7].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP7].ToString();
                    string col97 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP8].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP8].ToString();
                    string col98 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP9].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP9].ToString();
                    string col99 = row[xCPRHIADB.xCPRHIA.ATTRIBUTE_TIMESTAMP10].ToString().Equals("0") ? "" : row[xCPRLIADB.xCPRLIA.ATTRIBUTE_TIMESTAMP10].ToString();
                    string col100 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER1].ToString();

                    string col101 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER2].ToString();       //ATTRIBUTE_NUMBER1
                    string col102 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER3].ToString();
                    string col103 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER4].ToString();
                    string col104 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER5].ToString();
                    string col105 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER6].ToString();
                    string col106 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER7].ToString();
                    string col107 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER8].ToString();
                    string col108 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER9].ToString();
                    string col109 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_NUMBER10].ToString();
                    string col110 = row[xCPRLIADB.xCPRLIA.ATTRIBUTE_CATEGORY].ToString();

                    string col111 = "";       //Supplier Number
                    string col112 = "";       //
                    string col113 = "";       //Third-Party Tax Registration Number
                    string col114 = "";       //Location of Final Discharge
                    string col115 = "";       //Note To Supplier
                    string col116 = "";       //Carrier
                    string col117 = "";       //Mode Of Transport Code
                    string col118 = "";       //Requested Ship Date
                    string col119 = "";       //Service Level Code
                    DateTime dtDeli = DateTime.Parse(row[xCPRLIADB.xCPRLIA.delivery_date].ToString());
                    string col120 = dtDeli.Year.ToString()+"-"+dtDeli.ToString("MM")+"-"+dtDeli.Day.ToString("00");       //Requested Delivery Date

                    string col121 = "";       //Orchestration Code
                    string col122 = "";       //Work Order Product
                    string col123 = "";       //Work Order ID
                    string col124 = "";       //Work Order Number
                    string col125 = row[xCPRLIADB.xCPRLIA.UOM_CODE].ToString();     //UOM
                    string col126 = "";       //Secondary UOM

                    //col61 = "666";
                    
                    //col71 = "777";
                    //col72 = "72";
                    //col73 = "73";
                    //col74 = "74";
                    //col75 = "75";
                    //col76 = "76";
                    //col77 = "77";
                    //col78 = "78";
                    //col79 = "79";

                    //col81 = "888";
                    //col91 = "999";
                    //col110 = "110";
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
                        +","+ col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
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
                        + "," + col121 + "," + col122 + "," + col123 + "," + col124 + "," + col125 + "," + col126 + ",END";

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenCSVxCPRDIA(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag, String requestId)
        {
            var file = Cm.initC.PathFileCSV + "PorReqDistsInterfaceAll.csv";
            DataTable dt;
            if (flag.Equals("PO001"))
            {
                dt = xCPRDIADB.selectGenTextCSV(requestId);
            }
            else
            {
                dt = xCPRDIADB.selectAll();
            }
            addListView("processGenCSVxCPRDIA จำนวนข้อมูล " + dt.Rows.Count, "CVS", lv1, form1);
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = row[xCPRDIADB.xCPRDIA.REQ_HEADER_INTERFACE_ID].ToString();
                    string col02 = row[xCPRDIADB.xCPRDIA.REQ_LINE_INTERFACE_ID].ToString(); //REQ_LINE_INTERFACE_ID
                    string col03 = "";     //Percentage
                    string col04 = row[xCPRDIADB.xCPRDIA.REQ_DIST_INTERFACE_ID].ToString();     //Distribution
                    string col05 = row[xCPRDIADB.xCPRDIA.QTY].ToString();         //Quantity
                    String qty = "";
                    String price = "";
                    qty = row[xCPRDIADB.xCPRDIA.QTY].ToString();
                    price = row[xCPRDIADB.xCPRDIA.price].ToString();
                    price = price.Equals("") ? "1" : price;
                    string col06 = String.Concat(Double.Parse(qty)*Double.Parse(price));     //Amount
                    string col07 = "";     //Project Name
                    string col08 = "";     //Task Name
                    string col09 = "";     //Expenditure Type
                    string col10 = "";     //Expenditure Item Date

                    string col11 = "";       //Expenditure Organization
                    string col12 = "";     //Billable
                    string col13 = "";     //Capitalizable
                    string col14 = "";     //PJC_USER_DEF_ATTRIBUTE1
                    string col15 = "";     //PJC_USER_DEF_ATTRIBUTE2
                    string col16 = "";
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
                    string col34 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE1].ToString();         //ATTRIBUTE1 
                    string col35 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE2].ToString();
                    string col36 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE3].ToString();
                    string col37 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE4].ToString();
                    string col38 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE5].ToString();
                    string col39 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE6].ToString();
                    string col40 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE7].ToString();

                    string col41 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE8].ToString();
                    string col42 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE9].ToString();
                    string col43 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE10].ToString();
                    string col44 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE11].ToString();
                    string col45 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE12].ToString();
                    string col46 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE13].ToString();
                    string col47 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE14].ToString();
                    string col48 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE15].ToString();
                    string col49 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE16].ToString();
                    string col50 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE17].ToString();

                    string col51 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE18].ToString();
                    string col52 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE19].ToString();
                    string col53 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE20].ToString();
                    string col54 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE1].ToString();         //ATTRIBUTE_DATE1
                    string col55 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE2].ToString();
                    string col56 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE3].ToString();
                    string col57 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE4].ToString();
                    string col58 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE5].ToString();
                    string col59 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE6].ToString();
                    string col60 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE7].ToString();

                    string col61 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE8].ToString();
                    string col62 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE9].ToString();
                    string col63 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_DATE10].ToString();
                    string col64 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP1].ToString(); //ATTRIBUTE_TIMESTAMP1
                    string col65 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP2].ToString();
                    string col66 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP3].ToString();
                    string col67 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP4].ToString();
                    string col68 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP5].ToString();
                    string col69 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP6].ToString();
                    string col70 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP7].ToString();

                    string col71 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP8].ToString();
                    string col72 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP9].ToString();
                    string col73 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_TIMESTAMP10].ToString();
                    string col74 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER1].ToString();      //ATTRIBUTE_NUMBER1
                    string col75 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER2].ToString();
                    string col76 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER3].ToString();
                    string col77 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER4].ToString();
                    string col78 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER5].ToString();
                    string col79 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER6].ToString();
                    string col80 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER7].ToString();

                    string col81 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER8].ToString();
                    string col82 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER9].ToString();
                    string col83 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_NUMBER10].ToString();
                    //string col84 = row[xCPRDIADB.xCPRDIA.ATTRIBUTE_CATEGORY].ToString();        //ATTRIBUTE_CATEGORY
                    string col84 = "";      // ในtable ไม่มี field นี้
                    string col85 = row[xCPRDIADB.xCPRDIA.CHARGE_ACCOUNT_SEGMENT1].ToString();
                    string col86 = row[xCPRDIADB.xCPRDIA.CHARGE_ACCOUNT_SEGMENT2].ToString();
                    string col87 = row[xCPRDIADB.xCPRDIA.CHARGE_ACCOUNT_SEGMENT3].ToString();
                    string col88 = row[xCPRDIADB.xCPRDIA.CHARGE_ACCOUNT_SEGMENT4].ToString();
                    string col89 = row[xCPRDIADB.xCPRDIA.CHARGE_ACCOUNT_SEGMENT5].ToString();
                    string col90 = row[xCPRDIADB.xCPRDIA.CHARGE_ACCOUNT_SEGMENT6].ToString();

                    string col91 = "";         //CHARGE_ACCOUNT_SEGMENT7
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
                    string col112 = "";       //CHARGE_ACCOUNT_SEGMENT28
                    string col113 = "";       //CHARGE_ACCOUNT_SEGMENT29
                    string col114 = "";       //CHARGE_ACCOUNT_SEGMENT30
                    string col115 = "";       //Work Type
                    string col116 = "";       //Budget Date
                    string col117 = "";       //Project Number
                    string col118 = "";       //Contract Name
                    string col119 = "";       //Contract Number
                    string col120 = "";       //Funding Source Name

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
                        + "," + col111 + "," + col112 + "," + col113 + "," + col114 + "," + col115 + "," + col116 + "," + col117 + "," + col118 + "," + col119 + "," + col120+",END";

                    stream.WriteLine(csvRow);
                }
            }
        }
        public void processGenZIP(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String flag)
        {
            addListView("create zip file " + Cm.initC.PathFileCSV, "Validate", lv1, form1);
            String currDate = System.DateTime.Now.ToString("yyyyMMdd");
            String currTime = System.DateTime.Now.ToString("HHmmsss");

            //var result = DateTime.ParseExact(currDate+" "+currTime,
            //                     new CultureInfo("en-US"));

            String filenameZip = "", ilename2 = "", ilename3 = "", filename="";
            if (flag.Equals("PO001"))
            {
                filenameZip = Cm.initC.PathFileZip + "\\LINFOX_PR_"+ currDate +currTime+ ".zip";
                filename = @Cm.initC.PathFileCSV;
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
                //zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
            zip.Dispose();
        }
        /*
         * g.	กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL
         * ,XCUST_POR_REQ_LINE_INT_ALL ,XCUST_POR_REQ_DIST_INT_ALLและ Update Validate_flag = ‘Y’
         */
        public void processInsertTable(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + Cm.initC.PathProcess, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            foreach (XcustPorReqHeaderIntAll xcprhia in listXcustPRHIA)
            {
                String seqH = "";
                seqH = insertXcustPorReqHeaderIntAll(xcprhia, date, time);
                int seqH1 = 0;
                if(int.TryParse(seqH,out seqH1))
                {
                    //foreach (XcustPorReqLineIntAll xcprlia in listXcustPRLIA)
                    //{
                    //    if (xcprhia.REQ_HEADER_INTERFACE_ID.Trim().Equals(xcprhia.REQ_HEADER_INTERFACE_ID))
                    //    {
                    //        String chk = xCPRLIADB.insert(xcprlia);
                    //    }
                    //}
                }
            }
            foreach (XcustPorReqLineIntAll xcprlia in listXcustPRLIA)
            {
                //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                String chk = xCPRLIADB.insert(xcprlia, Cm.initC.pathLogErr);
            }
            foreach (XcustPorReqDistIntAll xcprdia in listXcustPRDIA)
            {
                //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                String chk = xCPRDIADB.insert(xcprdia, Cm.initC.PathLog);
            }
        }
        public void processInsertTable1(String requestId, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + Cm.initC.PathProcess, "Validate", lv1, form1);
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            int rowH = 0;
            DataTable dt = new DataTable();
            dt = xCLFPTDB.selectLinfoxGroupByPoNumber(requestId);
            foreach(DataRow row in dt.Rows)
            {
                rowH++;
                String poNumber = "";
                poNumber = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
                DataTable dtLinfox = new DataTable();
                dtLinfox = xCLFPTDB.selectLinfoxByPoNumber(requestId, poNumber);
                XcustPorReqHeaderIntAll xCPorRHIA = addXcustPRHIA1(poNumber, currDate, row[xCLFPTDB.xCLFPT.file_name].ToString(), requestId, rowH);
                String seqH = "";
                seqH = xCPRHIADB.insert(xCPorRHIA, Cm.initC.pathLogErr);
                foreach (DataRow rowLinfox in dtLinfox.Rows)
                {
                    XcustPorReqLineIntAll xCPorRLIA = new XcustPorReqLineIntAll();
                    xCPorRLIA = addXcustPRLIAFromxCLFPT1(rowLinfox);
                    xCPorRLIA.REQ_HEADER_INTERFACE_ID = seqH;
                    String seqL = xCPRLIADB.insert(xCPorRLIA, Cm.initC.pathLogErr);

                    XcustPorReqDistIntAll xCPorRDIA = new XcustPorReqDistIntAll();
                    xCPorRDIA = addXcustPRDIAFromxCLFPT1(rowLinfox);
                    xCPorRDIA.REQ_HEADER_INTERFACE_ID = seqH;
                    xCPorRDIA.REQ_LINE_INTERFACE_ID = seqL;
                    String chk = xCPRDIADB.insert(xCPorRDIA, Cm.initC.PO005PathLog);
                }
            }
            xCLFPTDB.updatePrcessFlag(requestId, "kfc_po", Cm.initC.pathLogErr);
        }
        public void processInsertTable2(String requestId, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + Cm.initC.PathProcess, "Validate", lv1, form1);
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            int rowH = 0;
            DataTable dt = new DataTable();
            dt = xCLFPTDB.selectFilenameByRequestId(requestId);     //moveFileToFolderArchiveError
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String filename = "", rowCnt = "";
                    filename = row[xCLFPTDB.xCLFPT.file_name].ToString();
                    rowCnt = row["row_Cnt"].ToString();
                    String cnt = "";
                    cnt = xCLFPTDB.getCountNoErrorByFilename(requestId, filename);

                    if (cnt.Equals(rowCnt))
                    {
                        //ที่ ผ่าน ทั้ง file
                        DataTable dtFilename = new DataTable();
                        //dtFilename = xCLFPTDB.selectLinfoxByFilename(filename, requestId);
                        dtFilename = xCLFPTDB.selectLinfoxGroupByPoNumber(filename, requestId);
                        if (dtFilename.Rows.Count > 0)
                        {
                            foreach(DataRow rowFilename in dtFilename.Rows)
                            {
                                rowH++;
                                String poNumber = "";
                                poNumber = rowFilename[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
                                DataTable dtLinfox = new DataTable();
                                dtLinfox = xCLFPTDB.selectLinfoxByPoNumber(requestId, poNumber);
                                XcustPorReqHeaderIntAll xCPorRHIA = addXcustPRHIA1(poNumber, currDate, row[xCLFPTDB.xCLFPT.file_name].ToString(), requestId, rowH);
                                String seqH = "";
                                seqH = xCPRHIADB.insert(xCPorRHIA, Cm.initC.pathLogErr);
                                foreach (DataRow rowLinfox in dtLinfox.Rows)
                                {
                                    XcustPorReqLineIntAll xCPorRLIA = new XcustPorReqLineIntAll();
                                    xCPorRLIA = addXcustPRLIAFromxCLFPT1(rowLinfox);
                                    xCPorRLIA.REQ_HEADER_INTERFACE_ID = seqH;
                                    String seqL = xCPRLIADB.insert(xCPorRLIA, Cm.initC.pathLogErr);

                                    XcustPorReqDistIntAll xCPorRDIA = new XcustPorReqDistIntAll();
                                    xCPorRDIA = addXcustPRDIAFromxCLFPT1(rowLinfox);
                                    xCPorRDIA.REQ_HEADER_INTERFACE_ID = seqH;
                                    xCPorRDIA.REQ_LINE_INTERFACE_ID = seqL;
                                    String chk = xCPRDIADB.insert(xCPorRDIA, Cm.initC.PO005PathLog);
                                }
                            }
                        }
                    }
                }
            }
            
            xCLFPTDB.updatePrcessFlag(requestId, "kfc_po", Cm.initC.pathLogErr);
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
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String requestId)
        {
            addListView("อ่าน file จาก " + Cm.initC.PathProcess, "Validate", lv1, form1);
            pB1.Visible = true;
            Boolean chk = false;
            DataTable dtGroupBy = new DataTable();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            String buCode = "", locator="", Org="", subInv_code="", currencyCode="", blanketAgreement="";

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
            int cntErr =0, cntFileErr=0;   // gen log

            buCode = xCBMTDB.selectActiveBuName();
            //Error PO001-004 : Invalid Requisitioning BU
            if (!buCode.Equals(Cm.initC.BU_NAME.Trim()))
            {
                chk = false;
                vPP = new ValidatePrPo();
                vPP.Filename = "PO001 Parameter ";
                vPP.Message = " PO001-004 : Invalid Requisitioning BU";
                vPP.Validate = "";
                lVPr.Add(vPP);
                cntErr++;       // gen log

            }
            
            //Error PO001-009 : Invalid Deliver-to Organization
            Org = xCDOMTDB.selectActiveByCode(Cm.initC.ORGANIZATION_code.Trim());
            if (Org.Equals(""))
            {
                chk = false;
                vPP = new ValidatePrPo();
                vPP.Filename = "PO001 Parameter ";
                vPP.Message = " PO001-009 : Invalid Deliver-to Organization";
                vPP.Validate = "";
                lVPr.Add(vPP);
                cntErr++;       // gen log
            }
            else if (Org.Equals("D"))
            {
                chk = false;
                vPP = new ValidatePrPo();
                vPP.Filename = "PO001 Parameter ";
                vPP.Message = " PO001-009 : Duppicate Deliver-to Organization";
                vPP.Validate = "";
                lVPr.Add(vPP);
                cntErr++;       // gen log
            }

            //Error PO001-008 : Invalid Deliver To Location
            locator = xCDLMTDB.selectLocatorByInvtory(Cm.initC.Locator.Trim(), Org);
            if (!locator.Equals(Cm.initC.Locator.Trim()))
            {
                chk = false;
                vPP = new ValidatePrPo();
                vPP.Filename = "PO001 Parameter ";
                vPP.Message = " PO001-008 : Invalid Deliver To Location";
                vPP.Validate = "";
                lVPr.Add(vPP);
                cntErr++;       // gen log
            }

            //Error PO001-013 : Invalid Currency Code
            if (!xCMTDB.validateCurrencyCodeBycurrCode(Cm.initC.CURRENCY_CODE))
            {
                chk = false;
                vPP = new ValidatePrPo();
                vPP.Filename = "PO001 Parameter ";
                vPP.Message = " PO001-013 : Invalid Currency Code";
                vPP.Validate = "";
                lVPr.Add(vPP);
                cntErr++;       // gen log
            }
            //Error PO001-014 : Invalid Procurement BU
            if (!buCode.Equals(Cm.initC.BU_NAME.Trim()))
            {
                chk = false;
                vPP = new ValidatePrPo();
                vPP.Filename = "PO001 Parameter ";
                vPP.Message = " PO001-014 : Invalid Procurement BU";
                vPP.Validate = "";
                lVPr.Add(vPP);
                cntErr++;       // gen log
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
                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();   // gen log
                vF.recordTotal = dt.Rows.Count.ToString();   // gen log
                dt = xCLFPTDB.selectLinfoxByFilename(rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim());    // ข้อมูลใน file
                row1 = 0;
                cntErr = 0;     //gen log
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {
                    String poNumber = "", lineNumber = "";
                    poNumber = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
                    lineNumber = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();

                    row1++;
                    pB1.Value = row1;
                    //Error PO001-006 : Invalid data type
                    chk = validateQTY(row[xCLFPTDB.xCLFPT.QTY].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-006 : Invalid data type";
                        vPP.Validate = "row "+ row1 + " QTY=" + row[xCLFPTDB.xCLFPT.QTY].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-006 : Invalid data type", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    //Error PO001-002 : Invalid data type
                    chk = validateDate(row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-002 : Date Format not correct";
                        vPP.Validate = "row " + row1 + " ORDER_DATE=" + row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-002 : Date Format not correct", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    chk = validateDate(row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString());//ต้องแก้ไข เพราะ agreement เข้า method มีค่าเป็น date
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-002 : Date Format not correct";
                        vPP.Validate = "row " + row1 + " REQUEST_DATE=" + row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString();
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-002 : Date Format not correct", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    
                    //Error PO001-010 : Invalid Subinventory Code
                    //subInv_code = xCSIMTDB.validateSubInventoryCode1(initC.ORGANIZATION_code.Trim(), row[xCLFPTDB.xCLFPT.store_code].ToString().Trim());
                    subInv_code = xCSIMTDB.validateSubInventoryCode1(Org, row[xCLFPTDB.xCLFPT.store_code].ToString().Trim());
                    if (subInv_code.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-010 : Invalid Subinventory Code";
                        vPP.Validate = "row " + row1 + " store_code =" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " ORGANIZATION_code " + Cm.initC.ORGANIZATION_code.Trim();
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-010 : Invalid Subinventory Code", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    chk = xCLFPTDB.validateGL("11", "00000", "219120", "000000", "00", "0000", buCode);//ต้องแก้ไข เพราะ agreement เข้า method มีค่าเป็น date
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-029 : validateGL ";
                        vPP.Validate = "row " + row1 + " validateGL";
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-029 : validateGL ", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    DateTime reDate = DateTime.Parse(xCLFPTDB.xCLFPT.dateYearToDB( row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString()));
                    String currDate1 = System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString("00") + "-" + System.DateTime.Now.Day.ToString("00");
                    DateTime currdate = DateTime.Parse(currDate1);
                    if (DateTime.Compare(currdate, reDate) >= 1)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-030 : request_date < current date ";
                        vPP.Validate = "row " + row1 + " request date <= current date " + row[xCLFPTDB.xCLFPT.REQUEST_DATE].ToString();
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-030 : request_date < current date ", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }

                    // Error PO001 - 011 : Invalid Item Number
                    //if (!xCIMTDB.validateItemCodeByOrgRef1("300000000949654", row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim()))// ต้องแก้ Fix code อยู่
                    if (!xCIMTDB.validateItemCodeByOrgRefLinfox(Org, row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim()))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-011 : Invalid Item Number ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " item_code " + row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-011 : Invalid Item Number ", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    // Error PO001-015 : Invalid Supplier
                    //if (!xCSMTDB.validateSupplierBySupplierCode(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim()))
                    String vendorId = "";
                    vendorId = xCSMTDB.validateSupplierBySupplierCode1(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim());
                    String suppName = "";
                    suppName = xCSMTDB.getSupplierNameBySupplierCode(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim());
                    if (vendorId.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-015 : Invalid Supplier";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " supplier_code " + row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-015 : Invalid Supplier", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    String supplierSiteCode = "";
                    supplierSiteCode = xCSSMTDB.getMinVendorSiteIdByVendorId(vendorId);

                    // Error PO001-016 :  Invalid UOM
                    if (!xCUMTDB.validateUOMCodeByUOMCode(row[xCLFPTDB.xCLFPT.UOMCODE].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-016 : Invalid UOM";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " uom_code " + row[xCLFPTDB.xCLFPT.UOMCODE].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-016 : Invalid UOM", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    // Error PO001-017 : Invalid CHARGE_ACCOUNT_SEGMENT1
                    //if (!xCVSMTDB.validateValueBySegment1("COMPANY RD CLOUD","Y", "11"))// ต้องแก้ Fix code อยู่
                    if (!xCVSMTDB.validateValueBySegment1("COMPANY RD CLOUD", "Y", "11"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-017 : Invalid CHARGE_ACCOUNT_SEGMENT1";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT1 " ;
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-017 : Invalid CHARGE_ACCOUNT_SEGMENT1", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    // Error PO001-018 : Invalid CHARGE_ACCOUNT_SEGMENT2
                    //if (!xCVSMTDB.validateValueBySegment2("STORE RD CLOUD", "Y", "00000"))// ต้องแก้ Fix code อยู่
                    if (!xCVSMTDB.validateValueBySegment2("STORE RD CLOUD", "Y", "00000"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-018 : Invalid CHARGE_ACCOUNT_SEGMENT2";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT2 ";
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-018 : Invalid CHARGE_ACCOUNT_SEGMENT2", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    // Error PO001-019 : Invalid CHARGE_ACCOUNT_SEGMENT3
                    //if (!xCVSMTDB.validateValueBySegment3("ACCOUNT RD CLOUD", "Y", "117101"))// ต้องแก้ Fix code อยู่
                    if (!xCVSMTDB.validateValueBySegment3("ACCOUNT RD CLOUD", "Y", "117101"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-019 : Invalid CHARGE_ACCOUNT_SEGMENT3";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT3 ";
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-019 : Invalid CHARGE_ACCOUNT_SEGMENT3", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    // Error PO001-020 : Invalid CHARGE_ACCOUNT_SEGMENT4
                    //if (!xCVSMTDB.validateValueBySegment4("PROJECT RD CLOUD", "Y", subInv_code))// ต้องแก้ Fix code อยู่
                    if (!xCVSMTDB.validateValueBySegment4("PROJECT RD CLOUD", "Y", "000000"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-020 : Invalid CHARGE_ACCOUNT_SEGMENT4";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT4 ";
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-020 : Invalid CHARGE_ACCOUNT_SEGMENT4", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    // Error PO001-021 : Invalid CHARGE_ACCOUNT_SEGMENT5
                    //if (!xCVSMTDB.validateValueBySegment5("FUTURE1 RD CLOUD", "Y", "00"))// ต้องแก้ Fix code อยู่
                    if (!xCVSMTDB.validateValueBySegment5("FUTURE1 RD CLOUD", "Y", "00"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-021 : Invalid CHARGE_ACCOUNT_SEGMENT5";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT5 ";
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-021 : Invalid CHARGE_ACCOUNT_SEGMENT5", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    // Error PO001-022 : Invalid CHARGE_ACCOUNT_SEGMENT6
                    //if (!xCVSMTDB.validateValueBySegment6("FUTURE2 RD CLOUD", "Y", "0000"))// ต้องแก้ Fix code อยู่
                    if (!xCVSMTDB.validateValueBySegment6("FUTURE2 RD CLOUD", "Y", "0000"))// ต้องแก้ Fix code อยู่
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-022 : Invalid CHARGE_ACCOUNT_SEGMENT6";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT6 ";
                        lVPr.Add(vPP);
                        cntErr++;
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-022 : Invalid CHARGE_ACCOUNT_SEGMENT6 ", requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    
                    blanketAgreement = getBlanketAgreement(row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim(), 
                        row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim(), row[xCLFPTDB.xCLFPT.QTY].ToString().Trim());
                    String[] aa = blanketAgreement.Split(',');
                    String price = "", agreementLineNumber="";
                    if (aa.Length > 0)
                    {
                        blanketAgreement = aa[0];
                        price = aa[1];
                        agreementLineNumber = aa[2];
                    }
                    if (blanketAgreement.IndexOf("false")>=0)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim();
                        vPP.Message = " PO001-"+ blanketAgreement.Replace("false","");
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCLFPTDB.xCLFPT.store_code].ToString().Trim() + " agreementLineNumber ";
                        lVPr.Add(vPP);
                        xCLFPTDB.updateValidateFlag(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim(), "E","", "kfc_po", Cm.initC.pathLogErr);
                        cntErr++;       // gen log
                        xCLFPTDB.updateErrorMessage(poNumber, lineNumber, "Error PO001-" + blanketAgreement.Replace("false", ""), requestId, "kfc_po", Cm.initC.pathLogErr);
                    }
                    else
                    {
                        addXcustPRHIA(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim(), currDate, rowG[xCLFPTDB.xCLFPT.file_name].ToString().Trim()
                            , row[xCLFPTDB.xCLFPT.request_id].ToString().Trim());//ทำ รอไว้ เพื่อ process ช้า
                        row.BeginEdit();
                        row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER] = blanketAgreement;
                        row[xCLFPTDB.xCLFPT.PRICE] = price;
                        row.EndEdit();
                        addXcustPRLIAFromxCLFPT(row, subInv_code, price, blanketAgreement, agreementLineNumber, supplierSiteCode, suppName);
                        addXcustPRDIAFromxCLFPT(row, subInv_code, price);
                        //xCLFPTDB.updateValidateFlag(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim(),"Y", blanketAgreement, "kfc_po");
                        xCLFPTDB.updateValidateFlag2(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim()
                            , blanketAgreement, agreementLineNumber, supplierSiteCode, suppName, subInv_code, price, "kfc_po", Cm.initC.pathLogErr);
                    }
                    if (cntErr > 0)   // gen log
                    {
                        cntFileErr++;
                    }
                    //dt.Rows[0][xc xCBMT.BU_ID].ToString();xCVSMTDB
                }
                vF.recordError = cntFileErr.ToString();   // gen log
                vF.totalError = cntErr.ToString();   // gen log
                lVfile.Add(vF);   // gen log
            }
            pB1.Visible = false;
            //Cm.logProcess("xcustpo001", lVPr, dateStart, lVfile);   // gen log
            addListView("Update Validate Flag  ", "Validate", lv1, form1);
            updateValidateFlagY(requestId);
            addListView("gen log file  ", "Validate", lv1, form1);
            xCLFPTDB.logProcessPO001("xcustpo001", dateStart, requestId);   // gen log
            addListView("move file to archive error  ", "Validate", lv1, form1);
            //moveFileToFolderArchiveError(requestId);  02 767 9333 446
        }
        private void moveFileToFolderArchiveError(String requestId)
        {
            String chk = "";
            DataTable dt = new DataTable();
            dt = xCLFPTDB.selectFilenameByRequestId(requestId);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    String filename = "", rowCnt="";
                    filename = row[xCLFPTDB.xCLFPT.file_name].ToString();
                    rowCnt = row["row_Cnt"].ToString();
                    String cnt = "";
                    cnt = xCLFPTDB.getCountNoErrorByFilename(requestId, filename);
                    if (cnt.Equals(rowCnt))
                    {
                        
                        Cm.moveFile(Cm.initC.PathProcess + filename, Cm.initC.PathArchive, filename);
                    }
                    else
                    {
                        Cm.moveFile(Cm.initC.PathProcess + filename, Cm.initC.PathError , filename);
                    }
                }
            }
        }
        private void updateValidateFlagY(String requestId)
        {
            DataTable dt = new DataTable();
            dt = xCLFPTDB.selectLinfoxByRequestId(requestId);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    String poNumber = "", lineNumber = "",chk="";
                    poNumber = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
                    lineNumber = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
                    if (poNumber.Equals("12819344"))
                    {
                        chk = "";
                    }
                    if (row[xCLFPTDB.xCLFPT.ERROR_MSG].ToString().Length == 0)
                    {
                        xCLFPTDB.updateValidateFlagY(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString(), row[xCLFPTDB.xCLFPT.request_id].ToString(), "kfc_po", Cm.initC.pathLogErr);
                    }
                }
                
            }
        }
        public String processLinfoxPOtoErpPR(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH_mm_ss");

            int cntErr = 0, cntFileErr = 0;   // gen log
            ReadText rd = new ReadText();
            String[] filePOProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;
            dateStart = date + " "+ time;       //gen log
            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process
            List<ValidateFileName> lVfile = new List<ValidateFileName>();   // gen log
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();   // gen log
            ValidatePrPo vPP = new ValidatePrPo();   // gen log
            addListView("อ่าน fileจาก" + Cm.initC.PathInitial, "", lv1, form1);
            foreach (string filename in filePO)
            {
                addListView("ย้าย file " + filename, "", lv1, form1);
                if (File.Exists(Cm.initC.PathProcess + filename.Replace(Cm.initC.PathInitial, "")))
                {
                    ValidateFileName vF = new ValidateFileName();   // gen log
                    vF.fileName = filename.Replace(Cm.initC.PathInitial, "");   // gen log
                    vF.recordTotal = "1";   // gen log

                    vPP = new ValidatePrPo();
                    vPP.Filename = filename.Replace(Cm.initC.PathInitial, "");
                    vPP.Message = "Error File Exists ";
                    vPP.Validate = "";
                    lVPr.Add(vPP);

                    cntFileErr++;
                    
                    lVfile.Add(vF);   // gen log
                    Cm.logProcess("xcustpo001", lVPr, dateStart, lVfile);   // gen log
                    //String filename1 = "";
                    //filename1 = aa.Replace(Cm.initC.PathInitial, "");
                    //if (File.Exists(Cm.initC.PathError + aa.Replace(Cm.initC.PathInitial, "")))
                    //{
                    //    String[] filename11 = filename1.Split('.');
                    //    if (filename11.Length >= 2)
                    //    {
                    //        filename1 = filename11[0] + "_new"+ date+"_"+ time + "." + filename11[1];
                    //    }
                    //    else
                    //    {
                    //        filename1 += "_new";
                    //    }
                    //}
                    Cm.moveFile(filename, Cm.initC.PathError, filename);
                }
                else
                {
                    Cm.moveFile(filename, Cm.initC.PathProcess + filename.Replace(Cm.initC.PathInitial, ""));
                }
            }
            addListView("Clear temp table", "", lv1, form1);
            //xCLFPTDB.DeleteLinfoxTemp();//  clear temp table
            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_LINFOX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert XCUST_LINFOX_PR_TBL
            filePOProcess = Cm.getFileinFolder(Cm.initC.PathProcess);
            addListView("อ่าน file จาก " + Cm.initC.PathProcess, "", lv1, form1);
            String requestId = "";
            requestId = xCLFPTDB.getRequestID();
            foreach (string aa in filePOProcess)
            {
                List<String> linfox = rd.ReadTextFile(aa);
                addListView("insert temp table " + aa, "", lv1, form1);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL
                pB1.Visible = true;
                xCLFPTDB.insertBluk(linfox, aa, "kfc_po", pB1, requestId, Cm, Cm.initC.pathLogErr);
                pB1.Visible = false;
            }
            return requestId;
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

            xCPRHIA.ATTRIBUTE_DATE1 = "";
            xCPRHIA.ATTRIBUTE_TIMESTAMP1 = "";
            xCPRHIA.BATCH_ID = xcprhia.BATCH_ID;
            xCPRHIA.DESCRIPTIONS = xcprhia.DESCRIPTIONS.Trim();
            xCPRHIA.REQUESTER_EMAIL_ADDR = "";
            xCPRHIA.INTERFACE_SOURCE_CODE = "";
            xCPRHIA.ATTRIBUTE_CATEGORY = xcprhia.ATTRIBUTE_CATEGORY;
            xCPRHIA.REQ_HEADER_INTERFACE_ID = xcprhia.REQ_HEADER_INTERFACE_ID.Trim();            
            xCPRHIA.PROCESS_FLAG = "N";
            xCPRHIA.APPROVER_EMAIL_ADDR = "";
            xCPRHIA.ATTRIBUTE2 = xcprhia.ATTRIBUTE2;
            xCPRHIA.REQUITITION_NUMBER = xcprhia.REQUITITION_NUMBER;
            xCPRHIA.IMPORT_SOURCE = xcprhia.IMPORT_SOURCE;
            xCPRHIA.ATTRIBUTE2 = xcprhia.ATTRIBUTE2;
            xCPRHIA.REQ_BU_NAME = xcprhia.REQ_BU_NAME;
            xCPRHIA.STATUS_CODE = xcprhia.STATUS_CODE;
            xCPRHIA.request_id = xcprhia.request_id;
            chk = xCPRHIADB.insert(xCPRHIA, Cm.initC.pathLogErr);
            return chk;
        }
        //private void insertXcustPorReqHeaderIntAll1(DataRow row, String date, String time)
        //{//row[dc].ToString().Trim().
        //    XcustPorReqHeaderIntAll xCPRHIA = new XcustPorReqHeaderIntAll();
        //    xCPRHIA.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
        //    xCPRHIA.INTERFACE_SOURCE_CODE = "";// ค่าจาก parameter import_source
        //    xCPRHIA.REQ_BU_NAME = "";// ค่าจาก parameter bu name     PARAMETER.PR_STATAUS        Requisition Number ค่าว่างไปก่อน
        //    xCPRHIA.BATCH_ID = "";
        //    xCPRHIA.ATTRIBUTE1 = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();

        //    xCPRHIA.ATTRIBUTE_DATE1 = "";
        //    //xCPRHIA.ATTRIBUTE_TIMESTAMP1 = date+" "+ time;
        //    xCPRHIA.ATTRIBUTE_TIMESTAMP1 = "";

        //    xCPRHIA.DESCRIPTIONS = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
        //    xCPRHIA.REQUESTER_EMAIL_ADDR = "";
            
        //    xCPRHIA.ATTRIBUTE_CATEGORY = "";
            
        //    xCPRHIA.PROCESS_FLAG = "N";
        //    xCPRHIA.APPROVER_EMAIL_ADDR = "";
        //    xCPRHIA.STATUS_CODE = "";
        //    xCPRHIA.REQ_BU_NAME = "";
        //    xCPRHIA.REQUITITION_NUMBER = "";
        //    xCPRHIA.request_id = row[xCLFPTDB.xCLFPT.request_id].ToString();
        //    xCPRHIADB.insert(xCPRHIA);
        //}
        //private void insertXcustPorReqLineIntAll1(DataRow row, String date, String time)
        //{
        //    XcustPorReqLineIntAll xCPRLIA = new XcustPorReqLineIntAll();
        //    xCPRLIA.ATTRIBUTE1 = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
        //    xCPRLIA.ATTRIBUTE_DATE1 = date;
        //    xCPRLIA.ATTRIBUTE_NUMBER1 = "";
        //    xCPRLIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
        //    xCPRLIA.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString().Trim();
        //    xCPRLIA.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
        //    xCPRLIA.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.store_code].ToString().Trim();
        //    xCPRLIA.DELIVER_TO_ORGANIZATION_CODE = Cm.initC.ORGANIZATION_code;
        //    xCPRLIA.Goods = "";
        //    xCPRLIA.DESTINATION_TYPE_CODE = "INVENTORY";
        //    xCPRLIA.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim();
        //    xCPRLIA.LINFOX_PR = "";
        //    xCPRLIA.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString().Trim();
        //    xCPRLIA.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
        //    xCPRLIA.REQ_HEADER_INTERFACE_ID = xCPRHIADB.selectReqHeaderNumber(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim());
        //    xCPRLIA.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString().Trim();
        //    xCPRLIA.PROCESS_FLAG = "N";
        //    xCPRLIA.PRC_BU_NAME = "";
        //    xCPRLIA.PR_APPROVER = "";
        //    xCPRLIA.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString().Trim();
        //    xCPRLIA.REQUESTER_EMAIL_ADDR = Cm.initC.Requester;
        //    xCPRLIA.Requisitioning_BU = Cm.initC.BU_NAME;
        //    xCPRLIA.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString().Trim();
        //    xCPRLIA.SUGGESTED_VENDOR_NAME = row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim();
        //    xCPRLIA.SUGGESTED_VENDOR_SITE = row[xCLFPTDB.xCLFPT.SUPPLIER_SITE_CODE].ToString().Trim();
        //    xCPRLIA.LINE_TYPE = Cm.initC.LINE_TYPE;
        //    xCPRLIA.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
        //    //xCPRLIA.NEED_BY_DATE = "";
        //    xCPRLIA.ATTRIBUTE_NUMBER10 = row[xCLFPTDB.xCLFPT.PRICE].ToString().Trim();
        //    xCPRLIA.request_id = row[xCLFPTDB.xCLFPT.request_id].ToString();
            
        //    //xCPRLIA.REQ_HEADER_INTERFACE_ID

        //    xCPRLIADB.insert(xCPRLIA);
        //}
        //private void insertXcustPorReqDistIntAll111(DataRow row, String date, String time)
        //{
        //    XcustPorReqDistIntAll xCPRDIA = new XcustPorReqDistIntAll();
        //    xCPRDIA.ATTRIBUTE1 = "";
        //    xCPRDIA.ATTRIBUTE_CATEGORY = "";
        //    xCPRDIA.ATTRIBUTE_DATE1 = date;
        //    xCPRDIA.ATTRIBUTE_NUMBER1 = "";
        //    xCPRDIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
        //    xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 = "";
        //    xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 = "";
        //    xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 = "";
        //    xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 = "";
        //    xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 = "";
        //    xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 = "";
        //    xCPRDIA.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
        //    xCPRDIA.REQ_HEADER_INTERFACE_ID = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
        //    xCPRDIA.PROCESS_FLAG = "N";
        //    xCPRDIA.REQ_DIST_INTERFACE_ID = "";
        //    xCPRDIA.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString().Trim();
        //    xCPRDIADB.insert(xCPRDIA);
        //}
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
            //sql = "Select xcbaht.*, xcblt."+xCBALTDB.xCBALT.PRICE+ ", "+ xCBALTDB.xCBALT.PRICE_LIMIT + ", "+ xCBALTDB.xCBALT.LINE_NUMBER +" " +
            //    " From " + xCBAHTDB.xCBAHT.table +
            //    " xcbaht " +
            //    "left join "+xCBALTDB.xCBALT.table+ " xcblt ON xcblt." + xCBALTDB.xCBALT.PO_HEADER_ID+ "=xcbaht."+xCBAHTDB.xCBAHT.PO_HEADER_ID+" "+
            //    "Where xcbaht." + xCBAHTDB.xCBAHT.SUPPLIER_CODE + "  = '" + supp_code + "' " +
            //    "and xcblt." + xCBALTDB.xCBALT.ITEM_CODE + "='" + item_code + "' and xcbaht." +
            //xCBAHTDB.xCBAHT.STATUS + "='OPEN'";
            sql = "select xcbaht.* , xcblt." + xCBALTDB.xCBALT.PRICE + ", " + xCBALTDB.xCBALT.PRICE_LIMIT + ", " + xCBALTDB.xCBALT.LINE_NUMBER +" "+
                "From XCUST_BLANKET_AGREEMENT_HEADER_TBL xcbaht "+
                ",XCUST_BLANKET_AGREEMENT_LINES_TBL xcblt "+
                "where xcblt.PO_HEADER_ID = xcbaht.PO_HEADER_ID "+
                "and xcbaht.SUPPLIER_CODE = '"+supp_code+"' "+
                "and xcblt.ITEM_CODE = '"+item_code+"' "+
                "and xcbaht.status = 'OPEN' "+
                "and xcblt.line_status = 'OPEN'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if ((dt.Rows[0][xCBALTDB.xCBALT.PRICE_LIMIT] == null) || (dt.Rows[0][xCBALTDB.xCBALT.PRICE_LIMIT].ToString().Equals("")))
                    {
                        chk = dt.Rows[0][xCBAHTDB.xCBAHT.AGREEMENT_NUMBER].ToString().Trim() + "," + dt.Rows[0][xCBALTDB.xCBALT.PRICE].ToString().Trim() + ","+ dt.Rows[0][xCBALTDB.xCBALT.LINE_NUMBER].ToString().Trim();
                    }
                    else
                    {
                        if (dt.Rows[0][xCBAHTDB.xCBAHT.MIN_RELEASE_AMT] != null)
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
                                            chk = dt.Rows[0][xCBAHTDB.xCBAHT.AGREEMENT_NUMBER].ToString().Trim() + "," + price1;
                                        }
                                        else
                                        {
                                            chk = "false,026,";
                                        }
                                    }
                                    else
                                    {
                                        chk = "false,025,";
                                    }
                                }
                                else
                                {
                                    chk = "false,,";
                                }
                            }
                        }
                    }
                }
                else
                {
                    chk = "false,024,";
                }
            }
            else
            {
                chk = "false,023,";
            }
            return chk;
        }
        public void ImportMasterXCUST_VALUE_SET_MST_TBL()
        {
            String sql = "", col = "", sql1 = "", chk="";
            
            DataTable dt = new DataTable();
            String[] filePOProcess;
            int i = 0;
            //conn.ExecuteNonQuery(sql, "orc_bit");            

            //อ่าน text file cvs
            filePOProcess = Cm.getFileinFolder(Cm.initC.PathMaster);
            ReadText rd = new ReadText();
            foreach (string filename in filePOProcess)//อ่าน file ใน folder
            {
                if (filename.Replace(Cm.initC.PathMaster,"").Equals("XCUST_MAS_VALUE_SET_REP_XCUST_MAS_VALUE_SET_REP (1).csv"))
                {
                    List<String> masterFile = rd.ReadTextFile(filename);
                    i = 0;
                    foreach(String data in masterFile)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        chk = xCVSMTDB.insertFromText(data, "kfc_po");
                    }
                }
            }
        }
        public void processCallWebService(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String requestId)
        {
            addListView("callWebService " , "web service", lv1, form1);
            String uri = "", dump = "";
            //HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            const Int32 BufferSize = 128;
            String[] filePO;
            String filename = "";
            
            addListView("callWebService อ่าน file ZIP", "web service", lv1, form1);
            filePO = Cm.getFileinFolder(Cm.initC.PathFileCSV);
            //String text = System.IO.File.ReadAllText(filePO[0]);
            filename = filePO[0].Replace(Cm.initC.PathFileCSV, "");
            //byte[] byteArraytext = Encoding.UTF8.GetBytes(text);
            //byte[] toEncodeAsBytestext = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            byte[] toEncodeAsBytestext = System.IO.File.ReadAllBytes(filePO[0]);
            String Arraytext = System.Convert.ToBase64String(toEncodeAsBytestext);
            
            uri = @" <soapenv:Envelope xmlns:soapenv ='http://schemas.xmlsoap.org/soap/envelope/' xmlns:typ='http://xmlns.oracle.com/oracle/apps/fnd/applcore/webservices/types/' xmlns:web='http://xmlns.oracle.com/oracle/apps/fnd/applcore/webservices/'> " +
                    "<soapenv:Header/> " +
                        "<soapenv:Body> " +
                         "<typ:uploadFiletoUCM> " +
                   "<typ:document> " +
                       "<!--Optional:--> " +
                        "<web:fileName>"+ filename + "</web:fileName> " +
                             "<!--Optional:--> " +
                              "<web:contentType>application/zip</web:contentType> " +
                                     "<!--Optional:--> " +
                                        "<web:content>" + Arraytext +
                                        "</web:content> " +
             "<!--Optional:--> " +
              "<web:documentAccount>prc$/requisition$/import$</web:documentAccount> " +
                    "<!--Optional:--> " +
                     "<web:documentTitle> amo_test_load </web:documentTitle> " +
                       "</typ:document> " +
                     "</typ:uploadFiletoUCM> " +
                   "</soapenv:Body> " +
                 "</soapenv:Envelope>";

            //byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
            byte[] byteArray = Encoding.UTF8.GetBytes(uri);
            addListView("callWebService prepare web service", "web service", lv1, form1);
            // Construct the base 64 encoded string used as credentials for the service call
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("icetech@iceconsulting.co.th" + ":" + "icetech@2017");
            string credentials = System.Convert.ToBase64String(toEncodeAsBytes);

            // Create HttpWebRequest connection to the service
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://eglj-test.fa.us2.oraclecloud.com:443/fndAppCoreServices/FndManageImportExportFilesService?WSDL");

            // Configure the request content type to be xml, HTTP method to be POST, and set the content length
            request1.Method = "POST";
            request1.ContentType = "text/xml;charset=UTF-8";
            request1.ContentLength = byteArray.Length;

            // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
            request1.Headers.Add("Authorization", "Basic " + credentials);

            // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
            request1.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/incentiveCompensation/cn/creditSetup/creditRule/creditRuleService/findRule");

            // Write the xml payload to the request
            Stream dataStream = request1.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            addListView("callWebService กำลังรอ รับข้อมูล จาก web service", "web service", lv1, form1);
            // Get the response and process it; In this example, we simply print out the response XDocument doc;
            string actNumber = "";
            XDocument doc;
            using (WebResponse response = request1.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    doc = XDocument.Load(stream);
                    addListView("callWebService กำลังถอดเลขที่เอกสาร", "web service", lv1, form1);
                    foreach (XNode node in doc.DescendantNodes())
                    {
                        if (node is XElement)
                        {
                            XElement element = (XElement)node;
                            if (element.Name.LocalName.Equals("result"))
                            {
                                actNumber = element.ToString().Replace("http://xmlns.oracle.com/oracle/apps/fnd/applcore/webservices/types/", "");
                                actNumber = actNumber.Replace("result xmlns=", "").Replace("</result>", "").Replace(@"""", "").Replace("<>", "");
                                addListView("callWebService เลขที่เอกสาร "+actNumber, "web service", lv1, form1);
                            }
                        }
                    }
                }
            }
            xCPRHIADB.updateDocumentId(actNumber, requestId, Cm.initC.pathLogErr);
            xCPRLIADB.updateDocumentId(actNumber, requestId, Cm.initC.pathLogErr);
            xCPRDIADB.updateDocumentId(actNumber, requestId, Cm.initC.pathLogErr);
            xCLPTDB.updatePrcessFlag(requestId, "kfc_po", Cm.initC.pathLogErr);
            Console.WriteLine(doc);

            moveFileToFolderArchiveError(requestId);
        }
    }
}
