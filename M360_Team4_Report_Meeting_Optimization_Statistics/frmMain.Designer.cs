namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMeetingReport = new System.Windows.Forms.TabPage();
            this.lstMeetingReportStatus = new System.Windows.Forms.ListView();
            this.tabMeeting = new System.Windows.Forms.TabPage();
            this.lstMeetingDetail = new System.Windows.Forms.ListView();
            this.lstMeeting = new System.Windows.Forms.ListView();
            this.tabReport = new System.Windows.Forms.TabPage();
            this.lstReport = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslAppName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDepStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabMeetingReport.SuspendLayout();
            this.tabMeeting.SuspendLayout();
            this.tabReport.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMeetingReport);
            this.tabControl1.Controls.Add(this.tabMeeting);
            this.tabControl1.Controls.Add(this.tabReport);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1298, 482);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabMeetingReport
            // 
            this.tabMeetingReport.Controls.Add(this.lstMeetingReportStatus);
            this.tabMeetingReport.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMeetingReport.Location = new System.Drawing.Point(4, 22);
            this.tabMeetingReport.Name = "tabMeetingReport";
            this.tabMeetingReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabMeetingReport.Size = new System.Drawing.Size(1290, 456);
            this.tabMeetingReport.TabIndex = 0;
            this.tabMeetingReport.Text = "Meeting & Report Status";
            this.tabMeetingReport.UseVisualStyleBackColor = true;
            // 
            // lstMeetingReportStatus
            // 
            this.lstMeetingReportStatus.Location = new System.Drawing.Point(6, 39);
            this.lstMeetingReportStatus.Name = "lstMeetingReportStatus";
            this.lstMeetingReportStatus.Size = new System.Drawing.Size(1275, 411);
            this.lstMeetingReportStatus.TabIndex = 0;
            this.lstMeetingReportStatus.UseCompatibleStateImageBehavior = false;
            this.lstMeetingReportStatus.View = System.Windows.Forms.View.Details;
            // 
            // tabMeeting
            // 
            this.tabMeeting.Controls.Add(this.lstMeetingDetail);
            this.tabMeeting.Controls.Add(this.lstMeeting);
            this.tabMeeting.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMeeting.Location = new System.Drawing.Point(4, 22);
            this.tabMeeting.Name = "tabMeeting";
            this.tabMeeting.Size = new System.Drawing.Size(1365, 456);
            this.tabMeeting.TabIndex = 1;
            this.tabMeeting.Text = "Meeting Status";
            this.tabMeeting.UseVisualStyleBackColor = true;
            // 
            // lstMeetingDetail
            // 
            this.lstMeetingDetail.Location = new System.Drawing.Point(818, 38);
            this.lstMeetingDetail.Name = "lstMeetingDetail";
            this.lstMeetingDetail.Size = new System.Drawing.Size(544, 411);
            this.lstMeetingDetail.TabIndex = 2;
            this.lstMeetingDetail.UseCompatibleStateImageBehavior = false;
            this.lstMeetingDetail.View = System.Windows.Forms.View.Details;
            // 
            // lstMeeting
            // 
            this.lstMeeting.Location = new System.Drawing.Point(6, 38);
            this.lstMeeting.Name = "lstMeeting";
            this.lstMeeting.Size = new System.Drawing.Size(806, 411);
            this.lstMeeting.TabIndex = 1;
            this.lstMeeting.UseCompatibleStateImageBehavior = false;
            this.lstMeeting.View = System.Windows.Forms.View.Details;
            this.lstMeeting.SelectedIndexChanged += new System.EventHandler(this.lstMeeting_SelectedIndexChanged);
            // 
            // tabReport
            // 
            this.tabReport.Controls.Add(this.lstReport);
            this.tabReport.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabReport.Location = new System.Drawing.Point(4, 22);
            this.tabReport.Name = "tabReport";
            this.tabReport.Size = new System.Drawing.Size(1365, 456);
            this.tabReport.TabIndex = 2;
            this.tabReport.Text = "Report Status";
            this.tabReport.UseVisualStyleBackColor = true;
            // 
            // lstReport
            // 
            this.lstReport.Location = new System.Drawing.Point(3, 32);
            this.lstReport.Name = "lstReport";
            this.lstReport.Size = new System.Drawing.Size(1359, 421);
            this.lstReport.TabIndex = 0;
            this.lstReport.UseCompatibleStateImageBehavior = false;
            this.lstReport.View = System.Windows.Forms.View.Details;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslAppName,
            this.tsslStatus,
            this.tsslDepStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 493);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1322, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslAppName
            // 
            this.tsslAppName.Name = "tsslAppName";
            this.tsslAppName.Size = new System.Drawing.Size(66, 17);
            this.tsslAppName.Text = "appName";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(43, 17);
            this.tsslStatus.Text = "Status";
            // 
            // tsslDepStatus
            // 
            this.tsslDepStatus.Name = "tsslDepStatus";
            this.tsslDepStatus.Size = new System.Drawing.Size(86, 17);
            this.tsslDepStatus.Text = "tsslDepStatus";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 515);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabMeetingReport.ResumeLayout(false);
            this.tabMeeting.ResumeLayout(false);
            this.tabReport.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMeetingReport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslAppName;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ListView lstMeetingReportStatus;
        private System.Windows.Forms.TabPage tabMeeting;
        private System.Windows.Forms.TabPage tabReport;
        private System.Windows.Forms.ListView lstMeeting;
        private System.Windows.Forms.ListView lstReport;
        private System.Windows.Forms.ListView lstMeetingDetail;
        private System.Windows.Forms.ToolStripStatusLabel tsslDepStatus;
    }
}