using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustBlanketAgreementHeaderTblDB
    {
        public XcustBlanketAgreementHeaderTbl xCBAHT;
        ConnectDB conn;
        private InitC initC;
        public XcustBlanketAgreementHeaderTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCBAHT = new XcustBlanketAgreementHeaderTbl();
            xCBAHT.AGREEMENT_AMT = "AGREEMENT_AMT";
            xCBAHT.AGREEMENT_NUMBER = "AGREEMENT_NUMBER";
            xCBAHT.BUYER = "BUYER";
            xCBAHT.COMUNICATION_METHOD = "COMUNICATION_METHOD";
            xCBAHT.CREATION_DATE = "CREATION_DATE";
            xCBAHT.DESCRIPTION = "DESCRIPTION";
            xCBAHT.EMAIL = "E_MAIL";
            xCBAHT.END_DATE = "END_DATE";
            xCBAHT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCBAHT.MIN_RELEASE_AMT = "MIN_RELEASE_AMT";
            xCBAHT.POCUMENT_BU = "POCUMENT_BU";
            xCBAHT.RELEASE_AMT = "RELEASE_AMT";
            xCBAHT.START_DATE = "START_DATE";
            xCBAHT.STATUS = "STATUS";
            xCBAHT.SUPPLIER = "SUPPLIER";
            xCBAHT.SUPPLIER_CODE = "SUPPLIER_CODE";
            xCBAHT.SUPPLIER_SITE = "SUPPLIER_SITE";
            xCBAHT.PO_HEADER_ID = "PO_HEADER_ID";

            xCBAHT.table = "XCUST_BLANKET_AGREEMENT_HEADER_TBL";
            xCBAHT.pkField = "";
        }
        
    }
}
