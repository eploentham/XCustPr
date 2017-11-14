using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoLineIntTblDB
    {
        XcustPoLineIntTbl xCPLIT;
        ConnectDB conn;

        public XcustPoLineIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPLIT = new XcustPoLineIntTbl();
            xCPLIT.action = "action";
            xCPLIT.category = "category";
            xCPLIT.creation_by = "creation_by";
            xCPLIT.creation_date = "creation_date";
            xCPLIT.error_message = "error_message";
            xCPLIT.interface_header_key = "interface_header_key";
            xCPLIT.interface_line_key = "interface_line_key";
            xCPLIT.item_description = "item_description";
            xCPLIT.last_update_by = "last_update_by";
            xCPLIT.last_update_date = "last_update_date";
            xCPLIT.line_num = "line_num";
            xCPLIT.line_type = "line_type";
            xCPLIT.process_flag = "process_flag";
            xCPLIT.unit_price = "unit_price";

            xCPLIT.table = "XCUST_PO_LINE_INT_TBL";
        }
    }
}
