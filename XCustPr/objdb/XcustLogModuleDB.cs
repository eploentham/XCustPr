using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustLogModuleDB
    {
        public XcustLogModule xCLM;
        ConnectDB conn;
        private InitC initC;

        public XcustLogModuleDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCLM = new XcustLogModule();
            xCLM.error_message = "error_message";
            xCLM.module_name = "module_name";
            xCLM.request_id = "request_id";
            xCLM.row1 = "row1";

            xCLM.table = "xcust_log_module";
        }
        public String insertLog(String module_name,String request_id, String err_msg, String pathLog)
        {
            String chk = "", sql="";

            sql = "Insert Into "+xCLM.table+" ("+xCLM.module_name+","+xCLM.request_id+","+xCLM.error_message+", date_current) "+
                "Values('"+module_name+"','"+request_id+"','"+err_msg+"',getdate())";
            chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
            return chk;
        }
    }
}
