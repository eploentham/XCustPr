using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustUinfoInvoiceDetailTblDB
    {
        public XcustUinfoInvoiceDetailTbl xCUiIDT;
        ConnectDB conn;
        private InitC initC;

        public XcustPrTblDB xCPrTDB;

        public XcustUinfoInvoiceDetailTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCPrTDB = new XcustPrTblDB(conn, initC);

            xCUiIDT = new XcustUinfoInvoiceDetailTbl();
            xCUiIDT.bu = "bu";
            xCUiIDT.company = "company";
            xCUiIDT.CREATE_BY = "CREATE_BY";
            xCUiIDT.CREATION_DATE = "CREATION_DATE";
            xCUiIDT.currency_code = "currency_code";
            xCUiIDT.currency_mode = "currency_mode";
            xCUiIDT.description1 = "description1";
            xCUiIDT.description2 = "description2";
            xCUiIDT.ERROR_MSG = "ERROR_MSG";
            xCUiIDT.FLIE_NAME = "FLIE_NAME";
            xCUiIDT.IMPORT_SOURCE = "IMPORT_SOURCE";
            xCUiIDT.invoice_date = "invoice_date";

            xCUiIDT.invoice_line_number = "invoice_line_number";
            xCUiIDT.invoice_number = "invoice_number";
            xCUiIDT.item_code = "item_code";
            xCUiIDT.lot_number = "lot_number";
            xCUiIDT.payment_term = "payment_term";
            xCUiIDT.po_account_code = "po_account_code";
            xCUiIDT.PO_ACT_TAX_AMT = "PO_ACT_TAX_AMT";
            xCUiIDT.po_code = "po_code";
            xCUiIDT.po_document_type = "po_document_type";
            xCUiIDT.po_line_no = "po_line_no";
            xCUiIDT.po_line_type = "po_line_type";
            xCUiIDT.po_receipt_amt = "po_receipt_amt";

            xCUiIDT.po_tax_code = "po_tax_code";
            xCUiIDT.po_tax_type = "po_tax_type";
            xCUiIDT.PROCESS_FLAG = "PROCESS_FLAG";
            xCUiIDT.qty = "qty";
            xCUiIDT.receipt_line_number = "receipt_line_number";
            xCUiIDT.receipt_number = "receipt_number";
            xCUiIDT.receipt_type = "receipt_type";
            xCUiIDT.reference1 = "reference1";
            xCUiIDT.reference2 = "reference2";
            xCUiIDT.remark = "remark";
            xCUiIDT.sub_ledger = "sub_ledger";
            xCUiIDT.Sub_ledger_type = "Sub_ledger_type";

            xCUiIDT.supplier_code = "supplier_code";
            xCUiIDT.SUPPLIER_TYPE = "SUPPLIER_TYPE";
            xCUiIDT.tax_cal = "tax_cal";
            xCUiIDT.transaction_orginator = "transaction_orginator";
            xCUiIDT.trans_date = "trans_date";
            xCUiIDT.unit_price = "unit_price";
            xCUiIDT.uom_code = "uom_code";
            xCUiIDT.VALIDATE_FLAG = "VALIDATE_FLAG";            
            xCUiIDT.WHT_AMOUNT = "WHT_AMOUNT";

            xCUiIDT.table = "XCUST_UINFO_INVOICE_DETAIL_TBL";
        }
        public void DeleteTemp()
        {
            String sql = "Delete From " + xCUiIDT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public void insertBluk(List<String> uinfo, String filename, String host, MaterialProgressBar pB1)
        {
            DataTable dtFixLen = xCPrTDB.selectPO007FixLenLine();
            
            foreach(String aa in uinfo)
            {
                if (aa.Length <= 400) continue;
                xCUiIDT.trans_date = aa.Substring(0, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                xCUiIDT.company = aa.Substring(int.Parse(dtFixLen.Rows[1]["X_START_POSITION"].ToString())-1, int.Parse(dtFixLen.Rows[1]["X_LENGTH"].ToString()));
                xCUiIDT.po_document_type = aa.Substring(int.Parse(dtFixLen.Rows[2]["X_START_POSITION"].ToString())-1, int.Parse(dtFixLen.Rows[2]["X_LENGTH"].ToString()));
                xCUiIDT.po_code = aa.Substring(int.Parse(dtFixLen.Rows[3]["X_START_POSITION"].ToString())-1, int.Parse(dtFixLen.Rows[3]["X_LENGTH"].ToString()));
                xCUiIDT.supplier_code = aa.Substring(int.Parse(dtFixLen.Rows[4]["X_START_POSITION"].ToString())-1, int.Parse(dtFixLen.Rows[4]["X_LENGTH"].ToString()));
                xCUiIDT.po_line_no = aa.Substring(int.Parse(dtFixLen.Rows[5]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[5]["X_LENGTH"].ToString()));
                xCUiIDT.item_code = aa.Substring(int.Parse(dtFixLen.Rows[6]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[6]["X_LENGTH"].ToString()));
                xCUiIDT.description1 = aa.Substring(int.Parse(dtFixLen.Rows[7]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[7]["X_LENGTH"].ToString()));
                xCUiIDT.description2 = aa.Substring(int.Parse(dtFixLen.Rows[8]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[8]["X_LENGTH"].ToString()));
                xCUiIDT.po_receipt_amt = aa.Substring(int.Parse(dtFixLen.Rows[9]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[9]["X_LENGTH"].ToString()));
                xCUiIDT.po_tax_code = aa.Substring(int.Parse(dtFixLen.Rows[10]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[10]["X_LENGTH"].ToString()));

                xCUiIDT.po_tax_type = aa.Substring(int.Parse(dtFixLen.Rows[11]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[11]["X_LENGTH"].ToString()));
                xCUiIDT.po_account_code = aa.Substring(int.Parse(dtFixLen.Rows[12]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[12]["X_LENGTH"].ToString()));
                xCUiIDT.qty = aa.Substring(int.Parse(dtFixLen.Rows[13]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[13]["X_LENGTH"].ToString()));
                xCUiIDT.uom_code = aa.Substring(int.Parse(dtFixLen.Rows[14]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[14]["X_LENGTH"].ToString()));
                xCUiIDT.unit_price = aa.Substring(int.Parse(dtFixLen.Rows[15]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[15]["X_LENGTH"].ToString()));
                xCUiIDT.sub_ledger = aa.Substring(int.Parse(dtFixLen.Rows[16]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[16]["X_LENGTH"].ToString()));
                xCUiIDT.Sub_ledger_type = aa.Substring(int.Parse(dtFixLen.Rows[17]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[17]["X_LENGTH"].ToString()));
                xCUiIDT.reference1 = aa.Substring(int.Parse(dtFixLen.Rows[18]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[18]["X_LENGTH"].ToString()));
                xCUiIDT.reference2 = aa.Substring(int.Parse(dtFixLen.Rows[19]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[19]["X_LENGTH"].ToString()));
                xCUiIDT.remark = aa.Substring(int.Parse(dtFixLen.Rows[20]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[20]["X_LENGTH"].ToString()));

                xCUiIDT.po_line_type = aa.Substring(int.Parse(dtFixLen.Rows[21]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[21]["X_LENGTH"].ToString()));
                xCUiIDT.payment_term = aa.Substring(int.Parse(dtFixLen.Rows[22]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[22]["X_LENGTH"].ToString()));
                xCUiIDT.bu = aa.Substring(int.Parse(dtFixLen.Rows[23]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[23]["X_LENGTH"].ToString()));
                xCUiIDT.currency_mode = aa.Substring(int.Parse(dtFixLen.Rows[24]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[24]["X_LENGTH"].ToString()));
                xCUiIDT.currency_code = aa.Substring(int.Parse(dtFixLen.Rows[25]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[25]["X_LENGTH"].ToString()));
                xCUiIDT.receipt_number = aa.Substring(int.Parse(dtFixLen.Rows[26]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[26]["X_LENGTH"].ToString()));
                xCUiIDT.receipt_type = aa.Substring(int.Parse(dtFixLen.Rows[27]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[27]["X_LENGTH"].ToString()));
                //xCUiIDT.po_line_no = aa.Substring(int.Parse(dtFixLen.Rows[2]["X_LENGTH"].ToString()), int.Parse(dtFixLen.Rows[3]["X_LENGTH"].ToString()));
                //xCUiIDT.supplier_code = aa.Substring(int.Parse(dtFixLen.Rows[2]["X_LENGTH"].ToString()), int.Parse(dtFixLen.Rows[3]["X_LENGTH"].ToString()));
                //xCUiIDT.supplier_code = aa.Substring(int.Parse(dtFixLen.Rows[2]["X_LENGTH"].ToString()), int.Parse(dtFixLen.Rows[3]["X_LENGTH"].ToString()));
            }
        }
        private String insert(XcustUinfoInvoiceDetailTbl p)
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
                sql = "Insert Into " + xCUiIDT.table + "(" + xCUiIDT.bu + "," + xCUiIDT.company + "," + xCUiIDT.CREATE_BY + "," +
                    xCUiIDT.CREATION_DATE + "," + xCUiIDT.currency_code + "," + xCUiIDT.currency_mode + "," +
                    xCUiIDT.description1 + "," + xCUiIDT.description2 + "," + xCUiIDT.ERROR_MSG + "," +
                    xCUiIDT.FLIE_NAME + "," + xCUiIDT.IMPORT_SOURCE + "," + xCUiIDT.invoice_date + "," +

                    xCUiIDT.invoice_line_number + "," + xCUiIDT.invoice_number + "," + xCUiIDT.item_code + "," +
                    xCUiIDT.lot_number + "," + xCUiIDT.payment_term + "," + xCUiIDT.po_account_code + "," +
                    xCUiIDT.PO_ACT_TAX_AMT + "," + xCUiIDT.po_code + "," + xCUiIDT.po_document_type + "," +
                    xCUiIDT.po_line_no + "," + xCUiIDT.po_line_type + "," + xCUiIDT.po_receipt_amt + "," +

                    xCUiIDT.po_tax_code + "," + xCUiIDT.po_tax_type + "," + xCUiIDT.PROCESS_FLAG + "," +
                    xCUiIDT.qty + "," + xCUiIDT.receipt_line_number + "," + xCUiIDT.receipt_number + "," +
                    xCUiIDT.receipt_type + "," + xCUiIDT.reference1 + "," + xCUiIDT.reference2 + "," +
                    xCUiIDT.remark + "," + xCUiIDT.sub_ledger + "," + xCUiIDT.Sub_ledger_type + "," +

                    xCUiIDT.supplier_code + "," + xCUiIDT.SUPPLIER_TYPE + "," + xCUiIDT.tax_cal + "," +
                    xCUiIDT.transaction_orginator + "," + xCUiIDT.trans_date + "," + xCUiIDT.unit_price + "," +
                    xCUiIDT.uom_code + "," + xCUiIDT.VALIDATE_FLAG + "," + xCUiIDT.WHT_AMOUNT + " " +
                    
                    ") " +
                    "Values('" + p.bu + "','" + p.company + "','" + p.CREATE_BY + "','" +
                    p.CREATION_DATE + "','" + p.currency_code + "','" + p.currency_mode + "'," +
                    p.description1 + "','" + p.description2 + "','" + p.ERROR_MSG + "','" +
                    p.FLIE_NAME + "','" + p.IMPORT_SOURCE + "','" + p.invoice_date + "','" +

                    p.invoice_line_number + "','" + p.invoice_number + "','" + p.item_code + "','" +
                    p.lot_number + "','" + p.payment_term + "','" + p.po_account_code + "','" +
                    p.PO_ACT_TAX_AMT + "','" + p.po_code + "','" + p.po_document_type + "','" +
                    p.po_line_no + "','" + p.po_line_type + "','" + p.po_receipt_amt + "','" +

                    p.po_tax_code + "','" + p.po_tax_type + "','" + p.PROCESS_FLAG + "','" +
                    p.qty + "','" + p.receipt_line_number + "','" + p.receipt_number + "','" +
                    p.receipt_type + "','" + p.reference1 + "','" + p.reference2 + "','" +
                    p.remark + "','" + p.sub_ledger + "','" + p.Sub_ledger_type + "','" +

                    p.supplier_code + "','" + p.SUPPLIER_TYPE + "','" + p.tax_cal + "','" +
                    p.transaction_orginator + "','" + p.trans_date + "','" + p.unit_price + "','" +
                    p.uom_code + "','" + p.VALIDATE_FLAG + "','" + p.WHT_AMOUNT + "' " +
                    
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
