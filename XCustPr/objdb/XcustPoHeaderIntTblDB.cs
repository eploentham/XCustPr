using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoHeaderIntTblDB
    {
        public XcustPoHeaderIntTbl xCPHIT;
        ConnectDB conn;

        public XcustPoHeaderIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPHIT = new XcustPoHeaderIntTbl();
            xCPHIT.acceptance_required_flag = "acceptance_required_flag";
            xCPHIT.action = "action";
            xCPHIT.approval_action = "approval_action";
            xCPHIT.bill_to_location = "bill_to_location";
            xCPHIT.buyyer_name = "buyyer_name";
            xCPHIT.creation_by = "creation_by";
            xCPHIT.creation_date = "creation_date";
            xCPHIT.currency_code = "currency_code";
            xCPHIT.document_num = "document_num";
            xCPHIT.document_typre_code = "document_type_code";
            xCPHIT.error_message = "error_message";
            xCPHIT.import_source = "import_source";
            xCPHIT.interface_header_key = "interface_header_key";
            xCPHIT.last_update_by = "last_update_by";
            xCPHIT.last_update_date = "last_update_date";
            xCPHIT.originator_rule = "originator_rule";
            xCPHIT.payment_term = "payment_term";
            xCPHIT.prc_bu_name = "prc_bu_name";
            xCPHIT.process_flag = "process_flag";
            xCPHIT.req_bu_name = "req_bu_name";
            xCPHIT.ship_to_location = "ship_to_location";
            xCPHIT.soldto_re_name = "soldto_re_name";
            xCPHIT.supplier_code = "supplier_code";
            xCPHIT.supplier_site_code = "supplier_site_code";
            xCPHIT.vendor_contact = "vendor_contact";
            xCPHIT.billto_bu_name = "billto_bu_name";
            xCPHIT.DOCUMENT_ID = "DOCUMENT_ID";
            xCPHIT.wo_no = "wo_no";
            xCPHIT.qt_no = "qt_no";
            xCPHIT.request_id = "request_id";
            xCPHIT.description = "description";

            xCPHIT.table = "XCUST_PO_HEADER_INT_TBL";

        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPHIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectByRequestId(String request_id)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPHIT.table+" Where "+xCPHIT.request_id+"='"+request_id+"'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String genSeqReqHeaderNumber()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "SELECT next value for xcust_po_header_seq ;";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString().Trim();
            }
            return chk;
        }
        public String insert(XcustPoHeaderIntTbl p, String pathLog)
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
                String seqH = genSeqReqHeaderNumber();
                p.interface_header_key = seqH;
                sql = "Insert Into " + xCPHIT.table + "(" + xCPHIT.acceptance_required_flag + "," + xCPHIT.action + "," + xCPHIT.approval_action + "," +
                    xCPHIT.bill_to_location + "," + xCPHIT.buyyer_name + "," + xCPHIT.creation_by + "," +
                    xCPHIT.creation_date + "," + xCPHIT.currency_code + "," + xCPHIT.document_num + "," +
                    xCPHIT.document_typre_code + "," + xCPHIT.error_message + "," + xCPHIT.import_source + "," +
                    xCPHIT.interface_header_key + "," + xCPHIT.last_update_by + "," + xCPHIT.last_update_date + "," +
                    xCPHIT.originator_rule + "," + xCPHIT.payment_term + "," + xCPHIT.prc_bu_name + "," +
                    xCPHIT.process_flag + "," + xCPHIT.req_bu_name + "," + xCPHIT.ship_to_location + "," +
                    xCPHIT.soldto_re_name + "," + xCPHIT.supplier_code + "," + xCPHIT.supplier_site_code + "," +
                    xCPHIT.vendor_contact + "," + xCPHIT.billto_bu_name + "," + xCPHIT.DOCUMENT_ID + "," + 
                    xCPHIT.wo_no + "," + xCPHIT.qt_no + "," + xCPHIT.request_id + "," + xCPHIT.description +
                    ") " +
                    "Values('" + p.acceptance_required_flag + "','" + p.action + "','" + p.approval_action + "','" +
                    p.bill_to_location + "','" + p.buyyer_name + "','" + creation_by + "'," +
                    "getdate(),'" + p.currency_code + "','" + p.document_num + "','" +
                    p.document_typre_code + "','" + p.error_message + "','" + p.import_source + "','" +
                    p.interface_header_key + "','" + last_update_by + "',null,'" +
                    p.originator_rule + "','" + p.payment_term + "','" + p.prc_bu_name + "','" +
                    p.process_flag + "','" + p.req_bu_name + "','" + p.ship_to_location + "','" +
                    p.soldto_re_name + "','" + p.supplier_code + "','" + p.supplier_site_code + "','" +
                    p.vendor_contact + "','" + p.billto_bu_name + "','" + p.DOCUMENT_ID + "','" + 
                    p.wo_no + "','" + p.qt_no + "','" + p.request_id + "','" + p.description + "'" +
                    ") ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
                if (chk.Equals("1"))
                {
                    chk = seqH;
                }
                //chk = p.RowNumber;
                //chk = p.Code;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }

            return chk;
        }
        public String updateDocumentId(String documentId, String requestId)
        {
            String chk = "", sql="";

            try
            {
                sql = "Update "+xCPHIT.table+" Set " +
                    "document_id ='"+ documentId+"' " +
                    "Where "+xCPHIT;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }
            return chk;
        }
    }
}
