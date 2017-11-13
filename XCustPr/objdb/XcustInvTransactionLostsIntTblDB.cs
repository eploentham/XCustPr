using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustInvTransactionLostsIntTblDB
    {
        ConnectDB conn;
        public XcustInvTransactionLostsIntTbl xCITLIT;

        public XcustInvTransactionLostsIntTblDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            xCITLIT = new XcustInvTransactionLostsIntTbl();

            xCITLIT.ATTRIBUTE_CATEGORY = "ATTRIBUTE_CATEGORY";
            xCITLIT.HEADER_INTERFACE_NUMBER = "HEADER_INTERFACE_NUMBER";
            xCITLIT.LINE_NUMBER = "LINE_NUMBER";
            xCITLIT.LOT_EXPIRATION_DATE = "LOT_EXPIRATION_DATE";
            xCITLIT.LOT_NUMBER = "LOT_NUMBER";
            xCITLIT.PRIMARY_QUANTITY = "PRIMARY_QUANTITY";
            xCITLIT.PROCESS_FLAG = "PROCESS_FLAG";
            xCITLIT.TRANSACTION_QUANTITY = "TRANSACTION_QUANTITY";

            xCITLIT.ATTRIBUTE1 = "ATTRIBUTE1";
            xCITLIT.ATTRIBUTE2 = "ATTRIBUTE2";
            xCITLIT.ATTRIBUTE3 = "ATTRIBUTE3";
            xCITLIT.ATTRIBUTE4 = "ATTRIBUTE4";
            xCITLIT.ATTRIBUTE5 = "ATTRIBUTE5";
            xCITLIT.ATTRIBUTE6 = "ATTRIBUTE6";
            xCITLIT.ATTRIBUTE7 = "ATTRIBUTE7";
            xCITLIT.ATTRIBUTE8 = "ATTRIBUTE8";
            xCITLIT.ATTRIBUTE9 = "ATTRIBUTE9";
            xCITLIT.ATTRIBUTE10 = "ATTRIBUTE10";
            xCITLIT.ATTRIBUTE11 = "ATTRIBUTE11";
            xCITLIT.ATTRIBUTE12 = "ATTRIBUTE12";
            xCITLIT.ATTRIBUTE13 = "ATTRIBUTE13";
            xCITLIT.ATTRIBUTE14 = "ATTRIBUTE14";
            xCITLIT.ATTRIBUTE15 = "ATTRIBUTE15";
            xCITLIT.ATTRIBUTE16 = "ATTRIBUTE16";
            xCITLIT.ATTRIBUTE17 = "ATTRIBUTE17";
            xCITLIT.ATTRIBUTE18 = "ATTRIBUTE18";
            xCITLIT.ATTRIBUTE19 = "ATTRIBUTE19";
            xCITLIT.ATTRIBUTE20 = "ATTRIBUTE20";

            xCITLIT.ATTRIBUTE_NUMBER1 = "ATTRIBUTE_NUMBER1";
            xCITLIT.ATTRIBUTE_NUMBER2 = "ATTRIBUTE_NUMBER2";
            xCITLIT.ATTRIBUTE_NUMBER3 = "ATTRIBUTE_NUMBER3";
            xCITLIT.ATTRIBUTE_NUMBER4 = "ATTRIBUTE_NUMBER4";
            xCITLIT.ATTRIBUTE_NUMBER5 = "ATTRIBUTE_NUMBER5";
            xCITLIT.ATTRIBUTE_NUMBER6 = "ATTRIBUTE_NUMBER6";
            xCITLIT.ATTRIBUTE_NUMBER7 = "ATTRIBUTE_NUMBER7";
            xCITLIT.ATTRIBUTE_NUMBER8 = "ATTRIBUTE_NUMBER8";
            xCITLIT.ATTRIBUTE_NUMBER9 = "ATTRIBUTE_NUMBER9";
            xCITLIT.ATTRIBUTE_NUMBER10 = "ATTRIBUTE_NUMBER10";

            xCITLIT.ATTRIBUTE_DATE1 = "ATTRIBUTE_DATE1";
            xCITLIT.ATTRIBUTE_DATE2 = "ATTRIBUTE_DATE2";
            xCITLIT.ATTRIBUTE_DATE3 = "ATTRIBUTE_DATE3";
            xCITLIT.ATTRIBUTE_DATE4 = "ATTRIBUTE_DATE4";
            xCITLIT.ATTRIBUTE_DATE5 = "ATTRIBUTE_DATE5";
            xCITLIT.ATTRIBUTE_DATE6 = "ATTRIBUTE_DATE6";
            xCITLIT.ATTRIBUTE_DATE7 = "ATTRIBUTE_DATE7";
            xCITLIT.ATTRIBUTE_DATE8 = "ATTRIBUTE_DATE8";
            xCITLIT.ATTRIBUTE_DATE9 = "ATTRIBUTE_DATE9";
            xCITLIT.ATTRIBUTE_DATE10 = "ATTRIBUTE_DATE10";

            xCITLIT.ATTRIBUTE_TIMESTAMP2 = "ATTRIBUTE_TIMESTAMP2";
            xCITLIT.ATTRIBUTE_TIMESTAMP3 = "ATTRIBUTE_TIMESTAMP3";
            xCITLIT.ATTRIBUTE_TIMESTAMP4 = "ATTRIBUTE_TIMESTAMP4";
            xCITLIT.ATTRIBUTE_TIMESTAMP5 = "ATTRIBUTE_TIMESTAMP5";
            xCITLIT.ATTRIBUTE_TIMESTAMP6 = "ATTRIBUTE_TIMESTAMP6";
            xCITLIT.ATTRIBUTE_TIMESTAMP7 = "ATTRIBUTE_TIMESTAMP7";
            xCITLIT.ATTRIBUTE_TIMESTAMP8 = "ATTRIBUTE_TIMESTAMP8";
            xCITLIT.ATTRIBUTE_TIMESTAMP9 = "ATTRIBUTE_TIMESTAMP9";
            xCITLIT.ATTRIBUTE_TIMESTAMP10 = "ATTRIBUTE_TIMESTAMP10";
            xCITLIT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCITLIT.CREATION_DATE = "CREATION_DATE";
            xCITLIT.IMPORT_SOURCE = "IMPORT_SOURCE";
            xCITLIT.CREATE_BY = "CREATE_BY";
            xCITLIT.LAST_UPDATE_BY = "LAST_UPDATE_BY";

            xCITLIT.table = "XCUST_INV_TRANSACTION_LOTS_INT_TBL";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCITLIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public String insert(XcustInvTransactionLostsIntTbl p)
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

                sql = "Insert Into  " + xCITLIT.table + "(" + xCITLIT.ATTRIBUTE_CATEGORY+","+ xCITLIT.HEADER_INTERFACE_NUMBER+","+ xCITLIT.LINE_NUMBER+","+
                    xCITLIT.LOT_EXPIRATION_DATE+","+ xCITLIT.LOT_NUMBER+","+ xCITLIT.PRIMARY_QUANTITY+","+ 
                    xCITLIT.TRANSACTION_QUANTITY+","+ xCITLIT.ATTRIBUTE1+","+
                    xCITLIT.ATTRIBUTE2 + "," + xCITLIT.ATTRIBUTE3 + "," + xCITLIT.ATTRIBUTE4 + "," +
                    xCITLIT.ATTRIBUTE5 + "," + xCITLIT.ATTRIBUTE6 + "," + xCITLIT.ATTRIBUTE7 + "," +
                    xCITLIT.ATTRIBUTE8 + "," + xCITLIT.ATTRIBUTE9 + "," + xCITLIT.ATTRIBUTE10 + "," +
                    xCITLIT.ATTRIBUTE11 + "," + xCITLIT.ATTRIBUTE12 + "," + xCITLIT.ATTRIBUTE13 + "," +
                    xCITLIT.ATTRIBUTE14 + "," + xCITLIT.ATTRIBUTE15 + "," + xCITLIT.ATTRIBUTE16 + "," +
                    xCITLIT.ATTRIBUTE17 + "," + xCITLIT.ATTRIBUTE18 + "," + xCITLIT.ATTRIBUTE19 + "," +
                    xCITLIT.ATTRIBUTE20 + "," +
                    xCITLIT.ATTRIBUTE_NUMBER1 + "," + xCITLIT.ATTRIBUTE_NUMBER2 + "," + xCITLIT.ATTRIBUTE_NUMBER3 + "," +
                    xCITLIT.ATTRIBUTE_NUMBER4 + "," + xCITLIT.ATTRIBUTE_NUMBER5 + "," + xCITLIT.ATTRIBUTE_NUMBER6 + "," +
                    xCITLIT.ATTRIBUTE_NUMBER7 + "," + xCITLIT.ATTRIBUTE_NUMBER8 + "," + xCITLIT.ATTRIBUTE_NUMBER9 + "," +
                    xCITLIT.ATTRIBUTE_NUMBER10 + "," + xCITLIT.ATTRIBUTE_DATE1+","+
                    xCITLIT.ATTRIBUTE_DATE2 + "," + xCITLIT.ATTRIBUTE_DATE3 + "," + xCITLIT.ATTRIBUTE_DATE4 + "," +
                    xCITLIT.ATTRIBUTE_DATE5 + "," + xCITLIT.ATTRIBUTE_DATE6 + "," + xCITLIT.ATTRIBUTE_DATE7 + "," +
                    xCITLIT.ATTRIBUTE_DATE8 + "," + xCITLIT.ATTRIBUTE_DATE9 + "," + xCITLIT.ATTRIBUTE_DATE10 + "," +
                    xCITLIT.ATTRIBUTE_TIMESTAMP3 + "," + xCITLIT.ATTRIBUTE_TIMESTAMP4 + "," +
                    xCITLIT.ATTRIBUTE_TIMESTAMP5 + "," + xCITLIT.ATTRIBUTE_TIMESTAMP6 + "," + xCITLIT.ATTRIBUTE_TIMESTAMP7 + "," +
                    xCITLIT.ATTRIBUTE_TIMESTAMP8 + "," + xCITLIT.ATTRIBUTE_TIMESTAMP9 + "," + xCITLIT.ATTRIBUTE_TIMESTAMP10 + "," +
                    xCITLIT.LAST_UPDATE_DATE + "," + xCITLIT.CREATION_DATE + "," + xCITLIT.IMPORT_SOURCE + "," +
                    xCITLIT.LAST_UPDATE_BY + 
                    ")" +
                    "Values ('"+p.ATTRIBUTE_CATEGORY + "','" + p.HEADER_INTERFACE_NUMBER + "','" + p.LINE_NUMBER + "','" +
                    p.LOT_EXPIRATION_DATE + "','" + p.LOT_NUMBER + "','" + p.PRIMARY_QUANTITY + "','" +
                    p.TRANSACTION_QUANTITY + "','" + p.ATTRIBUTE1 + "','" +
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
                    p.ATTRIBUTE_NUMBER10 + ",'" +p.ATTRIBUTE_DATE1+"','"+
                    p.ATTRIBUTE_DATE2 + "','" + p.ATTRIBUTE_DATE3 + "','" + p.ATTRIBUTE_DATE4 + "','" +
                    p.ATTRIBUTE_DATE5 + "','" + p.ATTRIBUTE_DATE6 + "','" + p.ATTRIBUTE_DATE7 + "','" +
                    p.ATTRIBUTE_DATE8 + "','" + p.ATTRIBUTE_DATE9 + "','" + p.ATTRIBUTE_DATE10 + "','" +
                    p.ATTRIBUTE_TIMESTAMP3 + "','" + p.ATTRIBUTE_TIMESTAMP4 + "','" +
                    p.ATTRIBUTE_TIMESTAMP5 + "','" + p.ATTRIBUTE_TIMESTAMP6 + "','" + p.ATTRIBUTE_TIMESTAMP7 + "','" +
                    p.ATTRIBUTE_TIMESTAMP8 + "','" + p.ATTRIBUTE_TIMESTAMP9 + "','" + p.ATTRIBUTE_TIMESTAMP10 + "','" +
                    p.LAST_UPDATE_DATE + "','" + p.CREATION_DATE + "','" + p.IMPORT_SOURCE + "','" +
                    p.LAST_UPDATE_BY +"')";
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
