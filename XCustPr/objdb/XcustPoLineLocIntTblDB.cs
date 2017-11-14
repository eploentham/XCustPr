using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoLineLocIntTblDB
    {
        XcustPoLineLocIntTbl xCPLLIT;
        ConnectDB conn;

        public XcustPoLineLocIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPLLIT = new XcustPoLineLocIntTbl();
            xCPLLIT.amt = "amt";
            xCPLLIT.attirbute1 = "attirbute1";
            xCPLLIT.attribute2 = "attribute2";
            xCPLLIT.attribute3 = "attribute3";
            xCPLLIT.attribute4 = "attribute4";
            xCPLLIT.cration_date = "cration_date";
            xCPLLIT.creation_by = "creation_by";
            xCPLLIT.destination_type_code = "destination_type_code";
            xCPLLIT.error_message = "error_message";
            xCPLLIT.input_tax_classsification_code = "input_tax_classsification_code";
            xCPLLIT.interface_header_key = "interface_header_key";
            xCPLLIT.interface_line_key = "interface_line_key";
            xCPLLIT.interface_line_location_key = "interface_line_location_key";
            xCPLLIT.last_date_by = "last_date_by";
            xCPLLIT.last_update_date = "last_update_date";
            xCPLLIT.need_by_date = "need_by_date";
            xCPLLIT.process_flag = "process_flag";
            xCPLLIT.shipment_number = "shipment_number";

            xCPLLIT.table = "XCUST_PO_LINE_LOC_INT_TBL";
        }
    }
}
