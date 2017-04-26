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
    public partial class frmMeetingReportDailyData : Form
    {
        public frmMeetingReportDailyData()
        {
            InitializeComponent();
        }

        //
        decimal _meetingtime = 0;
        decimal _reporttime = 0;
        //
        Int64 _dailymeetingtips = 0;
        Int64 _dailyreporttips = 0;
        decimal _dailymeetingtipssavetime = 0;
        decimal _dailyreporttipssavetime = 0;

        private void frmMeetingReportDailyData_Load(object sender, EventArgs e)
        {
            this.Text = p.titleModifyMeetingReportData;
            InitUI(p.titleModifyMeetingReportData);
            loadBaseline();
            p.DepartmentList dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + p.myDepartment);
            loadDailyData(dtpHistoryDate.Value, dep);

        }

        #region InitUI


        private void InitUI(string title)
        {
            if (title.ToLower ().Contains ("meeting"))
            {
                lblMeetingOrReportTIPs.Text = "Meeting TIPs:";
                lblMeetingorReportWorkingTime.Text = "Meeting Time:";  
            }

            if (title.ToLower().Contains("report"))
            {
                lblMeetingOrReportTIPs.Text = "Report TIPs:";
                lblMeetingorReportWorkingTime.Text = "Report Time:";  
            }
            //
            this.txtTotaWorkingTime.ReadOnly = true;
            this.txtMeetingOrReportTime.ReadOnly = true;
            this.btnModify.Enabled = true;
            this.btnSaveData.Enabled = false;
            //
            this.txtMeetingOrReportTIPs.ReadOnly = true;
            this.txtTIPsSaveTime.ReadOnly = true;
            this.btnModifyHistory.Enabled = true;
            this.btnSaveHistoryData.Enabled = false;

        }


        #endregion



        #region loadBaseLine


        private void loadBaseline()
        {
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            string sql = "select * from d_alldepstatus where depcode = '" + p.myDepartment + "'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {
                while (re.Read()) 
                {

                    try
                    {
                        txtTotaWorkingTime.Text = re["totalworkingtime"].ToString();
                        _meetingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                        _reporttime = Convert.ToDecimal(re["reportworkingtime"]);
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }

                    if (p.titleModifyMeetingReportData.ToLower().Contains("meeting"))
                    {
                        try
                        {
                            txtMeetingOrReportTime.Text = re["meetingworkingtime"].ToString();
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }
                       
                        
                    }
                    if (p.titleModifyMeetingReportData.ToLower().Contains("report"))
                    {
                        try
                        {
                            txtMeetingOrReportTime.Text = re["reportworkingtime"].ToString();
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }
                        
                    }
                }
            }
            conn.Close();

        }

        #endregion



        #region loadDailyData


        private void loadDailyData(DateTime dt,p.DepartmentList dep)
        {


            _dailymeetingtips = 0;
            _dailyreporttips = 0;
            _dailymeetingtipssavetime = 0;
            _dailyreporttipssavetime = 0;

            string date = dt.ToString("yyyy-MM-dd");
            string sql = "SELECT * FROM " + dep.ToString().ToLower() + " WHERE date = '" + date + "'";
            SQLiteConnection conn = new SQLiteConnection(p.dbConnectionString);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader re = cmd.ExecuteReader();
            if (re.HasRows)
            {

                tsslUpdateData.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ",there is record in " + dep.ToString().Replace("d_", "") + " database...";
                tsslUpdateData.ForeColor = Color.Blue;

                while (re.Read())
                {
                    try
                    {
                        _dailymeetingtips = Convert.ToInt64(re["dailymeetingtips"]);
                        _dailyreporttips = Convert.ToInt64(re["dailyreporttips"]);
                        _dailymeetingtipssavetime = Convert.ToDecimal(re["dailymeetingtipssavetime"]);
                        _dailyreporttipssavetime = Convert.ToDecimal(re["dailyreporttipssavetime"]);

                        if (p.titleModifyMeetingReportData.ToLower().Contains("report"))
                        {
                            txtMeetingOrReportTIPs.Text = _dailyreporttips.ToString();
                            txtTIPsSaveTime.Text = _dailyreporttipssavetime.ToString();

                        }
                        if (p.titleModifyMeetingReportData.ToLower().Contains("meeting"))
                        {
                            txtMeetingOrReportTIPs.Text = _dailymeetingtips.ToString();
                            txtTIPsSaveTime.Text = _dailymeetingtipssavetime.ToString();

                        }


                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }
                }
            }
            else
            {
                tsslUpdateData.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ",there is no recode in " + dep.ToString().ToUpper().Replace("D_", "") + " database...";
                tsslUpdateData.ForeColor = Color.Red;
            }

        }


        #endregion



        private void txtTotaWorkingTime_TextChanged(object sender, EventArgs e)
        {
            if (!p.IsDecimal(txtTotaWorkingTime.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number or Dot,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtTotaWorkingTime.SelectAll();
                this.txtTotaWorkingTime.Focus();
            }
        }

        private void txtMeetingOrReportTime_TextChanged(object sender, EventArgs e)
        {
            if (!p.IsDecimal(txtMeetingOrReportTime.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number or Dot,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtMeetingOrReportTime.SelectAll();
                this.txtMeetingOrReportTime.Focus();
            }
        }

        private void txtMeetingOrReportTIPs_TextChanged(object sender, EventArgs e)
        {
            if (!p.IsInt(txtMeetingOrReportTIPs.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtMeetingOrReportTIPs.SelectAll();
                this.txtMeetingOrReportTIPs.Focus();
            }
        }

        private void txtTIPsSaveTime_TextChanged(object sender, EventArgs e)
        {
            if (!p.IsDecimal(txtTIPsSaveTime.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number or Dot,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtTIPsSaveTime.SelectAll();
                this.txtTIPsSaveTime.Focus();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {


                 DialogResult dr = MessageBox.Show("R u sure to modify " + p.myDepartment + " baseline data?", "Modify Baseline", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.btnModify.Enabled = false;
                    this.btnSaveData.Enabled = true;
                    this.txtTotaWorkingTime.ReadOnly = false;
                    this.txtMeetingOrReportTime.ReadOnly = false;
                    this.txtTotaWorkingTime.SelectAll();
                    this.txtTotaWorkingTime.Focus();
                }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            //
            if (!p.IsDecimal(txtTotaWorkingTime.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number or Dot,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtTotaWorkingTime.SelectAll();
                this.txtTotaWorkingTime.Focus();
                return;
            }
            if (!p.IsDecimal(txtMeetingOrReportTime.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number or Dot,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtMeetingOrReportTime.SelectAll();
                this.txtMeetingOrReportTime.Focus();
                return;
            }

            //
            string sql = string.Empty;
            p.DepartmentList  dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + p.myDepartment);
            //
            if (p.titleModifyMeetingReportData.ToLower().Contains("meeting"))
            {
                sql = "REPLACE INTO d_alldepstatus (depcode,totalworkingtime,meetingworkingtime,reportworkingtime) VALUES ('" + p.myDepartment + "','" + txtTotaWorkingTime.Text.Trim() + "','" + txtMeetingOrReportTime.Text.Trim() + "','" + _reporttime +"')";
            }
            if (p.titleModifyMeetingReportData.ToLower().Contains("report"))
            {
                sql = "REPLACE INTO d_alldepstatus (depcode,totalworkingtime,meetingworkingtime,reportworkingtime) VALUES ('" + p.myDepartment + "','" + txtTotaWorkingTime.Text.Trim() + "','" + _meetingtime +"','"+ txtMeetingOrReportTime.Text.Trim() + "')";
            }

            if (p.updateData2DB(sql))
            {
                tsslUpdateData.Text = DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss") + ",update " + p.myDepartment + " baseline data success...";
                tsslUpdateData.ForeColor = Color.Blue;

                this.btnModify.Enabled = true;
                this.btnSaveData.Enabled = false;
                this.txtTotaWorkingTime.ReadOnly = true;
                this.txtMeetingOrReportTime.ReadOnly = true;
                

            }
            else
            {
                tsslUpdateData.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ",update " + p.myDepartment + " baseline data fail...";
                tsslUpdateData.ForeColor = Color.Red;
                return;
            }

        }

        private void btnModifyHistory_Click(object sender, EventArgs e)
        {
            //
            if (dtpHistoryDate.Value < p.sysStart)
            {
                MessageBox.Show("u select date is " + dtpHistoryDate.Value.ToString("yyyy-MM-dd") + ", M360 team4 not started...", "Datetime error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpHistoryDate.Focus();
                return;
            }
            if (dtpHistoryDate.Value > DateTime.Now )
            {
                MessageBox.Show("u select date is " + dtpHistoryDate.Value.ToString("yyyy-MM-dd") + ",today is " + DateTime.Now.ToString ("yyyy-MM-dd") , "Datetime error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpHistoryDate.Focus();
                return;
            }
            //
             DialogResult dr = MessageBox.Show("R u sure to modify " + p.myDepartment +  " " + dtpHistoryDate.Value.ToString ("yyyy-MM-dd")+ " detail data?", "Modify detail data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (dr == DialogResult.Yes)
             {

                 this.dtpHistoryDate.Enabled = false;
                 this.txtMeetingOrReportTIPs.ReadOnly = false;
                 this.txtTIPsSaveTime.ReadOnly = false;
                 this.btnModifyHistory.Enabled = false;
                 this.btnSaveHistoryData.Enabled = true;
             }


        }

        private void dtpHistoryDate_ValueChanged(object sender, EventArgs e)
        {

            txtMeetingOrReportTIPs.Text = string.Empty;
            txtTIPsSaveTime.Text = string.Empty;
            p.DepartmentList dep = (p.DepartmentList )Enum.Parse (typeof(p.DepartmentList ),"d_" + p.myDepartment);
            loadDailyData (dtpHistoryDate.Value ,dep);
        }

        private void btnSaveHistoryData_Click(object sender, EventArgs e)
        {
            if (!p.IsInt(txtMeetingOrReportTIPs.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtMeetingOrReportTIPs.SelectAll();
                this.txtMeetingOrReportTIPs.Focus();
                return;
            }
            if (!p.IsDecimal(txtTIPsSaveTime.Text.Trim()))
            {
                MessageBox.Show("what you input is not Number or Dot,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtTIPsSaveTime.SelectAll();
                this.txtTIPsSaveTime.Focus();
                return;
            }
            //
            string sql = string.Empty;
            p.DepartmentList dep = (p.DepartmentList)Enum.Parse(typeof(p.DepartmentList), "d_" + p.myDepartment);
            //
            if (p.titleModifyMeetingReportData.ToLower().Contains("meeting"))
                sql = "REPLACE INTO " + dep.ToString().ToLower() + " (date,dailymeetingtips,dailyreporttips,dailymeetingtipssavetime,dailyreporttipssavetime) VALUES ('" + dtpHistoryDate.Value.ToString("yyyy-MM-dd") + "','" + txtMeetingOrReportTIPs.Text.Trim() + "','" + _dailyreporttips.ToString() + "','" + txtTIPsSaveTime.Text.Trim() + "','" + _dailyreporttipssavetime.ToString() + "')";
                    
            if (p.titleModifyMeetingReportData.ToLower().Contains("report"))
                sql = "REPLACE INTO " + dep.ToString().ToLower() + " (date,dailymeetingtips,dailyreporttips,dailymeetingtipssavetime,dailyreporttipssavetime) VALUES ('" + dtpHistoryDate.Value.ToString("yyyy-MM-dd") + "','" + _dailymeetingtips.ToString() + "','" + txtMeetingOrReportTIPs.Text.Trim() + "','" + _dailymeetingtipssavetime .ToString () + "','" + txtTIPsSaveTime.Text.Trim() + "')";

            if (p.updateData2DB(sql))
            {
                tsslUpdateData.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ",update " + p.myDepartment +  " " + dtpHistoryDate.Value.ToString ("yyyy-MM-dd") +" detail data success...";
                tsslUpdateData.ForeColor = Color.Blue;

                this.dtpHistoryDate.Enabled = true;
                this.txtMeetingOrReportTIPs.ReadOnly = true;
                this.txtTIPsSaveTime.ReadOnly = true;
                this.btnModifyHistory.Enabled = true;
                this.btnSaveHistoryData.Enabled = false;

                


            }
            else
            {
                tsslUpdateData.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ",update " + p.myDepartment + " " + dtpHistoryDate.Value.ToString("yyyy-MM-dd") + " detail data fail...";
                tsslUpdateData.ForeColor = Color.Red;
                return;
            }
            
        }

       


    }
}
