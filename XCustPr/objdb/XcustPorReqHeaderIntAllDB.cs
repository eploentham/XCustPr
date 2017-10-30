using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPorReqHeaderIntAllDB
    {
        ConnectDB conn;
        public XcustPorReqHeaderIntAll xCPRHIA;
        public XcustPorReqHeaderIntAllDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPRHIA = new XcustPorReqHeaderIntAll();
            xCPRHIA.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPRHIA.ATTRIBUTE_DATE1 = "ATTRIBUTE_DATE1";
            xCPRHIA.ATTRIBUTE_TIMESTAMP1 = "ATTRIBUTE_TIMESTAMP1";
            xCPRHIA.Batch_ID = "Batch_ID";
            xCPRHIA.Description = "Description";
            xCPRHIA.ENTER_BY = "ENTER_BY";
            xCPRHIA.import_source = "import_source";
            xCPRHIA.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCPRHIA.PO_NUMBER = "PO_NUMBER";
            xCPRHIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCPRHIA.PR_APPROVER = "PR_APPROVER";
            xCPRHIA.PR_STATAUS = "PR_STATAUS";
            xCPRHIA.Requisitioning_BU = "Requisitioning_BU";
            xCPRHIA.Requisition_Number = "Requisition_Number";

            xCPRHIA.pkField = "";
            xCPRHIA.table = "xcust_por_req_header_int_all";
        }
        public String insert(XcustPorReqHeaderIntAll p)
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
                sql = "Insert Into " + xCPRHIA.table + "(" + xCPRHIA.ATTRIBUTE1 + "," + xCPRHIA.ATTRIBUTE_DATE1 + "," +
                    xCPRHIA.ATTRIBUTE_TIMESTAMP1 + "," + xCPRHIA.Batch_ID + "," +
                    xCPRHIA.Description + "," + xCPRHIA.ENTER_BY + "," + xCPRHIA.import_source + "," +
                    xCPRHIA.ATTRIBUTE_CATEGORY + "," + xCPRHIA.PO_NUMBER + "," + xCPRHIA.PROCESS_FLAG + "," +
                    xCPRHIA.PR_APPROVER + "," + xCPRHIA.PR_STATAUS + "," + xCPRHIA.Requisitioning_BU + "," +
                    xCPRHIA.Requisition_Number + ") " +
                    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_DATE1 + "','" +
                    p.ATTRIBUTE_TIMESTAMP1 + "','" + p.Batch_ID + "','" +
                    p.Description + "','" + p.ENTER_BY + "','" + p.import_source + "','" +
                    p.ATTRIBUTE_CATEGORY + "','" + p.PO_NUMBER + "','" + p.PROCESS_FLAG + "','" +
                    p.PR_APPROVER + "','" + p.PR_STATAUS + "','" + p.Requisitioning_BU + "','" +
                    p.Requisition_Number + "') ";
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
