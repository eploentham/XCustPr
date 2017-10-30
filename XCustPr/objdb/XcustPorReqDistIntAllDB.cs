using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPorReqDistIntAllDB
    {
        XcustPorReqDistIntAll xCPRDIA;
        ConnectDB conn;
        public XcustPorReqDistIntAllDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPRDIA = new XcustPorReqDistIntAll();
            xCPRDIA.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPRDIA.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCPRDIA.ATTRIBUTE_DATE1 = "ATTRIBUTE_DATE1";
            xCPRDIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCPRDIA.ATTRIBUTE_TIMESTAMP1 = "ATTRIBUTE_TIMESTAMP1";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 = "CHARGE_ACCOUNT_SEGMENT1";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 = "CHARGE_ACCOUNT_SEGMENT2";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 = "CHARGE_ACCOUNT_SEGMENT3";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 = "CHARGE_ACCOUNT_SEGMENT4";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 = "CHARGE_ACCOUNT_SEGMENT5";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 = "CHARGE_ACCOUNT_SEGMENT6";
            xCPRDIA.PO_LINE_NUMBER = "PO_LINE_NUMBER";
            xCPRDIA.PO_NUMBER = "PO_NUMBER";
            xCPRDIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCPRDIA.Program_running = "Program_running";
            xCPRDIA.QTY = "QTY";

            xCPRDIA.pkField = "";
            xCPRDIA.table = "xcust_por_req_dist_int_all";
        }
        public String insert(XcustPorReqDistIntAll p)
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
                sql = "Insert Into " + xCPRDIA.table + "(" + xCPRDIA.ATTRIBUTE1 + "," + xCPRDIA.ATTRIBUTE_DATE1 + "," + xCPRDIA.ATTRIBUTE_CATEGORY + ","+
                    xCPRDIA.ATTRIBUTE_NUMBER1 + "," + xCPRDIA.ATTRIBUTE_TIMESTAMP1 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 + "," +
                    xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 + "," +
                    xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 + "," + xCPRDIA.PO_LINE_NUMBER + "," +
                    xCPRDIA.PO_NUMBER + "," + xCPRDIA.PROCESS_FLAG + "," + xCPRDIA.Program_running + "," +                    
                    xCPRDIA.QTY  + ") " +
                    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_DATE1 + "','" + p.ATTRIBUTE_CATEGORY + "','" +
                    p.ATTRIBUTE_NUMBER1 + "',now(),'" + p.CHARGE_ACCOUNT_SEGMENT1 + "','" + 
                    p.CHARGE_ACCOUNT_SEGMENT2 + "','" + p.CHARGE_ACCOUNT_SEGMENT3 + "','" + p.CHARGE_ACCOUNT_SEGMENT4 + "','" +
                    p.CHARGE_ACCOUNT_SEGMENT5 + "','" + p.CHARGE_ACCOUNT_SEGMENT6 + "','" + p.PO_LINE_NUMBER + "','" +
                    p.PO_NUMBER + "','" + p.PROCESS_FLAG + "','" + p.Program_running + "','" +                    
                    p.QTY + "') ";
                chk = conn.ExecuteNonQueryAutoIncrement(sql, "kfc_po");
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
