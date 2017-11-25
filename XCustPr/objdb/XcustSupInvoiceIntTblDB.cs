using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustSupInvoiceIntTblDB
    {
        public XcustSupInvoiceIntTbl xCSIIT;
        ConnectDB conn;
        private InitC initC;

        public XcustSupInvoiceIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCSIIT = new XcustSupInvoiceIntTbl();
            xCSIIT.BASE_AMOUNT = "BASE_AMOUNT";
            xCSIIT.BASE_AMOUNT2 = "BASE_AMOUNT2";
            xCSIIT.ERROR_MSG = "ERROR_MSG";
            xCSIIT.FILE_NAME = "FILE_NAME";
            xCSIIT.INVOICE_DATE = "INVOICE_DATE";
            xCSIIT.INVOICE_NUM = "INVOICE_NUM";
            xCSIIT.PO_NUMBER = "PO_NUMBER";
            xCSIIT.PRICE = "PRICE";
            xCSIIT.PROCESS_FLAG = "PROCESS_FLAG";
            xCSIIT.QTY = "QTY";
            xCSIIT.STORE = "STORE";
            xCSIIT.TOTAL = "TOTAL";
            xCSIIT.TOTAL2 = "TOTAL2";
            xCSIIT.VALIDATE_FLAG = "VALIDATE_FLAG";
            xCSIIT.VAT_AMOUNT = "VAT_AMOUNT";
            xCSIIT.VAT_AMOUNT2 = "VAT_AMOUNT2";

            xCSIIT.table = "XCUST_SUP_INVOICE_INT_TBL";
        }
        public String insert(XcustSupInvoiceIntTbl p)
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
                sql = "Insert Into " + xCSIIT.table + "(" + xCSIIT.BASE_AMOUNT + "," + xCSIIT.BASE_AMOUNT2 + "," + xCSIIT.ERROR_MSG + "," +
                    xCSIIT.FILE_NAME + "," + xCSIIT.INVOICE_DATE + "," + xCSIIT.INVOICE_NUM + "," +
                    xCSIIT.PO_NUMBER + "," + xCSIIT.PRICE + "," + xCSIIT.PROCESS_FLAG + "," +
                    xCSIIT.QTY + "," + xCSIIT.STORE + "," + xCSIIT.TOTAL + "," +
                    xCSIIT.TOTAL2 + "," + xCSIIT.VALIDATE_FLAG + "," + xCSIIT.VAT_AMOUNT + "," +
                    xCSIIT.VAT_AMOUNT2 + " " +
                    ") " +
                    "Values('" + p.BASE_AMOUNT + "','" + p.BASE_AMOUNT2 + "','" + p.ERROR_MSG + "','" +
                    p.FILE_NAME + "','" + p.INVOICE_DATE + "','" + p.INVOICE_NUM + "'," +
                    p.PO_NUMBER + "','" + p.PRICE + "','" + p.PROCESS_FLAG + "','" +
                    p.QTY + "','" + p.STORE + "','" + p.TOTAL + "','" +
                    p.TOTAL2 + "','" + p.VALIDATE_FLAG + "','" + p.VAT_AMOUNT + "','" +
                    p.VAT_AMOUNT2 + "'" +
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
