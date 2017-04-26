using System;
using System.Collections.Generic;
using System.Text;
using Edward;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Drawing;

namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    public class p
    {
        #region 参数定义


        public static bool isAdmin = false;


        public static string appFolder = Application.StartupPath + @"\ReportMeetingStatistics";

        public static string netFolder =@"\\10.62.22.5\PMO Compaign\M360\Team4\Meeting&Report TIPS";
        public static string iniFilePath = appFolder + @"\SysConfig.ini";
        public static string myDepartment = string.Empty;
        public static string dbFile = appFolder + @"\Team4.sqlite";
        public static string netdbFile = @"Y:\M360\Team4\Meeting&Report TIPS\Team4.sqlite";

        public static string _netfolder = @"10.62.22.5\PMO Compaign\M360\Team4\Meeting&Report TIPS\Team4.sqlite";

        public static string dbConnectionString = "Data Source=" + @netdbFile;
        public static DateTime sysStart = new DateTime(2017, 04, 10);

        public static string titleModifyMeetingReportData = "Modify Data";


        public static List<string> departmentlist = new List<string>();



        public enum IniSection
        {
            SysConfig
        }





        public enum DepartmentList
        {
            d_1KC900,
            d_1KCD00,
            d_KD0B00,
            d_KD1100,
            d_KD1200,
            d_KD1300,
            d_KD1500,
            d_KD1600,
            d_KD1700,
            d_KD1C00,
            d_KD1E00,
            d_KD1M00,
            d_KD1P00,
            d_KD1Q00,
            d_KD1S00,
            d_KD1T00,
            d_KD1W00
        }




        #endregion


        /// <summary>
        /// check folder
        /// </summary>
        public static void checkFolder()
        {
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
                File.SetAttributes(appFolder, FileAttributes.Hidden);
                
            }

        }

        /// <summary>
        /// create ini file
        /// </summary>
        /// <param name="inifilepath">ini file path </param>
        public static void createIniFile(string inifilepath)
        {
            IniFile.CreateIniFile(inifilepath);
            File.SetAttributes(inifilepath , FileAttributes.Hidden);
            IniFile.IniWriteValue(IniSection.SysConfig.ToString(), "MyDepartment", myDepartment, inifilepath);

        }

        /// <summary>
        /// read ini file value 
        /// </summary>
        /// <param name="inifilepath">ini file path</param>
        public static void readIniValue(string inifilepath)
        {
            myDepartment = IniFile.IniReadValue(IniSection.SysConfig.ToString(), "MyDepartment", inifilepath);
        }


        /// <summary>
        /// check db file ,if not exits,create it
        /// </summary>
        /// <param name="_dbfile">db file path</param>
        /// <returns></returns>
        public static bool checkDB(string _dbfile)
        {
            if (!File.Exists(_dbfile))
            {
                try
                {
                    SQLiteConnection.CreateFile(_dbfile);

                    if (!p.createAllTable())
                        Environment.Exit(0);
                    p.writeDefaultData();


                    return true;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Create DB Fail." + ex.Message, "Create DB Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


            }
            return true;
        }


        // string sql = "create table highscores (name varchar(20), score int)";
        /// <summary>
        /// create table 
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>create ok,return true;create ng,return false</returns>
        public static bool createTable(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(dbConnectionString);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Connect to database fail," + ex.Message);
                return false;
            }

            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Create TABLE fail," + ex.Message);
                conn.Close();
                return false;
                    
            }
            
            return true;
        }


        /// <summary>
        /// create all defaul tables
        /// </summary>
        public static bool  createAllTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS d_alldepstatus(
depcode varchar(11) PRIMARY KEY NOT NULL,
totalworkingtime decimal(10,2) NULL,
meetingworkingtime decimal(10,2)  NULL,
reportworkingtime decimal(10,2) NULL,
meetingtips int(11) ,
reporttips int(11) ,
meetingtipssavetime decimal(10,2) NULL,
reporttipssavetime decimal(10,2) NULL
)";
            if (!p.createTable(sql))
                return false;

            if (!createAllDeptable())
                return false;

            if (!createTableSys())
                return false;
            return true;

