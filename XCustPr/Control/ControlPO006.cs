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
        public XcustSupplierSiteMstTblDB xCSSMTDB;
        
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
        List<XcustSupplierSiteMstTbl> lSuppEmail = new List<XcustSupplierSiteMstTbl>();
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
            xCSSMTDB = new XcustSupplierSiteMstTblDB(conn, Cm.initC);

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
            cPRPOWS.setXcustPRTbl(lv1, form1, pB1, Cm.initC.PO006PathLog, "");
            cPRPOWS.setXcustPOTbl(lv1, form1, pB1, Cm.initC.PO006PathLog, "");
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
            String deliveryDateLog1 = "";
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
            DateTime deliveryDateLog = System.DateTime.Now;
            if (dtFixLen.Rows.Count <= 0) return;

            //dt006 = xCPrTDB.selectPRPO006GroupByVendorDeliveryDate();
            String buId = "";
            buId = xCBMTDB.selectIdActiveBuName(Cm.initC.BU_NAME);
            Cm.GetConfigPO006();
            dt006 = xCPrTDB.selectPRPO006GroupByVendorDeliveryDate2(Cm.initC.Po006DeliveryDate, Cm.initC.PO006ReRun, buId);
            if (dt006.Rows.Count > 0)
            {
                pB1.Minimum = 0;
                pB1.Maximum = dt006.Rows.Count;
                foreach(DataRow row in dt006.Rows)
                {
                    i++;
                    pB1.Value = i;
                    String deliveryDate = row["delivery_Date"].ToString();
                    DataTable dt = new DataTable();
                    try
                    {
                        String vendorId = "";
                        //vendorId = xCSMTDB.validateSupplierBySupplierCode1(row["SUPPLIER_NUMBER"].ToString().Trim());
                        vendorId = row[xCPoTDB.xCPO.VENDOR_ID].ToString().Trim();
                        //if (Cm.initC.Po006DeliveryDate.Equals("sysdate"))
                        //{
                        //    date = System.DateTime.Now.ToString("yyyy-MM-dd");
                        //    dt = xCPrTDB.selectPRPO006(vendorId, date, Cm.initC.PO006ReRun);
                        //    deliveryDateLog = DateTime.Parse(date);
                        //}
                        //else
                        //{
                        //    dt = xCPrTDB.selectPRPO006(vendorId, Cm.initC.Po006DeliveryDate, Cm.initC.PO006ReRun);
                        //    deliveryDateLog = DateTime.Parse(Cm.initC.Po006DeliveryDate);
                        //}
                        dt = xCPrTDB.selectPRPO0061(vendorId, deliveryDate);
                        if (dt.Rows.Count > 0)
                        {
                            //String filename = writeTextPO006(row["SUPPLIER_NUMBER"].ToString(), deliveryDate, dt, dtFixLen);      //60-12-25
                            String filename = writeTextPO006(row["SUPPLIER_NUMBER"].ToString(),row["attribute2"].ToString(), deliveryDate, dt, dtFixLen);
                            xCPoTDB.updateOutBoundFlagPO006_1(vendorId, deliveryDate, Cm.initC.PO006PathLog);
                            ValidateFileName vF = new ValidateFileName();   // gen log
                            vF.fileName = filename;   // gen log
                            vF.recordTotal = "1";   // gen log
                            vF.Message = "";
                            vF.recordError = "0";
                            lVfile.Add(vF);   // gen log
                            DataTable dtSupp = new DataTable();
                            String email = "";
                            dtSupp = xCSSMTDB.SelectByVendorId(vendorId);
                            XcustSupplierSiteMstTbl supp = new XcustSupplierSiteMstTbl();
                            if (dtSupp.Rows.Count > 0)
                            {
                                supp.EMAIL_ADDRESS = dtSupp.Rows[0][xCSSMTDB.xCSSMT.EMAIL_ADDRESS].ToString();
                                supp.filename = filename;
                                supp.supp_name = dtSupp.Rows[0]["supplier_name"].ToString();
                                lSuppEmail.Add(supp);
                            }
                            
                        }
                        else
                        {
                            ValidateFileName vF = new ValidateFileName();   // gen log
                            vF.fileName = "PO006 before write file ";   // gen log
                            vF.recordTotal = "1";   // gen log
                            vF.Message = "(No Data DeliveryDate = " + Cm.initC.Po006DeliveryDate + " SUPPLIER_NUMBER = " + row["SUPPLIER_NUMBER"].ToString() + " re run = " + Cm.initC.PO006ReRun + ")";
                            vF.recordError = "1";

                            vPP = new ValidatePrPo();
                            vPP.Filename = "PO006";
                            vPP.Message = "(No Data DeliveryDate = " + Cm.initC.Po006DeliveryDate + " SUPPLIER_NUMBER = " + row["SUPPLIER_NUMBER"].ToString() + " re run = " + Cm.initC.PO006ReRun + ")";
                            vPP.Validate = "Error PO006-004: No Data Found "+ vPP.Message;
                            lVPr.Add(vPP);
                            cntErr++;       // gen log
                            lVfile.Add(vF);   // gen log
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
                deliveryDateLog1 = deliveryDateLog.ToString("dd MMM") + " " + deliveryDateLog.Year.ToString();
                logProcessPO006("xcustpo006", lVPr, deliveryDateLog1, lVfile,"");   // gen log
            }
            else
            {
                ValidateFileName vF = new ValidateFileName();   // gen log
                vF.fileName = "PO006 before write file ";   // gen log
                vF.recordTotal = "1";   // gen log

                vPP = new ValidatePrPo();
                vPP.Filename = "PO006";
                vPP.Message = "No Data";
                vPP.Validate = "Error PO006-001: No Data Found ";
                lVPr.Add(vPP);
                cntErr++;       // gen log
                lVfile.Add(vF);   // gen log
                deliveryDateLog1 = deliveryDateLog.ToString("dd MMM") + " " + deliveryDateLog.Year.ToString();
                logProcessPO006("xcustpo006", lVPr, deliveryDateLog1, lVfile,"no data");   // gen log
            }
            
            Cm.setConfig("Po006DeliveryDate", "sysdate");
            Cm.setConfig("PO006ReRun", "N");
            pB1.Hide();
        }
        public void logProcessPO006(String programname, List<ValidatePrPo> lVPr, String deliveryDate, List<ValidateFileName> listfile, String flag)
        {
            String line1 = "", parameter = "", programstart = "", filename = "", recordError = "", txt = "", path = "";
            int cntErr = 0, err = 0;

            String date = System.DateTime.Now.ToString("yyyy_MM_dd");
            String time = System.DateTime.Now.ToString("HH_mm_ss");

            line1 = "Program : XCUST Text File PO (ERP) to Supplier" + Environment.NewLine;
            path = Cm.getPathLogProcess(programname);
            parameter = "Parameter  " + Environment.NewLine;
            parameter += "           Path Initial :" + Cm.initC.PO006PathInitial + Environment.NewLine;
            parameter += "           Delivery Date :" + deliveryDate + Environment.NewLine;

            programstart = "Program Start : " + deliveryDate + Environment.NewLine;

            if (listfile.Count > 0)
            {
                foreach (ValidateFileName vF in listfile)
                {
                    if (flag.Equals("no data"))
                    {
                        filename += "Filename : " + vF.fileName + ", Total = " + vF.recordTotal + ", Validate pass = 0, Record Error = 1, Total Error = 1" + Environment.NewLine;
                        
                        cntErr++;
                            
                    }
                    else
                    {
                        filename += "Filename : " + vF.fileName + " " + Environment.NewLine;
                        if (int.TryParse(vF.recordError, out err))
                        {
                            if (int.Parse(vF.recordError) > 0)
                            {
                                cntErr++;
                            }
                        }
                    }
                }
            }
            if (lVPr.Count > 0)
            {
                foreach (ValidatePrPo vPr in lVPr)
                {
                    recordError += "FileName =>" + vPr.Filename + Environment.NewLine;
                    recordError += "==>" + vPr.Validate + Environment.NewLine;
                    //recordError += "     ====>Error" + vPr.Message + Environment.NewLine;
                }
            }
            //using (var stream = File.CreateText(Environment.CurrentDirectory + "\\" + programname + "_" + startdatetime.Replace("-", "_").Replace(":", "_") + ".log"))
            using (var stream = File.CreateText(path + programname + "_" + date+"_"+time + ".log"))
            {
                txt = line1;
                txt += parameter;
                txt += programstart + Environment.NewLine;
                txt += "File " + Environment.NewLine;
                txt += "--------------------------------------------------------------------------" + Environment.NewLine;
                txt += filename + Environment.NewLine;
                txt += "File Error " + Environment.NewLine;
                txt += "--------------------------------------------------------------------------" + Environment.NewLine;
                txt += recordError + Environment.NewLine;
                txt += Environment.NewLine + "==========================================================================" + Environment.NewLine;
                txt += "Total " + listfile.Count + " Files " + Environment.NewLine;
                txt += "Complete " + (listfile.Count - cntErr) + " Files " + Environment.NewLine;
                txt += "Error " + cntErr + " Files " + Environment.NewLine;
                stream.WriteLine(txt);
            }
            lVPr.Clear();
            listfile.Clear();

        }
        public String writeTextPO006(String vendor_id, String attribute2, String delivery_date, DataTable dt, DataTable dtFixLen)
        {
            //var file = Cm.initC.PO006PathInitial + "S" + vendor_id+"_R" + delivery_date.Replace("-","") + ".KFC";     //60-12-25
            //var file = Cm.initC.PO006PathInitial + vendor_id + "_R" + delivery_date.Replace("-", "") + ".KFC";
            var file = Cm.initC.PO006PathInitial + attribute2 + "_R" + delivery_date.Replace("-", "") + ".KFC";
            String Org = xCDOMTDB.selectActiveByCode(Cm.initC.ORGANIZATION_code.Trim());
            String deliveryDate1 = "";
            using (var stream = File.CreateText(file))
            {
                //String hCol01 = Cm.FixLen("S" + vendor_id, dtFixLen.Rows[0]["X_LENGTH"].ToString()," ","lpad");
                String hCol01 = Cm.FixLen(attribute2, dtFixLen.Rows[0]["X_LENGTH"].ToString(), " ", "lpad");
                String hCol02 = Cm.FixLen(delivery_date.Replace("-", ""), dtFixLen.Rows[1]["X_LENGTH"].ToString(), " ", "lpad");
                String hCol3 = Cm.FixLen(dt.Rows.Count.ToString(), dtFixLen.Rows[2]["X_LENGTH"].ToString(), "0", "rpad");
                String head = hCol01 + hCol02 + hCol3;//+System.Environment.NewLine;
                stream.WriteLine(head);
                foreach (DataRow row in dt.Rows)
                {
                    deliveryDate1 = row[xCPoTDB.xCPO.DELIVER_DATE].ToString();
                    String col01 = Cm.FixLen(row["po_number"].ToString(), dtFixLen.Rows[3]["X_LENGTH"].ToString()," ", "lpad");
                    String col02 = Cm.FixLen(row["CREATION_DATE"].ToString().Replace("-", ""), dtFixLen.Rows[4]["X_LENGTH"].ToString(), " ", "lpad");     //PO date
                    String col03 = Cm.FixLen(row["ATTRIBUTE3_L"].ToString(), dtFixLen.Rows[5]["X_LENGTH"].ToString(), "0","lpad");     //Store Code
                    String col04 = Cm.FixLen(row["DELIVER_DATE"].ToString().Replace("-", ""), dtFixLen.Rows[6]["X_LENGTH"].ToString(), " ","lpad");          //Delivery Date DELIVER_DATE
                    String col05 = "", item_code="";
                    item_code = row["ITEM_ID"].ToString();
                    col05 = xCIMTDB.selectItemCodeByItemId(Org,row["ITEM_ID"].ToString());
                    col05 = Cm.FixLen(col05, dtFixLen.Rows[7]["X_LENGTH"].ToString(), " ", "lpad");
                    String col06 = " "+Cm.FixLen(row["QUANTITY"].ToString(), dtFixLen.Rows[8]["X_LENGTH"].ToString(), "0","rpad");

                    //String col07 = xCVSMTDB.selectUOM(row["UOM_CODE"].ToString());        // comment 
                    String col07 =row["UOM_CODE"].ToString();
                    col07 = Cm.FixLen(col07, dtFixLen.Rows[9]["X_LENGTH"].ToString(), " ","lpad");

                    String csvRow = col01 + col02 + col03 + col04 + col05 + col06 + col07 ;

                    stream.WriteLine(csvRow);
                }
            }
            return file.Replace(Cm.initC.PO006PathInitial,"");
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
        public void sendEmailPO006()
        {
            foreach(XcustSupplierSiteMstTbl xcs in lSuppEmail)
            {
                sendEmailPO006(xcs.supp_name);
            }
            lSuppEmail.Clear();
        }
        public void sendEmailPO006(String vendorName)
        {
            var fromAddress = new MailAddress(Cm.initC.EmailUsername, "");
            var toAddress = new MailAddress(Cm.initC.APPROVER_EMAIL, "To Name");
            //var toAddress2 = new MailAddress("amo@iceconsulting.co.th", "To Name");
            var toAddress3 = new MailAddress("ekk@ii.co.th", "To Name");
            var toAddress4 = new MailAddress("vrw@ii.co.th", "To Name");// for test
            String fromPassword = Cm.initC.EmailPassword;
            const string subject = "test";
            DataTable dt006;
            dt006 = xCPrTDB.selectPRPO006GroupByVendor();
            if (dt006.Rows.Count <= 0) return;
            string Body = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\" + "email_regis.html");
            Body = Body.Replace("#vendorName#", vendorName);

            int port = 0;
            int.TryParse(Cm.initC.EmailPort, out port);

            var smtp1 = new SmtpClient
            {
                //Host = "smtp.office365.com",
                Host = Cm.initC.EmailHost,
                //Port = 587,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            var message = new MailMessage();
            message.From = fromAddress;
            message.Subject = "PO006 "+ vendorName;
            message.To.Add(toAddress);
            //message.To.Add(toAddress2);
            message.To.Add(toAddress3);
            message.To.Add(toAddress4);// for test
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            //MessageBox.Show(""+ @Environment.CurrentDirectory, "");
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
                    if (row["attribute2"].ToString().Equals(""))
                    {
                        continue;
                    }
                    String attr = "";
                    attr = row["attribute2"].ToString();
                    filePO = Cm.getFileinFolder(Cm.initC.PO006PathInitial, row["attribute2"].ToString());
                    if (filePO.Length > 0)
                    {
                        foreach (string aa in filePO)
                        {
                            //MessageBox.Show("Attachment " + aa, "");
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
