using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();

            ControlMain Cm = new ControlMain();
            Cm.args = args;
            Cm.setAgrument();
            //MessageBox.Show("args "+ args.Length, "");
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo001"))
            {
                Cm.initC.pathLogErr = Cm.initC.PathLog;
                Application.Run(new XCustPrToCloud(Cm));
            }
            else if(System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo004"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO004PathLog;
                Application.Run(new XCustPO004(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo005"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO005PathLog;
                Application.Run(new XCustPO005(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo003"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO006PathLog;
                Application.Run(new XCustPO003(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustextractzip"))
            {
                Application.Run(new XCustExtractZip(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo008"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO008PathLog;
                Application.Run(new XCustPO008(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustvalueset"))
            {
                Application.Run(new XcustValueSet(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo002"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO002PathLog;
                Application.Run(new XCustPO002(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo007"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO007PathLog;
                Application.Run(new XCustPO007(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo006"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO006PathLog;
                Application.Run(new XCustPO006(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpows"))
            {
                Application.Run(new XcustPOWebService(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustprws"))
            {
                Application.Run(new XcustPRWebService(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustporws"))
            {
                Application.Run(new XcustPRWebService(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustap001"))
            {
                Cm.initC.pathLogErr = Cm.initC.PathLog;
                Application.Run(new XCustAP001(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustap004"))
            {
                Cm.initC.pathLogErr = Cm.initC.PO004PathLog;
                Application.Run(new XCustAP004(Cm));
            }
            else
            {
                Cm.initC.pathLogErr = Cm.initC.PathLog;
                //Application.Run(new XCustPO008(Cm));
                //Application.Run(new XCustPO004(Cm));
                //Application.Run(new XCustPO005(Cm));
                //Application.Run(new XCustPO003(Cm));
                //Application.Run(new XCustPrToCloud(Cm));
                //Application.Run(new XCustExtractZip(Cm));
                //Application.Run(new XcustValueSet(Cm));
                //Application.Run(new XCustPO002(Cm));
                //Application.Run(new XcustPRWebService(Cm));
                //Application.Run(new XcustPOWebService(Cm));
                Application.Run(new XCustPO007(Cm));
                //Application.Run(new XCustPO006(Cm));
                //Application.Run(new XCustPoRWebService(Cm));
                //Application.Run(new XCustAP001(Cm));
                //Application.Run(new XCustAP004(Cm));
            }
        }
    }
}
