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


        decimal _meetingtime = 0;
        decimal _reporttime = 0;

        private void frmMeetingReportDailyData_Load(object sender, EventArgs e)
        {
            this.Text = p.titleModifyMeetingReportData;
            InitUI(p.titleModifyMeetingReportData);
            loadBaseline();

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

                    txtTotaWorkingTime.Text = re["totalworkingtime"].ToString();
                    _meetingtime = Convert.ToDecimal(re["meetingworkingtime"]);
                    _reporttime = Convert.ToDecimal(re["reportworkingtime"]);
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

        }

       


    }
}
