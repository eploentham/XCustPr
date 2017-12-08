using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustMmxPrRcpIntTblDB
    {
        public XcustMmxPrRcpIntTbl xCMPrRIT;
        ConnectDB conn;
        private InitC initC;
        public XcustMmxPrRcpIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
        }
        private void initConfig()
        {
            xCMPrRIT = new XcustMmxPrRcpIntTbl();
            xCMPrRIT.ACC_SEG1 = "ACC_SEG1";
            xCMPrRIT.ACC_SEG2 = "ACC_SEG2";
            xCMPrRIT.ACC_SEG3 = "ACC_SEG3";
            xCMPrRIT.ACC_SEG4 = "ACC_SEG4";
            xCMPrRIT.ACC_SEG5 = "ACC_SEG5";
            xCMPrRIT.ACC_SEG6 = "ACC_SEG6";
            xCMPrRIT.AGREEEMENT_NUMBER = "AGREEEMENT_NUMBER";
            xCMPrRIT.AGREEMENT_LINE_NUMBER = "AGREEMENT_LINE_NUMBER";
            xCMPrRIT.confirm_qty = "confirm_qty";
            xCMPrRIT.conf_delivery_date = "conf_delivery_date";
            xCMPrRIT.creation_by = "creation_by";
            xCMPrRIT.creation_date = "creation_date";
            xCMPrRIT.delivery_date = "delivery_date";
            xCMPrRIT.DELIVERY_INSTRUCTION = "DELIVERY_INSTRUCTION";
            xCMPrRIT.deriver_to_location = "deriver_to_location";
            xCMPrRIT.diriver_to_organization = "diriver_to_organization";
            xCMPrRIT.ERP_ITEM_CODE = "ERP_ITEM_CODE";
            xCMPrRIT.erp_subinventory_code = "erp_subinventory_code";
            xCMPrRIT.error_message = "error_message";
            xCMPrRIT.file_name = "file_name";
            xCMPrRIT.ITEM_CATEGORY_NAME = "ITEM_CATEGORY_NAME";
            xCMPrRIT.item_code = "item_code";
            xCMPrRIT.last_update_by = "last_update_by";
            xCMPrRIT.last_update_date = "last_update_date";
            xCMPrRIT.order_date = "order_date";
            xCMPrRIT.order_qty = "order_qty";
            xCMPrRIT.po_number = "po_number";
            xCMPrRIT.po_status = "po_status";
            xCMPrRIT.PRICE = "PRICE";
            xCMPrRIT.process_flag = "process_flag";
            xCMPrRIT.request_date = "request_date";
            xCMPrRIT.store_code = "store_code";
            xCMPrRIT.subinventory_code = "subinventory_code";
            xCMPrRIT.supplier_code = "supplier_code";
            xCMPrRIT.SUPPLIER_SITE_CODE = "SUPPLIER_SITE_CODE";
            xCMPrRIT.uom_code = "uom_code";
            xCMPrRIT.Validate_flag = "Validate_flag";

            xCMPrRIT.table = "xcust_mmx_pr_int_tbl";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPrRIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void DeleteMmxTemp(String pathLog)
        {
            String sql = "Delete From " + xCMPrRIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
        }
        public void insertBluk(List<String> mmx, String filename, String host, MaterialProgressBar pB1)
        {
            //int i = 0;
            //TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            //String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            //String time = System.DateTime.Now.ToString("HH:mm:ss");

            //String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", lastUpdateBy = "0", lastUpdateTime = "null";
            //if (host == "kfc_po")
            //{
            //    ConnectionString = conn.connKFC.ConnectionString;
            //}
            //StringBuilder sql = new StringBuilder();
            //pB1.Minimum = 1;
            //pB1.Maximum = mmx.Count();
            //using (SqlCommand mConnection = new SqlCommand(ConnectionString))
            //{
                
            //    List<string> Rows = new List<string>();
            //    foreach (String bbb in mmx)
            //    {
            //        i++;
            //        sql.Clear();
            //        pB1.Value = i;
            //        String[] aaa = bbb.Split('|');
            //        String store_code = aaa[0], po_number = "", supplier_code = "", delivery_date = "", subinventory_code = "", order_qty = "", confirm_qty = "";
            //        String item_code = aaa[1], order_date = "", po_status = "", conf_delivery_date = "", Validate_flag = "", process_flag = "", error_message = "", creation_by = "";
            //        String creation_date = "", last_update_date = "", last_update_by = "", file_name = "", request_date = "", diriver_to_organization = "", deriver_to_location = "";
            //        String erp_subinventory_code = "", ERP_ITEM_CODE = "", AGREEEMENT_NUMBER = "", AGREEMENT_LINE_NUMBER = "", PRICE = "", ITEM_CATEGORY_NAME = "", SUPPLIER_SITE_CODE = "";
            //        String ACC_SEG1 = "", ACC_SEG2 = "", ACC_SEG3 = "", ACC_SEG4 = "", ACC_SEG5 = "", ACC_SEG6 = "", uom_code = "", DELIVERY_INSTRUCTION = "";
            //        errMsg = "";
            //        processFlag = "N";
            //        validateFlag = "N";
            //        order_date = conn.dateYearShortToDBTemp(aaa[2]);        //DATE_OF_RECORD
            //        order_qty = aaa[3];     //RECEIVE_QTY
            //        po_number = aaa[4];     //INVOICE_NUMBER    INVOICE_AMOUNT

            //        sql.Append("Insert Into ").Append(xCMPrRIT.table).Append(" (").Append(xCMPrRIT.ACC_SEG1).Append(",").Append(xCMPrRIT.ACC_SEG2).Append(",").Append(xCMPrRIT.ACC_SEG3)
            //            .Append(",").Append(xCMPrRIT.ACC_SEG4).Append(",").Append(xCMPrRIT.ACC_SEG5).Append(",").Append(xCMPrRIT.ACC_SEG6)
            //            .Append(",").Append(xCMPrRIT.AGREEEMENT_NUMBER).Append(",").Append(xCMPrRIT.AGREEMENT_LINE_NUMBER).Append(",").Append(xCMPrRIT.confirm_qty)
            //            .Append(",").Append(xCMPrRIT.conf_delivery_date).Append(",").Append(xCMPrRIT.creation_by).Append(",").Append(xCMPrRIT.creation_date)
            //            .Append(",").Append(xCMPrRIT.delivery_date).Append(",").Append(xCMPrRIT.DELIVERY_INSTRUCTION).Append(",").Append(xCMPrRIT.deriver_to_location)
            //            .Append(",").Append(xCMPrRIT.diriver_to_organization).Append(",").Append(xCMPrRIT.ERP_ITEM_CODE).Append(",").Append(xCMPrRIT.erp_subinventory_code)
            //            .Append(",").Append(xCMPrRIT.error_message).Append(",").Append(xCMPrRIT.file_name).Append(",").Append(xCMPrRIT.ITEM_CATEGORY_NAME)
            //            .Append(",").Append(xCMPrRIT.item_code).Append(",").Append(xCMPrRIT.last_update_by).Append(",").Append(xCMPrRIT.last_update_date)
            //            .Append(",").Append(xCMPrRIT.order_date).Append(",").Append(xCMPrRIT.order_qty).Append(",").Append(xCMPrRIT.po_number)
            //            .Append(",").Append(xCMPrRIT.po_status).Append(",").Append(xCMPrRIT.PRICE).Append(",").Append(xCMPrRIT.process_flag)
            //            .Append(",").Append(xCMPrRIT.request_date).Append(",").Append(xCMPrRIT.store_code).Append(",").Append(xCMPrRIT.subinventory_code)
            //            .Append(",").Append(xCMPrRIT.supplier_code).Append(",").Append(xCMPrRIT.SUPPLIER_SITE_CODE).Append(",").Append(xCMPrRIT.uom_code)
            //            .Append(",").Append(xCMPrRIT.Validate_flag)
            //            .Append(") Values ('")
            //            .Append(ACC_SEG1).Append("','").Append(ACC_SEG2).Append("','").Append(ACC_SEG3)
            //            .Append("','").Append(ACC_SEG4).Append("',").Append(ACC_SEG5).Append(",'").Append(ACC_SEG6)
            //            .Append("','").Append(AGREEEMENT_NUMBER).Append("',").Append(AGREEMENT_LINE_NUMBER).Append(",'").Append(confirm_qty)
            //            .Append("','").Append(conf_delivery_date).Append("','").Append(creation_by).Append("','").Append(creation_date)
            //            .Append("','").Append(delivery_date).Append("','").Append(DELIVERY_INSTRUCTION).Append("',").Append(deriver_to_location)
            //            .Append(",'").Append(diriver_to_organization).Append("',").Append(ERP_ITEM_CODE).Append(",'").Append(erp_subinventory_code)
            //            .Append("','").Append(error_message).Append("','").Append(filename.Trim().Replace(initC.PO004PathProcess, "")).Append("',").Append(ITEM_CATEGORY_NAME)
            //            .Append("','").Append(item_code).Append("','").Append(last_update_by).Append("',").Append(last_update_date)
            //            .Append("','").Append(order_date).Append("','").Append(order_qty).Append("',").Append(po_number)
            //            .Append("','").Append(po_status).Append("','").Append(PRICE).Append("',").Append(process_flag)
            //            .Append("','").Append(request_date).Append("','").Append(store_code).Append("',").Append(subinventory_code)
            //            .Append("','").Append(supplier_code).Append("','").Append(SUPPLIER_SITE_CODE).Append("',").Append(uom_code)
            //            .Append("','").Append(Validate_flag).Append("') ");
            //        conn.ExecuteNonQuery(sql.ToString(), host);
            //    }
            //}
        }
    }
}
