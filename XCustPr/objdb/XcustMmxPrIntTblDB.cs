﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class XcustMmxPrIntTblDB
    {
        public XcustMmxPrIntTbl xCMPIT;
        ConnectDB conn;
        private InitC initC;
        public XcustMmxPrIntTblDB(ConnectDB c, InitC initc)
        {
            conn = c;
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {
            xCMPIT = new XcustMmxPrIntTbl();
            xCMPIT.ACC_SEG1 = "ACC_SEG1";
            xCMPIT.ACC_SEG2 = "ACC_SEG2";
            xCMPIT.ACC_SEG3 = "ACC_SEG3";
            xCMPIT.ACC_SEG4 = "ACC_SEG4";
            xCMPIT.ACC_SEG5 = "ACC_SEG5";
            xCMPIT.ACC_SEG6 = "ACC_SEG6";
            xCMPIT.AGREEEMENT_NUMBER = "AGREEEMENT_NUMBER";
            xCMPIT.AGREEMENT_LINE_NUMBER = "AGREEMENT_LINE_NUMBER";
            xCMPIT.confirm_qty = "confirm_qty";
            xCMPIT.conf_delivery_date = "conf_delivery_date";
            xCMPIT.creation_by = "creation_by";
            xCMPIT.creation_date = "creation_date";
            xCMPIT.delivery_date = "delivery_date";
            xCMPIT.DELIVERY_INSTRUCTION = "DELIVERY_INSTRUCTION";
            xCMPIT.deriver_to_location = "deriver_to_location";
            xCMPIT.diriver_to_organization = "diriver_to_organization";
            xCMPIT.ERP_ITEM_CODE = "ERP_ITEM_CODE";
            xCMPIT.erp_subinventory_code = "erp_subinventory_code";
            xCMPIT.error_message = "error_message";
            xCMPIT.file_name = "file_name";
            xCMPIT.ITEM_CATEGORY_NAME = "ITEM_CATEGORY_NAME";
            xCMPIT.item_code = "item_code";
            xCMPIT.last_update_by = "last_update_by";
            xCMPIT.last_update_date = "last_update_date";
            xCMPIT.order_date = "order_date";
            xCMPIT.order_qty = "order_qty";
            xCMPIT.po_number = "po_number";
            xCMPIT.po_status = "po_status";
            xCMPIT.PRICE = "PRICE";
            xCMPIT.process_flag = "process_flag";
            xCMPIT.request_date = "request_date";
            xCMPIT.store_code = "store_code";
            xCMPIT.subinventory_code = "subinventory_code";
            xCMPIT.supplier_code = "supplier_code";
            xCMPIT.SUPPLIER_SITE_CODE = "SUPPLIER_SITE_CODE";
            xCMPIT.uom_code = "uom_code";
            xCMPIT.Validate_flag = "Validate_flag";
             
            xCMPIT.table = "xcust_mmx_pr_int_tbl";
            xCMPIT.pkField = "";
        }
        public DataTable selectAll()
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPIT.table;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxGroupByFilename()
        {
            DataTable dt = new DataTable();
            String sql = "select " + xCMPIT.file_name + " From " + xCMPIT.table + " Group By " + xCMPIT.file_name;
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public DataTable selectMmxByFilename(String filename)
        {
            DataTable dt = new DataTable();
            String sql = "select * From " + xCMPIT.table + " Where " + xCMPIT.file_name + "='" + filename + "'";
            dt = conn.selectData(sql, "kfc_po");
            return dt;
        }
        public void DeleteMmxTemp()
        {
            String sql = "Delete From " + xCMPIT.table;
            conn.ExecuteNonQuery(sql, "kfc_po");
        }
        public String dateYearShortToDB(String date)
        {
            String chk = "", year = "", month = "", day = "";

            year = date.Substring(date.Length - 2);
            day = date.Substring(3, 2);
            month = date.Substring(0, 2);

            chk = "20" + year + "-" + month + "-" + day;

            return chk;
        }
        public String dateYearShortToDBTemp(String date)
        {
            String chk = "", year = "", month = "", day = "";

            year = date.Substring(date.Length - 2);
            day = date.Substring(3, 2);
            month = date.Substring(0, 2);

            chk = "20" + year + month + day;

            return chk;
        }
        public void insertBluk(List<String> mmx, String filename, String host, MaterialProgressBar pB1)
        {
            int i = 0;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");

            String ConnectionString = "", errMsg = "", processFlag = "", validateFlag = "", createBy = "0", createDate = "GETDATE()", last_update_by = "0", lastUpdateTime = "null";
            String DELIVERY_INSTRUCTION = "", diriver_to_organization="", ERP_ITEM_CODE="", erp_subinventory_code="", ITEM_CATEGORY_NAME="", uom_code="";
            String orderDate = "", deliDate="", confDate="", ACC_SEG1="", ACC_SEG2="", ACC_SEG3="", ACC_SEG4="", ACC_SEG5="", ACC_SEG6="", AGREEEMENT_NUMBER="", AGREEMENT_LINE_NUMBER="";
            if (host == "kfc_po")
            {
                ConnectionString = conn.connKFC.ConnectionString;
            }
            StringBuilder sql = new StringBuilder();
            pB1.Minimum = 1;
            pB1.Maximum = mmx.Count();
            using (SqlCommand mConnection = new SqlCommand(ConnectionString))
            {
                List<string> Rows = new List<string>();
                foreach (String bbb in mmx)
                {
                    i++;
                    sql.Clear();
                    pB1.Value = i;
                    String[] aaa = bbb.Split(',');
                    errMsg = "";
                    processFlag = "N";
                    validateFlag = "N";
                    orderDate = dateYearShortToDBTemp(aaa[8]);
                    deliDate = dateYearShortToDBTemp(aaa[3]);
                    confDate = dateYearShortToDBTemp(aaa[10]);
                    //bbb += "('" + aaa[0] + "','" +
                    //aaa[11] + "','" + errMsg + "','" + aaa[6] + "','" +
                    //aaa[2] + "','" + aaa[4] + "','" + aaa[5] + "','" +
                    //aaa[1] + "','" + processFlag + "','" + aaa[7] + "','" +
                    //aaa[3] + "','" + aaa[8] + "','" + validateFlag + "'),";
                    sql.Append("Insert Into ").Append(xCMPIT.table).Append(" (").Append(xCMPIT.ACC_SEG1).Append(",").Append(xCMPIT.ACC_SEG2).Append(",").Append(xCMPIT.ACC_SEG3)
                        .Append(",").Append(xCMPIT.ACC_SEG4).Append(",").Append(xCMPIT.ACC_SEG5).Append(",").Append(xCMPIT.ACC_SEG6)
                        .Append(",").Append(xCMPIT.AGREEEMENT_NUMBER).Append(",").Append(xCMPIT.AGREEMENT_LINE_NUMBER).Append(",").Append(xCMPIT.confirm_qty)
                        .Append(",").Append(xCMPIT.conf_delivery_date).Append(",").Append(xCMPIT.creation_by).Append(",").Append(xCMPIT.creation_date)
                        .Append(",").Append(xCMPIT.delivery_date).Append(",").Append(xCMPIT.DELIVERY_INSTRUCTION).Append(",").Append(xCMPIT.deriver_to_location)
                        .Append(",").Append(xCMPIT.diriver_to_organization).Append(",").Append(xCMPIT.ERP_ITEM_CODE).Append(",").Append(xCMPIT.erp_subinventory_code)
                        .Append(",").Append(xCMPIT.error_message).Append(",").Append(xCMPIT.file_name).Append(",").Append(xCMPIT.ITEM_CATEGORY_NAME)
                        .Append(",").Append(xCMPIT.item_code).Append(",").Append(xCMPIT.last_update_by).Append(",").Append(xCMPIT.last_update_date)
                        .Append(",").Append(xCMPIT.order_date).Append(",").Append(xCMPIT.order_qty).Append(",").Append(xCMPIT.po_number)
                        .Append(",").Append(xCMPIT.po_status).Append(",").Append(xCMPIT.PRICE).Append(",").Append(xCMPIT.process_flag)
                        .Append(",").Append(xCMPIT.request_date).Append(",").Append(xCMPIT.store_code).Append(",").Append(xCMPIT.subinventory_code)
                        .Append(",").Append(xCMPIT.supplier_code).Append(",").Append(xCMPIT.SUPPLIER_SITE_CODE).Append(",").Append(xCMPIT.uom_code)
                        .Append(",").Append(xCMPIT.Validate_flag).Append(" ")
                        .Append(") Values ('")
                        .Append(ACC_SEG1).Append("','").Append(ACC_SEG2).Append("','").Append(ACC_SEG3)
                        .Append("','").Append(ACC_SEG4).Append("','").Append(ACC_SEG5).Append("','").Append(ACC_SEG6)
                        .Append("','").Append(AGREEEMENT_NUMBER).Append("','").Append(AGREEMENT_LINE_NUMBER).Append("','").Append(aaa[6]/*CONFIRM  QTY*/)
                        .Append("','").Append(confDate/*CONF_DILIVERY_DATE*/).Append("','").Append(createBy).Append("',getdate()")
                        .Append(",'").Append(deliDate/*delivery_date*/).Append("','").Append(DELIVERY_INSTRUCTION).Append("','").Append(initC.DELIVER_TO_LOCATTION)
                        .Append("','").Append(diriver_to_organization).Append("','").Append(ERP_ITEM_CODE).Append("','").Append(erp_subinventory_code)
                        .Append("','").Append(errMsg/*errMsg*/).Append("','").Append(filename.Trim().Replace(initC.PO005PathProcess, "")).Append("','").Append(ITEM_CATEGORY_NAME)
                        .Append("','").Append(aaa[7]/*ITEM_CODE*/).Append("','").Append(last_update_by).Append("',").Append(lastUpdateTime)
                        .Append(",'").Append(orderDate/*ORDER_DATE*/).Append("','").Append(aaa[5]/*ORDER_QTY*/).Append("','").Append(aaa[1]/*PO_NUMBER*/)
                        .Append("','").Append(aaa[9]/*.PO_STATUS*/).Append("',0,'").Append(processFlag)
                        .Append("','").Append(orderDate).Append("','").Append(aaa[0]/*STRORE_NO*/).Append("','").Append(aaa[4]/*Subinventory Code*/)
                        .Append("','").Append(aaa[2]/*SUPPLIER_CODE*/).Append("','','").Append(uom_code)
                        .Append("','").Append(validateFlag).Append("') ");
                    conn.ExecuteNonQuery(sql.ToString(), host);
                }
            }
        }
        public String updateValidateFlag(String po_number, String line_number, String flag, String agreement_number, String host)
        {
            String sql = "";
            sql = "Update " + xCMPIT.table + " Set " + xCMPIT.Validate_flag + "='" + flag + "', " + xCMPIT.AGREEEMENT_NUMBER + " ='" + agreement_number + "' " +
                "Where " + xCMPIT.po_number + " = '" + po_number + "' and " + xCMPIT.AGREEMENT_LINE_NUMBER + "='" + line_number + "'";
            conn.ExecuteNonQuery(sql.ToString(), host);

            return "";
        }
    }
}
