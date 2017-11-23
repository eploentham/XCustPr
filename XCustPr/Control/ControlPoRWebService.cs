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
    public class ControlPoRWebService
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

        public XcustPoReceiptTblDB xCPoRDB;

        public ControlPoRWebService(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            conn = new ConnectDB("kfc_po", Cm.initC);        //standard
            vPrPo = new ValidatePrPo();

            xCPoRDB = new XcustPoReceiptTblDB(conn, Cm.initC);

            fontSize9 = 9.75f;        //standard
            fontSize8 = 8.25f;        //standard
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);        //standard
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);        //standard
        }
        public void setXcustPRTbl(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            String uri = "", dump = "";
            //HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            const Int32 BufferSize = 128;
            String[] filePO;
            addListView("setXcustPRTbl ", "Web Service", lv1, form1);
            //filePO = Cm.getFileinFolder(Cm.initC.PathZip);
            //String text = System.IO.File.ReadAllText(filePO[0]);
            //byte[] byteArraytext = Encoding.UTF8.GetBytes(text);
            //byte[] toEncodeAsBytestext = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            //String Arraytext = System.Convert.ToBase64String(toEncodeAsBytestext);
            //< soapenv:Envelope xmlns:soapenv = "http://schemas	xmlsoap	org/soap/envelope/" xmlns: v2 = "http://xmlns	oracle	com/oxp/service/v2" >
            uri = @" <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:pub='http://xmlns.oracle.com/oxp/service/PublicReportService'>  " +
            "<soapenv:Header/> " +
                    "<soapenv:Body> " +
                        "<v2:runReport> " +
                            "<v2:reportRequest> " +
                                "<v2:attributeLocale>en-US</v2:attributeLocale> " +
                                "<v2:attributeTemplate>XCUST_PO_RECEIPT_REP</v2:attributeTemplate> " +
                                "<v2:reportAbsolutePath>/Custom/XCUST_CUSTOM/XCUST_PO_RECEIPT_REP.xdo</v2:reportAbsolutePath> " +
                                "<pub:parameterNameValues> " +
                                "<pub:item> " +
                                    "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                    "<pub:name>p_cre_date_frm</pub:name> " +
                                    "<pub:values> " +
                                        "<pub:item></pub:item> " +
                                    "</pub:values>" +
                                "</pub:item>" +
                                "<pub:item>" +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed>" +
                                "<pub:name>p_cre_date_to</pub:name>" +
                                "<pub:values>" +
                                "<pub:item></pub:item>" +
                                "</pub:values>" +
                                "</pub:item> " +
                                "<pub:item>" +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_update_date_frm</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_update_date_to</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_receipt_num_frm</pub:name> " +
                                "<pub:values> " +
                                "<pub:item></pub:item> " +
                                "</pub:values> " +
                                "</pub:item> " +
                                "<pub:item> " +
                                "<pub:multiValuesAllowed>False</pub:multiValuesAllowed> " +
                                "<pub:name>p_receipt_num_to</pub:name> " +
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
                            if (element.Name.LocalName.Equals("reportBytes"))
                            {
                                actNumber = element.ToString().Replace(@"<ns1:reportBytes xmlns:ns1=""http://xmlns.oracle.com/oxp/service/PublicReportService"">", "");
                                actNumber = actNumber.Replace("</reportBytes>", "").Replace("</result>", "").Replace(@"""", "").Replace("<>", "");
                                actNumber = actNumber.Replace("<reportBytes>", "").Replace("</ns1:reportBytes>", "");
                            }
                        }
                    }
                }
            }
            actNumber = actNumber.Trim();
            actNumber = actNumber.IndexOf("<reportContentType>") >= 0 ? actNumber.Substring(0, actNumber.IndexOf("<reportContentType>")) : actNumber;
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
            for (int row = 0; row < data1.Length; row++)
            {
                if (row == 0) continue;
                if (data1[row].Length <= 0) continue;

                String[] data2 = data1[row].Split(',');
                XcustPoReceiptTbl item = new XcustPoReceiptTbl();
                item.LAST_UPDATE_DATE = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[0].Trim());
                item.CREATION_DATE = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[1].Trim());
                item.RECEIPT_SOURCE_CODE = data2[2].Trim();
                item.VENDOR_ID = data2[3].Trim().Equals("")?"0": data2[3].Trim();
                item.VENDOR_SITE_ID = data2[4].Trim().Equals("") ? "0" : data2[4].Trim();
                item.ORGANIZATION_ID = data2[5].Trim().Equals("") ? "0" : data2[5].Trim();
                item.SHIPMENT_NUM = data2[6].Trim();
                item.RECEIPT_NUM = data2[7].Trim();
                item.SHIP_TO_LOCATION_ID = data2[8].Trim();
                item.PACKING_SLIP = data2[9].Trim();
                item.SHIPPED_DATE = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[10].Trim());

                item.EXPECTED_RECEIPT_DATE = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[11].Trim());
                item.ATTRIBUTE_CATEGORY = data2[12].Trim();
                item.ATTRIBUTE1 = data2[13].Trim();
                item.ATTRIBUTE2 = data2[14].Trim();
                item.ATTRIBUTE3 = data2[15].Trim();
                item.ATTRIBUTE4 = data2[16].Trim();
                item.ATTRIBUTE5 = data2[17].Trim();
                item.ATTRIBUTE6 = data2[18].Trim();
                item.ATTRIBUTE7 = data2[19].Trim();
                item.ATTRIBUTE8 = data2[20].Trim();

                item.ATTRIBUTE9 = data2[21].Trim();
                item.ATTRIBUTE10 = data2[22].Trim();
                item.REQUEST_ID = data2[23].Trim().Equals("") ? "0" : data2[23].Trim();
                item.GROSS_WEIGHT = data2[24].Trim().Equals("") ? "0" : data2[24].Trim();
                item.GROSS_WEIGHT_UOM_CODE = data2[25].Trim();
                item.NET_WEIGHT = data2[26].Trim().Equals("") ? "0" : data2[26].Trim();
                item.NET_WEIGHT_UOM_CODE = data2[27].Trim().Equals("") ? "0" : data2[27].Trim();
                item.PACKAGING_CODE = data2[28].Trim();
                item.INVOICE_NUM = data2[29].Trim();
                item.INVOICE_DATE = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[30].Trim());

                item.INVOICE_AMOUNT = data2[31].Trim().Equals("") ? "0" : data2[31].Trim();
                item.TAX_NAME = data2[32].Trim();
                item.TAX_AMOUNT = data2[33].Trim().Equals("") ? "0" : data2[33].Trim();
                item.FREIGHT_AMOUNT = data2[34].Trim().Equals("") ? "0" : data2[34].Trim();
                item.INVOICE_STATUS_CODE = data2[35].Trim();
                item.CURRENCY_CODE = data2[36].Trim();
                item.CONVERSION_RATE_TYPE = data2[37].Trim();
                item.CONVERSION_RATE = data2[38].Trim().Equals("") ? "0" : data2[38].Trim();
                item.CONVERSION_DATE = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[39].Trim());
                item.PAYMENT_TERMS_ID = data2[40].Trim().Equals("") ? "0" : data2[40].Trim();

                item.SHIP_TO_ORG_ID = data2[41].Trim().Equals("") ? "0" : data2[41].Trim();
                item.CUSTOMER_ID = data2[42].Trim().Equals("") ? "0" : data2[42].Trim();
                item.CUSTOMER_SITE_ID = data2[43].Trim().Equals("") ? "0" : data2[43].Trim();
                item.REMIT_TO_SITE_ID = data2[44].Trim().Equals("") ? "0" : data2[44].Trim();
                item.SHIP_FROM_LOCATION_ID = data2[45].Trim().Equals("") ? "0" : data2[45].Trim();     //CATEGORY
                item.APPROVAL_STATUS = data2[46].Trim();      //CONVERSION_TYPE
                item.RMA_BU_ID = data2[47].Trim().Equals("") ? "0" : data2[47].Trim();
                item.HEADER_INTERFACE_ID = data2[48].Trim().Equals("") ? "0" : data2[48].Trim();//CONVERSION_RATE
                item.RA_ORIG_SYSTEM_REF = data2[49].Trim();
                item.SHIPMENT_LINE_ID = data2[50].Trim().Equals("") ? "0" : data2[50].Trim();

                item.LINE_NUM = data2[51].Trim().Equals("") ? "0" : data2[51].Trim();
                item.CATEGORY_ID = data2[52].Trim().Equals("") ? "0" : data2[52].Trim();
                item.QUANTITY_SHIPPED = data2[53].Trim().Equals("") ? "0" : data2[53].Trim();
                item.QUANTITY_RECEIVED = data2[54].Trim().Equals("") ? "0" : data2[54].Trim();
                item.QUANTITY_DELIVERED = data2[55].Trim().Equals("") ? "0" : data2[55].Trim();
                item.QUANTITY_RETURNED = data2[56].Trim().Equals("") ? "0" : data2[56].Trim();
                item.QUANTITY_ACCEPTED = data2[57].Trim().Equals("") ? "0" : data2[57].Trim();
                item.QUANTITY_REJECTED = data2[58].Trim().Equals("") ? "0" : data2[58].Trim();
                item.UOM_CODE = data2[59].Trim();
                item.ITEM_DESCRIPTION = data2[60].Trim();

                item.ITEM_ID = data2[61].Trim().Equals("") ? "0" : data2[61].Trim();
                item.ITEM_REVISION = data2[62].Trim();
                item.SHIPMENT_LINE_STATUS_CODE = data2[63].Trim().Replace(@"""", "");
                item.SOURCE_DOCUMENT_CODE = data2[64].Trim();
                item.PO_HEADER_ID = data2[65].Trim().Equals("") ? "0" : data2[65].Trim();
                item.PO_LINE_ID = data2[66].Trim().Equals("") ? "0" : data2[66].Trim();
                item.PO_LINE_LOCATION_ID = data2[67].Trim().Equals("") ? "0" : data2[67].Trim();
                item.PO_DISTRIBUTION_ID = data2[68].Trim().Equals("") ? "0" : data2[68].Trim();
                item.REQUISITION_LINE_ID = data2[69].Trim().Equals("") ? "0" : data2[69].Trim();
                item.REQ_DISTRIBUTION_ID = data2[70].Trim().Equals("") ? "0" : data2[70].Trim();

                item.FROM_ORGANIZATION_ID = data2[71].Trim().Equals("") ? "0" : data2[71].Trim();
                item.DESTINATION_TYPE_CODE = data2[72].Trim().Equals("") ? "0" : data2[72].Trim();
                item.TO_ORGANIZATION_ID = data2[73].Trim().Equals("") ? "0" : data2[73].Trim();
                item.TO_SUBINVENTORY = data2[74].Trim();
                item.LOCATOR_ID = data2[75].Trim().Equals("") ? "0" : data2[74].Trim();
                item.DELIVER_TO_LOCATION_ID = data2[76].Trim().Equals("") ? "0" : data2[75].Trim();
                item.SHIPMENT_UNIT_PRICE = data2[77].Trim().Equals("") ? "0" : data2[76].Trim();
                item.TRANSFER_COST = data2[78].Trim().Equals("") ? "0" : data2[77].Trim();
                item.TRANSPORTATION_COST = data2[79].Trim().Equals("") ? "0" : data2[78].Trim();
                item.ATTRIBUTE_CATEGORY_L = data2[80].Trim();

                item.ATTRIBUTE1_L = data2[81].Trim();
                item.ATTRIBUTE2_L = data2[82].Trim();
                item.ATTRIBUTE3_L = data2[83].Trim();
                item.ATTRIBUTE4_L = data2[84].Trim();
                item.ATTRIBUTE5_L = data2[85].Trim();
                item.ATTRIBUTE6_L = data2[86].Trim();
                item.ATTRIBUTE7_L = data2[87].Trim();
                item.ATTRIBUTE8_L = data2[88].Trim();
                item.ATTRIBUTE9_L = data2[89].Trim();
                item.ATTRIBUTE10_L = data2[90].Trim();

                item.ATTRIBUTE_NUMBER1_L = data2[91].Trim().Equals("") ? "0" : data2[91].Trim();
                item.ATTRIBUTE_NUMBER2_L = data2[92].Trim().Equals("") ? "0" : data2[92].Trim();
                item.ATTRIBUTE_NUMBER3_L = data2[93].Trim().Equals("") ? "0" : data2[93].Trim();
                item.ATTRIBUTE_NUMBER4_L = data2[94].Trim().Equals("") ? "0" : data2[94].Trim();
                item.ATTRIBUTE_NUMBER5_L = data2[95].Trim().Equals("") ? "0" : data2[95].Trim();
                item.ATTRIBUTE_NUMBER6_L = data2[96].Trim().Equals("") ? "0" : data2[96].Trim();
                item.ATTRIBUTE_NUMBER7_L = data2[97].Trim().Equals("") ? "0" : data2[97].Trim();
                item.ATTRIBUTE_NUMBER8_L = data2[98].Trim().Equals("") ? "0" : data2[98].Trim();
                item.ATTRIBUTE_NUMBER9_L = data2[99].Trim().Equals("") ? "0" : data2[99].Trim();
                item.ATTRIBUTE_NUMBER10_L = data2[100].Trim().Equals("") ? "0" : data2[100].Trim();

                item.ATTRIBUTE_DATE1_L = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[101].Trim());
                item.ATTRIBUTE_DATE2_L = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[102].Trim());
                item.ATTRIBUTE_DATE3_L = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[103].Trim());
                item.ATTRIBUTE_DATE4_L = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[104].Trim());
                item.REASON_ID = data2[105].Trim();
                item.REQUEST_ID_L = data2[106].Trim();
                item.DESTINATION_CONTEXT = data2[107].Trim();
                item.PRIMARY_UOM_CODE = data2[108].Trim();
                item.TAX_NAME_L = data2[109].Trim();
                item.TAX_AMOUNT_L = data2[110].Trim().Equals("") ? "0" : data2[110].Trim();

                item.INVOICE_STATUS_CODE_L = data2[111].Trim();
                item.SHIP_TO_LOCATION_ID_L = data2[112].Trim().Equals("") ? "0" : data2[112].Trim();
                item.SECONDARY_QUANTITY_SHIPPED = data2[113].Trim().Equals("") ? "0" : data2[113].Trim();
                item.SECONDARY_QUANTITY_RECEIVED = data2[114].Trim().Equals("") ? "0" : data2[114].Trim();
                item.SECONDARY_UOM_CODE = data2[115].Trim();
                item.MMT_TRANSACTION_ID = data2[116].Trim().Equals("") ? "0" : data2[116].Trim();
                item.AMOUNT = data2[117].Trim().Equals("") ? "0" : data2[117].Trim();
                item.AMOUNT_RECEIVED = data2[118].Trim().Equals("") ? "0" : data2[118].Trim();
                item.ATTRIBUTE_DATE5_L = xCPoRDB.xCPoR.dateTimeYearToDB1(data2[119].Trim());
                item.LOT_NUMBER = data2[120].Trim();
                
                //int VALUE_SET_ID = 0, VALUE_SET_CODE = 1, VALUE_ID = 2, VALUE = 3, DESCRIPTION = 4, ENABLED_FLAG = 5, LAST_UPDATE_DATE = 6, CREATION_DATE = 7;

                xCPoRDB.insertxCPoR(item);
            }

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
