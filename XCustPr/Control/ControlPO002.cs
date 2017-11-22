﻿using System;
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

        public ValidatePrPo vPrPo;

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
        public XcustLinfoxPrTblDB xCLPTDB;      //po001

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;

        private List<XcustRcvHeadersIntAll> listXcustRHIA;
        private List<XcustRcvTransactionsIntAll> listXcusTRTIA;
        private List<XcustInvTransactionLostsIntTbl> listXcusITLIT;

        private String dateStart = "";      //gen log

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
            xCLPTDB = new XcustLinfoxPrTblDB(conn, Cm.initC);

            Cm.createFolderPO003();
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
            cPRPOWS.setXcustPRTbl(lv1, form1, pB1);
            cPRPOWS.setXcustPOTbl(lv1, form1, pB1);
        }
        public void processMapping(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            DataTable dtLinfox = new DataTable();
            dtLinfox = xCLPTDB.selectPO002();//a.	Query ข้อมูลที่ทาง Linfox เคยส่งเข้ามา Interface PO001  ที่ Table XCUST_LINFOX_PR_TBL โดย SEND_PO_FLAG  = 'N' ,Process_flag = 'Y' และ GEN_OUTBOUD_FLAG = 'N'
            if (dtLinfox.Rows.Count > 0)
            {
                foreach(DataRow linfox in dtLinfox.Rows)
                {
                    DataTable dt = new DataTable();
                    //b.Program ทำการ mapping ข้อมูลกับ table XCUST_PR_PO_INFO_TBL แล้ว update ข้อมูล field ERP_PO_NUMBER ,ERP_QTY ที่ table XCUST_LINFOX_PR_TBL
                    dt = xCPRTDB.selectPRPO(linfox[xCLPTDB.xCLFPT.PO_NUMBER].ToString(), linfox[xCLPTDB.xCLFPT.LINE_NUMBER].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow prpo in dt.Rows)
                        {
                            xCLPTDB.updateFromPO002(prpo[xCPRTDB.xCPR.REQUISITION_HEADER_ID].ToString(), prpo[xCPRTDB.xCPR.REQUISITION_LINE_ID].ToString(),
                                linfox[xCLPTDB.xCLFPT.PO_NUMBER].ToString(), linfox[xCLPTDB.xCLFPT.LINE_NUMBER].ToString());
                        }
                    }
                }
            }
        }
        public void processGenTextLinfox(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            DataTable dtLinfoxPoNumber = new DataTable();
            dtLinfoxPoNumber = xCLPTDB.selectPO002GenTextLinfoxGroupByERPPONumber();
            if (dtLinfoxPoNumber.Rows.Count > 0)
            {
                foreach (DataRow linfox in dtLinfoxPoNumber.Rows)
                {
                    String erpPONumber = "";
                    erpPONumber = linfox[xCLPTDB.xCLFPT.ERP_PO_NUMBER].ToString();
                    DataTable dt = new DataTable();
                    dt = xCLPTDB.selectPO002GenTextLinfox(erpPONumber);
                    writeTextLinfox(erpPONumber,dt);
                }
            }
        }
        public void writeTextLinfox(String erpPONumber, DataTable dt)
        {
            String reqDate = "";
            reqDate = dt.Rows[0][xCLPTDB.xCLFPT.REQUEST_DATE].ToString().Replace("-","");
            var file = Cm.initC.PathInitial + "PO"+ erpPONumber+ reqDate+".txt";
            using (var stream = File.CreateText(file))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string col01 = "A";
                    string col02 = "";      //Branch/Plant
                    string col03 = row[xCLPTDB.xCLFPT.SUPPLIER_CODE].ToString();
                    string col04 = "WP";
                    string col05 = row[xCLPTDB.xCLFPT.PO_NUMBER].ToString();
                    string col06 = "col06";     //Company
                    string col07 = row[xCLPTDB.xCLFPT.LINE_NUMBER].ToString();
                    string col08 = row[xCLPTDB.xCLFPT.ERP_PO_NUMBER].ToString();
                    string col09 = reqDate;
                    string col10 = row[xCLPTDB.xCLFPT.REQUEST_TIME].ToString();

                    string col11 = row[xCLPTDB.xCLFPT.ITEM_CODE].ToString();
                    string col12 = row[xCLPTDB.xCLFPT.ERP_QTY].ToString();
                    string col13 = "col13";     //Unit Of Mesure
                    string col14 = row[xCLPTDB.xCLFPT.ORDER_DATE].ToString();
                    string col15 = row[xCLPTDB.xCLFPT.ERP_QTY].ToString();     //Delivery instruction

                    string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10
                        + "," + col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 ;

                    stream.WriteLine(csvRow);
                }
            }
        }
    }
}