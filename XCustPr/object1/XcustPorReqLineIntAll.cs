using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPorReqLineIntAll:Persistent
    {
        public String PO_NUMBER = "", PO_LINE_NUMBER = "", INVENTORY = "", Requisitioning_BU = "", Deliver_to_Location = "", Deliver_to_Organization = "", PR_APPROVER = "", Subinventory = "";
        public String requester = "", Category_Name = "", Need_by_Date = "", ITEM_NUMBER = "", Goods = "", QTY = "", CURRENCY_CODE = "", Price = "", Procurement_BU = "", SUPPLIER_CODE = "", Supplier_Site = "";
        public String LINFOX_PR = "", ATTRIBUTE1 = "", ATTRIBUTE_NUMBER1 = "", ATTRIBUTE_DATE1 = "", ATTRIBUTE_TIMESTAMP1 = "", PROCESS_FLAG = "";
    }
}
