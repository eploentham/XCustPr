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
            xCIMT = new XcustItemMstTbl();
            xCIMT.ATTRIBUTE1 = "ATTRIBUTE1";
            //xCIMT.ATTRIBUTE1 = "ATTRIBUTE1";
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
            xCIMT.ITEM_REFERENCE1 = "ITEM_REFERENCE1";// SUPPLIER_ITEM_CODE
            xCIMT.ITEM_STATUS = "ITEM_STATUS";
            xCIMT.ITEM_TYPE = "ITEM_TYPE";
            xCIMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCIMT.ORGAINZATION_ID = "ORGANIZATION_ID";
            xCIMT.PRIMARY_UOM = "PRIMARY_UOM";

            xCIMT.table = "xcust_item_mst_tbl";
            xCIMT.pkField = "";
        }
        public XcustItemMstTbl setData(DataRow row)
        {
            XcustItemMstTbl item;
            item = new XcustItemMstTbl();
            item.ATTRIBUTE1 = row[xCIMT.ATTRIBUTE1].ToString();
            //item.ATTRIBUTE1 = "ATTRIBUTE1";
            item.ATTRIBUTE11 = row[xCIMT.ATTRIBUTE11].ToString();
            item.ATTRIBUTE12 = row[xCIMT.ATTRIBUTE12].ToString();
            item.ATTRIBUTE13 = row[xCIMT.ATTRIBUTE13].ToString();
            item.ATTRIBUTE14 = row[xCIMT.ATTRIBUTE14].ToString();
            item.ATTRIBUTE15 = row[xCIMT.ATTRIBUTE15].ToString();
            item.ATTRIBUTE2 = row[xCIMT.ATTRIBUTE2].ToString();
            item.ATTRIBUTE3 = row[xCIMT.ATTRIBUTE3].ToString();
            item.ATTRIBUTE4 = row[xCIMT.ATTRIBUTE4].ToString();
            item.ATTRIBUTE5 = row[xCIMT.ATTRIBUTE5].ToString();
            item.ATTRIBUTE6 = row[xCIMT.ATTRIBUTE6].ToString();
            item.ATTRIBUTE7 = row[xCIMT.ATTRIBUTE7].ToString();
            item.ATTRIBUTE8 = row[xCIMT.ATTRIBUTE8].ToString();
            item.ATTRIBUTE9 = row[xCIMT.ATTRIBUTE9].ToString();
            item.CREATION_DATE = row[xCIMT.CREATION_DATE].ToString();
            item.DESCRIPTION = row[xCIMT.DESCRIPTION].ToString();
            item.INVENTORY_ITEM_ID = row[xCIMT.INVENTORY_ITEM_ID].ToString();
            item.ITEM_CATEGORY_CODE = row[xCIMT.ITEM_CATEGORY_CODE].ToString();
            item.ITEM_CATEGORY_NAME = row[xCIMT.ITEM_CATEGORY_NAME].ToString();
            item.ITEM_CLASS_CODE = row[xCIMT.ITEM_CLASS_CODE].ToString();
            item.ITEM_CLASS_NAME = row[xCIMT.ITEM_CLASS_NAME].ToString();
            item.ITEM_CODE = row[xCIMT.ITEM_CODE].ToString();
            item.ITEM_NAME = row[xCIMT.ITEM_NAME].ToString();
            item.ITEM_REFERENCE1 = row[xCIMT.ITEM_REFERENCE1].ToString();
            item.ITEM_STATUS = row[xCIMT.ITEM_STATUS].ToString();
            item.ITEM_TYPE = row[xCIMT.ITEM_TYPE].ToString();
            item.LAST_UPDATE_DATE = row[xCIMT.LAST_UPDATE_DATE].ToString();
            item.ORGAINZATION_ID = row[xCIMT.ORGAINZATION_ID].ToString();
            item.PRIMARY_UOM = row[xCIMT.PRIMARY_UOM].ToString();
            return item;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCIMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String selectItemCodeByOrgRef1(String OrgId, String ref1)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCIMT.table + " where " + xCIMT.ITEM_REFERENCE1 + "  = '"+ ref1 + "' and "+xCIMT.ORGAINZATION_ID + " ='"+OrgId+"'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][xCIMT.ITEM_CODE].ToString().Trim();
            }
            return chk;
        }
        public Boolean validateItemCodeByOrgRef1(String OrgId, String itemCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCIMT.table + " where " + xCIMT.ITEM_REFERENCE1 + "  = '" + itemCode + "' and " + xCIMT.ORGAINZATION_ID + " ='" + OrgId + "'";
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
        public Boolean validateItemCodeByOrgRefLinfox(String OrgId, String itemCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCIMT.table + 
                " where " + xCIMT.ITEM_CODE + "  = '" + itemCode + "' and " + xCIMT.ORGAINZATION_ID + " ='" + OrgId + "'";
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
