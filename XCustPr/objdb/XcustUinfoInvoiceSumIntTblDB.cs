using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustUinfoInvoiceSumIntTblDB
    {
        public XcustUinfoInvoiceSumIntTbl xCUiISIT;
        ConnectDB conn;
        private InitC initC;
        public XcustUinfoInvoiceSumIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCUiISIT = new XcustUinfoInvoiceSumIntTbl();
            xCUiISIT.BASE_AMOUNT = "BASE_AMOUNT";
            xCUiISIT.ERROR_MSG = "ERROR_MSG";
            xCUiISIT.FLIE_NAME = "FLIE_NAME";
            xCUiISIT.GL_DATE = "GL_DATE";
            xCUiISIT.INVOICE_DATE = "INVOICE_DATE";
            xCUiISIT.INVOICE_NUM = "INVOICE_NUM";
            xCUiISIT.PROCESS_FLAG = "PROCESS_FLAG";
            xCUiISIT.SUPPLIER_NUM = "SUPPLIER_NUM";
            xCUiISIT.TAX_AMOUNT = "TAX_AMOUNT";
            xCUiISIT.VALIDATE_FLAG = "VALIDATE_FLAG";

            xCUiISIT.table = "XCUST_UINFO_INVOICE_SUM_INT_TBL";
        }
        public void DeleteTemp()
        {
            String sql = "Delete From " + xCUiISIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public void insertBluk(List<String> uinfo, DataTable dtFixLen, String filename, String host, MaterialProgressBar pB1)
        {
            foreach (String aa in uinfo)
            {
                if (aa.Length <= 90) continue;
                XcustUinfoInvoiceSumIntTbl item = new XcustUinfoInvoiceSumIntTbl();
                item.BASE_AMOUNT = aa.Substring(0, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.ERROR_MSG = aa.Substring(int.Parse(dtFixLen.Rows[1]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.FLIE_NAME = aa.Substring(int.Parse(dtFixLen.Rows[2]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.GL_DATE = aa.Substring(int.Parse(dtFixLen.Rows[31]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.INVOICE_DATE = aa.Substring(int.Parse(dtFixLen.Rows[4]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.INVOICE_NUM = aa.Substring(int.Parse(dtFixLen.Rows[5]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString())); ;
                item.PROCESS_FLAG = aa.Substring(int.Parse(dtFixLen.Rows[6]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.SUPPLIER_NUM = aa.Substring(int.Parse(dtFixLen.Rows[7]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.TAX_AMOUNT = aa.Substring(int.Parse(dtFixLen.Rows[8]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                item.VALIDATE_FLAG = aa.Substring(int.Parse(dtFixLen.Rows[9]["X_START_POSITION"].ToString()) - 1, int.Parse(dtFixLen.Rows[0]["X_LENGTH"].ToString()));
                insert(item);
            }
        }
        public String insert(XcustUinfoInvoiceSumIntTbl p)
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
                sql = "Insert Into " + xCUiISIT.table + "(" + xCUiISIT.BASE_AMOUNT + "," + xCUiISIT.ERROR_MSG + "," + xCUiISIT.FLIE_NAME + "," +
                    xCUiISIT.GL_DATE + "," + xCUiISIT.INVOICE_DATE + "," + xCUiISIT.INVOICE_NUM + "," +
                    xCUiISIT.PROCESS_FLAG + "," + xCUiISIT.SUPPLIER_NUM + "," + xCUiISIT.TAX_AMOUNT + "," +
                    xCUiISIT.VALIDATE_FLAG + " " +
                                        
                    ") " +
                    "Values('" + p.BASE_AMOUNT + "','" + p.ERROR_MSG + "','" + p.FLIE_NAME + "','" +
                    p.GL_DATE + "','" + p.INVOICE_DATE + "','" + p.INVOICE_NUM + "'," +
                    p.PROCESS_FLAG + "','" + p.SUPPLIER_NUM + "','" + p.TAX_AMOUNT + "','" +
                    p.VALIDATE_FLAG + "' " + 
                    
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
