﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    /*
     * 1. 61-01-04 rerun -> create_date
     * 
     */
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
        public DataTable selectPO007FixLenHeader()
        {
            DataTable dt = new DataTable();
            String sql = "Select * From XCUST_FIX_LENGTH_TBL Where CUSTOMIZATION_NAME = 'PO007' and X_LEVEL = 'HEADER' Order By X_LEVEL, X_SEQ ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPO007FixLenLine()
        {
            DataTable dt = new DataTable();
            String sql = "Select * From XCUST_FIX_LENGTH_TBL Where CUSTOMIZATION_NAME = 'PO007' and X_LEVEL = 'LINE' Order By X_LEVEL, X_SEQ ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPO006FixLen()
        {
            DataTable dt = new DataTable();
            String sql = "Select * From XCUST_FIX_LENGTH_TBL Where CUSTOMIZATION_NAME = 'PO006_2' Order By X_LEVEL, X_SEQ ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }

        public DataTable selectPRPO(String linfox_po_number, String linfox_po_line_number, String flag)
        {
            String where = " and po.DOCUMENT_STATUS = 'OPEN' and pr.name = 'RD TEST' ";
            DataTable dt = new DataTable();
            //String sql = "SELECT po.PO_HEADER_ID, po.PO_LINE_ID,po.SEGMENT1 po_number,po.LINE_NUM po_line_number, po.QUANTITY/*, po.header_id*/ "+
            //    "From xcust_pr_tbl PR "+
            //    "Left Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //    "Where  "+                ""+
            //    " PR.ATTRIBUTE1_L = '"+ linfox_po_number + "' "+
            //    "and PR.ATTRIBUTE2_L = '"+ linfox_po_line_number + "' ";
            where = "";
            String sql = "SELECT po.PO_HEADER_ID, po.PO_LINE_ID,po.SEGMENT1 po_number,po.LINE_NUM po_line_number, po.QUANTITY/*, po.header_id*/ " +
                "From xcust_pr_tbl PR " +
                "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                "Where  " + "" +
                " PR.ATTRIBUTE2 = '" + linfox_po_number + "' " +
                "and PR.ATTRIBUTE2_L = '" + linfox_po_line_number + "' and pr.ATTRIBUTE1 = '"+flag+ "' " + where;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO007()
        {
            DataTable dt = new DataTable();
            String sql = "";
            //sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID " +
            //    ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM " +
            //    ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID " +
            //    "From xcust_pr_tbl PR " +
            //    "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //    "Where  " + "" +
            //    " PR.ATTRIBUTE1 <> 'MMX'  ";
            sql = "select PO.* from xcust_PO_TBL po "
                + " ,XCUST_SUPPLIER_MST_TBL t "
                + " where not exists (select 'x'   "
                + " from XCUST_PR_TBL pr "
                + " where pr.REQUISITION_HEADER_ID = po.REQUISITION_HEADER_ID "
                + " and pr.REQUISITION_LINE_ID = po.REQUISITION_LINE_ID "
                + " and pr.attribute1 = 'MMX') "
                + " and exists (select 'x' from xcust_po_receipt_tbl pocp  "
                + " where pocp.PO_HEADER_ID = po.PO_HEADER_ID "
                + "  and pocp.PO_LINE_ID = po.po_line_id)  "
                + " and po.VENDOR_ID = t.VENDOR_ID and t.ATTRIBUTE1 != 'Y'  and po.GEN_OUTBOUD_FLAG = '' ";
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
        public DataTable selectPRPO006GroupByVendorDeliveryDate()
        {
            DataTable dt = new DataTable();
            String sql = "";
            //sql = "SELECT po.VENDOR_ID, po.acc_segment1 as deliveryDate " +
            //    "From xcust_pr_tbl PR " +
            //    "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //    "Where  " + "" +
            //    " PR.ATTRIBUTE1 <> 'MMX' group by po.VENDOR_ID, po.acc_segment1 ";[DELIVER_DATE]
            sql = "select  t.SUPPLIER_NUMBER, po.DELIVER_DATE as DELIVERY_DATE ,po.VENDOR_ID " +
                "from xcust_PO_TBL po " +
                ",XCUST_SUPPLIER_MST_TBL t  " +
                "where  po.VENDOR_ID = t.VENDOR_ID  " + "" +
                " and t.ATTRIBUTE1 = 'Y' " +
                //" and po.GEN_OUTBOUD_FLAG = ''   and po.DELIVER_DATE is not null " +
                " and po.GEN_OUTBOUD_FLAG is null   and po.DELIVER_DATE is not null and PR.ATTRIBUTE1 = 'MMX' " +
                " GROUP BY po.VENDOR_ID, t.SUPPLIER_NUMBER, po.DELIVER_DATE ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO006GroupByVendorDeliveryDate1(String delivery_date, String rerun)
        {
            DataTable dt = new DataTable();
            String sql = "", where="", date="", whereRerun="";
            if (delivery_date.Equals("sysdate"))
            {
                date = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                date = delivery_date;
            }
            if (delivery_date.Equals(""))
            {
                where = " ";
            }
            else
            {
                where = " and po.DELIVER_DATE = '" + date + "' ";
                where = "  "; //for test
                //where = " and po.DELIVER_DATE = '2017-12-25' "; //for test
            }
            if (rerun.Equals("Y"))
            {
                whereRerun = " and po.GEN_OUTBOUD_FLAG = 'Y' ";
            }
            else
            {
                //whereRerun = " and po.GEN_OUTBOUD_FLAG = 'N' ";
                whereRerun = " and po.GEN_OUTBOUD_FLAG is null ";
            }
            //sql = "SELECT po.VENDOR_ID, po.acc_segment1 as deliveryDate " +
            //    "From xcust_pr_tbl PR " +
            //    "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //    "Where  " + "" +
            //    " PR.ATTRIBUTE1 <> 'MMX' group by po.VENDOR_ID, po.acc_segment1 ";[DELIVER_DATE]
            //sql = "select  t.SUPPLIER_NUMBER, po.DELIVER_DATE as DELIVERY_DATE ,po.VENDOR_ID,po.attribute2 " +
            //"from xcust_PO_TBL po " +
            //",XCUST_SUPPLIER_MST_TBL t  " +
            //"where  po.VENDOR_ID = t.VENDOR_ID  " + "" +
            //" and t.ATTRIBUTE1 = 'Y' " +
            ////" and po.GEN_OUTBOUD_FLAG = ''   and po.DELIVER_DATE is not null " +
            //"   " + whereRerun+ where+
            //" GROUP BY po.VENDOR_ID, t.SUPPLIER_NUMBER,po.attribute2, po.DELIVER_DATE ";

            sql = "select  t.SUPPLIER_NUMBER, po.DELIVER_DATE as DELIVERY_DATE ,po.VENDOR_ID,t.attribute2 " +
            "from xcust_PO_TBL po " +
            ",XCUST_SUPPLIER_MST_TBL t  " +
            ", XCUST_PR_TBL pr  " +
            "where  po.VENDOR_ID = t.VENDOR_ID  " + "" +
            " and t.ATTRIBUTE1 = 'Y' and   po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID and pr.ATTRIBUTE1 = 'MMX'  " +
            //" and po.GEN_OUTBOUD_FLAG = ''   and po.DELIVER_DATE is not null " +
            "   " + whereRerun + where +
            " GROUP BY po.VENDOR_ID, t.SUPPLIER_NUMBER,t.attribute2, po.DELIVER_DATE ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO006GroupByVendorDeliveryDate2(String delivery_date, String creation_date, String rerun, String buId)
        {
            DataTable dt = new DataTable();
            String sql = "", where = "", date = "", whereRerun = "", whereId="";
            if (delivery_date.Equals("sysdate"))
            {
                date = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                date = delivery_date;
            }

            if (delivery_date.Equals(""))
            {
                where = " ";
            }
            else
            {
                where = " and po.CREATION_DATE = '" + date + "' ";
                //where = "  "; //for test
                //where = " and po.CREATION_DATE = '2017-12-31' "; //for test
            }

            if (rerun.Equals("Y"))
            {
                whereRerun = " and po.GEN_OUTBOUD_FLAG = 'Y' ";
                where = " and po.CREATION_DATE >= '" + creation_date + " 00:00' and po.CREATION_DATE <= '"+creation_date+" 23:59'";       //+1
            }
            else
            {
                //whereRerun = " and po.GEN_OUTBOUD_FLAG = 'N' ";
                whereRerun = " and po.GEN_OUTBOUD_FLAG is null ";
            }

            if (buId.Equals(""))
            {
                whereId = " ";
            }
            else
            {
                whereId = " and po.PRC_BU_ID = '"+buId+"'";
            }
               
            //sql = "SELECT po.VENDOR_ID, po.acc_segment1 as deliveryDate " +
            //    "From xcust_pr_tbl PR " +
            //    "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //    "Where  " + "" +
            //    " PR.ATTRIBUTE1 <> 'MMX' group by po.VENDOR_ID, po.acc_segment1 ";[DELIVER_DATE]
            //sql = "select  t.SUPPLIER_NUMBER, po.DELIVER_DATE as DELIVERY_DATE ,po.VENDOR_ID,po.attribute2 " +
            //"from xcust_PO_TBL po " +
            //",XCUST_SUPPLIER_MST_TBL t  " +
            //"where  po.VENDOR_ID = t.VENDOR_ID  " + "" +
            //" and t.ATTRIBUTE1 = 'Y' " +
            ////" and po.GEN_OUTBOUD_FLAG = ''   and po.DELIVER_DATE is not null " +
            //"   " + whereRerun+ where+
            //" GROUP BY po.VENDOR_ID, t.SUPPLIER_NUMBER,po.attribute2, po.DELIVER_DATE ";

            //sql = "Select  t.SUPPLIER_NUMBER, po.DELIVER_DATE as DELIVERY_DATE ,po.VENDOR_ID,t.attribute2 " +
            //"from xcust_PO_TBL po " +
            //",XCUST_SUPPLIER_MST_TBL t  " +
            //", XCUST_PR_TBL pr  " +
            //"where  po.VENDOR_ID = t.VENDOR_ID  " + " " +
            //" and t.ATTRIBUTE1 = 'Y' and   po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID and pr.ATTRIBUTE1 = 'MMX'  " +
            ////" and po.GEN_OUTBOUD_FLAG = ''   and po.DELIVER_DATE is not null " +
            //"   " + whereRerun + where + whereId +
            //" GROUP BY po.VENDOR_ID, t.SUPPLIER_NUMBER,t.attribute2, po.DELIVER_DATE ";

            sql = "Select  t.SUPPLIER_NUMBER, substring(po.CREATION_DATE,1,10) as DELIVERY_DATE ,po.VENDOR_ID,t.attribute2 " +
            "from xcust_PO_TBL po " +
            ",XCUST_SUPPLIER_MST_TBL t  " +
            ", XCUST_PR_TBL pr  " +
            "where  po.VENDOR_ID = t.VENDOR_ID  " + " " +
            " and t.ATTRIBUTE1 = 'Y' and   po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID and pr.ATTRIBUTE1 = 'MMX'  " +
            //" and po.GEN_OUTBOUD_FLAG = ''   and po.DELIVER_DATE is not null " +
            "   " + whereRerun + where + whereId +
            " GROUP BY po.VENDOR_ID, t.SUPPLIER_NUMBER,t.attribute2, substring(po.CREATION_DATE,1,10) ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO006GroupByVendor()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT t.attribute2 " +
                "From xcust_pr_tbl PR "+
                " , XCUST_SUPPLIER_MST_TBL t" +
                ", XCUST_po_TBL po      " +
                "Where  po.VENDOR_ID = t.VENDOR_ID and po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                " and PR.ATTRIBUTE1 = 'MMX' group by t.attribute2 ";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO006(String VENDOR_ID, String delivery_date, String rerun)
        {
            DataTable dt = new DataTable();
            String sql = "", where="", whereRerun = "";
            if (delivery_date.Equals(""))
            {
                where = " ";
            }
            else
            {
                //where = " and po.DELIVER_DATE = '"+delivery_date+"' ";        //for test
                where = "  ";
            }
            if (rerun.Equals("Y"))
            {
                whereRerun = " and po.GEN_OUTBOUD_FLAG = 'Y' ";
            }
            else
            {
                whereRerun = " and po.GEN_OUTBOUD_FLAG = 'N' ";
            }
            whereRerun = " ";
            //sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID " +
            //        ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM " +
            //        ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID, po.DELIVER_DATE " +
            //        "From xcust_pr_tbl PR " +
            //        "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //        "Where po.VENDOR_ID =" + VENDOR_ID +
            //        " and PR.ATTRIBUTE1 <> 'MMX'  "+ where+ whereRerun+ 
            //        " Order By po.SEGMENT1 ";
            sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID " +
                    ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM " +
                    ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID, po.DELIVER_DATE " +
                    "From xcust_pr_tbl PR " +
                    "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                    "Where po.VENDOR_ID =" + VENDOR_ID +
                    "  " + where + whereRerun +
                    " Order By po.SEGMENT1 ";

            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectPRPO0061(String VENDOR_ID, String delivery_date)
        {
            DataTable dt = new DataTable();
            String sql = "", where = "", whereRerun = "";
            if (delivery_date.Equals(""))
            {
                where = " ";
            }
            else
            {
                //where = " and po.DELIVER_DATE = '" + delivery_date + "' ";
                where = " and po.CREATION_DATE >= '" + delivery_date + " 00:00' and po.CREATION_DATE <= '"+delivery_date+" 23:59'";       //+1
                //where = "  ";
            }
            //if (rerun.Equals("Y"))
            //{
            //    whereRerun = " and po.GEN_OUTBOUD_FLAG = 'Y' ";
            //}
            //else
            //{
            //    whereRerun = " and po.GEN_OUTBOUD_FLAG = 'N' ";
            //}
            whereRerun = " ";
            //sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID " +
            //        ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM " +
            //        ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID, po.DELIVER_DATE " +
            //        "From xcust_pr_tbl PR " +
            //        "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //        "Where po.VENDOR_ID =" + VENDOR_ID +
            //        " and PR.ATTRIBUTE1 <> 'MMX'  "+ where+ whereRerun+ 
            //        " Order By po.SEGMENT1 ";
            //sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID " +
            //        ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM " +
            //        ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID, po.DELIVER_DATE " +
            //        "From xcust_pr_tbl PR " +
            //        "inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //        "Where po.VENDOR_ID =" + VENDOR_ID +
            //        "  " + where + whereRerun +
            //        " Order By po.SEGMENT1 ";

            //sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID " +
            //        ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM " +
            //        ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID, po.DELIVER_DATE " +
            //        "From xcust_po_tbl po " +
            //        //"inner Join xcust_po_tbl po On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
            //        "Where po.VENDOR_ID =" + VENDOR_ID +
            //        "  " + where + whereRerun +
            //        " Order By po.SEGMENT1 ";
            sql = "SELECT po.CREATION_DATE, po.PO_LINE_ID,po.SEGMENT1 as po_number,po.LINE_NUM, po.QUANTITY, po.VENDOR_ID, po.PRC_BU_ID, po.ITEM_ID " +
                    ", po.ITEM_DESCRIPTION, po.QUANTITY_RECEIPT, po.QUANTITY, po.UOM_CODE, po.UNIT_PRICE, po.LINE_TYPE_ID, po.PAYMENT_TERM, po.CURRENCY_CODE, po.REVISION_NUM " +
                    ",po.SEGMENT1, po.ACC_SEGMENT1, po.ACC_SEGMENT2,po.TAX_CODE, po.PO_HEADER_ID, po.DELIVER_DATE, pr.ATTRIBUTE3_L " +
                    "From xcust_po_tbl po " +
                    "inner Join xcust_pr_tbl pr On  po.REQUISITION_HEADER_ID = PR.REQUISITION_HEADER_ID and po.REQUISITION_LINE_ID = PR.REQUISITION_LINE_ID  " +
                    "Where  pr.ATTRIBUTE1 = 'MMX' and po.VENDOR_ID =" + VENDOR_ID +
                    "  " + where + whereRerun +
                    " Order By po.SEGMENT1 ";

            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String selectItemCode(String item_id, String prc_bu_id)
        {
            String chk = "";
            DataTable dt = new DataTable();
            String sql = "select msi.ITEM_CODE "+
                            "from xcust_item_mst_tbl msi " +
                            ",xcust_organization_mst_tbl org " +//ORGANIZATION_ID
                            ", XCUST_BU_MST_TBL bu " +
                            " where " +
                            "/*msi.INVENTORY_ITEM_ID   = 300000001619857 "+
                            "and*/ msi.ORGANIZATION_ID = org.ORGANIZATION_ID " +
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
            conn.ExecuteNonQuery(sql, "kfc_po", initC.PO002PathLog);
        }
        public String insertxCPR(XcustPrTbl p, String pathLog)
        {
            String sql = "", chk="";
            if (selectDupPk(p.REQUISITION_HEADER_ID, p.REQUISITION_LINE_ID))
            {
                deletexCPR(p.REQUISITION_HEADER_ID, p.REQUISITION_LINE_ID);
            }
            chk = insert(p, pathLog);
            return chk;
        }
        private String insert(XcustPrTbl p, String pathLog)
        {
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
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
                p.UNIT_PRICE = p.UNIT_PRICE.Equals("") ? "null" : p.UNIT_PRICE;
                p.DESTINATION_ORGANIZATION_ID = p.DESTINATION_ORGANIZATION_ID.Equals("") ? "null" : p.DESTINATION_ORGANIZATION_ID;
                p.PERCENT = p.PERCENT.Equals("") ? "null" : p.PERCENT;
                //p.CURRENCY_AMOUNT = p.CURRENCY_AMOUNT.Equals("") ? "null" : p.CURRENCY_AMOUNT;
                p.VENDOR_ID = p.VENDOR_ID.Equals("") ? "null" : p.VENDOR_ID;
                p.VENDOR_SITE_ID = p.VENDOR_SITE_ID.Equals("") ? "null" : p.VENDOR_SITE_ID;
                //p.SECONDARY_QUANTITY = p.SECONDARY_QUANTITY.Equals("") ? "null" : p.SECONDARY_QUANTITY;

                p.AMOUNT = p.AMOUNT.Equals("") ? "null" : p.AMOUNT;
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
                    p.DESCRIPTION + "'," + p.DESTINATION_ORGANIZATION_ID + ",'" + p.DESTINATION_TYPE_CODE + "','" +
                    p.DISTRIBUTION + "','" + p.DISTRIBUTION_CURRENCY_AMOUNT + "','" + p.DOCUMENT_STATUS + "','" +
                    p.FUNDS_STATUS + "','" + p.ITEM_DESCRIPTION + "','" + p.ITEM_ID + "','" +
                    p.LAST_UPDATE_DATE + "','" + p.LINE_NUMBER + "','" + p.LINE_TYPE + "','" +
                    p.LOCATION + "','" + p.NAME + "'," + p.PERCENT + ",'" +
                    p.PO_ORDER + "','" + p.REQUISITION_HEADER_ID + "','" + p.REQUISITION_LINE_ID + "','" +
                    p.REQUISITION_NUMBER + "','" + p.REQ_BU_ID + "'," + p.SECONDARY_QUANTITY + ",'" +
                    p.SECONDARY_UOM_CODE + "','" + p.SOURCE_TYPE_CODE + "'," + p.UNIT_PRICE + ",'" +
                    p.UOM_CODE + "'," + p.VENDOR_ID + "," + p.VENDOR_SITE_ID + " " +                    

                    ") ";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
                
                //chk = p.RowNumber;
                //chk = p.Code;
            }
            catch (Exception ex)
            {
                chk = ex.Message;
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
                //String date1 = System.DateTime.Now.ToString("yyyy-MM-dd");
                //String time1 = System.DateTime.Now.ToString("HH_mm_ss");
                //String dateStart = date + " " + time;       //gen log

                //List<ValidatePrPo> lVPr = new List<ValidatePrPo>();   // gen log
                //List<ValidateFileName> lVfile = new List<ValidateFileName>();   // gen log
                //ValidatePrPo vPP = new ValidatePrPo();   // gen log
                //vPP = new ValidatePrPo();
                //vPP.Filename = "Table XCUST_PR_TBL";
                //vPP.Message = "Error PO002 Structure text file error "+ ex.Message;
                //vPP.Validate = "";
                //lVPr.Add(vPP);

                //ValidateFileName vF = new ValidateFileName();   // gen log
                //vF.recordError = "1";   // gen log
                //vF.totalError = "1";   // gen log
                //lVfile.Add(vF);   // gen log
                //Cm.logProcess("xcustpo002", lVPr, dateStart, lVfile);   // gen log
            }
            
            return chk;
        }
    }
}
