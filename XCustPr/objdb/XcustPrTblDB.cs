using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPrTblDB
    {
        public XcustPrTbl xCPR;
        ConnectDB conn;
        private InitC initC;

        public XcustPrTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCPR = new XcustPrTbl();
            xCPR.AMOUNT = "AMOUNT";
            xCPR.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPR.ATTRIBUTE10 = "ATTRIBUTE10";
            xCPR.ATTRIBUTE1_L = "ATTRIBUTE1_L";
            xCPR.ATTRIBUTE2 = "ATTRIBUTE2";
            xCPR.ATTRIBUTE2_L = "ATTRIBUTE2_L";
            xCPR.ATTRIBUTE3 = "ATTRIBUTE3";
            xCPR.ATTRIBUTE3_L = "ATTRIBUTE3_L";
            xCPR.ATTRIBUTE4 = "ATTRIBUTE4";
            xCPR.ATTRIBUTE5 = "ATTRIBUTE5";
            xCPR.ATTRIBUTE6 = "ATTRIBUTE6";
            xCPR.ATTRIBUTE7 = "ATTRIBUTE7";
            xCPR.ATTRIBUTE8 = "ATTRIBUTE8";
            xCPR.ATTRIBUTE9 = "ATTRIBUTE9";
            xCPR.ATTRIBUTE10 = "ATTRIBUTE10";
            xCPR.ATTRIBUTE11 = "ATTRIBUTE11";
            xCPR.ATTRIBUTE12 = "ATTRIBUTE12";            
            xCPR.ATTRIBUTE13 = "ATTRIBUTE13";            
            xCPR.ATTRIBUTE14 = "ATTRIBUTE14";
            xCPR.ATTRIBUTE15 = "ATTRIBUTE15";
            xCPR.ATTRIBUTE16 = "ATTRIBUTE16";
            xCPR.ATTRIBUTE17 = "ATTRIBUTE17";
            xCPR.ATTRIBUTE18 = "ATTRIBUTE18";
            xCPR.ATTRIBUTE19 = "ATTRIBUTE19";
            xCPR.ATTRIBUTE20 = "ATTRIBUTE20";
            xCPR.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCPR.ATTRIBUTE_CATEGORY_L = "ATTRIBUTE_CATEGORY_L";
            xCPR.BUDGET_DATE = "BUDGET_DATE";
            xCPR.CHARGE_ACCOUNT = "CHARGE_ACCOUNT";
            xCPR.CONVERSION_DATE = "CONVERSION_DATE";
            xCPR.CREATED_BY = "CREATED_BY";
            xCPR.CREATION_DATE = "CREATION_DATE";
            xCPR.CURRENCY_AMOUNT = "CURRENCY_AMOUNT";
            xCPR.DESCRIPTION = "DESCRIPTION";
            xCPR.DESTINATION_ORGANIZATION_ID = "DESTINATION_ORGANIZATION_ID";
            xCPR.DESTINATION_TYPE_CODE = "DESTINATION_TYPE_CODE";
            xCPR.DISTRIBUTION = "DISTRIBUTION";
            xCPR.DISTRIBUTION_CURRENCY_AMOUNT = "DISTRIBUTION_CURRENCY_AMOUNT";
            xCPR.DOCUMENT_STATUS = "DOCUMENT_STATUS";
            xCPR.FUNDS_STATUS = "FUNDS_STATUS";
            xCPR.ITEM_DESCRIPTION = "ITEM_DESCRIPTION";
            xCPR.ITEM_ID = "ITEM_ID";
            xCPR.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCPR.LINE_NUMBER = "LINE_NUMBER";
            xCPR.LINE_TYPE = "LINE_TYPE";
            xCPR.LOCATION = "LOCATION";
            xCPR.NAME = "NAME";
            xCPR.PERCENT = "PERCENT1";
            
            xCPR.PO_ORDER = "PO_ORDER";
            xCPR.REQUISITION_HEADER_ID = "REQUISITION_HEADER_ID";
            xCPR.REQUISITION_LINE_ID = "REQUISITION_LINE_ID";
            xCPR.REQUISITION_NUMBER = "REQUISITION_NUMBER";
            xCPR.REQ_BU_ID = "REQ_BU_ID";
            xCPR.SECONDARY_QUANTITY = "SECONDARY_QUANTITY";
            xCPR.SECONDARY_UOM_CODE = "SECONDARY_UOM_CODE";
            xCPR.SOURCE_TYPE_CODE = "SOURCE_TYPE_CODE";
            xCPR.UNIT_PRICE = "UNIT_PRICE";
            xCPR.UOM_CODE = "UOM_CODE";
            xCPR.VENDOR_ID = "VENDOR_ID";
            xCPR.VENDOR_SITE_ID = "VENDOR_SITE_ID";

            xCPR.table = "xcust_PR_TBL";
        }
        public Boolean selectDupPk(String requisition_header_id,String requisition_line_id)
        {
            String sql = "";
            Boolean chk = false;
            DataTable dt = new DataTable();
            sql = "Select count(1) as cnt From "+ xCPR.table +" Where "+ xCPR.REQUISITION_HEADER_ID+"='"+requisition_header_id+"' and "+ xCPR.REQUISITION_LINE_ID+"='"+requisition_header_id+"'";
            dt = conn.selectData(sql, "kfc_po");
            if (dt.Rows.Count >= 1)
            {
                chk = true;
            }
            return chk;
        }
        public DataTable selectPO007FixLen()
        {
            DataTable dt = new DataTable();
            String sql = "Select * From XCUST_FIX_LENGTH_TBL Where CUSTOMIZATION_NAME = 'PO007' Order By X_SEQ ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPO006FixLen()
        {
            DataTable dt = new DataTable();
            String sql = "Select * From XCUST_FIX_LENGTH_TBL Where CUSTOMIZATION_NAME = 'PO006' Order By X_SEQ ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }

        public DataTable selectPRPO(String linfox_po_number, String linfox_po_line_number)
        {
            DataTable dt = new DataTable();
            String sql = "SELECT po.PO_HEADER_ID, po.PO_LINE_ID,po.SEGMENT1 po_number,po.LINE_NUM po_line_number, po.QUANTITY/*, po.header_id*/ "+
                "From xcust_pr_tbl PR "+
                "Left Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                "Where  "+                ""+
                " PR.ATTRIBUTE1_L = '"+ linfox_po_number + "' "+
                "and PR.ATTRIBUTE2_L = '"+ linfox_po_line_number + "' ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO007()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID" +
                ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM" +
                ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID " +
                "From xcust_pr_tbl PR " +
                "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                "Where  " + "" +
                " PR.ATTRIBUTE1 <> 'MMX'  ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        /*
         * 1.5.1.1	เป็น  PO ที่เกิดจาก Direct supplier โดยดูจาก Attribute1 ของ PR ต้องไม่เป็นรายการที่เป็น "MMX"
         * join XCUST_SUPPLIER_MST_TBL.VENDOR_ID
         * where ATTRIBUTE1 = 'Y'
         * 1.1.1.1	เป็น PO ที่อนุมัติแล้ว
         * 
         */
        public DataTable selectPRPO006GroupByVendor()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT po.VENDOR_ID, po.acc_segment1 as deliveryDate " +
                "From xcust_pr_tbl PR " +
                "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                "Where  " + "" +
                " PR.ATTRIBUTE1 <> 'MMX' group by po.VENDOR_ID, po.acc_segment1 ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO006(String VENDOR_ID)
        {
            DataTable dt = new DataTable();
            String sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID" +
                ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM" +
                ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID " +
                "From xcust_pr_tbl PR " +
                "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                "Where  " + "" +
                " PR.ATTRIBUTE1 <> 'MMX'  ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String selectItemCode(String item_id, String prc_bu_id)
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "select msi.ITEM_CODE "+
                            "from xcust_item_mst_tbl msi " +
                            ",xcust_organization_mst_tbl org " +
                            ", XCUST_BU_MST_TBL bu " +
                            " where " +
                            "/*msi.INVENTORY_ITEM_ID   = 300000001619857 "+
                            "and*/ msi.ORGAINZATION_ID = org.ORGANIZATION_ID "+
                            "and bu.PRIMARY_LEDGER_ID = org.SET_OF_BOOKS_ID " +
                            "and msi.INVENTORY_ITEM_ID = '"+ item_id + "' " +
                            "and bu.BU_ID = '"+ prc_bu_id + "'  ";
            dt = conn.selectData(sql, "kfc_po");
            chk = dt.Rows.Count > 0 ? dt.Rows[0]["ITEM_CODE"].ToString() : "";
            return chk;
        }
        public DataTable selectLot(String po_header_id, String po_line_id)
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "select receipt_num,line_num,lot_number "+
                        "FROM XCUST_PO_RECEIPT_TBL PO_RCP " +
                        "where PO_RCP.PO_HEADER_ID = '"+po_header_id +"' "+
                        "and PO_RCP.PO_LINE_ID = '"+po_line_id  +"' ";
            dt = conn.selectData(sql, "kfc_po");
            
            return dt;
        }
        public void deletexCPR(String requisition_header_id, String requisition_line_id)
        {
            String sql = "Delete From "+xCPR.table+ " Where " + xCPR.REQUISITION_HEADER_ID + "='" + requisition_header_id + "' and " + xCPR.REQUISITION_LINE_ID + "='" + requisition_line_id + "'";
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public String insertxCPR(XcustPrTbl p)
        {
            String sql = "", chk="";
            if (selectDupPk(p.REQUISITION_HEADER_ID, p.REQUISITION_LINE_ID))
            {
                deletexCPR(p.REQUISITION_HEADER_ID, p.REQUISITION_LINE_ID);
            }
            chk = insert(p);
            return chk;
        }
        private String insert(XcustPrTbl p)
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
                p.AMOUNT = p.AMOUNT.Equals("") ? "0" : p.AMOUNT;
                p.CURRENCY_AMOUNT = p.CURRENCY_AMOUNT.Equals("") ? "0" : p.CURRENCY_AMOUNT;
                p.SECONDARY_QUANTITY = p.SECONDARY_QUANTITY.Equals("") ? "0" : p.SECONDARY_QUANTITY;
                sql = "Insert Into " + xCPR.table + "(" + xCPR.AMOUNT + "," + xCPR.ATTRIBUTE1 + "," + xCPR.ATTRIBUTE10 + "," +
                    xCPR.ATTRIBUTE1_L + "," + xCPR.ATTRIBUTE2 + "," + xCPR.ATTRIBUTE2_L + "," +
                    xCPR.ATTRIBUTE3 + "," + xCPR.ATTRIBUTE3_L + "," + xCPR.ATTRIBUTE4 + "," +
                    xCPR.ATTRIBUTE5 + "," + xCPR.ATTRIBUTE6 + "," + xCPR.ATTRIBUTE7 + "," +
                    xCPR.ATTRIBUTE8 + "," + xCPR.ATTRIBUTE9 + "," +
                    xCPR.ATTRIBUTE11 + "," + xCPR.ATTRIBUTE12 + "," + xCPR.ATTRIBUTE13 + "," +
                    xCPR.ATTRIBUTE14 + "," + xCPR.ATTRIBUTE15 + "," + xCPR.ATTRIBUTE16 + "," +
                    xCPR.ATTRIBUTE17 + "," + xCPR.ATTRIBUTE18 + "," + xCPR.ATTRIBUTE19 + "," +
                    xCPR.ATTRIBUTE20 + "," + xCPR.ATTRIBUTE_CATEGORY + "," + xCPR.ATTRIBUTE_CATEGORY_L + "," +
                    xCPR.BUDGET_DATE + "," + xCPR.CHARGE_ACCOUNT + "," + xCPR.CONVERSION_DATE + "," +
                    xCPR.CREATED_BY + "," + xCPR.CREATION_DATE + "," + xCPR.CURRENCY_AMOUNT + "," +
                    xCPR.DESCRIPTION + "," + xCPR.DESTINATION_ORGANIZATION_ID + "," + xCPR.DESTINATION_TYPE_CODE + "," +
                    xCPR.DISTRIBUTION + "," + xCPR.DISTRIBUTION_CURRENCY_AMOUNT + "," + xCPR.DOCUMENT_STATUS + "," +
                    xCPR.FUNDS_STATUS + "," + xCPR.ITEM_DESCRIPTION + "," + xCPR.ITEM_ID + "," +
                    xCPR.LAST_UPDATE_DATE + "," + xCPR.LINE_NUMBER + "," + xCPR.LINE_TYPE + "," +
                    xCPR.LOCATION + "," + xCPR.NAME + "," + xCPR.PERCENT + "," +
                    xCPR.PO_ORDER + "," + xCPR.REQUISITION_HEADER_ID + "," + xCPR.REQUISITION_LINE_ID + "," +
                    xCPR.REQUISITION_NUMBER + "," + xCPR.REQ_BU_ID + "," + xCPR.SECONDARY_QUANTITY + "," +
                    xCPR.SECONDARY_UOM_CODE + "," + xCPR.SOURCE_TYPE_CODE + "," + xCPR.UNIT_PRICE + "," +
                    xCPR.UOM_CODE + "," + xCPR.VENDOR_ID + "," + xCPR.VENDOR_SITE_ID +

                    ") " +
                    "Values(" + p.AMOUNT + ",'" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE10 + "','" +
                    p.ATTRIBUTE1_L + "','" + p.ATTRIBUTE2 + "','" + p.ATTRIBUTE2_L + "','" +
                    p.ATTRIBUTE3 + "','" + p.ATTRIBUTE3_L + "','" + p.ATTRIBUTE4 + "','" +
                    p.ATTRIBUTE5 + "','" + p.ATTRIBUTE6 + "','" + p.ATTRIBUTE7 + "','" +
                    p.ATTRIBUTE8 + "','" + p.ATTRIBUTE9 + "','" + 
                    p.ATTRIBUTE11 + "','" + p.ATTRIBUTE12 + "','" + p.ATTRIBUTE13 + "','" +
                    p.ATTRIBUTE14 + "','" + p.ATTRIBUTE15 + "','" + p.ATTRIBUTE16 + "','" +
                    p.ATTRIBUTE17 + "','" + p.ATTRIBUTE18 + "','" + p.ATTRIBUTE19 + "','" +
                    p.ATTRIBUTE20 + "','" + p.ATTRIBUTE_CATEGORY + "','" + p.ATTRIBUTE_CATEGORY_L + "','" +
                    p.BUDGET_DATE + "','" + p.CHARGE_ACCOUNT + "','" + p.CONVERSION_DATE + "','" +
                    p.CREATED_BY + "','" + p.CREATION_DATE + "'," + p.CURRENCY_AMOUNT + ",'" +
                    p.DESCRIPTION + "','" + p.DESTINATION_ORGANIZATION_ID + "','" + p.DESTINATION_TYPE_CODE + "','" +
                    p.DISTRIBUTION + "','" + p.DISTRIBUTION_CURRENCY_AMOUNT + "','" + p.DOCUMENT_STATUS + "','" +
                    p.FUNDS_STATUS + "','" + p.ITEM_DESCRIPTION + "','" + p.ITEM_ID + "','" +
                    p.LAST_UPDATE_DATE + "','" + p.LINE_NUMBER + "','" + p.LINE_TYPE + "','" +
                    p.LOCATION + "','" + p.NAME + "','" + p.PERCENT + "','" +
                    p.PO_ORDER + "','" + p.REQUISITION_HEADER_ID + "','" + p.REQUISITION_LINE_ID + "','" +
                    p.REQUISITION_NUMBER + "','" + p.REQ_BU_ID + "'," + p.SECONDARY_QUANTITY + ",'" +
                    p.SECONDARY_UOM_CODE + "','" + p.SOURCE_TYPE_CODE + "','" + p.UNIT_PRICE + "','" +
                    p.UOM_CODE + "','" + p.VENDOR_ID + "','" + p.VENDOR_SITE_ID + "'" +                    

                    ") ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po");
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
