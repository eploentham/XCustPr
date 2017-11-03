using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPorReqHeaderIntAll:Persistent
    {
        public String PO_NUMBER = "", import_source = "", Requisitioning_BU = "", Batch_ID = "", PR_STATAUS = "", PR_APPROVER = "", ENTER_BY = "";
        public String Requisition_Number = "", Description = "", ATTRIBUTE_CATEGORY = "";
        public String ATTRIBUTE1 = "", ATTRIBUTE_DATE1 = "", ATTRIBUTE_TIMESTAMP1 = "", PROCESS_FLAG = "";
    }
}
