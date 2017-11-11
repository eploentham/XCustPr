using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustBlanketAgreementLinesTblDB
    {
        public XcustBlanketAgreementLinesTbl xCBALT;
        ConnectDB conn;
        private InitC initC;
        public XcustBlanketAgreementLinesTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCBALT = new XcustBlanketAgreementLinesTbl();
            xCBALT.ALLOW_PRICE_OVERIDE = "ALLOW_PRICE_OVERIDE";
            xCBALT.CREATION_DATE = "CREATION_DATE";
            xCBALT.CURRENCY_CODE = "CURRENCY_CODE";
            xCBALT.DESCRIPTION = "DESCRIPTION";
            xCBALT.EXPIRATION_DATE = "EXPIRATION_DATE";
            xCBALT.ITEM_CODE = "ITEM_CODE";
            xCBALT.ITEM_ID = "ITEM_ID";
            xCBALT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCBALT.LINE_AGREEMENT_AMT = "LINE_AGREEMENT_AMT";
            xCBALT.LINE_AGREEMENT_QTY = "LINE_AGREEMENT_QTY";
            xCBALT.LINE_NUMBER = "LINE_NUMBER";
            xCBALT.LINE_RELEASE_AMT = "LINE_RELEASE_AMT";
            xCBALT.LINE_RELEASE_QTY = "LINE_RELEASE_QTY";
            xCBALT.LINE_REVISION = "LINE_REVISION";
            xCBALT.LINE_STATUS = "LINE_STATUS";
            xCBALT.PO_HEADER_ID = "PO_HEADER_ID";
            xCBALT.PO_LINE_ID = "PO_LINE_ID";
            xCBALT.PRICE = "PRICE";
            xCBALT.PRICE_LIMIT = "PRICE_LIMIT";
            xCBALT.RELEASE_AMT = "RELEASE_AMT";
            xCBALT.UOM = "UOM";

            xCBALT.table = "XCUST_BLANKET_AGREEMENT_LINES_TBL";
            xCBALT.pkField = "";
        }
        public XcustBlanketAgreementLinesTbl setData(DataRow row)
        {
            XcustBlanketAgreementLinesTbl item;
            item = new XcustBlanketAgreementLinesTbl();
            item.ALLOW_PRICE_OVERIDE = row[xCBALT.ALLOW_PRICE_OVERIDE].ToString();
            item.CREATION_DATE = row[xCBALT.CREATION_DATE].ToString();
            item.CURRENCY_CODE = row[xCBALT.CURRENCY_CODE].ToString();
            item.DESCRIPTION = row[xCBALT.DESCRIPTION].ToString();
            item.EXPIRATION_DATE = row[xCBALT.EXPIRATION_DATE].ToString();
            item.ITEM_CODE = row[xCBALT.ITEM_CODE].ToString();
            item.ITEM_ID = row[xCBALT.ITEM_ID].ToString();
            item.LAST_UPDATE_DATE = row[xCBALT.LAST_UPDATE_DATE].ToString();
            item.LINE_AGREEMENT_AMT = row[xCBALT.LINE_AGREEMENT_AMT].ToString();
            item.LINE_AGREEMENT_QTY = row[xCBALT.LINE_AGREEMENT_QTY].ToString();
            item.LINE_NUMBER = row[xCBALT.LINE_NUMBER].ToString();
            item.LINE_RELEASE_AMT = row[xCBALT.LINE_RELEASE_AMT].ToString();
            item.LINE_RELEASE_QTY = row[xCBALT.LINE_RELEASE_QTY].ToString();
            item.LINE_REVISION = row[xCBALT.LINE_REVISION].ToString();
            item.LINE_STATUS = row[xCBALT.LINE_STATUS].ToString();
            item.PO_HEADER_ID = row[xCBALT.PO_HEADER_ID].ToString();
            item.PO_LINE_ID = row[xCBALT.PO_LINE_ID].ToString();
            item.PRICE = row[xCBALT.PRICE].ToString();
            item.PRICE_LIMIT = row[xCBALT.PRICE_LIMIT].ToString();
            item.RELEASE_AMT = row[xCBALT.RELEASE_AMT].ToString();
            item.UOM = row[xCBALT.UOM].ToString();

            return item;
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCBALT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
    }
}
