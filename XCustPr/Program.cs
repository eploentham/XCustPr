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

            ControlMain Cm = new ControlMain();
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo001"))
            {
                Application.Run(new XCustPrToCloud(Cm));
            }
            else if(System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo004"))
            {
                Application.Run(new XCustPO004(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo005"))
            {
                Application.Run(new XCustPO005(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo003"))
            {
                Application.Run(new XCustPO003(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustextractzip"))
            {
                Application.Run(new XCustExtractZip(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo008"))
            {
                Application.Run(new XCustPO008(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustvalueset"))
            {
                Application.Run(new XcustValueSet(Cm));
            }
            else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("xcustpo002"))
            {
                Application.Run(new XCustPO002(Cm));
            }
            else
            {
                //Application.Run(new XCustPO008(Cm));
                //Application.Run(new XCustPO004(Cm));
                //Application.Run(new XCustPO005(Cm));
                //Application.Run(new XCustPO003(Cm));
                //Application.Run(new XCustPrToCloud(Cm));
                //Application.Run(new XCustExtractZip(Cm));
                //Application.Run(new XcustValueSet(Cm));
                //Application.Run(new XCustPO002(Cm));
                Application.Run(new XcustPRWebService(Cm));
            }
        }
    }
}
