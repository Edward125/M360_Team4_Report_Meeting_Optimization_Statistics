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
            tsslAppName.Text = "Author:edward_song@yeah.net";
            tsslStatus.Text ="Current Deparment:" + p.myDepartment;


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

            string sql = "REPLACE INTO d_1kc900 (date,dailymeetingtips,dailyreporttips,dailymeetingtipssavetime,dailyreporttipssavetime) VALUES ('" + DateTime.Now.ToString ("yyyy-MM-dd") + "','3','2','300','200')";
            
                
            p.updateData2DB(sql);

            sql = "REPLACE INTO d_alldepstatus (depcode,totalworkingtime,meetingworkingtime,reportworkingtime) VALUES ('1KC900','20000','2000','3000')";

            p.updateData2DB(sql);
            sql = "REPLACE INTO d_1kc900 (date,dailymeetingtips,dailyreporttips,dailymeetingtipssavetime,dailyreporttipssavetime) VALUES ('2017-04-23','1','2','20','50')";
            p.updateData2DB(sql);
            //
            //loadMeetingReportStatus(lstMeetingReportStatus);
            setMeeringReport(lstMeetingReportStatus);
            setMeeting(lstMeeting);
            setReport(lstReport);
            addTimeList(lstMeetingDetail);
            
            loadMeetingReportStatus(lstMeetingReportStatus);



            



        }





        #region loadMeetingReportSummaryStatus

        private void setMeeringReport(ListView listview)
        {

            listview.MultiSelect = false;
            listview.AutoArrange = true;
            listview.GridLines = true;
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
                while (re.Read()) 
                {
                    ListViewItem lt = new ListViewItem ();
                    string _depcode = re["depcode"].ToString();
                    if (string.IsNullOrEmpty(_depcode))
                        lt = listview.Items.Add(_depcode.Replace("d_", "").ToUpper());
                    else
                        lt = listview.Items.Add(_depcode);

                    try
                    {
                        decimal _totalworkingtime = Convert.ToDecimal(re["totalworkingtime"]);
                        lt.SubItems.Add(_totalworkingtime.ToString());
                        decimal _meetingworkingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                        lt.SubItems.Add(_meetingworkingtime.ToString());
                        decimal _reportworkingtime = Convert.ToDecimal(re["reportworkingtime"]);
                        lt.SubItems.Add(_reportworkingtime.ToString());
                        lt.SubItems.Add(p.CalcPCT(_meetingworkingtime, _totalworkingtime));
                        lt.SubItems.Add(p.CalcPCT(_reportworkingtime, _totalworkingtime));

                        p.DepartmentList dep = (p.DepartmentList )Enum.Parse (typeof (p.DepartmentList ),"d_" + _depcode );
                        loadDepMeetingReportHistory(dep, lt, _totalworkingtime, _meetingworkingtime, _reportworkingtime);


                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }


                    
                }
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
            if (re.HasRows)
            {
                while (re.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    string _depcode = re["depcode"].ToString();
                    if (string.IsNullOrEmpty(_depcode))
                        lt = listview.Items.Add(_depcode.Replace("d_", "").ToUpper());
                    else
                        lt = listview.Items.Add(_depcode);

                    try
                    {
                        decimal _totalworkingtime = Convert.ToDecimal(re["totalworkingtime"]);
                        lt.SubItems.Add(_totalworkingtime.ToString());
                        decimal _meetingworkingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                        lt.SubItems.Add(_meetingworkingtime.ToString());
                        //decimal _reportworkingtime = Convert.ToDecimal(re["reportworkingtime"]);
                        //lt.SubItems.Add(_reportworkingtime.ToString());
                        lt.SubItems.Add(p.CalcPCT(_meetingworkingtime, _totalworkingtime));
                        //lt.SubItems.Add(p.CalcPCT(_reportworkingtime, _totalworkingtime));
                        p.DepartmentList dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + _depcode);
                        loadDepMeetingOrReportDetail(dep, lt, _totalworkingtime, _meetingworkingtime, 1, "meeting");
                    }
                    catch (Exception)
                    {

                        //throw;
                    }



                }
            }
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
            if (re.HasRows)
            {
                while (re.Read())
                {
                    ListViewItem lt = new ListViewItem();
                    string _depcode = re["depcode"].ToString();
                    if (string.IsNullOrEmpty(_depcode))
                        lt = listview.Items.Add(_depcode.Replace("d_", "").ToUpper());
                    else
                        lt = listview.Items.Add(_depcode);

                    try
                    {
                        decimal _totalworkingtime = Convert.ToDecimal(re["totalworkingtime"]);
                        lt.SubItems.Add(_totalworkingtime.ToString());
                       // decimal _meetingworkingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                        //lt.SubItems.Add(_meetingworkingtime.ToString());
                        decimal _reportworkingtime = Convert.ToDecimal(re["reportworkingtime"]);
                        lt.SubItems.Add(_reportworkingtime.ToString());
                        //lt.SubItems.Add(p.CalcPCT(_meetingworkingtime, _totalworkingtime));
                        lt.SubItems.Add(p.CalcPCT(_reportworkingtime, _totalworkingtime));
                    }
                    catch (Exception)
                    {

                        //throw;
                    }



                }
            }

            conn.Close();
            listview.EndUpdate();//结束数据处理，UI界面一次性绘制。 
        }

        #endregion



        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabMeetingReport )
            {
                loadMeetingReportStatus(lstMeetingReportStatus);
            }

            if (e.TabPage == tabMeeting)
            {
                loadMeetingStatus(lstMeeting);
            }

            if (e.TabPage == tabReport)
            {
                loadReportStatus(lstReport);
            }
        }




        private void addTimeList(ListView listview)
        {
            listview.MultiSelect = false;
            listview.AutoArrange = true;
            listview.GridLines = true;
            for (DateTime dt = p.sysStart; dt < DateTime.Now; dt = dt.AddDays(1))
            {
                if (p.IsWorkDay(dt))
                {
                    listview.Columns.Add(dt.ToString("yyyy-MM-dd"), 80, HorizontalAlignment.Center);
                }
            }
        }


        private void loadDepMeetingReportHistory(p.DepartmentList dep,ListViewItem lt,decimal _totaltime,decimal _totalmeetingtime,decimal _totalreporttime)
        {
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            string sql = "SELECT COUNT(*) FROM " + dep.ToString();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            object o = cmd.ExecuteScalar();
            if ( (Convert.ToInt64(o)) > 0)
            {
                sql = "SELECT * FROM " + dep.ToString();
                cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader re = cmd.ExecuteReader ();


                if (re.HasRows)
                {
                    Int64 _dailymeetingtips = 0;
                    Int64 _dailyreporttips = 0;
                    decimal _dailymeetingtipssavetime = 0;
                    decimal _dailyreporttipssavetime = 0;
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
            


        }

        private void loadDepMeetingOrReportDetail(p.DepartmentList dep, ListViewItem lt, decimal _totaltime, decimal _totalmeetingtime, decimal _totalreporttime, string meetingOrreport)
        {
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            string sql = "SELECT COUNT(*) FROM " + dep.ToString();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            object o = cmd.ExecuteScalar();
            if ((Convert.ToInt64(o)) > 0)
            {
                sql = "SELECT * FROM " + dep.ToString() + " ORDER BY date DESC";
                cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader re = cmd.ExecuteReader();


                if (re.HasRows)
                {
                    Int64 _dailymeetingtips = 0;
                    Int64 _dailyreporttips = 0;
                    decimal _dailymeetingtipssavetime = 0;
                    decimal _dailyreporttipssavetime = 0;
                    while (re.Read())
                    {

                        try
                        {
                            string date = re["date"].ToString();

                            if (meetingOrreport.ToLower() == "meeting")
                            {
                                _dailymeetingtips = _dailymeetingtips + Convert.ToInt64(re["dailymeetingtips"]);
                                _dailymeetingtipssavetime = _dailymeetingtipssavetime  + Convert.ToDecimal(re["dailymeetingtipssavetime"]);
                                //listview.Items[date] .SubItems.Add(_dailymeetingtips.ToString());
                                //listview.Items[date].SubItems.Add(re["dailymeetingtips"].ToString());

                            }
                            if (meetingOrreport.ToLower() == "report")
                            {
                                _dailyreporttips =  Convert.ToInt64(re["dailyreporttips"]);
                                _dailyreporttipssavetime = Convert.ToDecimal(re["dailyreporttipssavetime"]);
                            }

                        }
                        catch (Exception)
                        {

                            // throw;
                        }

                    }
                    if (meetingOrreport.ToLower() == "meeting")
                    {
                        lt.SubItems.Add(_dailymeetingtips.ToString());
                        lt.SubItems.Add(_dailymeetingtipssavetime.ToString());
                        lt.SubItems.Add(p.CalcPCT(_dailymeetingtipssavetime, _totalmeetingtime));
                        lt.SubItems.Add(p.CalcPCT(_dailymeetingtipssavetime, _totaltime));
                    }
                    if (meetingOrreport.ToLower() == "report")
                    {
                        lt.SubItems.Add(_dailyreporttips.ToString());
                        lt.SubItems.Add(_dailyreporttipssavetime.ToString());
                        lt.SubItems.Add(p.CalcPCT(_dailyreporttipssavetime, _totalreporttime));
                        lt.SubItems.Add(p.CalcPCT(_dailyreporttipssavetime, _totaltime));
                    }
                   
                    
                }

            }
        }





    }
}
