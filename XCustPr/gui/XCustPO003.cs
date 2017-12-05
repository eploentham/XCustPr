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
    public class XCustPO003:Form
    {
        int gapLine = 5;
        int grd0 = 0, grd1 = 100, grd2 = 240, grd3 = 320, grd4 = 570, grd5 = 700, grd51 = 700, grd6 = 820, grd7 = 900, grd8 = 1070, grd9 = 1200;
        int line1 = 35, line2 = 27, line3 = 85, line4 = 105, line41 = 120, line42 = 111, line5 = 270, ControlHeight = 21, lineGap = 5;

        int formwidth = 860, formheight = 740;

        MaterialLabel lb1, lb2;
        MaterialSingleLineTextField txtFileName;
        MaterialFlatButton btnRead, btnPrepare, btnWebService, btnFTP, btnEmail;
        MaterialListView lv1;
        MaterialProgressBar pB1;

        Color cTxtL, cTxtE, cForm;

        ControlMain Cm;
        ControlPO003 cPo003;

        private ListViewColumnSorter lvwColumnSorter;
        String[] filePO;

        public XCustPO003(ControlMain cm)
        {
            this.Size = new Size(formwidth, formheight);
            this.StartPosition = FormStartPosition.CenterScreen;
            Cm = cm;
            initConfig();
            cTxtL = txtFileName.BackColor;
            cTxtE = Color.Yellow;
            this.Text = "Last Update 2017-11-08 ";
        }
        private void initConfig()
        {
            cPo003 = new ControlPO003(Cm);
            initCompoment();
            pB1.Visible = false;
            lvwColumnSorter = new ListViewColumnSorter();
            lvwColumnSorter.Order = SortOrder.Descending;
            lvwColumnSorter.SortColumn = 0;
            lv1.Sort();
            //txtFileName.Text = cRDPO.initC.PathInitial + "PR03102017.txt";
            txtFileName.Text = Cm.initC.PO003PathInitial;

            lv1.Columns.Add("NO", 50);
            lv1.Columns.Add("List File", formwidth - 50 - 40 - 100, HorizontalAlignment.Left);
            lv1.Columns.Add("   process   ", 100, HorizontalAlignment.Center);
            lv1.ListViewItemSorter = lvwColumnSorter;
            txtFileName.Text = Cm.initC.AutoRunPO003;

            int i = 1;
            if (Cm.initC.PO003PathInitial.Equals(""))
            {
                MessageBox.Show("Path Config PO003 ไม่ถูกต้อง", "");
                disableBtn();
                return;
            }
            filePO = Cm.getFileinFolder(Cm.initC.PO003PathInitial);
            if (filePO == null)
            {
                MessageBox.Show("Folder PO003 ไม่ถูกต้อง", "");
                disableBtn();
                return;
            }
            foreach (string aa in filePO)
            {
                lv1.Items.Add(AddToList((i++), aa, ""));
                //lv1.Items.s
            }
        }
        private void disableBtn()
        {
            btnRead.Enabled = false;
            btnPrepare.Enabled = false;
            btnFTP.Enabled = false;
            btnWebService.Enabled = false;
            btnEmail.Enabled = false;
        }
        private void initCompoment()
        {
            line1 = 35 + gapLine;
            line2 = 57 + gapLine;
            line3 = 75 + gapLine;
            line4 = 125 + gapLine;
            line41 = 120 + gapLine;
            line42 = 140 + gapLine;
            line5 = 270 + gapLine;

            lb1 = new MaterialLabel();
            lb1.Font = cPo003.fV1;
            lb1.Text = "Text File";
            lb1.AutoSize = true;
            Controls.Add(lb1);
            lb1.Location = new System.Drawing.Point(cPo003.formFirstLineX, cPo003.formFirstLineY + gapLine);

            lb2 = new MaterialLabel();
            lb2.Font = cPo003.fV1;
            lb2.Text = "Program Name XcustPO003";
            lb2.AutoSize = true;
            Controls.Add(lb2);
            lb2.Location = new System.Drawing.Point(grd3, cPo003.formFirstLineY + gapLine);

            txtFileName = new MaterialSingleLineTextField();
            txtFileName.Font = cPo003.fV1;
            txtFileName.Text = "";
            txtFileName.Size = new System.Drawing.Size(300 - grd1 - 20 - 30, ControlHeight);
            Controls.Add(txtFileName);
            txtFileName.Location = new System.Drawing.Point(grd1, cPo003.formFirstLineY + gapLine);
            txtFileName.Hint = lb1.Text;
            txtFileName.Enter += txtFileName_Enter; ;
            txtFileName.Leave += txtFileName_Leave;


            btnRead = new MaterialFlatButton();
            btnRead.Font = cPo003.fV1;
            btnRead.Text = "1. mod up Read Text";
            btnRead.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnRead);
            btnRead.Location = new System.Drawing.Point(grd1, line1);
            btnRead.Click += btnRead_Click;

            btnPrepare = new MaterialFlatButton();
            btnPrepare.Font = cPo003.fV1;
            btnPrepare.Text = "2. prepare Data, zip file";
            btnPrepare.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnPrepare);
            btnPrepare.Location = new System.Drawing.Point(grd3, line1);
            btnPrepare.Click += btnPrepare_Click;

            btnWebService = new MaterialFlatButton();
            btnWebService.Font = cPo003.fV1;
            btnWebService.Text = "3. Web Service";
            btnWebService.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnWebService);
            btnWebService.Location = new System.Drawing.Point(grd4, line1);
            btnWebService.Click += btnWebService_Click;

            btnFTP = new MaterialFlatButton();
            btnFTP.Font = cPo003.fV1;
            btnFTP.Text = "4. FTP to linfox";
            btnFTP.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnFTP);
            btnFTP.Location = new System.Drawing.Point(grd5, line1);
            btnFTP.Click += btnFTP_Click;

            btnEmail = new MaterialFlatButton();
            btnEmail.Font = cPo003.fV1;
            btnEmail.Text = "5. Send email ";
            btnEmail.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnEmail);
            btnEmail.Location = new System.Drawing.Point(grd1, line3);
            btnEmail.Click += btnEmail_Click;

            pB1 = new MaterialProgressBar();
            Controls.Add(pB1);
            pB1.Size = new System.Drawing.Size(formwidth - 40, pB1.Height);
            pB1.Location = new System.Drawing.Point(cPo003.formFirstLineX + 5, line41);

            lv1 = new MaterialListView();
            lv1.Font = cPo003.fV1;
            lv1.FullRowSelect = true;
            lv1.Size = new System.Drawing.Size(formwidth - 40, formheight - line3 - 100);
            lv1.Location = new System.Drawing.Point(cPo003.formFirstLineX + 5, line42);
            lv1.FullRowSelect = true;
            lv1.View = View.Details;
            //lv1.Dock = System.Windows.Forms.DockStyle.Fill;
            lv1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            Controls.Add(lv1);
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            lv1.Items.Clear();
            filePO = Cm.getFileinFolder(Cm.initC.PO003PathInitial);
            cPo003.processLinfoxRCVPOtoErpPR(filePO, lv1, this, pB1);
            //1.ดึงข้อมูลตาม group by filename เพราะ field filename เป็นตัวแบ่งข้อมูลแต่ละfile
            //2.ดึงข้อมูล where ตาม filename เพื่อ validate ถ้า validate ผ่าน ก็ update validate_flag = 'Y'
            // e.เช็คยอด rcv qty ของระบบ Linfox ต้องไม่เกินยอด PO qty ถ้าเกินให้ Validate ไม่ผ่าน
            //f.ทำการ Matching Order PO ของ ERP กับ GR ของระบบ Linfox โดย
            //- หา PO และ PO Line โดยใช้เลข MMX PO Number และ MMX Line Number มาหาโดยใน ERP จะเก็บไว้ที่ PR Header(Attribute2) ,PR Line(Attribute1)
            //-เมื่อเจอให้ตรวจสอบยอดที่ยังไม่ได้ทำรับ ว่าเหลือพอต่อการ Receipt หรือไม่ หากไม่พอให้ Validate ไม่ผ่าน

            cPo003.processGetTempTableToValidate(lv1, this, pB1);

            cPo003.processInsertTable(lv1, this, pB1, Cm.initC.PO003PathLog);
        }
        private void btnPrepare_Click(object sender, EventArgs e)
        {
            cPo003.processGenCSV(lv1, this, pB1);
        }
        private void btnWebService_Click(object sender, EventArgs e)
        {
            String uri = "", dump = "";
            //HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            const Int32 BufferSize = 128;
            String[] filePO;
            filePO = Cm.getFileinFolder(Cm.initC.PathFileCSV);
            String text = System.IO.File.ReadAllText(filePO[0]);
            //byte[] byteArraytext = Encoding.UTF8.GetBytes(text);
            byte[] toEncodeAsBytestext = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            String Arraytext = System.Convert.ToBase64String(toEncodeAsBytestext);

            uri = @" <soapenv:Envelope xmlns:soapenv ='http://schemas.xmlsoap.org/soap/envelope/' xmlns:typ='http://xmlns.oracle.com/oracle/apps/fnd/applcore/webservices/types/' xmlns:web='http://xmlns.oracle.com/oracle/apps/fnd/applcore/webservices/'> " +
                    "<soapenv:Header/> " +
                        "<soapenv:Body> " +
                         "<typ:uploadFiletoUCM> " +
                   "<typ:document> " +
                       "<!--Optional:--> " +
                        "<web:fileName>xcustpr.zip</web:fileName> " +
                             "<!--Optional:--> " +
                              "<web:contentType>application/zip</web:contentType> " +
                                     "<!--Optional:--> " +
                                        "<web:content>" + Arraytext +
                                        "</web:content> " +
             "<!--Optional:--> " +
              "<web:documentAccount>prc$/requisition$/import$</web:documentAccount> " +
                    "<!--Optional:--> " +
                     "<web:documentTitle> amo_test_load </web:documentTitle> " +
                       "</typ:document> " +
                     "</typ:uploadFiletoUCM> " +
                   "</soapenv:Body> " +
                 "</soapenv:Envelope>";

            //byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
            byte[] byteArray = Encoding.UTF8.GetBytes(uri);

            // Construct the base 64 encoded string used as credentials for the service call
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("icetech@iceconsulting.co.th" + ":" + "icetech@2017");
            string credentials = System.Convert.ToBase64String(toEncodeAsBytes);

            // Create HttpWebRequest connection to the service
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://eglj-test.fa.us2.oraclecloud.com:443/fndAppCoreServices/FndManageImportExportFilesService?WSDL");

            // Configure the request content type to be xml, HTTP method to be POST, and set the content length
            request1.Method = "POST";
            request1.ContentType = "text/xml;charset=UTF-8";
            request1.ContentLength = byteArray.Length;

            // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
            request1.Headers.Add("Authorization", "Basic " + credentials);

            // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
            request1.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/incentiveCompensation/cn/creditSetup/creditRule/creditRuleService/findRule");

            // Write the xml payload to the request
            Stream dataStream = request1.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // Get the response and process it; In this example, we simply print out the response XDocument doc;
            string actNumber = "";
            XDocument doc;
            using (WebResponse response = request1.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    doc = XDocument.Load(stream);
                    foreach (XNode node in doc.DescendantNodes())
                    {
                        if (node is XElement)
                        {
                            XElement element = (XElement)node;
                            if (element.Name.LocalName.Equals("result"))
                            {
                                actNumber = element.ToString().Replace("http://xmlns.oracle.com/oracle/apps/fnd/applcore/webservices/types/", "");
                                actNumber = actNumber.Replace("result xmlns=", "").Replace("</result>", "").Replace(@"""", "").Replace("<>", "");
                            }
                        }
                    }
                }
            }
            Console.WriteLine(doc);
        }
        private void btnFTP_Click(object sender, EventArgs e)
        {

        }
        private void btnEmail_Click(object sender, EventArgs e)
        {

        }
        private void txtFileName_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtL;
        }

        private void txtFileName_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtE;
        }
        private ListViewItem AddToList(int col1, string col2, string col3)
        {
            string[] array = new string[3];
            array[0] = col1.ToString();
            array[1] = col2;
            array[2] = col3;

            return (new ListViewItem(array));
        }
    }
}
