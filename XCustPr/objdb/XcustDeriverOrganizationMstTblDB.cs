using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustDeriverOrganizationMstTblDB
    {
        XcustDeriverOrganizationMstTbl xCDOMT;
        ConnectDB conn;
        private InitC initC;
        
        public XcustDeriverOrganizationMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCDOMT = new XcustDeriverOrganizationMstTbl();
            xCDOMT.bu_id = "bu_id";
            xCDOMT.CREATION_DATE = "CREATION_DATE";
            xCDOMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCDOMT.ledger_id = "ledger_id";
            xCDOMT.ORGANIZATION_code = "ORGANIZATION_code";
            xCDOMT.organization_name = "organization_name";
            xCDOMT.status = "status";

            xCDOMT.table = "xcust_deriver_organization_mst_tbl";
            xCDOMT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCDOMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        /*
         * Error PO001-009 : Invalid Deliver-to Organization
         */
        public DataTable selectActive()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCDOMT.table+" Where "+xCDOMT.status+"='A'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String selectActive1()
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "select * From " + xCDOMT.table + " Where " + xCDOMT.status + "='A'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0][xCDOMT.ORGANIZATION_code].ToString().Trim();
            }
            return chk;
        }
        public String selectActiveByCode(String code)
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "select organization_id "+
                "from xcust_organization_mst_tbl "+
                "where organization_code = '"+code+"' ";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                chk = dt.Rows[0]["organization_id"].ToString().Trim();
            }
            else
            {
                chk = "D";
            }
            return chk;
        }
    }
}
