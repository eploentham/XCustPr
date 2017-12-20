using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoLineLocIntTbl:Persistent
    {
        public String interface_header_key = "", interface_line_key = "", interface_line_location_key = "", shipment_number = "", amt = "", need_by_date = "", destination_type_code = "";
        public String input_tax_classsification_code = "", attribute1 = "", attribute2 = "", attribute3 = "", attribute4 = "", process_flag = "", error_message = "";
        public String cration_date = "", creation_by = "", last_update_date = "", last_date_by = "", wo_no = "", running = "", ship_to_location="", request_id = "";
    }
}
