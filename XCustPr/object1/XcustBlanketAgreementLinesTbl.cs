using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustBlanketAgreementLinesTbl:Persistent
    {
        public String LINE_NUMBER = "", ITEM_ID = "", ITEM_CODE = "", DESCRIPTION = "", UOM = "", PRICE = "";
        public String RELEASE_AMT = "", EXPIRATION_DATE = "", LINE_STATUS = "", LINE_AGREEMENT_AMT = "", LINE_AGREEMENT_QTY = "";
        public String LINE_RELEASE_AMT = "", LINE_RELEASE_QTY = "", ALLOW_PRICE_OVERIDE = "", PRICE_LIMIT = "", CURRENCY_CODE = "";
        public String LINE_REVISION = "", LAST_UPDATE_DATE = "", CREATION_DATE = "", PO_HEADER_ID = "", PO_LINE_ID = "";
    }
}
