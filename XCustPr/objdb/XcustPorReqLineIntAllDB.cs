using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPorReqLineIntAllDB
    {
        ConnectDB conn;
        public XcustPorReqLineIntAll xCPRLIA;
        private InitC initC;
        public XcustPorReqLineIntAllDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCPRLIA = new XcustPorReqLineIntAll();
            xCPRLIA.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPRLIA.ATTRIBUTE_DATE1 = "ATTRIBUTE_DATE1";
            xCPRLIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCPRLIA.ATTRIBUTE_TIMESTAMP1 = "ATTRIBUTE_TIMESTAMP1";
            xCPRLIA.CATEGORY_NAME = "CATEGORY_NAME";
            xCPRLIA.CURRENCY_CODE = "CURRENCY_CODE";
            xCPRLIA.DELIVER_TO_LOCATION_CODE = "DELIVER_TO_LOCATION_CODE";//Deliver_to_Location
            xCPRLIA.DELIVER_TO_ORGANIZATION_CODE = "DELIVER_TO_ORGANIZATION_CODE";//Deliver_to_Organization
            //xCPRLIA.Goods = "Goods";
            xCPRLIA.DESTINATION_TYPE_CODE = "DESTINATION_TYPE_CODE";    //INVENTORY
            xCPRLIA.ITEM_CODE = "ITEM_CODE";
            //xCPRLIA.LINFOX_PR = "LINFOX_PR";
            xCPRLIA.NEED_BY_DATE = "NEED_BY_DATE";
            xCPRLIA.REQ_LINE_INTERFACE_ID = "REQ_LINE_INTERFACE_ID";      //PO_LINE_NUMBER
            xCPRLIA.REQ_HEADER_INTERFACE_ID = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            xCPRLIA.Price = "Price";
            xCPRLIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCPRLIA.PRC_BU_NAME = "PRC_BU_NAME";//Procurement_BU
            xCPRLIA.PR_APPROVER = "PR_APPROVER";
            xCPRLIA.QTY = "QTY";
            //xCPRLIA.REQUESTER_EMAIL_ADDR = "REQUESTER_EMAIL_ADDR";//requester
            //xCPRLIA.Requisitioning_BU = "Requisitioning_BU";//Requisitioning_BU
            xCPRLIA.DESTINATION_SUBINVENTORY = "DESTINATION_SUBINVENTORY";//Subinventory
            xCPRLIA.SUGGESTED_VENDOR_NAME = "SUGGESTED_VENDOR_NAME";//SUPPLIER_CODE
            xCPRLIA.SUGGESTED_VENDOR_SITE = "SUGGESTED_VENDOR_SITE";//Supplier_Site
            xCPRLIA.CURRENCY_UNIT_PRICE = "CURRENCY_UNIT_PRICE";
            xCPRLIA.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";

            xCPRLIA.ATTRIBUTE2 = "ATTRIBUTE2";
            xCPRLIA.ATTRIBUTE3 = "ATTRIBUTE3";
            xCPRLIA.ATTRIBUTE4 = "ATTRIBUTE4";
            xCPRLIA.ATTRIBUTE5 = "ATTRIBUTE5";
            xCPRLIA.ATTRIBUTE6 = "ATTRIBUTE6";
            xCPRLIA.ATTRIBUTE7 = "ATTRIBUTE7";
            xCPRLIA.ATTRIBUTE8 = "ATTRIBUTE8";
            xCPRLIA.ATTRIBUTE9 = "ATTRIBUTE9";
            xCPRLIA.ATTRIBUTE10 = "ATTRIBUTE10";
            xCPRLIA.ATTRIBUTE11 = "ATTRIBUTE11";
            xCPRLIA.ATTRIBUTE12 = "ATTRIBUTE12";
            xCPRLIA.ATTRIBUTE13 = "ATTRIBUTE13";
            xCPRLIA.ATTRIBUTE14 = "ATTRIBUTE14";
            xCPRLIA.ATTRIBUTE15 = "ATTRIBUTE15";
            xCPRLIA.ATTRIBUTE16 = "ATTRIBUTE16";
            xCPRLIA.ATTRIBUTE17 = "ATTRIBUTE17";
            xCPRLIA.ATTRIBUTE18 = "ATTRIBUTE18";
            xCPRLIA.ATTRIBUTE19 = "ATTRIBUTE19";
            xCPRLIA.ATTRIBUTE20 = "ATTRIBUTE20";

            xCPRLIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCPRLIA.ATTRIBUTE_NUMBER2 = "ATTRIBUTE_NUMBER2";
            xCPRLIA.ATTRIBUTE_NUMBER3 = "ATTRIBUTE_NUMBER3";
            xCPRLIA.ATTRIBUTE_NUMBER4 = "ATTRIBUTE_NUMBER4";
            xCPRLIA.ATTRIBUTE_NUMBER5 = "ATTRIBUTE_NUMBER5";
            xCPRLIA.ATTRIBUTE_NUMBER6 = "ATTRIBUTE_NUMBER6";
            xCPRLIA.ATTRIBUTE_NUMBER7 = "ATTRIBUTE_NUMBER7";
            xCPRLIA.ATTRIBUTE_NUMBER8 = "ATTRIBUTE_NUMBER8";
            xCPRLIA.ATTRIBUTE_NUMBER9 = "ATTRIBUTE_NUMBER9";
            xCPRLIA.ATTRIBUTE_NUMBER10 = "ATTRIBUTE_NUMBER10";

            xCPRLIA.ATTRIBUTE_DATE2 = "ATTRIBUTE_DATE2";
            xCPRLIA.ATTRIBUTE_DATE3 = "ATTRIBUTE_DATE3";
            xCPRLIA.ATTRIBUTE_DATE4 = "ATTRIBUTE_DATE4";
            xCPRLIA.ATTRIBUTE_DATE5 = "ATTRIBUTE_DATE5";
            xCPRLIA.ATTRIBUTE_DATE6 = "ATTRIBUTE_DATE6";
            xCPRLIA.ATTRIBUTE_DATE7 = "ATTRIBUTE_DATE7";
            xCPRLIA.ATTRIBUTE_DATE8 = "ATTRIBUTE_DATE8";
            xCPRLIA.ATTRIBUTE_DATE9 = "ATTRIBUTE_DATE9";
            xCPRLIA.ATTRIBUTE_DATE10 = "ATTRIBUTE_DATE10";

            xCPRLIA.ATTRIBUTE_TIMESTAMP2 = "ATTRIBUTE_TIMESTAMP2";
            xCPRLIA.ATTRIBUTE_TIMESTAMP3 = "ATTRIBUTE_TIMESTAMP3";
            xCPRLIA.ATTRIBUTE_TIMESTAMP4 = "ATTRIBUTE_TIMESTAMP4";
            xCPRLIA.ATTRIBUTE_TIMESTAMP5 = "ATTRIBUTE_TIMESTAMP5";
            xCPRLIA.ATTRIBUTE_TIMESTAMP6 = "ATTRIBUTE_TIMESTAMP6";
            xCPRLIA.ATTRIBUTE_TIMESTAMP7 = "ATTRIBUTE_TIMESTAMP7";
            xCPRLIA.ATTRIBUTE_TIMESTAMP8 = "ATTRIBUTE_TIMESTAMP8";
            xCPRLIA.ATTRIBUTE_TIMESTAMP9 = "ATTRIBUTE_TIMESTAMP9";
            xCPRLIA.ATTRIBUTE_TIMESTAMP10 = "ATTRIBUTE_TIMESTAMP10";
            xCPRLIA.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCPRLIA.CREATION_DATE = "CREATION_DATE";
            //xCPRLIA.IMPORT_SOURCE = "IMPORT_SOURCE";
            xCPRLIA.CREATE_BY = "CREATE_BY";
            xCPRLIA.LAST_UPDATE_BY = "LAST_UPDATE_BY";

            xCPRLIA.LINE_TYPE = "LINE_TYPE";
            xCPRLIA.AGREEMENT_NUMBER = "AGREEMENT_NUMBER";
            xCPRLIA.UOM_CODE = "UOM_CODE";

            xCPRLIA.table = "xcust_por_req_line_int_all";
        }
        public XcustPorReqLineIntAll setData(DataRow row, XcustLinfoxPrTbl xclfpt)
        {
            XcustPorReqLineIntAll item = new XcustPorReqLineIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xclfpt.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xclfpt.LINE_NUMBER].ToString();
            item.DESTINATION_TYPE_CODE = "";
            item.PRC_BU_NAME = "";
            item.DELIVER_TO_ORGANIZATION_CODE = initC.ORGANIZATION_code;
            item.DELIVER_TO_LOCATION_CODE = row[xclfpt.deriver_to_location].ToString();
            item.DESTINATION_SUBINVENTORY = row[xclfpt.subinventory_code].ToString();
            item.CATEGORY_NAME = row[xclfpt.ITEM_CATEGORY_NAME].ToString();
            item.NEED_BY_DATE = row[xclfpt.REQUEST_TIME].ToString();
            item.ITEM_CODE = row[xclfpt.ITEM_CODE].ToString();
            item.LINE_TYPE = "";
            
            item.QTY = row[xclfpt.QTY].ToString();
            item.CURRENCY_CODE = initC.CURRENCY_CODE;
            item.AGREEMENT_NUMBER = row[xclfpt.AGREEEMENT_NUMBER].ToString();
            item.CURRENCY_UNIT_PRICE = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            item.Price = row[xclfpt.PRICE].ToString();
            item.PROCESS_FLAG = "Y";
            item.UOM_CODE = row[xclfpt.UOMCODE].ToString();

            return item;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCPRLIA.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String insert(XcustPorReqLineIntAll p)
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
                p.ATTRIBUTE_NUMBER1 = p.ATTRIBUTE_NUMBER1.Equals("") ? "0" : p.ATTRIBUTE_NUMBER1;
                p.ATTRIBUTE_NUMBER2 = p.ATTRIBUTE_NUMBER2.Equals("") ? "0" : p.ATTRIBUTE_NUMBER2;
                p.ATTRIBUTE_NUMBER3 = p.ATTRIBUTE_NUMBER3.Equals("") ? "0" : p.ATTRIBUTE_NUMBER3;
                p.ATTRIBUTE_NUMBER4 = p.ATTRIBUTE_NUMBER4.Equals("") ? "0" : p.ATTRIBUTE_NUMBER4;
                p.ATTRIBUTE_NUMBER5 = p.ATTRIBUTE_NUMBER5.Equals("") ? "0" : p.ATTRIBUTE_NUMBER5;
                p.ATTRIBUTE_NUMBER6 = p.ATTRIBUTE_NUMBER6.Equals("") ? "0" : p.ATTRIBUTE_NUMBER6;
                p.ATTRIBUTE_NUMBER7 = p.ATTRIBUTE_NUMBER7.Equals("") ? "0" : p.ATTRIBUTE_NUMBER7;
                p.ATTRIBUTE_NUMBER8 = p.ATTRIBUTE_NUMBER8.Equals("") ? "0" : p.ATTRIBUTE_NUMBER8;
                p.ATTRIBUTE_NUMBER9 = p.ATTRIBUTE_NUMBER9.Equals("") ? "0" : p.ATTRIBUTE_NUMBER9;
                p.ATTRIBUTE_NUMBER10 = p.ATTRIBUTE_NUMBER10.Equals("") ? "0" : p.ATTRIBUTE_NUMBER10;
                sql = "Insert Into " + xCPRLIA.table + "(" + xCPRLIA.ATTRIBUTE1 + "," + xCPRLIA.ATTRIBUTE_DATE1 + "," +
                    xCPRLIA.ATTRIBUTE_TIMESTAMP1 + "," + xCPRLIA.CATEGORY_NAME + "," +
                    xCPRLIA.CURRENCY_CODE + "," + xCPRLIA.DELIVER_TO_LOCATION_CODE + "," + xCPRLIA.DELIVER_TO_ORGANIZATION_CODE + "," +
                    xCPRLIA.DESTINATION_TYPE_CODE + "," + xCPRLIA.ITEM_CODE + "," +
                    xCPRLIA.NEED_BY_DATE + "," + xCPRLIA.REQ_LINE_INTERFACE_ID + "," +
                    xCPRLIA.REQ_HEADER_INTERFACE_ID + "," + xCPRLIA.PROCESS_FLAG + "," +
                    xCPRLIA.PRC_BU_NAME + "," + xCPRLIA.QTY + "," +
                    xCPRLIA.DESTINATION_SUBINVENTORY + "," + 
                    xCPRLIA.SUGGESTED_VENDOR_NAME + "," + xCPRLIA.SUGGESTED_VENDOR_SITE + "," +
                    xCPRLIA.ATTRIBUTE2 + "," + xCPRLIA.ATTRIBUTE3 + "," + xCPRLIA.ATTRIBUTE4 + "," +
                    xCPRLIA.ATTRIBUTE5 + "," + xCPRLIA.ATTRIBUTE6 + "," + xCPRLIA.ATTRIBUTE7 + "," +
                    xCPRLIA.ATTRIBUTE8 + "," + xCPRLIA.ATTRIBUTE9 + "," + xCPRLIA.ATTRIBUTE10 + "," +
                    xCPRLIA.ATTRIBUTE11 + "," + xCPRLIA.ATTRIBUTE12 + "," + xCPRLIA.ATTRIBUTE13 + "," +
                    xCPRLIA.ATTRIBUTE14 + "," + xCPRLIA.ATTRIBUTE15 + "," + xCPRLIA.ATTRIBUTE16 + "," +
                    xCPRLIA.ATTRIBUTE17 + "," + xCPRLIA.ATTRIBUTE18 + "," + xCPRLIA.ATTRIBUTE19 + "," +
                    xCPRLIA.ATTRIBUTE20 + "," +
                    xCPRLIA.ATTRIBUTE_NUMBER1 + "," + xCPRLIA.ATTRIBUTE_NUMBER2 + "," + xCPRLIA.ATTRIBUTE_NUMBER3 + "," +
                    xCPRLIA.ATTRIBUTE_NUMBER4 + "," + xCPRLIA.ATTRIBUTE_NUMBER5 + "," + xCPRLIA.ATTRIBUTE_NUMBER6 + "," +
                    xCPRLIA.ATTRIBUTE_NUMBER7 + "," + xCPRLIA.ATTRIBUTE_NUMBER8 + "," + xCPRLIA.ATTRIBUTE_NUMBER9 + "," +
                    xCPRLIA.ATTRIBUTE_NUMBER10 + "," +
                    xCPRLIA.ATTRIBUTE_DATE2 + "," + xCPRLIA.ATTRIBUTE_DATE3 + "," + xCPRLIA.ATTRIBUTE_DATE4 + "," +
                    xCPRLIA.ATTRIBUTE_DATE5 + "," + xCPRLIA.ATTRIBUTE_DATE6 + "," + xCPRLIA.ATTRIBUTE_DATE7 + "," +
                    xCPRLIA.ATTRIBUTE_DATE8 + "," + xCPRLIA.ATTRIBUTE_DATE9 + "," + xCPRLIA.ATTRIBUTE_DATE10 + "," +
                    xCPRLIA.ATTRIBUTE_TIMESTAMP2 + "," + xCPRLIA.ATTRIBUTE_TIMESTAMP3 + "," + xCPRLIA.ATTRIBUTE_TIMESTAMP4 + "," +
                    xCPRLIA.ATTRIBUTE_TIMESTAMP5 + "," + xCPRLIA.ATTRIBUTE_TIMESTAMP6 + "," + xCPRLIA.ATTRIBUTE_TIMESTAMP7 + "," +
                    xCPRLIA.ATTRIBUTE_TIMESTAMP8 + "," + xCPRLIA.ATTRIBUTE_TIMESTAMP9 + "," + xCPRLIA.ATTRIBUTE_TIMESTAMP10 + "," +
                    xCPRLIA.LAST_UPDATE_DATE + "," + xCPRLIA.CREATION_DATE + "," + xCPRLIA.CREATE_BY + "," +
                    xCPRLIA.LAST_UPDATE_BY + "," + xCPRLIA.LINE_TYPE + "," + xCPRLIA.AGREEMENT_NUMBER + ","+
                    xCPRLIA.UOM_CODE+") " +
                    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_DATE1 + "'," +
                    "getdate(),'" + p.CATEGORY_NAME + "','" +
                    p.CURRENCY_CODE + "','" + p.DELIVER_TO_LOCATION_CODE + "','" + p.DELIVER_TO_ORGANIZATION_CODE + "','" +
                    p.DESTINATION_TYPE_CODE + "','" + p.ITEM_CODE + "','" +
                    p.NEED_BY_DATE + "'," + p.REQ_LINE_INTERFACE_ID + ",'" +
                    p.REQ_HEADER_INTERFACE_ID + "','" + p.PROCESS_FLAG + "','" +
                    p.PRC_BU_NAME + "'," + p.QTY + ",'" +
                    p.DESTINATION_SUBINVENTORY + "','" + 
                    p.SUGGESTED_VENDOR_NAME + "','" + p.SUGGESTED_VENDOR_SITE + "','" +
                    p.ATTRIBUTE2 + "','" + p.ATTRIBUTE3 + "','" + p.ATTRIBUTE4 + "','" +
                    p.ATTRIBUTE5 + "','" + p.ATTRIBUTE6 + "','" + p.ATTRIBUTE7 + "','" +
                    p.ATTRIBUTE8 + "','" + p.ATTRIBUTE9 + "','" + p.ATTRIBUTE10 + "','" +
                    p.ATTRIBUTE11 + "','" + p.ATTRIBUTE12 + "','" + p.ATTRIBUTE13 + "','" +
                    p.ATTRIBUTE14 + "','" + p.ATTRIBUTE15 + "','" + p.ATTRIBUTE16 + "','" +
                    p.ATTRIBUTE17 + "','" + p.ATTRIBUTE18 + "','" + p.ATTRIBUTE19 + "','" +
                    p.ATTRIBUTE20 + "','" +
                    p.ATTRIBUTE_NUMBER1 + "','" + p.ATTRIBUTE_NUMBER2 + "','" + p.ATTRIBUTE_NUMBER3 + "','" +
                    p.ATTRIBUTE_NUMBER4 + "','" + p.ATTRIBUTE_NUMBER5 + "','" + p.ATTRIBUTE_NUMBER6 + "','" +
                    p.ATTRIBUTE_NUMBER7 + "','" + p.ATTRIBUTE_NUMBER8 + "','" + p.ATTRIBUTE_NUMBER9 + "','" +
                    p.ATTRIBUTE_NUMBER10 + "','" +
                    p.ATTRIBUTE_DATE2 + "','" + p.ATTRIBUTE_DATE3 + "','" + p.ATTRIBUTE_DATE4 + "','" +
                    p.ATTRIBUTE_DATE5 + "','" + p.ATTRIBUTE_DATE6 + "','" + p.ATTRIBUTE_DATE7 + "','" +
                    p.ATTRIBUTE_DATE8 + "','" + p.ATTRIBUTE_DATE9 + "','" + p.ATTRIBUTE_DATE10 + "','" +
                    p.ATTRIBUTE_TIMESTAMP2 + "','" + p.ATTRIBUTE_TIMESTAMP3 + "','" + p.ATTRIBUTE_TIMESTAMP4 + "','" +
                    p.ATTRIBUTE_TIMESTAMP5 + "','" + p.ATTRIBUTE_TIMESTAMP6 + "','" + p.ATTRIBUTE_TIMESTAMP7 + "','" +
                    p.ATTRIBUTE_TIMESTAMP8 + "','" + p.ATTRIBUTE_TIMESTAMP9 + "','" + p.ATTRIBUTE_TIMESTAMP10 + "','" +
                    p.LAST_UPDATE_DATE + "',getdate(),'" + p.CREATE_BY + "','" +
                    p.LAST_UPDATE_BY + "','" + p.LINE_TYPE + "','" + p.AGREEMENT_NUMBER + "','" +
                    p.UOM_CODE+"') ";
                chk = conn.ExecuteNonQueryAutoIncrement(sql, "kfc_po");
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
