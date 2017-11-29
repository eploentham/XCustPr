using System;
using System.Collections.Generic;
using System.Data;
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
        public void DeleteTemp()
        {
            String sql = "Delete From " + xCSIIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCSIIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCSIIT.FILE_NAME + " From " + xCSIIT.table + " Group By " + xCSIIT.FILE_NAME;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCSIIT.table + " Where " + xCSIIT.FILE_NAME + "='" + filename + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void insertBluk(List<String> supplier, String filename, String host, MaterialProgressBar pB1)
        {
            int i = 0;
            pB1.Minimum = 0;
            pB1.Maximum = supplier.Count;
            foreach(String aa in supplier)
            {
                i++;
                pB1.Value = i;
                String[] bb = aa.Split(',');
                if (bb[1].Length <= 8) continue;
                XcustSupInvoiceIntTbl item = new XcustSupInvoiceIntTbl();
                item.INVOICE_NUM = bb[0].Trim();
                item.INVOICE_DATE = item.dateShowToDB(bb[1].Trim());
                item.STORE = bb[2].Trim();
                item.BASE_AMOUNT = bb[3].Trim().Replace(@"""", "").Trim();        //Before Vat Amount
                item.VAT_AMOUNT = bb[4].Trim().Replace(@"""", "").Trim();        // Vat Amount
                item.TOTAL = bb[5].Trim().Replace(@"""", "").Trim();
                item.PRICE = bb[6].Trim().Replace(@"""", "").Trim();
                item.QTY = bb[7].Trim().Replace(@"""", "").Trim();
                item.BASE_AMOUNT2 = bb[8].Trim();       //Base Amount
                item.VAT_AMOUNT2 = bb[9].Trim();        //Vat Amount
                item.TOTAL2 = bb[10].Trim().Replace(@"""","").Trim();        //
                item.PO_NUMBER = "";
                item.FILE_NAME = filename.Replace(initC.AP001PathProcess,"");
                item.VALIDATE_FLAG = "";
                item.PROCESS_FLAG = "";
                item.ERROR_MSG = "";
                insert(item);
            }
            
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
                    "Values(" + p.BASE_AMOUNT + "," + p.BASE_AMOUNT2 + ",'" + p.ERROR_MSG + "','" +
                    p.FILE_NAME + "','" + p.INVOICE_DATE + "','" + p.INVOICE_NUM + "','" +
                    p.PO_NUMBER + "'," + p.PRICE + ",'" + p.PROCESS_FLAG + "'," +
                    p.QTY + ",'" + p.STORE + "'," + p.TOTAL + "," +
                    p.TOTAL2 + ",'" + p.VALIDATE_FLAG + "'," + p.VAT_AMOUNT + "," +
                    p.VAT_AMOUNT2 + "" +
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
