using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustInvTrnSerialInvTblDB
    {
        public XcustInvTrnSerialInvTbl xCITSIT;
        ConnectDB conn;

        public XcustInvTrnSerialInvTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCITSIT = new XcustInvTrnSerialInvTbl();
            xCITSIT.CREATION_BY = "CREATION_BY";
            xCITSIT.CREATION_DATE = "CREATION_DATE";
            xCITSIT.ERROR_MESSAGE = "ERROR_MESSAGE";
            xCITSIT.FM_SERIAL_NUMBER = "FM_SERIAL_NUMBER";
            xCITSIT.INV_SERIAL_INTERFACE_NUM = "INV_SERIAL_INTERFACE_NUM";
            xCITSIT.LAST_UPDATE_BY = "LAST_UPDATE_BY";
            xCITSIT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCITSIT.PROCESS_FLAG = "PROCESS_FLAG";
            xCITSIT.TO_SERIAL_NUMBER = "TO_SERIAL_NUMBER";

            xCITSIT.table = "XCUST_INV_TRN_SERIAL_INT_TBL";
        }
        public String insert(XcustInvTrnSerialInvTbl p)
        {
            String sql = "", chk = "";
            String createBy = "0", createDate = "GETDATE()", last_update_by = "0", lastUpdateTime = "null";
            try
            {
                String seq = "000000";
                //p.LOT_EXPIRATION_DATE = p.LOT_EXPIRATION_DATE.Equals("") ? "getdate()" : p.LOT_EXPIRATION_DATE;
                sql = "Insert Into " + xCITSIT.table + "(" + xCITSIT.CREATION_BY + "," + xCITSIT.CREATION_DATE + "," + xCITSIT.ERROR_MESSAGE + "," +
                    xCITSIT.FM_SERIAL_NUMBER + "," + xCITSIT.INV_SERIAL_INTERFACE_NUM + "," + xCITSIT.LAST_UPDATE_BY + "," +
                    xCITSIT.LAST_UPDATE_DATE + "," + xCITSIT.PROCESS_FLAG + "," + xCITSIT.TO_SERIAL_NUMBER + "," +                   

                    ") " +
                    "Values ('" + createBy + "'," + createDate + ",'" + p.ERROR_MESSAGE + "','" +
                    p.FM_SERIAL_NUMBER + "'," + p.INV_SERIAL_INTERFACE_NUM + ",'" + p.LAST_UPDATE_BY + "'," +
                    lastUpdateTime + ",'" + p.PROCESS_FLAG + "','" + p.TO_SERIAL_NUMBER + "','" +                    

                    ")";
                chk = conn.ExecuteNonQuery(sql, "kfc_po");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }
            return chk;
        }
    }
}
