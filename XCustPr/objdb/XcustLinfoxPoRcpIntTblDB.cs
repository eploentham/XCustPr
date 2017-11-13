using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustLinfoxPoRcpIntTblDB
    {
        public XcustLinfoxPoRcpIntTbl xCLPRIT;
        ConnectDB conn;
        private InitC initC;

        private StringBuilder sYear = new StringBuilder();
        private StringBuilder sMonth = new StringBuilder();
        private StringBuilder sDay = new StringBuilder();
        public XcustLinfoxPoRcpIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCLPRIT = new XcustLinfoxPoRcpIntTbl();
            xCLPRIT.branch_plant = "branch_plant";
            xCLPRIT.company_code = "company_code";
            xCLPRIT.creation_by = "creation_by";
            xCLPRIT.creation_date = "creation_date";
            xCLPRIT.DOCUMENT_LINE_NUMBER = "DOCUMENT_LINE_NUMBER";
            xCLPRIT.DOCUMENT_NUMBER = "DOCUMENT_NUMBER";
            xCLPRIT.doc_type = "doc_type";
            xCLPRIT.ERP_ITEM_CODE = "ERP_ITEM_CODE";
            xCLPRIT.error_message = "error_message";
            xCLPRIT.file_name = "file_name";
            xCLPRIT.item_code = "item_code";
            xCLPRIT.last_update_by = "last_update_by";
            xCLPRIT.last_update_date = "last_update_date";
            xCLPRIT.line_number = "line_number";
            xCLPRIT.location = "location";
            xCLPRIT.LOCATOR = "LOCATOR";
            xCLPRIT.lot_expire_date = "lot_expire_date";
            xCLPRIT.lot_number = "lot_number";
            xCLPRIT.order_company = "order_company";
            xCLPRIT.order_number = "order_number";
            xCLPRIT.process_flag = "process_flag";
            xCLPRIT.qty_receipt = "qty_receipt";
            xCLPRIT.reason_code = "reason_code";
            xCLPRIT.receipt_date = "receipt_date";
            xCLPRIT.receipt_time = "receipt_time";
            xCLPRIT.reference1 = "reference1";
            xCLPRIT.subinventory_code = "subinventory_code";
            xCLPRIT.supplier_code = "supplier_code";
            xCLPRIT.SUPPLIER_SITE_CODE = "SUPPLIER_SITE_CODE";
            xCLPRIT.uom_code = "uom_code";
            xCLPRIT.uom_code1 = "uom_code1";
            xCLPRIT.uom_code2 = "uom_code2";
            xCLPRIT.validate_flag = "validate_flag";
            xCLPRIT.vendor_remark = "vendor_remark";
            

            xCLPRIT.table = "xcust_linfox_po_rcp_int_tbl";
            xCLPRIT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLPRIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCLPRIT.file_name + " From " + xCLPRIT.table + " Group By " + xCLPRIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLPRIT.table + " Where " + xCLPRIT.file_name + "='" + filename + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void DeleteMmxTemp()
        {
            String sql = "Delete From " + xCLPRIT.table;
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
                    sql.Append("Insert Into ").Append(xCLPRIT.table).Append(" (").Append(xCLPRIT.branch_plant).Append(",").Append(xCLPRIT.company_code).Append(",").Append(xCLPRIT.creation_by)
                        .Append(",").Append(xCLPRIT.creation_date).Append(",").Append(xCLPRIT.DOCUMENT_LINE_NUMBER).Append(",").Append(xCLPRIT.DOCUMENT_NUMBER)
                        .Append(",").Append(xCLPRIT.doc_type).Append(",").Append(xCLPRIT.ERP_ITEM_CODE).Append(",").Append(xCLPRIT.error_message)
                        .Append(",").Append(xCLPRIT.file_name).Append(",").Append(xCLPRIT.item_code).Append(",").Append(xCLPRIT.last_update_by)
                        .Append(",").Append(xCLPRIT.last_update_date).Append(",").Append(xCLPRIT.line_number).Append(",").Append(xCLPRIT.location)
                        .Append(",").Append(xCLPRIT.LOCATOR).Append(",").Append(xCLPRIT.lot_expire_date).Append(",").Append(xCLPRIT.lot_number)
                        .Append(",").Append(xCLPRIT.order_company).Append(",").Append(xCLPRIT.order_number).Append(",").Append(xCLPRIT.process_flag)
                        .Append(",").Append(xCLPRIT.qty_receipt).Append(",").Append(xCLPRIT.reason_code).Append(",").Append(xCLPRIT.receipt_date)
                        .Append(",").Append(xCLPRIT.receipt_time).Append(",").Append(xCLPRIT.reference1).Append(",").Append(xCLPRIT.subinventory_code)
                        .Append(",").Append(xCLPRIT.supplier_code).Append(",").Append(xCLPRIT.SUPPLIER_SITE_CODE).Append(",").Append(xCLPRIT.uom_code)
                        .Append(",").Append(xCLPRIT.uom_code1).Append(",").Append(xCLPRIT.uom_code2).Append(",").Append(xCLPRIT.validate_flag)
                        .Append(",").Append(xCLPRIT.vendor_remark).Append(" ")
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
        public String updateValidateFlag(String po_number, String line_number, String flag, String agreement_number, String host)
        {
            String sql = "";
            //sql = "Update " + xCLRPIT.table + " Set " + xCLRPIT.Validate_flag + "='" + flag + "', " + xCLRPIT.AGREEEMENT_NUMBER + " ='" + agreement_number + "' " +
            //    "Where " + xCLRPIT.po_number + " = '" + po_number + "' and " + xCLRPIT.AGREEMENT_LINE_NUMBER + "='" + line_number + "'";
            //conn.ExecuteNonQuery(sql.ToString(), host);

            return "";
        }
    }
}
