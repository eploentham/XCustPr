using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoLineIntTbl:Persistent
    {
        public String interface_header_key = "", interface_line_key = "", action = "", line_num = "", line_type = "", item_description = "", category = "", unit_price = "", process_flag = "";
        public String error_message = "", creation_by = "", creation_date = "", last_update_date = "", last_update_by = "", wo_no = "", qt_no = "", running="", attribute1="", attribute2="", request_id = "";
        public String attribute3 = "", attribute4 = "", attribute5 = "";
    }
}
