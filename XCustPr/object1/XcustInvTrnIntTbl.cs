using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustInvTrnIntTbl:Persistent
    {
        public String ORGANIZATION_NAME = "", INV_PROCESS_FLAG = "", ITEM_CODE = "", INV_LOTSERIAL_INTERFACE_NUM = "", SUBINVENTORY_CODE = "", LOCATOR_NAME = "", SEGMENT1 = "", SEGMENT2 = "";
        public String QTY = "", UOM = "", TRANSACTION_DATE = "", TRANSACTION_TYPE_NAME = "", TRANSFER_ORGANIZATION_NAME = "", TRANSFER_SUBINVENTORY_CODE = "", TRANSFER_LOCATOR_NAME = "", SOURCE_CODE = "";
        public String TRANSACTION_MODE = "", process_flag = "", error_message = "", creation_by = "", creation_date = "", last_update_date = "", last_update_by = "";
    }
}
