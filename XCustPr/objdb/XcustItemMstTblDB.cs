using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustItemMstTblDB
    {
        XcustItemMstTbl xCIMT;
        ConnectDB conn;
        private InitC initC;
        public XcustItemMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCIMT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCIMT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCIMT.ATTRIBUTE11 = "ATTRIBUTE11";
            xCIMT.ATTRIBUTE12 = "ATTRIBUTE12";
            xCIMT.ATTRIBUTE13 = "ATTRIBUTE13";
            xCIMT.ATTRIBUTE14 = "ATTRIBUTE14";
            xCIMT.ATTRIBUTE15 = "ATTRIBUTE15";
            xCIMT.ATTRIBUTE2 = "ATTRIBUTE2";
            xCIMT.ATTRIBUTE3 = "ATTRIBUTE3";
            xCIMT.ATTRIBUTE4 = "ATTRIBUTE4";
            xCIMT.ATTRIBUTE5 = "ATTRIBUTE5";
            xCIMT.ATTRIBUTE6 = "ATTRIBUTE6";
            xCIMT.ATTRIBUTE7 = "ATTRIBUTE7";
            xCIMT.ATTRIBUTE8 = "ATTRIBUTE8";
            xCIMT.ATTRIBUTE9 = "ATTRIBUTE9";
            xCIMT.CREATION_DATE = "CREATION_DATE";
            xCIMT.DESCRIPTION = "DESCRIPTION";
            xCIMT.INVENTORY_ITEM_ID = "INVENTORY_ITEM_ID";
            xCIMT.ITEM_CATEGORY_CODE = "ITEM_CATEGORY_CODE";
            xCIMT.ITEM_CATEGORY_NAME = "ITEM_CATEGORY_NAME";
            xCIMT.ITEM_CLASS_CODE = "ITEM_CLASS_CODE";
            xCIMT.ITEM_CLASS_NAME = "ITEM_CLASS_NAME";
            xCIMT.ITEM_CODE = "ITEM_CODE";
            xCIMT.ITEM_NAME = "ITEM_NAME";
            xCIMT.ITEM_REFERENCE1 = "ITEM_REFERENCE1";
            xCIMT.ITEM_STATUS = "ITEM_STATUS";
            xCIMT.ITEM_TYPE = "ITEM_TYPE";
            xCIMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCIMT.ORGAINZATION_ID = "ORGAINZATION_ID";
            xCIMT.PRIMARY_UOM = "PRIMARY_UOM";

            xCIMT.table = "xcust_item_mst_tbl";
            xCIMT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCIMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
    }
}
