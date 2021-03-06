﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoDistIntTblDB
    {
        public XcustPoDistIntTbl xCPDIT;
        ConnectDB conn;

        public XcustPoDistIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPDIT = new XcustPoDistIntTbl();
            xCPDIT.amt = "amt";
            xCPDIT.charge_account_segment1 = "charge_account_segment1";
            xCPDIT.charge_account_segment2 = "charge_account_segment2";
            xCPDIT.charge_account_segment3 = "charge_account_segment3";
            xCPDIT.charge_account_segment4 = "charge_account_segment4";
            xCPDIT.charge_account_segment5 = "charge_account_segment5";
            xCPDIT.charge_account_segment6 = "charge_account_segment6";
            xCPDIT.creation_by = "creation_by";
            xCPDIT.creation_date = "creation_date";
            xCPDIT.deliver_to_location = "deliver_to_location";
            xCPDIT.destion_subinventory = "destination_subinventory";
            xCPDIT.distribution_num = "distribution_num";
            xCPDIT.error_message = "error_message";
            xCPDIT.interface_distribution_key = "interface_distribution_key";
            xCPDIT.interface_header_key = "interface_header_key";
            xCPDIT.interface_line_key = "interface_line_key";
            xCPDIT.interface_line_location_key = "interface_line_location_key";
            xCPDIT.last_update_by = "last_update_by";
            xCPDIT.last_update_date = "last_update_date";
            xCPDIT.process_flag = "process_flag";
            xCPDIT.wo_no = "wo_no";
            xCPDIT.running = "running";
            xCPDIT.request_id = "request_id";
            xCPDIT.requester = "requester";

            xCPDIT.table = "XCUST_PO_DIST_INT_TBL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPDIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectByRequestId(String requestId)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPDIT.table+" Where "+xCPDIT.request_id+"='"+requestId+"'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String genSeqReqDistNumber()
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "SELECT next value for xcust_po_distribution_seq ;";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString().Trim();
            }
            return chk;
        }
        public DataTable selectChargeAccMR(String orgId)
        {
            DataTable dt = new DataTable();
            String sql = "select org.material_account_ccid "+
                ", gcc.SEGMENT1 account_seg1 " +
                 ", gcc.SEGMENT2 account_seg2 " +
                 " , gcc.SEGMENT3 account_seg3 " +
                 "  , gcc.SEGMENT4 account_seg4 " +
                 "   , gcc.SEGMENT5 account_seg5 " +
                 "    , gcc.SEGMENT6 account_seg6 " +
                "from xcust_organization_mst_tbl org " +
                ",xcust_gl_code_combinations_tbl gcc " +
                "where org.organization_code = '"+ orgId + "' " +
                " and org.material_account_ccid = gcc.code_combination_id " +
                " and org.CHART_OF_ACCOUNTS_ID = gcc.CHART_OF_ACCOUNTS_ID ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String validateChargeAcc(String org,String seg1, String seg2, String seg3, String seg4, String seg5, String seg6)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select count(*) "+
                "from xcust_organization_mst_tbl org " +
                ",xcust_gl_code_combinations_tbl gcc " +
                "where org.organization_code = '"+ org + "' " +
                " and org.CHART_OF_ACCOUNTS_ID = gcc.CHART_OF_ACCOUNTS_ID " +
                " and gcc.SEGMENT1 = '" + seg1 + "'" +
                " and gcc.segment2 = '" + seg2 + "'" +
                " and gcc.segment3 = '" + seg3 + "'" +
                " and gcc.segment4 = '" + seg4 + "'" +
                " and gcc.segment5 = '" + seg5 + "'" +
                " and gcc.segment6 = '" + seg6 + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][0].ToString().Trim();
            }
            return chk;
        }
        public DataTable selectChargeAccFA(String orgCode)
        {
            DataTable dt = new DataTable();
            String sql = "select gl.segment1,gl.segment2,gl.segment3,gl.segment4,gl.segment5,gl.segment6 " +
                "from xcust_item_mst_tbl msi " +
                ",xcust_gl_code_combinations_tbl gl " +
                ",xcust_organization_mst_tbl org " +
                " where msi.asset_category_code = " +
                " and msi.organization_id = org.organization_id " +
                " and org.organization_code = '"+ orgCode + "' " +
                " and gl.CHART_OF_ACCOUNTS_ID = org.CHART_OF_ACCOUNTS_ID " +
                " and msi.account_code_combination_id = gl.code_combination_id ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String insert(XcustPoDistIntTbl p, String pathLog)
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
                String seqD = genSeqReqDistNumber();
                p.interface_distribution_key = seqD;
                sql = "Insert Into " + xCPDIT.table + "(" + xCPDIT.amt + "," + xCPDIT.charge_account_segment1 + "," + xCPDIT.charge_account_segment2 + "," +
                    xCPDIT.charge_account_segment3 + "," + xCPDIT.charge_account_segment4 + "," + xCPDIT.charge_account_segment5 + "," +
                    xCPDIT.charge_account_segment6 + "," + xCPDIT.creation_by + "," + xCPDIT.creation_date + "," +
                    xCPDIT.deliver_to_location + "," + xCPDIT.destion_subinventory + "," + xCPDIT.distribution_num + "," +
                    xCPDIT.error_message + "," + xCPDIT.interface_distribution_key + "," + xCPDIT.interface_header_key + "," +
                    xCPDIT.interface_line_key + "," + xCPDIT.interface_line_location_key + "," + xCPDIT.last_update_by + "," +
                    xCPDIT.last_update_date + "," + xCPDIT.process_flag + "," + xCPDIT.wo_no + "," + 
                    xCPDIT.running + "," + xCPDIT.request_id + "," + xCPDIT.requester +
                    ") " +
                    "Values('" + p.amt + "','" + p.charge_account_segment1 + "','" + p.charge_account_segment2 + "','" +
                    p.charge_account_segment3 + "','" + p.charge_account_segment4 + "','" + p.charge_account_segment5 + "','" +
                    p.charge_account_segment6 + "','" + creation_by + "',getdate(),'" +
                    p.deliver_to_location + "','" + p.destion_subinventory + "','" + p.distribution_num + "','" +
                    p.error_message + "','" + p.interface_distribution_key + "','" + p.interface_header_key + "','" +
                    p.interface_line_key + "','" + p.interface_line_location_key + "','" + last_update_by + "'," +
                    "null,'" + p.process_flag + "','" + p.wo_no + "','" + 
                    p.running + "','"+ p.request_id + "','" + p.requester + "'" +
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
    }
}
