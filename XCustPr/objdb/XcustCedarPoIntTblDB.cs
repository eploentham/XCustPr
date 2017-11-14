using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustCedarPoIntTblDB
    {
        XcustCedarPoIntTbl xCCPIT;
        ConnectDB conn;
        public XcustCedarPoIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCCPIT = new XcustCedarPoIntTbl();
            xCCPIT.account_segment1 = "account_segment1";
            xCCPIT.account_segment2 = "account_segment2";
            xCCPIT.account_segment3 = "account_segment3";
            xCCPIT.account_segment4 = "account_segment4";
            xCCPIT.account_segment5 = "account_segment5";
            xCCPIT.account_segment6 = "account_segment6";
            xCCPIT.account_segment_no = "account_segment_no";
            xCCPIT.admin = "admin";
            xCCPIT.admin_receipt_doc_date = "admin_receipt_doc_date";
            xCCPIT.amt = "amt";
            xCCPIT.approve_date = "approce_date";
            xCCPIT.asset_code = "asset_code";
            xCCPIT.asset_name = "asset_name";
            xCCPIT.branch_plant = "branch_plant";
            xCCPIT.category_name = "category_name";
            xCCPIT.cedar_close_date = "cedar_close_date";
            xCCPIT.creation_by = "creation_by";
            xCCPIT.creation_date = "creation_date";
            xCCPIT.data_source = "data_source";
            xCCPIT.error_message = "error_message";
            xCCPIT.file_name = "file_name";
            xCCPIT.invoice_due_date = "invoice_dur_date";
            xCCPIT.item_description = "item_description";
            xCCPIT.item_e1 = "item_e1";
            xCCPIT.last_update_by = "last_update_by";
            xCCPIT.last_update_date = "last_update_date";
            xCCPIT.loctype = "loctype";
            xCCPIT.payment_term = "payment_term";
            xCCPIT.period = "period";
            xCCPIT.po_no = "po_no";
            xCCPIT.process_flag = "process_flag";
            xCCPIT.qt_no = "qt_no";
            xCCPIT.shippto_location = "shippto_location";
            xCCPIT.supplier_code = "supplier_code";
            xCCPIT.supplier_contact = "supplier_contact";
            xCCPIT.supplier_name = "supplier_name";
            xCCPIT.supplier_site_code = "supplier_site_code";
            xCCPIT.sup_agreement_no = "sup_agreement_no";
            xCCPIT.total = "total";
            xCCPIT.validate_flag = "validate_flag";
            xCCPIT.vat = "vat";
            xCCPIT.week = "week";
            xCCPIT.work_type = "work_type";
            xCCPIT.wo_no = "wo_no";
            xCCPIT.xno = "xno";

            xCCPIT.table = "";
        }
        public void insertBluk(List<String> cedar, String filename, String host, MaterialProgressBar pB1)
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
            pB1.Maximum = cedar.Count();
            using (SqlCommand mConnection = new SqlCommand(ConnectionString))
            {
                List<string> Rows = new List<string>();
                foreach (String bbb in cedar)
                {
                    String account_segment1 = "", account_segment2 = "", account_segment3 = "", account_segment4 = "", account_segment5 = "", account_segment6 = "", account_segment_no = "", admin = "";
                    String admin_receipt_doc_date = "", amt = "", approve_date = "", asset_code = "", asset_name = "0", branch_plant = "", category_name = "", cedar_close_date = "0";
                    String creation_by = "", creation_date = "getdate()", data_source = "", error_message = "", file_name = "", invoice_due_date = "", item_description = "", item_e1 = "";
                    String last_update_by = "", last_update_date = "", loctype = "", payment_term = "", period = "", po_no = "", process_flag = "", qt_no = "";
                    String shippto_location = "", supplier_code = "", supplier_contact = "", supplier_name = "", supplier_site_code = "", sup_agreement_no = "", total = "", validate_flag = "";
                    String vat = "", week = "", work_type = "", wo_no = "", xno = "";
                    String erp_qty = "0", erp_uom = "";
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    String[] aaa = bbb.Split(',');
                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    store_cocde = aaa[0];
                    item_code = aaa[1];
                    date_of_record = conn.dateYearShortToDBTemp(aaa[2]);
                    RECEIVE_QTY = aaa[3].Trim();
                    INVOICE_NO = aaa[4];
                    INVOICE_AMT = aaa[5];
                    supplier_code = aaa[6];
                    //bbb += "('" + aaa[0] + "','" +
                    //aaa[11] + "','" + errMsg + "','" + aaa[6] + "','" +
                    //aaa[2] + "','" + aaa[4] + "','" + aaa[5] + "','" +
                    //aaa[1] + "','" + processFlag + "','" + aaa[7] + "','" +
                    //aaa[3] + "','" + aaa[8] + "','" + validateFlag + "'),";
                    sql.Append("Insert Into ").Append(xCCPIT.table).Append(" (").Append(xCCPIT.account_segment1).Append(",").Append(xCCPIT.account_segment2).Append(",").Append(xCCPIT.account_segment3)
                        .Append(",").Append(xCCPIT.account_segment4).Append(",").Append(xCCPIT.account_segment5).Append(",").Append(xCCPIT.account_segment6)
                        .Append(",").Append(xCCPIT.account_segment_no).Append(",").Append(xCCPIT.admin).Append(",").Append(xCCPIT.admin_receipt_doc_date)
                        .Append(",").Append(xCCPIT.amt).Append(",").Append(xCCPIT.approve_date).Append(",").Append(xCCPIT.asset_code)
                        .Append(",").Append(xCCPIT.asset_name).Append(",").Append(xCCPIT.branch_plant).Append(",").Append(xCCPIT.category_name)
                        .Append(",").Append(xCCPIT.cedar_close_date).Append(",").Append(xCCPIT.creation_by).Append(",").Append(xCCPIT.creation_date)
                        .Append(",").Append(xCCPIT.data_source).Append(",").Append(xCCPIT.error_message).Append(",").Append(xCCPIT.file_name)
                        .Append(",").Append(xCCPIT.invoice_due_date).Append(",").Append(xCCPIT.item_description).Append(",").Append(xCCPIT.item_e1)
                        .Append(",").Append(xCCPIT.last_update_by).Append(",").Append(xCCPIT.last_update_date).Append(",").Append(xCCPIT.loctype)
                        .Append(",").Append(xCCPIT.payment_term).Append(",").Append(xCCPIT.period).Append(",").Append(xCCPIT.po_no)
                        .Append(",").Append(xCCPIT.process_flag).Append(",").Append(xCCPIT.qt_no).Append(",").Append(xCCPIT.shippto_location)
                        .Append(",").Append(xCCPIT.supplier_code).Append(",").Append(xCCPIT.supplier_contact).Append(",").Append(xCCPIT.supplier_name)
                        .Append(",").Append(xCCPIT.supplier_site_code).Append(",").Append(xCCPIT.sup_agreement_no).Append(",").Append(xCCPIT.total)
                        .Append(",").Append(xCCPIT.validate_flag).Append(",").Append(xCCPIT.vat).Append(",").Append(xCCPIT.week)
                        .Append(",").Append(xCCPIT.work_type).Append(",").Append(xCCPIT.wo_no).Append(",").Append(xCCPIT.xno)                        
                        .Append(") Values ('")
                        .Append(account_segment1).Append("',").Append(account_segment2).Append(",'").Append(account_segment3)
                        .Append("','").Append(account_segment4).Append("','").Append(account_segment5).Append("','").Append(account_segment6)
                        .Append("','").Append(account_segment_no).Append("','").Append(admin).Append("','").Append(admin_receipt_doc_date)
                        .Append("','").Append(amt).Append("','").Append(approve_date).Append("','").Append(asset_code)
                        .Append("','").Append(asset_name).Append("','").Append(branch_plant).Append("','").Append(category_name)
                        .Append("','").Append(cedar_close_date).Append("','").Append(creation_by).Append("','").Append(creation_date)
                        .Append("','").Append(data_source).Append("','").Append(error_message).Append("','").Append(filename.Trim().Replace(initC.PO004PathProcess, ""))
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(supplier_code).Append("','").Append(validate_flag).Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host);
                }
            }
        }
    }
}
