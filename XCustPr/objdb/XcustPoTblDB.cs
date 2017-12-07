using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoTblDB
    {
        public XcustPoTbl xCPO;
        ConnectDB conn;
        private InitC initC;

        public XcustPoTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCPO = new XcustPoTbl();
            xCPO.AGENT_ID = "AGENT_ID";
            xCPO.APPROVED_DATE = "APPROVED_DATE";
            xCPO.APPROVED_FLAG = "APPROVED_FLAG";
            xCPO.ASSESSABLE_VALUE = "ASSESSABLE_VALUE";
            xCPO.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPO.ATTRIBUTE2 = "ATTRIBUTE2";
            xCPO.ATTRIBUTE3 = "ATTRIBUTE3";
            xCPO.ATTRIBUTE4 = "ATTRIBUTE4";
            xCPO.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCPO.BILLTO_BU = "BILLTO_BU";
            xCPO.BILLTO_BU_ID = "BILLTO_BU_ID";
            xCPO.BILL_TO_LOCATION_ID = "BILL_TO_LOCATION_ID";
            xCPO.BUYER = "BUYER";
            xCPO.CATEGORY_NM = "CATEGORY_NM";
            xCPO.CREATION_DATE = "CREATION_DATE";
            xCPO.CURRENCY_CODE = "CURRENCY_CODE";
            xCPO.DELIVER_TO_LOC = "DELIVER_TO_LOC";
            xCPO.DELIVER_TO_LOC_LINFOX = "DELIVER_TO_LOC_LINFOX";
            xCPO.DESTINATION_TYPE = "DESTINATION_TYPE";
            xCPO.DOCUMENT_STATUS = "DOCUMENT_STATUS";
            xCPO.EMAIL_ADDRESS = "EMAIL_ADDRESS";
            xCPO.GEN_OUTBOUD_FLAG = "GEN_OUTBOUD_FLAG";
            xCPO.ITEM_DESCRIPTION = "ITEM_DESCRIPTION";
            xCPO.ITEM_ID = "ITEM_ID";
            xCPO.LAST_UPDATED_BY = "LAST_UPDATED_BY";
            xCPO.LINE_NUM = "LINE_NUM";
            xCPO.LINE_STATUS = "LINE_STATUS";
            xCPO.LINE_TYPE_ID = "LINE_TYPE_ID";
            xCPO.PAYMENT_TERM = "PAYMENT_TERM";
            xCPO.PO_HEADER_ID = "PO_HEADER_ID";
            xCPO.PO_LINE_ID = "PO_LINE_ID";
            xCPO.PRC_BU = "PRC_BU";
            xCPO.PRC_BU_ID = "PRC_BU_ID";
            xCPO.PRODUCT_TYPE = "PRODUCT_TYPE";
            xCPO.QUANTITY = "QUANTITY";
            xCPO.QUANTITY_RECEIPT = "QUANTITY_RECEIPT";
            xCPO.REQUISITION_HEADER_ID = "REQUISITION_HEADER_ID";
            xCPO.REQUISITION_LINE_ID = "REQUISITION_LINE_ID";
            xCPO.REQ_BU = "REQ_BU";
            xCPO.REQ_BU_ID = "REQ_BU_ID";
            xCPO.REVISED_DATE = "REVISED_DATE";
            xCPO.REVISION_NUM = "REVISION_NUM";
            xCPO.SEGMENT1 = "SEGMENT1";
            xCPO.SHIP_TO_LOCATION_ID = "SHIP_TO_LOCATION_ID";
            xCPO.SOLDTO_LE = "SOLDTO_LE";
            xCPO.SOLDTO_LE_ID = "SOLDTO_LE_ID";
            xCPO.SUPPLIER_NOTIF_METHOD = "SUPPLIER_NOTIF_METHOD";
            xCPO.SUPP_NAME = "SUPP_NAME";
            xCPO.TYPE_LOOKUP_CODE = "TYPE_LOOKUP_CODE";
            xCPO.UNIT_PRICE = "UNIT_PRICE";
            xCPO.UOM_CODE = "UOM_CODE";
            xCPO.VENDOR_CONTACT_ID = "VENDOR_CONTACT_ID";
            xCPO.VENDOR_ID = "VENDOR_ID";
            xCPO.VENDOR_SITE = "VENDOR_SITE";

            xCPO.TAX_AMOUNT = "TAX_AMOUNT";
            xCPO.TAX_CODE = "TAX_CODE";
            xCPO.ACC_SEGMENT1 = "ACC_SEGMENT1";
            xCPO.ACC_SEGMENT2 = "ACC_SEGMENT2";
            xCPO.ACC_SEGMENT3 = "ACC_SEGMENT3";
            xCPO.ACC_SEGMENT4 = "ACC_SEGMENT4";
            xCPO.ACC_SEGMENT5 = "ACC_SEGMENT5";
            xCPO.ACC_SEGMENT6 = "ACC_SEGMENT6";

            xCPO.ATTRIBUTE1_L = "ATTRIBUTE1_L";
            xCPO.ATTRIBUTE2_L = "ATTRIBUTE2_L";
            xCPO.ATTRIBUTE3_L = "ATTRIBUTE3_L";
            xCPO.ATTRIBUTE_CATEGORY_L = "ATTRIBUTE_CATEGORY_L";

            xCPO.COUNT_AP_INVOICE = "COUNT_AP_INVOICE";
            xCPO.PRODUCT_TYPE = "PRODUCT_TYPE";
            xCPO.ASSESSABLE_VALUE = "ASSESSABLE_VALUE";

            xCPO.table = "xcust_PO_TBL";

        }
        public Boolean selectDupPk(String po_header_id, String po_line_id)
        {
            String sql = "";
            Boolean chk = false;
            DataTable dt = new DataTable();
            sql = "Select count(1) as cnt From " + xCPO.table + " Where " + xCPO.PO_HEADER_ID + "='" + po_header_id + "' and " + xCPO.PO_LINE_ID + "='" + po_line_id + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count >= 1)
            {
                chk = true;
            }
            return chk;
        }
        public void deletexCPR(String po_header_id, String po_line_id, String pathLog)
        {
            String sql = "Delete From " + xCPO.table + " Where " + xCPO.PO_HEADER_ID + "='" + po_header_id + "' and " + xCPO.PO_LINE_ID + "='" + po_line_id + "'";
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
        }
        public String updateOutBoundFlag(String linfox_po_number, String linfox_po_line_number, String pathLog)
        {
            String sql = "", chk = "";
            sql = "Update " + xCPO.table + " " +
                "Set " + xCPO.GEN_OUTBOUD_FLAG+"='Y' "+
                "Where "+xCPO.PO_HEADER_ID + " in " +
                "(SELECT po.PO_HEADER_ID From xcust_pr_tbl PR " +
                "Left Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID " +
                "Where PR.ATTRIBUTE1_L = '" + linfox_po_number + "' " +
                "and PR.ATTRIBUTE2_L = '" + linfox_po_line_number + "') " ;
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
            return chk;
        }
        public String insertxCPR(XcustPoTbl p, String pathLog)
        {
            String sql = "", chk = "";
            if (selectDupPk(p.PO_HEADER_ID, p.PO_LINE_ID))
            {
                deletexCPR(p.PO_HEADER_ID, p.PO_LINE_ID, pathLog);
            }
            chk = insert(p, pathLog);
            return chk;
        }
        public String insert(XcustPoTbl p, String pathLog)
        {
            String sql = "", chk = "";
            try
            {
                //if (p.OrpChtNum.Equals(""))
                //{
                //    return "";
                //}
                //p.RowNumber = selectMaxRowNumber(p.YearId);
                //p.Active = "1";

                if (p.QUANTITY.ToLower().Equals("PCS"))
                {
                    chk = "";
                }

                p.SOLDTO_LE_ID = p.SOLDTO_LE_ID.Equals("") ? "null" : p.SOLDTO_LE_ID;
                p.ASSESSABLE_VALUE = p.ASSESSABLE_VALUE.Equals("") ? "null" : p.ASSESSABLE_VALUE;
                p.SHIP_TO_LOCATION_ID = p.SHIP_TO_LOCATION_ID.Equals("") ? "null" : p.SHIP_TO_LOCATION_ID;
                p.REQUISITION_HEADER_ID = p.REQUISITION_HEADER_ID.Equals("") ? "null" : p.REQUISITION_HEADER_ID;
                p.REQUISITION_LINE_ID = p.REQUISITION_LINE_ID.Equals("") ? "null" : p.REQUISITION_LINE_ID;
                p.QUANTITY = p.QUANTITY.Equals("") ? "null" : p.QUANTITY;
                p.QUANTITY_RECEIPT = p.QUANTITY_RECEIPT.Equals("") ? "null" : p.QUANTITY_RECEIPT;
                String last_update_by = "0", creation_by = "0";
                p.TAX_AMOUNT = p.TAX_AMOUNT.Equals("") ? "0" : p.TAX_AMOUNT;
                sql = "Insert Into " + xCPO.table + "(" + xCPO.AGENT_ID + "," + xCPO.APPROVED_DATE + "," + xCPO.APPROVED_FLAG + "," +
                    xCPO.ASSESSABLE_VALUE + "," + xCPO.ATTRIBUTE1 + "," + xCPO.ATTRIBUTE2 + "," +
                    xCPO.ATTRIBUTE3 + "," + xCPO.ATTRIBUTE4 + "," + xCPO.ATTRIBUTE_CATEGORY + "," +
                    xCPO.BILLTO_BU + "," + xCPO.BILLTO_BU_ID + "," + xCPO.BILL_TO_LOCATION_ID + "," +
                    xCPO.BUYER + "," + xCPO.CATEGORY_NM + "," + xCPO.CREATION_DATE + "," +
                    xCPO.CURRENCY_CODE + "," + xCPO.DELIVER_TO_LOC + "," + xCPO.DELIVER_TO_LOC_LINFOX + "," +
                    xCPO.DESTINATION_TYPE + "," + xCPO.DOCUMENT_STATUS + "," + xCPO.EMAIL_ADDRESS + "," +
                    xCPO.GEN_OUTBOUD_FLAG + "," + xCPO.ITEM_DESCRIPTION + "," + xCPO.ITEM_ID + "," +
                    xCPO.LAST_UPDATED_BY + "," + xCPO.LINE_NUM + "," + xCPO.LINE_STATUS + "," +
                    xCPO.LINE_TYPE_ID + "," + xCPO.PAYMENT_TERM + "," + xCPO.PO_HEADER_ID + "," +
                    xCPO.PO_LINE_ID + "," + xCPO.PRC_BU + "," + xCPO.PRC_BU_ID + "," +
                    xCPO.PRODUCT_TYPE + "," + xCPO.QUANTITY + "," + xCPO.QUANTITY_RECEIPT + "," +
                    xCPO.REQUISITION_HEADER_ID + "," + xCPO.REQUISITION_LINE_ID + "," + xCPO.REQ_BU + "," +
                    xCPO.REQ_BU_ID + "," + xCPO.REVISED_DATE + "," + xCPO.REVISION_NUM + "," +
                    xCPO.SEGMENT1 + "," + xCPO.SHIP_TO_LOCATION_ID + "," + xCPO.SOLDTO_LE + "," +
                    xCPO.SOLDTO_LE_ID + "," + xCPO.SUPPLIER_NOTIF_METHOD + "," + xCPO.SUPP_NAME + "," +
                    xCPO.TYPE_LOOKUP_CODE + "," + xCPO.UNIT_PRICE + "," + xCPO.UOM_CODE + "," +
                    xCPO.VENDOR_CONTACT_ID + "," + xCPO.VENDOR_ID + "," + xCPO.VENDOR_SITE + "," +
                    xCPO.TAX_AMOUNT + "," + xCPO.TAX_CODE + "," + xCPO.ACC_SEGMENT1 + "," +
                    xCPO.ACC_SEGMENT2 + "," + xCPO.ACC_SEGMENT3 + "," + xCPO.ACC_SEGMENT4 + "," +
                    xCPO.ACC_SEGMENT5 + "," + xCPO.ACC_SEGMENT6 +

                    ") " +
                    "Values(" + p.AGENT_ID + ",'" + p.APPROVED_DATE + "','" + p.APPROVED_FLAG + "'," +
                    p.ASSESSABLE_VALUE + ",'" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE2 + "','" +
                    p.ATTRIBUTE3 + "','" + p.ATTRIBUTE4 + "','" + p.ATTRIBUTE_CATEGORY + "','" +
                    p.BILLTO_BU + "','" + p.BILLTO_BU_ID + "','" + p.BILL_TO_LOCATION_ID + "','" +
                    p.BUYER + "','" + p.CATEGORY_NM + "','" + p.CREATION_DATE + "','" +
                    p.CURRENCY_CODE + "','" + p.DELIVER_TO_LOC + "','" + p.DELIVER_TO_LOC_LINFOX + "','" +
                    p.DESTINATION_TYPE + "','" + p.DOCUMENT_STATUS + "','" + p.EMAIL_ADDRESS + "','" +
                    p.GEN_OUTBOUD_FLAG + "','" + p.ITEM_DESCRIPTION + "','" + p.ITEM_ID + "','" +
                    p.LAST_UPDATED_BY + "','" + p.LINE_NUM + "','" + p.LINE_STATUS + "','" +
                    p.LINE_TYPE_ID + "','" + p.PAYMENT_TERM + "','" + p.PO_HEADER_ID + "','" +
                    p.PO_LINE_ID + "','" + p.PRC_BU + "','" + p.PRC_BU_ID + "','" +
                    p.PRODUCT_TYPE + "'," + p.QUANTITY + "," + p.QUANTITY_RECEIPT + "," +
                    p.REQUISITION_HEADER_ID + "," + p.REQUISITION_LINE_ID + ",'" + p.REQ_BU + "','" +
                    p.REQ_BU_ID + "','" + p.REVISED_DATE + "','" + p.REVISION_NUM + "','" +
                    p.SEGMENT1 + "'," + p.SHIP_TO_LOCATION_ID + ",'" + p.SOLDTO_LE + "'," +
                    p.SOLDTO_LE_ID + ",'" + p.SUPPLIER_NOTIF_METHOD + "','" + p.SUPP_NAME + "','" +
                    p.TYPE_LOOKUP_CODE + "','" + p.UNIT_PRICE + "','" + p.UOM_CODE + "','" +
                    p.VENDOR_CONTACT_ID + "','" + p.VENDOR_ID + "','" + p.VENDOR_SITE + "','" +
                    p.TAX_AMOUNT + "','" + p.TAX_CODE + "','" + p.ACC_SEGMENT1 + "','" +
                    p.ACC_SEGMENT2 + "','" + p.ACC_SEGMENT3 + "','" + p.ACC_SEGMENT4 + "','" +
                    p.ACC_SEGMENT5 + "','" + p.ACC_SEGMENT6 + "'" +

                    ") ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
                //chk = p.RowNumber;
                //chk = p.Code;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }

            return chk;
        }
    }
}
