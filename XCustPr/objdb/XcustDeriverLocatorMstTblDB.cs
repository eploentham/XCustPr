using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustDeriverLocatorMstTblDB
    {
        XcustDeriverLocatorMstTbl xCDLMT;
        ConnectDB conn;
        private InitC initC;
        public XcustDeriverLocatorMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCDLMT = new XcustDeriverLocatorMstTbl();
            xCDLMT.CREATION_DATE = "CREATION_DATE";
            xCDLMT.effective_date = "effective_date";
            xCDLMT.end_effective_date = "end_effective_date";
            xCDLMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCDLMT.location_code = "location_code";
            xCDLMT.location_name = "location_name";
            xCDLMT.status = "status";

            xCDLMT.table = "xcust_deriver_locator_mst_tbl";
            xCDLMT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCDLMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        /*
         * Error PO001-008 : Invalid Deliver To Location  
         */
        public DataTable selectLocator()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCDLMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String selectLocator1()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCDLMT.table;
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][xCDLMT.location_code].ToString().Trim();
            }
            return chk;
        }
        public String selectLocatorByInvtory(String name, String org)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select  SECONDARY_INVENTORY_NAME "+
                 "from xcust_subinventory_mst_tbl " +
                "where SECONDARY_INVENTORY_NAME = '"+name+"' " +
                "and ORGANIZATION_ID = '"+org+"' ";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["SECONDARY_INVENTORY_NAME"].ToString().Trim();
            }
            return chk;
        }
    }
}
