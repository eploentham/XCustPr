using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XCustPr
{
    public class ControlPRPOWebService
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

        private String dateStart = "";      //gen log

        XcustPrTblDB xCPRDB;
        XcustPoTblDB xCPODB;

        public ControlPRPOWebService(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            conn = new ConnectDB("kfc_po", Cm.initC);        //standard
            vPrPo = new ValidatePrPo();
            Cm.createFolderPOWebService();
            Cm.createFolderPRWebService();
            xCPRDB = new XcustPrTblDB(conn, Cm.initC);
            xCPODB = new XcustPoTblDB(conn, Cm.initC);

            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard
        }
        public void setXcustPRTbl(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String pathLog)
        {
            String uri = "", dump = "";
            //HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            const Int32 BufferSize = 128;
            String[] filePO;
            //DateTime currDate = System.DateTime.Now.AddDays(-1);
            DateTime currDate = System.DateTime.Now;
            String date = currDate.Month.ToString("00") + "-" + currDate.Day.ToString("00") + "-" + currDate.Year.ToString();
            addListView("setXcustPRTbl ", "Web Service", lv1, form1);
            //filePO = Cm.getFileinFolder(Cm.initC.PathZip);
            //String text = System.IO.File.ReadAllText(filePO[0]);
            //byte[] byteArraytext = Encoding.UTF8.GetBytes(text);
            //byte[] toEncodeAsBytestext = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            //String Arraytext = System.Convert.ToBase64String(toEncodeAsBytestext);
            //< soapenv:Envelope xmlns:soapenv = "http://schemas	xmlsoap	org/soap/envelope/" xmlns: v2 = "http://xmlns	oracle	com/oxp/service/v2" >
            uri = @" <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:v2='http://xmlns.oracle.com/oxp/service/v2'>  " +
            "<soapenv:Header/> " +
                    "<soapenv:Body> " +
                        "<v2:runReport> " +
                            "<v2:reportRequest> " +
                                "<v2:attributeLocale>en-US</v2:attributeLocale> " +
                                "<v2:attributeTemplate>XCUST_REQUISITION_REP</v2:attributeTemplate> " +
                                "<v2:reportAbsolutePath>/Custom/XCUST_CUSTOM/XCUST_REQUISITION_REP.xdo</v2:reportAbsolutePath> " +
                                "<pub:parameterNameValues> " +
                                "<pub:item> " +
                                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                    "<pub:name>p_creation_dte_frm</pub:name> " +
                                    "<pub:values> " +
                                        "<pub:item>" + date + "</pub:item> " +
                                    "</pub:values>" +
                                "</pub:item>" +
                                "<pub:item>" +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed>" +
                                "<pub:name>p_creation_dte_to</pub:name>" +
                                "<pub:values>" +
                                "<pub:item>" + date + "</pub:item>" +
                                "</pub:values>" +
                                "</pub:item> " +
                                "<pub:item>" +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_last_update_dte_frm </pub:name> " +
                                "<pub:values> "+      
                                "<pub:item></pub:item> "+         
                                "</pub:values> "+          
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_last_update_dte_to</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_req_no_from</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_req_no_to</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "</pub:parameterNameValues>  " +
                                "</v2:reportRequest> " +
                                "<v2:userID>icetech@iceconsulting.co.th</v2:userID> " +
                                "<v2:password>icetech@2017</v2:password> " +
                                "</v2:runReport> " +
                                "</soapenv:Body> " +
                                "</soapenv:Envelope> ";

            //byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
            byte[] byteArray = Encoding.UTF8.GetBytes(uri);
            addListView("setXcustPRTbl Start", "Web Service", lv1, form1);
            // Construct the base 64 encoded string used as credentials for the service call
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("icetech@iceconsulting.co.th" + ":" + "icetech@2017");
            string credentials = System.Convert.ToBase64String(toEncodeAsBytes);

            // Create HttpWebRequest connection to the service
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://eglj-test.fa.us2.oraclecloud.com/xmlpserver/services/PublicReportService");

            // Configure the request content type to be xml, HTTP method to be POST, and set the content length
            request1.Method = "POST";
            request1.ContentType = "text/xml;charset=UTF-8";
            request1.ContentLength = byteArray.Length;

            // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
            request1.Headers.Add("Authorization", "Basic " + credentials);

            // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
            request1.Headers.Add("SOAPAction", "https://eglj-test.fa.us2.oraclecloud.com/xmlpserver/services/PublicReportService");

            // Write the xml payload to the request
            Stream dataStream = request1.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            addListView("setXcustPRTbl Request", "Web Service", lv1, form1);
            // Get the response and process it; In this example, we simply print out the response XDocument doc;
            string actNumber = "";
            XDocument doc;
            using (WebResponse response = request1.GetResponse())
            {
                addListView("setXcustPRTbl Response", "Web Service", lv1, form1);
                using (Stream stream = response.GetResponseStream())
                {

                    doc = XDocument.Load(stream);
                    foreach (XNode node in doc.DescendantNodes())
                    {
                        if (node is XElement)
                        {
                            XElement element = (XElement)node;
                            if (element.Name.LocalName.Equals("runReportReturn"))
                            {
                                actNumber = element.ToString().Replace(@"<runReportReturn xmlns=""http://xmlns.oracle.com/oxp/service/v2"">", "");
                                actNumber = actNumber.Replace("</runReportReturn>", "").Replace("</result>", "").Replace(@"""", "").Replace("<>", "");
                                actNumber = actNumber.Replace("<reportBytes>", "").Replace("</reportBytes>", "");
                            }
                        }
                    }
                }
            }
            actNumber = actNumber.Trim();
            actNumber = actNumber.IndexOf("<reportContentType>") >= 0 ? actNumber.Substring(0,actNumber.IndexOf("<reportContentType>")) : actNumber;
            addListView("setXcustPRTbl Extract html", "Web Service", lv1, form1);
            byte[] data = Convert.FromBase64String(actNumber);
            string decodedString = Encoding.UTF8.GetString(data);
            //XElement html = XElement.Parse(decodedString);
            //string[] values = html.Descendants("table").Select(td => td.Value).ToArray();

            //int row = -1;
            //var doc1 = new HtmlAgilityPack.HtmlDocument();
            //doc1.LoadHtml(html.ToString());
            //var nodesTable = doc1.DocumentNode.Descendants("tr");
            String[] data1 = decodedString.Split('\n');
            //foreach (var nodeTr in nodesTable)
            addListView("setXcustPRTbl Extract html จำนวนข้อมูล" + (data1.Length - 1), "Web Service", lv1, form1);
            pB1.Visible = true;
            pB1.Minimum = 0;
            pB1.Maximum = data1.Length;
            for (int row = 0; row < data1.Length; row++)
            {
                try
                {
                    if (row == 0) continue;
                    if (data1[row].Length <= 0) continue;
                    pB1.Value = row;
                    String[] data2 = data1[row].Split(',');
                    XcustPrTbl item = new XcustPrTbl();
                    item.REQUISITION_NUMBER = data2[0].Trim();
                    item.REQUISITION_HEADER_ID = data2[1].Trim();
                    item.REQUISITION_LINE_ID = data2[2].Trim();
                    item.REQ_BU_ID = data2[3].Trim();
                    item.NAME = data2[4].Trim().Replace(@"""", "");
                    item.CREATION_DATE = xCPRDB.xCPR.dateYearToDB(data2[5].Trim());
                    item.FUNDS_STATUS = data2[6].Trim();
                    item.DESCRIPTION = data2[7].Trim().Replace(@"""", "");
                    item.ATTRIBUTE1 = data2[8].Trim();
                    item.ATTRIBUTE2 = data2[9].Trim();
                    item.ATTRIBUTE3 = data2[10].Trim();

                    item.ATTRIBUTE4 = data2[11].Trim();
                    item.ATTRIBUTE5 = data2[12].Trim();
                    item.ATTRIBUTE6 = data2[13].Trim();
                    item.ATTRIBUTE7 = data2[14].Trim();
                    item.ATTRIBUTE8 = data2[15].Trim();
                    item.ATTRIBUTE9 = data2[16].Trim();
                    item.ATTRIBUTE10 = data2[17].Trim();
                    item.ATTRIBUTE11 = data2[18].Trim();
                    item.ATTRIBUTE12 = data2[19].Trim();
                    item.ATTRIBUTE13 = data2[20].Trim();

                    item.ATTRIBUTE14 = data2[21].Trim();
                    item.ATTRIBUTE15 = data2[22].Trim();
                    item.ATTRIBUTE16 = data2[23].Trim();
                    item.ATTRIBUTE17 = data2[24].Trim();
                    item.ATTRIBUTE18 = data2[25].Trim();
                    item.ATTRIBUTE19 = data2[26].Trim();
                    item.ATTRIBUTE20 = data2[27].Trim();
                    item.LAST_UPDATE_DATE = data2[28].Trim();
                    item.LINE_NUMBER = data2[29].Trim();
                    item.LINE_TYPE = data2[30].Trim();

                    item.UOM_CODE = data2[31].Trim();
                    item.UNIT_PRICE = data2[32].Trim();
                    item.ITEM_ID = data2[33].Trim();
                    item.ITEM_DESCRIPTION = data2[34].Trim().Replace(@"""", "");
                    item.AMOUNT = data2[35].Trim();
                    item.CURRENCY_AMOUNT = data2[36].Trim();
                    item.PO_ORDER = data2[37].Trim();
                    item.SECONDARY_UOM_CODE = data2[38].Trim();
                    item.SECONDARY_QUANTITY = data2[39].Trim();
                    item.CREATED_BY = data2[40].Trim();

                    item.DESTINATION_TYPE_CODE = data2[41].Trim();
                    item.DESTINATION_ORGANIZATION_ID = data2[42].Trim();
                    item.SOURCE_TYPE_CODE = data2[43].Trim();
                    item.DOCUMENT_STATUS = data2[44].Trim();
                    //item.ATTRIBUTE_CATEGORY = data2[45].Trim();     //CATEGORY
                    //item.CONVERSION_DATE = data2[46].Trim();      //CONVERSION_TYPE
                    item.CONVERSION_DATE = data2[47].Trim();
                    //item.CONVERSION_DATE = data2[48].Trim();//CONVERSION_RATE
                    item.LOCATION = data2[49].Trim();
                    item.VENDOR_SITE_ID = data2[50].Trim();

                    item.VENDOR_SITE_ID = data2[51].Trim();
                    item.ATTRIBUTE_CATEGORY = data2[52].Trim();
                    item.ATTRIBUTE1_L = data2[53].Trim();
                    item.ATTRIBUTE2_L = data2[54].Trim();
                    item.ATTRIBUTE3_L = data2[55].Trim();
                    item.ATTRIBUTE_CATEGORY_L = data2[56].Trim();
                    item.BUDGET_DATE = data2[57].Trim().Replace(@"""", "");
                    item.FUNDS_STATUS = data2[58].Trim();
                    item.REQUISITION_LINE_ID = data2[59].Trim();
                    item.PERCENT = data2[60].Trim();

                    item.DISTRIBUTION_CURRENCY_AMOUNT = data2[61].Trim();
                    item.DISTRIBUTION = data2[62].Trim();
                    item.CHARGE_ACCOUNT = data2[63].Trim();

                    //int VALUE_SET_ID = 0, VALUE_SET_CODE = 1, VALUE_ID = 2, VALUE = 3, DESCRIPTION = 4, ENABLED_FLAG = 5, LAST_UPDATE_DATE = 6, CREATION_DATE = 7;

                    xCPRDB.insertxCPR(item, pathLog);
                }
                catch(Exception ex)
                {

                }
                
            }
            pB1.Visible = false;
            Console.WriteLine(decodedString);
        }

        // ถ้าเจอ gen_out_bound = 'Y' ห้าม delete และ ไม่ต้อง insert เพิ่ม
        public void setXcustPOTbl(MaterialListView lv1, Form form1, MaterialProgressBar pB1, String pathLog)
        {
            String uri = "", dump = "";
            //HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            const Int32 BufferSize = 128;
            String[] filePO;
            addListView("setXcustPOTbl ", "Web Service", lv1, form1);
            //DateTime currDate = System.DateTime.Now.AddDays(-1);
            DateTime currDate = System.DateTime.Now;
            String date = currDate.Month.ToString("00")+"-"+currDate.Day.ToString("00")+"-"+currDate.Year.ToString();
            //date = "";
            //filePO = Cm.getFileinFolder(Cm.initC.PathZip);
            //String text = System.IO.File.ReadAllText(filePO[0]);
            //byte[] byteArraytext = Encoding.UTF8.GetBytes(text);
            //byte[] toEncodeAsBytestext = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            //String Arraytext = System.Convert.ToBase64String(toEncodeAsBytestext);
            //< soapenv:Envelope xmlns:soapenv = "http://schemas	xmlsoap	org/soap/envelope/" xmlns: v2 = "http://xmlns	oracle	com/oxp/service/v2" >
            uri = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:v2='http://xmlns.oracle.com/oxp/service/v2'>    " +
            "<soapenv:Header/> " +
                    "<soapenv:Body> " +
                        "<v2:runReport> " +
                            "<v2:reportRequest> " +
                                "<v2:attributeLocale>en-US</v2:attributeLocale> " +
                                "<v2:attributeTemplate>XCUST_PURCHASE_REP</v2:attributeTemplate> " +
                                "<v2:reportAbsolutePath>/Custom/XCUST_CUSTOM/XCUST_PURCHASE_REP.xdo</v2:reportAbsolutePath> " +
                                "<pub:parameterNameValues> " +
                                "<pub:item> " +
                                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                    "<pub:name>p_po_num_from</pub:name> " +
                                    "<pub:values> " +
                                        "<pub:item></pub:item> " +
                                    "</pub:values>" +
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_po_num_to</pub:name> " +
                                "<pub:values>" +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item>  " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_last_update_date_frm</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_last_update_date_to</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item>  " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_creation_dte_frm</pub:name> " +
                                "<pub:values> " +
                                "<pub:item>" + date + "</pub:item> " +
                                "</pub:values> " +
                                "</pub:item>  " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_creation_date_to</pub:name> " +
                                "<pub:values> " +
                                "<pub:item>" + date + "</pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "</pub:parameterNameValues>   " +
                                "</v2:reportRequest> " +
                                "<v2:userID>icetech@iceconsulting.co.th</v2:userID> " +
                                "<v2:password>icetech@2017</v2:password> " +
                                "</v2:runReport> " +
                                "</soapenv:Body> " +
                                "</soapenv:Envelope> ";
            //uri = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:v2='http://xmlns.oracle.com/oxp/service/v2'>    " +
            //"<soapenv:Header/> " +
            //        "<soapenv:Body> " +
            //            "<v2:runReport> " +
            //                "<v2:reportRequest> " +
            //                    "<v2:attributeLocale>en-US</v2:attributeLocale> " +
            //                    "<v2:attributeTemplate>XCUST_PURCHASE_REP</v2:attributeTemplate> " +
            //                    "<v2:reportAbsolutePath>/Custom/XCUST_CUSTOM/XCUST_PURCHASE_REP.xdo</v2:reportAbsolutePath> " +
            //                    "<pub:parameterNameValues> " +
            //                    "<pub:item> " +
            //                        "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
            //                        "<pub:name>p_po_num_from</pub:name> " +
            //                        "<pub:values> " +
            //                            "<pub:item></pub:item> " +
            //                        "</pub:values>" +
            //                    "</pub:item> " +
            //                    "<pub:item> " +
            //                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
            //                    "<pub:name>p_po_num_to</pub:name> " +
            //                    "<pub:values>" +
            //                    "<pub:item></pub:item> " +
            //                    "</pub:values> " +
            //                    "</pub:item>  " +
            //                    "<pub:item> " +
            //                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
            //                    "<pub:name>p_last_update_date_frm</pub:name> " +
            //                    "<pub:values> " +
            //                    "<pub:item></pub:item> " +
            //                    "</pub:values> " +
            //                    "</pub:item> " +
            //                    "<pub:item> " +
            //                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
            //                    "<pub:name>p_last_update_date_to</pub:name> " +
            //                    "<pub:values> " +
            //                    "<pub:item></pub:item> " +
            //                    "</pub:values> " +
            //                    "</pub:item>  " +
            //                    "<pub:item> " +
            //                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
            //                    "<pub:name>p_creation_dte_frm</pub:name> " +
            //                    "<pub:values> " +
            //                    "<pub:item></pub:item> " +
            //                    "</pub:values> " +
            //                    "</pub:item>  " +
            //                    "<pub:item> " +
            //                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
            //                    "<pub:name>p_creation_date_to</pub:name> " +
            //                    "<pub:values> " +
            //                    "<pub:item></pub:item> " +
            //                    "</pub:values> " +
            //                    "</pub:item> " +
            //                    "</pub:parameterNameValues>   " +
            //                    "</v2:reportRequest> " +
            //                    "<v2:userID>icetech@iceconsulting.co.th</v2:userID> " +
            //                    "<v2:password>icetech@2017</v2:password> " +
            //                    "</v2:runReport> " +
            //                    "</soapenv:Body> " +
            //                    "</soapenv:Envelope> ";

            //byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
            byte[] byteArray = Encoding.UTF8.GetBytes(uri);
            addListView("setXcustPOTbl Start", "Web Service", lv1, form1);
            // Construct the base 64 encoded string used as credentials for the service call
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("icetech@iceconsulting.co.th" + ":" + "icetech@2017");
            string credentials = System.Convert.ToBase64String(toEncodeAsBytes);

            // Create HttpWebRequest connection to the service
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://eglj-test.fa.us2.oraclecloud.com/xmlpserver/services/PublicReportService");

            // Configure the request content type to be xml, HTTP method to be POST, and set the content length
            request1.Method = "POST";
            request1.ContentType = "text/xml;charset=UTF-8";
            request1.ContentLength = byteArray.Length;

            // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
            request1.Headers.Add("Authorization", "Basic " + credentials);

            // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
            request1.Headers.Add("SOAPAction", "https://eglj-test.fa.us2.oraclecloud.com/xmlpserver/services/PublicReportService");

            // Write the xml payload to the request
            Stream dataStream = request1.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            addListView("setXcustPOTbl Request", "Web Service", lv1, form1);
            // Get the response and process it; In this example, we simply print out the response XDocument doc;
            string actNumber = "";
            XDocument doc;
            using (WebResponse response = request1.GetResponse())
            {
                addListView("setXcustPOTbl Response", "Web Service", lv1, form1);
                using (Stream stream = response.GetResponseStream())
                {

                    doc = XDocument.Load(stream);
                    foreach (XNode node in doc.DescendantNodes())
                    {
                        if (node is XElement)
                        {
                            XElement element = (XElement)node;
                            if (element.Name.LocalName.Equals("reportBytes"))
                            {
                                actNumber = element.ToString().Replace(@"<reportBytes xmlns=""http://xmlns.oracle.com/oxp/service/v2"">", "");
                                actNumber = actNumber.Replace("</reportBytes>", "").Replace("</result>", "").Replace(@"""", "").Replace("<>", "");
                                actNumber = actNumber.Replace("<reportBytes>", "").Replace("</reportBytes>", "");
                            }
                        }
                    }
                }
            }
            actNumber = actNumber.Trim();
            actNumber = actNumber.IndexOf("<reportContentType>") >= 0 ? actNumber.Substring(0, actNumber.IndexOf("<reportContentType>")) : actNumber;
            addListView("setXcustPOTbl Extract html", "Web Service", lv1, form1);
            byte[] data = Convert.FromBase64String(actNumber);
            string decodedString = Encoding.UTF8.GetString(data);
            //XElement html = XElement.Parse(decodedString);
            //string[] values = html.Descendants("table").Select(td => td.Value).ToArray();

            //int row = -1;
            //var doc1 = new HtmlAgilityPack.HtmlDocument();
            //doc1.LoadHtml(html.ToString());
            //var nodesTable = doc1.DocumentNode.Descendants("tr");
            String[] data1 = decodedString.Split('\n');
            //foreach (var nodeTr in nodesTable)
            addListView("setXcustPOTbl Extract html จำนวนข้อมูล" + (data1.Length-1), "Web Service", lv1, form1);
            pB1.Visible = true;
            pB1.Minimum = 0;
            pB1.Maximum = data1.Length;
            for (int row = 0; row < data1.Length; row++)
            {
                try
                {
                    if (row == 0) continue;
                    if (data1[row].Length <= 0) continue;
                    pB1.Value = row;
                    String[] data2 = data1[row].Split(',');
                    XcustPoTbl item = new XcustPoTbl();
                    item.PRC_BU = data2[0].Trim().Replace(@"""", "");
                    item.PO_HEADER_ID = data2[1].Trim();
                    item.PRC_BU_ID = data2[2].Trim();
                    item.REQ_BU = data2[3].Trim().Replace(@"""", "");
                    item.REQ_BU_ID = data2[4].Trim().Replace(@"""", "");
                    item.SOLDTO_LE = data2[5].Trim().Replace(@"""", "");
                    item.PO_LINE_ID = data2[6].Trim();
                    item.SOLDTO_LE_ID = data2[7].Trim().Replace(@"""", "");
                    item.BILLTO_BU = data2[8].Trim().Replace(@"""", "");
                    item.BILLTO_BU_ID = data2[9].Trim();
                    item.SEGMENT1 = data2[10].Trim();

                    item.DOCUMENT_STATUS = data2[11].Trim().Replace(@"""", "");
                    item.BUYER = data2[12].Trim();
                    item.CREATION_DATE = xCPODB.xCPO.dateTimeYearToDB(data2[13].Trim().Replace(@"""", ""));
                    item.AGENT_ID = data2[14].Trim();
                    item.TYPE_LOOKUP_CODE = data2[15].Trim();
                    item.LAST_UPDATED_BY = data2[16].Trim();
                    item.SHIP_TO_LOCATION_ID = data2[17].Trim();
                    item.BILL_TO_LOCATION_ID = data2[18].Trim();
                    item.CURRENCY_CODE = data2[19].Trim();
                    item.REVISION_NUM = data2[20].Trim();

                    item.REVISED_DATE = data2[21].Trim();
                    item.APPROVED_FLAG = data2[22].Trim();
                    item.APPROVED_DATE = data2[23].Trim();
                    item.ATTRIBUTE_CATEGORY = data2[24].Trim();
                    item.ATTRIBUTE1 = data2[25].Trim();
                    item.ATTRIBUTE2 = data2[26].Trim();
                    item.ATTRIBUTE3 = data2[27].Trim();
                    item.ATTRIBUTE4 = data2[28].Trim();
                    //item.LINE_NUMBER = data2[29].Trim();        //ATTRIBUTE5
                    item.VENDOR_CONTACT_ID = data2[30].Trim();

                    item.SUPPLIER_NOTIF_METHOD = data2[31].Trim();
                    item.EMAIL_ADDRESS = data2[32].Trim();
                    item.SUPP_NAME = data2[33].Trim().Replace(@"""", "");
                    item.VENDOR_SITE = data2[34].Trim().Replace(@"""", "");
                    item.PAYMENT_TERM = data2[35].Trim().Replace(@"""", "");
                    item.LINE_TYPE_ID = data2[36].Trim();
                    item.LINE_STATUS = data2[37].Trim().Replace(@"""", "");
                    item.LINE_NUM = data2[38].Trim();
                    item.CATEGORY_NM = data2[39].Trim();
                    item.ITEM_ID = data2[40].Trim();

                    item.ITEM_DESCRIPTION = data2[41].Trim().Replace(@"""", "");
                    item.UOM_CODE = data2[42].Trim();
                    item.QUANTITY = data2[43].Trim();
                    item.UNIT_PRICE = data2[44].Trim();
                    item.VENDOR_ID = data2[45].Trim();
                    item.VENDOR_SITE = data2[46].Trim();
                    item.REQUISITION_HEADER_ID = data2[47].Trim();
                    item.REQUISITION_LINE_ID = data2[48].Trim();
                    item.ATTRIBUTE_CATEGORY_L = data2[49].Trim();       //ATTRIBUTE_CATEGORY_L
                    item.ATTRIBUTE1_L = data2[50].Trim();     //ATTRIBUTE1_L

                    item.ATTRIBUTE2_L = data2[51].Trim();     //ATTRIBUTE2_L
                    item.ATTRIBUTE3_L = data2[52].Trim();     //ATTRIBUTE3_L
                    item.TAX_CODE = data2[53].Trim();
                    item.TAX_AMOUNT = data2[54].Trim();
                    item.ACC_SEGMENT1 = data2[55].Trim();
                    item.ACC_SEGMENT2 = data2[56].Trim();
                    item.ACC_SEGMENT3 = data2[57].Trim();
                    item.ACC_SEGMENT4 = data2[58].Trim();
                    item.ACC_SEGMENT5 = data2[59].Trim();

                    item.ACC_SEGMENT6 = data2[60].Trim();
                    item.COUNT_AP_INVOICE = data2[61].Trim();
                    item.DESTINATION_TYPE = data2[64].Trim();
                    item.DELIVER_TO_LOC = data2[65].Trim();
                    item.PRODUCT_TYPE = data2[66].Trim();
                    item.ASSESSABLE_VALUE = data2[67].Trim().Replace("|#",",");
                    item.DELIVER_TO_LOC_LINFOX = data2[68].Trim();
                    item.DELIVER_QTY = data2[69].Trim();
                    item.DELIVER_DATE = xCPODB.xCPO.dateYearToDB(data2[70].Trim());
                    if (item.ASSESSABLE_VALUE.ToLower().Equals("goods"))
                    {
                        dump = "";
                    }
                    if (item.ASSESSABLE_VALUE.ToLower().Equals("services"))
                    {
                        dump = "";
                    }
                    //int VALUE_SET_ID = 0, VALUE_SET_CODE = 1, VALUE_ID = 2, VALUE = 3, DESCRIPTION = 4, ENABLED_FLAG = 5, LAST_UPDATE_DATE = 6, CREATION_DATE = 7;
                    if (item.PO_HEADER_ID.Equals("12988051"))
                    {
                        dump = "";
                    }
                    xCPODB.insertxCPR(item, pathLog);
                }
                catch (Exception ex)
                {

                }
            }
            pB1.Visible = false;
            Console.WriteLine(decodedString);
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
