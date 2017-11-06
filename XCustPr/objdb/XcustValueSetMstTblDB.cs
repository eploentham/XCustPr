using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustValueSetMstTblDB
    {
        XcustValueSetMstTbl xCVSMT;
        ConnectDB conn;
        private InitC initC;
        public XcustValueSetMstTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCVSMT = new XcustValueSetMstTbl();
            xCVSMT.CREATION_DATE = "CREATION_DATE";
            xCVSMT.DESCRIPTION = "DESCRIPTION";
            xCVSMT.ENABLED_FLAG = "ENABLED_FLAG";
            xCVSMT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCVSMT.VALUE = "VALUE";
            xCVSMT.VALUE_ID = "VALUE_ID";
            xCVSMT.VALUE_SET_CODE = "VALUE_SET_CODE";
            xCVSMT.VALUE_SET_ID = "VALUE_SET_ID";

            xCVSMT.table = "XCUST_VALUE_SET_MST_TBL";
            xCVSMT.pkField = "";
        }
        public XcustValueSetMstTbl setData(DataRow row)
        {
            XcustValueSetMstTbl item;
            item = new XcustValueSetMstTbl();
            item.CREATION_DATE = row[xCVSMT.CREATION_DATE].ToString();
            item.DESCRIPTION = row[xCVSMT.DESCRIPTION].ToString();
            item.ENABLED_FLAG = row[xCVSMT.ENABLED_FLAG].ToString();
            item.LAST_UPDATE_DATE = row[xCVSMT.LAST_UPDATE_DATE].ToString();
            item.VALUE = row[xCVSMT.VALUE].ToString();
            item.VALUE_ID = row[xCVSMT.VALUE_ID].ToString();
            item.VALUE_SET_CODE = row[xCVSMT.VALUE_SET_CODE].ToString();
            item.VALUE_SET_ID = row[xCVSMT.VALUE_SET_ID].ToString();
            
            return item;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCVSMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public Boolean validateValueBySegment1(String valuesetcode, String enableflag, String value)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCVSMT.table + " where " + xCVSMT.VALUE_SET_CODE + "  = '"
                + valuesetcode + "' and " + xCVSMT.ENABLED_FLAG + "='"+ enableflag + "' and "+xCVSMT.VALUE +"='"+ value+"'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean validateValueBySegment2(String valuesetcode, String enableflag, String value)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCVSMT.table + " where " + xCVSMT.VALUE_SET_CODE + "  = '"
                + valuesetcode + "' and " + xCVSMT.ENABLED_FLAG + "='" + enableflag + "' and " + xCVSMT.VALUE + "='" + value + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean validateValueBySegment3(String valuesetcode, String enableflag, String value)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCVSMT.table + " where " + xCVSMT.VALUE_SET_CODE + "  = '"
                + valuesetcode + "' and " + xCVSMT.ENABLED_FLAG + "='" + enableflag + "' and " + xCVSMT.VALUE + "='" + value + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean validateValueBySegment4(String valuesetcode, String enableflag, String value)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCVSMT.table + " where " + xCVSMT.VALUE_SET_CODE + "  = '"
                + valuesetcode + "' and " + xCVSMT.ENABLED_FLAG + "='" + enableflag + "' and " + xCVSMT.VALUE + "='" + value + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean validateValueBySegment5(String valuesetcode, String enableflag, String value)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCVSMT.table + " where " + xCVSMT.VALUE_SET_CODE + "  = '"
                + valuesetcode + "' and " + xCVSMT.ENABLED_FLAG + "='" + enableflag + "' and " + xCVSMT.VALUE + "='" + value + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean validateValueBySegment6(String valuesetcode, String enableflag, String value)
        {
            DataTable dt = new DataTable();
            String chk = "";
            String sql = "select * From " + xCVSMT.table + " where " + xCVSMT.VALUE_SET_CODE + "  = '"
                + valuesetcode + "' and " + xCVSMT.ENABLED_FLAG + "='" + enableflag + "' and " + xCVSMT.VALUE + "='" + value + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
