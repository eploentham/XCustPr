using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustSupplierMstTblDB
    {
        XcustSupplierMstTbl xCSMT;
        ConnectDB conn;
        private InitC initC;
        public XcustSupplierMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCSMT = new XcustSupplierMstTbl();
            xCSMT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCSMT.ATTRIBUTE2 = "ATTRIBUTE2";
            xCSMT.ATTRIBUTE3 = "ATTRIBUTE3";
            xCSMT.ATTRIBUTE4 = "ATTRIBUTE4";
            xCSMT.ATTRIBUTE5 = "ATTRIBUTE5";
            xCSMT.CREATION_DATE = "CREATION_DATE";
            xCSMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCSMT.SUPPLIER_NAME = "SUPPLIER_NAME";
            xCSMT.SUPPLIER_NUMBER = "SUPPLIER_NUMBER";
            xCSMT.SUPPLIER_REG_ID = "SUPPLIER_REG_ID";
            xCSMT.VENDOR_ID = "VENDOR_ID";

            xCSMT.table = "XCUST_SUPPLIER_MST_TBL";
            xCSMT.pkField = "";
        }
        public XcustSupplierMstTbl setData(DataRow row)
        {
            XcustSupplierMstTbl item;
            item = new XcustSupplierMstTbl();
            item.ATTRIBUTE1 = row[xCSMT.ATTRIBUTE1].ToString();            
            item.ATTRIBUTE2 = row[xCSMT.ATTRIBUTE2].ToString();
            item.ATTRIBUTE3 = row[xCSMT.ATTRIBUTE3].ToString();
            item.ATTRIBUTE4 = row[xCSMT.ATTRIBUTE4].ToString();
            item.ATTRIBUTE5 = row[xCSMT.ATTRIBUTE5].ToString();
            item.CREATION_DATE = row[xCSMT.CREATION_DATE].ToString();            
            item.SUPPLIER_NAME = row[xCSMT.SUPPLIER_NAME].ToString();
            item.SUPPLIER_NUMBER = row[xCSMT.SUPPLIER_NUMBER].ToString();
            item.LAST_UPDATE_DATE = row[xCSMT.LAST_UPDATE_DATE].ToString();
            item.SUPPLIER_REG_ID = row[xCSMT.SUPPLIER_REG_ID].ToString();
            item.VENDOR_ID = row[xCSMT.VENDOR_ID].ToString();
            return item;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCSMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public Boolean validateSupplierBySupplierCode(String suppCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "Select * From " + xCSMT.table + " Where " + xCSMT.SUPPLIER_NUMBER + "  = '" + suppCode + "'";
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
        public Boolean getDirectSupplierBySupplierCode(String suppCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "Select * From " + xCSMT.table + " Where " + xCSMT.SUPPLIER_NUMBER + "  = '" + suppCode + "' and "+xCSMT.ATTRIBUTE1 +"='Y'";
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
