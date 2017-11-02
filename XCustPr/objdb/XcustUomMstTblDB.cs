using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustUomMstTblDB
    {
        XcustUomMstTbl xCUMT;
        ConnectDB conn;
        private InitC initC;
        public XcustUomMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCUMT = new XcustUomMstTbl();
            xCUMT.CREATION_DATE = "CREATION_DATE";
            xCUMT.DISABLE_DATE = "DISABLE_DATE";
            xCUMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCUMT.UNIT_OF_MEASURE_ID = "UNIT_OF_MEASURE_ID";
            xCUMT.UOM_CODE = "UOM_CODE";

            xCUMT.table = "XCUST_UOM_MST_TBL";
            xCUMT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCUMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        /*
         * Error PO001-016 : Invalid UOM
         */
        public DataTable selectByCode(String code)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCUMT.table + " Where " + xCUMT.UOM_CODE + "='" + code + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
    }
}
