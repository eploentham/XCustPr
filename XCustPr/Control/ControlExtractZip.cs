using System;
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

        }
        public void readZIPFile(String[] filePO, MaterialListView lv1, Form form1, MaterialProgressBar pB1)
        {
            addListView("read zip file " + Cm.initC.ExtractZipPathZipExtractRead, "read zip ", lv1, form1);
            String filename = "";
            List<ValidateFileName> lVfile = new List<ValidateFileName>();
            List<ValidatePrPo> lVPr = new List<ValidatePrPo>();
            ValidatePrPo vPP = new ValidatePrPo();
            String date = System.DateTime.Now.ToString("yyyy-MMM-dd");
            String time = System.DateTime.Now.ToString("HH:mm:ss");
            //filename = Cm.initC.PathZip + "\\xcustpr.zip";
            //Cm.deleteFile(filename);
            //ZipArchive zip = ZipFile.Open(filename, ZipArchiveMode.Read);
            //var allFiles = Directory.GetFiles(@Cm.initC.ExtractZipPathZipExtractRead, "*.*", SearchOption.AllDirectories);
            foreach (String file in filePO)
            {
                //using (ZipArchive archive = ZipFile.OpenRead(file))
                //{
                //    foreach (ZipArchiveEntry entry in archive.Entries)
                //    {
                //        //if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                //        //{
                //        ValidateFileName vF = new ValidateFileName();
                //        vF.fileName = entry.FullName;
                //        vF.recordTotal = "1";
                //        lVfile.Add(vF);
                //        try
                //        {
                //            entry.ExtractToFile(Path.Combine(@Cm.initC.ExtractZipPathZipExtract, entry.FullName));
                //        }
                //        catch (IOException ioe)
                //        {
                //            vPP = new ValidatePrPo();
                //            vPP.Filename = entry.FullName;
                //            vPP.Message = ioe.Message;
                //            //vPP.Validate = "row " + row1 + " ORDER_DATE=" + row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString();
                //            lVPr.Add(vPP);
                //            addListView("err " + ioe.Message, "extract error ", lv1, form1);
                //        }
                //        catch (ArgumentException ae)
                //        {
                //            vPP = new ValidatePrPo();
                //            vPP.Filename = entry.FullName;
                //            vPP.Message = ae.Message;
                //            //vPP.Validate = "row " + row1 + " ORDER_DATE=" + row[xCLFPTDB.xCLFPT.ORDER_DATE].ToString();
                //            lVPr.Add(vPP);
                //            addListView("err " + ae.Message, "extract error ", lv1, form1);
                //        }
                //        addListView("extract zip file " + entry.FullName, "extract zip ", lv1, form1);
                //        //}
                //    }
                //    archive.Dispose();
                //}
                //zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                //zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
            //zip.Dispose();
            Cm.logProcess("xcustextractzip", lVPr, date +" "+ time, lVfile);
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
