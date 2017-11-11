using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustBlanketAgreementHeaderTblDB
    {
        public XcustBlanketAgreementHeaderTbl xCBAHT;
        ConnectDB conn;
        private InitC initC;
        public XcustBlanketAgreementHeaderTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCBAHT = new XcustBlanketAgreementHeaderTbl();
            xCBAHT.AGREEMENT_AMT = "AGREEMENT_AMT";
            xCBAHT.AGREEMENT_NUMBER = "AGREEMENT_NUMBER";
            xCBAHT.BUYER = "BUYER";
            xCBAHT.COMUNICATION_METHOD = "COMUNICATION_METHOD";
            xCBAHT.CREATION_DATE = "CREATION_DATE";
            xCBAHT.DESCRIPTION = "DESCRIPTION";
            xCBAHT.EMAIL = "E_MAIL";
            xCBAHT.END_DATE = "END_DATE";
            xCBAHT.LAST_UPDATE_DATE = "LAST_UPDATE_DATE";
            xCBAHT.MIN_RELEASE_AMT = "MIN_RELEASE_AMT";
            xCBAHT.POCUMENT_BU = "POCUMENT_BU";
            xCBAHT.RELEASE_AMT = "RELEASE_AMT";
            xCBAHT.START_DATE = "START_DATE";
            xCBAHT.STATUS = "STATUS";
            xCBAHT.SUPPLIER = "SUPPLIER";
            xCBAHT.SUPPLIER_CODE = "SUPPLIER_CODE";
            xCBAHT.SUPPLIER_SITE = "SUPPLIER_SITE";
            xCBAHT.PO_HEADER_ID = "PO_HEADER_ID";

            xCBAHT.table = "XCUST_BLANKET_AGREEMENT_HEADER_TBL";
            xCBAHT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCBAHT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public XcustBlanketAgreementHeaderTbl setData(DataRow row)
        {
            XcustBlanketAgreementHeaderTbl item;
            item = new XcustBlanketAgreementHeaderTbl();
            item.AGREEMENT_AMT = row[xCBAHT.AGREEMENT_AMT].ToString();
            item.AGREEMENT_NUMBER = row[xCBAHT.AGREEMENT_NUMBER].ToString();
            item.BUYER = row[xCBAHT.BUYER].ToString();
            item.COMUNICATION_METHOD = row[xCBAHT.COMUNICATION_METHOD].ToString();
            item.CREATION_DATE = row[xCBAHT.CREATION_DATE].ToString();
            item.DESCRIPTION = row[xCBAHT.DESCRIPTION].ToString();
            item.EMAIL = row[xCBAHT.EMAIL].ToString();
            item.END_DATE = row[xCBAHT.END_DATE].ToString();
            item.LAST_UPDATE_DATE = row[xCBAHT.LAST_UPDATE_DATE].ToString();
            item.MIN_RELEASE_AMT = row[xCBAHT.MIN_RELEASE_AMT].ToString();
            item.POCUMENT_BU = row[xCBAHT.POCUMENT_BU].ToString();
            item.RELEASE_AMT = row[xCBAHT.RELEASE_AMT].ToString();
            item.START_DATE = row[xCBAHT.START_DATE].ToString();
            item.STATUS = row[xCBAHT.STATUS].ToString();
            item.SUPPLIER = row[xCBAHT.SUPPLIER].ToString();
            item.SUPPLIER_CODE = row[xCBAHT.SUPPLIER_CODE].ToString();
            item.SUPPLIER_SITE = row[xCBAHT.SUPPLIER_SITE].ToString();
            item.PO_HEADER_ID = row[xCBAHT.PO_HEADER_ID].ToString();

            return item;
        }
    }
}
