using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPoReceiptTblDB
    {
        public XcustPoReceiptTbl xCPoR;
        ConnectDB conn;
        private InitC initC;

        public XcustPoReceiptTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCPoR = new XcustPoReceiptTbl();

            xCPoR.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCPoR.CREATION_DATE = "CREATION_DATE";
            xCPoR.RECEIPT_SOURCE_CODE = "RECEIPT_SOURCE_CODE";
            xCPoR.VENDOR_ID = "VENDOR_ID";
            xCPoR.VENDOR_SITE_ID = "VENDOR_SITE_ID";
            xCPoR.ORGANIZATION_ID = "ORGANIZATION_ID";
            xCPoR.SHIPMENT_NUM = "SHIPMENT_NUM";
            xCPoR.RECEIPT_NUM = "RECEIPT_NUM";
            xCPoR.SHIP_TO_LOCATION_ID = "SHIP_TO_LOCATION_ID";
            xCPoR.PACKING_SLIP = "PACKING_SLIP";

            xCPoR.SHIPPED_DATE = "SHIPPED_DATE";
            xCPoR.EXPECTED_RECEIPT_DATE = "EXPECTED_RECEIPT_DATE";
            xCPoR.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCPoR.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPoR.ATTRIBUTE2 = "ATTRIBUTE2";
            xCPoR.ATTRIBUTE3 = "ATTRIBUTE3";
            xCPoR.ATTRIBUTE4 = "ATTRIBUTE4";
            xCPoR.ATTRIBUTE5 = "ATTRIBUTE5";
            xCPoR.ATTRIBUTE6 = "ATTRIBUTE6";
            xCPoR.ATTRIBUTE7 = "ATTRIBUTE7";

            xCPoR.ATTRIBUTE8 = "ATTRIBUTE8";
            xCPoR.ATTRIBUTE9 = "ATTRIBUTE9";
            xCPoR.ATTRIBUTE10 = "ATTRIBUTE10";
            xCPoR.REQUEST_ID = "REQUEST_ID";
            xCPoR.GROSS_WEIGHT = "GROSS_WEIGHT";
            xCPoR.GROSS_WEIGHT_UOM_CODE = "GROSS_WEIGHT_UOM_CODE";
            xCPoR.NET_WEIGHT = "NET_WEIGHT";
            xCPoR.NET_WEIGHT_UOM_CODE = "NET_WEIGHT_UOM_CODE";
            xCPoR.PACKAGING_CODE = "PACKAGING_CODE";
            xCPoR.INVOICE_NUM = "INVOICE_NUM";

            xCPoR.INVOICE_DATE = "INVOICE_DATE";
            xCPoR.INVOICE_AMOUNT = "INVOICE_AMOUNT";
            xCPoR.TAX_NAME = "TAX_NAME";
            xCPoR.TAX_AMOUNT = "TAX_AMOUNT";
            xCPoR.FREIGHT_AMOUNT = "FREIGHT_AMOUNT";
            xCPoR.INVOICE_STATUS_CODE = "INVOICE_STATUS_CODE";
            xCPoR.CURRENCY_CODE = "CURRENCY_CODE";
            xCPoR.CONVERSION_RATE_TYPE = "CONVERSION_RATE_TYPE";
            xCPoR.CONVERSION_RATE = "CONVERSION_RATE";
            xCPoR.CONVERSION_DATE = "CONVERSION_DATE";

            xCPoR.PAYMENT_TERMS_ID = "PAYMENT_TERMS_ID";
            xCPoR.SHIP_TO_ORG_ID = "SHIP_TO_ORG_ID";
            xCPoR.CUSTOMER_ID = "CUSTOMER_ID";
            xCPoR.CUSTOMER_SITE_ID = "CUSTOMER_SITE_ID";
            xCPoR.REMIT_TO_SITE_ID = "REMIT_TO_SITE_ID";
            xCPoR.SHIP_FROM_LOCATION_ID = "SHIP_FROM_LOCATION_ID";
            xCPoR.APPROVAL_STATUS = "APPROVAL_STATUS";
            xCPoR.RMA_BU_ID = "RMA_BU_ID";
            xCPoR.HEADER_INTERFACE_ID = "HEADER_INTERFACE_ID";

            xCPoR.RA_ORIG_SYSTEM_REF = "RA_ORIG_SYSTEM_REF";
            xCPoR.SHIPMENT_LINE_ID = "SHIPMENT_LINE_ID";
            xCPoR.LINE_NUM = "LINE_NUM";
            xCPoR.CATEGORY_ID = "CATEGORY_ID";
            xCPoR.QUANTITY_SHIPPED = "QUANTITY_SHIPPED";
            xCPoR.QUANTITY_RECEIVED = "QUANTITY_RECEIVED";
            xCPoR.QUANTITY_DELIVERED = "QUANTITY_DELIVERED";
            xCPoR.QUANTITY_RETURNED = "QUANTITY_RETURNED";
            xCPoR.QUANTITY_ACCEPTED = "QUANTITY_ACCEPTED";
            xCPoR.QUANTITY_REJECTED = "QUANTITY_REJECTED";

            xCPoR.UOM_CODE = "UOM_CODE";
            xCPoR.ITEM_DESCRIPTION = "ITEM_DESCRIPTION";
            xCPoR.ITEM_ID = "ITEM_ID";
            xCPoR.ITEM_REVISION = "ITEM_REVISION";
            xCPoR.SHIPMENT_LINE_STATUS_CODE = "SHIPMENT_LINE_STATUS_CODE";
            xCPoR.SOURCE_DOCUMENT_CODE = "SOURCE_DOCUMENT_CODE";
            xCPoR.PO_HEADER_ID = "PO_HEADER_ID";
            xCPoR.PO_LINE_ID = "PO_LINE_ID";
            xCPoR.PO_LINE_LOCATION_ID = "PO_LINE_LOCATION_ID";
            xCPoR.PO_DISTRIBUTION_ID = "PO_DISTRIBUTION_ID";

            xCPoR.REQUISITION_LINE_ID = "REQUISITION_LINE_ID";
            xCPoR.REQ_DISTRIBUTION_ID = "REQ_DISTRIBUTION_ID";
            xCPoR.FROM_ORGANIZATION_ID = "FROM_ORGANIZATION_ID";
            xCPoR.DESTINATION_TYPE_CODE = "DESTINATION_TYPE_CODE";
            xCPoR.TO_ORGANIZATION_ID = "TO_ORGANIZATION_ID";
            xCPoR.TO_SUBINVENTORY = "TO_SUBINVENTORY";
            xCPoR.LOCATOR_ID = "LOCATOR_ID";
            xCPoR.DELIVER_TO_LOCATION_ID = "DELIVER_TO_LOCATION_ID";
            xCPoR.SHIPMENT_UNIT_PRICE = "SHIPMENT_UNIT_PRICE";
            xCPoR.TRANSFER_COST = "TRANSFER_COST";

            xCPoR.TRANSPORTATION_COST = "TRANSPORTATION_COST";
            xCPoR.ATTRIBUTE_CATEGORY_L = "ATTRIBUTE_CATEGORY_L";
            xCPoR.ATTRIBUTE1_L = "ATTRIBUTE1_L";
            xCPoR.ATTRIBUTE2_L = "ATTRIBUTE2_L";
            xCPoR.ATTRIBUTE3_L = "ATTRIBUTE3_L";
            xCPoR.ATTRIBUTE4_L = "ATTRIBUTE4_L";
            xCPoR.ATTRIBUTE5_L = "ATTRIBUTE5_L";
            xCPoR.ATTRIBUTE6_L = "ATTRIBUTE6_L";
            xCPoR.ATTRIBUTE7_L = "ATTRIBUTE7_L";
            xCPoR.ATTRIBUTE8_L = "ATTRIBUTE8_L";

            xCPoR.ATTRIBUTE9_L = "ATTRIBUTE9_L";
            xCPoR.ATTRIBUTE10_L = "ATTRIBUTE10_L";
            xCPoR.ATTRIBUTE_NUMBER1_L = "ATTRIBUTE_NUMBER1_L";
            xCPoR.ATTRIBUTE_NUMBER2_L = "ATTRIBUTE_NUMBER2_L";
            xCPoR.ATTRIBUTE_NUMBER3_L = "ATTRIBUTE_NUMBER3_L";
            xCPoR.ATTRIBUTE_NUMBER4_L = "ATTRIBUTE_NUMBER4_L";
            xCPoR.ATTRIBUTE_NUMBER5_L = "ATTRIBUTE_NUMBER5_L";
            xCPoR.ATTRIBUTE_NUMBER6_L = "ATTRIBUTE_NUMBER6_L";
            xCPoR.ATTRIBUTE_NUMBER7_L = "ATTRIBUTE_NUMBER7_L";
            xCPoR.ATTRIBUTE_NUMBER8_L = "ATTRIBUTE_NUMBER8_L";

            xCPoR.ATTRIBUTE_NUMBER9_L = "ATTRIBUTE_NUMBER9_L";
            xCPoR.ATTRIBUTE_NUMBER10_L = "ATTRIBUTE_NUMBER10_L";
            xCPoR.ATTRIBUTE_DATE1_L = "ATTRIBUTE_DATE1_L";
            xCPoR.ATTRIBUTE_DATE2_L = "ATTRIBUTE_DATE2_L";
            xCPoR.ATTRIBUTE_DATE3_L = "ATTRIBUTE_DATE3_L";
            xCPoR.ATTRIBUTE_DATE4_L = "ATTRIBUTE_DATE4_L";
            xCPoR.REASON_ID = "REASON_ID";
            xCPoR.REQUEST_ID_L = "REQUEST_ID_L";
            xCPoR.DESTINATION_CONTEXT = "DESTINATION_CONTEXT";
            xCPoR.PRIMARY_UOM_CODE = "PRIMARY_UOM_CODE";

            xCPoR.TAX_NAME_L = "TAX_NAME_L";
            xCPoR.TAX_AMOUNT_L = "TAX_AMOUNT_L";
            xCPoR.INVOICE_STATUS_CODE_L = "INVOICE_STATUS_CODE_L";
            xCPoR.SHIP_TO_LOCATION_ID_L = "SHIP_TO_LOCATION_ID_L";
            xCPoR.SECONDARY_QUANTITY_SHIPPED = "SECONDARY_QUANTITY_SHIPPED";
            xCPoR.SECONDARY_QUANTITY_RECEIVED = "SECONDARY_QUANTITY_RECEIVED";
            xCPoR.SECONDARY_UOM_CODE = "SECONDARY_UOM_CODE";
            xCPoR.MMT_TRANSACTION_ID = "MMT_TRANSACTION_ID";
            xCPoR.AMOUNT = "AMOUNT";
            xCPoR.AMOUNT_RECEIVED = "AMOUNT_RECEIVED";

            xCPoR.ATTRIBUTE_DATE5_L = "ATTRIBUTE_DATE5_L";
            xCPoR.LOT_NUMBER = "LOT_NUMBER";

            xCPoR.table = "XCUST_PO_RECEIPT_TBL";
        }
        public Boolean selectDupPk(String po_header_id, String po_line_id)
        {
            String sql = "";
            Boolean chk = false;
            DataTable dt = new DataTable();
            sql = "Select count(1) as cnt From " + xCPoR.table + " Where " + xCPoR.PO_HEADER_ID + "='" + po_header_id + "' and " + xCPoR.PO_LINE_ID + "='" + po_line_id + "'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count >= 1)
            {
                chk = true;
            }
            return chk;
        }
        public void deletexCPoR(String po_header_id, String po_line_id, String pathLog)
        {
            String sql = "Delete From " + xCPoR.table + " Where " + xCPoR.PO_HEADER_ID + "='" + po_header_id + "' and " + xCPoR.PO_LINE_ID + "='" + po_line_id + "'";
            conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
        }
        public String insertxCPoR(XcustPoReceiptTbl p, String pathLog)
        {
            String sql = "", chk = "";
            if (selectDupPk(p.PO_HEADER_ID, p.PO_LINE_ID))
            {
                deletexCPoR(p.PO_HEADER_ID, p.PO_LINE_ID, pathLog);
            }
            chk = insert(p, pathLog);
            return chk;
        }
        public String insert(XcustPoReceiptTbl p, String pathLog)
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
                String last_update_by = "0", creation_by = "0";
                p.TAX_AMOUNT = p.TAX_AMOUNT.Equals("") ? "0" : p.TAX_AMOUNT;
                sql = "Insert Into " + xCPoR.table + "(" + xCPoR.LAST_UPDATE_DATE + "," + xCPoR.CREATION_DATE + "," + xCPoR.RECEIPT_SOURCE_CODE + "," +
                    xCPoR.VENDOR_ID + "," + xCPoR.VENDOR_SITE_ID + "," + xCPoR.ORGANIZATION_ID + "," +
                    xCPoR.SHIPMENT_NUM + "," + xCPoR.RECEIPT_NUM + "," + xCPoR.SHIP_TO_LOCATION_ID + "," +
                    xCPoR.PACKING_SLIP + "," + xCPoR.SHIPPED_DATE + "," + xCPoR.EXPECTED_RECEIPT_DATE + "," +
                    xCPoR.ATTRIBUTE_CATEGORY + "," + xCPoR.ATTRIBUTE1 + "," + xCPoR.ATTRIBUTE2 + "," +
                    xCPoR.ATTRIBUTE3 + "," + xCPoR.ATTRIBUTE4 + "," + xCPoR.ATTRIBUTE5 + "," +
                    xCPoR.ATTRIBUTE6 + "," + xCPoR.ATTRIBUTE7 + "," + xCPoR.ATTRIBUTE8 + "," +
                    xCPoR.ATTRIBUTE9 + "," + xCPoR.ATTRIBUTE10 + "," + xCPoR.REQUEST_ID + "," +
                    xCPoR.GROSS_WEIGHT + "," + xCPoR.GROSS_WEIGHT_UOM_CODE + "," + xCPoR.NET_WEIGHT + "," +
                    xCPoR.NET_WEIGHT_UOM_CODE + "," + xCPoR.PACKAGING_CODE + "," + xCPoR.INVOICE_NUM + "," +
                    xCPoR.INVOICE_DATE + "," + xCPoR.INVOICE_AMOUNT + "," + xCPoR.TAX_NAME + "," +
                    xCPoR.TAX_AMOUNT + "," + xCPoR.FREIGHT_AMOUNT + "," + xCPoR.INVOICE_STATUS_CODE + "," +
                    xCPoR.CURRENCY_CODE + "," + xCPoR.CONVERSION_RATE_TYPE + "," + xCPoR.CONVERSION_RATE + "," +
                    xCPoR.CONVERSION_DATE + "," + xCPoR.PAYMENT_TERMS_ID + "," + xCPoR.SHIP_TO_ORG_ID + "," +
                    xCPoR.CUSTOMER_ID + "," + xCPoR.CUSTOMER_SITE_ID + "," + xCPoR.REMIT_TO_SITE_ID + "," +
                    xCPoR.SHIP_FROM_LOCATION_ID + "," + xCPoR.APPROVAL_STATUS + "," + xCPoR.RMA_BU_ID + "," +
                    xCPoR.HEADER_INTERFACE_ID + "," + xCPoR.RA_ORIG_SYSTEM_REF + "," + xCPoR.SHIPMENT_LINE_ID + "," +
                    xCPoR.LINE_NUM + "," + xCPoR.CATEGORY_ID + "," + xCPoR.QUANTITY_SHIPPED + "," +
                    xCPoR.QUANTITY_RECEIVED + "," + xCPoR.QUANTITY_DELIVERED + "," + xCPoR.QUANTITY_RETURNED + "," +
                    xCPoR.QUANTITY_ACCEPTED + "," + xCPoR.QUANTITY_REJECTED + "," + xCPoR.UOM_CODE + "," +
                    xCPoR.ITEM_DESCRIPTION + "," + xCPoR.ITEM_ID + "," + xCPoR.ITEM_REVISION + "," +
                    xCPoR.SHIPMENT_LINE_STATUS_CODE + "," + xCPoR.SOURCE_DOCUMENT_CODE + "," + xCPoR.PO_HEADER_ID + "," +
                    xCPoR.PO_LINE_ID + "," + xCPoR.PO_LINE_LOCATION_ID + "," + xCPoR.PO_DISTRIBUTION_ID + "," +
                    xCPoR.REQUISITION_LINE_ID + "," + xCPoR.REQ_DISTRIBUTION_ID + "," + xCPoR.FROM_ORGANIZATION_ID + "," +
                    xCPoR.DESTINATION_TYPE_CODE + "," + xCPoR.TO_ORGANIZATION_ID + "," + xCPoR.TO_SUBINVENTORY + "," +
                    xCPoR.LOCATOR_ID + "," + xCPoR.DELIVER_TO_LOCATION_ID + "," + xCPoR.SHIPMENT_UNIT_PRICE + "," +
                    xCPoR.TRANSFER_COST + "," + xCPoR.TRANSPORTATION_COST + "," + xCPoR.ATTRIBUTE_CATEGORY_L + "," +
                    xCPoR.ATTRIBUTE1_L + "," + xCPoR.ATTRIBUTE2_L + "," + xCPoR.ATTRIBUTE3_L + "," +
                    xCPoR.ATTRIBUTE4_L + "," + xCPoR.ATTRIBUTE5_L + "," + xCPoR.ATTRIBUTE6_L + "," +
                    xCPoR.ATTRIBUTE7_L + "," + xCPoR.ATTRIBUTE8_L + "," + xCPoR.ATTRIBUTE9_L + "," +
                    xCPoR.ATTRIBUTE10_L + "," + xCPoR.ATTRIBUTE_NUMBER1_L + "," + xCPoR.ATTRIBUTE_NUMBER2_L + "," +
                    xCPoR.ATTRIBUTE_NUMBER3_L + "," + xCPoR.ATTRIBUTE_NUMBER4_L + "," + xCPoR.ATTRIBUTE_NUMBER5_L + "," +
                    xCPoR.ATTRIBUTE_NUMBER6_L + "," + xCPoR.ATTRIBUTE_NUMBER7_L + "," + xCPoR.ATTRIBUTE_NUMBER8_L + "," +
                    xCPoR.ATTRIBUTE_NUMBER9_L + "," + xCPoR.ATTRIBUTE_NUMBER10_L + "," + xCPoR.ATTRIBUTE_DATE1_L + "," +
                    xCPoR.ATTRIBUTE_DATE2_L + "," + xCPoR.ATTRIBUTE_DATE3_L + "," + xCPoR.ATTRIBUTE_DATE4_L + "," +
                    xCPoR.REASON_ID + "," + xCPoR.REQUEST_ID_L + "," + xCPoR.DESTINATION_CONTEXT + "," +
                    xCPoR.PRIMARY_UOM_CODE + "," + xCPoR.TAX_NAME_L + "," + xCPoR.TAX_AMOUNT_L + "," +
                    xCPoR.INVOICE_STATUS_CODE_L + "," + xCPoR.SHIP_TO_LOCATION_ID_L + "," + xCPoR.SECONDARY_QUANTITY_SHIPPED + "," +
                    xCPoR.SECONDARY_QUANTITY_RECEIVED + "," + xCPoR.SECONDARY_UOM_CODE + "," + xCPoR.MMT_TRANSACTION_ID + "," +
                    xCPoR.AMOUNT + "," + xCPoR.AMOUNT_RECEIVED + "," + xCPoR.ATTRIBUTE_DATE5_L + "," +
                    xCPoR.LOT_NUMBER + " " +

                    ") " +
                    "Values('" + p.LAST_UPDATE_DATE + "','" + p.CREATION_DATE + "','" + p.RECEIPT_SOURCE_CODE + "','" +
                    p.VENDOR_ID + "','" + p.VENDOR_SITE_ID + "','" + p.ORGANIZATION_ID + "','" +
                    p.SHIPMENT_NUM + "','" + p.RECEIPT_NUM + "','" + p.SHIP_TO_LOCATION_ID + "','" +
                    p.PACKING_SLIP + "','" + p.SHIPPED_DATE + "','" + p.EXPECTED_RECEIPT_DATE + "','" +
                    p.ATTRIBUTE_CATEGORY + "','" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE2 + "','" +
                    p.ATTRIBUTE3 + "','" + p.ATTRIBUTE4 + "','" + p.ATTRIBUTE5 + "','" +
                    p.ATTRIBUTE6 + "','" + p.ATTRIBUTE7 + "','" + p.ATTRIBUTE8 + "','" +
                    p.ATTRIBUTE9 + "','" + p.ATTRIBUTE10 + "','" + p.REQUEST_ID + "','" +
                    p.GROSS_WEIGHT + "','" + p.GROSS_WEIGHT_UOM_CODE + "','" + p.NET_WEIGHT + "','" +
                    p.NET_WEIGHT_UOM_CODE + "','" + p.PACKAGING_CODE + "','" + p.INVOICE_NUM + "','" +
                    p.INVOICE_DATE + "','" + p.INVOICE_AMOUNT + "','" + p.TAX_NAME + "','" +
                    p.TAX_AMOUNT + "','" + p.FREIGHT_AMOUNT + "','" + p.INVOICE_STATUS_CODE + "','" +
                    p.CURRENCY_CODE + "','" + p.CONVERSION_RATE_TYPE + "','" + p.CONVERSION_RATE + "','" +
                    p.CONVERSION_DATE + "','" + p.PAYMENT_TERMS_ID + "','" + p.SHIP_TO_ORG_ID + "','" +
                    p.CUSTOMER_ID + "','" + p.CUSTOMER_SITE_ID + "','" + p.REMIT_TO_SITE_ID + "','" +
                    p.SHIP_FROM_LOCATION_ID + "','" + p.APPROVAL_STATUS + "','" + p.RMA_BU_ID + "','" +
                    p.HEADER_INTERFACE_ID + "','" + p.RA_ORIG_SYSTEM_REF + "','" + p.SHIPMENT_LINE_ID + "','" +
                    p.LINE_NUM + "','" + p.CATEGORY_ID + "','" + p.QUANTITY_SHIPPED + "','" +
                    p.QUANTITY_RECEIVED + "','" + p.QUANTITY_DELIVERED + "','" + p.QUANTITY_RETURNED + "','" +
                    p.QUANTITY_ACCEPTED + "','" + p.QUANTITY_REJECTED + "','" + p.UOM_CODE + "','" +
                    p.ITEM_DESCRIPTION + "','" + p.ITEM_ID + "','" + p.ITEM_REVISION + "','" +
                    p.SHIPMENT_LINE_STATUS_CODE + "','" + p.SOURCE_DOCUMENT_CODE + "','" + p.PO_HEADER_ID + "','" +
                    p.PO_LINE_ID + "','" + p.PO_LINE_LOCATION_ID + "','" + p.PO_DISTRIBUTION_ID + "','" +
                    p.REQUISITION_LINE_ID + "','" + p.REQ_DISTRIBUTION_ID + "','" + p.FROM_ORGANIZATION_ID + "','" +
                    p.DESTINATION_TYPE_CODE + "','" + p.TO_ORGANIZATION_ID + "','" + p.TO_SUBINVENTORY + "','" +
                    p.LOCATOR_ID + "','" + p.DELIVER_TO_LOCATION_ID + "','" + p.SHIPMENT_UNIT_PRICE + "','" +
                    p.TRANSFER_COST + "','" + p.TRANSPORTATION_COST + "','" + p.ATTRIBUTE_CATEGORY_L + "','" +
                    p.ATTRIBUTE1_L + "','" + p.ATTRIBUTE2_L + "','" + p.ATTRIBUTE3_L + "','" +
                    p.ATTRIBUTE4_L + "','" + p.ATTRIBUTE5_L + "','" + p.ATTRIBUTE6_L + "','" +
                    p.ATTRIBUTE7_L + "','" + p.ATTRIBUTE8_L + "','" + p.ATTRIBUTE9_L + "','" +
                    p.ATTRIBUTE10_L + "','" + p.ATTRIBUTE_NUMBER1_L + "','" + p.ATTRIBUTE_NUMBER2_L + "','" +
                    p.ATTRIBUTE_NUMBER3_L + "','" + p.ATTRIBUTE_NUMBER4_L + "','" + p.ATTRIBUTE_NUMBER5_L + "','" +
                    p.ATTRIBUTE_NUMBER6_L + "','" + p.ATTRIBUTE_NUMBER7_L + "','" + p.ATTRIBUTE_NUMBER8_L + "','" +
                    p.ATTRIBUTE_NUMBER9_L + "','" + p.ATTRIBUTE_NUMBER10_L + "','" + p.ATTRIBUTE_DATE1_L + "','" +
                    p.ATTRIBUTE_DATE2_L + "','" + p.ATTRIBUTE_DATE3_L + "','" + p.ATTRIBUTE_DATE4_L + "','" +
                    p.REASON_ID + "','" + p.REQUEST_ID_L + "','" + p.DESTINATION_CONTEXT + "','" +
                    p.PRIMARY_UOM_CODE + "','" + p.TAX_NAME_L + "','" + p.TAX_AMOUNT_L + "','" +
                    p.INVOICE_STATUS_CODE_L + "','" + p.SHIP_TO_LOCATION_ID_L + "','" + p.SECONDARY_QUANTITY_SHIPPED + "','" +
                    p.SECONDARY_QUANTITY_RECEIVED + "','" + p.SECONDARY_UOM_CODE + "','" + p.MMT_TRANSACTION_ID + "','" +
                    p.AMOUNT + "','" + p.AMOUNT_RECEIVED + "','" + p.ATTRIBUTE_DATE5_L + "','" +
                    p.LOT_NUMBER + "'" +

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
