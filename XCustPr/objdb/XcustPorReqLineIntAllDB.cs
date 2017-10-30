using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPorReqLineIntAllDB
    {
        ConnectDB conn;
        XcustPorReqLineIntAll xCPRLIA;
        public XcustPorReqLineIntAllDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPRLIA = new XcustPorReqLineIntAll();
            xCPRLIA.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPRLIA.ATTRIBUTE_DATE1 = "ATTRIBUTE_DATE1";
            xCPRLIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCPRLIA.ATTRIBUTE_TIMESTAMP1 = "ATTRIBUTE_TIMESTAMP1";
            xCPRLIA.Category_Name = "Category_Name";
            xCPRLIA.CURRENCY_CODE = "CURRENCY_CODE";
            xCPRLIA.Deliver_to_Location = "Deliver_to_Location";
            xCPRLIA.Deliver_to_Organization = "Deliver_to_Organization";
            xCPRLIA.Goods = "Goods";
            xCPRLIA.INVENTORY = "INVENTORY";
            xCPRLIA.ITEM_NUMBER = "ITEM_NUMBER";
            xCPRLIA.LINFOX_PR = "LINFOX_PR";
            xCPRLIA.Need_by_Date = "Need_by_Date";
            xCPRLIA.PO_LINE_NUMBER = "PO_LINE_NUMBER";
            xCPRLIA.PO_NUMBER = "PO_NUMBER";
            xCPRLIA.Price = "Price";
            xCPRLIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCPRLIA.Procurement_BU = "Procurement_BU";
            xCPRLIA.PR_APPROVER = "PR_APPROVER";
            xCPRLIA.QTY = "QTY";
            xCPRLIA.requester = "requester";
            xCPRLIA.Requisitioning_BU = "Requisitioning_BU";
            xCPRLIA.Subinventory = "Subinventory";
            xCPRLIA.SUPPLIER_CODE = "SUPPLIER_CODE";
            xCPRLIA.Supplier_Site = "Supplier_Site";

            xCPRLIA.table = "xcust_por_req_line_int_all";
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
                sql = "Insert Into " + xCPRLIA.table + "(" + xCPRLIA.ATTRIBUTE1 + "," + xCPRLIA.ATTRIBUTE_DATE1 + "," +
                    xCPRLIA.ATTRIBUTE_NUMBER1 + "," + xCPRLIA.ATTRIBUTE_TIMESTAMP1 + "," + xCPRLIA.Category_Name + "," +
                    xCPRLIA.CURRENCY_CODE + "," + xCPRLIA.Deliver_to_Location + "," + xCPRLIA.Deliver_to_Organization + "," +
                    xCPRLIA.Goods + "," + xCPRLIA.INVENTORY + "," + xCPRLIA.ITEM_NUMBER + "," +
                    xCPRLIA.LINFOX_PR + "," + xCPRLIA.Need_by_Date + "," + xCPRLIA.PO_LINE_NUMBER + "," +
                    xCPRLIA.PO_NUMBER + "," + xCPRLIA.Price + "," + xCPRLIA.PROCESS_FLAG + "," +
                    xCPRLIA.Procurement_BU + "," + xCPRLIA.PR_APPROVER + "," + xCPRLIA.QTY + "," +
                    xCPRLIA.requester + "," + xCPRLIA.Requisitioning_BU + "," + xCPRLIA.Subinventory + "," + xCPRLIA.SUPPLIER_CODE + "," + xCPRLIA.Supplier_Site + ") " +
                    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_DATE1 + "','" +
                    p.ATTRIBUTE_NUMBER1 + "',now(),'" + p.Category_Name + "','" +
                    p.CURRENCY_CODE + "','" + p.Deliver_to_Location + "','" + p.Deliver_to_Organization + "','" +
                    p.Goods + "','" + p.INVENTORY + "','" + p.ITEM_NUMBER + "','" +
                    p.LINFOX_PR + "','" + p.Need_by_Date + "','" + p.PO_LINE_NUMBER + "','" +
                    p.PO_NUMBER + "','" + p.Price + "','" + p.PROCESS_FLAG + "','" +
                    p.Procurement_BU + "','" + p.PR_APPROVER + "','" + p.QTY + "','" +
                    p.requester + "','" + p.Requisitioning_BU + "','" + p.Subinventory + "','" + p.SUPPLIER_CODE + "','" + p.Supplier_Site + "') ";
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
