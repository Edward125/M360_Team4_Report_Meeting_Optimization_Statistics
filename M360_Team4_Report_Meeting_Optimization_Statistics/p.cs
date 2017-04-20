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



       public  enum IniSection
       {
           SysConfig
       }


  


       #endregion


       private void createIniFile(string inifilepath)
       {
           IniFile.CreateIniFile(inifilepath);
           IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "MyDepartment", myDepartment, inifilepath);

       }


       private void readIniValue(string inifilepath)
       {
           myDepartment = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "MyDepartment", inifilepath);
       }






   }
}
