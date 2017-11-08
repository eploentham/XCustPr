using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class ControlMain
    {
        public InitC initC;        //standard
        private IniFile iniFile;        //standard

        public ControlMain()
        {
            initConfig();            
        }
        private void initConfig()
        {
            iniFile = new IniFile(Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini");        //standard
            initC = new InitC();        //standard

            GetConfig();
        }
        public void createFolder(String path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public void createFolderPO001()
        {
            createFolderPO001PathProcess();
            createFolderPO001PathInitial();
            createFolderPO001PathError();
            createFolderPO001PathArchive();
        }
        public void createFolderPO004()
        {
            createFolderPO001PathProcess();
            createFolderPO001PathInitial();
            createFolderPO001PathError();
            createFolderPO001PathArchive();
        }
        public void createFolderPO005()
        {
            createFolderPO001PathProcess();
            createFolderPO001PathInitial();
            createFolderPO001PathError();
            createFolderPO001PathArchive();
        }
        public void createFolderPO001PathProcess()
        {
            bool folderExists = Directory.Exists(initC.PathProcess);
            if (!folderExists)
                Directory.CreateDirectory(initC.PathProcess);
        }
        public void createFolderPO001PathInitial()
        {
            bool folderExists = Directory.Exists(initC.PathInitial);
            if (!folderExists)
                Directory.CreateDirectory(initC.PathInitial);
        }
        public void createFolderPO001PathError()
        {
            bool folderExists = Directory.Exists(initC.PathError);
            if (!folderExists)
                Directory.CreateDirectory(initC.PathError);
        }
        public void createFolderPO001PathArchive()
        {
            bool folderExists = Directory.Exists(initC.PathArchive);
            if (!folderExists)
                Directory.CreateDirectory(initC.PathArchive);
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
        public void deleteFile(String sourceFile)
        {
            if (System.IO.File.Exists(sourceFile))
            {
                System.IO.File.Delete(@sourceFile);
            }
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
            initC.ImportSource = iniFile.Read("ImportSource");
            initC.Company = iniFile.Read("Company");
            initC.DELIVER_TO_LOCATTION = iniFile.Read("DELIVER_TO_LOCATTION");
            initC.ORGANIZATION_code = iniFile.Read("ORGANIZATION_code");
            initC.Locator = iniFile.Read("Locator");
            initC.Subinventory_Code = iniFile.Read("Subinventory_Code");
            initC.CURRENCY_CODE = iniFile.Read("CURRENCY_CODE");
            initC.PR_STATAUS = iniFile.Read("PR_STATAUS");
            initC.LINE_TYPE = iniFile.Read("LINE_TYPE");

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

            initC.AutoRunPO001 = iniFile.Read("AutoRunPO001");
            initC.AutoRunPO004 = iniFile.Read("AutoRunPO004");
            initC.AutoRunPO005 = iniFile.Read("AutoRunPO005");
            initC.PathMaster = iniFile.Read("PathMaster");


            //initC.grdQuoColor = iniFile.Read("gridquotationcolor");

            //initC.HideCostQuotation = iniFile.Read("hidecostquotation");
            //if (initC.grdQuoColor.Equals(""))
            //{
            //    initC.grdQuoColor = "#b7e1cd";
            //}
            //initC.Password = regE.getPassword();
        }
        
    }
}
