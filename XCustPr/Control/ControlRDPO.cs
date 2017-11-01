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
    public class ControlRDPO
    {
        static String fontName = "Microsoft Sans Serif";
        public String backColor1 = "#1E1E1E";
        public String backColor2 = "#2D2D30";
        public String foreColor1 = "#fff";
        static float fontSize9 = 9.75f;
        static float fontSize8 = 8.25f;
        public Font fV1B, fV1;
        public int tcW = 0, tcH = 0, tcWMinus = 25, tcHMinus = 70, formFirstLineX = 5, formFirstLineY = 5;

        public ConnectDB conn;

        private IniFile iniFile;
        public InitC initC;
        public XcustLinfoxPrTblDB xCLFPTDB;
        public XcustPorReqHeaderIntAllDB xCPRHIADB;
        public XcustPorReqLineIntAllDB xCPRLIADB;
        public XcustPorReqDistIntAllDB xCPRDIADB;
        public ControlRDPO()
        {
            iniFile = new IniFile(Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini");
            initC = new InitC();
            GetConfig();

            conn = new ConnectDB("kfc_po", initC);
            xCLFPTDB = new XcustLinfoxPrTblDB(conn, initC);
            xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
            xCPRLIADB = new XcustPorReqLineIntAllDB(conn);
            xCPRDIADB = new XcustPorReqDistIntAllDB(conn);

            fontSize9 = 9.75f;
            fontSize8 = 8.25f;
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);
            
        }
        public void GetConfig()
        {
            initC.PathArchive = iniFile.Read("PathArchive");    //bit
            initC.PathError = iniFile.Read("PathError");
            initC.PathInitial = iniFile.Read("PathInitial");
            initC.PathProcess = iniFile.Read("PathProcess");
            initC.portDBBIT = iniFile.Read("portDBBIT");

            initC.APPROVER_EMAIL = iniFile.Read("APPROVER_EMAIL");    //bit demo
            initC.BU_NAME = iniFile.Read("BU_NAME");
            initC.Requester = iniFile.Read("Requester");
            initC.passDBBITDemo = iniFile.Read("passDBBITDemo");
            initC.EmailPort = iniFile.Read("EmailPort");

            initC.EmailCharset = iniFile.Read("EmailCharset");      //orc master
            initC.EmailUsername = iniFile.Read("EmailUsername");
            initC.EmailPassword = iniFile.Read("EmailPassword");
            initC.EmailSMTPSecure = iniFile.Read("EmailSMTPSecure");
            initC.PathLinfox = iniFile.Read("PathLinfox");

            initC.EmailHost = iniFile.Read("EmailHost");        // orc backoffice
            initC.EmailSender = iniFile.Read("EmailSender");
            initC.FTPServer = iniFile.Read("FTPServer");
            initC.PathZipExtract = iniFile.Read("PathZipExtract");
            initC.PathZip = iniFile.Read("PathZip");

            initC.databaseDBKFCPO = iniFile.Read("databaseDBKFCPO");        // orc BIT
            initC.hostDBKFCPO = iniFile.Read("hostDBKFCPO");
            initC.userDBKFCPO = iniFile.Read("userDBKFCPO");
            initC.passDBKFCPO = iniFile.Read("passDBKFCPO");
            initC.portDBKFCPO = iniFile.Read("portDBKFCPO");
            //initC.quoLine6 = iniFile.Read("quotationline6");

            //initC.grdQuoColor = iniFile.Read("gridquotationcolor");

            //initC.HideCostQuotation = iniFile.Read("hidecostquotation");
            //if (initC.grdQuoColor.Equals(""))
            //{
            //    initC.grdQuoColor = "#b7e1cd";
            //}
            //initC.Password = regE.getPassword();
        }
        public void CreateIfMissing(String path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public String[] getFileinFolder(String path)
        {
            string[] filePaths = Directory.GetFiles(@path);
            return filePaths;
        }
        public void moveFile(String sourceFile, String destinationFile)
        {
            System.IO.File.Move(@sourceFile, @destinationFile);
        }
        private Boolean validateLinfox(DataRow row)
        {
            //row[dc].ToString().Trim()
            return true;
        }
        public void processLinfoxPOtoErpPR(String[] filePO)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            ReadText rd = new ReadText();
            String[] filePOProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;
            
            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process
            foreach (string aa in filePO)
            {
                moveFile(aa, initC.PathProcess + aa.Replace(initC.PathInitial, ""));
            }

            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_LINFOX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert XCUST_LINFOX_PR_TBL
            filePOProcess = getFileinFolder(initC.PathProcess);
            foreach (string aa in filePOProcess)
            {
                List<String> linfox = rd.ReadTextFile(aa);
                //conn.BulkToMySQL("kfc_po", linfox);       // ย้ายจาก MySQL ไป MSSQL
                xCLFPTDB.insertBluk(linfox, aa, "kfc_po");

                dt.Clear();
                //d.	จากนั้น Program จะเอาข้อมูลจาก Table XCUST_LINFOX_PR_TBL มาทำการ Validate 
                //e.กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL, XCUST_POR_REQ_LINE_INT_ALL, XCUST_POR_REQ_DIST_INT_ALL
                dt = xCLFPTDB.selectLinfox();
                foreach (DataRow row in dt.Rows)
                {
                    chk = validateLinfox(row);
                    if (chk)
                    {
                        //e.	กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL,XCUST_POR_REQ_LINE_INT_ALL ,XCUST_POR_REQ_DIST_INT_ALLและ Update Validate_flag = ‘Y’
                        insertXcustPorReqHeaderIntAll(row, date, time);

                        insertXcustPorReqLineIntAll(row, date, time);

                        insertXcustPorReqDistIntAll(row, date, time);
                        //และ Update Validate_flag = ‘Y’

                    }
                    else
                    {
                        //f.	กรณีที่ Validate ไม่ผ่าน จะะ Update Validate_flag = ‘E’ พร้อมระบุ Error Message
                    }
                }
                //g.	จากนั้นรายการที่ผ่าน Program จะอ่านค่าจาก Table XCUST_POR_REQ_HEADER_INT_ALL
                //h.  ,XCUST_POR_REQ_LINE_INT_ALL ,XCUST_POR_REQ_DIST_INT_ALL โดยมี Process_flag = ‘N’  แล้วทำการ Generate File PR Interface ตาม Format Standard
                genFilePR();


            }

        }
        private void genFilePR()
        {

        }
        private void insertXcustPorReqHeaderIntAll(DataRow row, String date, String time)
        {//row[dc].ToString().Trim().
            XcustPorReqHeaderIntAll xCPRHIA = new XcustPorReqHeaderIntAll();
            xCPRHIA.ATTRIBUTE1 = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();

            xCPRHIA.ATTRIBUTE_DATE1 = date;
            xCPRHIA.ATTRIBUTE_TIMESTAMP1 = date+" "+ time;
            xCPRHIA.Batch_ID = "";
            xCPRHIA.Description = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRHIA.ENTER_BY = "";
            xCPRHIA.import_source = "";
            xCPRHIA.ATTRIBUTE_CATEGORY = "";
            xCPRHIA.PO_NUMBER = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
            xCPRHIA.PROCESS_FLAG = "N";
            xCPRHIA.PR_APPROVER = "";
            xCPRHIA.PR_STATAUS = "";
            xCPRHIA.Requisitioning_BU = "";
            xCPRHIA.Requisition_Number = "";
            xCPRHIADB.insert(xCPRHIA);
        }
        private void insertXcustPorReqLineIntAll(DataRow row, String date, String time)
        {
            XcustPorReqLineIntAll xCPRLIA = new XcustPorReqLineIntAll();
            xCPRLIA.ATTRIBUTE1 = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRLIA.ATTRIBUTE_DATE1 = date;
            xCPRLIA.ATTRIBUTE_NUMBER1 = "";
            xCPRLIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
            xCPRLIA.Category_Name = "";
            xCPRLIA.CURRENCY_CODE = "";
            xCPRLIA.Deliver_to_Location = "";
            xCPRLIA.Deliver_to_Organization = "";
            xCPRLIA.Goods = "";
            xCPRLIA.INVENTORY = "";
            xCPRLIA.ITEM_NUMBER = row[xCLFPTDB.xCLFPT.ITEM_CODE].ToString().Trim();
            xCPRLIA.LINFOX_PR = "";
            xCPRLIA.Need_by_Date = "";
            xCPRLIA.PO_LINE_NUMBER = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRLIA.PO_NUMBER = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
            xCPRLIA.Price = "";
            xCPRLIA.PROCESS_FLAG = "N";
            xCPRLIA.Procurement_BU = "";
            xCPRLIA.PR_APPROVER = "";
            xCPRLIA.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString().Trim();
            xCPRLIA.requester = initC.Requester;
            xCPRLIA.Requisitioning_BU = initC.BU_NAME;
            xCPRLIA.Subinventory = "";
            xCPRLIA.SUPPLIER_CODE = row[xCLFPTDB.xCLFPT.SUPPLIER_CODE].ToString().Trim();
            xCPRLIA.Supplier_Site = "";
            xCPRLIADB.insert(xCPRLIA);
        }
        private void insertXcustPorReqDistIntAll(DataRow row, String date, String time)
        {
            XcustPorReqDistIntAll xCPRDIA = new XcustPorReqDistIntAll();
            xCPRDIA.ATTRIBUTE1 = "";
            xCPRDIA.ATTRIBUTE_CATEGORY = "";
            xCPRDIA.ATTRIBUTE_DATE1 = date;
            xCPRDIA.ATTRIBUTE_NUMBER1 = "";
            xCPRDIA.ATTRIBUTE_TIMESTAMP1 = date + " " + time;
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 = "";
            xCPRDIA.PO_LINE_NUMBER = row[xCLFPTDB.xCLFPT.LINE_NUMBER].ToString().Trim();
            xCPRDIA.PO_NUMBER = row[xCLFPTDB.xCLFPT.PO_NUMBER].ToString().Trim();
            xCPRDIA.PROCESS_FLAG = "N";
            xCPRDIA.Program_running = "";
            xCPRDIA.QTY = row[xCLFPTDB.xCLFPT.QTY].ToString().Trim();
            xCPRDIADB.insert(xCPRDIA);
        }
    }
}
