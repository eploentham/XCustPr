using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustSubInventoryMstTblDB
    {
        XcustSubInventoryMstTbl xCSIMT;
        ConnectDB conn;
        private InitC initC;
        public XcustSubInventoryMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCSIMT = new XcustSubInventoryMstTbl();
            xCSIMT.attribute1 = "attribute1";
            xCSIMT.attribute2 = "attribute2";
            xCSIMT.CREATION_DATE = "CREATION_DATE";
            xCSIMT.DESCRIPTION = "DESCRIPTION";
            xCSIMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCSIMT.LOCATOR_TYPE = "LOCATOR_TYPE";
            xCSIMT.ORGAINZATION_ID = "ORGAINZATION_ID";
            xCSIMT.SECONDARY_INVENTORY_NAME = "SECONDARY_INVENTORY_NAME";
            xCSIMT.SUBINVENTORY_ID = "SUBINVENTORY_ID";

            xCSIMT.table = "XCUST_SUBINVENTORY_MST_TBL";
            xCSIMT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCSIMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        /*
         * Error PO001-010 : Invalid Subinventory Code
         */
        public DataTable validateSubInventoryCode(String ordId, String StoreCode)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCSIMT.table+ 
                " Where "+ xCSIMT.SUBINVENTORY_ID + " = '"+ ordId+"' and "+ xCSIMT.attribute1 + " = '"+ StoreCode+"'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String validateSubInventoryCode1(String ordId, String StoreCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCSIMT.table +
                " Where " + xCSIMT.SUBINVENTORY_ID + " = '" + ordId + "' and " + xCSIMT.attribute1 + " = '" + StoreCode + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][xCSIMT.SECONDARY_INVENTORY_NAME].ToString().Trim();
            }
            return chk;
        }
    }
}
