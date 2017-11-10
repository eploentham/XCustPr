using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustRcvTransactionsIntAll:Persistent
    {
        public String HEADER_INTERFACE_NUMBER = "", LINE_NUMBER = "", TRANSACTION_TYPE = "", TRANSACTION_DATE = "", SOURCE_DOCUMENT_CODE = "", RECEIPT_SOURCE_CODE = "";
        public String ORGANIZATION_CODE = "", ITEM_CODE = "", DOCUMENT_NUMBER = "", DOCUMENT_LINE_NUMBER = "", BUSINESS_UNIT = "", SUBINVENTORY_CODE = "", LOCATOR_CODE = "";
        public String QUANTITY = "", UOM_CODE = "", INTERFACE_SOURCE_CODE = "";
        
        public String ATTRIBUTE1 = "", ATTRIBUTE_DATE1 = "", ATTRIBUTE_TIMESTAMP1 = "", PROCESS_FLAG = "";
        public String ATTRIBUTE2 = "", ATTRIBUTE3 = "", ATTRIBUTE4 = "", ATTRIBUTE5 = "", ATTRIBUTE6 = "", ATTRIBUTE7 = "", ATTRIBUTE8 = "", ATTRIBUTE9 = "", ATTRIBUTE10 = "";
        public String ATTRIBUTE11 = "", ATTRIBUTE12 = "", ATTRIBUTE13 = "", ATTRIBUTE14 = "", ATTRIBUTE15 = "", ATTRIBUTE16 = "", ATTRIBUTE17 = "", ATTRIBUTE18 = "", ATTRIBUTE19 = "", ATTROBUTE20 = "";
        public String ATTRIBUTE20 = "";
        public String ATTRIBUTE_NUMBER1 = "", ATTRIBUTE_NUMBER2 = "", ATTRIBUTE_NUMBER3 = "", ATTRIBUTE_NUMBER4 = "", ATTRIBUTE_NUMBER5 = "", ATTRIBUTE_NUMBER6 = "";
        public String ATTRIBUTE_NUMBER7 = "", ATTRIBUTE_NUMBER8 = "", ATTRIBUTE_NUMBER9 = "", ATTRIBUTE_NUMBER10 = "";

        public String ATTRIBUTE_DATE2 = "", ATTRIBUTE_DATE3 = "", ATTRIBUTE_DATE4 = "", ATTRIBUTE_DATE5 = "", ATTRIBUTE_DATE6 = "";
        public String ATTRIBUTE_DATE7 = "", ATTRIBUTE_DATE8 = "", ATTRIBUTE_DATE9 = "", ATTRIBUTE_DATE10 = "";

        public String ATTRIBUTE_TIMESTAMP2 = "", ATTRIBUTE_TIMESTAMP3 = "", ATTRIBUTE_TIMESTAMP4 = "", ATTRIBUTE_TIMESTAMP5 = "", ATTRIBUTE_TIMESTAMP6 = "";
        public String ATTRIBUTE_TIMESTAMP7 = "", ATTRIBUTE_TIMESTAMP8 = "", ATTRIBUTE_TIMESTAMP9 = "", ATTRIBUTE_TIMESTAMP10 = "";

        public String ATTRIBUTE_CATEGORY = "";

        public String LAST_UPDATE_DATE = "", CREATION_DATE = "", LAST_UPDATE_BY = "", CREATE_BY = "", IMPORT_SOURCE = "";
    }
}
