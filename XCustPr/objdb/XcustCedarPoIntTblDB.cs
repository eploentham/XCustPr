using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            xCCPIT.row_cnt = "row_cnt";
            xCCPIT.row_number = "row_number";
            xCCPIT.request_id = "request_id";
            xCCPIT.project_code = "project_code";

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
            String sql = "select " + xCCPIT.file_name + 
                " From " + xCCPIT.table + 
                " Group By " + xCCPIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectCedarGroupByFilename(String request_id)
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCCPIT.file_name +
                " From " + xCCPIT.table +
                " Where "+xCCPIT.request_id+"='"+request_id+"' "+
                " Group By " + xCCPIT.file_name;
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
        public DataTable selectCedarByFilename(String filename, String request_id)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCCPIT.table + 
                " Where " + xCCPIT.file_name + "='" + filename + "' "+
                " and "+xCCPIT.request_id+"='"+request_id+"' ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectCedarByReqestId(String request_id)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCCPIT.table +
                " Where " + xCCPIT.request_id + "='" + request_id + "' ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectFilenameByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select  " + xCCPIT.file_name + ", row_cnt " +
                " From " + xCCPIT.table +
                " Where " + xCCPIT.request_id + "='" + requestId + "' " +
                " Group By " + xCCPIT.file_name + ", row_cnt " +
                " Order By " + xCCPIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String getCountNoErrorByFilename(String requestId, String filename)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "";
            sql = "Select  count(1) as cnt " +
                " From " + xCCPIT.table +
                " Where " + xCCPIT.request_id + "='" + requestId + "' and " + xCCPIT.file_name + "='" + filename + "' " +
                " and len(" + xCCPIT.error_message + ") <= 0";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["cnt"].ToString();
            }
            return chk;
        }
        public DataTable selectCedarGroupByPoNo(String filename, String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select " + xCCPIT.po_no + "," + xCCPIT.file_name +
                " From " + xCCPIT.table +
                " Where " + xCCPIT.validate_flag + "='Y' and " + xCCPIT.request_id + "='" + requestId + "' " +
                " and " + xCCPIT.file_name + "='" + filename + "' " +
                " Group By " + xCCPIT.po_no + "," + xCCPIT.file_name +
                " Order By " + xCCPIT.file_name + "," + xCCPIT.po_no +
                " ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectByPoNo(String filename, String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select * "  +
                " From " + xCCPIT.table +
                " Where " + xCCPIT.validate_flag + "='Y' and " + xCCPIT.request_id + "='" + requestId + "' " +
                " and " + xCCPIT.file_name + "='" + filename + "' " +
                //" Group By " + xCCPIT.po_no + "," + xCCPIT.file_name +
                " Order By " + xCCPIT.file_name + "," + xCCPIT.po_no +
                " ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectCedarByPoNumber(String requestId, String poNumber)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                " From " + xCCPIT.table +
                " Where " + xCCPIT.po_no + "='" + poNumber + "' and " + xCCPIT.request_id + "='" + requestId + "' " +
                " Order By " + xCCPIT.po_no + "," + xCCPIT.xno;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String updateValidateFlagY(String filename, String row_number, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCCPIT.table + " Set " + xCCPIT.validate_flag + "='Y' " +
                "Where " + xCCPIT.file_name + " = '" + filename + "' and " + xCCPIT.row_number + "='" + row_number + "' and " + xCCPIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateProcessFlagY(String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCCPIT.table + " Set " + xCCPIT.process_flag + "='Y' " +
                "Where  " + xCCPIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateErrorMessage(String filename, String row_number, String msg, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCCPIT.table + " Set " + xCCPIT.error_message + "=" + xCCPIT.error_message + "+'," + msg.Replace("'", "''") + "' " +
                ", " + xCCPIT.validate_flag + "='E' " +//VALIDATE_FLAG
                "Where " + xCCPIT.file_name + " = '" + filename + "' and " + xCCPIT.row_number + "='" + row_number + "' and " + xCCPIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public void insertBluk(List<String> cedar, String filename, String host, MaterialProgressBar pB1, String requestId, String pathLog)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", lastUpdateBy = "0", lastUpdateTime = "null";
            String chk = "";

            int colPO_NO = 1, colQT_NO = 2, colWO_NO =3, colPERIOD = 4, colWeek = 5, colBRANCH_PLANT = 6, colBRANCH_NAME = 7, colLOCTYPE = 8, colITEM_E1 = 9, colASSET_CODE = 10, colASSET_NAME = 11, colWORK_TYPE = 12, colAMOUNT = 13;
            int colVAT = 14, colTOTAL = 15, colSUPPLIER_CODE = 16, colSUPPLIER_NAME = 17, colADMIN = 18, colADMIN_RECEIVE_DOC = 19, colAPPROVE_DATE = 20, colCEDAR_CLOSE_DATE = 21, colINVOICE_DUE_DATE = 22;
            int colSUPP_AGREEMENT_NO = 23, colACCOUNT_SEGMENT = 24, colDATA_SOURCE = 25;        

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
                    String data_source = "", invoice_due_date = "", item_description = "", item_e1 = "";
                    String loctype = "", payment_term = "", period = "", po_no = "", qt_no = "";
                    String shippto_location = "", supplier_code = "", supplier_contact = "", supplier_name = "", supplier_site_code = "", sup_agreement_no = "", total = "";
                    String vat = "", week = "", work_type = "", wo_no = "", xno = "";
                    String erp_qty = "0", erp_uom = "", BRANCH_NAME="", project_code="";
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    String[] aaa = bbb.Split('|');
                    xno = aaa[0];

                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    po_no = aaa[colPO_NO];
                    qt_no = aaa[colQT_NO];
                    wo_no = aaa[colWO_NO];
                    period = aaa[colPERIOD];
                    week = aaa[colWeek];
                    branch_plant = aaa[colBRANCH_PLANT];
                    BRANCH_NAME = aaa[colBRANCH_NAME];      // no field
                    loctype = aaa[colLOCTYPE];
                    item_e1 = aaa[colITEM_E1];
                    asset_code = aaa[colASSET_CODE];

                    asset_name = aaa[colASSET_NAME];
                    work_type = aaa[colWORK_TYPE];
                    amt = aaa[colAMOUNT];
                    vat = aaa[colVAT];
                    total = aaa[colTOTAL];
                    supplier_code = aaa[colSUPPLIER_CODE];
                    supplier_name = aaa[colSUPPLIER_NAME];
                    admin = aaa[colADMIN];
                    admin_receipt_doc_date = aaa[colADMIN_RECEIVE_DOC];
                    approve_date = aaa[colAPPROVE_DATE];

                    cedar_close_date = aaa[colCEDAR_CLOSE_DATE];
                    invoice_due_date = aaa[colINVOICE_DUE_DATE];
                    sup_agreement_no = aaa[colSUPP_AGREEMENT_NO];       // no field
                    account_segment2 = aaa[colACCOUNT_SEGMENT];       // 60-12-27
                    project_code = aaa[colACCOUNT_SEGMENT];             // 60-12-27
                    data_source = aaa[colDATA_SOURCE];

                    admin_receipt_doc_date = dateYearShortToDB(admin_receipt_doc_date);
                    approve_date = dateYearShortToDB(approve_date);
                    cedar_close_date = dateYearShortToDB(cedar_close_date);
                    invoice_due_date = dateYearShortToDB(invoice_due_date);

                    requestId = requestId.Equals("") ? "0" : requestId;
                    xno = xno.Equals("") ? "null" : xno;
                    amt = amt.Equals("") ? "null" : amt;
                    vat = vat.Equals("") ? "null" : vat;
                    total = total.Equals("") ? "null" : total;

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
                    String ship = "";
                    ship = selectShiptoLocation(branch_plant);
                    shippto_location = ship;
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
                        .Append(",").Append(xCCPIT.work_type).Append(",").Append(xCCPIT.wo_no).Append(",").Append(xCCPIT.xno).Append(",").Append(xCCPIT.request_id).Append(",row_number, row_cnt, project_code")
                        .Append(") Values ('")
                        .Append(account_segment1).Append("','").Append(account_segment2).Append("','").Append(account_segment3)
                        .Append("','").Append(account_segment4).Append("','").Append(account_segment5).Append("','").Append(account_segment6)
                        .Append("','").Append(account_segment_no).Append("','").Append(admin).Append("','").Append(admin_receipt_doc_date)
                        .Append("',").Append(amt).Append(",'").Append(approve_date).Append("','").Append(asset_code)
                        .Append("','").Append(asset_name).Append("','").Append(branch_plant).Append("','").Append(category_name)
                        .Append("','").Append(cedar_close_date).Append("','").Append(createBy).Append("',").Append(createDate)
                        .Append(",'").Append(data_source).Append("','").Append(errMsg).Append("','").Append(filename.Trim().Replace(initC.PO008PathProcess, ""))
                        .Append("','").Append(invoice_due_date).Append("','").Append(item_description).Append("','").Append(item_e1)
                        .Append("','").Append(lastUpdateBy).Append("',").Append(lastUpdateTime).Append(",'").Append(loctype)
                        .Append("','").Append(payment_term).Append("','").Append(period).Append("','").Append(po_no)
                        .Append("','").Append(processFlag).Append("','").Append(qt_no).Append("','").Append(shippto_location)
                        .Append("','").Append(supplier_code).Append("','").Append(supplier_contact).Append("','").Append(supplier_name)
                        .Append("','").Append(supplier_site_code).Append("','").Append(sup_agreement_no).Append("',").Append(total)
                        .Append(",'").Append(validateFlag).Append("',").Append(vat).Append(",'").Append(week)
                        .Append("','").Append(work_type).Append("','").Append(wo_no).Append("',").Append(xno).Append(",'").Append(requestId).Append("','").Append(i).Append("','").Append(cedar.Count).Append("','").Append(project_code).Append("' ")
                        .Append(") ");
                    chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);
                }
            }
        }
        public String selectShiptoLocation(String branchPlant)
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "SELECT location_code " +
                    "FROM XCUST_LOCATIONS_MST_TBL " +
                    "WHERE location_code = '" + branchPlant + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["location_code"].ToString().Trim();
            }
            return chk;
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
        public void logProcessPO008(String programname, String startdatetime, String requestId)
        {
            String line1 = "", parameter = "", programstart = "", filename = "", recordError = "", txt = "", path = "", sql = "";
            String date = System.DateTime.Now.ToString("dd MMM yyyy");
            String time = System.DateTime.Now.ToString("HH.mm");

            int cntErr = 0, cntPass = 0;
            ControlMain cm = new ControlMain();
            line1 = "Program : XCUST Interface PO <CEDAR> to PO (ERP)" + Environment.NewLine;

            path = cm.getPathLogProcess(programname);
            parameter = "Parameter : " + Environment.NewLine;
            parameter += "           Path Initial = " + initC.PO008PathInitial + Environment.NewLine;
            parameter += "           Path Process = " + initC.PO008PathProcess + Environment.NewLine;
            parameter += "           Path Error = " + initC.PO008PathError + Environment.NewLine;
            parameter += "           Path Archive = " + initC.PO008PathArchive + Environment.NewLine;
            parameter += "           Import Source = " + initC.PO008ImportSource + Environment.NewLine;
            programstart = "Program Start : " + date+time  + Environment.NewLine;


            sql = "Select count(1) as cnt, " + xCCPIT.file_name
            + " From " + xCCPIT.table
            + " Where " + xCCPIT.request_id + " ='" + requestId + "' " +
            "Group By " + xCCPIT.file_name;
            DataTable dtFile = conn.selectData(sql, "kfc_po");

            if (dtFile.Rows.Count > 0)
            {
                foreach (DataRow rowFile in dtFile.Rows)
                {
                    String valiPass = "", valiErr = "", filenameR="";
                    filenameR = rowFile[xCCPIT.file_name].ToString();
                    sql = "Select count(1) as cnt_vali " +
                        " From " + xCCPIT.table + " " +
                        " Where " + xCCPIT.request_id + " ='" + requestId + "' " +
                        " and " + xCCPIT.file_name + "='" + filenameR + "' and " + xCCPIT.validate_flag + "='Y' ";

                    DataTable dtR = conn.selectData(sql, "kfc_po");
                    if (dtR.Rows.Count > 0)
                    {
                        foreach (DataRow rowVali in dtR.Rows)
                        {
                            valiPass = rowVali["cnt_vali"].ToString();
                            //cntPass++;
                        }
                    }
                    dtR.Clear();
                    sql = "Select count(1) as cnt_vali " +
                        " From " + xCCPIT.table + " " +
                        " Where " + xCCPIT.request_id + " ='" + requestId + "' " +
                        " and " + xCCPIT.file_name + "='" + filenameR + "' and " + xCCPIT.validate_flag + "='E' ";
                    dtR = conn.selectData(sql, "kfc_po");
                    if (dtR.Rows.Count > 0)
                    {
                        foreach (DataRow rowVali in dtR.Rows)
                        {
                            valiErr = rowVali["cnt_vali"].ToString();
                            //cntErr++;
                        }
                    }
                    if (valiErr.Equals("0"))
                    {
                        cntPass++;
                    }
                    else
                    {
                        cntErr++;
                    }
                    filename += "Filename " + filenameR + ", Total = " + rowFile["cnt"].ToString() + " record, Pass = " + valiPass + " record, Record Error = " + valiErr + " record" + Environment.NewLine;
                    //if (int.TryParse(rowFile.recordError, out err))
                    //{
                    //    if (int.Parse(rowFile.recordError) > 0)
                    //    {
                    //        cntErr++;
                    //    }
                    //}
                }
            }
            String filename1 = "", filename1old = "";
            sql = "Select * From " + xCCPIT.table + " " +
                "Where " + xCCPIT.request_id + " ='" + requestId + "' " +
                "Order By " + xCCPIT.file_name + ", " + xCCPIT.row_number;
            DataTable dtErr = new DataTable();
            dtErr = conn.selectData(sql, "kfc_po");
            if (dtErr.Rows.Count > 0)
            {
                foreach (DataRow dtErr1 in dtErr.Rows)
                {
                    if (dtErr1[xCCPIT.error_message].ToString().Equals(""))
                    {
                        continue;
                    }
                    filename1 = dtErr1[xCCPIT.file_name].ToString();
                    if (!filename1.Equals(filename1old))
                    {
                        filename1old = filename1;
                        recordError += Environment.NewLine + "FileName : " + dtErr1[xCCPIT.file_name].ToString() + Environment.NewLine;
                    }
                    //recordError += "FileName " + dtErr1[xCLFPT.file_name].ToString() + Environment.NewLine;
                    recordError += "=>PO_NUMER = " + dtErr1[xCCPIT.po_no].ToString() + ",QT NO = " + dtErr1[xCCPIT.qt_no].ToString() + ",ERROR" + Environment.NewLine;
                    recordError += "     ====>" + dtErr1[xCCPIT.error_message].ToString() + Environment.NewLine;
                }
                if (recordError.Length > 0)
                {
                    recordError = recordError.Replace("     ====>,", "     ====>");

                }
            }
            //String comp = "", error = "";
            //sql = "Select Count(1) as cnt From "+xCLFPT.table+ " Where " + xCLFPT.request_id + " ='" + requestId + "' "+
            //    " and "+xCLFPT.VALIDATE_FLAG+"='Y' Group By "+xCLFPT.file_name ;
            //DataTable dt = new DataTable();
            //dt = conn.selectData(sql, "kfc_po");
            //if (dt.Rows.Count >0)
            //{
            //    comp = dt.Rows[0]["cnt"].ToString();
            //}
            //dt.Clear();
            //sql = "Select Count(1) as cnt From " + xCLFPT.table + " Where " + xCLFPT.request_id + " ='" + requestId + "' " +
            //    " and " + xCLFPT.VALIDATE_FLAG + "='E' Group By " + xCLFPT.file_name;
            //dt = conn.selectData(sql, "kfc_po");
            //if (dt.Rows.Count > 0)
            //{
            //    error = dt.Rows[0]["cnt"].ToString();
            //}
            //using (var stream = File.CreateText(Environment.CurrentDirectory + "\\" + programname + "_" + startdatetime.Replace("-", "_").Replace(":", "_") + ".log"))
            using (var stream = File.CreateText(path + programname + "_" + startdatetime.Replace("-", "_").Replace(":", "_") + ".log"))
            {
                txt = line1;
                txt += parameter;
                txt += programstart + Environment.NewLine;
                txt += "File " + Environment.NewLine;
                txt += "--------------------------------------------------------------------------" + Environment.NewLine;
                txt += filename + Environment.NewLine;
                txt += "File Error " + Environment.NewLine;
                txt += "--------------------------------------------------------------------------" + Environment.NewLine;
                txt += recordError + Environment.NewLine;
                txt += Environment.NewLine + "==========================================================================" + Environment.NewLine;
                txt += "Total = " + dtFile.Rows.Count+" File" + Environment.NewLine;
                txt += "Complete = " + cntPass + " File" + Environment.NewLine;
                txt += "Error = " + cntErr + " File" + Environment.NewLine;
                stream.WriteLine(txt);
            }
        }
    }
}
