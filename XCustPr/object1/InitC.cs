using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class InitC
    {
        public String PathInitial = "", PathProcess="",PathError="",PathArchive="", PathLog = "", BU_NAME="", APPROVER_EMAIL="", Requester="";

        public String pathLogErr = "";

        public String databaseDBBIT = "bithis_demo1";             //bit
        public String hostDBBIT = "172.25.1.153";
        public String userDBBIT = "sa";
        public String passDBBIT = "Orawanhospital1*";
        public String portDBBIT = "3306";

        public String databaseDBBITDemo = "bithis_demo";             //bit demo
        public String hostDBBITDemo = "172.25.1.153";
        public String userDBBITDemo = "sa";
        public String ImportSource = "ImportSource";
        public String Company = "Company";
        public String DELIVER_TO_LOCATTION = "DELIVER_TO_LOCATTION";
        public String ORGANIZATION_code = "ORGANIZATION_code";
        public String Locator = "Locator";
        public String Subinventory_Code = "Locator";
        public String CURRENCY_CODE = "CURRENCY_CODE";
        public String PR_STATAUS = "";
        public String LINE_TYPE = "";
        public String AutoRunPO001 = "";
        public String AutoRunPO004 = "";
        public String AutoRunPO005 = "";
        public String AutoRunPO003 = "";
        public String AutoRunPO008 = "";
        public String AutoRunExtractZip = "";
        public String AutoValueSet = "";
        public String AutoRunPO002 = "";
        public String AutoRunPO006 = "";

        public String POAPPROVER = "";
        public String POAPPROVER_EMAIL = "";
        public String DESTINATION_TYPE_CODE = "";

        public String PathMaster = "";

        public String EmailPort = "3306";
        public String EmailCharset = "hisorc_ma";        //orc master
        public String EmailUsername = "172.25.1.153";
        public String EmailPassword = "root";
        public String EmailSMTPSecure = "Ekartc2c5";
        public String PathLinfox = "3306";

        public String EmailHost = "hisorc_ba";        // orc backoffice
        public String EmailSender = "172.25.1.153";
        public String FTPServer = "root";
        public String PathZipExtract = "Ekartc2c5";
        public String PathZip = "3306";

        public String databaseDBKFCPO = "bithis";        // orc BIT
        public String hostDBKFCPO = "172.25.1.153";
        public String userDBKFCPO = "root";
        public String passDBKFCPO = "Ekartc2c5";
        public String portDBKFCPO = "3306";

        public String PO005PathInitial = "", PO005PathProcess = "", PO005PathError = "", PO005PathArchive = "", PO005ImportSource="", PO005pathZip="";
        public String PO005PathLog = "";

        public String PO003PathInitial = "", PO003PathProcess = "", PO003PathError = "", PO003PathArchive = "", PO003ImportSource = "", PO003RECEIPT_SOURCE="", PO003TRANSACTION_TYPE="";
        public String PO003PathLog = "";

        public String PO004PathInitial = "", PO004PathProcess = "", PO004PathError = "", PO004RECEIPT_SOURCE = "", PO004ImportSource = "", PO004ZipFileSearch="", PO004RECEIPT_TRANSACTION_TYPE="";
        public String PO004SOURCE_DOCUMENT_CODE = "", PO004RECEIPT_SOURCE_CODE="", PO004INTERFACE_SOURCE_CODE="";
        public String PO004PathLog = "";

        public String PO008PathInitial = "", PO008PathProcess = "", PO008PathError = "", PO008PathArchive = "", PO008ImportSource = "", PO008ZipFileSearch = "", PO008LEGAL_ENTITY="", PO008BUYER="";
        public String PO008PathLog = "";

        public String PO004FileType = "";
        public String PO007PathInitial = "";
        public String PO007PathLog = "";

        public String PO002PathInitial = "", PO002PathDestinaion = "", PO002PathLog = "";
        public String PO006PathInitial = "", PO006ReRun="", Po006DeliveryDate="";
        public String PO006PathLog = "";

        public String ExtractZipPathZipExtractRead = "", ExtractZipPathZipExtract="";

        public String AP001PathInitial = "", AP001PathProcess = "", AP001PathError = "", AP001PathArchive = "", AP001ImportSource = "", AP001LEGAL_ENTITY="", AP001INVOICE_TYPE="";
        public String AP001PathLog = "";

        public String AP004PathInitial = "", AP004PathProcess = "", AP004PathError = "", AP004PathArchive = "", AP004ImportSource="";
        public String AP004PathLog = "";


    }
}
