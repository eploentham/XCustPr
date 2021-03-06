﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class ReadExcel
    {
        private InitC initC;
        public ReadExcel(InitC initc)
        {
            initC = initc;
            initConfig();
        }
        private void initConfig()
        {

        }
        public List<String> ReadExcelPO008(String filename, MaterialProgressBar pB1, ValidateFileName vFexcel)
        {
            pB1.Show();
            pB1.Minimum = 0;
            String chk = "";

            int colPO_NO = 2, colQT_NO = 3, colWO_NO = 4, colPERIOD = 5, colWeek = 6, colBRANCH_PLANT = 7, colBRANCH_NAME = 8, colLOCTYPE = 9, colITEM_E1 = 10, colASSET_CODE = 11, colASSET_NAME = 12, colWORK_TYPE = 13, colAMOUNT = 14;
            int colVAT = 15, colTOTAL = 16, colSUPPLIER_CODE = 17, colSUPPLIER_NAME = 18, colADMIN = 19, colADMIN_RECEIVE_DOC = 20, colAPPROVE_DATE = 21, colCEDAR_CLOSE_DATE = 22, colINVOICE_DUE_DATE = 23;
            int colSUPP_AGREEMENT_NO = 24, colACCOUNT_SEGMENT = 25, colDATA_SOURCE = 26;
            int col_no = 1;
            int col_project_code = 25;      //แก้ 

            List<String> result = new List<String>();
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            //Thread.Sleep(500);
            excelapp.Visible = false;
            try
            {
                Microsoft.Office.Interop.Excel._Workbook workbook = (Microsoft.Office.Interop.Excel._Workbook)(excelapp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0));
                Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                if (worksheet == null)
                {
                    result.Add("");
                    return result;
                }
                Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
                int cnt = range.Rows.Count;
                pB1.Maximum = cnt;
                for (int i = 1; i <= cnt; i++)
                {
                    pB1.Value = i;
                    if (i == 1) continue;
                    int no = 0;
                    String txt = "";
                    String PO_NO = "", QT_NO = "", WO_NO = "", PERIOD = "", Week = "", BRANCH_PLANT = "", BRANCH_NAME = "", LOCTYPE = "", ITEM_E1 = "", ASSET_CODE = "", ASSET_NAME = "", WORK_TYPE = "", AMOUNT = "";
                    String VAT = "", TOTAL = "", SUPPLIER_CODE = "", SUPPLIER_NAME = "", ADMIN = "", ADMIN_RECEIVE_DOC = "", APPROVE_DATE = "", CEDAR_CLOSE_DATE = "", INVOICE_DUE_DATE = "";
                    String SUPP_AGREEMENT_NO = "", ACCOUNT_SEGMENT = "", DATA_SOURCE = "", xno="", project_code="";

                    xno = worksheet.Cells[i, col_no].value != null ? String.Concat(worksheet.Cells[i, col_no].value) : "";
                    if(!int.TryParse(xno, out no))
                    {
                        continue;
                    }

                    PO_NO = worksheet.Cells[i, colPO_NO].value != null ? String.Concat(worksheet.Cells[i, colPO_NO].value) : "";
                    QT_NO = worksheet.Cells[i, colQT_NO].value != null ? String.Concat(worksheet.Cells[i, colQT_NO].value) : "";
                    WO_NO = worksheet.Cells[i, colWO_NO].value != null ? String.Concat(worksheet.Cells[i, colWO_NO].value) : "";
                    PERIOD = worksheet.Cells[i, colPERIOD].value != null ? String.Concat(worksheet.Cells[i, colPERIOD].value) : "";
                    Week = worksheet.Cells[i, colWeek].value != null ? String.Concat(worksheet.Cells[i, colWeek].value) : "";
                    BRANCH_PLANT = worksheet.Cells[i, colBRANCH_PLANT].value != null ? String.Concat(worksheet.Cells[i, colBRANCH_PLANT].value) : "";
                    BRANCH_NAME = worksheet.Cells[i, colBRANCH_NAME].value != null ? String.Concat(worksheet.Cells[i, colBRANCH_NAME].value) : "";
                    LOCTYPE = worksheet.Cells[i, colLOCTYPE].value != null ? String.Concat(worksheet.Cells[i, colLOCTYPE].value) : "";
                    ITEM_E1 = worksheet.Cells[i, colITEM_E1].value != null ? String.Concat(worksheet.Cells[i, colITEM_E1].value) : "";

                    ASSET_CODE = worksheet.Cells[i, colASSET_CODE].value != null ? String.Concat(worksheet.Cells[i, colASSET_CODE].value) : "";
                    ASSET_NAME = worksheet.Cells[i, colASSET_NAME].value != null ? String.Concat(worksheet.Cells[i, colASSET_NAME].value) : "";
                    WORK_TYPE = worksheet.Cells[i, colWORK_TYPE].value != null ? String.Concat(worksheet.Cells[i, colWORK_TYPE].value) : "";
                    AMOUNT = worksheet.Cells[i, colAMOUNT].value != null ? String.Concat(worksheet.Cells[i, colAMOUNT].value) : "";
                    VAT = worksheet.Cells[i, colVAT].value != null ? String.Concat(worksheet.Cells[i, colVAT].value) : "";
                    TOTAL = worksheet.Cells[i, colTOTAL].value != null ? String.Concat(worksheet.Cells[i, colTOTAL].value) : "";
                    SUPPLIER_CODE = worksheet.Cells[i, colSUPPLIER_CODE].value != null ? String.Concat(worksheet.Cells[i, colSUPPLIER_CODE].value) : "";
                    SUPPLIER_NAME = worksheet.Cells[i, colSUPPLIER_NAME].value != null ? String.Concat(worksheet.Cells[i, colSUPPLIER_NAME].value) : "";
                    ADMIN = worksheet.Cells[i, colADMIN].value != null ? String.Concat(worksheet.Cells[i, colADMIN].value) : "";
                    ADMIN_RECEIVE_DOC = worksheet.Cells[i, colADMIN_RECEIVE_DOC].value != null ? String.Concat(worksheet.Cells[i, colADMIN_RECEIVE_DOC].value) : "";

                    APPROVE_DATE = worksheet.Cells[i, colAPPROVE_DATE].value != null ? String.Concat(worksheet.Cells[i, colAPPROVE_DATE].value) : "";
                    CEDAR_CLOSE_DATE = worksheet.Cells[i, colCEDAR_CLOSE_DATE].value != null ? String.Concat(worksheet.Cells[i, colCEDAR_CLOSE_DATE].value) : "";
                    INVOICE_DUE_DATE = worksheet.Cells[i, colINVOICE_DUE_DATE].value != null ? String.Concat(worksheet.Cells[i, colINVOICE_DUE_DATE].value) : "";
                    SUPP_AGREEMENT_NO = worksheet.Cells[i, colSUPP_AGREEMENT_NO].value != null ? String.Concat(worksheet.Cells[i, colSUPP_AGREEMENT_NO].value) : "";
                    ACCOUNT_SEGMENT = worksheet.Cells[i, colACCOUNT_SEGMENT].value != null ? String.Concat(worksheet.Cells[i, colACCOUNT_SEGMENT].value) : "";
                    DATA_SOURCE = worksheet.Cells[i, colDATA_SOURCE].value != null ? String.Concat(worksheet.Cells[i, colDATA_SOURCE].value) : "";

                    //project_code = worksheet.Cells[i, col_project_code].value != null ? String.Concat(worksheet.Cells[i, col_project_code].value) : "";

                    txt += xno + "|" + PO_NO + "|" + QT_NO + "|" + WO_NO + "|" + PERIOD + "|" + Week + "|" + BRANCH_PLANT + "|" + BRANCH_NAME + "|" + LOCTYPE + "|" + ITEM_E1 + "|" + ASSET_CODE + "|" + ASSET_NAME + "|" +
                        WORK_TYPE + "|" + AMOUNT + "|" + VAT + "|" + TOTAL + "|" + SUPPLIER_CODE + "|" + SUPPLIER_NAME + "|" + ADMIN + "|" + ADMIN_RECEIVE_DOC + "|" + APPROVE_DATE + "|" + CEDAR_CLOSE_DATE + "|" +
                        INVOICE_DUE_DATE + "|" + SUPP_AGREEMENT_NO + "|" + ACCOUNT_SEGMENT + "|" + DATA_SOURCE;

                    //txt += xno + "|" + PO_NO + "|" + QT_NO + "|" + WO_NO + "|" + PERIOD + "|" + Week + "|" + BRANCH_PLANT + "|" + BRANCH_NAME + "|" + LOCTYPE + "|" + ITEM_E1 + "|" + ASSET_CODE + "|" + ASSET_NAME + "|" +
                    //    WORK_TYPE + "|" + AMOUNT + "|" + VAT + "|" + TOTAL + "|" + SUPPLIER_CODE + "|" + SUPPLIER_NAME + "|" + ADMIN + "|" + ADMIN_RECEIVE_DOC + "|" + APPROVE_DATE + "|" + CEDAR_CLOSE_DATE + "|" +
                    //    INVOICE_DUE_DATE + "|" + SUPP_AGREEMENT_NO + "|" + project_code + "|" + DATA_SOURCE;

                    result.Add(txt);
                }
                Thread.Sleep(500);
                workbook.Close(true, null, null);
            }
            catch (Exception ex)
            {
                chk = "";
                vFexcel.fileName = filename.Replace(initC.PO008PathProcess,"");
                vFexcel.Message = " PO008-0020 : read excel error"+ex.Message;
                //vFexcel.Validate = "";
            }

            excelapp.Quit();
            pB1.Visible = false;

            //vPPexcel.Filename = filename.Replace(initC.PO008PathProcess, "");       //for test
            //vPPexcel.Message = " PO008-0020 : read excel error";       //for test
            //vPPexcel.Validate = "";       //for test

            return result;
        }
    }
}
