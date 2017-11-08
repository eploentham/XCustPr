using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            String sql = "select * From " + xCLFPT.table + " Where " + xCLFPT.file_name+"='"+filename+"'";
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
        public void DeleteLinfoxTemp()
        {
            String sql = "Delete From " + xCLFPT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");            
        }
        public void DeleteLinfoxTempByFilename(String filename)
        {
            String sql = "Delete From " + xCLFPT.table + " Where " + xCLFPT.file_name + "='" + filename + "'";
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        
        public void insertBluk(List<String> linfox, String filename, String host, MaterialProgressBar pB1)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy="0", createDate= "GETDATE()", lastUpdateBy="0", lastUpdateTime="null";
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
                        .Append(",").Append(xCLFPT.store_code).Append(",").Append(xCLFPT.REQUEST_TIME).Append(") Values ('")
                        .Append(aaa[0]).Append("','").Append(aaa[1]).Append("','").Append(aaa[2])
                        .Append("','").Append(aaa[3]).Append("',").Append(aaa[4]).Append(",'").Append(aaa[5])
                        .Append("','").Append(aaa[7]).Append("',").Append(aaa[8]).Append(",'").Append(aaa[9])                        
                        .Append("','").Append(aaa[10]).Append("','").Append(validateFlag).Append("','").Append(processFlag)
                        .Append("','").Append(errMsg).Append("','").Append(createBy).Append("',").Append(createDate)
                        .Append(",'").Append(lastUpdateBy).Append("',").Append(lastUpdateTime).Append(",'").Append(filename.Trim().Replace(initC.PathProcess,""))
                        .Append("','").Append(aaa[11]).Append("','").Append(aaa[6]).Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host);
                }
            }
        }
        public String updateValidateFlag(String po_number, String line_number, String flag, String agreement_number, String host)
        {
            String sql = "";
            sql = "Update "+xCLFPT.table +" Set "+xCLFPT.VALIDATE_FLAG+"='"+flag+"', "+xCLFPT.AGREEEMENT_NUMBER+" ='"+ agreement_number+"' "+
                "Where " +xCLFPT.PO_NUMBER+" = '"+po_number+"' and "+xCLFPT.LINE_NUMBER+"='"+line_number+"'";
            conn.ExecuteNonQuery(sql.ToString(), host);

            return "";
        }
        
        public DataTable selectValidateFlagYGroupByPoNumber()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCLFPT.table + " Where " + xCLFPT.VALIDATE_FLAG + "='Y' Group By "+xCLFPT.PO_NUMBER;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
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
