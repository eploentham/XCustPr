﻿using System;
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
        public XcustSupplierSiteMstTblDB xCSSMTDB;

        private List<XcustPorReqHeaderIntAll> listXcustPRHIA;
        private List<XcustPorReqLineIntAll> listXcustPRLIA;
        private List<XcustPorReqDistIntAll> listXcustPRDIA;

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;
        private String dateStart = "";      //gen log

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
            xCBAHTDB = new XcustBlanketAgreementHeaderTblDB(conn, Cm.initC);
            xCBALTDB = new XcustBlanketAgreementLinesTblDB(conn, Cm.initC);
            xCSSMTDB = new XcustSupplierSiteMstTblDB(conn, Cm.initC);

            Cm.createFolderPO005();
            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard

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
         * a.	ระบบ MMX จะ  SFTP file จากระบบงาน MMX และนำ File มาวางไว้ที่ Server ตาม Path Parameter Path Initial
         * b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process 
         */
        public String processMMXPOtoErpPR(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
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
            addListView("อ่าน fileจาก" + Cm.initC.PO005PathInitial, "", lv1, form1);
            foreach (string aa in filePO)
            {
                addListView("ย้าย file " + aa, "", lv1, form1);
                Cm.moveFile(aa, Cm.initC.PO005PathProcess + aa.Replace(Cm.initC.PO005PathInitial, ""));
            }
            //addListView("Clear temp table", "", lv1, form1);
            String requestId = "";
            requestId = xCMPITDB.getRequestID();
            //xCMPITDB.DeleteMmxTemp(Cm.initC.PO005PathLog);//  clear temp table
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
                xCMPITDB.insertBluk(mmx, aa, "kfc_po", pB1, requestId, Cm.initC.PO005PathLog);
                pB1.Visible = false;
            }
            return requestId;
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
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String requestId)
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
            //Org = xCDOMTDB.selectActive1();
            Org = xCDOMTDB.selectActiveByCode(Cm.initC.ORGANIZATION_code.Trim());
            //if (!Org.Equals(Cm.initC.ORGANIZATION_code.Trim()))
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
            dtGroupBy = xCMPITDB.selectMmxGroupByFilename(requestId);//   ดึง filename
            foreach (DataRow rowG in dtGroupBy.Rows)
            {
                
                dt = xCMPITDB.selectMmxByFilename(rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim(), requestId);    // ข้อมูลใน file
                addListView("ดึงข้อมูล  " + rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim()+dt.Rows.Count, "Validate", lv1, form1);
                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();   // gen log
                vF.recordTotal = dt.Rows.Count.ToString();   // gen log

                row1 = 0;
                cntErr = 0;     //gen log
                pB1.Minimum = 0;
                pB1.Maximum = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {
                    String poNumber = "", item_code = "", store_code="";
                    poNumber = row[xCMPITDB.xCMPIT.po_number].ToString().Trim();
                    item_code = row[xCMPITDB.xCMPIT.item_code].ToString().Trim();
                    store_code = row[xCMPITDB.xCMPIT.store_code].ToString().Trim();

                    row1++;
                    pB1.Value = row1;
                    //Error PO001-006 : Invalid data type
                    chk = Cm.validateQTY(row[xCMPITDB.xCMPIT.order_qty].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-006 ";
                        vPP.Validate = "row " + row1 + " order_qty=" + row[xCMPITDB.xCMPIT.order_qty].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code,store_code, "Error PO005-006 : Invalid data type", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    chk = Cm.validateQTY(row[xCMPITDB.xCMPIT.confirm_qty].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-006 ";
                        vPP.Validate = "row " + row1 + " confirm_qty=" + row[xCMPITDB.xCMPIT.confirm_qty].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-006 : Invalid data type", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-002 : Date Format not correct 
                    chk = Cm.validateDate(row[xCMPITDB.xCMPIT.conf_delivery_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-002 ";
                        vPP.Validate = "row " + row1 + " conf_delivery_date=" + row[xCMPITDB.xCMPIT.conf_delivery_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-002 : Date Format not correct conf_delivery_date", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    chk = Cm.validateDate(row[xCMPITDB.xCMPIT.order_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-002 ";
                        vPP.Validate = "row " + row1 + " delivery_date=" + row[xCMPITDB.xCMPIT.order_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-002 : Date Format not correct order_date", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //chk = Cm.validateDate(row[xCMPITDB.xCMPIT.conf_delivery_date].ToString());
                    //if (!chk)
                    //{
                    //    vPP = new ValidatePrPo();
                    //    vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                    //    vPP.Message = "Error PO001-002 ";
                    //    vPP.Validate = "row " + row1 + " order_date=" + row[xCMPITDB.xCMPIT.conf_delivery_date].ToString();
                    //    lVPr.Add(vPP);
                    //    cntErr++;       // gen log
                    //    xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-002 : Date Format not correct", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    //}
                    chk = Cm.validateDate(row[xCMPITDB.xCMPIT.request_date].ToString());
                    if (!chk)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-002 ";
                        vPP.Validate = "row " + row1 + " request_date=" + row[xCMPITDB.xCMPIT.request_date].ToString();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-002 : Date Format not correct request_date", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-010 : Invalid Subinventory Code
                    //subInv_code = Cm.validateSubInventoryCode(Cm.initC.ORGANIZATION_code.Trim(), row[xCMPITDB.xCMPIT.store_code].ToString().Trim(), listXcSIMT);
                    subInv_code = xCSIMTDB.validateSubInventoryCode1(Org, row[xCMPITDB.xCMPIT.store_code].ToString().Trim());
                    if (subInv_code.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-010 ";
                        vPP.Validate = "row " + row1 + " store_code =" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " ORGANIZATION_code " + Cm.initC.ORGANIZATION_code.Trim();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0010 : Invalid Subinventory Code", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-011 : Invalid Item Number
                    //if (Cm.validateItemCodeByOrgRef("300000000949654", row[xCMPITDB.xCMPIT.item_code].ToString().Trim(),listXcIMT))// ต้องแก้ Fix code อยู่
                    if (!xCIMTDB.validateItemCodeByOrgRefLinfox(Org, row[xCMPITDB.xCMPIT.item_code].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-011 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " item_code " + row[xCMPITDB.xCMPIT.item_code].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0011 : Invalid Item Number", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-015 : Invalid Supplier
                    String vendorId = "";
                    vendorId = xCSMTDB.validateSupplierBySupplierCode1(row[xCMPITDB.xCMPIT.supplier_code].ToString().Trim());
                    String suppName = "";
                    suppName = xCSMTDB.getSupplierNameBySupplierCode(row[xCMPITDB.xCMPIT.supplier_code].ToString().Trim());

                    String supplierSiteCode = "";
                    supplierSiteCode = xCSSMTDB.getMinVendorSiteIdByVendorId(vendorId);

                    //if (Cm.validateSupplierBySupplierCode(row[xCMPITDB.xCMPIT.supplier_code].ToString().Trim(), listXcSMT))
                    if (vendorId.Equals(""))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-015 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " supplier_code " + row[xCMPITDB.xCMPIT.supplier_code].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0011 : Invalid Supplier", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-016 : Invalid UOM
                    if (!xCUMTDB.validateUOMCodeByUOMCode(row[xCMPITDB.xCMPIT.uom_code].ToString().Trim()))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO001-016 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " uom_code " + row[xCMPITDB.xCMPIT.uom_code].ToString().Trim();
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0016 : Invalid UOM", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-017 : Invalid CHARGE_ACCOUNT_SEGMENT1
                    if (!xCVSMTDB.validateValueBySegment1("COMPANY RD CLOUD", "Y", "11"))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-017 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT1 ";
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0017 : Invalid CHARGE_ACCOUNT_SEGMENT1", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-018 : Invalid CHARGE_ACCOUNT_SEGMENT2
                    if (!xCVSMTDB.validateValueBySegment2("STORE RD CLOUD", "Y", "00000"))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-018 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT2 ";
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0018 : Invalid CHARGE_ACCOUNT_SEGMENT2", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-018 : Invalid CHARGE_ACCOUNT_SEGMENT3
                    if (!xCVSMTDB.validateValueBySegment3("ACCOUNT RD CLOUD", "Y", "117101"))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-019 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT3 ";
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0018 : Invalid CHARGE_ACCOUNT_SEGMENT3", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-018 : Invalid CHARGE_ACCOUNT_SEGMENT4
                    if (!xCVSMTDB.validateValueBySegment4("PROJECT RD CLOUD", "Y", "000000"))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-020 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT4 ";
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0020 : Invalid CHARGE_ACCOUNT_SEGMENT4", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-018 : Invalid CHARGE_ACCOUNT_SEGMENT5
                    if (!xCVSMTDB.validateValueBySegment5("FUTURE1 RD CLOUD", "Y", "00"))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = row[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-021 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT5 ";
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0021 : Invalid CHARGE_ACCOUNT_SEGMENT5", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    //Error PO005-018 : Invalid CHARGE_ACCOUNT_SEGMENT6
                    if (!xCVSMTDB.validateValueBySegment6("FUTURE2 RD CLOUD", "Y", "0000"))
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-022 ";
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT6 ";
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO005-0022 : Invalid CHARGE_ACCOUNT_SEGMENT6", requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }

                    blanketAgreement = getBlanketAgreement1(row[xCMPITDB.xCMPIT.supplier_code].ToString().Trim(),
                        row[xCMPITDB.xCMPIT.item_code].ToString().Trim(), row[xCMPITDB.xCMPIT.confirm_qty].ToString().Trim());
                    String[] aa = blanketAgreement.Split(',');
                    String price = "", agreementLineNumber = "";
                    if (aa.Length > 0)
                    {
                        blanketAgreement = aa[0];
                        price = aa[1];
                        agreementLineNumber = aa[2];
                    }
                    if (blanketAgreement.IndexOf("false") >= 0)
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim();
                        vPP.Message = "Error PO005-" + blanketAgreement.Replace("flase", "");
                        vPP.Validate = "row " + row1 + " store_code=" + row[xCMPITDB.xCMPIT.store_code].ToString().Trim() + " CHARGE_ACCOUNT_SEGMENT6 ";
                        lVPr.Add(vPP);
                        xCMPITDB.updateValidateFlag(row[xCMPITDB.xCMPIT.po_number].ToString().Trim(), row[xCMPITDB.xCMPIT.AGREEMENT_LINE_NUMBER].ToString().Trim(), "E", "", "kfc_po", Cm.initC.PO005PathLog);
                        cntErr++;       // gen log
                        xCMPITDB.updateErrorMessage(poNumber, item_code, store_code, "Error PO001-" + blanketAgreement.Replace("false", ""), requestId, "kfc_po", Cm.initC.PO005PathLog);
                    }
                    else
                    {
                        addXcustPRHIA(row[xCMPITDB.xCMPIT.po_number].ToString().Trim(), currDate, rowG[xCMPITDB.xCMPIT.file_name].ToString().Trim());//ทำ รอไว้ เพื่อ process ช้า
                        row.BeginEdit();
                        row[xCMPITDB.xCMPIT.AGREEEMENT_NUMBER] = blanketAgreement;
                        row[xCMPITDB.xCMPIT.PRICE] = price;
                        row.EndEdit();
                        addXcustPRLIAFromxCLFPT(row);
                        addXcustPRDIAFromxCLFPT(row);
                        //xCMPITDB.updateValidateFlag(row[xCMPITDB.xCMPIT.po_number].ToString().Trim(), row[xCMPITDB.xCMPIT.AGREEMENT_LINE_NUMBER].ToString().Trim(), "Y", blanketAgreement, "kfc_po", Cm.initC.PO005PathLog);
                        xCMPITDB.updateValidateFlag2(row[xCMPITDB.xCMPIT.po_number].ToString().Trim(), row[xCMPITDB.xCMPIT.AGREEMENT_LINE_NUMBER].ToString().Trim()
                            , blanketAgreement, agreementLineNumber, supplierSiteCode, suppName, subInv_code, price, "kfc_po", Cm.initC.pathLogErr);
                    }
                    if (cntErr > 0)
                    {
                        cntFileErr++;
                    }
                }
                vF.recordError = cntFileErr.ToString();   // gen log
                vF.totalError = cntErr.ToString();   // gen log
                lVfile.Add(vF);   // gen log
            }
            pB1.Visible = false;
            addListView("Update Validate Flag  ", "Validate", lv1, form1);
            updateValidateFlagY(requestId);
            addListView("gen log file  ", "Validate", lv1, form1);
            xCMPITDB.logProcessPO005("xcustpo005", dateStart, requestId);   // gen log
            addListView("move file to archive error  ", "Validate", lv1, form1);
            //Cm.logProcess("xcustpo005", lVPr, dateStart, lVfile);   // gen log
        }
        private void updateValidateFlagY(String requestId)
        {
            DataTable dt = new DataTable();
            dt = xCMPITDB.selectByRequestId(requestId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String poNumber = "", item_code = "", store_code = "", chk="";
                    poNumber = row[xCMPITDB.xCMPIT.po_number].ToString();
                    item_code = row[xCMPITDB.xCMPIT.item_code].ToString();
                    store_code = row[xCMPITDB.xCMPIT.store_code].ToString();
                    if (poNumber.Equals("12819344"))
                    {
                        chk = "";
                    }
                    if (row[xCMPITDB.xCMPIT.error_message].ToString().Length == 0)
                    {
                        xCMPITDB.updateValidateFlagY(poNumber, item_code, store_code, requestId, "kfc_po", Cm.initC.PO005PathLog);
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
            addListView("insert table " + Cm.initC.PO005PathProcess, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            foreach (XcustPorReqHeaderIntAll xcprhia in listXcustPRHIA)
            {
                if (insertXcustPorReqHeaderIntAll(xcprhia, date, time).Equals("1"))
                {
                    foreach (XcustPorReqLineIntAll xcprlia in listXcustPRLIA)
                    {
                        //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                        String chk = xCPRLIADB.insert(xcprlia, Cm.initC.PO005PathLog);
                    }
                    foreach (XcustPorReqDistIntAll xcprdia in listXcustPRDIA)
                    {
                        //XcustPorReqLineIntAll xcprlia = xCPRLIADB.setData(row, xCLFPTDB.xCLFPT);
                        String chk = xCPRDIADB.insert(xcprdia, Cm.initC.PO005PathLog);
                    }
                }
            }
        }
        public void processInsertTable2(String requestId, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("insert table " + Cm.initC.PO005PathProcess, "Validate", lv1, form1);
            String currDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            int rowH = 0;
            DataTable dt = new DataTable();
            dt = xCMPITDB.selectFilenameByRequestId(requestId);     //moveFileToFolderArchiveError
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String filename = "", rowCnt = "";
                    filename = row[xCMPITDB.xCMPIT.file_name].ToString();
                    rowCnt = row["row_Cnt"].ToString();
                    String cnt = "";
                    cnt = xCMPITDB.getCountNoErrorByFilename(requestId, filename);

                    if (cnt.Equals(rowCnt))
                    {
                        //ที่ ผ่าน ทั้ง file
                        DataTable dtFilename = new DataTable();
                        //dtFilename = xCLFPTDB.selectLinfoxByFilename(filename, requestId);
                        dtFilename = xCMPITDB.selectLinfoxGroupByPoNumber(filename, requestId);
                        if (dtFilename.Rows.Count > 0)
                        {
                            foreach (DataRow rowFilename in dtFilename.Rows)
                            {
                                rowH++;
                                String poNumber = "";
                                poNumber = rowFilename[xCMPITDB.xCMPIT.po_number].ToString();
                                DataTable dtLinfox = new DataTable();
                                dtLinfox = xCMPITDB.selectLinfoxByPoNumber(requestId, poNumber);
                                XcustPorReqHeaderIntAll xCPorRHIA = addXcustPRHIA1(poNumber, currDate, row[xCMPITDB.xCMPIT.file_name].ToString(), requestId, rowH);
                                String seqH = "";
                                seqH = xCPRHIADB.insert(xCPorRHIA, Cm.initC.PO005PathLog);
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

            xCMPITDB.updatePrcessFlag(requestId, "kfc_po", Cm.initC.pathLogErr);
        }
        private String insertXcustPorReqHeaderIntAll(XcustPorReqHeaderIntAll xcprhia, String date, String time)
        {//row[dc].ToString().Trim().
            String chk = "";
            XcustPorReqHeaderIntAll xCPRHIA = new XcustPorReqHeaderIntAll();
            xCPRHIA.ATTRIBUTE1 = xcprhia.ATTRIBUTE1.Trim();

            xCPRHIA.ATTRIBUTE_DATE1 = date;
            xCPRHIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
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
            xCPRHIA.ATTRIBUTE1 = xcprhia.ATTRIBUTE1;
            xCPRHIA.REQ_BU_NAME = xcprhia.REQ_BU_NAME;
            xCPRHIA.STATUS_CODE = xcprhia.STATUS_CODE;
            chk = xCPRHIADB.insert(xCPRHIA, Cm.initC.PO005PathLog);
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
            sql = "Select xcbaht.*, xcblt." + xCBALTDB.xCBALT.PRICE + " From " + xCBAHTDB.xCBAHT.table +
                " xcbaht " +
                "left join " + xCBALTDB.xCBALT.table + " xcblt ON xcblt." + xCBALTDB.xCBALT.PO_HEADER_ID + "=xcbaht." + xCBAHTDB.xCBAHT.PO_HEADER_ID + " " +
                "Where xcbaht." + xCBAHTDB.xCBAHT.SUPPLIER_CODE + "  = '" + supp_code + "' " +
                "and xcblt." + xCBALTDB.xCBALT.ITEM_CODE + "='" + item_code + "' and xcbaht." +
            xCBAHTDB.xCBAHT.STATUS + "='OPEN'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
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
        public String getBlanketAgreement1(String supp_code, String item_code, String qty)
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
            sql = "select xcbaht.* , xcblt." + xCBALTDB.xCBALT.PRICE + ", " + xCBALTDB.xCBALT.PRICE_LIMIT + ", " + xCBALTDB.xCBALT.LINE_NUMBER + " " +
                "From XCUST_BLANKET_AGREEMENT_HEADER_TBL xcbaht " +
                ",XCUST_BLANKET_AGREEMENT_LINES_TBL xcblt " +
                "where xcblt.PO_HEADER_ID = xcbaht.PO_HEADER_ID " +
                "and xcbaht.SUPPLIER_CODE = '" + supp_code + "' " +
                "and xcblt.ITEM_CODE = '" + item_code + "' " +
                "and xcbaht.status = 'OPEN' " +
                "and xcblt.line_status = 'OPEN'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if ((dt.Rows[0][xCBALTDB.xCBALT.PRICE_LIMIT] == null) || (dt.Rows[0][xCBALTDB.xCBALT.PRICE_LIMIT].ToString().Equals("")))
                    {
                        chk = dt.Rows[0][xCBAHTDB.xCBAHT.AGREEMENT_NUMBER].ToString().Trim() + "," + dt.Rows[0][xCBALTDB.xCBALT.PRICE].ToString().Trim() + "," + dt.Rows[0][xCBALTDB.xCBALT.LINE_NUMBER].ToString().Trim();
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
        private XcustPorReqHeaderIntAll addXcustPRHIA1(String po_number, String curr_date, String filename, String requestId, int rowH)
        {
            String seq = String.Concat("00" + rowH);
            //String seq = String.Concat("00" + listXcustPRHIA.Count);
            XcustPorReqHeaderIntAll xcprhia1 = new XcustPorReqHeaderIntAll();
            xcprhia1.ATTRIBUTE1 = po_number;
            xcprhia1.IMPORT_SOURCE = Cm.initC.PO005ImportSource;
            xcprhia1.REQ_BU_NAME = Cm.initC.BU_NAME;
            xcprhia1.STATUS_CODE = Cm.initC.PR_STATAUS;
            xcprhia1.REQ_HEADER_INTERFACE_ID = po_number;
            xcprhia1.BATCH_ID = curr_date.Replace("-", "") + seq.Substring(seq.Length - 2);
            xcprhia1.REQUITITION_NUMBER = "PR" + curr_date.Substring(2, 2);
            xcprhia1.DESCRIPTIONS = "LINFOX_" + po_number + "_" + filename;
            xcprhia1.ATTRIBUTE_CATEGORY = "LINFOX_PR’";
            xcprhia1.ATTRIBUTE2 = po_number;
            xcprhia1.request_id = requestId;

            return xcprhia1;
        }
        private void addXcustPRHIA(String po_number, String curr_date, String filename)
        {
            Boolean chk = true;
            foreach (XcustPorReqHeaderIntAll xcprhia in listXcustPRHIA)
            {
                if (xcprhia.ATTRIBUTE1.Equals(po_number))
                {
                    chk = false;
                }
            }
            if (chk)
            {
                String seq = String.Concat("00" + listXcustPRHIA.Count);
                XcustPorReqHeaderIntAll xcprhia1 = new XcustPorReqHeaderIntAll();
                xcprhia1.ATTRIBUTE1 = po_number;
                xcprhia1.IMPORT_SOURCE = Cm.initC.PO005ImportSource;
                xcprhia1.REQ_BU_NAME = Cm.initC.BU_NAME;
                xcprhia1.STATUS_CODE = Cm.initC.PR_STATAUS;
                xcprhia1.REQ_HEADER_INTERFACE_ID = po_number;
                xcprhia1.BATCH_ID = curr_date.Replace("-", "") + seq.Substring(seq.Length - 2);
                xcprhia1.REQUITITION_NUMBER = "PR" + curr_date.Substring(2, 2);
                xcprhia1.DESCRIPTIONS = "LINFOX_" + po_number + "_" + filename;
                xcprhia1.ATTRIBUTE_CATEGORY = "LINFOX_PR’";
                xcprhia1.ATTRIBUTE2 = po_number;
                listXcustPRHIA.Add(xcprhia1);
            }
        }
        private XcustPorReqLineIntAll addXcustPRLIAFromxCLFPT1(DataRow row)
        {
            XcustPorReqLineIntAll item = new XcustPorReqLineIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCMPITDB.xCMPIT.po_number].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCMPITDB.xCMPIT.AGREEMENT_LINE_NUMBER].ToString();
            item.DESTINATION_TYPE_CODE = "";
            item.PRC_BU_NAME = "";
            item.DELIVER_TO_ORGANIZATION_CODE = Cm.initC.ORGANIZATION_code;
            item.DELIVER_TO_LOCATION_CODE = row[xCMPITDB.xCMPIT.deriver_to_location].ToString();
            item.DESTINATION_SUBINVENTORY = row[xCMPITDB.xCMPIT.subinventory_code].ToString();
            item.CATEGORY_NAME = row[xCMPITDB.xCMPIT.ITEM_CATEGORY_NAME].ToString();
            item.NEED_BY_DATE = row[xCMPITDB.xCMPIT.delivery_date].ToString();
            item.ITEM_CODE = row[xCMPITDB.xCMPIT.item_code].ToString();
            item.LINE_TYPE = "";

            item.QTY = row[xCMPITDB.xCMPIT.confirm_qty].ToString();
            item.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
            item.AGREEMENT_NUMBER = row[xCMPITDB.xCMPIT.AGREEEMENT_NUMBER].ToString();
            item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            item.Price = row[xCMPITDB.xCMPIT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";
            item.UOM_CODE = row[xCMPITDB.xCMPIT.uom_code].ToString();

            return item;
        }
        private void addXcustPRLIAFromxCLFPT(DataRow row)
        {
            XcustPorReqLineIntAll item = new XcustPorReqLineIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCMPITDB.xCMPIT.po_number].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCMPITDB.xCMPIT.AGREEMENT_LINE_NUMBER].ToString();
            item.DESTINATION_TYPE_CODE = "";
            item.PRC_BU_NAME = "";
            item.DELIVER_TO_ORGANIZATION_CODE = Cm.initC.ORGANIZATION_code;
            item.DELIVER_TO_LOCATION_CODE = row[xCMPITDB.xCMPIT.deriver_to_location].ToString();
            item.DESTINATION_SUBINVENTORY = row[xCMPITDB.xCMPIT.subinventory_code].ToString();
            item.CATEGORY_NAME = row[xCMPITDB.xCMPIT.ITEM_CATEGORY_NAME].ToString();
            item.NEED_BY_DATE = row[xCMPITDB.xCMPIT.delivery_date].ToString();
            item.ITEM_CODE = row[xCMPITDB.xCMPIT.item_code].ToString();
            item.LINE_TYPE = "";

            item.QTY = row[xCMPITDB.xCMPIT.confirm_qty].ToString();
            item.CURRENCY_CODE = Cm.initC.CURRENCY_CODE;
            item.AGREEMENT_NUMBER = row[xCMPITDB.xCMPIT.AGREEEMENT_NUMBER].ToString();
            item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            item.Price = row[xCMPITDB.xCMPIT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";
            item.UOM_CODE = row[xCMPITDB.xCMPIT.uom_code].ToString();

            listXcustPRLIA.Add(item);
        }
        private XcustPorReqDistIntAll addXcustPRDIAFromxCLFPT1(DataRow row)
        {
            XcustPorReqDistIntAll item = new XcustPorReqDistIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCMPITDB.xCMPIT.po_number].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCMPITDB.xCMPIT.AGREEMENT_LINE_NUMBER].ToString();
            //item.DESTINATION_TYPE_CODE = "";
            //item.PRC_BU_NAME = "";
            //item.DELIVER_TO_ORGANIZATION_CODE = initC.ORGANIZATION_code;
            //item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            //item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            //item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            //item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            //item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            //item.LINE_TYPE = "";

            item.QTY = row[xCMPITDB.xCMPIT.confirm_qty].ToString();
            //item.CURRENCY_CODE = initC.CURRENCY_CODE;
            //item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";

            return item;
        }
        private void addXcustPRDIAFromxCLFPT(DataRow row)
        {
            XcustPorReqDistIntAll item = new XcustPorReqDistIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xCMPITDB.xCMPIT.po_number].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xCMPITDB.xCMPIT.AGREEMENT_LINE_NUMBER].ToString();
            //item.DESTINATION_TYPE_CODE = "";
            //item.PRC_BU_NAME = "";
            //item.DELIVER_TO_ORGANIZATION_CODE = initC.ORGANIZATION_code;
            //item.DELIVER_TO_LOCATION_CODE = row[xCLFPTDB.xCLFPT.deriver_to_location].ToString();
            //item.DESTINATION_SUBINVENTORY = row[xCLFPTDB.xCLFPT.subinventory_code].ToString();
            //item.CATEGORY_NAME = row[xCLFPTDB.xCLFPT.ITEM_CATEGORY_NAME].ToString();
            //item.NEED_BY_DATE = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
            //item.ITEM_CODE = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
            //item.LINE_TYPE = "";

            item.QTY = row[xCMPITDB.xCMPIT.confirm_qty].ToString();
            //item.CURRENCY_CODE = initC.CURRENCY_CODE;
            //item.AGREEMENT_NUMBER = row[xCLFPTDB.xCLFPT.AGREEEMENT_NUMBER].ToString();
            //item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            //item.Price = row[xCLFPTDB.xCLFPT.PRICE].ToString();
            item.PROCESS_FLAG = "Y";

            listXcustPRDIA.Add(item);
        }
        public void processGenCSV(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String requestId)
        {
            ControlRDPO cRDPO = new ControlRDPO(Cm);
            addListView("processGenCSVxCPRHIA ", "CVS", lv1, form1);
            cRDPO.processGenCSVxCPRHIA(lv1, form1, pB1,"PO005", requestId);
            addListView("processGenCSVxCPRLIA ", "CVS", lv1, form1);
            cRDPO.processGenCSVxCPRLIA(lv1, form1, pB1, "PO005", requestId);
            addListView("processGenCSVxCPRDIA ", "CVS", lv1, form1);
            cRDPO.processGenCSVxCPRDIA(lv1, form1, pB1, "PO005", requestId);
            addListView("processGenZIP ", "CVS", lv1, form1);
            cRDPO.processGenZIP(lv1, form1, pB1, "PO005");
        }
    }
}
