using System;
using System.Collections.Generic;
using System.Text;
using Edward;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
   public  class p
   {
       #region 参数定义

       public static string appFolder = Application.StartupPath + @"\ReportMeetingStatistics";
       public static string iniFilePath = appFolder + @"SysConfig";
       public static string myDepartment = string.Empty;
       

       #endregion
   }
}
