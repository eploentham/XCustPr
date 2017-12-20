using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoHeaderIntTbl:Persistent
    {
        public String interface_header_key = "", action = "", import_source = "", approval_action = "", document_num = "", document_typre_code = "", prc_bu_name = "", req_bu_name = "";
        public String soldto_re_name = "", buyyer_name = "", currency_code = "", bill_to_location = "", ship_to_location = "", supplier_code = "", supplier_site_code = "", vendor_contact = "";
        public String payment_term = "", originator_rule = "", acceptance_required_flag = "", process_flag = "", error_message = "", creation_by = "", creation_date = "";
        public String last_update_date = "", last_update_by = "", billto_bu_name="", DOCUMENT_ID="", wo_no="", qt_no="";
    }
}