////            1KC900
////1KCD00
////KD0B00
////KD1100
////KD1200
////KD1300
////KD1500
////KD1600
////KD1700
////KD1C00
////KD1E00
////KD1M00
////KD1P00
////KD1Q00




//            sql = @"CREATE TABLE IF NOT EXISTS d_1kc900(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_1kcd00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd0b00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1100(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1200(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1300(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1500(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1600(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1700(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);




//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1c00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);


//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1e00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1m00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1p00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1s00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);

//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1t00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);


//            sql = @"CREATE TABLE IF NOT EXISTS d_kd1w00(
//date date PRIMARY KEY,
//dailymeetingtips int(11),
//dailyreporttips int(11),
//dailymeetingtipssavetime decimal(10,2) NULL,
//dailyreporttipssavetime decimal(10,2) NULL
//)";
//            p.createTable(sql);
        }



        private static bool  createAllDeptable()
        {
            if (!createDepTable(DepartmentList.d_1KC900))
                return false;
            if (!createDepTable(DepartmentList.d_1KCD00))
                return false;
            if (!createDepTable(DepartmentList.d_KD0B00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1100))
                return false;
            if (!createDepTable(DepartmentList.d_KD1200))
                return false;
            if (!createDepTable(DepartmentList.d_KD1300))
                return false;
            if (!createDepTable(DepartmentList.d_KD1500))
                return false;
            if (!createDepTable(DepartmentList.d_KD1600))
                return false;
            if (!createDepTable(DepartmentList.d_KD1700))
                return false;
            if (!createDepTable(DepartmentList.d_KD1C00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1E00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1M00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1P00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1Q00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1S00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1T00))
                return false;
            if (!createDepTable(DepartmentList.d_KD1W00))
                return false;
            if (!createTableSys())
                return false;
            return true;
            
        }





        /// <summary>
        /// create deparmtent table
        /// </summary>
        /// <param name="dep">deppartment</param>
        public static bool  createDepTable(DepartmentList dep)
        {
            string  sql = @"CREATE TABLE IF NOT EXISTS " + dep.ToString ().ToLower () +@"(
date date PRIMARY KEY,
dailymeetingtips int(11),
dailyreporttips int(11),
dailymeetingtipssavetime decimal(10,2) NULL,
dailyreporttipssavetime decimal(10,2) NULL
)";
            if (p.createTable(sql))
                return true;
            else
                return false;
        }






        /// <summary>
        /// 
        /// </summary>
        public static bool  createTableSys()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS " + "sys" + @"(
version  varchar(11) PRIMARY KEY NOT NULL,
value varchar(11) NULL)";
            if (p.updateData2DB(sql))
                return true;
            else
                return false;
        }



        /// <summary>
        /// update data to sqlite
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>success,return true;fail,return false</returns>
        public static bool updateData2DB(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(dbConnectionString);

            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                //cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Execute sql: " + sql + " fail," + ex.Message);
                return false;
            }
            return true;
        }



        public static void writeDefaultData()
        {

            writeDepDefaultData(DepartmentList.d_1KC900);
            writeDepDefaultData(DepartmentList.d_1KCD00);
            writeDepDefaultData(DepartmentList.d_KD0B00);
            writeDepDefaultData(DepartmentList.d_KD1100);
            writeDepDefaultData(DepartmentList.d_KD1200);
            writeDepDefaultData(DepartmentList.d_KD1300);
            writeDepDefaultData(DepartmentList.d_KD1500);
            writeDepDefaultData(DepartmentList.d_KD1600);
            writeDepDefaultData(DepartmentList.d_KD1700);
            writeDepDefaultData(DepartmentList.d_KD1C00);
            writeDepDefaultData(DepartmentList.d_KD1E00);
            writeDepDefaultData(DepartmentList.d_KD1M00);
            writeDepDefaultData(DepartmentList.d_KD1P00);
            writeDepDefaultData(DepartmentList.d_KD1Q00);
            writeDepDefaultData(DepartmentList.d_KD1S00);
            writeDepDefaultData(DepartmentList.d_KD1T00);
            writeDepDefaultData(DepartmentList.d_KD1W00);
            writeVersionDefault();

        }

        private static  void writeDepDefaultData(DepartmentList dep)
        {
            string sql = "REPLACE INTO d_alldepstatus (depcode) VALUES ('" + dep.ToString().Replace("d_", "") + "')";
            updateData2DB(sql);
        }


        private static void writeVersionDefault()
        {
            string sql = "REPLACE INTO sys (version,value) VALUES ('version','" + Application.ProductVersion + "')";
            updateData2DB(sql);
        }
        



        /// <summary>
        /// calc percentage 
        /// </summary>
        /// <param name="member">分子</param>
        /// <param name="denominator">分母</param>
        /// <returns></returns>
        public static string CalcPCT(decimal member, decimal denominator)
        {
            try
            {
                return string.Format("{0:0.00%}", member / denominator);
            }
            catch (Exception)
            {

                return "0.00%";
            }

            

        }


        /// <summary>
        /// 判断是否为工作日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWorkDay(DateTime dt)
        {
            //先从日期表中，查找不是上班时间，如果不是直接返回 false ，如果是，直接返回 true。
            //如果在日期表中，找不到，则查找定义的日历，依据日历定义的周末时间来定义是否为工作日。
            //获取日历中不上班的标准周末时间,判断是不是上班时间
            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                return false;
            else
                return true;
        }



        /// <summary>
        /// check the string if it is decimal
        /// </summary>
        /// <param name="str">string </param>
        /// <returns>Hex,return true;not hex,return false</returns>
        public static bool IsDecimal(string str)
        {
            return Regex.IsMatch(str, @"^[0-9,.]*$");
        }

        /// <summary>
        /// check the string if it is int
        /// </summary>
        /// <param name="str">string </param>
        /// <returns>Hex,return true;not hex,return false</returns>
        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*$");
        }


        /// <summary>
        /// 设置ListItem的字体大小,颜色
        /// </summary>
        /// <param name="li">需要设置的那一项</param>
        /// <param name="fontSize">字体大小,如9</param>
        public static void SetListItemFont(ListViewItem li, int fontSize)//Color fontColor)
        {
            System.Drawing.Font myFont;
            string strName = "Calibri";
            FontStyle myFontStyle;
            int sngSize;
            sngSize = fontSize;
            //int intColorR = 255;
            //int intColorG = 0;
            //int intColorB = 0;
            myFontStyle = FontStyle.Bold;
            Color myColor;
            myColor = Color.Red;
            //myColor = fontColo

            FontFamily myFontFamily;
            myFontFamily = new FontFamily(strName);
            myFont = new Font(myFontFamily, sngSize, myFontStyle, GraphicsUnit.Point);
            li.Font = myFont;
        }


        /// <summary>
        /// 设置ListItem的字体大小,颜色
        /// </summary>
        /// <param name="li">需要设置的那一项</param>
        /// <param name="fontSize">字体大小,如9</param>
        public static void SetListItemFont(ListViewItem li, int fontSize,Color fontColor)
        {
            System.Drawing.Font myFont;
            string strName = "Calibri";
            FontStyle myFontStyle;
            int sngSize;
            sngSize = fontSize;
            //int intColorR = 255;
            //int intColorG = 0;
            //int intColorB = 0;
            myFontStyle = FontStyle.Bold;
            Color myColor;
            myColor = fontColor;
            //myColor = fontColo

            FontFamily myFontFamily;
            myFontFamily = new FontFamily(strName);
            myFont = new Font(myFontFamily, sngSize, myFontStyle, GraphicsUnit.Point);
            li.Font = myFont;
        }
    }
}
