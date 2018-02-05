using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class XCustExtractZip:Form
    {
        int gapLine = 5;
        int grd0 = 0, grd1 = 100, grd2 = 240, grd3 = 320, grd4 = 570, grd5 = 700, grd51 = 700, grd6 = 820, grd7 = 900, grd8 = 1070, grd9 = 1200;
        int line1 = 35, line2 = 27, line3 = 85, line4 = 105, line41 = 120, line42 = 111, line5 = 270, ControlHeight = 21, lineGap = 5;

        int formwidth = 860, formheight = 740;

        MaterialLabel lb1, lb2;
        MaterialSingleLineTextField txtFileName;
        MaterialFlatButton btnRead, btnPrepare, btnWebService, btnFTP, btnEmail;
        MaterialListView lv1;
        MaterialProgressBar pB1;

        Color cTxtL, cTxtE, cForm;

        ControlMain Cm;
        ControlExtractZip cEZ;
        String[] filePO;

        private ListViewColumnSorter lvwColumnSorter;
        public XCustExtractZip(ControlMain cm)
        {
            this.Size = new Size(formwidth, formheight);
            this.StartPosition = FormStartPosition.CenterScreen;
            Cm = cm;
            initConfig();
            cTxtL = txtFileName.BackColor;
            cTxtE = Color.Yellow;
            this.Text = "Last Update 2018-01-26 ";
        }
        private void initConfig()
        {
            cEZ = new ControlExtractZip(Cm);

            initCompoment();
            pB1.Visible = false;
            lvwColumnSorter = new ListViewColumnSorter();
            lvwColumnSorter.Order = SortOrder.Descending;
            lvwColumnSorter.SortColumn = 0;
            lv1.Sort();

            lv1.Columns.Add("NO", 50);
            lv1.Columns.Add("List File", formwidth - 50 - 40 - 100, HorizontalAlignment.Left);
            lv1.Columns.Add("   process   ", 100, HorizontalAlignment.Center);
            lv1.ListViewItemSorter = lvwColumnSorter;
            txtFileName.Text = Cm.initC.AutoRunExtractZip;

            int i = 1;
            if (Cm.initC.ExtractZipPathInitial.Equals(""))
            {
                MessageBox.Show("Path Config ExtractZip ไม่ถูกต้อง", "");
                disableBtn();
                return;
            }
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathInitial);
            if (filePO == null)
            {
                MessageBox.Show("Folder ExtractZip ไม่ถูกต้อง", "");
                disableBtn();
                return;
            }
            foreach (string aa in filePO)
            {
                lv1.Items.Add(AddToList((i++), aa, ""));
                //lv1.Items.s
            }
        }
        private void disableBtn()
        {
            btnRead.Enabled = false;            
        }
        private void initCompoment()
        {
            line1 = 35 + gapLine;
            line2 = 57 + gapLine;
            line3 = 75 + gapLine;
            line4 = 125 + gapLine;
            line41 = 120 + gapLine;
            line42 = 140 + gapLine;
            line5 = 270 + gapLine;

            lb1 = new MaterialLabel();
            lb1.Font = cEZ.fV1;
            lb1.Text = "Text File";
            lb1.AutoSize = true;
            Controls.Add(lb1);
            lb1.Location = new System.Drawing.Point(cEZ.formFirstLineX, cEZ.formFirstLineY + gapLine);

            lb2 = new MaterialLabel();
            lb2.Font = cEZ.fV1;
            lb2.Text = "Program Name XcustExtractZip";
            lb2.AutoSize = true;
            Controls.Add(lb2);
            lb2.Location = new System.Drawing.Point(grd3, cEZ.formFirstLineY + gapLine);

            txtFileName = new MaterialSingleLineTextField();
            txtFileName.Font = cEZ.fV1;
            txtFileName.Text = "";
            txtFileName.Size = new System.Drawing.Size(300 - grd1 - 20 - 30, ControlHeight);
            Controls.Add(txtFileName);
            txtFileName.Location = new System.Drawing.Point(grd1, cEZ.formFirstLineY + gapLine);
            txtFileName.Hint = lb1.Text;
            txtFileName.Enter += txtFileName_Enter;
            txtFileName.Leave += txtFileName_Leave;

            btnRead = new MaterialFlatButton();
            btnRead.Font = cEZ.fV1;
            btnRead.Text = "Extract Zip File";
            btnRead.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnRead);
            btnRead.Location = new System.Drawing.Point(grd1, line1);
            btnRead.Click += btnRead_Click;
            
            pB1 = new MaterialProgressBar();
            Controls.Add(pB1);
            pB1.Size = new System.Drawing.Size(formwidth - 40, pB1.Height);
            pB1.Location = new System.Drawing.Point(cEZ.formFirstLineX + 5, line41);

            lv1 = new MaterialListView();
            lv1.Font = cEZ.fV1;
            lv1.FullRowSelect = true;
            lv1.Size = new System.Drawing.Size(formwidth - 40, formheight - line3 - 100);
            lv1.Location = new System.Drawing.Point(cEZ.formFirstLineX + 5, line42);
            lv1.FullRowSelect = true;
            lv1.View = View.Details;
            //lv1.Dock = System.Windows.Forms.DockStyle.Fill;
            lv1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            Controls.Add(lv1);
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            lv1.Items.Clear();
            filePO = Cm.getFileinFolder(Cm.initC.ExtractZipPathInitial);
            cEZ.readZIPFileToTemp(filePO, lv1, this, pB1);
            cEZ.moveFileToFolder(lv1, this, pB1);
        }
        private void txtFileName_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtL;
        }

        private void txtFileName_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtE;
        }
        private ListViewItem AddToList(int col1, string col2, string col3)
        {
            string[] array = new string[3];
            array[0] = col1.ToString();
            array[1] = col2;
            array[2] = col3;

            return (new ListViewItem(array));
        }
    }
}
