using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoDistIntTblDB
    {
        XcustPoDistIntTbl xCPDIT;
        ConnectDB conn;

        public XcustPoDistIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPDIT = new XcustPoDistIntTbl();
            xCPDIT.amt = "amt";
            xCPDIT.charge_account_segment1 = "charge_account_segment1";
            xCPDIT.charge_account_segment2 = "charge_account_segment2";
            xCPDIT.charge_account_segment3 = "charge_account_segment3";
            xCPDIT.charg_accounte_segment4 = "charg_accounte_segment4";
            xCPDIT.charge_account_segment5 = "charge_account_segment5";
            xCPDIT.charge_account_segment6 = "charge_account_segment6";
            xCPDIT.creation_by = "creation_by";
            xCPDIT.creation_date = "creation_date";
            xCPDIT.deliver_to_location = "deliver_to_location";
            xCPDIT.destion_subinventory = "destion_subinventory";
            xCPDIT.distribution_num = "distribution_num";
            xCPDIT.error_message = "error_message";
            xCPDIT.interface_distribution_key = "interface_distribution_key";
            xCPDIT.interface_header_key = "interface_header_key";
            xCPDIT.interface_line_key = "interface_line_key";
            xCPDIT.interface_line_location_key = "interface_line_location_key";
            xCPDIT.last_update_by = "last_update_by";
            xCPDIT.last_update_date = "last_update_date";
            xCPDIT.process_flag = "process_flag";

            xCPDIT.table = "XCUST_PO_DIST_INT_TBL";
        }
    }
}
