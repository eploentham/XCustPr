using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustInvTrnIntTblDB
    {
        public XcustInvTrnIntTbl xCITIT;
        ConnectDB conn;

        public XcustInvTrnIntTblDB(ConnectDB c)
        {
            conn = c;            
            initConfig();

        }
        private void initConfig()
        {
            xCITIT = new XcustInvTrnIntTbl();
            xCITIT.creation_by = "creation_by";
            xCITIT.creation_date = "creation_date";
            xCITIT.error_message = "error_message";
            xCITIT.INV_LOTSERIAL_INTERFACE_NUM = "INV_LOTSERIAL_INTERFACE_NUM";
            xCITIT.INV_PROCESS_FLAG = "INV_PROCESS_FLAG";
            xCITIT.ITEM_CODE = "ITEM_CODE";
            xCITIT.last_update_by = "last_update_by";
            xCITIT.last_update_date = "last_update_date";
            xCITIT.LOCATOR_NAME = "LOCATOR_NAME";
            xCITIT.ORGANIZATION_NAME = "ORGANIZATION_NAME";
            xCITIT.process_flag = "process_flag";
            xCITIT.QTY = "QTY";
            xCITIT.SEGMENT1 = "SEGMENT1";
            xCITIT.SEGMENT2 = "SEGMENT2";
            xCITIT.SOURCE_CODE = "SOURCE_CODE";
            xCITIT.SUBINVENTORY_CODE = "SUBINVENTORY_CODE";
            xCITIT.TRANSACTION_DATE = "TRANSACTION_DATE";
            xCITIT.TRANSACTION_MODE = "TRANSACTION_MODE";
            xCITIT.TRANSACTION_TYPE_NAME = "TRANSACTION_TYPE_NAME";
            xCITIT.TRANSFER_LOCATOR_NAME = "TRANSFER_LOCATOR_NAMETRANSACTION_TYPE_NAME";
            xCITIT.TRANSFER_ORGANIZATION_NAME = "TRANSFER_ORGANIZATION_NAME";
            xCITIT.TRANSFER_SUBINVENTORY_CODE = "TRANSFER_SUBINVENTORY_CODE";
            xCITIT.UOM = "UOM";

            xCITIT.table = "XCUST_INV_TRN_INT_TBL";
        }
        public String insert(XcustInvTrnIntTbl p)
        {
            String sql = "", chk = "";
            String createBy = "0", createDate = "GETDATE()", last_update_by = "0", lastUpdateTime = "null";
            try
            {
                String seq = "000000";
                p.TRANSACTION_DATE = p.TRANSACTION_DATE.Equals("") ? "getdate()" : p.TRANSACTION_DATE;
                sql = "Insert Into " + xCITIT.table + "(" + xCITIT.creation_by + "," + xCITIT.creation_date + "," + xCITIT.error_message + "," +
                    xCITIT.INV_LOTSERIAL_INTERFACE_NUM + "," + xCITIT.INV_PROCESS_FLAG + ","+ xCITIT.ITEM_CODE + "," +
                   xCITIT.last_update_by + "," + xCITIT.last_update_date + "," + xCITIT.LOCATOR_NAME + "," +
                    xCITIT.ORGANIZATION_NAME + "," + xCITIT.process_flag + "," + xCITIT.QTY + "," +
                    xCITIT.SEGMENT1 + "," + xCITIT.SEGMENT2 + "," + xCITIT.SOURCE_CODE + "," +
                    xCITIT.SUBINVENTORY_CODE + "," + xCITIT.TRANSACTION_DATE + "," + xCITIT.TRANSACTION_MODE + "," +
                    xCITIT.TRANSACTION_TYPE_NAME + "," + xCITIT.TRANSFER_LOCATOR_NAME + "," + xCITIT.TRANSFER_ORGANIZATION_NAME + "," +
                    xCITIT.TRANSFER_SUBINVENTORY_CODE + "," + xCITIT.UOM +
                    ") " +
                    "Values ('" + createBy + "'," + createDate + ",'" + p.error_message + "','" +
                    p.INV_LOTSERIAL_INTERFACE_NUM + "','" + p.INV_PROCESS_FLAG + "','" + p.ITEM_CODE + "','" +
                    last_update_by + "'," + lastUpdateTime + ",'" + p.LOCATOR_NAME + "','" +
                    p.ORGANIZATION_NAME + "','" + p.process_flag + "','" + p.QTY + "','" +
                    p.SEGMENT1 + "','" + p.SEGMENT2 + "','" + p.SOURCE_CODE + "','" +
                    p.SUBINVENTORY_CODE + "'," + p.TRANSACTION_DATE + ",'" + p.TRANSACTION_MODE + "','" +
                    p.TRANSACTION_TYPE_NAME + "','" + p.TRANSFER_LOCATOR_NAME + "','" + p.TRANSFER_ORGANIZATION_NAME + "','" +
                    p.TRANSFER_SUBINVENTORY_CODE + "','" + p.UOM + "','" + 
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
