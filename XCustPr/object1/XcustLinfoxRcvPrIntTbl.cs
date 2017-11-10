using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustLinfoxRcvPrIntTbl:Persistent
    {
        public String company_code = "", branch_plant = "", order_number = "", doc_type = "", order_company = "", line_number = "";
        public String qty_receipt = "", item_code = "", uom_code = "", lot_number = "", lot_expire_date = "", reference1 = "", vendor_remark = "";
        public String receipt_date = "", receipt_time = "", reason_code = "", location = "", uom_code1 = "", uom_code2 = "", validate_flag = "", process_flag = "";
        public String error_message = "", creation_by = "", creation_date = "", last_update_date = "", last_update_by = "", file_name = "", supplier_code = "";
        public String SUPPLIER_SITE_CODE = "", ERP_ITEM_CODE = "", DOCUMENT_NUMBER = "", DOCUMENT_LINE_NUMBER = "", subinventory_code = "", LOCATOR = "";
    }
}
