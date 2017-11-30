using System;
using System.Collections.Generic;
using System.Data;
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
            xCPRHIA.BATCH_ID = "BATCH_ID";/*Batch ID*/
            xCPRHIA.DESCRIPTIONS = "DESCRIPTIONS";
            xCPRHIA.REQUESTER_EMAIL_ADDR = "REQUESTER_EMAIL_ADDR";      //ENTER_BY
            xCPRHIA.INTERFACE_SOURCE_CODE = "INTERFACE_SOURCE_CODE";/*import_source*/
            xCPRHIA.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";//ATTRIBUTE_CATEGORY
            xCPRHIA.REQ_HEADER_INTERFACE_ID = "REQ_HEADER_INTERFACE_ID";/*PO_NUMBER*/
            xCPRHIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCPRHIA.APPROVER_EMAIL_ADDR = "APPROVER_EMAIL_ADDR";//APPROVER_EMAIL_ADDR PR_APPROVER
            xCPRHIA.STATUS_CODE = "STATUS_CODE";//STATUS_CODE  //PR_STATAUS
            xCPRHIA.REQ_BU_NAME = "REQ_BU_NAME";/*Requisitioning_BU*/
            xCPRHIA.REQUITITION_NUMBER = "REQUITITION_NUMBER";
            
            xCPRHIA.ATTRIBUTE2 = "ATTRIBUTE2";
            xCPRHIA.ATTRIBUTE3 = "ATTRIBUTE3";
            xCPRHIA.ATTRIBUTE4 = "ATTRIBUTE4";
            xCPRHIA.ATTRIBUTE5 = "ATTRIBUTE5";
            xCPRHIA.ATTRIBUTE6 = "ATTRIBUTE6";
            xCPRHIA.ATTRIBUTE7 = "ATTRIBUTE7";
            xCPRHIA.ATTRIBUTE8 = "ATTRIBUTE8";
            xCPRHIA.ATTRIBUTE9 = "ATTRIBUTE9";
            xCPRHIA.ATTRIBUTE10 = "ATTRIBUTE10";
            xCPRHIA.ATTRIBUTE11 = "ATTRIBUTE11";
            xCPRHIA.ATTRIBUTE12 = "ATTRIBUTE12";
            xCPRHIA.ATTRIBUTE13 = "ATTRIBUTE13";
            xCPRHIA.ATTRIBUTE14 = "ATTRIBUTE14";
            xCPRHIA.ATTRIBUTE15 = "ATTRIBUTE15";
            xCPRHIA.ATTRIBUTE16 = "ATTRIBUTE16";
            xCPRHIA.ATTRIBUTE17 = "ATTRIBUTE17";
            xCPRHIA.ATTRIBUTE18 = "ATTRIBUTE18";
            xCPRHIA.ATTRIBUTE19 = "ATTRIBUTE19";
            xCPRHIA.ATTRIBUTE20 = "ATTRIBUTE20";

            xCPRHIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCPRHIA.ATTRIBUTE_NUMBER2 = "ATTRIBUTE_NUMBER2";
            xCPRHIA.ATTRIBUTE_NUMBER3 = "ATTRIBUTE_NUMBER3";
            xCPRHIA.ATTRIBUTE_NUMBER4 = "ATTRIBUTE_NUMBER4";
            xCPRHIA.ATTRIBUTE_NUMBER5 = "ATTRIBUTE_NUMBER5";
            xCPRHIA.ATTRIBUTE_NUMBER6 = "ATTRIBUTE_NUMBER6";
            xCPRHIA.ATTRIBUTE_NUMBER7 = "ATTRIBUTE_NUMBER7";
            xCPRHIA.ATTRIBUTE_NUMBER8 = "ATTRIBUTE_NUMBER8";
            xCPRHIA.ATTRIBUTE_NUMBER9 = "ATTRIBUTE_NUMBER9";
            xCPRHIA.ATTRIBUTE_NUMBER10 = "ATTRIBUTE_NUMBER10";

            xCPRHIA.ATTRIBUTE_DATE2 = "ATTRIBUTE_DATE2";
            xCPRHIA.ATTRIBUTE_DATE3 = "ATTRIBUTE_DATE3";
            xCPRHIA.ATTRIBUTE_DATE4 = "ATTRIBUTE_DATE4";
            xCPRHIA.ATTRIBUTE_DATE5 = "ATTRIBUTE_DATE5";
            xCPRHIA.ATTRIBUTE_DATE6 = "ATTRIBUTE_DATE6";
            xCPRHIA.ATTRIBUTE_DATE7 = "ATTRIBUTE_DATE7";
            xCPRHIA.ATTRIBUTE_DATE8 = "ATTRIBUTE_DATE8";
            xCPRHIA.ATTRIBUTE_DATE9 = "ATTRIBUTE_DATE9";
            xCPRHIA.ATTRIBUTE_DATE10 = "ATTRIBUTE_DATE10";

            xCPRHIA.ATTRIBUTE_TIMESTAMP2 = "ATTRIBUTE_TIMESTAMP2";
            xCPRHIA.ATTRIBUTE_TIMESTAMP3 = "ATTRIBUTE_TIMESTAMP3";
            xCPRHIA.ATTRIBUTE_TIMESTAMP4 = "ATTRIBUTE_TIMESTAMP4";
            xCPRHIA.ATTRIBUTE_TIMESTAMP5 = "ATTRIBUTE_TIMESTAMP5";
            xCPRHIA.ATTRIBUTE_TIMESTAMP6 = "ATTRIBUTE_TIMESTAMP6";
            xCPRHIA.ATTRIBUTE_TIMESTAMP7 = "ATTRIBUTE_TIMESTAMP7";
            xCPRHIA.ATTRIBUTE_TIMESTAMP8 = "ATTRIBUTE_TIMESTAMP8";
            xCPRHIA.ATTRIBUTE_TIMESTAMP9 = "ATTRIBUTE_TIMESTAMP9";
            xCPRHIA.ATTRIBUTE_TIMESTAMP10 = "ATTRIBUTE_TIMESTAMP10";
            xCPRHIA.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCPRHIA.CREATION_DATE = "CREATION_DATE";
            xCPRHIA.IMPORT_SOURCE = "IMPORT_SOURCE";
            xCPRHIA.CREATE_BY = "CREATE_BY";
            xCPRHIA.LAST_UPDATE_BY = "LAST_UPDATE_BY";
            xCPRHIA.request_id = "request_id";

            xCPRHIA.pkField = "";
            xCPRHIA.table = "xcust_por_req_header_int_all";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPRHIA.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectGenTextCSV(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPRHIA.table + " WHere "+xCPRHIA.request_id+"='"+requestId+"'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String genRequisition_Number()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "SELECT next value for Sequence_Requisition_Number ;";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString().Trim();
            }
            return chk;
        }
        public String insert(XcustPorReqHeaderIntAll p)
        {
            String sql = "", chk = "";
            try
            {
                String seq = "000000" + genRequisition_Number();
                p.REQUITITION_NUMBER = p.REQUITITION_NUMBER + seq.Substring(seq.Length-6);
                p.ATTRIBUTE_NUMBER1 = p.ATTRIBUTE_NUMBER1.Equals("") ? "0" : p.ATTRIBUTE_NUMBER1;
                p.ATTRIBUTE_NUMBER2 = p.ATTRIBUTE_NUMBER2.Equals("") ? "0" : p.ATTRIBUTE_NUMBER2;
                p.ATTRIBUTE_NUMBER3= p.ATTRIBUTE_NUMBER3.Equals("") ? "0" : p.ATTRIBUTE_NUMBER3;
                p.ATTRIBUTE_NUMBER4 = p.ATTRIBUTE_NUMBER4.Equals("") ? "0" : p.ATTRIBUTE_NUMBER4;
                p.ATTRIBUTE_NUMBER5 = p.ATTRIBUTE_NUMBER5.Equals("") ? "0" : p.ATTRIBUTE_NUMBER5;
                p.ATTRIBUTE_NUMBER6 = p.ATTRIBUTE_NUMBER6.Equals("") ? "0" : p.ATTRIBUTE_NUMBER6;
                p.ATTRIBUTE_NUMBER7 = p.ATTRIBUTE_NUMBER7.Equals("") ? "0" : p.ATTRIBUTE_NUMBER7;
                p.ATTRIBUTE_NUMBER8 = p.ATTRIBUTE_NUMBER8.Equals("") ? "0" : p.ATTRIBUTE_NUMBER8;
                p.ATTRIBUTE_NUMBER9 = p.ATTRIBUTE_NUMBER9.Equals("") ? "0" : p.ATTRIBUTE_NUMBER9;
                p.ATTRIBUTE_NUMBER10 = p.ATTRIBUTE_NUMBER10.Equals("") ? "0" : p.ATTRIBUTE_NUMBER10;
                //if (p.OrpChtNum.Equals(""))
                //{
                //    return "";
                //}
                //p.RowNumber = selectMaxRowNumber(p.YearId);
                //p.Active = "1";
                //sql = "Insert Into " + xCPRHIA.table + "(" + xCPRHIA.ATTRIBUTE1 + "," + xCPRHIA.ATTRIBUTE_DATE1 + "," +
                //    xCPRHIA.ATTRIBUTE_TIMESTAMP1 + "," + xCPRHIA.BATCH_ID + "," +
                //    xCPRHIA.DESCRIPTIONS + "," + xCPRHIA.REQUESTER_EMAIL_ADDR + "," + xCPRHIA.INTERFACE_SOURCE_CODE + "," +
                //    xCPRHIA.ATTRIBUTE_CATEGORY + "," + xCPRHIA.REQ_HEADER_INTERFACE_ID + "," + xCPRHIA.PROCESS_FLAG + "," +
                //    xCPRHIA.APPROVER_EMAIL_ADDR + "," + xCPRHIA.STATUS_CODE + "," + xCPRHIA.REQ_BU_NAME + "," +
                //    xCPRHIA.REQUITITION_NUMBER + "," +
                //    xCPRHIA.ATTRIBUTE2 + "," + xCPRHIA.ATTRIBUTE3 + "," + xCPRHIA.ATTRIBUTE4 + "," +
                //    xCPRHIA.ATTRIBUTE5 + "," + xCPRHIA.ATTRIBUTE6 + "," + xCPRHIA.ATTRIBUTE7 + "," +
                //    xCPRHIA.ATTRIBUTE8 + "," + xCPRHIA.ATTRIBUTE9 + "," + xCPRHIA.ATTRIBUTE10 + "," +
                //    xCPRHIA.ATTRIBUTE11 + "," + xCPRHIA.ATTRIBUTE12 + "," + xCPRHIA.ATTRIBUTE13 + "," +
                //    xCPRHIA.ATTRIBUTE14 + "," + xCPRHIA.ATTRIBUTE15 + "," + xCPRHIA.ATTRIBUTE16 + "," +
                //    xCPRHIA.ATTRIBUTE17 + "," + xCPRHIA.ATTRIBUTE18 + "," + xCPRHIA.ATTRIBUTE19 + "," +
                //    xCPRHIA.ATTRIBUTE20 + "," +
                //    xCPRHIA.ATTRIBUTE_NUMBER1 + "," + xCPRHIA.ATTRIBUTE_NUMBER2 + "," + xCPRHIA.ATTRIBUTE_NUMBER3 + "," +
                //    xCPRHIA.ATTRIBUTE_NUMBER4 + "," + xCPRHIA.ATTRIBUTE_NUMBER5 + "," + xCPRHIA.ATTRIBUTE_NUMBER6 + "," +
                //    xCPRHIA.ATTRIBUTE_NUMBER7 + "," + xCPRHIA.ATTRIBUTE_NUMBER8 + "," + xCPRHIA.ATTRIBUTE_NUMBER9 + "," +
                //    xCPRHIA.ATTRIBUTE_NUMBER10 + "," +
                //    xCPRHIA.ATTRIBUTE_DATE2 + "," + xCPRHIA.ATTRIBUTE_DATE3 + "," + xCPRHIA.ATTRIBUTE_DATE4 + "," +
                //    xCPRHIA.ATTRIBUTE_DATE5 + "," + xCPRHIA.ATTRIBUTE_DATE6 + "," + xCPRHIA.ATTRIBUTE_DATE7 + "," +
                //    xCPRHIA.ATTRIBUTE_DATE8+ "," + xCPRHIA.ATTRIBUTE_DATE9 + "," + xCPRHIA.ATTRIBUTE_DATE10 + "," +
                //    xCPRHIA.ATTRIBUTE_TIMESTAMP2 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP3 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP4 + "," +
                //    xCPRHIA.ATTRIBUTE_TIMESTAMP5 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP6 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP7 + "," +
                //    xCPRHIA.ATTRIBUTE_TIMESTAMP8 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP9 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP10 + "," +
                //    xCPRHIA.LAST_UPDATE_DATE + "," + xCPRHIA.CREATION_DATE + "," + xCPRHIA.IMPORT_SOURCE + "," +
                //    xCPRHIA.LAST_UPDATE_BY +
                //    ") " +
                //    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_DATE1 + "','" +
                //    p.ATTRIBUTE_TIMESTAMP1 + "','" + p.BATCH_ID + "','" +
                //    p.DESCRIPTIONS + "','" + p.REQUESTER_EMAIL_ADDR + "','" + p.INTERFACE_SOURCE_CODE + "','" +
                //    p.ATTRIBUTE_CATEGORY + "','" + p.REQ_HEADER_INTERFACE_ID + "','" + p.PROCESS_FLAG + "','" +
                //    p.APPROVER_EMAIL_ADDR + "','" + p.STATUS_CODE + "','" + p.REQ_BU_NAME + "','" +
                //    p.REQUITITION_NUMBER + "','" +
                //    p.ATTRIBUTE2 + "','" + p.ATTRIBUTE3 + "','" + p.ATTRIBUTE4 + "','" +
                //    p.ATTRIBUTE5 + "','" + p.ATTRIBUTE6 + "','" + p.ATTRIBUTE7 + "','" +
                //    p.ATTRIBUTE8 + "','" + p.ATTRIBUTE9 + "','" + p.ATTRIBUTE10 + "','" +
                //    p.ATTRIBUTE11 + "','" + p.ATTRIBUTE12 + "','" + p.ATTRIBUTE13 + "','" +
                //    p.ATTRIBUTE14 + "','" + p.ATTRIBUTE15 + "','" + p.ATTRIBUTE16 + "','" +
                //    p.ATTRIBUTE17 + "','" + p.ATTRIBUTE18 + "','" + p.ATTRIBUTE19 + "','" +
                //    p.ATTRIBUTE20 + "','" +
                //    p.ATTRIBUTE_NUMBER1 + "','" + p.ATTRIBUTE_NUMBER2 + "','" + p.ATTRIBUTE_NUMBER3 + "','" +
                //    p.ATTRIBUTE_NUMBER4 + "','" + p.ATTRIBUTE_NUMBER5 + "','" + p.ATTRIBUTE_NUMBER6 + "','" +
                //    p.ATTRIBUTE_NUMBER7 + "','" + p.ATTRIBUTE_NUMBER8 + "','" + p.ATTRIBUTE_NUMBER9 + "','" +
                //    p.ATTRIBUTE_NUMBER10 + "','" +
                //    p.ATTRIBUTE_DATE2 + "','" + p.ATTRIBUTE_DATE3 + "','" + p.ATTRIBUTE_DATE4 + "','" +
                //    p.ATTRIBUTE_DATE5 + "','" + p.ATTRIBUTE_DATE6 + "','" + p.ATTRIBUTE_DATE7 + "','" +
                //    p.ATTRIBUTE_DATE8 + "','" + p.ATTRIBUTE_DATE9 + "','" + p.ATTRIBUTE_DATE10 + "','" +
                //    p.ATTRIBUTE_TIMESTAMP2 + "','" + p.ATTRIBUTE_TIMESTAMP3 + "','" + p.ATTRIBUTE_TIMESTAMP4 + "','" +
                //    p.ATTRIBUTE_TIMESTAMP5 + "','" + p.ATTRIBUTE_TIMESTAMP6 + "','" + p.ATTRIBUTE_TIMESTAMP7 + "','" +
                //    p.ATTRIBUTE_TIMESTAMP8 + "','" + p.ATTRIBUTE_TIMESTAMP9 + "','" + p.ATTRIBUTE_TIMESTAMP10 + "','" +
                //    p.LAST_UPDATE_DATE + "','" + p.CREATION_DATE + "','" + p.IMPORT_SOURCE + "','" +
                //    p.LAST_UPDATE_BY + 
                //    "') ";
                p.ATTRIBUTE_DATE1 = "";
                p.ATTRIBUTE_TIMESTAMP1 = "";
                sql = "Insert Into " + xCPRHIA.table + "(" + xCPRHIA.ATTRIBUTE1 + "," + xCPRHIA.ATTRIBUTE_DATE1 + "," +
                    xCPRHIA.ATTRIBUTE_TIMESTAMP1 + "," + xCPRHIA.BATCH_ID + "," +
                    xCPRHIA.DESCRIPTIONS + "," + xCPRHIA.REQUESTER_EMAIL_ADDR + "," + xCPRHIA.INTERFACE_SOURCE_CODE + "," +
                    xCPRHIA.ATTRIBUTE_CATEGORY + "," + xCPRHIA.REQ_HEADER_INTERFACE_ID + "," + xCPRHIA.PROCESS_FLAG + "," +
                    xCPRHIA.APPROVER_EMAIL_ADDR + "," + xCPRHIA.STATUS_CODE + "," + xCPRHIA.REQ_BU_NAME + "," +
                    xCPRHIA.REQUITITION_NUMBER + "," +
                    xCPRHIA.ATTRIBUTE2 + "," + xCPRHIA.ATTRIBUTE3 + "," + xCPRHIA.ATTRIBUTE4 + "," +
                    xCPRHIA.ATTRIBUTE5 + "," + xCPRHIA.ATTRIBUTE6 + "," + xCPRHIA.ATTRIBUTE7 + "," +
                    xCPRHIA.ATTRIBUTE8 + "," + xCPRHIA.ATTRIBUTE9 + "," + xCPRHIA.ATTRIBUTE10 + "," +
                    xCPRHIA.ATTRIBUTE11 + "," + xCPRHIA.ATTRIBUTE12 + "," + xCPRHIA.ATTRIBUTE13 + "," +
                    xCPRHIA.ATTRIBUTE14 + "," + xCPRHIA.ATTRIBUTE15 + "," + xCPRHIA.ATTRIBUTE16 + "," +
                    xCPRHIA.ATTRIBUTE17 + "," + xCPRHIA.ATTRIBUTE18 + "," + xCPRHIA.ATTRIBUTE19 + "," +
                    xCPRHIA.ATTRIBUTE20 + "," +
                    xCPRHIA.ATTRIBUTE_NUMBER1 + "," + xCPRHIA.ATTRIBUTE_NUMBER2 + "," + xCPRHIA.ATTRIBUTE_NUMBER3 + "," +
                    xCPRHIA.ATTRIBUTE_NUMBER4 + "," + xCPRHIA.ATTRIBUTE_NUMBER5 + "," + xCPRHIA.ATTRIBUTE_NUMBER6 + "," +
                    xCPRHIA.ATTRIBUTE_NUMBER7 + "," + xCPRHIA.ATTRIBUTE_NUMBER8 + "," + xCPRHIA.ATTRIBUTE_NUMBER9 + "," +
                    xCPRHIA.ATTRIBUTE_NUMBER10 + "," +
                    xCPRHIA.ATTRIBUTE_DATE2 + "," + xCPRHIA.ATTRIBUTE_DATE3 + "," + xCPRHIA.ATTRIBUTE_DATE4 + "," +
                    xCPRHIA.ATTRIBUTE_DATE5 + "," + xCPRHIA.ATTRIBUTE_DATE6 + "," + xCPRHIA.ATTRIBUTE_DATE7 + "," +
                    xCPRHIA.ATTRIBUTE_DATE8 + "," + xCPRHIA.ATTRIBUTE_DATE9 + "," + xCPRHIA.ATTRIBUTE_DATE10 + "," +
                    xCPRHIA.ATTRIBUTE_TIMESTAMP3 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP4 + "," +
                    xCPRHIA.ATTRIBUTE_TIMESTAMP5 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP6 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP7 + "," +
                    xCPRHIA.ATTRIBUTE_TIMESTAMP8 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP9 + "," + xCPRHIA.ATTRIBUTE_TIMESTAMP10 + "," +
                    xCPRHIA.LAST_UPDATE_DATE + "," + xCPRHIA.CREATION_DATE + "," + xCPRHIA.IMPORT_SOURCE + "," +
                    xCPRHIA.LAST_UPDATE_BY +","+ xCPRHIA.request_id+
                    ") " +
                    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_DATE1 + "','" +
                    p.ATTRIBUTE_TIMESTAMP1 + "'," + p.BATCH_ID + ",'" +
                    p.DESCRIPTIONS + "','" + p.REQUESTER_EMAIL_ADDR + "','" + p.INTERFACE_SOURCE_CODE + "','" +
                    p.ATTRIBUTE_CATEGORY + "','" + p.REQ_HEADER_INTERFACE_ID + "','" + p.PROCESS_FLAG + "','" +
                    p.APPROVER_EMAIL_ADDR + "','" + p.STATUS_CODE + "','" + p.REQ_BU_NAME + "','" +
                    p.REQUITITION_NUMBER + "','" +
                    p.ATTRIBUTE2 + "','" + p.ATTRIBUTE3 + "','" + p.ATTRIBUTE4 + "','" +
                    p.ATTRIBUTE5 + "','" + p.ATTRIBUTE6 + "','" + p.ATTRIBUTE7 + "','" +
                    p.ATTRIBUTE8 + "','" + p.ATTRIBUTE9 + "','" + p.ATTRIBUTE10 + "','" +
                    p.ATTRIBUTE11 + "','" + p.ATTRIBUTE12 + "','" + p.ATTRIBUTE13 + "','" +
                    p.ATTRIBUTE14 + "','" + p.ATTRIBUTE15 + "','" + p.ATTRIBUTE16 + "','" +
                    p.ATTRIBUTE17 + "','" + p.ATTRIBUTE18 + "','" + p.ATTRIBUTE19 + "','" +
                    p.ATTRIBUTE20 + "'," +
                    p.ATTRIBUTE_NUMBER1 + "," + p.ATTRIBUTE_NUMBER2 + "," + p.ATTRIBUTE_NUMBER3 + "," +
                    p.ATTRIBUTE_NUMBER4 + "," + p.ATTRIBUTE_NUMBER5 + "," + p.ATTRIBUTE_NUMBER6 + "," +
                    p.ATTRIBUTE_NUMBER7 + "," + p.ATTRIBUTE_NUMBER8 + "," + p.ATTRIBUTE_NUMBER9 + "," +
                    p.ATTRIBUTE_NUMBER10 + ",'" +
                    p.ATTRIBUTE_DATE2 + "','" + p.ATTRIBUTE_DATE3 + "','" + p.ATTRIBUTE_DATE4 + "','" +
                    p.ATTRIBUTE_DATE5 + "','" + p.ATTRIBUTE_DATE6 + "','" + p.ATTRIBUTE_DATE7 + "','" +
                    p.ATTRIBUTE_DATE8 + "','" + p.ATTRIBUTE_DATE9 + "','" + p.ATTRIBUTE_DATE10 + "','" +
                    p.ATTRIBUTE_TIMESTAMP3 + "','" + p.ATTRIBUTE_TIMESTAMP4 + "','" +
                    p.ATTRIBUTE_TIMESTAMP5 + "','" + p.ATTRIBUTE_TIMESTAMP6 + "','" + p.ATTRIBUTE_TIMESTAMP7 + "','" +
                    p.ATTRIBUTE_TIMESTAMP8 + "','" + p.ATTRIBUTE_TIMESTAMP9 + "','" + p.ATTRIBUTE_TIMESTAMP10 + "','" +
                    p.LAST_UPDATE_DATE + "','" + p.CREATION_DATE + "','" + p.IMPORT_SOURCE + "','" +
                    p.LAST_UPDATE_BY + "','" + p.request_id +
                    "') ";
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
