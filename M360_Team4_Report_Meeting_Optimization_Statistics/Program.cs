using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Edward;

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

            if (!File.Exists(p.iniFilePath)) //不存在，第一次運行
            {
                Application.Run(new frmWelcom());
            }
            else //存在,查看設置的值
            {
                string _mydepartment = IniFile.IniReadValue(p.IniSection.SysConfig.ToString(), "MyDepartment", p.iniFilePath);
                p.myDepartment = _mydepartment;

                if (string.IsNullOrEmpty(_mydepartment))
                    Application.Run(new frmWelcom());
                else
                    Application.Run(new frmMain());
                  
  
            }
        }
    }
}
