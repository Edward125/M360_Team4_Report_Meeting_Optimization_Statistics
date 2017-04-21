using System;
using System.Collections.Generic;
using System.Text;
using Edward;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
   public  class p
   {
       #region 参数定义

       public static string appFolder = Application.StartupPath + @"\ReportMeetingStatistics";
       public static string iniFilePath = appFolder + @"\SysConfig.ini";
       public static string myDepartment = string.Empty;



       public  enum IniSection
       {
           SysConfig
       }


  


       #endregion



       public static  void checkFolder()
       {
           if (!Directory.Exists(appFolder))
           {
               Directory.CreateDirectory(appFolder);
           }

       }


       public static  void createIniFile(string inifilepath)
       {
           IniFile.CreateIniFile(inifilepath);
           IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "MyDepartment", myDepartment, inifilepath);

       }


      public static  void readIniValue(string inifilepath)
       {
           myDepartment = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "MyDepartment", inifilepath);
       }






   }
}
