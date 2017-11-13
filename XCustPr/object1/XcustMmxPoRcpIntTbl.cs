using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustMmxPoRcpIntTbl:Persistent
    {
        public String store_cocde = "", item_code = "", date_of_record = "", RECEIVE_QTY = "", INVOICE_NO = "", INVOICE_AMT = "", supplier_code = "", lot_number = "";
        public String serial_number = "", validate_flag = "", process_flag = "", error_message = "", creation_by = "", creation_date = "", last_update_date = "", last_update_by = "";
        public String file_name = "", erp_supplier_code = "", erp_supplier_site_code = "", erp_item_code = "", po_number = "", po_line_number = "", erp_subinventory_code = "", erp_locator = "";
        public String erp_qty = "", erp_uom = "";
    }
}
