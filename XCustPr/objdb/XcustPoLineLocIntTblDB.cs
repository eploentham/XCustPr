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
        public String insert(XcustPoLineLocIntTbl p)
        {
            String sql = "", chk = "";
            try
            {
                //if (p.OrpChtNum.Equals(""))
                //{
                //    return "";
                //}
                //p.RowNumber = selectMaxRowNumber(p.YearId);
                //p.Active = "1";
                String last_update_by = "0", creation_by = "0";
                sql = "Insert Into " + xCPLLIT.table + "(" + xCPLLIT.amt + "," + xCPLLIT.attirbute1 + "," + xCPLLIT.attribute2 + "," +
                    xCPLLIT.attribute3 + "," + xCPLLIT.attribute4 + "," + xCPLLIT.cration_date + "," +
                    xCPLLIT.creation_by + "," + xCPLLIT.destination_type_code + "," + xCPLLIT.error_message + "," +
                    xCPLLIT.input_tax_classsification_code + "," + xCPLLIT.interface_header_key + "," + xCPLLIT.interface_line_key + "," +
                    xCPLLIT.interface_line_location_key + "," + xCPLLIT.last_date_by + "," + xCPLLIT.last_update_date + "," +
                    xCPLLIT.need_by_date + "," + xCPLLIT.process_flag + "," + xCPLLIT.shipment_number+
                    ") " +
                    "Values(" + p.amt + ",'" + p.attirbute1 + "','" + p.attribute2 + "','" +
                    p.attribute3 + "','" + p.attribute4 + "',getdate(),'" +
                    creation_by + "','" + p.destination_type_code + "','" + p.error_message + "'," +
                    p.input_tax_classsification_code + "','" + p.interface_header_key + "','" + p.interface_line_key + "'," +
                    p.interface_line_location_key + "','" + last_update_by + "',null,'" +
                    p.need_by_date + "','" + p.process_flag + p.shipment_number + "','" +
                    ") ";
                chk = conn.ExecuteNonQueryAutoIncrement(sql, "kfc_po");
                //chk = p.RowNumber;
                //chk = p.Code;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }

            return chk;
        }
    }
}
