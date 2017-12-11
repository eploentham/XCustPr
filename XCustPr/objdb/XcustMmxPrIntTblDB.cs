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
    public class XcustMmxPrIntTblDB
    {
        public XcustMmxPrIntTbl xCMPIT;
        ConnectDB conn;
        private InitC initC;
        public XcustMmxPrIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCMPIT = new XcustMmxPrIntTbl();
            xCMPIT.ACC_SEG1 = "ACC_SEG1";
            xCMPIT.ACC_SEG2 = "ACC_SEG2";
            xCMPIT.ACC_SEG3 = "ACC_SEG3";
            xCMPIT.ACC_SEG4 = "ACC_SEG4";
            xCMPIT.ACC_SEG5 = "ACC_SEG5";
            xCMPIT.ACC_SEG6 = "ACC_SEG6";
            xCMPIT.AGREEEMENT_NUMBER = "AGREEEMENT_NUMBER";
            xCMPIT.AGREEMENT_LINE_NUMBER = "AGREEMENT_LINE_NUMBER";
            xCMPIT.confirm_qty = "confirm_qty";
            xCMPIT.conf_delivery_date = "conf_delivery_date";
            xCMPIT.creation_by = "creation_by";
            xCMPIT.creation_date = "creation_date";
            xCMPIT.delivery_date = "delivery_date";
            xCMPIT.DELIVERY_INSTRUCTION = "DELIVERY_INSTRUCTION";
            xCMPIT.deriver_to_location = "deriver_to_location";
            xCMPIT.diriver_to_organization = "diriver_to_organization";
            xCMPIT.ERP_ITEM_CODE = "ERP_ITEM_CODE";
            xCMPIT.erp_subinventory_code = "erp_subinventory_code";
            xCMPIT.error_message = "error_message";
            xCMPIT.file_name = "file_name";
            xCMPIT.ITEM_CATEGORY_NAME = "ITEM_CATEGORY_NAME";
            xCMPIT.item_code = "item_code";
            xCMPIT.last_update_by = "last_update_by";
            xCMPIT.last_update_date = "last_update_date";
            xCMPIT.order_date = "order_date";
            xCMPIT.order_qty = "order_qty";
            xCMPIT.po_number = "po_number";
            xCMPIT.po_status = "po_status";
            xCMPIT.PRICE = "PRICE";
            xCMPIT.process_flag = "process_flag";
            xCMPIT.request_date = "request_date";
            xCMPIT.store_code = "store_code";
            xCMPIT.subinventory_code = "subinventory_code";
            xCMPIT.supplier_code = "supplier_code";
            xCMPIT.SUPPLIER_SITE_CODE = "SUPPLIER_SITE_CODE";
            xCMPIT.uom_code = "uom_code";
            xCMPIT.Validate_flag = "Validate_flag";
            xCMPIT.request_id = "request_id";
             
            xCMPIT.table = "xcust_mmx_pr_int_tbl";
            xCMPIT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxGroupByFilename(String request_id)
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCMPIT.file_name + " From " + xCMPIT.table + " Where "+xCMPIT.request_id+"='"+request_id+"' Group By " + xCMPIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxByFilename(String filename, String request_id)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPIT.table + " Where " + xCMPIT.file_name + "='" + filename + "' and "+xCMPIT.request_id+"='"+request_id+"'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxByPoNumber(String requestId, String poNumber)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                " From " + xCMPIT.table +
                " Where " + xCMPIT.po_number + "='" + poNumber + "' and " + xCMPIT.request_id + "='" + requestId + "' " +
                " Order By " + xCMPIT.po_number + "," + xCMPIT.item_code;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectLinfoxGroupByPoNumber(String filename, String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select " + xCMPIT.po_number + "," + xCMPIT.file_name +
                " From " + xCMPIT.table +
                " Where " + xCMPIT.Validate_flag + "='Y' and " + xCMPIT.request_id + "='" + requestId + "' " +
                " and " + xCMPIT.file_name + "='" + filename + "' " +
                " Group By " + xCMPIT.po_number + "," + xCMPIT.file_name +
                " Order By " + xCMPIT.file_name + "," + xCMPIT.po_number +
                " ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String getCountNoErrorByFilename(String requestId, String filename)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "";
            sql = "Select  count(1) as cnt " +
                " From " + xCMPIT.table +
                " Where " + xCMPIT.request_id + "='" + requestId + "' and " + xCMPIT.file_name + "='" + filename + "' " +
                " and len(" + xCMPIT.error_message + ") <= 0";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["cnt"].ToString();
            }
            return chk;
        }
        public DataTable selectFilenameByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select  " + xCMPIT.file_name + ", row_cnt " +
                " From " + xCMPIT.table +
                " Where " + xCMPIT.request_id + "='" + requestId + "' " +
                " Group By " + xCMPIT.file_name + ", row_cnt " +
                " Order By " + xCMPIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void DeleteMmxTemp(String pathLog)
        {
            String sql = "Delete From " + xCMPIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
        }
        public DataTable selectByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                " From " + xCMPIT.table +
                " Where " + xCMPIT.request_id + "='" + requestId + "' " +
                " Order By " + xCMPIT.po_number + "," + xCMPIT.item_code;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
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
        public String dateYearShortToDBTemp(String date)
        {
            String chk = "", year = "", month = "", day = "";

            year = date.Substring(date.Length - 2);
            day = date.Substring(3, 2);
            month = date.Substring(0, 2);

            chk = "20" + year + month + day;

            return chk;
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
        public void updatePrcessFlag(String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCMPIT.table + " Set " + xCMPIT.process_flag + "='Y' " +
                " Where " + xCMPIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql, host, pathLog);
        }
        public String updateValidateFlagY(String po_number, String item_code, String store_code, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCMPIT.table + " Set " + xCMPIT.Validate_flag + "='Y' " +
                "Where " + xCMPIT.po_number + " = '" + po_number + "' and " + xCMPIT.item_code + "='" + item_code + "' " + xCMPIT.store_code + "='" + store_code + "' and " + xCMPIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateValidateFlag2(String po_number, String store_code, String agreement_number, String agreement_line_number
            , String supplierSiteCode, String suppName, String subInv_code, String price, String host, String pathLog)
        {
            String sql = "", chk = "";
            Double price1 = 0;
            Double.TryParse(price, out price1);
            sql = "Update " + xCMPIT.table + " Set " +
                " " + xCMPIT.AGREEEMENT_NUMBER + " ='" + agreement_number + "'" +
                ", " + xCMPIT.AGREEMENT_LINE_NUMBER + "='" + agreement_line_number + "' " +
                ", " + xCMPIT.SUPPLIER_SITE_CODE + "='" + supplierSiteCode + "' " +
                //", " + xCMPIT.su + "='" + suppName.Replace("'", "''") + "' " +        // รอแจ้ง
                ", " + xCMPIT.subinventory_code + "='" + subInv_code + "' " +
                ", " + xCMPIT.PRICE + "='" + price1.ToString() + "' " +
                "Where " + xCMPIT.po_number + " = '" + po_number + "' and " + xCMPIT.store_code + "='" + store_code + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String updateErrorMessage(String po_number, String item_code, String store_code, String msg, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCMPIT.table + " Set " + xCMPIT.error_message + "=" + xCMPIT.error_message + "+'," + msg.Replace("'", "''") + "' " +
                ", " + xCMPIT.Validate_flag + "='E' " +//VALIDATE_FLAG
                "Where " + xCMPIT.po_number + " = '" + po_number + "' and " 
                + xCMPIT.item_code + "='" + item_code + "' and " + xCMPIT.request_id + "='" + requestId + "' and "+xCMPIT.store_code +"='"+store_code+"'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public void insertBluk(List<String> mmx, String filename, String host, MaterialProgressBar pB1, String request_id, String pathLog)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", last_update_by = "0", lastUpdateTime = "null";
            String DELIVERY_INSTRUCTION = "", diriver_to_organization="", ERP_ITEM_CODE="", erp_subinventory_code="", ITEM_CATEGORY_NAME="", uom_code="";
            String orderDate = "", deliDate="", confDate="", ACC_SEG1="", ACC_SEG2="", ACC_SEG3="", ACC_SEG4="", ACC_SEG5="", ACC_SEG6="", AGREEEMENT_NUMBER="", AGREEMENT_LINE_NUMBER="";
            if (host == "kfc_po")
            {
                ConnectionString = conn.connKFC.ConnectionString;
            }
            StringBuilder sql = new StringBuilder();
            pB1.Minimum = 1;
            pB1.Maximum = mmx.Count();
            using (SqlCommand mConnection = new SqlCommand(ConnectionString))
            {
                List<string> Rows = new List<string>();
                foreach (String bbb in mmx)
                {
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    String[] aaa = bbb.Split(',');
                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    orderDate = dateYearShortToDBTemp(aaa[8]);
                    deliDate = dateYearShortToDBTemp(aaa[3]);
                    confDate = dateYearShortToDBTemp(aaa[10]);
                    //bbb += "('" + aaa[0] + "','" +
                    //aaa[11] + "','" + errMsg + "','" + aaa[6] + "','" +
                    //aaa[2] + "','" + aaa[4] + "','" + aaa[5] + "','" +
                    //aaa[1] + "','" + processFlag + "','" + aaa[7] + "','" +
                    //aaa[3] + "','" + aaa[8] + "','" + validateFlag + "'),";
                    sql.Append("Insert Into ").Append(xCMPIT.table).Append(" (").Append(xCMPIT.ACC_SEG1).Append(",").Append(xCMPIT.ACC_SEG2).Append(",").Append(xCMPIT.ACC_SEG3)
                        .Append(",").Append(xCMPIT.ACC_SEG4).Append(",").Append(xCMPIT.ACC_SEG5).Append(",").Append(xCMPIT.ACC_SEG6)
                        .Append(",").Append(xCMPIT.AGREEEMENT_NUMBER).Append(",").Append(xCMPIT.AGREEMENT_LINE_NUMBER).Append(",").Append(xCMPIT.confirm_qty)
                        .Append(",").Append(xCMPIT.conf_delivery_date).Append(",").Append(xCMPIT.creation_by).Append(",").Append(xCMPIT.creation_date)
                        .Append(",").Append(xCMPIT.delivery_date).Append(",").Append(xCMPIT.DELIVERY_INSTRUCTION).Append(",").Append(xCMPIT.deriver_to_location)
                        .Append(",").Append(xCMPIT.diriver_to_organization).Append(",").Append(xCMPIT.ERP_ITEM_CODE).Append(",").Append(xCMPIT.erp_subinventory_code)
                        .Append(",").Append(xCMPIT.error_message).Append(",").Append(xCMPIT.file_name).Append(",").Append(xCMPIT.ITEM_CATEGORY_NAME)
                        .Append(",").Append(xCMPIT.item_code).Append(",").Append(xCMPIT.last_update_by).Append(",").Append(xCMPIT.last_update_date)
                        .Append(",").Append(xCMPIT.order_date).Append(",").Append(xCMPIT.order_qty).Append(",").Append(xCMPIT.po_number)
                        .Append(",").Append(xCMPIT.po_status).Append(",").Append(xCMPIT.PRICE).Append(",").Append(xCMPIT.process_flag)
                        .Append(",").Append(xCMPIT.request_date).Append(",").Append(xCMPIT.store_code).Append(",").Append(xCMPIT.subinventory_code)
                        .Append(",").Append(xCMPIT.supplier_code).Append(",").Append(xCMPIT.SUPPLIER_SITE_CODE).Append(",").Append(xCMPIT.uom_code)
                        .Append(",").Append(xCMPIT.Validate_flag).Append(",").Append(xCMPIT.request_id).Append(", row_number, row_cnt ")
                        .Append(") Values ('")
                        .Append(ACC_SEG1).Append("','").Append(ACC_SEG2).Append("','").Append(ACC_SEG3)
                        .Append("','").Append(ACC_SEG4).Append("','").Append(ACC_SEG5).Append("','").Append(ACC_SEG6)
                        .Append("','").Append(AGREEEMENT_NUMBER).Append("','").Append(AGREEMENT_LINE_NUMBER).Append("','").Append(aaa[6]/*CONFIRM  QTY*/)
                        .Append("','").Append(confDate/*CONF_DILIVERY_DATE*/).Append("','").Append(createBy).Append("',getdate()")
                        .Append(",'").Append(deliDate/*delivery_date*/).Append("','").Append(DELIVERY_INSTRUCTION).Append("','").Append(initC.DELIVER_TO_LOCATTION)
                        .Append("','").Append(diriver_to_organization).Append("','").Append(ERP_ITEM_CODE).Append("','").Append(erp_subinventory_code)
                        .Append("','").Append(errMsg/*errMsg*/).Append("','").Append(filename.Trim().Replace(initC.PO005PathProcess, "")).Append("','").Append(ITEM_CATEGORY_NAME)
                        .Append("','").Append(aaa[7]/*ITEM_CODE*/).Append("','").Append(last_update_by).Append("',").Append(lastUpdateTime)
                        .Append(",'").Append(orderDate/*ORDER_DATE*/).Append("','").Append(aaa[5]/*ORDER_QTY*/).Append("','").Append(aaa[1]/*PO_NUMBER*/)
                        .Append("','").Append(aaa[9]/*.PO_STATUS*/).Append("',0,'").Append(processFlag)
                        .Append("','").Append(orderDate).Append("','").Append(aaa[0]/*STRORE_NO*/).Append("','").Append(aaa[4]/*Subinventory Code*/)
                        .Append("','").Append(aaa[2]/*SUPPLIER_CODE*/).Append("','','").Append(uom_code)
                        .Append("','").Append(validateFlag).Append("','").Append(request_id).Append("','").Append(i).Append("','").Append(mmx.Count).Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host, pathLog);
                }
            }
        }
        public String updateValidateFlag(String po_number, String line_number, String flag, String agreement_number, String host, String pathLog)
        {
            String sql = "";
            sql = "Update " + xCMPIT.table + " Set " + xCMPIT.Validate_flag + "='" + flag + "', " + xCMPIT.AGREEEMENT_NUMBER + " ='" + agreement_number + "' " +
                "Where " + xCMPIT.po_number + " = '" + po_number + "' and " + xCMPIT.AGREEMENT_LINE_NUMBER + "='" + line_number + "'";
            conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return "";
        }
        public void logProcessPO005(String programname, String startdatetime, String requestId)
        {
            String line1 = "", parameter = "", programstart = "", filename = "", recordError = "", txt = "", path = "", sql = "";
            int cntErr = 0, cntPass = 0;
            line1 = "Program : XCUST Interface PR<Linfox>To PO(ERP)" + Environment.NewLine;
            ControlMain cm = new ControlMain();
            path = cm.getPathLogProcess(programname);
            parameter = "Parameter : " + Environment.NewLine;
            parameter += "           Path Initial :" + initC.PathInitial + Environment.NewLine;
            parameter += "           Path Process :" + initC.PathProcess + Environment.NewLine;
            parameter += "           Path Error :" + initC.PathError + Environment.NewLine;
            parameter += "           Import Source :" + initC.ImportSource + Environment.NewLine;
            programstart = "Program Start : " + startdatetime + Environment.NewLine;
            sql = "Select count(1) as cnt, " + xCMPIT.file_name
                + " From " + xCMPIT.table
                + " Where " + xCMPIT.request_id + " ='" + requestId + "' " +
                "Group By " + xCMPIT.file_name;
            DataTable dtFile = conn.selectData(sql, "kfc_po");

            if (dtFile.Rows.Count > 0)
            {
                foreach (DataRow rowFile in dtFile.Rows)
                {
                    String valiPass = "", valiErr = "";
                    sql = "Select count(1) as cnt_vali " +
                        " From " + xCMPIT.table + " " +
                        " Where " + xCMPIT.request_id + " ='" + requestId + "' " +
                        " and " + xCMPIT.file_name + "='" + rowFile[xCMPIT.file_name].ToString() + "' and " + xCMPIT.Validate_flag + "='Y' ";

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
                        " From " + xCMPIT.table + " " +
                        " Where " + xCMPIT.request_id + " ='" + requestId + "' " +
                        " and " + xCMPIT.file_name + "='" + rowFile[xCMPIT.file_name].ToString() + "' and " + xCMPIT.Validate_flag + "='E' ";
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
                    filename += "Filename " + rowFile[xCMPIT.file_name].ToString() + ", Total = " + rowFile["cnt"].ToString() + ", Validate pass = " + valiPass + ", Record Error = " + valiErr + " " + Environment.NewLine;
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
            sql = "Select * From " + xCMPIT.table + " " +
                "Where " + xCMPIT.request_id + " ='" + requestId + "' " +
                "Order By " + xCMPIT.file_name + ", " + xCMPIT.po_number;
            DataTable dtErr = new DataTable();
            dtErr = conn.selectData(sql, "kfc_po");
            if (dtErr.Rows.Count > 0)
            {
                foreach (DataRow dtErr1 in dtErr.Rows)
                {
                    if (dtErr1[xCMPIT.error_message].ToString().Equals(""))
                    {
                        continue;
                    }
                    filename1 = dtErr1[xCMPIT.file_name].ToString();
                    if (!filename1.Equals(filename1old))
                    {
                        filename1old = filename1;
                        recordError += Environment.NewLine + "FileName : " + dtErr1[xCMPIT.file_name].ToString() + Environment.NewLine;
                    }
                    //recordError += "FileName " + dtErr1[xCLFPT.file_name].ToString() + Environment.NewLine;
                    recordError += "=>PO_NUMER = " + dtErr1[xCMPIT.po_number].ToString() + ",item_code = " + dtErr1[xCMPIT.item_code].ToString() + ",ERROR" + Environment.NewLine;
                    recordError += "     ====>" + dtErr1[xCMPIT.error_message].ToString() + Environment.NewLine;
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
    }
}
