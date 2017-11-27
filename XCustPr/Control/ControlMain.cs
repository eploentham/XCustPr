using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XCustPr
{
    public class ControlMain
    {
        public InitC initC;        //standard
        private IniFile iniFile;        //standard
        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();
        public string[] args;
        public String xcustpowebservice_run="", xcustprwebservice_run="";

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
        public void setAgrument()
        {
            if (args == null) return;
            foreach (String arg in args)
            {
                String[] aaa = arg.Split('=');
                if (aaa.Length > 1)
                {
                    //MessageBox.Show("arg[0] " + aaa[0]+"    arg[1] " + aaa[1], "arg[1] "+ aaa[1]);
                    xcustpowebservice_run = aaa[0].Equals("xcustpowebservice_run") ? aaa[1] : "";   //xcustpowebservice_run
                    xcustprwebservice_run = aaa[0].Equals("xcustprwebservice_run") ? aaa[1] : "";
                }
            }
        }
        public void createFolder(String path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public void createFolderPO001()
        {
            if (initC.PathError.Equals(""))
            {
                MessageBox.Show("Path PO001PathError empty", "createFolderPO001");
                return;
            }
            if (initC.PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO001PathInitial empty", "createFolderPO001");
                return;
            }
            if (initC.PathProcess.Equals(""))
            {
                MessageBox.Show("Path PO001PathProcess empty", "createFolderPO001");
                return;
            }
            if (initC.PathArchive.Equals(""))
            {
                MessageBox.Show("Path PO001PathArchive empty", "createFolderPO001");
                return;
            }
            if (initC.PathZip.Equals(""))
            {
                MessageBox.Show("Path PO001PathZip empty", "createFolderPO001");
                return;
            }
            createFolderPO001PathProcess();
            createFolderPO001PathInitial();
            createFolderPO001PathError();
            createFolderPO001PathArchive();
            createFolderPO001PathZip();
        }
        public void createFolderPO004()
        {
            if (initC.PO004PathError.Equals(""))
            {
                MessageBox.Show("Path PO004PathError empty", "createFolderPO004");
                return;
            }
            if (initC.PO004PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO004PathInitial empty", "createFolderPO004");
                return;
            }
            if (initC.PO004PathProcess.Equals(""))
            {
                MessageBox.Show("Path PO004PathProcess empty", "createFolderPO004");
                return;
            }
            //createFolder(initC.PO004PathArchive);
            createFolder(initC.PO004PathError);
            createFolder(initC.PO004PathInitial);
            createFolder(initC.PO004PathProcess);
        }
        public void createFolderPO005()
        {
            if (initC.PO005PathError.Equals(""))
            {
                MessageBox.Show("Path PO005PathError empty", "createFolderPO005");
                return;
            }
            if (initC.PO005PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO005PathInitial empty", "createFolderPO005");
                return;
            }
            if (initC.PO005PathProcess.Equals(""))
            {
                MessageBox.Show("Path PO005PathProcess empty", "createFolderPO005");
                return;
            }
            if (initC.PO005PathArchive.Equals(""))
            {
                MessageBox.Show("Path PO005PathArchive empty", "createFolderPO005");
                return;
            }
            if (initC.PO005pathZip.Equals(""))
            {
                MessageBox.Show("Path PO005PathZip empty", "createFolderPO005");
                return;
            }
            createFolder(initC.PO005PathArchive);
            createFolder(initC.PO005PathError);
            createFolder(initC.PO005PathInitial);
            createFolder(initC.PO005PathProcess);
            createFolder(initC.PO005pathZip);
        }
        public void createFolderPO003()
        {
            if (initC.PO003PathError.Equals(""))
            {
                MessageBox.Show("Path PO003PathError empty", "createFolderPO003");
                return;
            }
            if (initC.PO003PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO003PathInitial empty", "createFolderPO003");
                return;
            }
            if (initC.PO003PathProcess.Equals(""))
            {
                MessageBox.Show("Path PO003PathProcess empty", "createFolderPO003");
                return;
            }
            if (initC.PO003PathArchive.Equals(""))
            {
                MessageBox.Show("Path PO003PathArchive empty", "createFolderPO003");
                return;
            }
            createFolder(initC.PO003PathArchive);
            createFolder(initC.PO003PathError);
            createFolder(initC.PO003PathInitial);
            createFolder(initC.PO003PathProcess);
        }
        public void createFolderPO008()
        {
            if (initC.PO008PathError.Equals(""))
            {
                MessageBox.Show("Path PO008PathError empty", "createFolderPO008");
                return;
            }
            if (initC.PO008PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO008PathInitial empty", "createFolderPO008");
                return;
            }
            if (initC.PO008PathProcess.Equals(""))
            {
                MessageBox.Show("Path PO008PathProcess empty", "createFolderPO008");
                return;
            }
            if (initC.PO008PathArchive.Equals(""))
            {
                MessageBox.Show("Path PO003PathArchive empty", "createFolderPO008");
                return;
            }
            createFolder(initC.PO008PathArchive);
            createFolder(initC.PO008PathError);
            createFolder(initC.PO008PathInitial);
            createFolder(initC.PO008PathProcess);
        }
        public void createFolderPO007()
        {
            if (initC.PO007PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO007PathInitial empty", "createFolderPO007");
                return;
            }
            createFolder(initC.PO007PathInitial);
        }
        public void createFolderPO006()
        {
            if (initC.PO006PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO006PathInitial empty", "createFolderPO006");
                return;
            }
            createFolder(initC.PO006PathInitial);
        }
        public void createFolderPO002()
        {
            if (initC.PO002PathInitial.Equals(""))
            {
                MessageBox.Show("Path PO002PathInitial empty", "createFolderPO002");
                return;
            }
            if (initC.PO002PathDestinaion.Equals(""))
            {
                MessageBox.Show("Path PO002PathDestinaion empty", "createFolderPO002");
                return;
            }
            createFolder(initC.PO002PathInitial);
            createFolder(initC.PO002PathDestinaion);
        }
        public void createFolderAP001()
        {
            if (initC.AP001PathInitial.Equals(""))
            {
                MessageBox.Show("Path AP001PathInitial empty", "createFolderAP001");
                return;
            }
            if (initC.AP001PathProcess.Equals(""))
            {
                MessageBox.Show("Path AP001PathProcess empty", "createFolderAP001");
                return;
            }
            if (initC.AP001PathError.Equals(""))
            {
                MessageBox.Show("Path AP001PathError empty", "createFolderAP001");
                return;
            }
            if (initC.AP001PathArchive.Equals(""))
            {
                MessageBox.Show("Path AP001PathArchive empty", "createFolderAP001");
                return;
            }
            createFolder(initC.AP001PathInitial);
            createFolder(initC.AP001PathProcess);
            createFolder(initC.AP001PathError);
            createFolder(initC.AP001PathArchive);
        }
        public void createFolderAP004()
        {
            if (initC.AP004PathInitial.Equals(""))
            {
                MessageBox.Show("Path AP004PathInitial empty", "createFolderAP004");
                return;
            }
            if (initC.AP004PathProcess.Equals(""))
            {
                MessageBox.Show("Path AP004PathProcess empty", "createFolderAP004");
                return;
            }
            if (initC.AP004PathError.Equals(""))
            {
                MessageBox.Show("Path AP004PathError empty", "createFolderAP004");
                return;
            }
            if (initC.AP004PathArchive.Equals(""))
            {
                MessageBox.Show("Path AP004PathArchive empty", "createFolderAP004");
                return;
            }
            if (initC.AP004ImportSource.Equals(""))
            {
                MessageBox.Show("Path AP004ImportSource empty", "createFolderAP004");
                return;
            }
            createFolder(initC.AP004PathInitial);
            createFolder(initC.AP004PathProcess);
            createFolder(initC.AP004PathError);
            createFolder(initC.AP004PathArchive);
            createFolder(initC.AP004ImportSource);
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
        public void createFolderPO001PathZip()
        {
            bool folderExists = Directory.Exists(initC.PathZip);
            if (!folderExists)
                Directory.CreateDirectory(initC.PathZip);
        }
        public String[] getFileinFolder(String path)
        {
            string[] filePaths = null;
            if (Directory.Exists(path))
            {
                filePaths = Directory.GetFiles(@path);
            }
             
            return filePaths;
        }
        public String[] getFileinFolder(String path, String app)
        {
            string[] filePaths = null;
            String filename = "";
            if (Directory.Exists(path))
            {
                filePaths = Directory.GetFiles(@path, "*"+app+"*");
            }

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
        public void setConfig(String key, String value)
        {
            iniFile.Write(key, value);
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
            initC.AutoRunPO003 = iniFile.Read("AutoRunPO003");
            initC.AutoRunPO008 = iniFile.Read("AutoRunPO008");
            initC.AutoValueSet = iniFile.Read("AutoValueSet");
            initC.AutoRunPO002 = iniFile.Read("AutoRunPO002");
            initC.AutoRunPO006 = iniFile.Read("AutoRunPO006");

            initC.PathMaster = iniFile.Read("PathMaster");

            initC.PO005PathArchive = iniFile.Read("PO005PathArchive").Trim();    //bit
            initC.PO005PathError = iniFile.Read("PO005PathError").Trim();
            initC.PO005PathInitial = iniFile.Read("PO005PathInitial").Trim();
            initC.PO005PathProcess = iniFile.Read("PO005PathProcess").Trim();
            initC.PO005ImportSource = iniFile.Read("PO005ImportSource").Trim();
            initC.PO005pathZip = iniFile.Read("PO005pathZip").Trim();

            initC.PO003PathArchive = iniFile.Read("PO003PathArchive").Trim();    //bit
            initC.PO003PathError = iniFile.Read("PO003PathError").Trim();
            initC.PO003PathInitial = iniFile.Read("PO003PathInitial").Trim();
            initC.PO003PathProcess = iniFile.Read("PO003PathProcess").Trim();
            initC.PO003ImportSource = iniFile.Read("PO003ImportSource").Trim();
            initC.PO003RECEIPT_SOURCE = iniFile.Read("PO003RECEIPT_SOURCE").Trim();
            initC.PO003TRANSACTION_TYPE = iniFile.Read("PO003TRANSACTION_TYPE").Trim();

            //initC.PO004PathArchive = iniFile.Read("PO004PathArchive").Trim();    //bit
            initC.PO004PathError = iniFile.Read("PO004PathError").Trim();
            initC.PO004PathInitial = iniFile.Read("PO004PathInitial").Trim();
            initC.PO004PathProcess = iniFile.Read("PO004PathProcess").Trim();
            initC.PO004ImportSource = iniFile.Read("PO004ImportSource").Trim();
            initC.PO004ZipFileSearch = iniFile.Read("PO004ZipFileSearch").Trim();
            initC.PO004FileType = iniFile.Read("PO004FileType").Trim();
            initC.PO004RECEIPT_SOURCE = iniFile.Read("PO004RECEIPT_SOURCE").Trim();
            initC.PO004RECEIPT_TRANSACTION_TYPE = iniFile.Read("PO004RECEIPT_TRANSACTION_TYPE").Trim();
            initC.PO004SOURCE_DOCUMENT_CODE = iniFile.Read("PO004SOURCE_DOCUMENT_CODE").Trim();
            initC.PO004RECEIPT_SOURCE_CODE = iniFile.Read("PO004RECEIPT_SOURCE_CODE").Trim();
            initC.PO004INTERFACE_SOURCE_CODE = iniFile.Read("PO004INTERFACE_SOURCE_CODE").Trim();

            initC.PO008PathArchive = iniFile.Read("PO008PathArchive").Trim();
            initC.PO008PathError = iniFile.Read("PO008PathError").Trim();
            initC.PO008PathInitial = iniFile.Read("PO008PathInitial").Trim();
            initC.PO008PathProcess = iniFile.Read("PO008PathProcess").Trim();
            initC.PO008ImportSource = iniFile.Read("PO008ImportSource").Trim();
            initC.PO008ZipFileSearch = iniFile.Read("PO008ZipFileSearch").Trim();
            initC.PO008LEGAL_ENTITY = iniFile.Read("PO008LEGAL_ENTITY").Trim();
            initC.PO008BUYER = iniFile.Read("PO008BUYER").Trim();

            initC.PO002PathInitial = iniFile.Read("PO002PathInitial").Trim();
            initC.PO002PathDestinaion = iniFile.Read("PO002PathDestinaion").Trim();

            initC.PO007PathInitial = iniFile.Read("PO007PathInitial").Trim();

            initC.PO006PathInitial = iniFile.Read("PO006PathInitial").Trim();
            initC.PO006ReRun = iniFile.Read("PO006ReRun").Trim();
            initC.Po006DeliveryDate = iniFile.Read("Po006DeliveryDate").Trim();

            initC.ExtractZipPathZipExtractRead = iniFile.Read("ExtractZipPathZipExtractRead").Trim();
            initC.ExtractZipPathZipExtract = iniFile.Read("ExtractZipPathZipExtract").Trim();
            initC.AutoRunExtractZip = iniFile.Read("AutoRunExtractZip").Trim();

            initC.AP001PathArchive = iniFile.Read("AP001PathArchive").Trim();
            initC.AP001PathError = iniFile.Read("AP001PathError").Trim();
            initC.AP001PathInitial = iniFile.Read("AP001PathInitial").Trim();
            initC.AP001PathProcess = iniFile.Read("AP001PathProcess").Trim();

            initC.AP004PathArchive = iniFile.Read("AP004PathArchive").Trim();
            initC.AP004PathError = iniFile.Read("AP004PathError").Trim();
            initC.AP004PathInitial = iniFile.Read("AP004PathInitial").Trim();
            initC.AP004PathProcess = iniFile.Read("AP004PathProcess").Trim();
            initC.AP004ImportSource = iniFile.Read("AP004ImportSource").Trim();

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
        public Boolean validateDate1(DateTime date)
        {
            Boolean chk = false;
            //if (date.Length == 8)
            //{
            //    sYear.Clear();
            //    sMonth.Clear();
            //    sDay.Clear();
            //    try
            //    {
            //        sYear.Append(date.Substring(0, 4));
            //        sMonth.Append(date.Substring(4, 2));
            //        sDay.Append(date.Substring(6, 2));
            //        if ((int.Parse(sYear.ToString()) > 2000) && (int.Parse(sYear.ToString()) < 2100))
            //        {
            //            if ((int.Parse(sMonth.ToString()) >= 1) && (int.Parse(sMonth.ToString()) <= 12))
            //            {
            //                if ((int.Parse(sDay.ToString()) >= 1) && (int.Parse(sDay.ToString()) <= 31))
            //                {
            //                    chk = true;
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        chk = false;
            //    }
            //    finally
            //    {

            //    }

            //}
            //else
            //{
            //    chk = false;
            //}
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
        public void logProcess(String programname, List<ValidatePrPo> lVPr, String startdatetime, List<ValidateFileName> listfile)
        {
            String line1 = "", parameter="", programstart="", filename="", recordError="", txt="";
            int cntErr = 0, err=0;
            if (programname.ToLower().Equals("xcustpo001"))
            {
                line1 = "Program : XCUST Interface PR<Linfox>To PO(ERP)" + Environment.NewLine;
            }
            parameter = "Parameter : "+Environment.NewLine;
            parameter += "           Path Initial :" + initC.PathInitial + Environment.NewLine;
            parameter += "           Path Process :" + initC.PathProcess + Environment.NewLine;
            parameter += "           Path Error :" + initC.PathError + Environment.NewLine;
            parameter += "           Import Source :" + initC.ImportSource + Environment.NewLine;
            programstart = "Program Start : " + startdatetime + Environment.NewLine;
            if (listfile.Count > 0)
            {
                foreach (ValidateFileName vF in listfile)
                {
                    filename += "Filename " + vF.fileName + ", Total = " + vF.recordTotal + ", Validate pass = " + vF.validatePass + ", Record Error = " + vF.recordError+", Total Error = "+vF.totalError + Environment.NewLine;
                    if (int.TryParse(vF.recordError, out err))
                    {
                        if (int.Parse(vF.recordError) > 0)
                        {
                            cntErr++;
                        }
                    }
                }
            }
            if (lVPr.Count > 0)
            {
                foreach(ValidatePrPo vPr in lVPr)
                {
                    recordError += "FileName " + vPr.Filename + Environment.NewLine;
                    recordError += "==>" + vPr.Validate + Environment.NewLine;
                    recordError += "     ====>Error"+vPr.Message + Environment.NewLine;
                }
            }
            using (var stream = File.CreateText(Environment.CurrentDirectory + "\\" + programname+"_"+ startdatetime.Replace("-","_").Replace(":","_") + ".log"))
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
                txt += "Total "+ listfile.Count + Environment.NewLine;
                txt += "Complete " + (listfile.Count-cntErr) + Environment.NewLine;
                txt += "Error " + cntErr + Environment.NewLine;
                stream.WriteLine(txt);
            }
        }
        public void callWebService(String flag)
        {
            String uri = "", dump = "";
            //HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            const Int32 BufferSize = 128;
            String[] filePO;
            if (flag.Equals("PO001"))
            {
                filePO = getFileinFolder(initC.PathZip);
            }
            else if (flag.Equals("PO005"))
            {
                filePO = getFileinFolder(initC.PathZip);
            }
            else
            {
                filePO = getFileinFolder(initC.PathZip);
            }
            
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
                     "<web:documentTitle>amo_test_load</web:documentTitle> " +
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
        public void runCommand(String filename, String argument)
        {
            Process process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = argument; 
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            string err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            process.WaitForExit();
        }
        public String dateDBtoShow(String date)
        {
            String chk = "", year = "", month = "", day = "";
            if (date.Length >= 10)
            {
                day = date.Substring(8, 2);
                month = date.Substring(5, 2);
                year = date.Substring(0, 4);

                chk = day + "/" + month + "/" + year;
            }
            else
            {
                chk = date;
            }
            return chk;
        }
        public String FixLen(String str, String len, String chrFix)
        {
            String chk = "", aaa = "";
            int len1 = 0;
            if (int.TryParse(len, out len1))
            {
                if (len1 > str.Length)
                {
                    for (int i = 0; i < len1; i++)
                    {
                        aaa += chrFix;
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
        
        private AlternateView getEmbeddedImage(String filePath, String cid)
        {
            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = cid;
            string htmlBody = "<img src=cid:" + res.ContentId + ">";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }
    }
}
