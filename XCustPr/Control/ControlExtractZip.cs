﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class ControlExtractZip
    {
        static String fontName = "Microsoft Sans Serif";        //standard
        public String backColor1 = "#1E1E1E";        //standard
        public String backColor2 = "#2D2D30";        //standard
        public String foreColor1 = "#fff";        //standard
        static float fontSize9 = 9.75f;        //standard
        static float fontSize8 = 8.25f;        //standard
        public Font fV1B, fV1;        //standard
        public int tcW = 0, tcH = 0, tcWMinus = 25, tcHMinus = 70, formFirstLineX = 5, formFirstLineY = 5;        //standard

        public ControlMain Cm;

        public ControlExtractZip(ControlMain cm)
        {
            Cm = cm;
            initConfig();
        }
        private void initConfig()
        {
            Cm.createFolderExtractZip();
        }
        public void readZIPFileToTemp(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            //addListView("read zip file " + Cm.initC.ExtractZipPathZipExtractRead, "read zip ", lv1, form1);
            addListView("read zip file " + Cm.initC.ExtractZipPathInitial, "read zip ", lv1, form1);
            String filename = "";
            List<ValidateFileName> lVfile = new List<ValidateFileName>();
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();
            ValidatePrPo vPP = new ValidatePrPo();
            String date = System.DateTime.Now.ToString("yyyy-MMM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            //filename = Cm.initC.ExtractZipPathZipExtractRead + "\\xcustpr.zip";
            //Cm.deleteFile(filename);
            //ZipArchive zip = ZipFile.Open(filename, ZipArchiveMode.Read);
            //var allFiles = Directory.GetFiles(@Cm.initC.ExtractZipPathZipExtractRead, "*.*", SearchOption.AllDirectories);
            Cm.deleteFileInFolder(Cm.initC.ExtractZipPathTmp);
            foreach (String file in filePO)
            {
                using (ZipArchive archive = ZipFile.OpenRead(file))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        //if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        //{
                        if (entry.Name.Equals(""))
                        {
                            continue;
                        }
                        ValidateFileName vF = new ValidateFileName();
                        vF.fileName = entry.Name;
                        vF.recordTotal = "1";
                        lVfile.Add(vF);
                        try
                        {
                            //Cm.moveFile(@Cm.initC.ExtractZipPathTmp + entry.Name, @Cm.initC.Ext1ractZipPathTmp, entry.Name);
                            entry.ExtractToFile(Path.Combine(@Cm.initC.ExtractZipPathTmp, entry.Name));
                        }
                        catch (IOException ioe)
                        {
                            vPP = new ValidatePrPo();
                            vPP.Filename = entry.Name;
                            vPP.Message = ioe.Message;
                            //vPP.Validate = "row " + row1 + " ORDER_DATE=" + row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString();
                            lVPr.Add(vPP);
                            addListView("err " + ioe.Message, "extract error ", lv1, form1);
                        }
                        catch (ArgumentException ae)
                        {
                            vPP = new ValidatePrPo();
                            vPP.Filename = entry.Name;
                            vPP.Message = ae.Message;
                            //vPP.Validate = "row " + row1 + " ORDER_DATE=" + row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString();
                            lVPr.Add(vPP);
                            addListView("err " + ae.Message, "extract error ", lv1, form1);
                        }
                        //addListView("extract zip file " + entry.Name, "extract zip ", lv1, form1);
                        //}
                    }
                    archive.Dispose();
                }
                //zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                //zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
            //zip.Dispose();
            Cm.logProcess("xcustextractzip", lVPr, date +" "+ time, lVfile);
        }
        public void backupFileNoProcess()
        {
            String date = System.DateTime.Now.ToString("yyyy-MM-dd");
            Cm.createFolderExtractZipNoProcess();
            String[] filePO;
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathZipExtract_DEX);
            foreach (String file in filePO)
            {
                Cm.moveFile(file, Cm.initC.ExtractZipPathNoProcess + "\\" + date+"\\" + file.Replace(Cm.initC.ExtractZipPathZipExtract_DEX, ""));
            }
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathZipExtract_DFT);
            foreach (String file in filePO)
            {
                Cm.moveFile(file, Cm.initC.ExtractZipPathNoProcess + "\\" + date + "\\" + file.Replace(Cm.initC.ExtractZipPathZipExtract_DFT, ""));
            }
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathZipExtract_DRT);
            foreach (String file in filePO)
            {
                Cm.moveFile(file, Cm.initC.ExtractZipPathNoProcess + "\\" + date + "\\" + file.Replace(Cm.initC.ExtractZipPathZipExtract_DRT, ""));
            }
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathZipExtract_DUS_WUS);
            foreach (String file in filePO)
            {
                Cm.moveFile(file, Cm.initC.ExtractZipPathNoProcess + "\\" + date + "\\" + file.Replace(Cm.initC.ExtractZipPathZipExtract_DUS_WUS, ""));
            }
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathZipExtract_DIN_PIN_WIN);
            foreach (String file in filePO)
            {
                Cm.moveFile(file, Cm.initC.ExtractZipPathNoProcess + "\\" + date + "\\" + file.Replace(Cm.initC.ExtractZipPathZipExtract_DIN_PIN_WIN, ""));
            }
        }
        public void moveFileToFolder(MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("move File To Folder " + Cm.initC.ExtractZipPathInitial, "read zip ", lv1, form1);
            backupFileNoProcess();
            String[] filePO;
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathTmp);
            foreach (String file in filePO)
            {
                if (file.ToLower().IndexOf("wus")>0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DUS_WUS, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
                else if (file.ToLower().IndexOf("dus") > 0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DUS_WUS, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
                else if (file.ToLower().IndexOf("dex") > 0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DEX, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
                else if (file.ToLower().IndexOf("dft") > 0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DFT, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
                else if (file.ToLower().IndexOf("win") > 0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DIN_PIN_WIN, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
                else if (file.ToLower().IndexOf("din") > 0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DIN_PIN_WIN, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
                else if (file.ToLower().IndexOf("pin") > 0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DIN_PIN_WIN, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
                else if (file.ToLower().IndexOf("drt") > 0)
                {
                    Cm.moveFile(file, Cm.initC.ExtractZipPathZipExtract_DRT, file.Replace(Cm.initC.ExtractZipPathTmp, ""), Cm.initC.ExtractZipPathLog);
                    continue;
                }
            }
        }
        private void addListView(String col1, String col2, MaterialListView lv1, Form form1)
        {
            lv1.Items.Add(AddToList((lv1.Items.Count + 1), col1, col2));
            form1.Refresh();
        }
        private ListViewItem AddToList(int col1, string col2, string col3)
        {
            //int i = 0;
            string[] array = new string[3];
            array[0] = col1.ToString();
            //i = lv.Items.Count();
            //array[0] = lv.Items.Count();
            array[1] = col2;
            array[2] = col3;

            return (new ListViewItem(array));
        }
    }
}
