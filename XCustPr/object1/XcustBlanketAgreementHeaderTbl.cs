using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustBlanketAgreementHeaderTbl:Persistent
    {
        public String POCUMENT_BU = "", AGREEMENT_NUMBER = "", STATUS = "", BUYER = "", SUPPLIER = "", SUPPLIER_SITE = "";
        public String SUPPLIER_CODE = "", COMUNICATION_METHOD = "", EMAIL = "", START_DATE = "", END_DATE = "",AGREEMENT_AMT="";
        public String MIN_RELEASE_AMT = "", RELEASE_AMT = "", DESCRIPTION = "", LAST_UPDATE_DATE = "", CREATION_DATE = "";
    }
}
