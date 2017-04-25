using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region 窗体放大缩小

        private float X;
        private float Y;

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }

        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
           // this.Text = this.Width.ToString() + " " + this.Height.ToString();

        }

        #endregion
        


        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + ",Ver.:" + Application.ProductVersion + "(" + p.myDepartment + ")";
            tsslAppName.Text = "Author:edward_song@yeah.net | ";
            tsslStatus.Text = "Current Deparment:" + p.myDepartment + " | ";
            tsslDepStatus.Text = "";


            //窗体放大缩小
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;
            Y = this.Height;    
            setTag(this);
            Form1_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用

          //
            if (!p.checkDB(p.dbFile))
            {
                Application.Exit();
            }
            else
            {
                p.createAllTable();
                p.writeDefaultData();
            }
            //test
           // string sql = "INSERT INTO d_alldepstatus (depcode,totalworkingtime,meetingworkingtime,reportworkingtime) VALUES ('KD1200','20000','2000','3000')";

            
#if DEBUG
            string sql = "REPLACE INTO d_1kc900 (date,dailymeetingtips,dailyreporttips,dailymeetingtipssavetime,dailyreporttipssavetime) VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd") + "','3','2','300','200')";
            p.updateData2DB(sql);
            sql = "REPLACE INTO d_alldepstatus (depcode,totalworkingtime,meetingworkingtime,reportworkingtime) VALUES ('1KC900','20000','2000','3000')";
            p.updateData2DB(sql);
            sql = "REPLACE INTO d_alldepstatus (depcode,totalworkingtime,meetingworkingtime,reportworkingtime) VALUES ('KD1200','30000','1000','2500')";
            p.updateData2DB(sql);
            sql = "REPLACE INTO d_1kc900 (date,dailymeetingtips,dailyreporttips,dailymeetingtipssavetime,dailyreporttipssavetime) VALUES ('2017-04-23','1','2','20','50')";
            p.updateData2DB(sql);
