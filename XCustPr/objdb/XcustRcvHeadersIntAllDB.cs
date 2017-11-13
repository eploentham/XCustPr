using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustRcvHeadersIntAllDB
    {
        ConnectDB conn;
        public XcustRcvHeadersIntAll xCRHIA;

        public XcustRcvHeadersIntAllDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCRHIA = new XcustRcvHeadersIntAll();

            xCRHIA.ASN_TYPE = "ASN_TYPE";
            xCRHIA.BUSINESS_UNIT = "BUSINESS_UNIT";
            
            xCRHIA.HEADER_INTERFACE_NUMBER = "HEADER_INTERFACE_NUMBER";            
            
            xCRHIA.PROCESS_FLAG = "PROCESS_FLAG";
            xCRHIA.RECEIPT_NUM = "RECEIPT_NUM";
            xCRHIA.RECEIPT_SOURCE_CODE = "RECEIPT_SOURCE_CODE";
            xCRHIA.SHIPTO_ORGANIZATION_CODE = "SHIPTO_ORGANIZATION_CODE";
            xCRHIA.TRANSACTION_DATE = "TRANSACTION_DATE";
            xCRHIA.TRANSACTION_TYPE = "TRANSACTION_TYPE";
            xCRHIA.VENDOR_NUM = "VENDOR_NUM";
            xCRHIA.VENDOR_SITE_CODE = "VENDOR_SITE_CODE";

            xCRHIA.ATTRIBUTE1 = "ATTRIBUTE1";
            xCRHIA.ATTRIBUTE2 = "ATTRIBUTE2";
            xCRHIA.ATTRIBUTE3 = "ATTRIBUTE3";
            xCRHIA.ATTRIBUTE4 = "ATTRIBUTE4";
            xCRHIA.ATTRIBUTE5 = "ATTRIBUTE5";
            xCRHIA.ATTRIBUTE6 = "ATTRIBUTE6";
            xCRHIA.ATTRIBUTE7 = "ATTRIBUTE7";
            xCRHIA.ATTRIBUTE8 = "ATTRIBUTE8";
            xCRHIA.ATTRIBUTE9 = "ATTRIBUTE9";
            xCRHIA.ATTRIBUTE10 = "ATTRIBUTE10";
            xCRHIA.ATTRIBUTE11 = "ATTRIBUTE11";
            xCRHIA.ATTRIBUTE12 = "ATTRIBUTE12";
            xCRHIA.ATTRIBUTE13 = "ATTRIBUTE13";
            xCRHIA.ATTRIBUTE14 = "ATTRIBUTE14";
            xCRHIA.ATTRIBUTE15 = "ATTRIBUTE15";
            xCRHIA.ATTRIBUTE16 = "ATTRIBUTE16";
            xCRHIA.ATTRIBUTE17 = "ATTRIBUTE17";
            xCRHIA.ATTRIBUTE18 = "ATTRIBUTE18";
            xCRHIA.ATTRIBUTE19 = "ATTRIBUTE19";
            xCRHIA.ATTRIBUTE20 = "ATTRIBUTE20";

            xCRHIA.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCRHIA.ATTRIBUTE_NUMBER2 = "ATTRIBUTE_NUMBER2";
            xCRHIA.ATTRIBUTE_NUMBER3 = "ATTRIBUTE_NUMBER3";
            xCRHIA.ATTRIBUTE_NUMBER4 = "ATTRIBUTE_NUMBER4";
            xCRHIA.ATTRIBUTE_NUMBER5 = "ATTRIBUTE_NUMBER5";
            xCRHIA.ATTRIBUTE_NUMBER6 = "ATTRIBUTE_NUMBER6";
            xCRHIA.ATTRIBUTE_NUMBER7 = "ATTRIBUTE_NUMBER7";
            xCRHIA.ATTRIBUTE_NUMBER8 = "ATTRIBUTE_NUMBER8";
            xCRHIA.ATTRIBUTE_NUMBER9 = "ATTRIBUTE_NUMBER9";
            xCRHIA.ATTRIBUTE_NUMBER10 = "ATTRIBUTE_NUMBER10";

            xCRHIA.ATTRIBUTE_DATE2 = "ATTRIBUTE_DATE2";
            xCRHIA.ATTRIBUTE_DATE3 = "ATTRIBUTE_DATE3";
            xCRHIA.ATTRIBUTE_DATE4 = "ATTRIBUTE_DATE4";
            xCRHIA.ATTRIBUTE_DATE5 = "ATTRIBUTE_DATE5";
            xCRHIA.ATTRIBUTE_DATE6 = "ATTRIBUTE_DATE6";
            xCRHIA.ATTRIBUTE_DATE7 = "ATTRIBUTE_DATE7";
            xCRHIA.ATTRIBUTE_DATE8 = "ATTRIBUTE_DATE8";
            xCRHIA.ATTRIBUTE_DATE9 = "ATTRIBUTE_DATE9";
            xCRHIA.ATTRIBUTE_DATE10 = "ATTRIBUTE_DATE10";

            xCRHIA.ATTRIBUTE_TIMESTAMP2 = "ATTRIBUTE_TIMESTAMP2";
            xCRHIA.ATTRIBUTE_TIMESTAMP3 = "ATTRIBUTE_TIMESTAMP3";
            xCRHIA.ATTRIBUTE_TIMESTAMP4 = "ATTRIBUTE_TIMESTAMP4";
            xCRHIA.ATTRIBUTE_TIMESTAMP5 = "ATTRIBUTE_TIMESTAMP5";
            xCRHIA.ATTRIBUTE_TIMESTAMP6 = "ATTRIBUTE_TIMESTAMP6";
            xCRHIA.ATTRIBUTE_TIMESTAMP7 = "ATTRIBUTE_TIMESTAMP7";
            xCRHIA.ATTRIBUTE_TIMESTAMP8 = "ATTRIBUTE_TIMESTAMP8";
            xCRHIA.ATTRIBUTE_TIMESTAMP9 = "ATTRIBUTE_TIMESTAMP9";
            xCRHIA.ATTRIBUTE_TIMESTAMP10 = "ATTRIBUTE_TIMESTAMP10";
            xCRHIA.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCRHIA.CREATION_DATE = "CREATION_DATE";
            xCRHIA.IMPORT_SOURCE = "IMPORT_SOURCE";
            xCRHIA.CREATE_BY = "CREATE_BY";
            xCRHIA.LAST_UPDATE_BY = "LAST_UPDATE_BY";
            xCRHIA.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";

            xCRHIA.pkField = "";
            xCRHIA.table = "XCUST_RCV_HEADERS_INT_ALL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCRHIA.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String insert(XcustRcvHeadersIntAll p)
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

                sql = "Insert Into " + xCRHIA.table + "(" + xCRHIA.ASN_TYPE + "," + xCRHIA.ATTRIBUTE1 + "," + xCRHIA.ATTRIBUTE_CATEGORY + "," +
                    xCRHIA.BUSINESS_UNIT + "," + xCRHIA.CREATE_BY + "," +
                    xCRHIA.HEADER_INTERFACE_NUMBER + "," + xCRHIA.PROCESS_FLAG + "," + xCRHIA.RECEIPT_NUM + "," +
                    xCRHIA.RECEIPT_SOURCE_CODE + "," + xCRHIA.SHIPTO_ORGANIZATION_CODE + "," + xCRHIA.TRANSACTION_DATE + "," +
                    xCRHIA.TRANSACTION_TYPE + "," + xCRHIA.VENDOR_NUM + "," + xCRHIA.VENDOR_SITE_CODE + "," +
                    xCRHIA.ATTRIBUTE2 + "," + xCRHIA.ATTRIBUTE3 + "," + xCRHIA.ATTRIBUTE4 + "," +
                    xCRHIA.ATTRIBUTE5 + "," + xCRHIA.ATTRIBUTE6 + "," + xCRHIA.ATTRIBUTE7 + "," +
                    xCRHIA.ATTRIBUTE8 + "," + xCRHIA.ATTRIBUTE9 + "," + xCRHIA.ATTRIBUTE10 + "," +
                    xCRHIA.ATTRIBUTE11 + "," + xCRHIA.ATTRIBUTE12 + "," + xCRHIA.ATTRIBUTE13 + "," +
                    xCRHIA.ATTRIBUTE14 + "," + xCRHIA.ATTRIBUTE15 + "," + xCRHIA.ATTRIBUTE16 + "," +
                    xCRHIA.ATTRIBUTE17 + "," + xCRHIA.ATTRIBUTE18 + "," + xCRHIA.ATTRIBUTE19 + "," +
                    xCRHIA.ATTRIBUTE20 + "," +
                    xCRHIA.ATTRIBUTE_NUMBER1 + "," + xCRHIA.ATTRIBUTE_NUMBER2 + "," + xCRHIA.ATTRIBUTE_NUMBER3 + "," +
                    xCRHIA.ATTRIBUTE_NUMBER4 + "," + xCRHIA.ATTRIBUTE_NUMBER5 + "," + xCRHIA.ATTRIBUTE_NUMBER6 + "," +
                    xCRHIA.ATTRIBUTE_NUMBER7 + "," + xCRHIA.ATTRIBUTE_NUMBER8 + "," + xCRHIA.ATTRIBUTE_NUMBER9 + "," +
                    xCRHIA.ATTRIBUTE_NUMBER10 + "," +
                    xCRHIA.ATTRIBUTE_DATE2 + "," + xCRHIA.ATTRIBUTE_DATE3 + "," + xCRHIA.ATTRIBUTE_DATE4 + "," +
                    xCRHIA.ATTRIBUTE_DATE5 + "," + xCRHIA.ATTRIBUTE_DATE6 + "," + xCRHIA.ATTRIBUTE_DATE7 + "," +
                    xCRHIA.ATTRIBUTE_DATE8 + "," + xCRHIA.ATTRIBUTE_DATE9 + "," + xCRHIA.ATTRIBUTE_DATE10 + "," +
                    xCRHIA.ATTRIBUTE_TIMESTAMP3 + "," + xCRHIA.ATTRIBUTE_TIMESTAMP4 + "," +
                    xCRHIA.ATTRIBUTE_TIMESTAMP5 + "," + xCRHIA.ATTRIBUTE_TIMESTAMP6 + "," + xCRHIA.ATTRIBUTE_TIMESTAMP7 + "," +
                    xCRHIA.ATTRIBUTE_TIMESTAMP8 + "," + xCRHIA.ATTRIBUTE_TIMESTAMP9 + "," + xCRHIA.ATTRIBUTE_TIMESTAMP10 + "," +
                    xCRHIA.LAST_UPDATE_DATE + "," + xCRHIA.CREATION_DATE + "," + xCRHIA.IMPORT_SOURCE + "," +
                    xCRHIA.LAST_UPDATE_BY + 
                    ") "+
                    "Values ('"+ p.ASN_TYPE + "','" + p.ATTRIBUTE1 + "','" + p.ATTRIBUTE_CATEGORY + "','" +
                    p.BUSINESS_UNIT + "','" + p.CREATE_BY + "','" + 
                    p.HEADER_INTERFACE_NUMBER + "','" + p.PROCESS_FLAG + "','" + p.RECEIPT_NUM + "','" +
                    p.RECEIPT_SOURCE_CODE + "','" + p.SHIPTO_ORGANIZATION_CODE + "','" + p.TRANSACTION_DATE + "','" +
                    p.TRANSACTION_TYPE + "','" + p.VENDOR_NUM + "','" + p.VENDOR_SITE_CODE + "','" +
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
                    p.LAST_UPDATE_DATE + "',getdate(),'" + p.IMPORT_SOURCE + "'" +
                    p.LAST_UPDATE_BY + 
                    ")";
                chk = conn.ExecuteNonQuery(sql, "kfc_po");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error " + ex.ToString(), "insert Doctor");
            }
            return chk;
        }
    }
}
