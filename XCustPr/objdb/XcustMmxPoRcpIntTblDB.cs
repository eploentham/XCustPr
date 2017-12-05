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
        public XcustMmxPoRcpIntTbl xCMPoRIT;
        ConnectDB conn;
        private InitC initC;

        public XcustMmxPoRcpIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCMPoRIT = new XcustMmxPoRcpIntTbl();
            xCMPoRIT.creation_by = "creation_by";
            xCMPoRIT.creation_date = "creation_date";
            xCMPoRIT.date_of_record = "date_of_record";
            xCMPoRIT.erp_item_code = "erp_item_code";
            xCMPoRIT.erp_locator = "erp_locator";
            xCMPoRIT.erp_qty = "erp_qty";
            xCMPoRIT.erp_subinventory_code = "erp_subinventory_code";
            xCMPoRIT.erp_supplier_code = "erp_supplier_code";
            xCMPoRIT.erp_supplier_site_code = "erp_supplier_site_code";
            xCMPoRIT.erp_uom = "erp_uom";
            xCMPoRIT.error_message = "error_message";
            xCMPoRIT.file_name = "file_name";
            xCMPoRIT.INVOICE_AMT = "INVOICE_AMT";
            xCMPoRIT.INVOICE_NO = "INVOICE_NO";
            xCMPoRIT.item_code = "item_code";
            xCMPoRIT.last_update_by = "last_update_by";
            xCMPoRIT.last_update_date = "last_update_date";
            xCMPoRIT.lot_number = "lot_number";
            xCMPoRIT.po_line_number = "po_line_number";
            xCMPoRIT.po_number = "po_number";
            xCMPoRIT.process_flag = "process_flag";
            xCMPoRIT.RECEIVE_QTY = "RECEIVE_QTY";
            xCMPoRIT.serial_number = "serial_number";
            xCMPoRIT.store_code = "store_cocde";
            xCMPoRIT.supplier_code = "supplier_code";
            xCMPoRIT.validate_flag = "validate_flag";

            xCMPoRIT.table = "xcust_mmx_po_rcp_int_tbl";
            xCMPoRIT.pkField = "";
        }
        public void DeleteMmxTemp(String pathLog)
        {
            String sql = "Delete From " + xCMPoRIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPoRIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCMPoRIT.file_name + " From " + xCMPoRIT.table + " Group By " + xCMPoRIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPoRIT.table + " Where " + xCMPoRIT.file_name + "='" + filename + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void updateValidate(String store_code, String item_code, String invoice_no, String file_name, String flag, String message, String pathLog)
        {
            String sql = "";
            sql = "Update "+xCMPoRIT.table+" "+
                "Set "+xCMPoRIT.validate_flag+"='" +flag+"' "+
                "Where "+xCMPoRIT.store_code+"='"+store_code+"' and "+xCMPoRIT.item_code+"='"+item_code+"' and "+xCMPoRIT.INVOICE_NO+"='"+invoice_no+"' and "+xCMPoRIT.file_name+"='"+file_name+"'";
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
        }
        public DataTable gePOReceipt(String ITEM_CODE, String SUPPLIER_NUMBER)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select po.SEGMENT1, po.PO_HEADER_ID as PO_NUMBER /*po.PO_NUMBER*/,PO.LINE_NUM,po.quantity " +
                "from XCUST_PO_TBL PO " +
                "Left Join xcust_pr_TBL pr On PO.REQUISITION_HEADER_ID = pr.REQUISITION_HEADER_ID and PO.REQUISITION_LINE_ID = pr.REQUISITION_LINE_ID " +
                "Left Join xcust_item_mst_tbl msi On PO.ITEM_ID = msi.INVENTORY_ITEM_ID " +
                "Left Join xcust_organization_mst_tbl org On msi.ORGAINZATION_ID = org.ORGANIZATION_ID " +
                "Left Join xcust_supplier_mst_tbl sup On PO.VENDOR_ID = sup.VENDOR_ID and sup.ATTRIBUTE1 = 'Y' " +
                "Where PO.DOCUMENT_STATUS = 'OPEN' and PO.LINE_STATUS = 'OPEN'   " +                
                "and org.INVENTORY_FLAG = 'Y' and msi.ITEM_CATEGORY_NAME = 'Finish Goods' " +
                "and msi.ITEM_CODE = '"+ ITEM_CODE + "' and sup.SUPPLIER_NUMBER = '"+ SUPPLIER_NUMBER + "' " +
                "Order By PO.PO_HEADER_ID ,PO.PO_LINE_ID ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void insertBluk(List<String> linfox, String filename, String host, MaterialProgressBar pB1, String pathLog)
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
                    String store_cocde = "", item_code = "", date_of_record = "", RECEIVE_QTY = "", INVOICE_NO = "", INVOICE_AMT = "", supplier_code = "", lot_number = "";
                    String serial_number = "", validate_flag = "", process_flag = "", error_message = "", creation_by = "0", creation_date = "getdate()", last_update_date = "", last_update_by = "0";
                    String file_name = "", erp_supplier_code = "", erp_supplier_site_code = "", erp_item_code = "", po_number = "", po_line_number = "", erp_subinventory_code = "", erp_locator = "";
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
                    sql.Append("Insert Into ").Append(xCMPoRIT.table).Append(" (").Append(xCMPoRIT.creation_by).Append(",").Append(xCMPoRIT.creation_date).Append(",").Append(xCMPoRIT.date_of_record)
                        .Append(",").Append(xCMPoRIT.erp_item_code).Append(",").Append(xCMPoRIT.erp_locator).Append(",").Append(xCMPoRIT.erp_qty)
                        .Append(",").Append(xCMPoRIT.erp_subinventory_code).Append(",").Append(xCMPoRIT.erp_supplier_code).Append(",").Append(xCMPoRIT.erp_supplier_site_code)
                        .Append(",").Append(xCMPoRIT.erp_uom).Append(",").Append(xCMPoRIT.error_message).Append(",").Append(xCMPoRIT.file_name)
                        .Append(",").Append(xCMPoRIT.INVOICE_AMT).Append(",").Append(xCMPoRIT.INVOICE_NO).Append(",").Append(xCMPoRIT.item_code)
                        .Append(",").Append(xCMPoRIT.last_update_by).Append(",").Append(xCMPoRIT.last_update_date).Append(",").Append(xCMPoRIT.lot_number)
                        .Append(",").Append(xCMPoRIT.po_line_number).Append(",").Append(xCMPoRIT.po_number).Append(",").Append(xCMPoRIT.process_flag)
                        .Append(",").Append(xCMPoRIT.RECEIVE_QTY).Append(",").Append(xCMPoRIT.serial_number).Append(",").Append(xCMPoRIT.store_code)
                        .Append(",").Append(xCMPoRIT.supplier_code).Append(",").Append(xCMPoRIT.validate_flag)
                        .Append(") Values ('")
                        .Append(creation_by).Append("',").Append(creation_date).Append(",'").Append(date_of_record)
                        .Append("','").Append(erp_item_code).Append("','").Append(erp_locator).Append("','").Append(erp_qty)
                        .Append("','").Append(erp_subinventory_code).Append("','").Append(erp_supplier_code).Append("','").Append(erp_supplier_site_code)
                        .Append("','").Append(erp_uom).Append("','").Append(validateFlag).Append("','").Append(filename.Trim().Replace(initC.PO004PathProcess, ""))
                        .Append("','").Append(INVOICE_AMT).Append("','").Append(INVOICE_NO).Append("','").Append(item_code)
                        .Append("','").Append(last_update_by).Append("','").Append(last_update_date).Append("','").Append(lot_number)
                        .Append("','").Append(po_line_number).Append("','").Append(po_number).Append("','").Append(process_flag)
                        .Append("','").Append(RECEIVE_QTY).Append("','").Append(lastUpdateTime).Append("','").Append(store_cocde)
                        .Append("','").Append(supplier_code).Append("','").Append(validate_flag).Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host, pathLog);
                }
            }
        }
    }
}
