using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoDistIntTbl:Persistent
    {
        public String interface_header_key = "", interface_line_key = "", interface_line_location_key = "", interface_distribution_key = "", distribution_num = "", deliver_to_location = "";
        public String destion_subinventory = "", amt = "", charge_account_segment1 = "", charge_account_segment2 = "", charge_account_segment3 = "", charg_accounte_segment4 = "", charge_account_segment5 = "", charge_account_segment6 = "";
        public String process_flag = "", error_message = "", creation_by = "", creation_date = "", last_update_date = "", last_update_by = "";
    }
}
