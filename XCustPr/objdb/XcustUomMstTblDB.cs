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
        public XcustUomMstTbl setData(DataRow row)
        {
            XcustUomMstTbl item;
            item = new XcustUomMstTbl();
            item.CREATION_DATE = row[xCUMT.CREATION_DATE].ToString();
            item.DISABLE_DATE = row[xCUMT.DISABLE_DATE].ToString();            
            item.LAST_UPDATE_DATE = row[xCUMT.LAST_UPDATE_DATE].ToString();
            item.UNIT_OF_MEASURE_ID = row[xCUMT.UNIT_OF_MEASURE_ID].ToString();
            item.UOM_CODE = row[xCUMT.UOM_CODE].ToString();            

            return item;
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
        public Boolean validateUOMCodeByUOMCode(String uomCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCUMT.table + " where " + xCUMT.UOM_CODE + "  = '" + uomCode + "'";
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
