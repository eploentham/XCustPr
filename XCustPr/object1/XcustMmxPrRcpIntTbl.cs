using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustMmxPrRcpIntTbl:Persistent
    {
        public String store_code = "", po_number = "", supplier_code = "", delivery_date = "", subinventory_code = "", order_qty = "", confirm_qty = "";
        public String item_code = "", order_date = "", po_status = "", conf_delivery_date = "", Validate_flag = "", process_flag = "", error_message = "", creation_by = "";
        public String creation_date = "", last_update_date = "", last_update_by = "", file_name = "", request_date = "", diriver_to_organization = "", deriver_to_location = "";
        public String erp_subinventory_code = "", ERP_ITEM_CODE = "", AGREEEMENT_NUMBER = "", AGREEMENT_LINE_NUMBER = "", PRICE = "", ITEM_CATEGORY_NAME = "", SUPPLIER_SITE_CODE = "";
        public String ACC_SEG1 = "", ACC_SEG2 = "", ACC_SEG3 = "", ACC_SEG4 = "", ACC_SEG5 = "", ACC_SEG6 = "", uom_code = "", DELIVERY_INSTRUCTION = "";
    }
}
