using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustPorReqDistIntAllDB
    {
        XcustPorReqDistIntAll xCPRDIA;
        ConnectDB conn;
        public XcustPorReqDistIntAllDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCPRDIA = new XcustPorReqDistIntAll();
            xCPRDIA.ATTRIBUTE1 = "ATTRIBUTE1";
            xCPRDIA.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCPRDIA.ATTRIBUTE_DATE1 = "ATTRIBUTE_DATE1";
            xCPRDIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCPRDIA.ATTRIBUTE_TIMESTAMP1 = "ATTRIBUTE_TIMESTAMP1";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 = "CHARGE_ACCOUNT_SEGMENT1";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 = "CHARGE_ACCOUNT_SEGMENT2";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 = "CHARGE_ACCOUNT_SEGMENT3";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 = "CHARGE_ACCOUNT_SEGMENT4";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 = "CHARGE_ACCOUNT_SEGMENT5";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 = "CHARGE_ACCOUNT_SEGMENT6";
            xCPRDIA.REQ_LINE_INTERFACE_ID = "REQ_LINE_INTERFACE_ID";//PO_LINE_NUMBER
            xCPRDIA.REQ_HEADER_INTERFACE_ID = "REQ_HEADER_INTERFACE_ID";//PO_NUMBER
            xCPRDIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCPRDIA.REQ_DIST_INTERFACE_ID = "REQ_DIST_INTERFACE_ID";// Program_running
            xCPRDIA.QTY = "QTY";

            xCPRDIA.DISTRIBUTION_CURRENCY_AMT = "DISTRIBUTION_CURRENCY_AMT";
            xCPRDIA.ATTRIBUTE2 = "ATTRIBUTE2";
            xCPRDIA.ATTRIBUTE3 = "ATTRIBUTE3";
            xCPRDIA.ATTRIBUTE4 = "ATTRIBUTE4";
            xCPRDIA.ATTRIBUTE5 = "ATTRIBUTE5";
            xCPRDIA.ATTRIBUTE6 = "ATTRIBUTE6";
            xCPRDIA.ATTRIBUTE7 = "ATTRIBUTE7";
            xCPRDIA.ATTRIBUTE8 = "ATTRIBUTE8";
            xCPRDIA.ATTRIBUTE9 = "ATTRIBUTE9";
            xCPRDIA.ATTRIBUTE10 = "ATTRIBUTE10";
            xCPRDIA.ATTRIBUTE11 = "ATTRIBUTE11";
            xCPRDIA.ATTRIBUTE12 = "ATTRIBUTE12";
            xCPRDIA.ATTRIBUTE13 = "ATTRIBUTE13";
            xCPRDIA.ATTRIBUTE14 = "ATTRIBUTE14";
            xCPRDIA.ATTRIBUTE15 = "ATTRIBUTE15";
            xCPRDIA.ATTRIBUTE16 = "ATTRIBUTE16";
            xCPRDIA.ATTRIBUTE17 = "ATTRIBUTE17";
            xCPRDIA.ATTRIBUTE18 = "ATTRIBUTE18";
            xCPRDIA.ATTRIBUTE19 = "ATTRIBUTE19";
            xCPRDIA.ATTRIBUTE20 = "ATTRIBUTE20";

            xCPRDIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCPRDIA.ATTRIBUTE_NUMBER2 = "ATTRIBUTE_NUMBER2";
            xCPRDIA.ATTRIBUTE_NUMBER3 = "ATTRIBUTE_NUMBER3";
            xCPRDIA.ATTRIBUTE_NUMBER4 = "ATTRIBUTE_NUMBER4";
            xCPRDIA.ATTRIBUTE_NUMBER5 = "ATTRIBUTE_NUMBER5";
            xCPRDIA.ATTRIBUTE_NUMBER6 = "ATTRIBUTE_NUMBER6";
            xCPRDIA.ATTRIBUTE_NUMBER7 = "ATTRIBUTE_NUMBER7";
            xCPRDIA.ATTRIBUTE_NUMBER8 = "ATTRIBUTE_NUMBER8";
            xCPRDIA.ATTRIBUTE_NUMBER9 = "ATTRIBUTE_NUMBER9";
            xCPRDIA.ATTRIBUTE_NUMBER10 = "ATTRIBUTE_NUMBER10";

            xCPRDIA.ATTRIBUTE_DATE2 = "ATTRIBUTE_DATE2";
            xCPRDIA.ATTRIBUTE_DATE3 = "ATTRIBUTE_DATE3";
            xCPRDIA.ATTRIBUTE_DATE4 = "ATTRIBUTE_DATE4";
            xCPRDIA.ATTRIBUTE_DATE5 = "ATTRIBUTE_DATE5";
            xCPRDIA.ATTRIBUTE_DATE6 = "ATTRIBUTE_DATE6";
            xCPRDIA.ATTRIBUTE_DATE7 = "ATTRIBUTE_DATE7";
            xCPRDIA.ATTRIBUTE_DATE8 = "ATTRIBUTE_DATE8";
            xCPRDIA.ATTRIBUTE_DATE9 = "ATTRIBUTE_DATE9";
            xCPRDIA.ATTRIBUTE_DATE10 = "ATTRIBUTE_DATE10";

            xCPRDIA.ATTRIBUTE_TIMESTAMP2 = "ATTRIBUTE_TIMESTAMP2";
            xCPRDIA.ATTRIBUTE_TIMESTAMP3 = "ATTRIBUTE_TIMESTAMP3";
            xCPRDIA.ATTRIBUTE_TIMESTAMP4 = "ATTRIBUTE_TIMESTAMP4";
            xCPRDIA.ATTRIBUTE_TIMESTAMP5 = "ATTRIBUTE_TIMESTAMP5";
            xCPRDIA.ATTRIBUTE_TIMESTAMP6 = "ATTRIBUTE_TIMESTAMP6";
            xCPRDIA.ATTRIBUTE_TIMESTAMP7 = "ATTRIBUTE_TIMESTAMP7";
            xCPRDIA.ATTRIBUTE_TIMESTAMP8 = "ATTRIBUTE_TIMESTAMP8";
            xCPRDIA.ATTRIBUTE_TIMESTAMP9 = "ATTRIBUTE_TIMESTAMP9";
            xCPRDIA.ATTRIBUTE_TIMESTAMP10 = "ATTRIBUTE_TIMESTAMP10";
            xCPRDIA.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCPRDIA.CREATION_DATE = "CREATION_DATE";            
            xCPRDIA.CREATE_BY = "CREATE_BY";
            xCPRDIA.LAST_UPDATE_BY = "LAST_UPDATE_BY";

            xCPRDIA.pkField = "";
            xCPRDIA.table = "xcust_por_req_dist_int_all";
        }
        public XcustPorReqDistIntAll setData(DataRow row, XcustLinfoxPrTbl xclfpt)
        {
            XcustPorReqDistIntAll item = new XcustPorReqDistIntAll();
            item.REQ_HEADER_INTERFACE_ID = row[xclfpt.PO_NUMBER].ToString();
            item.REQ_LINE_INTERFACE_ID = row[xclfpt.LINE_NUMBER].ToString();

            item.QTY = row[xclfpt.QTY].ToString();

            return item;
        }
        public String insert(XcustPorReqDistIntAll p)
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
                sql = "Insert Into " + xCPRDIA.table + "(" + xCPRDIA.ATTRIBUTE1 + "," + xCPRDIA.ATTRIBUTE_DATE1 + ","+
                    xCPRDIA.ATTRIBUTE_TIMESTAMP1 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 + "," +
                    xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 + "," +
                    xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 + "," + xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 + "," + xCPRDIA.REQ_LINE_INTERFACE_ID + "," +
                    xCPRDIA.REQ_HEADER_INTERFACE_ID + "," + xCPRDIA.PROCESS_FLAG + "," + xCPRDIA.REQ_DIST_INTERFACE_ID + "," +  
                    xCPRDIA.QTY  + "," +
                    xCPRDIA.ATTRIBUTE2 + "," + xCPRDIA.ATTRIBUTE3 + "," + xCPRDIA.ATTRIBUTE4 + "," +
                    xCPRDIA.ATTRIBUTE5 + "," + xCPRDIA.ATTRIBUTE6 + "," + xCPRDIA.ATTRIBUTE7 + "," +
                    xCPRDIA.ATTRIBUTE8 + "," + xCPRDIA.ATTRIBUTE9 + "," + xCPRDIA.ATTRIBUTE10 + "," +
                    xCPRDIA.ATTRIBUTE11 + "," + xCPRDIA.ATTRIBUTE12 + "," + xCPRDIA.ATTRIBUTE13 + "," +
                    xCPRDIA.ATTRIBUTE14 + "," + xCPRDIA.ATTRIBUTE15 + "," + xCPRDIA.ATTRIBUTE16 + "," +
                    xCPRDIA.ATTRIBUTE17 + "," + xCPRDIA.ATTRIBUTE18 + "," + xCPRDIA.ATTRIBUTE19 + "," +
                    xCPRDIA.ATTRIBUTE20 + "," +
                    xCPRDIA.ATTRIBUTE_NUMBER1 + "," + xCPRDIA.ATTRIBUTE_NUMBER2 + "," + xCPRDIA.ATTRIBUTE_NUMBER3 + "," +
                    xCPRDIA.ATTRIBUTE_NUMBER4 + "," + xCPRDIA.ATTRIBUTE_NUMBER5 + "," + xCPRDIA.ATTRIBUTE_NUMBER6 + "," +
                    xCPRDIA.ATTRIBUTE_NUMBER7 + "," + xCPRDIA.ATTRIBUTE_NUMBER8 + "," + xCPRDIA.ATTRIBUTE_NUMBER9 + "," +
                    xCPRDIA.ATTRIBUTE_NUMBER10 +"," +
                    xCPRDIA.ATTRIBUTE_DATE2 + "," + xCPRDIA.ATTRIBUTE_DATE3 + "," + xCPRDIA.ATTRIBUTE_DATE4 + "," +
                    xCPRDIA.ATTRIBUTE_DATE5 + "," + xCPRDIA.ATTRIBUTE_DATE6 + "," + xCPRDIA.ATTRIBUTE_DATE7 + "," +
                    xCPRDIA.ATTRIBUTE_DATE8 + "," + xCPRDIA.ATTRIBUTE_DATE9 + "," + xCPRDIA.ATTRIBUTE_DATE10 + "," +
                    xCPRDIA.ATTRIBUTE_TIMESTAMP2 + "," + xCPRDIA.ATTRIBUTE_TIMESTAMP3 + "," + xCPRDIA.ATTRIBUTE_TIMESTAMP4 + "," +
                    xCPRDIA.ATTRIBUTE_TIMESTAMP5 + "," + xCPRDIA.ATTRIBUTE_TIMESTAMP6 + "," + xCPRDIA.ATTRIBUTE_TIMESTAMP7 + "," +
                    xCPRDIA.ATTRIBUTE_TIMESTAMP8 + "," + xCPRDIA.ATTRIBUTE_TIMESTAMP9 + "," + xCPRDIA.ATTRIBUTE_TIMESTAMP10 + "," +
                    xCPRDIA.DISTRIBUTION_CURRENCY_AMT + "," + xCPRDIA.LAST_UPDATE_DATE + "," +
                    xCPRDIA.CREATION_DATE + "," + xCPRDIA.CREATE_BY + "," + xCPRDIA.LAST_UPDATE_BY +
                    ") " +
                    "Values('" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_DATE1 + "'," +
                    "getdate(),'" + p.CHARGE_ACCOUNT_SEGMENT1 + "','" + 
                    p.CHARGE_ACCOUNT_SEGMENT2 + "','" + p.CHARGE_ACCOUNT_SEGMENT3 + "','" + p.CHARGE_ACCOUNT_SEGMENT4 + "','" +
                    p.CHARGE_ACCOUNT_SEGMENT5 + "','" + p.CHARGE_ACCOUNT_SEGMENT6 + "'," + p.REQ_LINE_INTERFACE_ID + ",'" +
                    p.REQ_HEADER_INTERFACE_ID + "','" + p.PROCESS_FLAG + "','" + p.REQ_DIST_INTERFACE_ID + "'," + 
                    p.QTY + ",'" +
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
                    p.DISTRIBUTION_CURRENCY_AMT + "','" + p.LAST_UPDATE_DATE + "','" +
                    p.CREATION_DATE + "','" + p.CREATE_BY + "','" + p.LAST_UPDATE_BY + "'" +
                    ") ";
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
