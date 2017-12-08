using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustCedarPoIntTblDB
    {
        public XcustCedarPoIntTbl xCCPIT;
        ConnectDB conn;
        private InitC initC;
        public XcustCedarPoIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
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
            xCCPIT.account_segment_no = "account_segment";
            xCCPIT.admin = "admin";
            xCCPIT.admin_receipt_doc_date = "admin_receipt_doc_date";
            xCCPIT.amt = "amt";
            xCCPIT.approve_date = "approve_date";
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
            xCCPIT.invoice_due_date = "invoice_due_date";
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
            xCCPIT.shippto_location = "shipto_location";
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

            xCCPIT.table = "XCUST_CEDAR_PO_INT_TBL";
        }
        public void DeleteCedarTemp(string pathLog)
        {
            String sql = "Delete From " + xCCPIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
        }
        public DataTable selectCedarGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCCPIT.file_name + " From " + xCCPIT.table + " Group By " + xCCPIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectCedarByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCCPIT.table + " Where " + xCCPIT.file_name + "='" + filename + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void insertBluk(List<String> cedar, String filename, String host, MaterialProgressBar pB1, String pathLog)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", lastUpdateBy = "0", lastUpdateTime = "null";

            int colPO_NO = 2, colQT_NO = 3, colWO_NO = 4, colPERIOD = 5, colWeek = 6, colBRANCH_PLANT = 7, colBRANCH_NAME = 8, colLOCTYPE = 9, colITEM_E1 = 10, colASSET_CODE = 11, colASSET_NAME = 12, colWORK_TYPE = 13, colAMOUNT = 14;
            int colVAT = 15, colTOTAL = 16, colSUPPLIER_CODE = 17, colSUPPLIER_NAME = 18, colADMIN = 19, colADMIN_RECEIVE_DOC = 20, colAPPROVE_DATE = 21, colCEDAR_CLOSE_DATE = 22, colINVOICE_DUE_DATE = 23;
            int colSUPP_AGREEMENT_NO = 24, colACCOUNT_SEGMENT = 25, colDATA_SOURCE = 26;        //ต้องเหมือน Method ReadExcel

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
                    String data_source = "", error_message = "", invoice_due_date = "", item_description = "", item_e1 = "";
                    String loctype = "", payment_term = "", period = "", po_no = "", process_flag = "", qt_no = "";
                    String shippto_location = "", supplier_code = "", supplier_contact = "", supplier_name = "", supplier_site_code = "", sup_agreement_no = "", total = "", validate_flag = "";
                    String vat = "", week = "", work_type = "", wo_no = "", xno = "";
                    String erp_qty = "0", erp_uom = "", BRANCH_NAME="";
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    String[] aaa = bbb.Split('|');
                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    po_no = aaa[colPO_NO-2];// column ใน excel เลยต้อง -2
                    qt_no = aaa[colQT_NO - 2];// column ใน excel เลยต้อง -2
                    wo_no = aaa[colWO_NO - 2];// column ใน excel เลยต้อง -2
                    period = aaa[colPERIOD - 2];// column ใน excel เลยต้อง -2
                    week = aaa[colWeek - 2];// column ใน excel เลยต้อง -2
                    branch_plant = aaa[colBRANCH_PLANT - 2];// column ใน excel เลยต้อง -2
                    BRANCH_NAME = aaa[colBRANCH_NAME - 2];      // no field// column ใน excel เลยต้อง -2
                    loctype = aaa[colLOCTYPE - 2];// column ใน excel เลยต้อง -2
                    item_e1 = aaa[colITEM_E1 - 2];// column ใน excel เลยต้อง -2
                    asset_code = aaa[colASSET_CODE - 2];// column ใน excel เลยต้อง -2

                    asset_name = aaa[colASSET_NAME - 2];// column ใน excel เลยต้อง -2
                    work_type = aaa[colWORK_TYPE - 2];// column ใน excel เลยต้อง -2
                    amt = aaa[colAMOUNT - 2];// column ใน excel เลยต้อง -2
                    vat = aaa[colVAT - 2];// column ใน excel เลยต้อง -2
                    total = aaa[colTOTAL - 2];// column ใน excel เลยต้อง -2
                    supplier_code = aaa[colSUPPLIER_CODE - 2];// column ใน excel เลยต้อง -2
                    supplier_name = aaa[colSUPPLIER_NAME - 2];// column ใน excel เลยต้อง -2
                    admin = aaa[colADMIN - 2];// column ใน excel เลยต้อง -2
                    admin_receipt_doc_date = aaa[colADMIN_RECEIVE_DOC - 2];// column ใน excel เลยต้อง -2
                    approve_date = aaa[colAPPROVE_DATE - 2];

                    cedar_close_date = aaa[colCEDAR_CLOSE_DATE - 2];// column ใน excel เลยต้อง -2
                    invoice_due_date = aaa[colINVOICE_DUE_DATE - 2];// column ใน excel เลยต้อง -2
                    sup_agreement_no = aaa[colSUPP_AGREEMENT_NO - 2];       // no field
                    account_segment2 = aaa[colACCOUNT_SEGMENT - 2];// column ใน excel เลยต้อง -2
                    data_source = aaa[colDATA_SOURCE - 2];// column ใน excel เลยต้อง -2

                    admin_receipt_doc_date = dateYearShortToDB(admin_receipt_doc_date);
                    approve_date = dateYearShortToDB(approve_date);
                    cedar_close_date = dateYearShortToDB(cedar_close_date);
                    invoice_due_date = dateYearShortToDB(invoice_due_date);

                    //store_cocde = aaa[0];
                    //item_code = aaa[1];
                    //date_of_record = conn.dateYearShortToDBTemp(aaa[2]);
                    //RECEIVE_QTY = aaa[3].Trim();
                    //INVOICE_NO = aaa[4];
                    //INVOICE_AMT = aaa[5];
                    //supplier_code = aaa[6];
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
                        .Append(account_segment1).Append("','").Append(account_segment2).Append("','").Append(account_segment3)
                        .Append("','").Append(account_segment4).Append("','").Append(account_segment5).Append("','").Append(account_segment6)
                        .Append("','").Append(account_segment_no).Append("','").Append(admin).Append("','").Append(admin_receipt_doc_date)
                        .Append("',").Append(amt).Append(",'").Append(approve_date).Append("','").Append(asset_code)
                        .Append("','").Append(asset_name).Append("','").Append(branch_plant).Append("','").Append(category_name)
                        .Append("','").Append(cedar_close_date).Append("','").Append(createBy).Append("',").Append(createDate)
                        .Append(",'").Append(data_source).Append("','").Append(error_message).Append("','").Append(filename.Trim().Replace(initC.PO008PathProcess, ""))
                        .Append("','").Append(invoice_due_date).Append("','").Append(item_description).Append("','").Append(item_e1)
                        .Append("','").Append(lastUpdateBy).Append("',").Append(lastUpdateTime).Append(",'").Append(loctype)
                        .Append("','").Append(payment_term).Append("','").Append(period).Append("','").Append(po_no)
                        .Append("','").Append(process_flag).Append("','").Append(qt_no).Append("','").Append(shippto_location)
                        .Append("','").Append(supplier_code).Append("','").Append(supplier_contact).Append("','").Append(supplier_name)
                        .Append("','").Append(supplier_site_code).Append("','").Append(lastUpdateTime).Append("',").Append(total)
                        .Append(",'").Append(validate_flag).Append("',").Append(vat).Append(",'").Append(week)
                        .Append("','").Append(work_type).Append("','").Append(wo_no).Append("','").Append(xno)                        
                        .Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host, pathLog);
                }
            }
        }
        public String dateYearShortToDB(String date)
        {
            String chk = "", year = "", month = "", day = "";
            //String[] txt = date.Split('/');
            if (date.Length >= 10)
            {
                year = date.Substring(date.Length - 4);
                month = date.Substring(3, 2);
                day = date.Substring(0, 2);

                chk = year + "-" + month + "-" + day;
            }
            else
            {
                chk = date;
            }
            

            return chk;
        }
    }
}
