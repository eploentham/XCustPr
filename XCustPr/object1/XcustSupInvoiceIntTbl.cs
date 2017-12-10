using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustSupInvoiceIntTbl:Persistent
    {
        public String INVOICE_NUM="";
        public String INVOICE_DATE="";
        public String STORE="";
        public String BASE_AMOUNT="";
        public String VAT_AMOUNT="";
        public String TOTAL="";
        public String PRICE="";
        public String QTY="";
        public String BASE_AMOUNT2="";
        public String VAT_AMOUNT2="";
        public String TOTAL2="";
        public String PO_NUMBER="";
        public String FILE_NAME="";
        public String VALIDATE_FLAG="";
        public String PROCESS_FLAG="";
        public String ERROR_MSG="";
        public String request_id="";
        public String row_number = "";
        public String row_cnt = "";
    }
}
