using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoHeaderIntTblDB
    {
        XcustPoHeaderIntTbl xCPHIT;
        ConnectDB conn;

        public XcustPoHeaderIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPHIT = new XcustPoHeaderIntTbl();
            xCPHIT.acceptance_required_flag = "acceptance_required_flag";
            xCPHIT.action = "action";
            xCPHIT.approval_action = "approval_action";
            xCPHIT.bill_to_location = "bill_to_location";
            xCPHIT.buyyer_name = "buyyer_name";
            xCPHIT.creation_by = "creation_by";
            xCPHIT.creation_date = "creation_date";
            xCPHIT.currency_code = "currency_code";
            xCPHIT.document_num = "document_num";
            xCPHIT.document_typre_code = "document_typre_code";
            xCPHIT.error_message = "error_message";
            xCPHIT.import_source = "import_source";
            xCPHIT.interface_header_key = "interface_header_key";
            xCPHIT.last_update_by = "last_update_by";
            xCPHIT.last_update_date = "last_update_date";
            xCPHIT.originator_rule = "originator_rule";
            xCPHIT.payment_term = "payment_term";
            xCPHIT.prc_bu_name = "prc_bu_name";
            xCPHIT.process_flag = "process_flag";
            xCPHIT.req_bu_name = "req_bu_name";
            xCPHIT.ship_to_location = "ship_to_location";
            xCPHIT.soldto_re_name = "soldto_re_name";
            xCPHIT.supplier_code = "supplier_code";
            xCPHIT.supplier_site_code = "supplier_site_code";
            xCPHIT.vendor_contact = "vendor_contact";

            xCPHIT.table = "XCUST_PO_HEADER_INT_TBL";

        }
    }
}
