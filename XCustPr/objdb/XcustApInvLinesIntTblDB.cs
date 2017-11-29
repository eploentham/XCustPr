using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustApInvLinesIntTblDB
    {
        public XcustApInvLinesIntTbl xCAILIT;
        ConnectDB conn;
        private InitC initC;
        public XcustApInvLinesIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCAILIT = new XcustApInvLinesIntTbl();
            xCAILIT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCAILIT.ATTRIBUTE10 = "ATTRIBUTE10";
            xCAILIT.ATTRIBUTE11 = "ATTRIBUTE11";
            xCAILIT.ATTRIBUTE12 = "ATTRIBUTE12";
            xCAILIT.ATTRIBUTE13 = "ATTRIBUTE13";
            xCAILIT.ATTRIBUTE14 = "ATTRIBUTE14";
            xCAILIT.ATTRIBUTE15 = "ATTRIBUTE15";
            xCAILIT.ATTRIBUTE2 = "ATTRIBUTE2";
            xCAILIT.ATTRIBUTE3 = "ATTRIBUTE3";
            xCAILIT.ATTRIBUTE4 = "ATTRIBUTE4";
            xCAILIT.ATTRIBUTE5 = "ATTRIBUTE5";
            xCAILIT.ATTRIBUTE6 = "ATTRIBUTE6";
            xCAILIT.ATTRIBUTE7 = "ATTRIBUTE7";
            xCAILIT.ATTRIBUTE8 = "ATTRIBUTE8";
            xCAILIT.ATTRIBUTE9 = "ATTRIBUTE9";
            xCAILIT.AWT_GROUP_NAME = "AWT_GROUP_NAME";
            xCAILIT.DESCRIPTION = "DESCRIPTION";
            xCAILIT.GLOBAL_ATTRIBUTE1 = "GLOBAL_ATTRIBUTE1";
            xCAILIT.GLOBAL_ATTRIBUTE11 = "GLOBAL_ATTRIBUTE11";
            xCAILIT.GLOBAL_ATTRIBUTE12 = "GLOBAL_ATTRIBUTE12";
            xCAILIT.GLOBAL_ATTRIBUTE13 = "GLOBAL_ATTRIBUTE13";
            xCAILIT.GLOBAL_ATTRIBUTE14 = "GLOBAL_ATTRIBUTE14";
            xCAILIT.GLOBAL_ATTRIBUTE14 = "GLOBAL_ATTRIBUTE14";
            xCAILIT.GLOBAL_ATTRIBUTE15 = "GLOBAL_ATTRIBUTE15";
            xCAILIT.GLOBAL_ATTRIBUTE16 = "GLOBAL_ATTRIBUTE16";
            xCAILIT.GLOBAL_ATTRIBUTE17 = "GLOBAL_ATTRIBUTE17";
            xCAILIT.GLOBAL_ATTRIBUTE18 = "GLOBAL_ATTRIBUTE18";
            xCAILIT.GLOBAL_ATTRIBUTE19 = "GLOBAL_ATTRIBUTE19";
            xCAILIT.GLOBAL_ATTRIBUTE2 = "GLOBAL_ATTRIBUTE2";
            xCAILIT.GLOBAL_ATTRIBUTE20 = "GLOBAL_ATTRIBUTE20";
            xCAILIT.GLOBAL_ATTRIBUTE3 = "GLOBAL_ATTRIBUTE3";
            xCAILIT.GLOBAL_ATTRIBUTE4 = "GLOBAL_ATTRIBUTE4";
            xCAILIT.GLOBAL_ATTRIBUTE5 = "GLOBAL_ATTRIBUTE5";
            xCAILIT.GLOBAL_ATTRIBUTE6 = "GLOBAL_ATTRIBUTE6";
            xCAILIT.GLOBAL_ATTRIBUTE7 = "GLOBAL_ATTRIBUTE7";
            xCAILIT.GLOBAL_ATTRIBUTE8 = "GLOBAL_ATTRIBUTE8";
            xCAILIT.GLOBAL_ATTRIBUTE9 = "GLOBAL_ATTRIBUTE9";
            xCAILIT.INVOICE_AMOUNT = "INVOICE_AMOUNT";
            xCAILIT.INVOICE_ID = "INVOICE_ID";
            xCAILIT.INVOICE_TYPE_LOOKUP_CODE = "INVOICE_TYPE_LOOKUP_CODE";
            xCAILIT.LINE_NUMBER = "LINE_NUMBER";
            xCAILIT.PO_LINE_NUMBER = "PO_LINE_NUMBER";
            xCAILIT.PO_NUMBER = "PO_NUMBER";
            xCAILIT.PRICE = "PRICE";
            xCAILIT.QUANTITY = "QUANTITY";
            xCAILIT.RECEIPT_LINE_NUMBER = "RECEIPT_LINE_NUMBER";
            xCAILIT.RECEIPT_NUMBER = "RECEIPT_NUMBER";
            xCAILIT.TAX_CLASSIFICATION_CODE = "TAX_CLASSIFICATION_CODE";
            xCAILIT.TAX_RATE = "TAX_RATE";
            xCAILIT.TAX_REGIME_CODE = "TAX_REGIME_CODE";
            
            xCAILIT.table = "XCUST_AP_INV_LINES_INT_TBL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCAILIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String insert(XcustApInvLinesIntTbl p)
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
                p.INVOICE_ID = p.INVOICE_ID.Equals("") ? "0" : p.INVOICE_ID;
                p.LINE_NUMBER = p.LINE_NUMBER.Equals("") ? "0" : p.LINE_NUMBER;
                p.INVOICE_AMOUNT = p.INVOICE_AMOUNT.Equals("") ? "0" : p.INVOICE_AMOUNT;
                p.QUANTITY = p.QUANTITY.Equals("") ? "0" : p.QUANTITY;
                p.PRICE = p.PRICE.Equals("") ? "0" : p.PRICE;
                p.PO_LINE_NUMBER = p.PO_LINE_NUMBER.Equals("") ? "0" : p.PO_LINE_NUMBER;
                p.RECEIPT_LINE_NUMBER = p.RECEIPT_LINE_NUMBER.Equals("") ? "0" : p.RECEIPT_LINE_NUMBER;
                p.TAX_RATE = p.TAX_RATE.Equals("") ? "0" : p.TAX_RATE;
                //p.TAX_RATE = p.TAX_RATE.Equals("0") ? "" : p.TAX_RATE;

                p.PRICE = String.Concat(Double.Parse(p.PRICE));
                p.QUANTITY = String.Concat(Double.Parse(p.QUANTITY));
                p.INVOICE_AMOUNT = String.Concat(Double.Parse(p.INVOICE_AMOUNT));

                String last_update_by = "0", creation_by = "0";
                sql = "Insert Into " + xCAILIT.table + "(" + xCAILIT.ATTRIBUTE1 + "," + xCAILIT.ATTRIBUTE10 + "," + xCAILIT.ATTRIBUTE11 + "," +
                    xCAILIT.ATTRIBUTE12 + "," + xCAILIT.ATTRIBUTE13 + "," + xCAILIT.ATTRIBUTE14 + "," +
                    xCAILIT.ATTRIBUTE15 + "," + xCAILIT.ATTRIBUTE2 + "," + xCAILIT.ATTRIBUTE3 + "," +
                    xCAILIT.ATTRIBUTE4 + "," + xCAILIT.ATTRIBUTE5 + "," + xCAILIT.ATTRIBUTE6 + "," +
                    xCAILIT.ATTRIBUTE7 + "," + xCAILIT.ATTRIBUTE8 + "," + xCAILIT.ATTRIBUTE9 + "," +
                    xCAILIT.AWT_GROUP_NAME + "," + xCAILIT.DESCRIPTION + "," + xCAILIT.GLOBAL_ATTRIBUTE1 + "," +
                    xCAILIT.GLOBAL_ATTRIBUTE11 + "," + xCAILIT.GLOBAL_ATTRIBUTE12 + "," + xCAILIT.GLOBAL_ATTRIBUTE13 + "," +
                    xCAILIT.GLOBAL_ATTRIBUTE14 + "," + xCAILIT.GLOBAL_ATTRIBUTE15 + "," + xCAILIT.GLOBAL_ATTRIBUTE16 + "," +
                    xCAILIT.GLOBAL_ATTRIBUTE17 + "," + xCAILIT.GLOBAL_ATTRIBUTE18 + "," + xCAILIT.GLOBAL_ATTRIBUTE19 + "," +
                    xCAILIT.GLOBAL_ATTRIBUTE2 + "," + xCAILIT.GLOBAL_ATTRIBUTE20 + "," + xCAILIT.GLOBAL_ATTRIBUTE3 + "," +
                    xCAILIT.GLOBAL_ATTRIBUTE4 + "," + xCAILIT.GLOBAL_ATTRIBUTE5 + "," + xCAILIT.GLOBAL_ATTRIBUTE6 + "," +
                    xCAILIT.GLOBAL_ATTRIBUTE7 + "," + xCAILIT.GLOBAL_ATTRIBUTE8 + "," + xCAILIT.GLOBAL_ATTRIBUTE9 + "," +
                    xCAILIT.INVOICE_AMOUNT + "," + xCAILIT.INVOICE_ID + "," + xCAILIT.INVOICE_TYPE_LOOKUP_CODE + "," +
                    xCAILIT.LINE_NUMBER + "," + xCAILIT.PO_LINE_NUMBER + "," + xCAILIT.PO_NUMBER + "," +
                    xCAILIT.PRICE + "," + xCAILIT.QUANTITY + "," + xCAILIT.RECEIPT_LINE_NUMBER + "," +
                    xCAILIT.RECEIPT_NUMBER + "," + xCAILIT.TAX_CLASSIFICATION_CODE + "," + xCAILIT.TAX_RATE + "," +
                    xCAILIT.TAX_REGIME_CODE + " " +
                    ") " +
                    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE10 + "','" + p.ATTRIBUTE11 + "','" +
                    p.ATTRIBUTE12 + "','" + p.ATTRIBUTE13 + "','" + p.ATTRIBUTE14 + "','" +
                    p.ATTRIBUTE15 + "','" + p.ATTRIBUTE2 + "','" + p.ATTRIBUTE3 + "','" +
                    p.ATTRIBUTE4 + "','" + p.ATTRIBUTE5 + "','" + p.ATTRIBUTE6 + "','" +
                    p.ATTRIBUTE7 + "','" + p.ATTRIBUTE8 + "','" + p.ATTRIBUTE9 + "','" +
                    p.AWT_GROUP_NAME + "','" + p.DESCRIPTION + "','" + p.GLOBAL_ATTRIBUTE1 + "','" +
                    p.GLOBAL_ATTRIBUTE11 + "','" + p.GLOBAL_ATTRIBUTE12 + "','" + p.GLOBAL_ATTRIBUTE13 + "','" +
                    p.GLOBAL_ATTRIBUTE14 + "','" + p.GLOBAL_ATTRIBUTE15 + "','" + p.GLOBAL_ATTRIBUTE16 + "','" +
                    p.GLOBAL_ATTRIBUTE17 + "','" + p.GLOBAL_ATTRIBUTE18 + "','" + p.GLOBAL_ATTRIBUTE19 + "','" +
                    p.GLOBAL_ATTRIBUTE2 + "','" + p.GLOBAL_ATTRIBUTE20 + "','" + p.GLOBAL_ATTRIBUTE3 + "','" +
                    p.GLOBAL_ATTRIBUTE4 + "','" + p.GLOBAL_ATTRIBUTE5 + "','" + p.GLOBAL_ATTRIBUTE6 + "','" +
                    p.GLOBAL_ATTRIBUTE7 + "','" + p.GLOBAL_ATTRIBUTE8 + "','" + p.GLOBAL_ATTRIBUTE9 + "','" +
                    p.INVOICE_AMOUNT + "','" + p.INVOICE_ID + "','" + p.INVOICE_TYPE_LOOKUP_CODE + "','" +
                    p.LINE_NUMBER + "','" + p.PO_LINE_NUMBER + "','" + p.PO_NUMBER + "','" +
                    p.PRICE + "','" + p.QUANTITY + "','" + p.RECEIPT_LINE_NUMBER + "','" +
                    p.RECEIPT_NUMBER + "','" + p.TAX_CLASSIFICATION_CODE + "','" + p.TAX_RATE + "','" +
                    p.TAX_REGIME_CODE + "'" +
                    
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
