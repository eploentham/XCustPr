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
        public XcustValueSetMstTbl xCVSMT;
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
        public DataTable selectByPk(String value_set_id, String value_id)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCVSMT.table+" Where "+xCVSMT.VALUE_SET_ID+"="+value_set_id+" and "+xCVSMT.VALUE_ID+"="+value_id+"";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String selectUOM(String desc)
        {
            String uom = "";
            DataTable dt = new DataTable();
            String sql = "select * From " + xCVSMT.table + " Where " + xCVSMT.VALUE_SET_CODE + "='RD_UOM_MMX_MAPPING' and " + xCVSMT.DESCRIPTION + "='" + desc + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                uom = dt.Rows[0][xCVSMT.VALUE].ToString().Trim();
            }
            return uom;
        }
        public String selectUOMLFM(String desc)
        {
            String uom = "";
            DataTable dt = new DataTable();
            String sql = "select * From " + xCVSMT.table + " Where " + xCVSMT.VALUE_SET_CODE + "='RD_UOM_LFX_MAPPING' and " + xCVSMT.DESCRIPTION + "='" + desc + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count > 0)
            {
                uom = dt.Rows[0][xCVSMT.VALUE].ToString().Trim();
            }
            return uom;
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
        /*
         * 1. Primary key คือ value_set_id ,value_id
         * 2. กรณีเจอพบว่า PK ไม่เจอใน Table ต้องทำการ Insesrt
         * 3. กรณีที่เจอว่า PK เจอใน Table ต้อง check last update date หาก เจอว่าไม่เหมือนกัน ให้ทำการ Update 2 Field คือ
         * - Description
         * - Enable_flag
         */
        public String insertFromText(String data, String host)
        {
            String chk = "";

            int VALUE_SET_ID = 0, VALUE_SET_CODE = 1, VALUE_ID = 2, VALUE = 3, DESCRIPTION = 4, ENABLED_FLAG = 5, LAST_UPDATE_DATE = 6, CREATION_DATE = 7;


            String sql = "";
            String[] data1 = data.Split(',');

            DataTable dt = selectByPk(data[VALUE_SET_ID].ToString(), data[VALUE_ID].ToString());
            if (dt.Rows.Count>0)
            {
                sql = "Update "+xCVSMT.table+" Set "+xCVSMT.DESCRIPTION+"='"+data1[DESCRIPTION].ToString()+"', "+xCVSMT.ENABLED_FLAG+"='"+data1[ENABLED_FLAG].ToString()+"' "+
                    "Where "+xCVSMT.VALUE_SET_ID+" = "+ data1[VALUE_SET_ID].ToString() + " and "+xCVSMT.VALUE_ID+" = "+ data1[VALUE_ID].ToString() + "";
            }
            else
            {

                sql = "Insert into " + xCVSMT.table + "(" + xCVSMT.CREATION_DATE + "," + xCVSMT.DESCRIPTION + "," + xCVSMT.ENABLED_FLAG + "," +
                    xCVSMT.LAST_UPDATE_DATE + "," + xCVSMT.VALUE + "," + xCVSMT.VALUE_ID + "," +
                    xCVSMT.VALUE_SET_CODE + "," + xCVSMT.VALUE_SET_ID + ") " +
                    "Values ('" + data1[CREATION_DATE] + "','" + data1[DESCRIPTION] + "','" + data1[ENABLED_FLAG]
                    + "','" + data1[LAST_UPDATE_DATE] + "','" + data1[VALUE] + "'," + data1[VALUE_ID]
                    + ",'" + data1[VALUE_SET_CODE] + "'," + data1[VALUE_SET_ID] + ")";
            }

            chk = conn.ExecuteNonQuery(sql.ToString(), host, initC.ValueSetPathLog);
            return chk;
        }
        public String insertFromxCVSMT(XcustValueSetMstTbl item, String host)
        {
            String chk = "";

            int VALUE_SET_ID = 0, VALUE_SET_CODE = 1, VALUE_ID = 2, VALUE = 3, DESCRIPTION = 4, ENABLED_FLAG = 5, LAST_UPDATE_DATE = 6, CREATION_DATE = 7;


            String sql = "";
            //String[] data1 = item.Split(',');

            DataTable dt = selectByPk(item.VALUE_SET_ID, item.VALUE_ID);
            if (dt.Rows.Count > 0)
            {
                sql = "Update " + xCVSMT.table + " Set " + xCVSMT.DESCRIPTION + "='" + item.DESCRIPTION + "', " + xCVSMT.ENABLED_FLAG + "='" + item.ENABLED_FLAG + "' " +
                    "Where " + xCVSMT.VALUE_SET_ID + " = " + item.VALUE_SET_ID + " and " + xCVSMT.VALUE_ID + " = " + item.VALUE_ID + "";
            }
            else
            {

                sql = "Insert into " + xCVSMT.table + "(" + xCVSMT.CREATION_DATE + "," + xCVSMT.DESCRIPTION + "," + xCVSMT.ENABLED_FLAG + "," +
                    xCVSMT.LAST_UPDATE_DATE + "," + xCVSMT.VALUE + "," + xCVSMT.VALUE_ID + "," +
                    xCVSMT.VALUE_SET_CODE + "," + xCVSMT.VALUE_SET_ID + ") " +
                    "Values ('" + item.CREATION_DATE + "','" + item.DESCRIPTION + "','" + item.ENABLED_FLAG
                    + "','" + item.LAST_UPDATE_DATE + "','" + item.VALUE + "'," + item.VALUE_ID
                    + ",'" + item.VALUE_SET_CODE + "'," + item.VALUE_SET_ID + ")";
            }

            chk = conn.ExecuteNonQuery(sql.ToString(), host, initC.ValueSetPathLog);
            return chk;
        }
    }
}
