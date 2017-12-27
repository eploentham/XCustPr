using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustCedarPoIntTbl:Persistent
    {
        public String xno = "", po_no = "", qt_no = "", wo_no = "", period = "", week = "", branch_plant = "", loctype = "", item_e1 = "", asset_code = "", asset_name = "", work_type = "";
        public String amt = "", vat = "", total = "", supplier_code = "", supplier_name = "", admin = "", admin_receipt_doc_date = "", approve_date = "", cedar_close_date = "", invoice_due_date = "";
        public String sup_agreement_no = "", account_segment_no = "", data_source = "", validate_flag = "", process_flag = "", error_message = "", creation_by = "", creation_date = "";
        public String last_update_date = "", last_update_by = "", file_name = "", supplier_site_code = "", supplier_contact = "", payment_term = "", item_description = "", category_name = "";
        public String shippto_location = "", account_segment1 = "", account_segment2 = "", account_segment3 = "", account_segment4 = "", account_segment5 = "", account_segment6 = "";
        public String request_id = "", row_number = "", row_cnt = "", project_code;
    }
}
