using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustLinfoxRcvPrIntTblDB
    {
        public XcustLinfoxRcvPrIntTbl xCLRPIT;
        ConnectDB conn;
        private InitC initC;

        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();
        public XcustLinfoxRcvPrIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCLRPIT = new XcustLinfoxRcvPrIntTbl();
            xCLRPIT.branch_plant = "branch_plant";
            xCLRPIT.company_code = "company_code";
            xCLRPIT.creation_by = "creation_by";
            xCLRPIT.creation_date = "creation_date";
            xCLRPIT.DOCUMENT_LINE_NUMBER = "DOCUMENT_LINE_NUMBER";
            xCLRPIT.DOCUMENT_NUMBER = "DOCUMENT_NUMBER";
            xCLRPIT.doc_type = "doc_type";
            xCLRPIT.ERP_ITEM_CODE = "ERP_ITEM_CODE";
            xCLRPIT.error_message = "error_message";
            xCLRPIT.file_name = "file_name";
            xCLRPIT.item_code = "item_code";
            xCLRPIT.last_update_by = "last_update_by";
            xCLRPIT.last_update_date = "last_update_date";
            xCLRPIT.line_number = "line_number";
            xCLRPIT.location = "location";
            xCLRPIT.LOCATOR = "LOCATOR";
            xCLRPIT.lot_expire_date = "lot_expire_date";
            xCLRPIT.lot_number = "lot_number";
            xCLRPIT.order_company = "order_company";
            xCLRPIT.order_number = "order_number";
            xCLRPIT.process_flag = "process_flag";
            xCLRPIT.qty_receipt = "qty_receipt";
            xCLRPIT.reason_code = "reason_code";
            xCLRPIT.receipt_date = "receipt_date";
            xCLRPIT.receipt_time = "receipt_time";
            xCLRPIT.reference1 = "reference1";
            xCLRPIT.subinventory_code = "subinventory_code";
            xCLRPIT.supplier_code = "supplier_code";
            xCLRPIT.SUPPLIER_SITE_CODE = "SUPPLIER_SITE_CODE";
            xCLRPIT.uom_code = "uom_code";
            xCLRPIT.uom_code1 = "uom_code1";
            xCLRPIT.uom_code2 = "uom_code2";
            xCLRPIT.validate_flag = "validate_flag";
            xCLRPIT.vendor_remark = "vendor_remark";
            

            xCLRPIT.table = "xcust_linfox_po_rcp_int_tbl";
            xCLRPIT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLRPIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCLRPIT.file_name + " From " + xCLRPIT.table + " Group By " + xCLRPIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLRPIT.table + " Where " + xCLRPIT.file_name + "='" + filename + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void DeleteMmxTemp()
        {
            String sql = "Delete From " + xCLRPIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public String dateYearShortToDB(String date)
        {
            String chk = "", year = "", month = "", day = "";

            year = date.Substring(date.Length - 2);
            day = date.Substring(3, 2);
            month = date.Substring(0, 2);

            chk = "20" + year + "-" + month + "-" + day;

            return chk;
        }
        public void insertBluk(List<String> rcv, String filename, String host, MaterialProgressBar pB1)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", last_update_by = "0", lastUpdateTime = "null";
            
            if (host == "kfc_po")
            {
                ConnectionString = conn.connKFC.ConnectionString;
            }
            StringBuilder sql = new StringBuilder();
            pB1.Minimum = 1;
            pB1.Maximum = rcv.Count();
            using (SqlCommand mConnection = new SqlCommand(ConnectionString))
            {
                List<string> Rows = new List<string>();
                foreach (String bbb in rcv)
                {
                    String[] aaa = bbb.Split('|');

                    String branch_plant = aaa[1], company_code = aaa[0], creation_by = "0", creation_date = "", DOCUMENT_LINE_NUMBER = "", DOCUMENT_NUMBER = "";
                    String doc_type = aaa[3], ERP_ITEM_CODE = "", error_message = "", file_name = "", item_code = aaa[6];
                    String last_update_date = "", line_number = aaa[5], location = aaa[16], LOCATOR = "", lot_expire_date = aaa[10], lot_number = aaa[9];
                    String order_company = aaa[4], order_number = aaa[2], process_flag = "", qty_receipt = aaa[7], reason_code = aaa[15], receipt_date = aaa[13];
                    String receipt_time = aaa[14], reference1 = aaa[11], subinventory_code = "", supplier_code = "", SUPPLIER_SITE_CODE = "", uom_code = aaa[8];
                    String uom_code1 = "", uom_code2 = "", validate_flag = "", vendor_remark = aaa[12];
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    
                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    //receipt_date = validateDate(receipt_date);
                    //deliDate = dateYearShortToDB(aaa[3]);
                    //confDate = dateYearShortToDB(aaa[10]);
                    //bbb += "('" + aaa[0] + "','" +
                    //aaa[11] + "','" + errMsg + "','" + aaa[6] + "','" +
                    //aaa[2] + "','" + aaa[4] + "','" + aaa[5] + "','" +
                    //aaa[1] + "','" + processFlag + "','" + aaa[7] + "','" +
                    //aaa[3] + "','" + aaa[8] + "','" + validateFlag + "'),";
                    sql.Append("Insert Into ").Append(xCLRPIT.table).Append(" (").Append(xCLRPIT.branch_plant).Append(",").Append(xCLRPIT.company_code).Append(",").Append(xCLRPIT.creation_by)
                        .Append(",").Append(xCLRPIT.creation_date).Append(",").Append(xCLRPIT.DOCUMENT_LINE_NUMBER).Append(",").Append(xCLRPIT.DOCUMENT_NUMBER)
                        .Append(",").Append(xCLRPIT.doc_type).Append(",").Append(xCLRPIT.ERP_ITEM_CODE).Append(",").Append(xCLRPIT.error_message)
                        .Append(",").Append(xCLRPIT.file_name).Append(",").Append(xCLRPIT.item_code).Append(",").Append(xCLRPIT.last_update_by)
                        .Append(",").Append(xCLRPIT.last_update_date).Append(",").Append(xCLRPIT.line_number).Append(",").Append(xCLRPIT.location)
                        .Append(",").Append(xCLRPIT.LOCATOR).Append(",").Append(xCLRPIT.lot_expire_date).Append(",").Append(xCLRPIT.lot_number)
                        .Append(",").Append(xCLRPIT.order_company).Append(",").Append(xCLRPIT.order_number).Append(",").Append(xCLRPIT.process_flag)
                        .Append(",").Append(xCLRPIT.qty_receipt).Append(",").Append(xCLRPIT.reason_code).Append(",").Append(xCLRPIT.receipt_date)
                        .Append(",").Append(xCLRPIT.receipt_time).Append(",").Append(xCLRPIT.reference1).Append(",").Append(xCLRPIT.subinventory_code)
                        .Append(",").Append(xCLRPIT.supplier_code).Append(",").Append(xCLRPIT.SUPPLIER_SITE_CODE).Append(",").Append(xCLRPIT.uom_code)
                        .Append(",").Append(xCLRPIT.uom_code1).Append(",").Append(xCLRPIT.uom_code2).Append(",").Append(xCLRPIT.validate_flag)
                        .Append(",").Append(xCLRPIT.vendor_remark).Append(" ")
                        .Append(") Values ('")
                        .Append(branch_plant).Append("','").Append(company_code).Append("','").Append(creation_by)
                        .Append("','").Append(creation_date).Append("','").Append(DOCUMENT_LINE_NUMBER).Append("','").Append(DOCUMENT_NUMBER)
                        .Append("','").Append(doc_type).Append("','").Append(ERP_ITEM_CODE).Append("','").Append(error_message/*CONFIRM  QTY*/)
                        .Append("','").Append(file_name.Trim().Replace(initC.PO003PathProcess, "")/*CONF_DILIVERY_DATE*/).Append("','").Append(item_code).Append("','").Append(last_update_by)
                        .Append("','").Append(last_update_date/*delivery_date*/).Append("','").Append(line_number).Append("','").Append(location)
                        .Append("','").Append(LOCATOR).Append("','").Append(lot_expire_date).Append("','").Append(lot_number)
                        .Append("','").Append(order_company/*errMsg*/).Append("','").Append(order_number).Append("','").Append(process_flag)
                        .Append("','").Append(qty_receipt/*ITEM_CODE*/).Append("','").Append(reason_code).Append("',").Append(receipt_date)
                        .Append(",'").Append(receipt_time/*ORDER_DATE*/).Append("','").Append(reference1/*ORDER_QTY*/).Append("','").Append(subinventory_code/*PO_NUMBER*/)
                        .Append("','").Append(supplier_code/*.PO_STATUS*/).Append("','").Append(SUPPLIER_SITE_CODE).Append("','").Append(uom_code)
                        .Append("','").Append(uom_code1/*SUPPLIER_CODE*/).Append("','").Append(uom_code2).Append("','").Append(validate_flag)
                        .Append("','").Append(vendor_remark).Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host);
                }
            }
        }
        
    }
}
