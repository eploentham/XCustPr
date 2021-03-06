﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoLineIntTblDB
    {
        public XcustPoLineIntTbl xCPLIT;
        ConnectDB conn;

        public XcustPoLineIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPLIT = new XcustPoLineIntTbl();
            xCPLIT.action = "action";
            xCPLIT.category = "category";
            xCPLIT.creation_by = "creation_by";
            xCPLIT.creation_date = "creation_date";
            xCPLIT.error_message = "error_message";
            xCPLIT.interface_header_key = "interface_header_key";
            xCPLIT.interface_line_key = "interface_line_key";
            xCPLIT.item_description = "item_description";
            xCPLIT.last_update_by = "last_update_by";
            xCPLIT.last_update_date = "last_update_date";
            xCPLIT.line_num = "line_num";
            xCPLIT.line_type = "line_type";
            xCPLIT.process_flag = "process_flag";
            xCPLIT.unit_price = "unit_price";
            xCPLIT.wo_no = "wo_no";
            xCPLIT.qt_no = "qt_no";
            xCPLIT.running = "running";
            xCPLIT.attribute1 = "attribute1";
            xCPLIT.attribute2 = "attribute2";
            xCPLIT.request_id = "request_id";
            xCPLIT.attribute3 = "attribute3";
            xCPLIT.attribute4 = "attribute4";
            xCPLIT.attribute5 = "attribute5";
            xCPLIT.attribute6 = "attribute6";

            xCPLIT.table = "XCUST_PO_LINE_INT_TBL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPLIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPLIT.table +" Where " + xCPLIT.request_id + "='" + requestId + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String genSeqReqLineNumber()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "SELECT next value for xcust_po_line_seq ;";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString().Trim();
            }
            return chk;
        }
        public String insert(XcustPoLineIntTbl p, String pathLog)
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
                String seqL = genSeqReqLineNumber();
                p.interface_line_key = seqL;
                String last_update_by = "0", creation_by="0";
                sql = "Insert Into " + xCPLIT.table + "(" + xCPLIT.action + "," + xCPLIT.category + "," + xCPLIT.creation_by + "," +
                    xCPLIT.creation_date + "," + xCPLIT.error_message + "," + xCPLIT.interface_header_key + "," +
                    xCPLIT.interface_line_key + "," + xCPLIT.item_description + "," + xCPLIT.last_update_by + "," +
                    xCPLIT.last_update_date + "," + xCPLIT.line_num + "," + xCPLIT.line_type + "," +
                    xCPLIT.process_flag + "," + xCPLIT.unit_price + "," + xCPLIT.wo_no + "," + 
                    xCPLIT.qt_no + "," + xCPLIT.running + "," + xCPLIT.attribute1 + "," + 
                    xCPLIT.attribute2 + "," + xCPLIT.request_id + "," + xCPLIT.attribute3 + "," + 
                    xCPLIT.attribute4 + "," + xCPLIT.attribute5 + "," + xCPLIT.attribute6 +
                    ") " +
                    "Values('" + p.action + "','" + p.category + "','" + creation_by + "'," +
                    "getdate(),'" + p.error_message + "','" + p.interface_header_key + "','" +
                    p.interface_line_key + "','" + p.item_description + "','" + last_update_by + "'," +
                    "null,'" + p.line_num + "','" + p.line_type + "','" +
                    p.process_flag + "'," + p.unit_price + ",'" + p.wo_no + "','" + 
                    p.qt_no + "','" + p.running + "','" + p.attribute1 + "','" + 
                    p.attribute2 + "','" + p.request_id + "','" + p.attribute3 + "','" + 
                    p.attribute4 + "','" + p.attribute5 + "','" + p.attribute6 +
                    "') ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
                if (chk.Equals("1"))
                {
                    chk = seqL;
                }
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