#endif
            //
            //loadMeetingReportStatus(lstMeetingReportStatus);
            setMeeringReport(lstMeetingReportStatus);
            setMeeting(lstMeeting);
            setReport(lstReport);
            addDetail(lstMeetingDetail);
            addDetail(lstReportDetail);
            //

            loadMeetingReportStatus(lstMeetingReportStatus);
            
        }





        #region loadMeetingReportSummaryStatus

        private void setMeeringReport(ListView listview)
        {

            listview.MultiSelect = false;
            listview.AutoArrange = true;
            listview.GridLines = true;
            listview.FullRowSelect = true;
            listview.Columns.Add("Dep.Code", 60, HorizontalAlignment.Center);
            listview.Columns.Add("Total Time", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Time", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Report Time", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting PCT", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Report PCT", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Total TIPs", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Total TIPs", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Total Save Time", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Total Save Time", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Optimize PCT", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Optimize PCT", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
        }

        private void loadMeetingReportStatus(ListView listview)
        {
            listview.Items.Clear();
            listview.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            //
            string sql = "SELECT * FROM d_alldepstatus";
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                ListViewItem lt = new ListViewItem();
                decimal _T_totalworkingtime = 0;
                decimal _T_meetingworkingtime = 0;
                decimal _T_reportworkingtime = 0;
                Int64 _T_meetingtips = 0;
                Int64 _T_reporttips = 0;
                decimal _T_meetingtipssavetime = 0;
                decimal _T_reporttipssavetime = 0;
                while (re.Read()) 
                {


                    Int64 _meetingtips = 0;
                    Int64 _reporttips = 0;
                    decimal _meetingtipssavetime = 0;
                    decimal _reporttipssavetime = 0;



                    string _depcode = re["depcode"].ToString();
                    if (string.IsNullOrEmpty(_depcode))
                        lt = listview.Items.Add(_depcode.Replace("d_", "").ToUpper());
                    else
                        lt = listview.Items.Add(_depcode);

                    try
                    {
                        decimal _totalworkingtime = Convert.ToDecimal(re["totalworkingtime"]);
                        _T_totalworkingtime = _T_totalworkingtime + _totalworkingtime;
                        lt.SubItems.Add(_totalworkingtime.ToString());
                        decimal _meetingworkingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                        _T_meetingworkingtime = _T_meetingworkingtime + _meetingworkingtime;
                        lt.SubItems.Add(_meetingworkingtime.ToString());
                        decimal _reportworkingtime = Convert.ToDecimal(re["reportworkingtime"]);
                        _T_reportworkingtime = _T_reportworkingtime + _reportworkingtime;
                        lt.SubItems.Add(_reportworkingtime.ToString());
                        lt.SubItems.Add(p.CalcPCT(_meetingworkingtime, _totalworkingtime));
                        lt.SubItems.Add(p.CalcPCT(_reportworkingtime, _totalworkingtime));                       
                        p.DepartmentList dep = (p.DepartmentList )Enum.Parse (typeof (p.DepartmentList ),"d_" + _depcode );
                        loadDepMeetingReportHistory(dep, lt, _totalworkingtime, _meetingworkingtime, _reportworkingtime, out _meetingtips,out  _reporttips, out _meetingtipssavetime,out  _reporttipssavetime);

                        _T_meetingtips = _T_meetingtips + _meetingtips;
                        _T_reporttips = _T_reporttips + _reporttips;
                        _T_meetingtipssavetime = _T_meetingtipssavetime + _meetingtipssavetime;
                        _T_reporttipssavetime = _T_reporttipssavetime + _reporttipssavetime;
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }

                }

                lt = listview.Items.Add("Total");
                lt.SubItems.Add(_T_totalworkingtime.ToString());
                lt.SubItems.Add(_T_meetingworkingtime.ToString());
                lt.SubItems.Add(_T_reportworkingtime.ToString());
                lt.SubItems.Add(p.CalcPCT(_T_meetingworkingtime, _T_totalworkingtime));
                lt.SubItems.Add(p.CalcPCT(_T_reportworkingtime, _T_totalworkingtime));
                lt.SubItems.Add(_T_meetingtips.ToString());
                lt.SubItems.Add(_T_reporttips.ToString());
                lt.SubItems.Add(_T_meetingtipssavetime.ToString ());
                lt.SubItems.Add(_T_reporttipssavetime.ToString ());
                lt.SubItems.Add(p.CalcPCT(_T_meetingtipssavetime, _T_meetingworkingtime));
                lt.SubItems.Add(p.CalcPCT(_T_reporttipssavetime, _T_reportworkingtime));
                lt.SubItems.Add(p.CalcPCT(_T_meetingtipssavetime, _T_totalworkingtime));
                lt.SubItems.Add(p.CalcPCT(_T_reporttipssavetime, _T_totalworkingtime));
                
                


               // MessageBox.Show(listview.Items["KD1200"].SubItems["Meeting Time"].Text);

        
               
            }


            
            conn.Close();
            listview.EndUpdate();//结束数据处理，UI界面一次性绘制。 
        }


        #endregion



        #region loadMeetingStatus


        private void setMeeting(ListView listview)
        {
            listview.Items.Clear();
            listview.MultiSelect = false;
            listview.FullRowSelect = true;
            //listview.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            listview.AutoArrange = true;
            listview.GridLines = true;
            listview.Columns.Add("Dep.Code", 60, HorizontalAlignment.Center);
            listview.Columns.Add("Total Time", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Time", 80, HorizontalAlignment.Center);
            //listview.Columns.Add("Report Time", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting PCT", 80, HorizontalAlignment.Center);
            //listview.Columns.Add("Report PCT", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Total TIPs", 120, HorizontalAlignment.Center);
            // listview.Columns.Add("Report Total TIPs", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Total Save Time", 120, HorizontalAlignment.Center);
            // listview.Columns.Add("Report Total Save Time", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Optimize PCT", 120, HorizontalAlignment.Center);
            // listview.Columns.Add("Report Optimize PCT", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Meeting Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
            //listview.Columns.Add("Report Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
            //
           // addTimeList(lstMeeting);




        }


        private void loadMeetingStatus(ListView listview)
        {
            listview.Items.Clear();
            listview.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            //
            string sql = "SELECT * FROM d_alldepstatus";
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader re = cmd.ExecuteReader();
            ListViewItem lt = new ListViewItem();

            decimal _T_totalworkingtime = 0;
            decimal _T_meetingworkingtime = 0;
            Int64 _T_meetingtips = 0;
            decimal _T_meetingtipssavetime = 0;
            

            if (re.HasRows)
            {
                while (re.Read())
                {
                    
                   Int64  _meetingtips = 0;
                   decimal _meetingtipssavetime = 0;

                    string _depcode = re["depcode"].ToString();
                    if (string.IsNullOrEmpty(_depcode))
                        lt = listview.Items.Add(_depcode.Replace("d_", "").ToUpper());
                    else
                        lt = listview.Items.Add(_depcode);

                    try
                    {
                        decimal _totalworkingtime = Convert.ToDecimal(re["totalworkingtime"]);
                        lt.SubItems.Add(_totalworkingtime.ToString());
                        _T_totalworkingtime = _T_totalworkingtime + _totalworkingtime;
                        decimal _meetingworkingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                        lt.SubItems.Add(_meetingworkingtime.ToString());
                        _T_meetingworkingtime = _T_meetingworkingtime + _meetingworkingtime;
                        //decimal _reportworkingtime = Convert.ToDecimal(re["reportworkingtime"]);
                        //lt.SubItems.Add(_reportworkingtime.ToString());
                        lt.SubItems.Add(p.CalcPCT(_meetingworkingtime, _totalworkingtime));
                        //lt.SubItems.Add(p.CalcPCT(_reportworkingtime, _totalworkingtime));
                        p.DepartmentList dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + _depcode);
                        loadDepMeetingOrReportDetail(dep, lt, _totalworkingtime, _meetingworkingtime , "meeting", out _meetingtips, out _meetingtipssavetime);
                        _T_meetingtips = _T_meetingtips + _meetingtips;
                        _T_meetingtipssavetime = _T_meetingtipssavetime + _meetingtipssavetime;
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    
                }
            }

            lt = listview.Items.Add("Total");
            lt.SubItems.Add(_T_totalworkingtime.ToString());
            lt.SubItems.Add(_T_meetingworkingtime.ToString());
            lt.SubItems.Add(p.CalcPCT(_T_meetingworkingtime, _T_totalworkingtime));
            lt.SubItems.Add(_T_meetingtips.ToString());
            lt.SubItems.Add(_T_meetingtipssavetime.ToString());
            lt.SubItems.Add(p.CalcPCT(_T_meetingtipssavetime, _T_meetingworkingtime));
            lt.SubItems.Add(p.CalcPCT(_T_meetingtipssavetime, _T_totalworkingtime));
           


            conn.Close();

            listview.EndUpdate();//结束数据处理，UI界面一次性绘制。 
        }


        #endregion


        #region loadReportStatus

        private void setReport(ListView listview)
        {
            listview.MultiSelect = false;
            listview.AutoArrange = true;
            listview.GridLines = true;
            listview.FullRowSelect = true;
            listview.Columns.Add("Dep.Code", 60, HorizontalAlignment.Center);
            listview.Columns.Add("Total Time", 80, HorizontalAlignment.Center);
            //listview.Columns.Add("Meeting Time", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Report Time", 80, HorizontalAlignment.Center);
            //listview.Columns.Add("Meeting PCT", 80, HorizontalAlignment.Center);
            listview.Columns.Add("Report PCT", 80, HorizontalAlignment.Center);
            //listview.Columns.Add("Meeting Total TIPs", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Total TIPs", 120, HorizontalAlignment.Center);
            //listview.Columns.Add("Meeting Total Save Time", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Total Save Time", 120, HorizontalAlignment.Center);
            // listview.Columns.Add("Meeting Optimize PCT", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Optimize PCT", 120, HorizontalAlignment.Center);
            //listview.Columns.Add("Meeting Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
            listview.Columns.Add("Report Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
            //
            
        }


        private void loadReportStatus(ListView listview)
        {

            listview.Items.Clear();
            listview.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            //
            string sql = "SELECT * FROM d_alldepstatus";
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader re = cmd.ExecuteReader();
            ListViewItem lt = new ListViewItem();

            decimal _T_totalworkingtime = 0;
            decimal _T_reportworkingtime = 0;
            Int64 _T_reporttips = 0;
            decimal _T_reporttipssavetime = 0;


            if (re.HasRows)
            {
                while (re.Read())
                {

                    Int64 _reporttips = 0;
                    decimal _reporttipssavetime = 0;


                    string _depcode = re["depcode"].ToString();
                    if (string.IsNullOrEmpty(_depcode))
                        lt = listview.Items.Add(_depcode.Replace("d_", "").ToUpper());
                    else
                        lt = listview.Items.Add(_depcode);

                    try
                    {
                        decimal _totalworkingtime = Convert.ToDecimal(re["totalworkingtime"]);
                        lt.SubItems.Add(_totalworkingtime.ToString());
                        _T_totalworkingtime = _T_totalworkingtime + _totalworkingtime;
                       // decimal _meetingworkingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                        //lt.SubItems.Add(_meetingworkingtime.ToString());
                        decimal _reportworkingtime = Convert.ToDecimal(re["reportworkingtime"]);
                        lt.SubItems.Add(_reportworkingtime.ToString());
                        _T_reportworkingtime = _T_reportworkingtime + _reportworkingtime;
                        //lt.SubItems.Add(p.CalcPCT(_meetingworkingtime, _totalworkingtime));
                        lt.SubItems.Add(p.CalcPCT(_reportworkingtime, _totalworkingtime));

                        p.DepartmentList dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + _depcode);
                        loadDepMeetingOrReportDetail(dep, lt, _totalworkingtime, _reportworkingtime, "report", out _reporttips, out _reporttipssavetime);
                        _T_reporttips = _T_reporttips + _reporttips;
                        _T_reporttipssavetime = _T_reporttipssavetime + _reporttipssavetime;
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }

            lt = listview.Items.Add("Total");
            lt.SubItems.Add(_T_totalworkingtime.ToString());
            lt.SubItems.Add(_T_reportworkingtime.ToString());
            lt.SubItems.Add(p.CalcPCT(_T_reportworkingtime, _T_totalworkingtime));
            lt.SubItems.Add(_T_reporttips.ToString());
            lt.SubItems.Add(_T_reporttipssavetime.ToString());
            lt.SubItems.Add(p.CalcPCT(_T_reporttipssavetime, _T_reportworkingtime));
            lt.SubItems.Add(p.CalcPCT(_T_reporttipssavetime, _T_totalworkingtime));



            conn.Close();
            listview.EndUpdate();//结束数据处理，UI界面一次性绘制。 
        }

        #endregion



        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabMeetingReport )
            {
                loadMeetingReportStatus(lstMeetingReportStatus);
                tsslDepStatus.Text = "";
            }

            if (e.TabPage == tabMeeting)
            {
                loadMeetingStatus(lstMeeting);
                tsslDepStatus.Text = "";
            }

            if (e.TabPage == tabReport)
            {
                loadReportStatus(lstReport);
                tsslDepStatus.Text = "";
            }
        }




        private void addDetail(ListView listview)
        {
            listview.MultiSelect = false;
            listview.AutoArrange = true;
            listview.GridLines = true;
            //for (DateTime dt = p.sysStart; dt < DateTime.Now; dt = dt.AddDays(1))
            //{
            //    if (p.IsWorkDay(dt))
            //    {
            //        listview.Columns.Add(dt.ToString("yyyy-MM-dd"), 80, HorizontalAlignment.Center);
            //    }
            //}

            listview.FullRowSelect = true;
            listview.Columns.Add("Dep.Code", 50, HorizontalAlignment.Center);
            listview.Columns.Add("Date", 80, HorizontalAlignment.Center);
            listview.Columns.Add("TIPs", 40, HorizontalAlignment.Center);        
            listview.Columns.Add("TIPs Save Time", 100, HorizontalAlignment.Center);
            listview.Columns.Add("Optimize PCT", 100, HorizontalAlignment.Center);            
            listview.Columns.Add("Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);

        }

        private void addDetail(ListView listview,string meetingorreport)
        {
            listview.MultiSelect = false;
            listview.AutoArrange = true;
            listview.GridLines = true;
            //for (DateTime dt = p.sysStart; dt < DateTime.Now; dt = dt.AddDays(1))
            //{
            //    if (p.IsWorkDay(dt))
            //    {
            //        listview.Columns.Add(dt.ToString("yyyy-MM-dd"), 80, HorizontalAlignment.Center);
            //    }
            //}

            listview.FullRowSelect = true;
            listview.Columns.Add("Dep.Code", 60, HorizontalAlignment.Center);
            listview.Columns.Add("Date", 80, HorizontalAlignment.Center);

            if (meetingorreport.ToLower() == "meeting")
            {
                listview.Columns.Add("Meeting TIPs", 80, HorizontalAlignment.Center);
                listview.Columns.Add("Meeting TIPs Save Time", 120, HorizontalAlignment.Center);
                listview.Columns.Add("Meeting Optimize PCT", 120, HorizontalAlignment.Center);
                listview.Columns.Add("Meeting Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
            }

            if (meetingorreport.ToLower() == "report")
            {
                listview.Columns.Add("Report TIPs", 80, HorizontalAlignment.Center);
                listview.Columns.Add("Report TIPs Save Time", 120, HorizontalAlignment.Center);
                listview.Columns.Add("Report Optimize PCT", 120, HorizontalAlignment.Center);
                listview.Columns.Add("Report Optimize PCT(Total Working)", 120, HorizontalAlignment.Center);
            }
        }


        private void loadDepMeetingReportHistory(p.DepartmentList dep,ListViewItem lt,decimal _totaltime,decimal _totalmeetingtime,decimal _totalreporttime ,out Int64 meetingtips,out Int64 reporttips,out decimal meetingtipssavetime,out decimal reporttipssavetime)
        {
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            string sql = "SELECT COUNT(*) FROM " + dep.ToString();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            object o = cmd.ExecuteScalar();
            Int64 _dailymeetingtips = 0;
            Int64 _dailyreporttips = 0;
            decimal _dailymeetingtipssavetime = 0;
            decimal _dailyreporttipssavetime = 0;

            if ( (Convert.ToInt64(o)) > 0)
            {
                sql = "SELECT * FROM " + dep.ToString();
                cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader re = cmd.ExecuteReader ();


                if (re.HasRows)
                {
                    
                    while (re.Read())
                    {
                       
                        try
                        {
                            _dailymeetingtips = _dailymeetingtips + Convert.ToInt64(re["dailymeetingtips"]);
                            _dailyreporttips = _dailyreporttips + Convert.ToInt64(re["dailyreporttips"]);
                            _dailymeetingtipssavetime = _dailymeetingtipssavetime + Convert.ToDecimal(re["dailymeetingtipssavetime"]);
                            _dailyreporttipssavetime = _dailyreporttipssavetime + Convert.ToDecimal(re["dailyreporttipssavetime"]);
                           
                        }
                        catch (Exception)
                        {

                           // throw;
                        }
                        
                    }

                    lt.SubItems.Add(_dailymeetingtips.ToString());
                    lt.SubItems.Add(_dailyreporttips.ToString());
                    lt.SubItems.Add(_dailymeetingtipssavetime.ToString());
                    lt.SubItems.Add(_dailyreporttipssavetime.ToString());
                    lt.SubItems.Add(p.CalcPCT(_dailymeetingtipssavetime, _totalmeetingtime));
                    lt.SubItems.Add(p.CalcPCT(_dailyreporttipssavetime, _totalreporttime));
                    lt.SubItems.Add(p.CalcPCT(_dailymeetingtipssavetime, _totaltime));
                    lt.SubItems.Add(p.CalcPCT(_dailyreporttipssavetime, _totaltime));

                    
                }

            }

            meetingtips = _dailymeetingtips;
            reporttips = _dailyreporttips;
            meetingtipssavetime = _dailymeetingtipssavetime;
            reporttipssavetime = _dailyreporttipssavetime;

        }

        private void loadDepMeetingOrReportDetail(p.DepartmentList dep, ListViewItem lt, decimal _totaltime, decimal _totalmeetingorreporttime, string meetingOrreport ,out Int64 meetingorreporttips, out decimal meetingorreporttipssavetime)
        {
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            string sql = "SELECT COUNT(*) FROM " + dep.ToString();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            object o = cmd.ExecuteScalar();

            Int64 _meetingorreporttips = 0;
            decimal _meetingorreporttipssavetime = 0;

            if ((Convert.ToInt64(o)) > 0)
            {                
                sql = "SELECT * FROM " + dep.ToString() + " ORDER BY date DESC";
                cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader re = cmd.ExecuteReader();

                Int64 _dailymeetingorreporttips = 0;              
                decimal _dailymeetingorreporttipssavetime = 0;              

                if (re.HasRows)
                {
                    
                    while (re.Read())
                    {

                        try
                        {
                            string date = re["date"].ToString();

                            if (meetingOrreport.ToLower() == "meeting")
                            {
                                _dailymeetingorreporttips = _dailymeetingorreporttips + Convert.ToInt64(re["dailymeetingtips"]);
                                _dailymeetingorreporttipssavetime = _dailymeetingorreporttipssavetime  + Convert.ToDecimal(re["dailymeetingtipssavetime"]);
                                //listview.Items[date] .SubItems.Add(_dailymeetingtips.ToString());
                                //listview.Items[date].SubItems.Add(re["dailymeetingtips"].ToString());

                            }
                            if (meetingOrreport.ToLower() == "report")
                            {
                                _dailymeetingorreporttips = _dailymeetingorreporttips + Convert.ToInt64(re["dailyreporttips"]);
                                _dailymeetingorreporttipssavetime = _dailymeetingorreporttipssavetime + Convert.ToDecimal(re["dailyreporttipssavetime"]);
                            }

                        }
                        catch (Exception)
                        {

                            // throw;
                        }

                    }
                    if (meetingOrreport.ToLower() == "meeting")
                    {
                        lt.SubItems.Add(_dailymeetingorreporttips.ToString());
                        _meetingorreporttips = _dailymeetingorreporttips + _meetingorreporttips;
                        lt.SubItems.Add(_dailymeetingorreporttipssavetime.ToString());
                        _meetingorreporttipssavetime = _meetingorreporttipssavetime + _dailymeetingorreporttipssavetime;
                        lt.SubItems.Add(p.CalcPCT(_dailymeetingorreporttipssavetime, _totalmeetingorreporttime ));
                        lt.SubItems.Add(p.CalcPCT(_dailymeetingorreporttipssavetime, _totaltime));
                    }
                    if (meetingOrreport.ToLower() == "report")
                    {
                        lt.SubItems.Add(_dailymeetingorreporttips.ToString());
                        _meetingorreporttips = _dailymeetingorreporttips + _meetingorreporttips;
                        lt.SubItems.Add(_dailymeetingorreporttipssavetime.ToString());
                        _meetingorreporttipssavetime = _meetingorreporttipssavetime + _dailymeetingorreporttipssavetime;
                        lt.SubItems.Add(p.CalcPCT(_dailymeetingorreporttipssavetime, _totalmeetingorreporttime));
                        lt.SubItems.Add(p.CalcPCT(_dailymeetingorreporttipssavetime, _totaltime));
                    }
                   
               }   
      


            }

            meetingorreporttips = _meetingorreporttips;
            meetingorreporttipssavetime = _meetingorreporttipssavetime;
        }


        private void loadDepMeetingOrReportDetailHistory(p.DepartmentList dep,ListView listview,string meetingorreport,decimal _totaltime =1,decimal _totalmeetingorreporttime =1)
        {
            listview.Items.Clear();
            listview.BeginUpdate();
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            string sql = "SELECT COUNT(*) FROM " + dep.ToString();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            object o = cmd.ExecuteScalar();
            if ((Convert.ToInt64(o)) > 0)
            {
                tsslDepStatus.Text = dep.ToString().Replace("d_", "") + " is " + o.ToString () + " record(s) in the database...";
                tsslDepStatus.ForeColor = Color.Blue;
                sql = "SELECT * FROM " + dep.ToString() + " ORDER BY date DESC";
                cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader re = cmd.ExecuteReader();
                if (re.HasRows)
                {
                    decimal _dailytipssavetime = 0;
                    ListViewItem lt = new ListViewItem();
                      while (re.Read())
                    {
                        
                        lt = listview.Items . Add(dep.ToString().Replace("d_", ""));
                        lt.SubItems.Add(Convert.ToDateTime(re["date"].ToString()).ToString("yyy-MM-dd"));
                        if (meetingorreport.ToLower() == "meeting")
                        {
                            lt.SubItems.Add(re["dailymeetingtips"].ToString());
                            lt.SubItems .Add (re["dailymeetingtipssavetime"].ToString ());
                            _dailytipssavetime =  Convert.ToDecimal(re["dailymeetingtipssavetime"]);
                        }
                        if (meetingorreport.ToLower() == "report")
                        {
                            lt.SubItems.Add(re["dailyreporttips"].ToString());
                            lt.SubItems.Add(re["dailyreporttipssavetime"].ToString());
                            _dailytipssavetime =  Convert.ToDecimal(re["dailyreporttipssavetime"]);
                            
                        }
                        lt.SubItems.Add(p.CalcPCT(_dailytipssavetime, _totalmeetingorreporttime));
                        lt.SubItems .Add (p.CalcPCT (_dailytipssavetime ,_totaltime ));
                    }
                }
            }
            else
            {
                tsslDepStatus.Text = dep.ToString ().Replace ("d_","") + " is not any record in the database...";
                tsslDepStatus.ForeColor = Color.Red;

            }
            conn.Close();
            listview.EndUpdate();

        }

        private void lstMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lstMeeting.SelectedItems.Count >=1 )
            {
                //MessageBox.Show(lstMeeting.SelectedItems[0].SubItems.Count.ToString());
                

                string depStr = lstMeeting.SelectedItems[0].SubItems[0].Text;

                if (depStr.ToLower() == "total")
                    return;

                p.DepartmentList dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + depStr);
                decimal _totaltime = 1;
                decimal _meetingtime = 1;
                try
                {
                  _totaltime =  Convert.ToDecimal(lstMeeting.SelectedItems[0].SubItems[1].Text);
                    _meetingtime =  Convert.ToDecimal(lstMeeting.SelectedItems[0].SubItems[2].Text);
                }
                catch (Exception)
                {
                    //throw;
                }             
                loadDepMeetingOrReportDetailHistory(dep, lstMeetingDetail, "meeting",_totaltime, _meetingtime);

            }

        }

        private void lstReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReport.SelectedItems.Count >= 1)
            {
                //MessageBox.Show(lstMeeting.SelectedItems[0].SubItems.Count.ToString());

                string depStr = lstReport.SelectedItems[0].SubItems[0].Text;
                if (depStr.ToLower() == "total")
                    return;
                p.DepartmentList dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + depStr);
                decimal _totaltime = 1;
                decimal _reporttime = 1;
                try
                {
                    _totaltime = Convert.ToDecimal(lstReport.SelectedItems[0].SubItems[1].Text);
                    _reporttime = Convert.ToDecimal(lstReport.SelectedItems[0].SubItems[2].Text);
                }
                catch (Exception)
                {
                    //throw;
                }
                loadDepMeetingOrReportDetailHistory(dep, lstReportDetail, "report", _totaltime, _reporttime);

            }
        }

        private void lstReportDetail_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void lstReport_DoubleClick(object sender, EventArgs e)
        {
            p.titleModifyMeetingReportData = "Modify Report Related Data...(" + p.myDepartment + ")";
            if (lstReport.SelectedItems.Count >= 1)
            {
                //MessageBox.Show(lstMeeting.SelectedItems[0].SubItems.Count.ToString());

                string depStr = lstReport.SelectedItems[0].SubItems[0].Text;
                if (depStr.ToLower() == "total")
                    return;
                if (depStr.ToUpper() != p.myDepartment.ToUpper())
                {
                    MessageBox.Show("u'r not " + depStr + " member,u can only modify your dep.:" + p.myDepartment, "Dep. Not Match", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    frmMeetingReportDailyData f = new frmMeetingReportDailyData();
                    if (f.ShowDialog(this) != DialogResult.OK) //close
                    {
                        //MessageBox.Show("OK");
                        //refresh data
                        loadMeetingReportStatus(lstMeetingReportStatus);
                        tsslDepStatus.Text = "";
                        loadMeetingStatus(lstMeeting);
                        loadReportStatus(lstReport);
                    }
                }
            }
        }

        private void lstMeeting_DoubleClick(object sender, EventArgs e)
        {
            p.titleModifyMeetingReportData = "Modify Meeting Related Data...(" + p.myDepartment + ")";


            if (lstMeeting.SelectedItems.Count >= 1)
            {
                //MessageBox.Show(lstMeeting.SelectedItems[0].SubItems.Count.ToString());

                string depStr = lstMeeting.SelectedItems[0].SubItems[0].Text;
                if (depStr.ToLower() == "total")
                    return;
                if (depStr.ToUpper() != p.myDepartment.ToUpper())
                {
                    MessageBox.Show("u'r not " + depStr + " member,u can only modify your dep.:" + p.myDepartment, "Dep. Not Match", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    frmMeetingReportDailyData f = new frmMeetingReportDailyData();
                    if (f.ShowDialog(this) != DialogResult.OK) //close
                    {
                        //MessageBox.Show("OK");
                        //refresh data
                        loadMeetingReportStatus(lstMeetingReportStatus);
                        tsslDepStatus.Text = "";
                        loadMeetingStatus(lstMeeting);
                        loadReportStatus(lstReport);
                    }
                }
            }
           
        }



    }
}
