using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoDistIntTblDB
    {
        public XcustPoDistIntTbl xCPDIT;
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
            xCPDIT.charge_account_segment4 = "charg_accounte_segment4";
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
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPDIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String genSeqReqDistNumber()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "SELECT next value for xcust_po_distribution_seq ;";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString().Trim();
            }
            return chk;
        }
        public String insert(XcustPoDistIntTbl p, String pathLog)
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
                String seqD = genSeqReqDistNumber();
                p.interface_distribution_key = seqD;
                sql = "Insert Into " + xCPDIT.table + "(" + xCPDIT.amt + "," + xCPDIT.charge_account_segment1 + "," + xCPDIT.charge_account_segment2 + "," +
                    xCPDIT.charge_account_segment3 + "," + xCPDIT.charge_account_segment4 + "," + xCPDIT.charge_account_segment5 + "," +
                    xCPDIT.charge_account_segment6 + "," + xCPDIT.creation_by + "," + xCPDIT.creation_date + "," +
                    xCPDIT.deliver_to_location + "," + xCPDIT.destion_subinventory + "," + xCPDIT.distribution_num + "," +
                    xCPDIT.error_message + "," + xCPDIT.interface_distribution_key + "," + xCPDIT.interface_header_key + "," +
                    xCPDIT.interface_line_key + "," + xCPDIT.interface_line_location_key + "," + xCPDIT.last_update_by + "," +
                    xCPDIT.last_update_date + "," + xCPDIT.process_flag +
                    ") " +
                    "Values('" + p.amt + "','" + p.charge_account_segment1 + "','" + p.charge_account_segment2 + "','" +
                    p.charge_account_segment3 + "','" + p.charge_account_segment4 + "','" + p.charge_account_segment5 + "','" +
                    p.charge_account_segment6 + "','" + creation_by + "',getdate(),'" +
                    p.deliver_to_location + "','" + p.destion_subinventory + "','" + p.distribution_num + "','" +
                    p.error_message + "','" + p.interface_distribution_key + "','" + p.interface_header_key + "','" +
                    p.interface_line_key + "','" + p.interface_line_location_key + "','" + last_update_by + "'," +
                    "null,'" + p.process_flag +
                    ") ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
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
