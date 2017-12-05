using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustInvTrnLotInvTblDB
    {
        public XcustInvTrnLotInvTbl xCITLIT;
        ConnectDB conn;

        public XcustInvTrnLotInvTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCITLIT = new XcustInvTrnLotInvTbl();
            xCITLIT.CREATION_BY = "CREATION_BY";
            xCITLIT.CREATION_DATE = "CREATION_DATE";
            xCITLIT.ERROR_MESSAGE = "ERROR_MESSAGE";
            xCITLIT.INV_LOT_INTERFACE_NUM = "INV_LOT_INTERFACE_NUM";
            xCITLIT.INV_SERIAL_INTERFACE_NUM = "INV_SERIAL_INTERFACE_NUM";
            xCITLIT.LAST_UPDATE_BY = "LAST_UPDATE_BY";
            xCITLIT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCITLIT.LOT_EXPIRATION_DATE = "LOT_EXPIRATION_DATE";
            xCITLIT.LOT_NUMBER = "LOT_NUMBER";
            xCITLIT.PRIMARY_QUANTITY = "PRIMARY_QUANTITY";
            xCITLIT.PROCESS_FLAG = "PROCESS_FLAG";
            xCITLIT.TRANSACTION_QUANTITY = "TRANSACTION_QUANTITY";

            xCITLIT.table = "XCUST_INV_TRN_LOT_INT_TBL";
        }
        public String insert(XcustInvTrnLotInvTbl p, String pathLog)
        {
            String sql = "", chk = "";
            String createBy = "0", createDate = "GETDATE()", last_update_by = "0", lastUpdateTime = "null";
            try
            {
                String seq = "000000";
                p.LOT_EXPIRATION_DATE = p.LOT_EXPIRATION_DATE.Equals("") ? "getdate()" : p.LOT_EXPIRATION_DATE;
                sql = "Insert Into " + xCITLIT.table + "(" + xCITLIT.CREATION_BY + "," + xCITLIT.CREATION_DATE + "," + xCITLIT.ERROR_MESSAGE + "," +
                    xCITLIT.INV_LOT_INTERFACE_NUM + "," + xCITLIT.INV_SERIAL_INTERFACE_NUM + "," + xCITLIT.LAST_UPDATE_BY + "," +
                   xCITLIT.LAST_UPDATE_DATE + "," + xCITLIT.LOT_EXPIRATION_DATE + "," + xCITLIT.LOT_NUMBER + "," +
                    xCITLIT.PRIMARY_QUANTITY + "," + xCITLIT.PROCESS_FLAG + "," + xCITLIT.TRANSACTION_QUANTITY + "," +
                   
                    ") " +
                    "Values ('" + createBy + "'," + createDate + ",'" + p.ERROR_MESSAGE + "','" +
                    p.INV_LOT_INTERFACE_NUM + "'," + p.INV_SERIAL_INTERFACE_NUM + ",'" + p.LAST_UPDATE_BY + "'," +
                    lastUpdateTime + ",'" + p.LOT_EXPIRATION_DATE + "','" + p.LOT_NUMBER + "','" +
                    p.PRIMARY_QUANTITY + "','" + p.PROCESS_FLAG + "','" + p.TRANSACTION_QUANTITY + "','" +
                    
                    ")";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }
            return chk;
        }
    }
}
