using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustBuMstTblDB
    {
        public XcustBuMstTbl xCBMT;
        ConnectDB conn;
        private InitC initC;
        public XcustBuMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCBMT = new XcustBuMstTbl();
            xCBMT.BU_ID = "BU_ID";
            xCBMT.BU_NAME = "BU_NAME";
            xCBMT.CREATION_DATE = "CREATION_DATE";
            xCBMT.DATE_FROM = "DATE_FROM";
            xCBMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCBMT.LEGAL_ENTITY_ID = "LEGAL_ENTITY_ID";
            xCBMT.PRIMARY_LEDGER_ID = "PRIMARY_LEDGER_ID";
            xCBMT.SHORT_CODE = "SHORT_CODE";
            xCBMT.STATUS = "STATUS";

            xCBMT.table = "XCUST_BU_MST_TBL";
            xCBMT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCBMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        /*
         * Error PO001-004 : Invalid Requisitioning BU
         * Error PO001-014 : Invalid Procurement BU
         */
        public DataTable selectActive()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCBMT.table+ " where "+ xCBMT.STATUS + "  = 'A'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String selectActive1()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCBMT.table + " where " + xCBMT.STATUS + "  = 'A'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][xCBMT.BU_ID].ToString().Trim();
            }
            return chk;
        }
        public String selectActiveBuName()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCBMT.table + " where " + xCBMT.STATUS + "  = 'A'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][xCBMT.BU_NAME].ToString().Trim();
            }
            return chk;
        }
    }
}
