using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    public partial class frmMeetingReportDailyData : Form
    {
        public frmMeetingReportDailyData()
        {
            InitializeComponent();
        }

        private void frmMeetingReportDailyData_Load(object sender, EventArgs e)
        {
            this.Text = p.titleModifyMeetingReportData;
            InitUI(p.titleModifyMeetingReportData);

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

        private void btnModify_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty (txtTotaWorkingTime.Text .Trim ()) || string.IsNullOrEmpty (txtMeetingOrReportTime .Text.Trim ()))
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
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {

            //
            string sql = string.Empty;
            //
            if (p.titleModifyMeetingReportData.ToLower().Contains("meeting"))
            {
            }
            if (p.titleModifyMeetingReportData.ToLower().Contains("report"))
            {
            }
            
        }

        private void txtTotaWorkingTime_TextChanged(object sender, EventArgs e)
        {
            if (!p.IsDecimal (txtTotaWorkingTime.Text.Trim ()))
            {
                MessageBox.Show("what you input is not Number or Dot,pls check...", "Check Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtTotaWorkingTime.SelectAll();
                this.txtTotaWorkingTime.Focus();
            }
        }

        private void txtMeetingOrReportTime_TextChanged(object sender, EventArgs e)
        {
            if (!p.IsDecimal (txtMeetingOrReportTime .Text.Trim ()))
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


    }
}
