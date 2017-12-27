using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XCustPr
{
    public class ImportExportService
    {
        public String WebServiceUrl = "";
        public String SoapAction = "";
        public String Username = "wwp-procurement";
        public String Password = "superice1";
        public ImportExportService()
        {

        }
        public String uploadFiletoUCM(byte[] FileContent, String FileName, String JobName, String ParameterList)
        {
            String uri = "";
            String Arraytext = System.Convert.ToBase64String(FileContent);

            uri = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:typ='http://xmlns.oracle.com/apps/financials/commonModules/shared/model/erpIntegrationService/types/' xmlns:erp='http://xmlns.oracle.com/apps/financials/commonModules/shared/model/erpIntegrationService/'> ";
            uri += "<soapenv:Header/>";
            uri += "<soapenv:Body>";
            uri += "<typ:importBulkData>";

            uri += "<typ:document>";
            uri += "<erp:Content>" + Arraytext + "</erp:Content> ";
            uri += "<erp:FileName>" + FileName + ".zip</erp:FileName> ";
            uri += "<erp:ContentType></erp:ContentType> ";
            uri += "<erp:DocumentTitle>" + FileName + "</erp:DocumentTitle> ";
            uri += "<erp:DocumentAuthor></erp:DocumentAuthor> ";
            uri += "<erp:DocumentSecurityGroup></erp:DocumentSecurityGroup> ";
            uri += "<erp:DocumentAccount></erp:DocumentAccount> ";
            uri += "<erp:DocumentName></erp:DocumentName> ";
            uri += "<erp:DocumentId></erp:DocumentId> ";
            uri += "</typ:document> ";

            uri += "<typ:jobDetails> ";
            uri += "<erp:JobName>" + JobName + "</erp:JobName> ";
            uri += "<erp:ParameterList>" + ParameterList + "</erp:ParameterList> ";
            uri += "<erp:JobRequestId></erp:JobRequestId> ";
            uri += "</typ:jobDetails> ";

            uri += "<typ:notificationCode></typ:notificationCode> ";
            uri += "<typ:callbackURL></typ:callbackURL> ";
            uri += "<typ:jobOptions></typ:jobOptions> ";

            uri += "</typ:importBulkData> ";
            uri += "</soapenv:Body>";
            uri += "</soapenv:Envelope>";



            byte[] byteArray = Encoding.UTF8.GetBytes(uri);
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(Username + ":" + Password);
            string credentials = System.Convert.ToBase64String(toEncodeAsBytes);

            // Create HttpWebRequest connection to the service
            //String WebServiceUrl = "";
            WebServiceUrl = "https://eglj-test.fa.us2.oraclecloud.com/publicFinancialCommonErpIntegration/ErpIntegrationService?WSDL";
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(WebServiceUrl);
            request1.Method = "POST";
            request1.ContentType = "text/xml;charset=UTF-8";
            request1.Accept = "text/xml";
            request1.ContentLength = byteArray.Length;

            // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
            request1.Headers.Add("Authorization", "Basic " + credentials);
            request1.Headers.Add("SOAPAction", SoapAction);

            // Write the xml payload to the request
            Stream dataStream = request1.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // Get the response and process
            String soapResult = "";
            String ResultId = "";
            try
            {
                WebResponse response = request1.GetResponse();
                StreamReader rd = new StreamReader(response.GetResponseStream());

                while (!rd.EndOfStream)
                {
                    String line = rd.ReadLine();
                    if (line.ToLower().Contains("result"))
                    {
                        soapResult = line;
                    }
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(soapResult);

                XmlNodeList nList = doc.GetElementsByTagName("result");
                ResultId = nList[0].InnerText;
            }
            catch (Exception ex) { }

            return ResultId;
        }


    }

}
