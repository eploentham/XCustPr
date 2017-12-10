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
            xCSIIT.request_id = "request_id";
            xCSIIT.row_number = "row_number";
            xCSIIT.row_cnt = "row_cnt";

            xCSIIT.table = "XCUST_SUP_INVOICE_INT_TBL";
        }
        public void DeleteTemp(String pathLog)
        {
            String sql = "Delete From " + xCSIIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
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
        public void insertBluk(List<String> supplier, String filename, String host, MaterialProgressBar pB1, String pathLog, String requestId, String storePlus)
        {
            int i = 0;
            pB1.Minimum = 0;
            pB1.Maximum = supplier.Count;
            foreach(String aa in supplier)
            {
                
                String[] bb = aa.Split(',');
                if (bb[1].Length <= 8) continue;

                i++;
                pB1.Value = i;

                String storeCode = "";
                storeCode = bb[2].Trim();
                if (storePlus.Length > 0)
                {
                    int plus = 0;
                    int.TryParse(storePlus,out plus);
                    if (plus > 0)
                    {
                        for (int j = 0; j < plus; j++)
                        {
                            storeCode = "0"+storeCode;
                        }
                    }
                }

                requestId = requestId.Equals("") ? "0" : requestId;
                XcustSupInvoiceIntTbl item = new XcustSupInvoiceIntTbl();
                item.INVOICE_NUM = bb[0].Trim();
                item.INVOICE_DATE = item.dateShowToDB(bb[1].Trim());
                item.STORE = storeCode;
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
                item.request_id = requestId;
                item.row_number = i.ToString();
                item.row_cnt = String.Concat(supplier.Count-2);


                insert(item, pathLog, requestId);
            }
            
        }
        public String insert(XcustSupInvoiceIntTbl p, String pathLog, String requestId)
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
                p.QTY = p.QTY.Equals("") ? "null" : p.QTY;
                p.PRICE = p.PRICE.Equals("") ? "null" : p.PRICE;
                p.TOTAL = p.TOTAL.Equals("") ? "null" : p.TOTAL;
                p.VAT_AMOUNT = p.VAT_AMOUNT.Equals("") ? "null" : p.VAT_AMOUNT;
                p.BASE_AMOUNT2 = p.BASE_AMOUNT2.Equals("") ? "null" : p.BASE_AMOUNT2;
                p.BASE_AMOUNT = p.BASE_AMOUNT.Equals("") ? "null" : p.BASE_AMOUNT;
                p.TOTAL2 = p.TOTAL2.Equals("") ? "null" : p.TOTAL2;
                p.request_id = p.request_id.Equals("") ? "null" : p.request_id;
                String last_update_by = "0", creation_by = "0";
                sql = "Insert Into " + xCSIIT.table + "(" + xCSIIT.BASE_AMOUNT + "," + xCSIIT.BASE_AMOUNT2 + "," + xCSIIT.ERROR_MSG + "," +
                    xCSIIT.FILE_NAME + "," + xCSIIT.INVOICE_DATE + "," + xCSIIT.INVOICE_NUM + "," +
                    xCSIIT.PO_NUMBER + "," + xCSIIT.PRICE + "," + xCSIIT.PROCESS_FLAG + "," +
                    xCSIIT.QTY + "," + xCSIIT.STORE + "," + xCSIIT.TOTAL + "," +
                    xCSIIT.TOTAL2 + "," + xCSIIT.VALIDATE_FLAG + "," + xCSIIT.VAT_AMOUNT + "," +
                    xCSIIT.VAT_AMOUNT2 + ", " + xCSIIT.request_id+", "+ xCSIIT.row_cnt + ", "+xCSIIT.row_number + " "+
                    ") " +
                    "Values(" + p.BASE_AMOUNT + "," + p.BASE_AMOUNT2 + ",'" + p.ERROR_MSG + "','" +
                    p.FILE_NAME + "','" + p.INVOICE_DATE + "','" + p.INVOICE_NUM + "','" +
                    p.PO_NUMBER + "'," + p.PRICE + ",'" + p.PROCESS_FLAG + "'," +
                    p.QTY + ",'" + p.STORE + "'," + p.TOTAL + "," +
                    p.TOTAL2 + ",'" + p.VALIDATE_FLAG + "'," + p.VAT_AMOUNT + "," +
                    p.VAT_AMOUNT2 + "," +p.request_id+",'"+ p.row_cnt + "','"+p.row_number + "' "+
                    ") ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
                //chk = p.RowNumber;
                //chk = p.Code;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }

            return chk;
        }
        public String updateErrorMessage(String invoice_num, String store_code, String msg, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCSIIT.table + " Set " + xCSIIT.ERROR_MSG + "=" + xCSIIT.ERROR_MSG + "+'," + msg.Replace("'", "''") + "' " +
                ", " + xCSIIT.VALIDATE_FLAG + "='E' " +//VALIDATE_FLAG
                "Where " + xCSIIT.INVOICE_NUM + " = '" + invoice_num + "' and " + xCSIIT.STORE + "='" + store_code + "' and " + xCSIIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
        public String getRequestID()
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "select next value for xcust_request_id ";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString();
            }
            return chk;
        }
        public DataTable selectFilenameByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select  " + xCSIIT.FILE_NAME + ", row_cnt " +
                " From " + xCSIIT.table +
                " Where " + xCSIIT.request_id + "='" + requestId + "' " +
                " Group By " + xCSIIT.FILE_NAME + ", row_cnt " +
                " Order By " + xCSIIT.FILE_NAME;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select  * " +
                " From " + xCSIIT.table +
                " Where " + xCSIIT.request_id + "='" + requestId + "' " +
                
                " Order By " + xCSIIT.FILE_NAME;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String getCountNoErrorByFilename(String requestId, String filename)
        {
            DataTable dt = new DataTable();
            String sql = "", chk = "";
            sql = "Select  count(1) as cnt " +
                " From " + xCSIIT.table +
                " Where " + xCSIIT.request_id + "='" + requestId + "' and " + xCSIIT.FILE_NAME + "='" + filename + "' " +
                " and len(" + xCSIIT.ERROR_MSG + ") <= 0";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["cnt"].ToString();
            }
            return chk;
        }
        public DataTable selectLinfoxGroupByPoNumber(String filename, String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "";

            sql = "Select " + xCSIIT.PO_NUMBER + "," + xCSIIT.FILE_NAME +","+xCSIIT.INVOICE_NUM+","+xCSIIT.INVOICE_DATE+","+xCSIIT.STORE+
                " From " + xCSIIT.table +
                " Where " + xCSIIT.VALIDATE_FLAG + "='Y' and " + xCSIIT.request_id + "='" + requestId + "' " +
                " and " + xCSIIT.FILE_NAME + "='" + filename + "' " +
                " Group By " + xCSIIT.PO_NUMBER + "," + xCSIIT.FILE_NAME+"," + xCSIIT.INVOICE_NUM + "," + xCSIIT.INVOICE_DATE + "," + xCSIIT.STORE +
               " Order By " + xCSIIT.FILE_NAME + "," + xCSIIT.PO_NUMBER +
                " ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectApLineByInvNumber(String requestId, String invNumber, String storeCode)
        {
            DataTable dt = new DataTable();
            String sql = "";
            sql = "Select * " +
                " From " + xCSIIT.table +
                " Where " + xCSIIT.INVOICE_NUM + "='" + invNumber + "' and " + xCSIIT.request_id + "='" + requestId + "' and " +
                xCSIIT.STORE + "='" + storeCode + "' "+
                " Order By " + xCSIIT.INVOICE_NUM + "," + xCSIIT.INVOICE_DATE;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void updatePrcessFlag(String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCSIIT.table + " Set " + xCSIIT.PROCESS_FLAG + "='Y' " +
                " Where " + xCSIIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql, host, pathLog);
        }
        public String updateValidateFlagY(String inv_number, String store_code, String requestId, String host, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCSIIT.table + " Set " + xCSIIT.VALIDATE_FLAG + "='Y' " +
                "Where " + xCSIIT.INVOICE_NUM + " = '" + inv_number + "' and " + xCSIIT.STORE + "='" + store_code + "' and " + xCSIIT.request_id + "='" + requestId + "'";
            chk = conn.ExecuteNonQuery(sql.ToString(), host, pathLog);

            return chk;
        }
    }
}
