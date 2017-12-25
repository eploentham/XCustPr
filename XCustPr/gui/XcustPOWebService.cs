using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    public class XcustPOWebService:Form
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

        CheckBox chkDeliveryDate, chkPo, chkPr, chkPoR;
        DateTimePicker dtpDeliveryDate;

        Color cTxtL, cTxtE, cForm;

        ControlMain Cm;
        ControlPRPOWebService cPrWS;
        ControlPoRWebService cPorWS;
        private ListViewColumnSorter lvwColumnSorter;

        public XcustPOWebService(ControlMain cm)
        {
            this.Size = new Size(formwidth, formheight);
            this.StartPosition = FormStartPosition.CenterScreen;
            Cm = cm;
            initConfig();
            cTxtL = txtFileName.BackColor;
            cTxtE = Color.Yellow;
            this.Text = "Last Update 2017-12-25 17.55 ";
        }
        private void initConfig()
        {
            cPrWS = new ControlPRPOWebService(Cm);
            cPorWS = new ControlPoRWebService(Cm);

            initCompoment();
            pB1.Visible = false;
            dtpDeliveryDate.Enabled = false;
            lvwColumnSorter = new ListViewColumnSorter();
            lvwColumnSorter.Order = SortOrder.Descending;
            lvwColumnSorter.SortColumn = 0;
            lv1.Sort();
            //txtFileName.Text = cRDPO.initC.PathInitial + "PR03102017.txt";
            txtFileName.Text = Cm.initC.AutoValueSet;

            lv1.Columns.Add("NO", 50);
            lv1.Columns.Add("List File", formwidth - 50 - 40 - 100, HorizontalAlignment.Left);
            lv1.Columns.Add("   process   ", 100, HorizontalAlignment.Center);
            lv1.ListViewItemSorter = lvwColumnSorter;

            lb2.Text = lb2.Text + " " + Cm.xcustprwebservice_run;
            if (Cm.xcustpowebservice_run.ToLower().Equals("on"))
            {
                cPrWS.setXcustPOTbl(lv1, this, pB1, Cm.initC.POWebServicePathLog, "");
            }
        }
        private void disableBtn()
        {
            btnRead.Enabled = false;
            btnPrepare.Enabled = false;
            btnFTP.Enabled = false;
            btnWebService.Enabled = false;
            btnEmail.Enabled = false;
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
            lb1.Font = cPrWS.fV1;
            lb1.Text = "Text File";
            lb1.AutoSize = true;
            Controls.Add(lb1);
            lb1.Location = new System.Drawing.Point(cPrWS.formFirstLineX, cPrWS.formFirstLineY + gapLine);

            lb2 = new MaterialLabel();
            lb2.Font = cPrWS.fV1;
            lb2.Text = "Program Name XcustPO Web";
            lb2.AutoSize = true;
            Controls.Add(lb2);
            lb2.Location = new System.Drawing.Point(grd3, cPrWS.formFirstLineY + gapLine);

            txtFileName = new MaterialSingleLineTextField();
            txtFileName.Font = cPrWS.fV1;
            txtFileName.Text = "";
            txtFileName.Size = new System.Drawing.Size(300 - grd1 - 20 - 30, ControlHeight);
            Controls.Add(txtFileName);
            txtFileName.Location = new System.Drawing.Point(grd1, cPrWS.formFirstLineY + gapLine);
            txtFileName.Hint = lb1.Text;
            txtFileName.Enter += txtFileName_Enter;
            txtFileName.Leave += txtFileName_Leave;

            chkDeliveryDate = new CheckBox();
            chkDeliveryDate.Font = cPrWS.fV1B;
            chkDeliveryDate.Text = "ระบุวันที่ ";
            chkDeliveryDate.Size = new System.Drawing.Size(80, ControlHeight);
            Controls.Add(chkDeliveryDate);
            chkDeliveryDate.Location = new System.Drawing.Point(grd4, line1 + 10);
            chkDeliveryDate.Click += ChkDeliveryDate_Click;

            chkPo = new CheckBox();
            chkPo.Font = cPrWS.fV1B;
            chkPo.Text = "sign PO ";
            chkPo.Size = new System.Drawing.Size(120, ControlHeight);
            Controls.Add(chkPo);
            chkPo.Location = new System.Drawing.Point(grd4, line2 + 20);

            chkPr = new CheckBox();
            chkPr.Font = cPrWS.fV1B;
            chkPr.Text = "sign PR ";
            chkPr.Size = new System.Drawing.Size(120, ControlHeight);
            Controls.Add(chkPr);
            chkPr.Location = new System.Drawing.Point(grd5, line2 + 20);

            chkPoR = new CheckBox();
            chkPoR.Font = cPrWS.fV1B;
            chkPoR.Text = "sign PO Receipt ";
            chkPoR.Size = new System.Drawing.Size(150, ControlHeight);
            Controls.Add(chkPoR);
            chkPoR.Location = new System.Drawing.Point(grd3+100, line2 + 20);

            dtpDeliveryDate = new DateTimePicker();
            //dtpDeliveryDate = new MaterialFlatButton();
            dtpDeliveryDate.Font = cPrWS.fV1;
            //btnWebService.Text = "3. Web Service";
            dtpDeliveryDate.Size = new System.Drawing.Size(120, ControlHeight);
            Controls.Add(dtpDeliveryDate);
            dtpDeliveryDate.Location = new System.Drawing.Point(grd4 + chkDeliveryDate.Width + 5, line1 + 10);
            dtpDeliveryDate.Format = DateTimePickerFormat.Short;
            dtpDeliveryDate.ValueChanged += DtpDeliveryDate_ValueChanged; ;


            btnRead = new MaterialFlatButton();
            btnRead.Font = cPrWS.fV1;
            btnRead.Text = "Web Service";
            btnRead.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnRead);
            btnRead.Location = new System.Drawing.Point(grd1, line1);
            btnRead.Click += btnRead_Click;



            pB1 = new MaterialProgressBar();
            Controls.Add(pB1);
            pB1.Size = new System.Drawing.Size(formwidth - 40, pB1.Height);
            pB1.Location = new System.Drawing.Point(cPrWS.formFirstLineX + 5, line41);

            lv1 = new MaterialListView();
            lv1.Font = cPrWS.fV1;
            lv1.FullRowSelect = true;
            lv1.Size = new System.Drawing.Size(formwidth - 40, formheight - line3 - 100);
            lv1.Location = new System.Drawing.Point(cPrWS.formFirstLineX + 5, line42);
            lv1.FullRowSelect = true;
            lv1.View = View.Details;
            //lv1.Dock = System.Windows.Forms.DockStyle.Fill;
            lv1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            Controls.Add(lv1);
        }

        private void DtpDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setDtpDelivery();
        }

        private void setDtpDelivery()
        {
            String year = "", month = "", day = "";
            year = dtpDeliveryDate.Value.Year.ToString();
            month = dtpDeliveryDate.Value.Month.ToString("00");
            day = dtpDeliveryDate.Value.Day.ToString("00");
            Cm.setConfig("Po006DeliveryDate", year + "-" + month + "-" + day);
        }

        private void ChkDeliveryDate_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chkDeliveryDate.Checked)
            {
                dtpDeliveryDate.Enabled = true;
                setDtpDelivery();
            }
            else
            {
                dtpDeliveryDate.Enabled = false;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            String date = dtpDeliveryDate.Value.Month.ToString("00") + "-" + dtpDeliveryDate.Value.Day.ToString("00") + "-" + dtpDeliveryDate.Value.Year.ToString();
            if (chkPo.Checked)
            {
                cPrWS.setXcustPOTbl(lv1, this, pB1, Cm.initC.POWebServicePathLog, date);
            }
            if (chkPr.Checked)
            {
                cPrWS.setXcustPRTbl(lv1, this, pB1, Cm.initC.POWebServicePathLog, date);
            }
            if (chkPoR.Checked)
            {
                cPorWS.setXcustPoRTbl(lv1, this, pB1);
            }
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
