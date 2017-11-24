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
    public class ControlPO007
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
                
        public XcustBuMstTblDB xCBMTDB;
        public XcustDeriverLocatorMstTblDB xCDLMTDB;
        public XcustDeriverOrganizationMstTblDB xCDOMTDB;
        public XcustSubInventoryMstTblDB xCSIMTDB;
        public XcustItemMstTblDB xCIMTDB;
        public XcustCurrencyMstTblDB xCMTDB;
        public XcustSupplierMstTblDB xCSMTDB;
        public XcustUomMstTblDB xCUMTDB;
        public XcustValueSetMstTblDB xCVSMTDB;

        private List<XcustSubInventoryMstTbl> listXcSIMT;
        private List<XcustItemMstTbl> listXcIMT;
        private List<XcustSupplierMstTbl> listXcSMT;
        private List<XcustValueSetMstTbl> listXcVSMT;
        private List<XcustUomMstTbl> listXcUMT;

        public XcustPrTblDB xCPrTDB;

        public ControlPO007(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            vPrPo = new ValidatePrPo();

            conn = new ConnectDB("kfc_po", Cm.initC);

            xCBMTDB = new XcustBuMstTblDB(conn, Cm.initC);
            xCDLMTDB = new XcustDeriverLocatorMstTblDB(conn, Cm.initC);
            xCDOMTDB = new XcustDeriverOrganizationMstTblDB(conn, Cm.initC);
            xCSIMTDB = new XcustSubInventoryMstTblDB(conn, Cm.initC);
            xCIMTDB = new XcustItemMstTblDB(conn, Cm.initC);
            xCMTDB = new XcustCurrencyMstTblDB(conn, Cm.initC);
            xCSMTDB = new XcustSupplierMstTblDB(conn, Cm.initC);
            xCUMTDB = new XcustUomMstTblDB(conn, Cm.initC);
            xCVSMTDB = new XcustValueSetMstTblDB(conn, Cm.initC);

            xCPrTDB = new XcustPrTblDB(conn, Cm.initC);

            Cm.createFolderPO007();
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
        /*
         * a.	Query ข้อมูลที่ Table XCUST_PR_PO_INFO_TBL โดยมี Data Source ที่ไม่ได้เป็น “MMX”  
         * b.	Write file ตาม Format  ลงใน Folder ตาม Path Parameter Path Initial
         */
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("gen file " + Cm.initC.PO007PathInitial, "Validate", lv1, form1);
            pB1.Visible = true;
            Boolean chk = false;
            
            getListXcSIMT();
            getListXcIMT();
            getListXcSMT();
            getListXcVSMT();
            getListXcUMT();

            DataTable dt007 = new DataTable();
            DataTable dtFixLen = xCPrTDB.selectPO007FixLen();
            dt007 = xCPrTDB.selectPRPO007();
            if (dt007.Rows.Count>0)
            {
                String date = System.DateTime.Now.ToString("yyyy-MM-dd");
                String time = System.DateTime.Now.ToString("HH");
                date = date.Replace("-", "");
                var file = Cm.initC.PO007PathInitial + "ID20_"+ date + time + "24MISS.txt";
                using (var stream = File.CreateText(file))
                {
                    foreach (DataRow row in dt007.Rows)
                    {
                        String itemcode = xCPrTDB.selectItemCode(row["PRC_BU_ID"].ToString(), row["ITEM_ID"].ToString());
                        String desc1 = "", desc2 = "",taxExp="", taxCal="",receipt_num="",line_num="",lot_number="";
                        desc1 = row["ITEM_DESCRIPTION"].ToString();
                        if(desc1.Length > int.Parse(dtFixLen.Rows[7]["X_LENGTH"].ToString()))
                        {
                            desc2 = desc1.Substring(int.Parse(dtFixLen.Rows[7]["X_LENGTH"].ToString()));
                        }
                        taxExp = !row["TAX_CODE"].ToString().Equals("") ? "V" : "E";
                        taxCal = !row["TAX_CODE"].ToString().Equals("") ? "Y" : "N";
                        DataTable dtLot = xCPrTDB.selectLot(row["PO_HEADER_ID"].ToString(), row["PO_LINE_ID"].ToString());
                        if (dtLot.Rows.Count > 0)
                        {
                            receipt_num = dtLot.Rows[0]["receipt_num"].ToString();
                            line_num = dtLot.Rows[0]["line_num"].ToString();
                            lot_number = dtLot.Rows[0]["lot_number"].ToString();
                        }
                        string col01 = Cm.FixLen(Cm.dateDBtoShow(row["CREATION_DATE"].ToString()), dtFixLen.Rows[0]["X_LENGTH"].ToString()," ");
                        string col02 = FixLen(Cm.initC.Company, dtFixLen.Rows[1]["X_LENGTH"].ToString());
                        string col03 = FixLen("col3", dtFixLen.Rows[2]["X_LENGTH"].ToString());      //PO DT
                        string col04 = FixLen(row["SEGMENT1"].ToString(), dtFixLen.Rows[3]["X_LENGTH"].ToString());
                        string col05 = FixLen(row["VENDOR_ID"].ToString(), dtFixLen.Rows[4]["X_LENGTH"].ToString());
                        string col06 = FixLen(row["PO_LINE_ID"].ToString(), dtFixLen.Rows[5]["X_LENGTH"].ToString());
                        string col07 = FixLen(itemcode, dtFixLen.Rows[6]["X_LENGTH"].ToString());
                        string col08 = FixLen(desc1, dtFixLen.Rows[7]["X_LENGTH"].ToString());
                        string col09 = FixLen(desc2, dtFixLen.Rows[8]["X_LENGTH"].ToString());      //กรณี Descriptions 1 ไม่พอ ให้ตัดมาที่ Column นี้ หรือกรณีไม่ใช่ Item Inventory
                        string col10 = FixLen(row["QUANTITY_RECEIPT"].ToString(), dtFixLen.Rows[9]["X_LENGTH"].ToString());

                        string col11 = FixLen(row["TAX_CODE"].ToString(), dtFixLen.Rows[10]["X_LENGTH"].ToString());     //PO Tax Code
                        string col12 = FixLen(taxExp, dtFixLen.Rows[11]["X_LENGTH"].ToString());      //PO Tax Exp Code
                        string col13 = FixLen("", dtFixLen.Rows[12]["X_LENGTH"].ToString());     //PO Account Code        ระบุค่าว่าง
                        string col14 = FixLen(String.Format("{0:#,##0.00}", int.Parse(row["QUANTITY"].ToString())), dtFixLen.Rows[13]["X_LENGTH"].ToString());      //FORMAT : N,NNN,NN0.00
                        string col15 = FixLen(row["UOM_CODE"].ToString(), dtFixLen.Rows[14]["X_LENGTH"].ToString());
                        string col16 = FixLen(row["UNIT_PRICE"].ToString(), dtFixLen.Rows[15]["X_LENGTH"].ToString());
                        string col17 = FixLen("", dtFixLen.Rows[16]["X_LENGTH"].ToString());      //Sub Ledger        ระบุค่าว่าง
                        string col18 = FixLen("", dtFixLen.Rows[17]["X_LENGTH"].ToString());      //Sub Ledger Type   ระบุค่าว่าง
                        string col19 = FixLen("", dtFixLen.Rows[18]["X_LENGTH"].ToString());      //Reference 1       ระบุค่าว่าง
                        string col20 = FixLen("", dtFixLen.Rows[19]["X_LENGTH"].ToString());      //Reference 2       ระบุค่าว่าง

                        string col21 = FixLen("", dtFixLen.Rows[20]["X_LENGTH"].ToString());      //Remark        ระบุค่าว่าง
                        string col22 = FixLen(row["LINE_TYPE_ID"].ToString(), dtFixLen.Rows[21]["X_LENGTH"].ToString());      //PO Line Type
                        string col23 = FixLen(row["PAYMENT_TERM"].ToString(), dtFixLen.Rows[22]["X_LENGTH"].ToString());
                        string col24 = FixLen(row["ACC_SEGMENT2"].ToString(), dtFixLen.Rows[23]["X_LENGTH"].ToString());      //Business Unit     XCUST_PO_INT_TBL.ACC_SEGMENT2
                        string col25 = FixLen("", dtFixLen.Rows[24]["X_LENGTH"].ToString());        //Transaction Originator
                        string col26 = FixLen("Direct", dtFixLen.Rows[25]["X_LENGTH"].ToString());        //Curency Mode
                        string col27 = FixLen(row["CURRENCY_CODE"].ToString(), dtFixLen.Rows[26]["X_LENGTH"].ToString());
                        string col28 = FixLen(receipt_num, dtFixLen.Rows[27]["X_LENGTH"].ToString());     //Receipt NO.
                        string col29 = FixLen("", dtFixLen.Rows[28]["X_LENGTH"].ToString());      //ระบุค่าว่าง
                        string col30 = FixLen(line_num, dtFixLen.Rows[29]["X_LENGTH"].ToString());      //XCUST_PR_PO_INFO_TBL.PO_RECEIPT_LINE_NO

                        string col31 = FixLen(taxCal, dtFixLen.Rows[30]["X_LENGTH"].ToString());      //Tax(Y / N)
                        string col32 = FixLen(lot_number, dtFixLen.Rows[31]["X_LENGTH"].ToString());      //XCUST_PR_PO_INFO_TBL.LOT_NUMBER
                        string col33 = FixLen("", dtFixLen.Rows[32]["X_LENGTH"].ToString());      //ระบุค่าว่าง
                        string col34 = FixLen("", dtFixLen.Rows[33]["X_LENGTH"].ToString());      //ระบุค่าว่าง
                        string col35 = FixLen("", dtFixLen.Rows[34]["X_LENGTH"].ToString());      //ระบุค่าว่าง
                        string col36 = FixLen("", dtFixLen.Rows[35]["X_LENGTH"].ToString());      //ระบุค่าว่าง
                        string col37 = FixLen("", dtFixLen.Rows[36]["X_LENGTH"].ToString());      //ระบุค่าว่าง
                        string col38 = FixLen("", dtFixLen.Rows[37]["X_LENGTH"].ToString());      //ระบุค่าว่าง

                        //string csvRow = col01 + "," + col02 + "," + col03 + "," + col04 + "," + col05 + "," + col06 + "," + col07 + "," + col08 + "," + col09 + "," + col10
                        //+ "," + col11 + "," + col12 + "," + col13 + "," + col14 + "," + col15 + "," + col16 + "," + col17 + "," + col18 + "," + col19 + "," + col20
                        //+ "," + col21 + "," + col22 + "," + col23 + "," + col24 + "," + col25 + "," + col26 + "," + col27 + "," + col28 + "," + col29 + "," + col30
                        //+ "," + col31 + "," + col32 + "," + col33 + "," + col34 + "," + col35 + "," + col36 + "," + col37;

                        string csvRow = col01 + col02 + col03 + col04 + col05 + col06 + col07 + col08 + col09 + col10
                        + col11 + col12 + col13 + col14 + col15 + col16 + col17  + col18  + col19  + col20
                         + col21  + col22  + col23  + col24  + col25  + col26  + col27  + col28  + col29  + col30
                         + col31  + col32  + col33  + col34  + col35  + col36  + col37;

                        stream.WriteLine(csvRow);
                    }
                }
            }
        }
        private String FixLen(String str, String len)
        {
            String chk = "", aaa="";
            int len1 = 0;
            if(int.TryParse(len,out len1))
            {
                if (len1 > str.Length)
                {
                    for (int i = 0; i < len1; i++)
                    {
                        aaa += " ";
                    }
                    chk = aaa + str;
                    chk = chk.Substring(str.Length);
                }
                else
                {
                    chk = str.Substring(0, len1);
                }                
            }
            return chk;
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
