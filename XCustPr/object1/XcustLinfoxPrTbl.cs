using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustLinfoxPrTbl:Persistent
    {
        public String COMPANYCODE = "", PO_NUMBER = "", LINE_NUMBER = "", SUPPLIER_CODE = "", ORDER_DATE = "", REQUEST_DATE = "", ITEM_CODE = "", QTY = "", UOMCODE = "", DELIVERY_INSTRUCTION = "", VALIDATE_FLAG = "", PROCESS_FLAG = "", ERROR_MSG = "";
        public String create_by = "", create_date = "", last_update_date = "", last_update_by = "", file_name = "", store_code="", REQUEST_TIME="";

        public String diriver_to_organization = "", deriver_to_location = "", subinventory_code = "", ERP_ITEM_CODE = "", AGREEEMENT_NUMBER = "";
        public String AGREEMENT_LINE_NUMBER = "", PRICE = "", ITEM_CATEGORY_NAME = "", SUPPLIER_SITE_CODE = "", ACC_SEG1 = "", ACC_SEG2 = "";
        public String ACC_SEG3 = "", ACC_SEG4 = "", ACC_SEG5 = "", ACC_SEG6 = "";

        public String SEND_PO_FLAG = "", GEN_OUTBOUD_FLAG = "", ERP_PO_HEADER_ID = "", ERP_PO_LINE_ID = "", ERP_PO_NUMBER = "", ERP_PO_LINE_NUMBER = "", ERP_QTY = "", request_id="";
    }
}
