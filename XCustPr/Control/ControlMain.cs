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
        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();

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

            initC.PO005PathArchive = iniFile.Read("PO005PathArchive");    //bit
            initC.PO005PathError = iniFile.Read("PO005PathError");
            initC.PO005PathInitial = iniFile.Read("PO005PathInitial");
            initC.PO005PathProcess = iniFile.Read("PO005PathProcess");
            initC.PO005ImportSource = iniFile.Read("PO005ImportSource");

            //initC.grdQuoColor = iniFile.Read("gridquotationcolor");

            //initC.HideCostQuotation = iniFile.Read("hidecostquotation");
            //if (initC.grdQuoColor.Equals(""))
            //{
            //    initC.grdQuoColor = "#b7e1cd";
            //}
            //initC.Password = regE.getPassword();
        }
        /*
         * check qty ว่า data type ถูกต้องไหม
         * ที่ใช้ int.tryparse เพราะ ใน database เป็น decimal(18,0)
         * Error PO001-006 : Invalid data type
         */
        public Boolean validateQTY(String qty)
        {
            Boolean chk = false;
            int i = 0;
            chk = int.TryParse(qty, out i);
            return chk;
        }
        public String dateYearShortToDB(String date)
        {
            String chk = "", year = "", month="", day="";

            year = date.Substring(date.Length - 2);
            day = date.Substring(4, 2);
            month = date.Substring(0, 2);

            chk = "20" + year + "-" + month + "-" + day;

            return chk;
        }
        public Boolean validateDate(String date)
        {
            Boolean chk = false;
            if (date.Length == 8)
            {
                sYear.Clear();
                sMonth.Clear();
                sDay.Clear();
                try
                {
                    sYear.Append(date.Substring(0, 4));
                    sMonth.Append(date.Substring(4, 2));
                    sDay.Append(date.Substring(6, 2));
                    if ((int.Parse(sYear.ToString()) > 2000) && (int.Parse(sYear.ToString()) < 2100))
                    {
                        if ((int.Parse(sMonth.ToString()) >= 1) && (int.Parse(sMonth.ToString()) <= 12))
                        {
                            if ((int.Parse(sDay.ToString()) >= 1) && (int.Parse(sDay.ToString()) <= 31))
                            {
                                chk = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    chk = false;
                }
                finally
                {

                }

            }
            else
            {
                chk = false;
            }
            return chk;
        }
        public String validateSubInventoryCode(String ordId, String StoreCode, List<XcustSubInventoryMstTbl> listXcSIMT)
        {
            String chk = "";
            foreach (XcustSubInventoryMstTbl item in listXcSIMT)
            {
                if (item.ORGAINZATION_ID.Equals(ordId.Trim()))
                {
                    if (item.attribute1.Equals(StoreCode.Trim()))
                    {
                        chk = item.SECONDARY_INVENTORY_NAME;
                        break;
                    }
                }
            }
            return chk;
        }
        public Boolean validateItemCodeByOrgRef(String ordId, String item_code, List<XcustItemMstTbl> listXcIMT)
        {
            Boolean chk = false;
            foreach (XcustItemMstTbl item in listXcIMT)
            {
                if (item.ORGAINZATION_ID.Equals(ordId.Trim()))
                {
                    if (item.ITEM_REFERENCE1.Equals(item_code.Trim()))
                    {
                        chk = true;
                        break;
                    }
                }
            }
            return chk;
        }
        public Boolean validateSupplierBySupplierCode(String supplier_code, List<XcustSupplierMstTbl> listXcSMT)
        {
            Boolean chk = false;
            foreach (XcustSupplierMstTbl item in listXcSMT)
            {
                if (item.SUPPLIER_NUMBER.Equals(supplier_code.Trim()))
                {
                    chk = true;
                    break;
                }
            }
            return chk;
        }
        public Boolean validateUOMCodeByUOMCode(String uomCode, List<XcustUomMstTbl> listXcUMT)
        {
            Boolean chk = false;
            foreach (XcustUomMstTbl item in listXcUMT)
            {
                if (item.UOM_CODE.Equals(uomCode.Trim()))
                {
                    chk = true;
                    break;
                }
            }
            return chk;
        }
        public Boolean validateValueBySegment1(String valuesetcode, String enableflag, String value,List<XcustValueSetMstTbl> listXcVSMT)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        public Boolean validateValueBySegment2(String valuesetcode, String enableflag, String value, List<XcustValueSetMstTbl> listXcVSMT)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        public Boolean validateValueBySegment3(String valuesetcode, String enableflag, String value, List<XcustValueSetMstTbl> listXcVSMT)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        public Boolean validateValueBySegment4(String valuesetcode, String enableflag, String value, List<XcustValueSetMstTbl> listXcVSMT)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        public Boolean validateValueBySegment5(String valuesetcode, String enableflag, String value, List<XcustValueSetMstTbl> listXcVSMT)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }
        public Boolean validateValueBySegment6(String valuesetcode, String enableflag, String value, List<XcustValueSetMstTbl> listXcVSMT)
        {
            Boolean chk = false;
            foreach (XcustValueSetMstTbl item in listXcVSMT)
            {
                if (item.VALUE_SET_CODE.Equals(valuesetcode.Trim()))
                {
                    if (item.ENABLED_FLAG.Equals(enableflag.Trim()))
                    {
                        if (item.VALUE.Equals(value.Trim()))
                        {
                            chk = true;
                            break;
                        }
                    }
                }
            }
            return chk;
        }

    }
}
