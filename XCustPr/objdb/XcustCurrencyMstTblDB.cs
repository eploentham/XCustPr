using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustCurrencyMstTblDB
    {
        XcustCurrencyMstTbl xCCMT;
        ConnectDB conn;
        private InitC initC;
        public XcustCurrencyMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCCMT = new XcustCurrencyMstTbl();
            xCCMT.CREATION_DATE = "CREATION_DATE";
            xCCMT.CURRENCY_CODE = "CURRENCY_CODE";
            xCCMT.CURRENCY_NAME = "CURRENCY_NAME";
            xCCMT.DESCRIPTION = "DESCRIPTION";
            xCCMT.ENABLE = "ENABLE";
            xCCMT.END_DATE = "END_DATE";
            xCCMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCCMT.START_DATE = "START_DATE";

            xCCMT.table = "xcust_currency_mst_tbl";
            xCCMT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCCMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        /*
         * Error PO001-013 : Invalid Currency Code
         */
        public DataTable selectByCode(String code)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCCMT.table+" Where "+xCCMT.CURRENCY_CODE +"='"+code+"'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public Boolean validateCurrencyCodeBycurrCode(String currCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCCMT.table + " where " + xCCMT.CURRENCY_CODE + "  = '" + currCode + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
