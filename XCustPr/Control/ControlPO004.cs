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

        //public XcustPorReqHeaderIntAllDB xCPRHIADB;
        //public XcustPorReqLineIntAllDB xCPRLIADB;
        //public XcustPorReqDistIntAllDB xCPRDIADB;

        public XcustBuMstTblDB xCBMTDB;
        public XcustDeriverLocatorMstTblDB xCDLMTDB;
        public XcustDeriverOrganizationMstTblDB xCDOMTDB;
        public XcustSubInventoryMstTblDB xCSIMTDB;
        public XcustItemMstTblDB xCIMTDB;
        public XcustCurrencyMstTblDB xCMTDB;
        public XcustSupplierMstTblDB xCSMTDB;
        public XcustUomMstTblDB xCUMTDB;
        public XcustValueSetMstTblDB xCVSMTDB;
        public XcustSupplierSiteMstTblDB xCSSMTDB;

        public XcustRcvHeadersIntAllDB xCRHIADB;        //ใช้ table เดียวกับ PO003
        public XcustRcvTransactionsIntAllDB xCRTIADB;        //ใช้ table เดียวกับ PO003
        public XcustInvTransactionLostsIntTblDB xITLITDB;        //ใช้ table เดียวกับ PO003

        public XcustMmxPoRcpIntTblDB xCMPoRITDB;       //table temp

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
        private List<XcustSupplierSiteMstTbl> listXcustSSMT;

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

            
            //xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
            //xCPRLIADB = new XcustPorReqLineIntAllDB(conn, Cm.initC);
            //xCPRDIADB = new XcustPorReqDistIntAllDB(conn);
            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, Cm.initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, Cm.initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn, Cm.initC);
            xCIMTDB = new XcustItemMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCUMTDB = new XcustUomMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);

            xCMPoRITDB = new XcustMmxPoRcpIntTblDB(conn, Cm.initC);
            xCRHIADB = new XcustRcvHeadersIntAllDB(conn);
            xCRTIADB = new XcustRcvTransactionsIntAllDB(conn);
            xITLITDB = new XcustInvTransactionLostsIntTblDB(conn);
            xCSSMTDB = new XcustSupplierSiteMstTblDB(conn, Cm.initC);
            //xCBALTDB = new XcustBlanketAgreementLinesTblDB(conn, Cm.initC);

            Cm.createFolderPO004();
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
                xCMPoRITDB.insertBluk(rcv, aa, "kfc_po", pB1);
                pB1.Visible = false;
            }
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

            int row1 = 0;
            int cntErr = 0, cntFileErr = 0;   // gen log

            dtGroupBy = xCMPoRITDB.selectMmxGroupByFilename();//   ดึง filename
            foreach (DataRow rowG in dtGroupBy.Rows)
            {
                addListView("ดึงข้อมูล  " + rowG[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim(), "Validate", lv1, form1);
                dt = xCMPoRITDB.selectMmxByFilename(rowG[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim());    // ข้อมูลใน file
                row1 = 0;
                cntErr = 0;     //gen log
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;

                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = rowG[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim();   // gen log
                vF.recordTotal = dt.Rows.Count.ToString();   // gen log

                foreach (DataRow row in dt.Rows)
                {
                    row1++;
                    pB1.Value = row1;
                    //Error PO003-002 : Date Format not correct 
                    chk = Cm.validateDate(row[xCMPoRITDB.xCMPoRIT.date_of_record].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO004-002 ";
                        vPP.Validate = "row " + row1 + " date_of_record=" + row[xCMPoRITDB.xCMPoRIT.date_of_record].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO004-004 : Invalid  Supplier Code
                    if (Cm.validateSupplierBySupplierCode(row[xCMPoRITDB.xCMPoRIT.supplier_code].ToString().Trim(), listXcSMT))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO004-015 ";
                        vPP.Validate = "row " + row1 + "  supplier_code " + row[xCMPoRITDB.xCMPoRIT.supplier_code].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                    }
                    //Error PO004-003 : Invalid  Store No
                    //subInv_code = xCSIMTDB.validateSubInventoryCode1(initC.ORGANIZATION_code.Trim(), row[xCLFPTDB.xCLFPT.store_code].ToString().Trim());
                    subInv_code = validateSubInventoryCode(Cm.initC.ORGANIZATION_code.Trim(), row[xCMPoRITDB.xCMPoRIT.store_code].ToString().Trim());
                    if (subInv_code.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-010 ";
                        vPP.Validate = "row " + row1 + " store_code =" + row[xCMPoRITDB.xCMPoRIT.store_code].ToString().Trim() + " ORGANIZATION_code " + Cm.initC.ORGANIZATION_code.Trim();
                        lVPr.Add(vPP);
                        cntErr++;
                    }
                    //ดูว่า เป็น direct หรือ non direct
                    if (getDirectSupplierBySupplierCode(row[xCMPoRITDB.xCMPoRIT.supplier_code].ToString().Trim()))
                    {//direct
                        DataTable dtR = xCMPoRITDB.gePOReceipt(row[xCMPoRITDB.xCMPoRIT.item_code].ToString().Trim(), row[xCMPoRITDB.xCMPoRIT.supplier_code].ToString().Trim());
                        if (dtR.Rows.Count > 0)//พบ
                        {
                            Double qty = 0, qtyAmt=0, qtyMMx=0;
                            qtyMMx = Double.Parse(row[xCMPoRITDB.xCMPoRIT.erp_qty].ToString().Trim());
                            //นับก่อน ว่าพอตัดไหม
                            foreach (DataRow rowR in dtR.Rows)       //นัดก่อน ว่าพอตัดไหม
                            {
                                if(Double.TryParse(rowR["quantity"].ToString(),out qty))
                                {
                                    qtyAmt += qty;
                                }
                                else// qty ไม่ถูกต้อง
                                {
                                }
                            }
                            //เริ่มตัด
                            if(qtyMMx <= qtyAmt)//-	กรณียอด QTY มีเพียงพอสำหรับการ Receipt
                            {//พอ ตัด
                                Double qtyCut = 0;
                                foreach (DataRow rowR in dtR.Rows)
                                {
                                    if (Double.TryParse(rowR["quantity"].ToString(), out qty))
                                    {
                                        if(qty >= qtyMMx)
                                        {
                                            //  insert
                                            String vendor_code = "", supplier_site_code="";
                                            vendor_code = getVendorBySupplierCode(row[xCMPoRITDB.xCMPoRIT.supplier_code].ToString().Trim());
                                            supplier_site_code = getSupplierSiteCodeBySupplierCode(row[xCMPoRITDB.xCMPoRIT.supplier_code].ToString().Trim());
                                            addXcustListHeader(row[xCMPoRITDB.xCMPoRIT.INVOICE_NO].ToString().Trim(), vendor_code
                                                , supplier_site_code, row[xCMPoRITDB.xCMPoRIT.INVOICE_NO].ToString().Trim());//ทำ รอไว้ เพื่อ process ช้า
                                            addListXcustRTIA(row);

                                            xCMPoRITDB.updateValidate(row[xCMPoRITDB.xCMPoRIT.store_code].ToString().Trim(), row[xCMPoRITDB.xCMPoRIT.item_code].ToString().Trim()
                                                , row[xCMPoRITDB.xCMPoRIT.INVOICE_NO].ToString().Trim(), row[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim(), "Y", "");
                                        }
                                    }
                                    qtyMMx -= qty;
                                    if (qtyMMx <= 0) break;
                                }
                            }
                            else//-	กรณียอด QTY ไม่เพียงพอสำหรับการ Receipt
                            {
                                xCMPoRITDB.updateValidate(row[xCMPoRITDB.xCMPoRIT.store_code].ToString().Trim(), row[xCMPoRITDB.xCMPoRIT.item_code].ToString().Trim()
                                , row[xCMPoRITDB.xCMPoRIT.INVOICE_NO].ToString().Trim(), row[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim(), "E", "Error PO004-006 : PO QTY is less than Receipt QTY");
                            }
                        }
                        else//ไม่พบ      -	กรณี Map ไม่เจอ PO ,PO Line
                        {
                            xCMPoRITDB.updateValidate(row[xCMPoRITDB.xCMPoRIT.store_code].ToString().Trim(), row[xCMPoRITDB.xCMPoRIT.item_code].ToString().Trim()
                                , row[xCMPoRITDB.xCMPoRIT.INVOICE_NO].ToString().Trim(), row[xCMPoRITDB.xCMPoRIT.file_name].ToString().Trim(),"E", "Error PO004-005 : Not found PO Number/PO Line");
                        }
                    }
                    else
                    {//เป็น inven

                    }
                }
            }
        }
        private void addXcustListHeader(String ref1, String supplier_code, String SUPPLIER_SITE_CODE, String Supplier_Site_Code)
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
                xcrhia1.HEADER_INTERFACE_NUMBER = ref1;
                xcrhia1.RECEIPT_SOURCE_CODE = Cm.initC.PO004RECEIPT_SOURCE;
                xcrhia1.ASN_TYPE = "STD";
                xcrhia1.TRANSACTION_TYPE = "NEW";//ถาม
                xcrhia1.RECEIPT_NUM = "";       // running
                xcrhia1.VENDOR_NUM = supplier_code;//ถาม
                xcrhia1.VENDOR_SITE_CODE = supplier_code;//ถาม
                xcrhia1.VENDOR_SITE_CODE = SUPPLIER_SITE_CODE;//ถาม
                xcrhia1.SHIPTO_ORGANIZATION_CODE = Cm.initC.ORGANIZATION_code;//ถาม
                xcrhia1.TRANSACTION_DATE = "";//ถาม
                xcrhia1.BUSINESS_UNIT = "";//ถาม
                xcrhia1.ATTRIBUTE_CATEGORY = "RCP_MMX_INFO";//ถาม
                xcrhia1.PROCESS_FLAG = "N";
                listXcustRHIA.Add(xcrhia1);
            }
        }
        private void addListXcustRTIA(DataRow row)   //xCRTIADB
        {
            XcustRcvTransactionsIntAll item = new XcustRcvTransactionsIntAll();
            item.HEADER_INTERFACE_NUMBER = row[xCRTIADB.xCRTIA.he].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
            item.DESTINATION_TYPE_CODE = "";
            item.PRC_BU_NAME = "";
            item.DELIVER_TO_ORGANIZATION_CODE = Cm.initC.ORGANIZATION_code;
            item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            item.LINE_TYPE = "";

            item.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString();
            item.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
            item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";
            item.UOM_CODE = row[xCLFPTDB.xCLFPT.UOMCODE].ToString();

            listXcusTRTIA.Add(item);
        }
        private Boolean getDirectSupplierBySupplierCode(String supplier_code)
        {
            Boolean chk = false;
            foreach (XcustSupplierMstTbl item in listXcSMT)
            {
                if (item.SUPPLIER_NUMBER.Equals(supplier_code.Trim()))
                {
                    if (item.ATTRIBUTE1.Equals("Y"))
                    {
                        chk = true;
                        break;
                    }
                }
            }
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
        private String getSupplierSiteCodeBySupplierCode(String supplier_code)
        {
            String chk = "";
            foreach (XcustSupplierSiteMstTbl item in listXcustSSMT)
            {
                if (item.VENDOR_ID.Equals(supplier_code.Trim()))
                {
                    chk = item.VENDOR_ID;
                    break;
                }
            }
            return chk;
        }
        private String getVendorBySupplierCode(String supplier_code)
        {
            String chk = "";
            foreach (XcustSupplierMstTbl item in listXcSMT)
            {
                if (item.SUPPLIER_NUMBER.Equals(supplier_code.Trim()))
                {
                    chk = item.VENDOR_ID;
                    break;
                }
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
        /*
         * Method นี้ ไม่แน่ใจว่า จะแยกหรือ รวม
         */
        private void getListXcSSMT()
        {
            listXcustSSMT.Clear();
            DataTable dt = xCSSMTDB.selectAll();
            foreach (DataRow row in dt.Rows)
            {
                XcustSupplierSiteMstTbl item = new XcustSupplierSiteMstTbl();
                item = xCSSMTDB.setData(row);
                listXcustSSMT.Add(item);
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
