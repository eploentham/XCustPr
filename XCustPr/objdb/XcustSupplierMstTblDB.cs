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
        public Boolean validateSupplierBySupplierCode(String suppCode)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCSMT.table + " where " + xCSMT.SUPPLIER_NUMBER + "  = '" + suppCode + "'";
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
