using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustLinfoxPrTbl:Persistent
    {
        public String COMPANYCODE = "", PO_NUMBER = "", LINE_NUMBER = "", SUPPLIER_CODE = "", ORDER_DATE = "", ORDER_TIME = "", ITEM_CODE = "", QTY = "", UOMCODE = "", DELIVERY_INSTRUCTION = "", VALIDATE_FLAG = "", PROCESS_FLAG = "", ERROR_MSG = "";
        public String create_by = "", create_date = "", last_update_date = "", last_update_by = "", file_name = "";
    }
}
