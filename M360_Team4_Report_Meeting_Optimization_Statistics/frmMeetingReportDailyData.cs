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
        }
    }
}
