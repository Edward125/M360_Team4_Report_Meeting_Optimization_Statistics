using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmWelcom());
        }
    }
}
