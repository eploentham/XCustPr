using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustRcvTransactionsIntAllDB
    {
        public XcustRcvTransactionsIntAll xCRTIA;
        ConnectDB conn;
        private InitC initC;
        public XcustRcvTransactionsIntAllDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCRTIA = new XcustRcvTransactionsIntAll();
            xCRTIA.BUSINESS_UNIT = "BUSINESS_UNIT";
            
            xCRTIA.DOCUMENT_LINE_NUMBER = "DOCUMENT_LINE_NUMBER";
            xCRTIA.DOCUMENT_NUMBER = "DOCUMENT_NUMBER";
            xCRTIA.HEADER_INTERFACE_NUMBER = "HEADER_INTERFACE_NUMBER";
            
            xCRTIA.INTERFACE_SOURCE_CODE = "INTERFACE_SOURCE_CODE";
            xCRTIA.ITEM_CODE = "ITEM_CODE";
            
            xCRTIA.LINE_NUMBER = "LINE_NUMBER";
            xCRTIA.LOCATOR_CODE = "LOCATOR_CODE";
            xCRTIA.ORGANIZATION_CODE = "ORGANIZATION_CODE";
            xCRTIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCRTIA.QUANTITY = "QUANTITY";
            xCRTIA.RECEIPT_SOURCE_CODE = "RECEIPT_SOURCE_CODE";
            xCRTIA.SOURCE_DOCUMENT_CODE = "SOURCE_DOCUMENT_CODE";
            xCRTIA.SUBINVENTORY_CODE = "SUBINVENTORY_CODE";
            xCRTIA.TRANSACTION_DATE = "TRANSACTION_DATE";
            xCRTIA.TRANSACTION_TYPE = "TRANSACTION_TYPE";
            xCRTIA.UOM_CODE = "UOM_CODE";
            xCRTIA.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";


            xCRTIA.ATTRIBUTE1 = "ATTRIBUTE1";
            xCRTIA.ATTRIBUTE2 = "ATTRIBUTE2";
            xCRTIA.ATTRIBUTE3 = "ATTRIBUTE3";
            xCRTIA.ATTRIBUTE4 = "ATTRIBUTE4";
            xCRTIA.ATTRIBUTE5 = "ATTRIBUTE5";
            xCRTIA.ATTRIBUTE6 = "ATTRIBUTE6";
            xCRTIA.ATTRIBUTE7 = "ATTRIBUTE7";
            xCRTIA.ATTRIBUTE8 = "ATTRIBUTE8";
            xCRTIA.ATTRIBUTE9 = "ATTRIBUTE9";
            xCRTIA.ATTRIBUTE10 = "ATTRIBUTE10";
            xCRTIA.ATTRIBUTE11 = "ATTRIBUTE11";
            xCRTIA.ATTRIBUTE12 = "ATTRIBUTE12";
            xCRTIA.ATTRIBUTE13 = "ATTRIBUTE13";
            xCRTIA.ATTRIBUTE14 = "ATTRIBUTE14";
            xCRTIA.ATTRIBUTE15 = "ATTRIBUTE15";
            xCRTIA.ATTRIBUTE16 = "ATTRIBUTE16";
            xCRTIA.ATTRIBUTE17 = "ATTRIBUTE17";
            xCRTIA.ATTRIBUTE18 = "ATTRIBUTE18";
            xCRTIA.ATTRIBUTE19 = "ATTRIBUTE19";
            xCRTIA.ATTRIBUTE20 = "ATTRIBUTE20";

            xCRTIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCRTIA.ATTRIBUTE_NUMBER2 = "ATTRIBUTE_NUMBER2";
            xCRTIA.ATTRIBUTE_NUMBER3 = "ATTRIBUTE_NUMBER3";
            xCRTIA.ATTRIBUTE_NUMBER4 = "ATTRIBUTE_NUMBER4";
            xCRTIA.ATTRIBUTE_NUMBER5 = "ATTRIBUTE_NUMBER5";
            xCRTIA.ATTRIBUTE_NUMBER6 = "ATTRIBUTE_NUMBER6";
            xCRTIA.ATTRIBUTE_NUMBER7 = "ATTRIBUTE_NUMBER7";
            xCRTIA.ATTRIBUTE_NUMBER8 = "ATTRIBUTE_NUMBER8";
            xCRTIA.ATTRIBUTE_NUMBER9 = "ATTRIBUTE_NUMBER9";
            xCRTIA.ATTRIBUTE_NUMBER10 = "ATTRIBUTE_NUMBER10";

            xCRTIA.ATTRIBUTE_DATE1 = "ATTRIBUTE_DATE1";
            xCRTIA.ATTRIBUTE_DATE2 = "ATTRIBUTE_DATE2";
            xCRTIA.ATTRIBUTE_DATE3 = "ATTRIBUTE_DATE3";
            xCRTIA.ATTRIBUTE_DATE4 = "ATTRIBUTE_DATE4";
            xCRTIA.ATTRIBUTE_DATE5 = "ATTRIBUTE_DATE5";
            xCRTIA.ATTRIBUTE_DATE6 = "ATTRIBUTE_DATE6";
            xCRTIA.ATTRIBUTE_DATE7 = "ATTRIBUTE_DATE7";
            xCRTIA.ATTRIBUTE_DATE8 = "ATTRIBUTE_DATE8";
            xCRTIA.ATTRIBUTE_DATE9 = "ATTRIBUTE_DATE9";
            xCRTIA.ATTRIBUTE_DATE10 = "ATTRIBUTE_DATE10";

            xCRTIA.ATTRIBUTE_TIMESTAMP2 = "ATTRIBUTE_TIMESTAMP2";
            xCRTIA.ATTRIBUTE_TIMESTAMP3 = "ATTRIBUTE_TIMESTAMP3";
            xCRTIA.ATTRIBUTE_TIMESTAMP4 = "ATTRIBUTE_TIMESTAMP4";
            xCRTIA.ATTRIBUTE_TIMESTAMP5 = "ATTRIBUTE_TIMESTAMP5";
            xCRTIA.ATTRIBUTE_TIMESTAMP6 = "ATTRIBUTE_TIMESTAMP6";
            xCRTIA.ATTRIBUTE_TIMESTAMP7 = "ATTRIBUTE_TIMESTAMP7";
            xCRTIA.ATTRIBUTE_TIMESTAMP8 = "ATTRIBUTE_TIMESTAMP8";
            xCRTIA.ATTRIBUTE_TIMESTAMP9 = "ATTRIBUTE_TIMESTAMP9";
            xCRTIA.ATTRIBUTE_TIMESTAMP10 = "ATTRIBUTE_TIMESTAMP10";
            xCRTIA.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCRTIA.CREATION_DATE = "CREATION_DATE";
            xCRTIA.IMPORT_SOURCE = "IMPORT_SOURCE";
            xCRTIA.CREATE_BY = "CREATE_BY";
            xCRTIA.LAST_UPDATE_BY = "LAST_UPDATE_BY";
            //xCRTIA.IMPORT_SOURCE = "IMPORT_SOURCE";

            xCRTIA.pkField = "";
            xCRTIA.table = "XCUST_RCV_TRANSACTIONS_INT_ALL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCRTIA.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String insert(XcustRcvTransactionsIntAll p, String pathLog)
        {
            String sql = "", chk = "";
            try
            {
                String seq = "000000";
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

                sql = "Insert Into " + xCRTIA.table + "(" + xCRTIA.BUSINESS_UNIT + "," + xCRTIA.DOCUMENT_LINE_NUMBER + "," + xCRTIA.DOCUMENT_NUMBER + "," +
                    xCRTIA.HEADER_INTERFACE_NUMBER + "," + xCRTIA.INTERFACE_SOURCE_CODE + "," + xCRTIA.ITEM_CODE + "," +
                    xCRTIA.LINE_NUMBER + "," + xCRTIA.LOCATOR_CODE + "," + xCRTIA.ORGANIZATION_CODE + "," +
                    xCRTIA.PROCESS_FLAG + "," + xCRTIA.QUANTITY + "," + xCRTIA.RECEIPT_SOURCE_CODE + "," +
                    xCRTIA.SOURCE_DOCUMENT_CODE + "," + xCRTIA.SUBINVENTORY_CODE + "," + xCRTIA.TRANSACTION_DATE + "," +
                    xCRTIA.TRANSACTION_TYPE + "," + xCRTIA.UOM_CODE + "," + xCRTIA.ATTRIBUTE_CATEGORY + "," +
                    xCRTIA.ATTRIBUTE2 + "," + xCRTIA.ATTRIBUTE3 + "," + xCRTIA.ATTRIBUTE4 + "," +
                    xCRTIA.ATTRIBUTE5 + "," + xCRTIA.ATTRIBUTE6 + "," + xCRTIA.ATTRIBUTE7 + "," +
                    xCRTIA.ATTRIBUTE8 + "," + xCRTIA.ATTRIBUTE9 + "," + xCRTIA.ATTRIBUTE10 + "," +
                    xCRTIA.ATTRIBUTE11 + "," + xCRTIA.ATTRIBUTE12 + "," + xCRTIA.ATTRIBUTE13 + "," +
                    xCRTIA.ATTRIBUTE14 + "," + xCRTIA.ATTRIBUTE15 + "," + xCRTIA.ATTRIBUTE16 + "," +
                    xCRTIA.ATTRIBUTE17 + "," + xCRTIA.ATTRIBUTE18 + "," + xCRTIA.ATTRIBUTE19 + "," +
                    xCRTIA.ATTRIBUTE20 + "," +
                    xCRTIA.ATTRIBUTE_NUMBER1 + "," + xCRTIA.ATTRIBUTE_NUMBER2 + "," + xCRTIA.ATTRIBUTE_NUMBER3 + "," +
                    xCRTIA.ATTRIBUTE_NUMBER4 + "," + xCRTIA.ATTRIBUTE_NUMBER5 + "," + xCRTIA.ATTRIBUTE_NUMBER6 + "," +
                    xCRTIA.ATTRIBUTE_NUMBER7 + "," + xCRTIA.ATTRIBUTE_NUMBER8 + "," + xCRTIA.ATTRIBUTE_NUMBER9 + "," +
                    xCRTIA.ATTRIBUTE_NUMBER10 + "," + xCRTIA.ATTRIBUTE_DATE1 + "," +
                    xCRTIA.ATTRIBUTE_DATE2 + "," + xCRTIA.ATTRIBUTE_DATE3 + "," + xCRTIA.ATTRIBUTE_DATE4 + "," +
                    xCRTIA.ATTRIBUTE_DATE5 + "," + xCRTIA.ATTRIBUTE_DATE6 + "," + xCRTIA.ATTRIBUTE_DATE7 + "," +
                    xCRTIA.ATTRIBUTE_DATE8 + "," + xCRTIA.ATTRIBUTE_DATE9 + "," + xCRTIA.ATTRIBUTE_DATE10 + "," +
                    xCRTIA.ATTRIBUTE_TIMESTAMP3 + "," + xCRTIA.ATTRIBUTE_TIMESTAMP4 + "," +
                    xCRTIA.ATTRIBUTE_TIMESTAMP5 + "," + xCRTIA.ATTRIBUTE_TIMESTAMP6 + "," + xCRTIA.ATTRIBUTE_TIMESTAMP7 + "," +
                    xCRTIA.ATTRIBUTE_TIMESTAMP8 + "," + xCRTIA.ATTRIBUTE_TIMESTAMP9 + "," + xCRTIA.ATTRIBUTE_TIMESTAMP10 + "," +
                    xCRTIA.LAST_UPDATE_DATE + "," + xCRTIA.CREATION_DATE + "," + xCRTIA.IMPORT_SOURCE + "," +
                    xCRTIA.LAST_UPDATE_BY + "," + xCRTIA.ATTRIBUTE1 +
                    ") " +
                    "Values ('" + p.BUSINESS_UNIT + "','" + p.DOCUMENT_LINE_NUMBER + "','" + p.DOCUMENT_NUMBER + "','" +
                    p.HEADER_INTERFACE_NUMBER + "','" + p.INTERFACE_SOURCE_CODE + "','" + p.ITEM_CODE + "','" +
                    p.LINE_NUMBER + "','" + p.LOCATOR_CODE + "','" + p.ORGANIZATION_CODE + "','" +
                    p.PROCESS_FLAG + "','" + p.QUANTITY + "','" + p.RECEIPT_SOURCE_CODE + "','" +
                    p.SOURCE_DOCUMENT_CODE + "','" + p.SUBINVENTORY_CODE + "','" + p.TRANSACTION_DATE + "','" +
                    p.TRANSACTION_TYPE + "','" + p.UOM_CODE + "','" + p.ATTRIBUTE_CATEGORY + "','" +
                    p.ATTRIBUTE2 + "','" + p.ATTRIBUTE3 + "','" + p.ATTRIBUTE4 + "','" +
                    p.ATTRIBUTE5 + "','" + p.ATTRIBUTE6 + "','" + p.ATTRIBUTE7 + "','" +
                    p.ATTRIBUTE8 + "','" + p.ATTRIBUTE9 + "','" + p.ATTRIBUTE10 + "','" +
                    p.ATTRIBUTE11 + "','" + p.ATTRIBUTE12 + "','" + p.ATTRIBUTE13 + "','" +
                    p.ATTRIBUTE14 + "','" + p.ATTRIBUTE15 + "','" + p.ATTRIBUTE16 + "','" +
                    p.ATTRIBUTE17 + "','" + p.ATTRIBUTE18 + "','" + p.ATTRIBUTE19 + "','" +
                    p.ATTRIBUTE20 + "'," +
                    p.ATTRIBUTE_NUMBER1 + "," + p.ATTRIBUTE_NUMBER2 + "," + p.ATTRIBUTE_NUMBER3 + "," +
                    p.ATTRIBUTE_NUMBER4 + "," + p.ATTRIBUTE_NUMBER5 + "," + p.ATTRIBUTE_NUMBER6 + "," +
                    p.ATTRIBUTE_NUMBER7 + "," + p.ATTRIBUTE_NUMBER8 + "," + p.ATTRIBUTE_NUMBER9 + "," +
                    p.ATTRIBUTE_NUMBER10 + ",'" + p.ATTRIBUTE_DATE1 + "','" +
                    p.ATTRIBUTE_DATE2 + "','" + p.ATTRIBUTE_DATE3 + "','" + p.ATTRIBUTE_DATE4 + "','" +
                    p.ATTRIBUTE_DATE5 + "','" + p.ATTRIBUTE_DATE6 + "','" + p.ATTRIBUTE_DATE7 + "','" +
                    p.ATTRIBUTE_DATE8 + "','" + p.ATTRIBUTE_DATE9 + "','" + p.ATTRIBUTE_DATE10 + "','" +
                    p.ATTRIBUTE_TIMESTAMP3 + "','" + p.ATTRIBUTE_TIMESTAMP4 + "','" +
                    p.ATTRIBUTE_TIMESTAMP5 + "','" + p.ATTRIBUTE_TIMESTAMP6 + "','" + p.ATTRIBUTE_TIMESTAMP7 + "','" +
                    p.ATTRIBUTE_TIMESTAMP8 + "','" + p.ATTRIBUTE_TIMESTAMP9 + "','" + p.ATTRIBUTE_TIMESTAMP10 + "','" +
                    p.LAST_UPDATE_DATE + "',getdate(),'" + p.IMPORT_SOURCE + "','" +
                    p.LAST_UPDATE_BY + "','" + p.ATTRIBUTE1 +
                    "')";
                chk = conn.ExecuteNonQuery(sql, "kfc_po", pathLog);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }
            return chk;
        }
    }
}
