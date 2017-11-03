using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustBlanketAgreementLinesTblDB
    {
        XcustBlanketAgreementLinesTbl xCBALT;
        ConnectDB conn;
        private InitC initC;
        public XcustBlanketAgreementLinesTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCBALT = new XcustBlanketAgreementLinesTbl();
            xCBALT.ALLOW_PRICE_OVERIDE = "ALLOW_PRICE_OVERIDE";
            xCBALT.CREATION_DATE = "CREATION_DATE";
            xCBALT.CURRENCY_CODE = "CURRENCY_CODE";
            xCBALT.DESCRIPTION = "DESCRIPTION";
            xCBALT.EXPIRATION_DATE = "EXPIRATION_DATE";
            xCBALT.ITEM_CODE = "ITEM_CODE";
            xCBALT.ITEM_ID = "ITEM_ID";
            xCBALT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCBALT.LINE_AGREEMENT_AMT = "LINE_AGREEMENT_AMT";
            xCBALT.LINE_AGREEMENT_QTY = "LINE_AGREEMENT_QTY";
            xCBALT.LINE_NUMBER = "LINE_NUMBER";
            xCBALT.LINE_RELEASE_AMT = "LINE_RELEASE_AMT";
            xCBALT.LINE_RELEASE_QTY = "LINE_RELEASE_QTY";
            xCBALT.LINE_REVISION = "LINE_REVISION";
            xCBALT.LINE_STATUS = "LINE_STATUS";
            xCBALT.PO_HEADER_ID = "PO_HEADER_ID";
            xCBALT.PO_LINE_ID = "PO_LINE_ID";
            xCBALT.PRICE = "PRICE";
            xCBALT.PRICE_LIMIT = "PRICE_LIMIT";
            xCBALT.RELEASE_AMT = "RELEASE_AMT";
            xCBALT.UOM = "UOM";

            xCBALT.table = "XCUST_BLANKET_AGREEMENT_LINES_TBL";
            xCBALT.pkField = "";
        }
    }
}
