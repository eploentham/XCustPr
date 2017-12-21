using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustSupplierSiteMstTblDB
    {
        XcustSupplierSiteMstTbl xCSSMT;
        ConnectDB conn;
        private InitC initC;

        public XcustSupplierSiteMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCSSMT = new XcustSupplierSiteMstTbl();
            xCSSMT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCSSMT.ATTRIBUTE10 = "ATTRIBUTE10";
            xCSSMT.ATTRIBUTE11 = "ATTRIBUTE11";
            xCSSMT.ATTRIBUTE12 = "ATTRIBUTE12";
            xCSSMT.ATTRIBUTE13 = "ATTRIBUTE13";
            xCSSMT.ATTRIBUTE14 = "ATTRIBUTE14";
            xCSSMT.ATTRIBUTE15 = "ATTRIBUTE15";
            xCSSMT.ATTRIBUTE2 = "ATTRIBUTE2";
            xCSSMT.ATTRIBUTE3 = "ATTRIBUTE3";
            xCSSMT.ATTRIBUTE4 = "ATTRIBUTE4";
            xCSSMT.ATTRIBUTE5 = "ATTRIBUTE5";
            xCSSMT.ATTRIBUTE6 = "ATTRIBUTE6";
            xCSSMT.ATTRIBUTE7 = "ATTRIBUTE7";
            xCSSMT.ATTRIBUTE8 = "ATTRIBUTE8";
            xCSSMT.ATTRIBUTE9 = "ATTRIBUTE9";
            xCSSMT.CREATION_DATE = "CREATION_DATE";
            xCSSMT.EMAIL_ADDRESS = "EMAIL_ADDRESS";
            xCSSMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCSSMT.LOCATION_ID = "LOCATION_ID";
            xCSSMT.MATCH_OPTION = "MATCH_OPTION";
            xCSSMT.PAY_SITE_FLAG = "PAY_SITE_FLAG";
            xCSSMT.PURCHASING_SITE_FLAG = "PURCHASING_SITE_FLAG";
            xCSSMT.RFQ_ONLY_SITE_FLAG = "RFQ_ONLY_SITE_FLAG";
            xCSSMT.SUPPLIER_NOTIF_METHOD = "SUPPLIER_NOTIF_METHOD";
            xCSSMT.VENDOR_ID = "VENDOR_ID";
            xCSSMT.VENDOR_SITE_CODE = "VENDOR_SITE_CODE";
            xCSSMT.VENDOR_SITE_ID = "VENDOR_SITE_ID";
            xCSSMT.VENDOR_SITE_SPK_ID = "VENDOR_SITE_SPK_ID";

            xCSSMT.table = "XCUST_SUPPLIER_SITE_MST_TBL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCSSMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public XcustSupplierSiteMstTbl setData(DataRow row)
        {
            XcustSupplierSiteMstTbl item;
            item = new XcustSupplierSiteMstTbl();
            item.ATTRIBUTE1 = row[xCSSMT.ATTRIBUTE1].ToString();
            item.ATTRIBUTE2 = row[xCSSMT.ATTRIBUTE2].ToString();
            item.ATTRIBUTE3 = row[xCSSMT.ATTRIBUTE3].ToString();
            item.ATTRIBUTE4 = row[xCSSMT.ATTRIBUTE4].ToString();
            item.ATTRIBUTE5 = row[xCSSMT.ATTRIBUTE5].ToString();
            item.ATTRIBUTE6 = row[xCSSMT.ATTRIBUTE6].ToString();
            item.ATTRIBUTE7 = row[xCSSMT.ATTRIBUTE7].ToString();
            item.ATTRIBUTE8 = row[xCSSMT.ATTRIBUTE8].ToString();
            item.ATTRIBUTE9 = row[xCSSMT.ATTRIBUTE9].ToString();
            item.ATTRIBUTE10 = row[xCSSMT.ATTRIBUTE10].ToString();

            item.CREATION_DATE = row[xCSSMT.CREATION_DATE].ToString();
            item.EMAIL_ADDRESS = row[xCSSMT.EMAIL_ADDRESS].ToString();
            item.LAST_UPDATE_DATE = row[xCSSMT.LAST_UPDATE_DATE].ToString();
            item.LOCATION_ID = row[xCSSMT.LOCATION_ID].ToString();
            item.MATCH_OPTION = row[xCSSMT.MATCH_OPTION].ToString();
            item.PAY_SITE_FLAG = row[xCSSMT.PAY_SITE_FLAG].ToString();
            item.PURCHASING_SITE_FLAG = row[xCSSMT.PURCHASING_SITE_FLAG].ToString();
            item.RFQ_ONLY_SITE_FLAG = row[xCSSMT.RFQ_ONLY_SITE_FLAG].ToString();
            item.SUPPLIER_NOTIF_METHOD = row[xCSSMT.SUPPLIER_NOTIF_METHOD].ToString();
            item.VENDOR_ID = row[xCSSMT.VENDOR_ID].ToString();

            item.VENDOR_SITE_CODE = row[xCSSMT.VENDOR_SITE_CODE].ToString();
            item.VENDOR_SITE_ID = row[xCSSMT.VENDOR_SITE_ID].ToString();
            item.VENDOR_SITE_SPK_ID = row[xCSSMT.VENDOR_SITE_SPK_ID].ToString();

            return item;
        }
        public String getMinVendorSiteIdByVendorId(String vendor_id)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select t.VENDOR_SITE_CODE "+
                "from XCUST_SUPPLIER_SITE_MST_TBL t " +
                "where VENDOR_ID = '"+vendor_id+"' " +
                "and PURCHASING_SITE_FLAG = 'Y' " +
                "and t.VENDOR_SITE_ID = (select min(tt.VENDOR_SITE_ID) " +
                 "                       from XCUST_SUPPLIER_SITE_MST_TBL tt " +
                 "                       where tt.VENDOR_ID = t.vendor_id " +
                 "                       and tt.PURCHASING_SITE_FLAG = t.PURCHASING_SITE_FLAG)";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["VENDOR_SITE_CODE"].ToString();
            }
            return chk;
        }
        public String getMinVendorSiteIdByVendorIdPO008(String vendor_id)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "";
            if (vendor_id.Equals(""))
            {
                return "";
            }
            sql = "SELECT T.VENDOR_SITE_CODE " +
                "FROM XCUST_SUPPLIER_SITE_MST_TBL T " +
                "WHERE VENDOR_ID = "+ vendor_id + " " +
                "AND PURCHASING_SITE_FLAG = 'Y' " +
                "AND T.VENDOR_SITE_ID = (SELECT  " +
                 "                       MAX(TT.VENDOR_SITE_ID) " +
                 "                       FROM  " +
                 "                       XCUST_SUPPLIER_SITE_MST_TBL TT " +
                 " WHERE TT.VENDOR_ID  " +
                 " = T.VENDOR_ID and " +
                 " TT.PURCHASING_SITE_FLAG = T.PURCHASING_SITE_FLAG                          )";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["VENDOR_SITE_CODE"].ToString();
            }
            return chk;
        }
        public String getVendorSiteCode(String vendor_spk_id)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select " + xCSSMT.VENDOR_SITE_CODE + " " +
                "From " + xCSSMT.table + " " +
                "Where " + xCSSMT.VENDOR_SITE_SPK_ID + "='" + vendor_spk_id + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][xCSSMT.VENDOR_SITE_CODE].ToString();
            }
            return chk;
        }
        public String getVendorSiteCodeBySupplierCode(String supplier_code)
        {
            DataTable dt = new DataTable();
            String chk = "", vendorSPKId="";
            String sql = "select vendor_id "+
                "from XCUST_SUPPLIER_MST_TBL "+
                "where SUPPLIER_NUMBER = '"+ supplier_code+"'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["vendor_id"].ToString();
                vendorSPKId = getMinVendorSiteIdByVendorId(chk);
                chk = vendorSPKId.Equals("") ? getVendorSiteCode(vendorSPKId) : "";
            }
            return chk;
        }
    }
}
