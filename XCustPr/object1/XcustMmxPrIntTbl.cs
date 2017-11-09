using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustMmxPrIntTbl:Persistent
    {
        public String store_code = "", po_number = "", supplier_code = "", delivery_date = "", subinventory_code = "", order_qty = "";
        public String confirm_qty = "", item_code = "", order_date = "", po_status = "", conf_delivery_date = "", Validate_flag = "";
        public String process_flag = "", error_message = "", creation_by = "", creation_date = "", last_update_date = "", last_update_by = "";
        public String file_name = "", request_date = "", diriver_to_organization = "", deriver_to_location = "", erp_subinventory_code = "";
        public String ERP_ITEM_CODE = "", AGREEEMENT_NUMBER = "", AGREEMENT_LINE_NUMBER = "", PRICE = "", ITEM_CATEGORY_NAME = "", SUPPLIER_SITE_CODE = "";
        public String ACC_SEG1 = "", ACC_SEG2 = "", ACC_SEG3 = "", ACC_SEG4 = "", ACC_SEG5 = "", ACC_SEG6 = "", uom_code="", DELIVERY_INSTRUCTION="";
    }
}
