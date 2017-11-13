using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustMmxPoRcpIntTblDB
    {
        public XcustMmxPoRcpIntTbl xCMPRIT;
        ConnectDB conn;
        private InitC initC;

        public XcustMmxPoRcpIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
        }
        private void initConfig()
        {
            xCMPRIT = new XcustMmxPoRcpIntTbl();
            xCMPRIT.creation_by = "creation_by";
            xCMPRIT.creation_date = "creation_date";
            xCMPRIT.date_of_record = "date_of_record";
            xCMPRIT.erp_item_code = "erp_item_code";
            xCMPRIT.erp_locator = "erp_locator";
            xCMPRIT.erp_qty = "erp_qty";
            xCMPRIT.erp_subinventory_code = "erp_subinventory_code";
            xCMPRIT.erp_supplier_code = "erp_supplier_code";
            xCMPRIT.erp_supplier_site_code = "erp_supplier_site_code";
            xCMPRIT.erp_uom = "erp_uom";
            xCMPRIT.error_message = "error_message";
            xCMPRIT.file_name = "file_name";
            xCMPRIT.INVOICE_AMT = "INVOICE_AMT";
            xCMPRIT.INVOICE_NO = "INVOICE_NO";
            xCMPRIT.item_code = "item_code";
            xCMPRIT.last_update_by = "last_update_by";
            xCMPRIT.last_update_date = "last_update_date";
            xCMPRIT.lot_number = "lot_number";
            xCMPRIT.po_line_number = "po_line_number";
            xCMPRIT.po_number = "po_number";
            xCMPRIT.process_flag = "process_flag";
            xCMPRIT.RECEIVE_QTY = "RECEIVE_QTY";
            xCMPRIT.serial_number = "serial_number";
            xCMPRIT.store_cocde = "store_cocde";
            xCMPRIT.supplier_code = "supplier_code";
            xCMPRIT.validate_flag = "validate_flag";

            xCMPRIT.table = "xcust_mmx_po_rcp_int_tbl";
            xCMPRIT.pkField = "";
        }
        public void DeleteMmxTemp()
        {
            String sql = "Delete From " + xCMPRIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPRIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void insertBluk(List<String> linfox, String filename, String host, MaterialProgressBar pB1)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", lastUpdateBy = "0", lastUpdateTime = "null";
            if (host == "kfc_po")
            {
                ConnectionString = conn.connKFC.ConnectionString;
            }
            StringBuilder sql = new StringBuilder();
            pB1.Minimum = 1;
            pB1.Maximum = linfox.Count();
            using (SqlCommand mConnection = new SqlCommand(ConnectionString))
            {
                List<string> Rows = new List<string>();
                foreach (String bbb in linfox)
                {
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    String[] aaa = bbb.Split('|');
                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    //bbb += "('" + aaa[0] + "','" +
                    //aaa[11] + "','" + errMsg + "','" + aaa[6] + "','" +
                    //aaa[2] + "','" + aaa[4] + "','" + aaa[5] + "','" +
                    //aaa[1] + "','" + processFlag + "','" + aaa[7] + "','" +
                    //aaa[3] + "','" + aaa[8] + "','" + validateFlag + "'),";
                    sql.Append("Insert Into ").Append(xCMPRIT.table).Append(" (").Append(xCMPRIT.creation_by).Append(",").Append(xCMPRIT.creation_date).Append(",").Append(xCMPRIT.date_of_record)
                        .Append(",").Append(xCMPRIT.erp_item_code).Append(",").Append(xCMPRIT.erp_locator).Append(",").Append(xCMPRIT.erp_qty)
                        .Append(",").Append(xCMPRIT.erp_subinventory_code).Append(",").Append(xCMPRIT.erp_supplier_code).Append(",").Append(xCMPRIT.erp_supplier_site_code)
                        .Append(",").Append(xCMPRIT.erp_uom).Append(",").Append(xCMPRIT.error_message).Append(",").Append(xCMPRIT.file_name)
                        .Append(",").Append(xCMPRIT.INVOICE_AMT).Append(",").Append(xCMPRIT.INVOICE_NO).Append(",").Append(xCMPRIT.item_code)
                        .Append(",").Append(xCMPRIT.last_update_by).Append(",").Append(xCMPRIT.last_update_date).Append(",").Append(xCMPRIT.lot_number)
                        .Append(",").Append(xCMPRIT.po_line_number).Append(",").Append(xCMPRIT.po_number).Append(",").Append(xCMPRIT.process_flag)
                        .Append(",").Append(xCMPRIT.RECEIVE_QTY).Append(",").Append(xCMPRIT.serial_number).Append(",").Append(xCMPRIT.store_cocde)
                        .Append(",").Append(xCMPRIT.supplier_code).Append(",").Append(xCMPRIT.validate_flag)
                        .Append(") Values ('")
                        .Append(aaa[0]).Append("','").Append(aaa[1]).Append("','").Append(aaa[2])
                        .Append("','").Append(aaa[3]).Append("',").Append(aaa[4]).Append(",'").Append(aaa[5])
                        .Append("','").Append(aaa[7]).Append("',").Append(aaa[8]).Append(",'").Append(aaa[9])
                        .Append("','").Append(aaa[10]).Append("','").Append(validateFlag).Append("','").Append(processFlag)
                        .Append("','").Append(errMsg).Append("','").Append(createBy).Append("',").Append(createDate)
                        .Append(",'").Append(lastUpdateBy).Append("',").Append(lastUpdateTime).Append(",'").Append(filename.Trim().Replace(initC.PathProcess, ""))
                        .Append("','").Append(aaa[11]).Append("','").Append(aaa[6]).Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host);
                }
            }
        }
    }
}
