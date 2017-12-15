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
    public class ControlPO002
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

        ControlPRPOWebService cPRPOWS;

        public ValidatePrPo vPrPo;      //gen log

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

        public XcustPrTblDB xCPRTDB;
        public XcustPoTblDB xCPOTDB;
        public XcustLinfoxPrTblDB xCLFPTDB;      //po001
        public XcustLogModuleDB xCLMDB;

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;

        private List<XcustRcvHeadersIntAll> listXcustRHIA;
        private List<XcustRcvTransactionsIntAll> listXcusTRTIA;
        private List<XcustInvTransactionLostsIntTbl> listXcusITLIT;

        private String dateStart = "";      //gen log

        List<ValidatePrPo> lVPr = new List<ValidatePrPo>();   // gen log
        List<ValidateFileName> lVfile = new List<ValidateFileName>();   // gen log
        int cntErr = 0, cntFileErr = 0;   // gen log

        String requestId = "";

        public ControlPO002(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            vPrPo = new ValidatePrPo();

            conn = new ConnectDB("kfc_po", Cm.initC);        //standard     XcustRcvHeadersIntAllDB

            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, Cm.initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, Cm.initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn, Cm.initC);
            xCIMTDB = new XcustItemMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCUMTDB = new XcustUomMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);

            xCPRTDB = new XcustPrTblDB(conn, Cm.initC);
            xCPOTDB = new XcustPoTblDB(conn, Cm.initC);
            xCLFPTDB = new XcustLinfoxPrTblDB(conn, Cm.initC);
            xCLMDB = new XcustLogModuleDB(conn, Cm.initC);

            Cm.createFolderPO002();
            cPRPOWS = new ControlPRPOWebService(Cm);

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
        }
        public void processWebService(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            cPRPOWS.setXcustPRTbl(lv1, form1, pB1, Cm.initC.PO002PathLog);
            cPRPOWS.setXcustPOTbl(lv1, form1, pB1, Cm.initC.PO002PathLog);
        }
        public void processMapping(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("เข้า processMapping", "Web Service", lv1, form1);
            pB1.Show();
            pB1.Minimum = 0;
            int i = 0;

            ValidatePrPo vPP = new ValidatePrPo();   // gen log
            DataTable dtLinfox = new DataTable();
            //a.	Query ข้อมูลที่ทาง Linfox เคยส่งเข้ามา Interface PO001  ที่ Table XCUST_LINFOX_PR_TBL โดย SEND_PO_FLAG  = 'N' ,Process_flag = 'Y' และ GEN_OUTBOUD_FLAG = 'N'
            
            requestId = xCLFPTDB.getPO002RequestID();       //ใช้ ตัวเดียวกันกับ PO001
            dtLinfox = xCLFPTDB.selectPO002();
            if (dtLinfox.Rows.Count > 0)
            {
                xCLFPTDB.updateRequestIdPo002(requestId);
                pB1.Maximum = dtLinfox.Rows.Count;
                foreach(DataRow linfox in dtLinfox.Rows)
                {
                    i++;
                    pB1.Value = i;
                    DataTable dt = new DataTable();
                    String poNumber = "", lineNumber="";
                    poNumber = linfox[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
                    lineNumber = linfox[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
                    //b.Program ทำการ mapping ข้อมูลกับ table XCUST_PR_PO_INFO_TBL แล้ว update ข้อมูล field ERP_PO_NUMBER ,ERP_QTY ที่ table XCUST_LINFOX_PR_TBL
                    dt = xCPRTDB.selectPRPO(linfox[xCLFPTDB.xCLFPT.PO_NUMBER].ToString(), linfox[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString(), "LINFOX");
                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow prpo in dt.Rows)
                        {//
                            //xCLPTDB.updateFromPO002(prpo[xCPRTDB.xCPR.REQUISITION_HEADER_ID].ToString(), prpo[xCPRTDB.xCPR.REQUISITION_LINE_ID].ToString(),
                            //    linfox[xCLPTDB.xCLFPT.PO_NUMBER].ToString(), linfox[xCLPTDB.xCLFPT.LINE_NUMBER].ToString());
                            String ERP_PO_HEADER_ID = prpo["PO_HEADER_ID"].ToString();
                            String ERP_PO_LINE_ID = prpo["PO_LINE_ID"].ToString();
                            String ERP_PO_LINE_NUMBER = prpo["po_line_number"].ToString();
                            xCLFPTDB.updateFromPO002(prpo["po_number"].ToString(), prpo["QUANTITY"].ToString()
                                , ERP_PO_HEADER_ID, ERP_PO_LINE_ID
                                , ERP_PO_LINE_NUMBER, requestId,
                                linfox[xCLFPTDB.xCLFPT.PO_NUMBER].ToString(), linfox[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString());
                        }
                    }
                    else
                    {
                        vPP = new ValidatePrPo();
                        vPP.Filename = "PO002";
                        vPP.Message = "No Data "+ linfox[xCLFPTDB.xCLFPT.PO_NUMBER].ToString() + "" + linfox[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
                        vPP.Validate = "Error PO002-001 mapping : No Data Found  ";
                        lVPr.Add(vPP);
                        cntErr++;       // gen log
                        xCLFPTDB.updateErrorMessagePO002(poNumber, lineNumber, "Error PO002-001 mapping : No Data Found", requestId, "kfc_po", Cm.initC.PO002PathLog);
                    }
                }
            }
            else
            {
                vPP = new ValidatePrPo();
                vPP.Filename = "PO002";
                vPP.Message = "No Data";
                vPP.Validate = "Error PO002-001: No Data Found  ";
                lVPr.Add(vPP);
                cntErr++;       // gen log
                xCLMDB.insertLog("PO002", "", "Error PO002-001: No Data Found", Cm.initC.PO002PathLog);
            }
            updateValidateFlagY(requestId);
            xCLFPTDB.logProcessPO001("xcustpo002", dateStart, requestId);   // gen log

            pB1.Hide();
        }
        private void updateValidateFlagY(String requestId)
        {
            DataTable dt = new DataTable();
            dt = xCLFPTDB.selectPO002LinfoxByRequestId(requestId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String poNumber = "", lineNumber = "", chk = "";
                    poNumber = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
                    lineNumber = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
                    if (poNumber.Equals("12819344"))
                    {
                        chk = "";
                    }
                    if (row[xCLFPTDB.xCLFPT.ERROR_MSG2].ToString().Length == 0)
                    {
                        //xCLFPTDB.updateValidateFlagY(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString(), row[xCLFPTDB.xCLFPT.request_id].ToString(), "kfc_po", Cm.initC.pathLogErr);
                        xCLFPTDB.updateValidateFlagYPO002(row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString(), row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString(), row[xCLFPTDB.xCLFPT.request_id].ToString(), "kfc_po", Cm.initC.PO002PathLog);
                    }
                }
            }
        }
        public void processGenTextLinfox(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("เข้า processGenTextLinfox", "Web Service", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH_mm_ss");
            dateStart = date + " " + time;       //gen log
            DataTable dtLinfoxPoNumber = new DataTable();
            int i = 0;
            dtLinfoxPoNumber = xCLFPTDB.selectPO002GenTextLinfoxGroupByERPPONumber();
            pB1.Show();
            pB1.Minimum = 0;
            if (dtLinfoxPoNumber.Rows.Count > 0)
            {
                pB1.Maximum = dtLinfoxPoNumber.Rows.Count;
                addListView("processGenTextLinfox พบข้อมูล "+dtLinfoxPoNumber.Rows.Count, "Web Service", lv1, form1);
                foreach (DataRow linfox in dtLinfoxPoNumber.Rows)
                {
                    i++;
                    pB1.Value = i;
                    String erpPONumber = "";
                    erpPONumber = linfox[xCLFPTDB.xCLFPT.ERP_PO_NUMBER].ToString();
                    DataTable dt = new DataTable();
                    dt = xCLFPTDB.selectPO002GenTextLinfox(erpPONumber);
                    writeTextLinfox(erpPONumber,dt);

                    // f.	Update po_trb ว่า gen_outbound_flag เรียบร้อย 
                    xCPOTDB.updateOutBoundFlag(dt.Rows[0][xCLFPTDB.xCLFPT.PO_NUMBER].ToString(), dt.Rows[0][xCLFPTDB.xCLFPT.LINE_NUMBER].ToString(), Cm.initC.PO002PathLog);
                    //d.Program update ข้อมูล XCUST_LINFOX_PR_TBL.GEN_OUTBOUND_FLAG = 'Y'

                    //update xcust_linfox_pr_int_tbl set GEN_OUTBOUD_FLAG = null where GEN_OUTBOUD_FLAG = 'Y'
                    xCLFPTDB.updateOutBoundFlag(dt.Rows[0][xCLFPTDB.xCLFPT.PO_NUMBER].ToString(), dt.Rows[0][xCLFPTDB.xCLFPT.LINE_NUMBER].ToString());     
                }
                pB1.Visible = false;
                addListView("processGenTextLinfox gen log file ", "Web Service", lv1, form1);
                Cm.logProcess("xcustpo002", lVPr, dateStart, lVfile);   // gen log
                xCLFPTDB.logProcessPO002("xcustpo002", dateStart, requestId);   // gen log
            }
            else
            {
                addListView("processGenTextLinfox  ไม่พบข้อมูล", "Web Service", lv1, form1);
            }
            pB1.Hide();
        }
        public void writeTextLinfox(String erpPONumber, DataTable dt)
        {
            ValidatePrPo vPP = new ValidatePrPo();   // gen log

            String reqDate = "";
            reqDate = dt.Rows[0][xCLFPTDB.xCLFPT.REQUEST_DATE].ToString().Replace("-","").Replace(":", "").Replace("/", "");
            var file = Cm.initC.PO002PathInitial + "PO"+ erpPONumber+ reqDate+".txt";
            try
            {
                using (var stream = File.CreateText(file))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        String ponumber = "";
                        String linenumber = "";
                        try
                        {
                            ponumber = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString();
                            linenumber = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString();
                            string col01 = "A";
                            string col02 = row[xCLFPTDB.xCLFPT.store_code].ToString();      //Branch/Plant
                            string col03 = row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString();
                            string col04 = "WP";
                            string col05 = ponumber;
                            string col06 = Cm.initC.Company;     //Company
                            string col07 = linenumber;
                            string col08 = row[xCLFPTDB.xCLFPT.ERP_PO_NUMBER].ToString();
                            string col09 = reqDate;
                            string col10 = row[xCLFPTDB.xCLFPT.REQUEST_TIME].ToString();
                            col10 = col10.Equals("0") ? "000000" : col10;
                            string col11 = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString();
                            string col12 = row[xCLFPTDB.xCLFPT.ERP_QTY].ToString();
                            string col13 = row[xCLFPTDB.xCLFPT.UOMCODE].ToString();     //Unit Of Mesure
                            string col14 = row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString();
                            string col15 = row[xCLFPTDB.xCLFPT.DELIVERY_INSTRUCTION].ToString();     //Delivery instruction

                            string csvRow = col01 + "|" + col02 + "|" + col03 + "|" + col04 + "|" + col05 + "|" + col06 + "|" + col07 + "|" + col08 + "|" + col09 + "|" + col10
                                + "|" + col11 + "|" + col12 + "|" + col13 + "|" + col14 + "|" + col15;

                            stream.WriteLine(csvRow);
                        }
                        catch (Exception ex)
                        {
                            vPP = new ValidatePrPo();
                            vPP.Filename = file;
                            vPP.Message = "error " + ex.Message;
                            vPP.Validate = "Error PO002-003 : : Cannot write file ";
                            lVPr.Add(vPP);
                            cntErr++;       // gen log
                            xCLFPTDB.updateErrorMessagePO002(ponumber, linenumber, "Error PO002-003 : Cannot write file", "", "kfc_po", Cm.initC.PO002PathLog);
                        }
                    }
                }
            }
            catch (IOException ioex)
            {
                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = file;   // gen log
                vF.recordTotal = dt.Rows.Count.ToString();   // gen log
                vF.recordError = cntFileErr.ToString();   // gen log
                vF.totalError = cntErr.ToString();   // gen log
                lVfile.Add(vF);   // gen log

                vPP = new ValidatePrPo();
                vPP.Filename = file;
                vPP.Message = "error " + ioex.Message;
                vPP.Validate = "Error PO002-003 : Cannot write file "; 
                lVPr.Add(vPP);
                cntErr++;       // gen log
                xCLFPTDB.updateErrorMessagePO002("", "", "Error PO002-003 : Cannot write file", "", "kfc_po", Cm.initC.PO002PathLog);
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
