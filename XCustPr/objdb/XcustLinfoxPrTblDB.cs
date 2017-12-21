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
    public class XcustLinfoxPrTblDB
    {
        public XcustLinfoxPrTbl xCLFPT;
        ConnectDB conn;
        private InitC initC;
        public XcustLinfoxPrTblDB()
        {
            initConfig();
        }
        public XcustLinfoxPrTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCLFPT = new XcustLinfoxPrTbl();
            xCLFPT.COMPANYCODE = "COMPANY_CODE";
            xCLFPT.DELIVERY_INSTRUCTION = "DELIVERY_INSTRUCTION";
            xCLFPT.ERROR_MSG = "error_message";
            xCLFPT.ITEM_CODE = "ITEM_CODE";
            xCLFPT.LINE_NUMBER = "LINE_NUMBER";
            xCLFPT.ORDER_DATE = "ORDER_DATE";
            xCLFPT.REQUEST_DATE = "REQUEST_DATE";
            xCLFPT.PO_NUMBER = "PO_NUMBER";
            xCLFPT.PROCESS_FLAG = "PROCESS_FLAG";
            xCLFPT.QTY = "QTY";
            xCLFPT.SUPPLIER_CODE = "SUPPLIER_CODE";
            xCLFPT.UOMCODE = "UOM_CODE";
            xCLFPT.VALIDATE_FLAG = "VALIDATE_FLAG";
            xCLFPT.create_date = "creation_date";
            xCLFPT.create_by = "creation_by";
            xCLFPT.last_update_by = "last_update_by";
            xCLFPT.last_update_date = "last_update_date";
            xCLFPT.file_name = "file_name";
            xCLFPT.store_code = "store_code";
            //xCLFPT.REQUEST_TIME = "REQUEST_TIME";
            xCLFPT.REQUEST_TIME = "ORDER_TIME";

            xCLFPT.diriver_to_organization = "diriver_to_organization";
            xCLFPT.deriver_to_location = "deriver_to_location";
            xCLFPT.subinventory_code = "subinventory_code";
            xCLFPT.ERP_ITEM_CODE = "ERP_ITEM_CODE";
            xCLFPT.AGREEEMENT_NUMBER = "AGREEEMENT_NUMBER";
            xCLFPT.AGREEMENT_LINE_NUMBER = "AGREEMENT_LINE_NUMBER";
            xCLFPT.PRICE = "PRICE";
            xCLFPT.ITEM_CATEGORY_NAME = "ITEM_CATEGORY_NAME";
            xCLFPT.SUPPLIER_SITE_CODE = "SUPPLIER_SITE_CODE";
            xCLFPT.ACC_SEG1 = "ACC_SEG1";
            xCLFPT.ACC_SEG2 = "ACC_SEG2";
            xCLFPT.ACC_SEG3 = "ACC_SEG3";
            xCLFPT.ACC_SEG4 = "ACC_SEG4";
            xCLFPT.ACC_SEG5 = "ACC_SEG5";
            xCLFPT.ACC_SEG6 = "ACC_SEG6";

            xCLFPT.SEND_PO_FLAG = "SEND_PO_FLAG";
            xCLFPT.GEN_OUTBOUD_FLAG = "GEN_OUTBOUD_FLAG";
            xCLFPT.ERP_PO_HEADER_ID = "ERP_PO_HEADER_ID";
            xCLFPT.ERP_PO_LINE_ID = "ERP_PO_LINE_ID";
            xCLFPT.ERP_PO_NUMBER = "ERP_PO_NUMBER";
            xCLFPT.ERP_PO_LINE_NUMBER = "ERP_PO_LINE_NUMBER";
            xCLFPT.ERP_QTY = "ERP_QTY";
            xCLFPT.request_id = "request_id";
            xCLFPT.supplier_name = "supplier_name";
            xCLFPT.ERROR_MSG2 = "error_message_po002";
            xCLFPT.request_id_po002 = "request_id_po002";
            xCLFPT.file_name_po002 = "file_name_po002";

            //xCLFPT.table = "xcust_linfox_pr_tbl";
            xCLFPT.table = "xcust_linfox_pr_int_tbl";
        }
        public DataTable selectLinfox()
        {
            DataTable dt = new DataTable();
            String sql = "select * From "+xCLFPT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select "+xCLFPT.file_name+" From " + xCLFPT.table+" Group By "+xCLFPT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLFPT.table + " Where " + xCLFPT.file_name+"='"+filename+"' Order By "+xCLFPT.PO_NUMBER+","+xCLFPT.LINE_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxByFilename(String filename, String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLFPT.table + 
                " Where " + xCLFPT.file_name + "='" + filename + "' " +
                " and "+xCLFPT.request_id+"='"+requestId+"' "+
                "Order By " + xCLFPT.PO_NUMBER + "," + xCLFPT.LINE_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxEmptyRow()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLFPT.table + " Where " + xCLFPT.file_name + "='aaaaaaaaaa'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        
        public String getRequestID()
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "select next value for xcust_request_id ";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString();
            }
            return chk;
        }
        public String getPO002RequestID()
        {
            String chk = "";
            DataTable dt = new DataTable();
            //ใช้ ตัวเดียวกันกับ PO001
            String sql = "select next value for xcust_request_id ";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString();
            }
            return chk;
        }
        public Boolean validateGL(String seg1, String seg2, String seg3, String seg4, String seg5, String seg6, String bu_name)
        {
            Boolean chk = false;
            DataTable dt = new DataTable();
            String sql = "select code_combination_id "+
                        "from xcust_gl_code_combinations_tbl glc " +
                        ", xcust_bu_mst_tbl  BU " +
                        ",xcust_gl_ledger_mst_tbl gll " +
                        "where BU.PRIMARY_LEDGER_ID = gll.ledger_id " +
                        "and gll.CHART_OF_ACCOUNTS_ID = glc.CHART_OF_ACCOUNTS_ID " +
                        "and BU.BU_NAME = '"+ bu_name + "' " +
                        "and glc.segment1 = '" +seg1+"' " +
                        "and glc.segment2 = '" + seg2 + "' " +
                        "and glc.segment3 = '" + seg3 + "' " +
                        "and glc.segment4 = '" + seg4 + "' " +
                        "and glc.segment5 = '" + seg5 + "' " +
                        "and glc.segment6 = '" + seg6 + "' " ;
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count == 1)
            {
                chk = true;
            }
            return chk;
        }
        public void DeleteLinfoxTemp()
        {
            String sql = "Delete From " + xCLFPT.table;
            conn.ExecuteNonQuery(sql, "kfc_po", initC.PO002PathLog);            
        }
        public void DeleteLinfoxTempByFilename(String filename)
        {
            String sql = "Delete From " + xCLFPT.table + " Where " + xCLFPT.file_name + "='" + filename + "'";
            conn.ExecuteNonQuery(sql, "kfc_po", initC.PO002PathLog);
        }
        public DataTable selectPO002()
        {
            DataTable dt = new DataTable();
            
            String sql = "Select * From " + xCLFPT.table + 
                " Where " + xCLFPT.SEND_PO_FLAG + " is null and "+xCLFPT.PROCESS_FLAG+"='Y' and "+xCLFPT.GEN_OUTBOUD_FLAG+ " is null and validate_flag = 'Y'";
                //" Where " + xCLFPT.SEND_PO_FLAG + " is null and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + " = '' and validate_flag = 'Y'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPO002GenTextLinfoxGroupByERPPONumber()
        {
            DataTable dt = new DataTable();
            String sql = "select "+ xCLFPT .ERP_PO_NUMBER+ //","+xCLFPT.PO_NUMBER+","+xCLFPT.LINE_NUMBER+
                " From " + xCLFPT.table +
                //" Where " + xCLFPT.SEND_PO_FLAG + "='N' and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + "='N' and " + xCLFPT.ERP_PO_NUMBER + " is not null "+
                //" Where " + xCLFPT.SEND_PO_FLAG + "='N' and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + "='N' and " + xCLFPT.ERP_PO_NUMBER + " ='' " +
                //" Where " + xCLFPT.SEND_PO_FLAG + " is null and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + " is null and " + xCLFPT.ERP_PO_NUMBER + " ='' " +
                " Where " + xCLFPT.SEND_PO_FLAG + " is null and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + " is null and " + xCLFPT.ERP_PO_NUMBER + " !='' " +    // for test
                "Group By " +xCLFPT.ERP_PO_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPO002GenTextLinfox(String erp_po_number)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLFPT.table + " Where " + xCLFPT.SEND_PO_FLAG + " is null "+
                "and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + " is null and "+xCLFPT.ERP_PO_NUMBER+" ='"+erp_po_number+"'";
            //String sql = "select * From " + xCLFPT.table + 
            //    " Where " + xCLFPT.SEND_PO_FLAG + " is null and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + " is null and " + xCLFPT.ERP_PO_NUMBER + " ='" + erp_po_number + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String updateFromPO002(String erp_po_number, String erp_qty, String ERP_PO_HEADER_ID, String ERP_PO_LINE_ID, String ERP_PO_LINE_NUMBER, String request_id_po002, String po_number, String line_number)
        {
            String sql = "", chk = "";
            erp_qty = erp_qty.Equals("") ? "null" : erp_qty;
            ERP_PO_HEADER_ID = ERP_PO_HEADER_ID.Equals("") ? "null" : ERP_PO_HEADER_ID;
            ERP_PO_LINE_ID = ERP_PO_LINE_ID.Equals("") ? "null" : ERP_PO_LINE_ID;
            ERP_PO_LINE_NUMBER = ERP_PO_LINE_NUMBER.Equals("") ? "null" : ERP_PO_LINE_NUMBER;
            request_id_po002 = request_id_po002.Equals("") ? "null" : request_id_po002;
            sql = "Update "+xCLFPT.table +" "+
                "Set "+xCLFPT.ERP_PO_NUMBER+"='"+ erp_po_number + "', "+
                xCLFPT.ERP_QTY +"="+ erp_qty+", " +
                xCLFPT.ERP_PO_HEADER_ID + "=" + ERP_PO_HEADER_ID + ", " +
                xCLFPT.ERP_PO_LINE_ID + "=" + ERP_PO_LINE_ID + ", " +
                xCLFPT.ERP_PO_LINE_NUMBER + "=" + ERP_PO_LINE_NUMBER + ", " +
                //xCLFPT.GEN_OUTBOUD_FLAG + "='Y', " +      // ไปupdate ตอน gen text
                xCLFPT.request_id_po002 + "=" + request_id_po002 + " " +
                "Where " +xCLFPT.PO_NUMBER+"='"+po_number+"' and "+xCLFPT.LINE_NUMBER+"='"+line_number+"'";
            chk = conn.ExecuteNonQuery(sql, "kfc_po", initC.PO002PathLog);

            return chk;
        }
        public String updateRequestIdPo002(String requestId)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " " +
                "Set " + xCLFPT.request_id_po002 + "='"+ requestId + "' " +
                ","+xCLFPT.ERROR_MSG2+"=''"+
                " Where " + xCLFPT.SEND_PO_FLAG + " is null and " + xCLFPT.PROCESS_FLAG + "='Y' and " + xCLFPT.GEN_OUTBOUD_FLAG + " is null and validate_flag = 'Y'";
            chk = conn.ExecuteNonQuery(sql, "kfc_po", initC.PO002PathLog);

            return chk;
        }
        public String updateOutBoundFlag(String po_number, String line_number)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " " +
                "Set " + xCLFPT.GEN_OUTBOUD_FLAG + "='Y' " +                
                "Where " + xCLFPT.PO_NUMBER + "='" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "'";
            chk = conn.ExecuteNonQuery(sql, "kfc_po", initC.PO002PathLog);

            return chk;
        }
        public DataTable selectLinfoxGroupByPoNumber(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select "+xCLFPT.PO_NUMBER+","+xCLFPT.file_name+
                " From "+xCLFPT.table+
                " Where "+xCLFPT.VALIDATE_FLAG+"='Y' and "+xCLFPT.request_id+"='"+requestId+"' "+
                " Group By "+xCLFPT.PO_NUMBER+","+xCLFPT.file_name+
                " Order By "+xCLFPT.file_name+","+xCLFPT.PO_NUMBER+
                " ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxGroupByPoNumber(String filename, String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select " + xCLFPT.PO_NUMBER + "," + xCLFPT.file_name +
                " From " + xCLFPT.table +
                " Where " + xCLFPT.VALIDATE_FLAG + "='Y' and " + xCLFPT.request_id + "='" + requestId + "' " +
                " and "+xCLFPT.file_name+"='"+ filename + "' "+
                " Group By " + xCLFPT.PO_NUMBER + "," + xCLFPT.file_name +
                " Order By " + xCLFPT.file_name + "," + xCLFPT.PO_NUMBER +
                " ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxByPoNumber(String requestId, String poNumber)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                " From "+xCLFPT.table +
                " Where "+xCLFPT.PO_NUMBER+"='"+poNumber+"' and "+xCLFPT.request_id+"='"+requestId+"' " +
                " Order By "+xCLFPT.PO_NUMBER+","+xCLFPT.LINE_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPO002LinfoxByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                " From " + xCLFPT.table +
                " Where " + xCLFPT.request_id_po002 + "='" + requestId + "' " +
                " Order By " + xCLFPT.PO_NUMBER + "," + xCLFPT.LINE_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                " From " + xCLFPT.table +
                " Where " + xCLFPT.request_id + "='" + requestId + "' " +
                " Order By " + xCLFPT.PO_NUMBER + "," + xCLFPT.LINE_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectFilenameByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select  "+xCLFPT.file_name +", row_cnt "+
                " From " + xCLFPT.table +
                " Where " + xCLFPT.request_id + "='" + requestId + "' " +
                " Group By "+xCLFPT.file_name + ", row_cnt " +
                " Order By " + xCLFPT.file_name ;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String getCountNoErrorByFilename(String requestId,String filename)
        {
            DataTable dt = new DataTable();
            String sql = "", chk="";
            sql = "Select  count(1) as cnt " +
                " From " + xCLFPT.table +
                " Where " + xCLFPT.request_id + "='" + requestId + "' and "+xCLFPT.file_name+"='"+filename+"' "+
                " and len("+xCLFPT.ERROR_MSG+") <= 0";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["cnt"].ToString();
            }
            return chk;
        }
        public void insertBluk(List<String> linfox, String filename, String host, MaterialProgressBar pB1, String requestId, ControlMain Cm, String pathLog)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy="0", createDate= "GETDATE()", lastUpdateBy="0", lastUpdateTime="null", chk="";
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
                    String qty = "";
                    qty = aaa[8].Equals("")?"0": aaa[8];
                    requestId = requestId.Equals("") ? "0" : requestId;
                    //bbb += "('" + aaa[0] + "','" +
                    //aaa[11] + "','" + errMsg + "','" + aaa[6] + "','" +
                    //aaa[2] + "','" + aaa[4] + "','" + aaa[5] + "','" +
                    //aaa[1] + "','" + processFlag + "','" + aaa[7] + "','" +
                    //aaa[3] + "','" + aaa[8] + "','" + validateFlag + "'),";
                    sql.Append("Insert Into ").Append(xCLFPT.table).Append(" (").Append(xCLFPT.COMPANYCODE).Append(",").Append(xCLFPT.PO_NUMBER).Append(",").Append(xCLFPT.LINE_NUMBER)
                        .Append(",").Append(xCLFPT.SUPPLIER_CODE).Append(",").Append(xCLFPT.ORDER_DATE).Append(",").Append(xCLFPT.REQUEST_DATE)
                        .Append(",").Append(xCLFPT.ITEM_CODE).Append(",").Append(xCLFPT.QTY).Append(",").Append(xCLFPT.UOMCODE)
                        .Append(",").Append(xCLFPT.DELIVERY_INSTRUCTION).Append(",").Append(xCLFPT.VALIDATE_FLAG).Append(",").Append(xCLFPT.PROCESS_FLAG)
                        .Append(",").Append(xCLFPT.ERROR_MSG).Append(",").Append(xCLFPT.create_by).Append(",").Append(xCLFPT.create_date)
                        .Append(",").Append(xCLFPT.last_update_by).Append(",").Append(xCLFPT.last_update_date).Append(",").Append(xCLFPT.file_name)
                        .Append(",").Append(xCLFPT.store_code).Append(",").Append(xCLFPT.REQUEST_TIME).Append(",").Append(xCLFPT.request_id).Append(",row_number, row_cnt")
                        .Append(") Values ('")
                        .Append(aaa[0]).Append("','").Append(aaa[1]).Append("','").Append(aaa[2])
                        .Append("','").Append(aaa[3]).Append("','").Append(aaa[4]).Append("','").Append(aaa[5])
                        .Append("','").Append(aaa[7]).Append("',").Append(aaa[8]).Append(",'").Append(aaa[9])                        
                        .Append("','").Append(aaa[10]).Append("','").Append(validateFlag).Append("','").Append(processFlag)
                        .Append("','").Append(errMsg).Append("','").Append(createBy).Append("',").Append(createDate)
                        .Append(",'").Append(lastUpdateBy).Append("',").Append(lastUpdateTime).Append(",'").Append(filename.Trim().Replace(initC.PathProcess,""))
                        .Append("','").Append(aaa[11]).Append("','").Append(aaa[6]).Append("','").Append(requestId).Append("','").Append(i).Append("','").Append(linfox.Count).Append("') ");
                    chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);
                    if (chk.Length > 1)
                    {
                        String date1 = System.DateTime.Now.ToString("yyyy-MM-dd");
                        String time1 = System.DateTime.Now.ToString("HH_mm_ss");
                        String dateStart = date + " " + time;       //gen log

                        List<ValidatePrPo> lVPr = new List<ValidatePrPo>();   // gen log
                        List<ValidateFileName> lVfile = new List<ValidateFileName>();   // gen log
                        ValidatePrPo vPP = new ValidatePrPo();   // gen log
                        vPP = new ValidatePrPo();
                        vPP.Filename = filename;
                        vPP.Message = "Error PO001 Structure text file error" ;
                        vPP.Validate = "";
                        lVPr.Add(vPP);

                        ValidateFileName vF = new ValidateFileName();   // gen log
                        vF.recordError = "1";   // gen log
                        vF.totalError = "1";   // gen log
                        lVfile.Add(vF);   // gen log
                        Cm.logProcess("xcustpo001", lVPr, dateStart, lVfile);   // gen log
                        break;
                    }
                }
                //sql = "Update "+xCLFPT.table+" Set row_cnt ='"+i+"' Where ";
            }
        }
        public void updatePrcessFlag(String requestId, String host, String pathLog)
        {
            String sql = "", chk="";
            sql = "Update "+xCLFPT.table+" Set "+xCLFPT.PROCESS_FLAG+"='Y' "+
                " Where "+xCLFPT.request_id+"='"+requestId+"'";
            chk = conn.ExecuteNonQuery(sql, host, pathLog);
        }
        public String updateErrorMessage(String po_number, String line_number, String msg, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " Set " + xCLFPT.ERROR_MSG + "="+ xCLFPT.ERROR_MSG + "+'," + msg.Replace("'","''") + "' " +
                ", "+xCLFPT.VALIDATE_FLAG+"='E' " +//VALIDATE_FLAG
                "Where " + xCLFPT.PO_NUMBER + " = '" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "' and "+xCLFPT.request_id+"='"+requestId+"'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateErrorMessagePO002(String po_number, String line_number, String msg, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " Set " + xCLFPT.ERROR_MSG2 + "=" + xCLFPT.ERROR_MSG2 + "+'," + msg.Replace("'", "''") + "' " +
                "Where " + xCLFPT.PO_NUMBER + " = '" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "' and " + xCLFPT.request_id_po002 + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateValidateFlagY(String po_number, String line_number, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " Set " + xCLFPT.VALIDATE_FLAG + "='Y' " +
                "Where " + xCLFPT.PO_NUMBER + " = '" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "' and " + xCLFPT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateValidateFlagYPO002(String po_number, String line_number, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " Set " + xCLFPT.VALIDATE_FLAG + "='Y' " +
                "Where " + xCLFPT.PO_NUMBER + " = '" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "' and " + xCLFPT.request_id_po002 + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateValidateFlag(String po_number, String line_number, String flag, String agreement_number, String host, String pathLog)
        {
            String sql = "", chk="";
            sql = "Update "+xCLFPT.table +" Set "+xCLFPT.VALIDATE_FLAG+"='"+flag+"', "+xCLFPT.AGREEEMENT_NUMBER+" ='"+ agreement_number+"' "+
                "Where " +xCLFPT.PO_NUMBER+" = '"+po_number+"' and "+xCLFPT.LINE_NUMBER+"='"+line_number+"'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateProcessFlagY(String po_number, String line_number, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " Set " + xCLFPT.PROCESS_FLAG + "='Y' " +
                "Where " + xCLFPT.PO_NUMBER + " = '" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "' and "+xCLFPT.request_id+"='"+requestId+"'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateFilenamePo002(String erp_po_number, String filename_po002, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCLFPT.table + " Set "+xCLFPT.file_name_po002+" = '"+filename_po002+"' " +
                "Where  " + xCLFPT.ERP_PO_NUMBER + " ='" + erp_po_number + "' ";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateValidateFlag1(String po_number, String line_number, String flag, String agreement_number, String agreement_line_number
            ,String supplierSiteCode, String suppName, String subInv_code, String price, String host, String pathLog)
        {
            String sql = "", chk = "";
            Double price1 = 0;
            Double.TryParse(price, out price1);
            sql = "Update " + xCLFPT.table + " Set " + xCLFPT.VALIDATE_FLAG + "='" + flag + "'" +
                ", " + xCLFPT.AGREEEMENT_NUMBER + " ='" + agreement_number + "'" +
                ", " + xCLFPT.AGREEMENT_LINE_NUMBER+"='"+agreement_line_number+"' " +
                ", " + xCLFPT.SUPPLIER_SITE_CODE + "='" + supplierSiteCode + "' " +
                ", " + xCLFPT.supplier_name + "='" + suppName.Replace("'","''") + "' " +
                ", " + xCLFPT.subinventory_code + "='" + subInv_code + "' " +
                ", " + xCLFPT.PRICE + "='" + price1.ToString() + "' " +
                "Where " + xCLFPT.PO_NUMBER + " = '" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateValidateFlag2(String po_number, String line_number, String agreement_number, String agreement_line_number
            , String supplierSiteCode, String suppName, String subInv_code, String price, String host, String pathLog)
        {
            String sql = "", chk = "";
            Double price1 = 0;
            Double.TryParse(price, out price1);
            sql = "Update " + xCLFPT.table + " Set "  +
                " " + xCLFPT.AGREEEMENT_NUMBER + " ='" + agreement_number + "'" +
                ", " + xCLFPT.AGREEMENT_LINE_NUMBER + "='" + agreement_line_number + "' " +
                ", " + xCLFPT.SUPPLIER_SITE_CODE + "='" + supplierSiteCode + "' " +
                ", " + xCLFPT.supplier_name + "='" + suppName.Replace("'", "''") + "' " +
                ", " + xCLFPT.subinventory_code + "='" + subInv_code + "' " +
                ", " + xCLFPT.PRICE + "='" + price1.ToString() + "' " +
                "Where " + xCLFPT.PO_NUMBER + " = '" + po_number + "' and " + xCLFPT.LINE_NUMBER + "='" + line_number + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public DataTable selectValidateFlagYGroupByPoNumber()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLFPT.table + " Where " + xCLFPT.VALIDATE_FLAG + "='Y' Group By "+xCLFPT.PO_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void logProcessPO001(String programname, String startdatetime, String requestId)
        {
            String line1 = "", parameter = "", programstart = "", filename = "", recordError = "", txt = "", path = "", sql="";
            int cntErr = 0, cntPass = 0;
            ControlMain cm = new ControlMain();
                line1 = "Program : XCUST Interface PR<Linfox>To PO(ERP)" + Environment.NewLine;
                
                path = cm.getPathLogProcess(programname);
                parameter = "Parameter : " + Environment.NewLine;
                parameter += "           Path Initial :" + initC.PathInitial + Environment.NewLine;
                parameter += "           Path Process :" + initC.PathProcess + Environment.NewLine;
                parameter += "           Path Error :" + initC.PathError + Environment.NewLine;
                parameter += "           Import Source :" + initC.ImportSource + Environment.NewLine;
                programstart = "Program Start : " + startdatetime + Environment.NewLine;


            sql = "Select count(1) as cnt, "+xCLFPT.file_name
            +" From "+xCLFPT.table 
            +" Where "+xCLFPT.request_id+" ='"+requestId+"' "+
            "Group By "+xCLFPT.file_name;
            DataTable dtFile =  conn.selectData(sql, "kfc_po");
            
            if (dtFile.Rows.Count > 0)
            {
                foreach (DataRow rowFile in dtFile.Rows)
                {
                    String valiPass = "", valiErr = "";
                    sql = "Select count(1) as cnt_vali " +
                        " From " + xCLFPT.table + " " +
                        " Where " + xCLFPT.request_id + " ='" + requestId + "' "+ 
                        " and " + xCLFPT.file_name + "='" + rowFile[xCLFPT.file_name].ToString()+"' and "+xCLFPT.VALIDATE_FLAG+"='Y' ";
                    
                    DataTable dtR = conn.selectData(sql, "kfc_po");
                    if (dtR.Rows.Count > 0)
                    {
                        foreach(DataRow rowVali in dtR.Rows)
                        {
                            valiPass = rowVali["cnt_vali"].ToString();
                            //cntPass++;
                        }
                    }
                    dtR.Clear();
                    sql = "Select count(1) as cnt_vali " +
                        " From " + xCLFPT.table + " " +
                        " Where " + xCLFPT.request_id + " ='" + requestId + "' " +
                        " and " + xCLFPT.file_name + "='" + rowFile[xCLFPT.file_name].ToString() + "' and " + xCLFPT.VALIDATE_FLAG + "='E' ";
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
                    filename += "Filename " + rowFile[xCLFPT.file_name].ToString() + ", Total = " + rowFile["cnt"].ToString() + ", Validate pass = " + valiPass + ", Record Error = " + valiErr + " " + Environment.NewLine;
                    //if (int.TryParse(rowFile.recordError, out err))
                    //{
                    //    if (int.Parse(rowFile.recordError) > 0)
                    //    {
                    //        cntErr++;
                    //    }
                    //}
                }
            }
            String filename1 = "", filename1old="";
            sql = "Select * From " + xCLFPT.table + " "+
                "Where " + xCLFPT.request_id + " ='" + requestId + "' "+
                "Order By "+xCLFPT.file_name+", "+xCLFPT.PO_NUMBER;
            DataTable dtErr = new DataTable();
            dtErr = conn.selectData(sql, "kfc_po");
            if (dtErr.Rows.Count > 0)
            {
                foreach (DataRow dtErr1 in dtErr.Rows)
                {
                    if (dtErr1[xCLFPT.ERROR_MSG].ToString().Equals(""))
                    {
                        continue;
                    }
                    filename1 = dtErr1[xCLFPT.file_name].ToString();
                    if (!filename1.Equals(filename1old))
                    {
                        filename1old = filename1;
                        recordError += Environment.NewLine+"FileName : " +dtErr1[xCLFPT.file_name].ToString() + Environment.NewLine;
                    }
                    //recordError += "FileName " + dtErr1[xCLFPT.file_name].ToString() + Environment.NewLine;
                    recordError += "=>PO_NUMER = " + dtErr1[xCLFPT.PO_NUMBER].ToString()+ ",LINE_NUMER = " + dtErr1[xCLFPT.LINE_NUMBER].ToString()+",ERROR" + Environment.NewLine;
                    recordError += "     ====>" + dtErr1[xCLFPT.ERROR_MSG].ToString() + Environment.NewLine;
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
                txt += "Total " + dtFile.Rows.Count + Environment.NewLine;
                txt += "Complete " + cntPass + Environment.NewLine;
                txt += "Error " + cntErr + Environment.NewLine;
                stream.WriteLine(txt);
            }
        }
        public void logProcessPO002(String programname, String startdatetime, String requedtId, String flag)
        {
            String line1 = "", parameter = "", programstart = "", filename = "", recordError = "", txt = "", path = "", sql = "";
            int cntErr = 0, cntPass = 0, cntTotal=0;

            String date = System.DateTime.Now.ToString("dd MMM yyyy");
            String time = System.DateTime.Now.ToString("HH.mm");
            DataTable dtFile = new DataTable();

            line1 = "Program : XCUST Interface PO (ERP) To PO <LINFOX>" + Environment.NewLine;
            ControlMain cm = new ControlMain();
            path = cm.getPathLogProcess(programname);
            parameter = "Parameter : " + Environment.NewLine;
            parameter += "           Path Initial = " + initC.PO002PathInitial + Environment.NewLine;
            parameter += "           Path Destination = " + initC.PO002PathDestinaion + Environment.NewLine;
            parameter += "           Create Date = " + date + Environment.NewLine;
            programstart = "Program Start : " + date+" " + time + Environment.NewLine;
            if (flag.Equals("nodata"))
            {
                recordError = "==> Error PO002-001: No Data Found ";
            }
            else
            {
                //sql = "Select count(1) as cnt, " + xCLFPT.file_name
                //+ " From " + xCLFPT.table
                //+ " Where " + xCLFPT.request_id_po002 + " ='" + requedtId + "' " +
                //"Group By " + xCLFPT.file_name;
                sql = "Select count(1) as cnt, " + xCLFPT.file_name_po002
                + " From " + xCLFPT.table
                + " Where " + xCLFPT.request_id_po002 + " ='" + requedtId + "' and len("+xCLFPT.file_name_po002+") > 0 " +
                "Group By " + xCLFPT.file_name_po002;

                dtFile = conn.selectData(sql, "kfc_po");

                if (dtFile.Rows.Count > 0)
                {
                    cntTotal = dtFile.Rows.Count;
                    foreach (DataRow rowFile in dtFile.Rows)
                    {
                        if (rowFile[xCLFPT.file_name_po002].ToString().Equals(""))
                        {
                            continue;
                        }
                        String valiPass = "", valiErr = "";
                        sql = "Select count(1) as cnt_vali " +
                            " From " + xCLFPT.table + " " +
                            " Where " + xCLFPT.request_id_po002 + " ='" + requedtId + "' " +
                            " and " + xCLFPT.file_name_po002 + "='" + rowFile[xCLFPT.file_name_po002].ToString() + "' and len(" + xCLFPT.ERROR_MSG2 + ") > 0 ";

                        DataTable dtR = conn.selectData(sql, "kfc_po");
                        if (dtR.Rows.Count > 0)
                        {
                            foreach (DataRow rowVali in dtR.Rows)
                            {
                                valiPass = rowVali["cnt_vali"].ToString();
                                if (valiPass.Equals("0"))
                                {
                                    cntPass++;
                                }
                                else
                                {
                                    cntErr++;
                                }
                                //cntPass++;
                            }
                        }
                        dtR.Clear();
                        //sql = "Select count(1) as cnt_vali " +
                        //    " From " + xCLFPT.table + " " +
                        //    " Where " + xCLFPT.request_id_po002 + " ='"+ requedtId + "' " +
                        //    " and " + xCLFPT.file_name + "='" + rowFile[xCLFPT.file_name].ToString() + "' and " + xCLFPT.VALIDATE_FLAG + "='E' ";
                        //dtR = conn.selectData(sql, "kfc_po");
                        //if (rowFile[xCLFPT.file_name].ToString().Equals("PR17122017_24.txt"))
                        //{
                        //    sql = "";
                        //}
                        //if (dtR.Rows.Count > 0)
                        //{
                        //    foreach (DataRow rowVali in dtR.Rows)
                        //    {
                        //        valiErr = rowVali["cnt_vali"].ToString();
                        //        //cntErr++;
                        //    }
                        //}
                        //if (valiErr.Equals("0"))
                        //{
                        //    cntPass++;
                        //}
                        //else
                        //{
                        //    cntErr++;
                        //}
                        //filename += "Filename " + rowFile[xCLFPT.file_name].ToString() + ", Total = " + rowFile["cnt"].ToString() + ", Validate pass = " + valiPass + ", Record Error = " + valiErr + " " + Environment.NewLine;
                        filename += "File Name : " + rowFile[xCLFPT.file_name_po002].ToString() + Environment.NewLine;
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
                sql = "Select * From " + xCLFPT.table + " " +
                    "Where " + xCLFPT.request_id_po002 + " ='" + requedtId + "'  and len(" + xCLFPT.file_name_po002 + ") > 0 " +
                    "Order By " + xCLFPT.file_name_po002 + ", " + xCLFPT.PO_NUMBER;
                DataTable dtErr = new DataTable();
                dtErr = conn.selectData(sql, "kfc_po");
                if (dtErr.Rows.Count > 0)
                {
                    foreach (DataRow dtErr1 in dtErr.Rows)
                    {
                        if (dtErr1[xCLFPT.file_name_po002].ToString().Equals(""))
                        {
                            continue;
                        }
                        if (dtErr1[xCLFPT.ERROR_MSG2].ToString().Equals(""))
                        {
                            continue;
                        }
                        filename1 = dtErr1[xCLFPT.file_name_po002].ToString();
                        if (!filename1.Equals(filename1old))
                        {
                            filename1old = filename1;
                            recordError += "File Name : " + dtErr1[xCLFPT.file_name_po002].ToString() + Environment.NewLine;
                        }
                        //recordError += "FileName " + dtErr1[xCLFPT.file_name].ToString() + Environment.NewLine;
                        //recordError += "=>PO_NUMER = " + dtErr1[xCLFPT.PO_NUMBER].ToString() + ",LINE_NUMER = " + dtErr1[xCLFPT.LINE_NUMBER].ToString() + ",ERROR" + Environment.NewLine;
                        recordError += "     ====>" + dtErr1[xCLFPT.ERROR_MSG2].ToString() + Environment.NewLine;
                    }
                    if (recordError.Length > 0)
                    {
                        recordError = recordError.Replace("     ====>,", "     ====>");

                    }
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
                txt += Environment.NewLine+"==========================================================================" + Environment.NewLine;
                txt += "Total = " + cntTotal+" Files " + Environment.NewLine;
                txt += "Complete = " + cntPass+" Files " + Environment.NewLine;
                txt += "Error = " + cntErr+" Files " + Environment.NewLine;
                stream.WriteLine(txt);
            }
        }
        //public static void BulkToMySQL()  
        //{
        //    string ConnectionString = "server=192.168.1xxx";
        //    StringBuilder sCommand = new StringBuilder("INSERT INTO User (FirstName, LastName) VALUES ");
        //    using (cRDPO.conn = new MySqlConnection(ConnectionString))
        //    {
        //        List<string> Rows = new List<string>();
        //        for (int i = 0; i < 100000; i++)
        //        {
        //            Rows.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString("test"), MySqlHelper.EscapeString("test")));
        //        }
        //        sCommand.Append(string.Join(",", Rows));
        //        sCommand.Append(";");
        //        mConnection.Open();
        //        using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
        //        {
        //            myCmd.CommandType = CommandType.Text;
        //            myCmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
