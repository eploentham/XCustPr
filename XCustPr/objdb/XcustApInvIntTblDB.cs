using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustApInvIntTblDB
    {
        public XcustApInvIntTbl xCAIIT;
        ConnectDB conn;
        private InitC initC;
        public XcustApInvIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCAIIT = new XcustApInvIntTbl();
            xCAIIT.ACCOUNTING_DATE = "ACCOUNTING_DATE";
            xCAIIT.ACCTS_PAY = "ACCTS_PAY";
            xCAIIT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCAIIT.ATTRIBUTE10 = "ATTRIBUTE10";
            xCAIIT.ATTRIBUTE11 = "ATTRIBUTE11";
            xCAIIT.ATTRIBUTE12 = "ATTRIBUTE12";
            xCAIIT.ATTRIBUTE13 = "ATTRIBUTE13";
            xCAIIT.ATTRIBUTE14 = "ATTRIBUTE14";
            xCAIIT.ATTRIBUTE15 = "ATTRIBUTE15";
            xCAIIT.ATTRIBUTE2 = "ATTRIBUTE2";
            xCAIIT.ATTRIBUTE3 = "ATTRIBUTE3";
            xCAIIT.ATTRIBUTE4 = "ATTRIBUTE4";
            xCAIIT.ATTRIBUTE5 = "ATTRIBUTE5";
            xCAIIT.ATTRIBUTE6 = "ATTRIBUTE6";
            xCAIIT.ATTRIBUTE7 = "ATTRIBUTE7";
            xCAIIT.ATTRIBUTE8 = "ATTRIBUTE8";
            xCAIIT.ATTRIBUTE9 = "ATTRIBUTE9";
            xCAIIT.BUSINESS_UNIT = "BUSINESS_UNIT";
            xCAIIT.DESCRIPTION = "DESCRIPTION";
            xCAIIT.GLOBAL_ATTRIBUTE1 = "GLOBAL_ATTRIBUTE1";
            xCAIIT.GLOBAL_ATTRIBUTE11 = "GLOBAL_ATTRIBUTE11";
            xCAIIT.GLOBAL_ATTRIBUTE12 = "GLOBAL_ATTRIBUTE12";
            xCAIIT.GLOBAL_ATTRIBUTE13 = "GLOBAL_ATTRIBUTE13";
            xCAIIT.GLOBAL_ATTRIBUTE14 = "GLOBAL_ATTRIBUTE14";
            xCAIIT.GLOBAL_ATTRIBUTE15 = "GLOBAL_ATTRIBUTE15";
            xCAIIT.GLOBAL_ATTRIBUTE16 = "GLOBAL_ATTRIBUTE16";
            xCAIIT.GLOBAL_ATTRIBUTE17 = "GLOBAL_ATTRIBUTE17";
            xCAIIT.GLOBAL_ATTRIBUTE18 = "GLOBAL_ATTRIBUTE18";
            xCAIIT.GLOBAL_ATTRIBUTE19 = "GLOBAL_ATTRIBUTE19";
            xCAIIT.GLOBAL_ATTRIBUTE2 = "GLOBAL_ATTRIBUTE2";
            xCAIIT.GLOBAL_ATTRIBUTE20 = "GLOBAL_ATTRIBUTE20";
            xCAIIT.GLOBAL_ATTRIBUTE3 = "GLOBAL_ATTRIBUTE3";
            xCAIIT.GLOBAL_ATTRIBUTE4 = "GLOBAL_ATTRIBUTE4";
            xCAIIT.GLOBAL_ATTRIBUTE5 = "GLOBAL_ATTRIBUTE5";
            xCAIIT.GLOBAL_ATTRIBUTE6 = "GLOBAL_ATTRIBUTE6";
            xCAIIT.GLOBAL_ATTRIBUTE7 = "GLOBAL_ATTRIBUTE7";
            xCAIIT.GLOBAL_ATTRIBUTE8 = "GLOBAL_ATTRIBUTE8";
            xCAIIT.GLOBAL_ATTRIBUTE9 = "GLOBAL_ATTRIBUTE9";
            xCAIIT.INVOICE_CUR_CODE = "INVOICE_CUR_CODE";
            xCAIIT.INVOICE_DATE = "INVOICE_DATE";
            xCAIIT.INVOICE_ID = "INVOICE_ID";
            xCAIIT.INVOICE_NUM = "INVOICE_NUM";
            xCAIIT.INVOICE_TYPE_LOOKUP_CODE = "INVOICE_TYPE_LOOKUP_CODE";
            xCAIIT.LEGAL_ENTITY = "LEGAL_ENTITY";
            xCAIIT.PAYMENT_CURR_CODE = "PAYMENT_CURR_CODE";
            xCAIIT.PAYMENT_METHOD = "PAYMENT_METHOD";
            xCAIIT.PAY_GROUP = "PAY_GROUP";
            xCAIIT.SOURCE = "SOURCE";
            xCAIIT.SOURCE_FROM = "SOURCE_FROM";
            xCAIIT.TERMS_DATE = "TERMS_DATE";
            xCAIIT.TERMS_NAME = "TERMS_NAME";
            xCAIIT.VENDOR_NAME = "VENDOR_NAME";
            xCAIIT.VENDOR_NUMBER = "VENDOR_NUMBER";
            xCAIIT.VENDOR_SITE_CODE = "VENDOR_SITE_CODE";

            xCAIIT.table = "XCUST_AP_INV_INT_TBL";
        }
        public String insert(XcustApInvIntTbl p)
        {
            String sql = "", chk = "";
            try
            {
                //if (p.OrpChtNum.Equals(""))
                //{
                //    return "";
                //}
                //p.RowNumber = selectMaxRowNumber(p.YearId);
                //p.Active = "1";
                String last_update_by = "0", creation_by = "0";
                sql = "Insert Into " + xCAIIT.table + "(" + xCAIIT.ACCOUNTING_DATE + "," + xCAIIT.ACCTS_PAY + "," + xCAIIT.ATTRIBUTE1 + "," +
                    xCAIIT.ATTRIBUTE10 + "," + xCAIIT.ATTRIBUTE11 + "," + xCAIIT.ATTRIBUTE12 + "," +
                    xCAIIT.ATTRIBUTE13 + "," + xCAIIT.ATTRIBUTE14 + "," + xCAIIT.ATTRIBUTE15 + "," +
                    xCAIIT.ATTRIBUTE2 + "," + xCAIIT.ATTRIBUTE3 + "," + xCAIIT.ATTRIBUTE4 + "," +
                    xCAIIT.ATTRIBUTE5 + "," + xCAIIT.ATTRIBUTE6 + "," + xCAIIT.ATTRIBUTE7 + "," +
                    xCAIIT.ATTRIBUTE8 + "," + xCAIIT.ATTRIBUTE9 + "," + xCAIIT.BUSINESS_UNIT + "," +
                    xCAIIT.DESCRIPTION + "," + xCAIIT.GLOBAL_ATTRIBUTE1 + "," + xCAIIT.GLOBAL_ATTRIBUTE11 + "," +
                    xCAIIT.GLOBAL_ATTRIBUTE12 + "," + xCAIIT.GLOBAL_ATTRIBUTE13 + "," + xCAIIT.GLOBAL_ATTRIBUTE14 + "," +
                    xCAIIT.GLOBAL_ATTRIBUTE15 + "," + xCAIIT.GLOBAL_ATTRIBUTE16 + "," + xCAIIT.GLOBAL_ATTRIBUTE17 + "," +
                    xCAIIT.GLOBAL_ATTRIBUTE18 + "," + xCAIIT.GLOBAL_ATTRIBUTE19 + "," + xCAIIT.GLOBAL_ATTRIBUTE2 + "," +
                    xCAIIT.GLOBAL_ATTRIBUTE20 + "," + xCAIIT.GLOBAL_ATTRIBUTE3 + "," + xCAIIT.GLOBAL_ATTRIBUTE4 + "," +
                    xCAIIT.GLOBAL_ATTRIBUTE5 + "," + xCAIIT.GLOBAL_ATTRIBUTE6 + "," + xCAIIT.GLOBAL_ATTRIBUTE7 + "," +
                    xCAIIT.GLOBAL_ATTRIBUTE8 + "," + xCAIIT.GLOBAL_ATTRIBUTE9 + "," + xCAIIT.INVOICE_CUR_CODE + "," +
                    xCAIIT.INVOICE_DATE + "," + xCAIIT.INVOICE_ID + "," + xCAIIT.INVOICE_NUM + "," +
                    xCAIIT.INVOICE_TYPE_LOOKUP_CODE + "," + xCAIIT.LEGAL_ENTITY + "," + xCAIIT.PAYMENT_CURR_CODE + "," +
                    xCAIIT.PAYMENT_METHOD + "," + xCAIIT.PAY_GROUP + "," + xCAIIT.SOURCE + "," +
                    xCAIIT.SOURCE_FROM + "," + xCAIIT.TERMS_DATE + "," + xCAIIT.TERMS_NAME + "," +
                    xCAIIT.VENDOR_NAME + "," + xCAIIT.VENDOR_NUMBER + "," + xCAIIT.VENDOR_SITE_CODE + 
                    ") " +
                    "Values('" + p.ACCOUNTING_DATE + "','" + p.ACCTS_PAY + "','" + p.ATTRIBUTE1 + "','" +
                    p.ATTRIBUTE10 + "','" + p.ATTRIBUTE11 + "','" + p.ATTRIBUTE12 + "','" +
                    p.ATTRIBUTE13 + "','" + p.ATTRIBUTE14 + "','" + p.ATTRIBUTE15 + "','" +
                    p.ATTRIBUTE2 + "','" + p.ATTRIBUTE3 + "','" + p.ATTRIBUTE4 + "','" +
                    p.ATTRIBUTE5 + "','" + p.ATTRIBUTE6 + "','" + p.ATTRIBUTE7 + "','" +
                    p.ATTRIBUTE8 + "','" + p.ATTRIBUTE9 + "','" + p.BUSINESS_UNIT + "','" +
                    p.DESCRIPTION + "','" + p.GLOBAL_ATTRIBUTE1 + "','" + p.GLOBAL_ATTRIBUTE11 + "','" +
                    p.GLOBAL_ATTRIBUTE12 + "','" + p.GLOBAL_ATTRIBUTE13 + "','" + p.GLOBAL_ATTRIBUTE14 + "','" +
                    p.GLOBAL_ATTRIBUTE15 + "','" + p.GLOBAL_ATTRIBUTE16 + "','" + p.GLOBAL_ATTRIBUTE17 + "','" +
                    p.GLOBAL_ATTRIBUTE18 + "','" + p.GLOBAL_ATTRIBUTE19 + "','" + p.GLOBAL_ATTRIBUTE2 + "','" +
                    p.GLOBAL_ATTRIBUTE20 + "','" + p.GLOBAL_ATTRIBUTE3 + "','" + p.GLOBAL_ATTRIBUTE4 + "','" +
                    p.GLOBAL_ATTRIBUTE5 + "','" + p.GLOBAL_ATTRIBUTE6 + "','" + p.GLOBAL_ATTRIBUTE7 + "','" +
                    p.GLOBAL_ATTRIBUTE8 + "','" + p.GLOBAL_ATTRIBUTE9 + "','" + p.INVOICE_CUR_CODE + "','" +
                    p.INVOICE_DATE + "','" + p.INVOICE_ID + "','" + p.INVOICE_NUM + "','" +
                    p.INVOICE_TYPE_LOOKUP_CODE + "','" + p.LEGAL_ENTITY + "','" + p.PAYMENT_CURR_CODE + "','" +
                    p.PAYMENT_METHOD + "','" + p.PAY_GROUP + "','" + p.SOURCE + "','" +
                    p.SOURCE_FROM + "','" + p.TERMS_DATE + "','" + p.TERMS_NAME + "','" +
                    p.VENDOR_NAME + "','" + p.VENDOR_NUMBER + "','" + p.VENDOR_SITE_CODE + "'" +                    
                    ") ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po");
                //chk = p.RowNumber;
                //chk = p.Code;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }

            return chk;
        }
    }
}
