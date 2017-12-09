using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class ControlPO006
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
        ControlPRPOWebService cPRPOWS;

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
        public XcustPoTblDB xCPoTDB;
        private String dateStart = "";      //gen log

        List<ValidatePrPo> lVPr = new List<ValidatePrPo>();   // gen log
        List<ValidateFileName> lVfile = new List<ValidateFileName>();   // gen log
        int cntErr = 0, cntFileErr = 0;   // gen log

        public ControlPO006(ControlMain cm)
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
            xCPoTDB = new XcustPoTblDB(conn, Cm.initC);

            Cm.createFolderPO006();
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
            cPRPOWS.setXcustPRTbl(lv1, form1, pB1, Cm.initC.PO006PathLog);
            cPRPOWS.setXcustPOTbl(lv1, form1, pB1, Cm.initC.PO006PathLog);
        }
        /*
         * a.	Query ข้อมูลที่ Table XCUST_PR_PO_INFO_TBL โดยมี Data Source ที่เป็น “MMX”    และมี Delivery date ตาม Parameter
         * b.	Write file ตาม Format  ลงใน Folder ตาม Path Parameter Path Initial โดย File จะทำการ Gen 
         */
        public void processGetTempTableToValidate(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("gen file " + Cm.initC.PO006PathInitial, "Validate", lv1, form1);
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH_mm_ss");
            dateStart = date + " " + time;       //gen log
            int i = 0;

            pB1.Show();
            Boolean chk = false;

            getListXcSIMT();
            getListXcIMT();
            getListXcSMT();
            getListXcVSMT();
            getListXcUMT();

            ValidatePrPo vPP = new ValidatePrPo();   // gen log
            DataTable dt006 = new DataTable();
            DataTable dtFixLen = xCPrTDB.selectPO006FixLen();
            if (dtFixLen.Rows.Count <= 0) return;
            dt006 = xCPrTDB.selectPRPO006GroupByVendorDeliveryDate();
            if (dt006.Rows.Count > 0)
            {
                pB1.Minimum = 0;
                pB1.Maximum = dt006.Rows.Count;
                foreach(DataRow row in dt006.Rows)
                {
                    i++;
                    pB1.Value = i;
                    String deliveryDate = row["deliveryDate"].ToString();
                    DataTable dt = new DataTable();
                    if (Cm.initC.Po006DeliveryDate.Equals("sysdate"))
                    {
                        date = System.DateTime.Now.ToString("yyyy-MM-dd");
                        dt = xCPrTDB.selectPRPO006(row[xCPoTDB.xCPO.VENDOR_ID].ToString(), date, Cm.initC.PO006ReRun);
                    }
                    else
                    {
                        dt = xCPrTDB.selectPRPO006(row[xCPoTDB.xCPO.VENDOR_ID].ToString(), Cm.initC.Po006DeliveryDate, Cm.initC.PO006ReRun);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        writeTextPO006(row[xCPoTDB.xCPO.VENDOR_ID].ToString(), deliveryDate, dt, dtFixLen);
                    }
                }
            }
            else
            {
                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = Cm.initC.PO006PathLog;   // gen log
                vF.recordTotal = "1";   // gen log

                vPP = new ValidatePrPo();
                vPP.Filename = "PO006";
                vPP.Message = "No Data";
                vPP.Validate = "Error PO006-001: No Data Found ";
                lVPr.Add(vPP);
                cntErr++;       // gen log
                lVfile.Add(vF);   // gen log
            }
            Cm.logProcess("xcustpo006", lVPr, dateStart, lVfile);   // gen log
            Cm.setConfig("Po006DeliveryDate", "sysdate");
            Cm.setConfig("PO006ReRun", "N");
            pB1.Hide();
        }
        public void writeTextPO006(String vendor_id, String delivery_date, DataTable dt, DataTable dtFixLen)
        {
            var file = Cm.initC.PO006PathInitial + "S" + vendor_id+"_R" + delivery_date.Replace("-","") + ".KFC";
            using (var stream = File.CreateText(file))
            {
                String hCol01 = Cm.FixLen("S" + vendor_id, dtFixLen.Rows[0]["X_LENGTH"].ToString()," ");
                String hCol02 = Cm.FixLen(delivery_date.Replace("-", ""), dtFixLen.Rows[1]["X_LENGTH"].ToString(), " ");
                String hCol3 = Cm.FixLen(dt.Rows.Count.ToString(), dtFixLen.Rows[2]["X_LENGTH"].ToString(), "0");
                String head = hCol01 + hCol02 + hCol3;//+System.Environment.NewLine;
                stream.WriteLine(head);
                foreach (DataRow row in dt.Rows)
                {
                    String col01 = Cm.FixLen(row["po_number"].ToString(), dtFixLen.Rows[3]["X_LENGTH"].ToString()," ");
                    String col02 = Cm.FixLen(delivery_date.Replace("-", ""), dtFixLen.Rows[4]["X_LENGTH"].ToString(), " ");     //PO date
                    String col03 = Cm.FixLen(Cm.initC.ORGANIZATION_code, dtFixLen.Rows[5]["X_LENGTH"].ToString(), " ");     //Store Code
                    String col04 = Cm.FixLen(delivery_date.Replace("-", ""), dtFixLen.Rows[6]["X_LENGTH"].ToString(), " ");          //Delivery Date
                    String col05 = Cm.FixLen(row["ITEM_ID"].ToString(), dtFixLen.Rows[7]["X_LENGTH"].ToString(), " ");
                    String col06 = Cm.FixLen(row["QUANTITY"].ToString(), dtFixLen.Rows[8]["X_LENGTH"].ToString(), " ");
                    String col07 = Cm.FixLen(row["UOM_CODE"].ToString(), dtFixLen.Rows[9]["X_LENGTH"].ToString(), " ");

                    String csvRow = col01 + col02 + col03 + col04 + col05 + col06 + col07 ;

                    stream.WriteLine(csvRow);
                }
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
        public void sendEmailPO006(String vendorName)
        {
            var fromAddress = new MailAddress(Cm.initC.EmailUsername, "Ekapop Ploentham");
            var toAddress = new MailAddress(Cm.initC.APPROVER_EMAIL, "To Name");
            var toAddress2 = new MailAddress("amo@iceconsulting.co.th", "To Name");
            var toAddress3 = new MailAddress("ekk@ii.co.th", "To Name");
            String fromPassword = Cm.initC.EmailPassword;
            const string subject = "test";
            DataTable dt006;
            dt006 = xCPrTDB.selectPRPO006GroupByVendor();
            if (dt006.Rows.Count <= 0) return;
            string Body = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\" + "email_regis.html");
            Body = Body.Replace("#vendorName#", vendorName);

            var smtp1 = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            var message = new MailMessage();
            message.From = fromAddress;
            message.Subject = "Test send Email form PO006";
            message.To.Add(toAddress);
            message.To.Add(toAddress2);
            message.To.Add(toAddress3);
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            LinkedResource LinkedImage = new LinkedResource(@Environment.CurrentDirectory + "\\" + "logo_ice_consulting.png");
            LinkedImage.ContentId = "logo_ice";
            //Added the patch for Thunderbird as suggested by Jorge
            LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
            htmlView.LinkedResources.Add(LinkedImage);
            message.AlternateViews.Add(htmlView);

            
            if (dt006.Rows.Count > 0)
            {
                foreach(DataRow row in dt006.Rows)
                {
                    String[] filePO;
                    filePO = Cm.getFileinFolder(Cm.initC.PO006PathInitial, row["VENDOR_ID"].ToString());
                    if (filePO.Length > 0)
                    {
                        foreach (string aa in filePO)
                        {
                            Attachment attachment;
                            attachment = new System.Net.Mail.Attachment(aa);
                            message.Attachments.Add(attachment);
                        }
                    }
                }                
            }

            message.Body = Body;

            smtp1.Send(message);

        }
    }
}
