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
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCVSMT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }

    }
}
