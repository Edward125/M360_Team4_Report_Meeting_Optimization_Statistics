namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    partial class frmMeetingReportDailyData
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.grbHistory = new System.Windows.Forms.GroupBox();
            this.btnSaveHistoryData = new System.Windows.Forms.Button();
            this.btnModifyHistory = new System.Windows.Forms.Button();
            this.txtTIPsSaveTime = new System.Windows.Forms.TextBox();
            this.lblMeetingOrReportTIPsSaveTime = new System.Windows.Forms.Label();
            this.txtMeetingOrReportTIPs = new System.Windows.Forms.TextBox();
            this.lblMeetingOrReportTIPs = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.grbBaseline = new System.Windows.Forms.GroupBox();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.txtMeetingOrReportTime = new System.Windows.Forms.TextBox();
            this.lblMeetingorReportWorkingTime = new System.Windows.Forms.Label();
            this.txtTotaWorkingTime = new System.Windows.Forms.TextBox();
            this.lblTotalWorkingTime = new System.Windows.Forms.Label();
            this.grbHistory.SuspendLayout();
            this.grbBaseline.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 163);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(728, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // grbHistory
            // 
            this.grbHistory.Controls.Add(this.btnSaveHistoryData);
            this.grbHistory.Controls.Add(this.btnModifyHistory);
            this.grbHistory.Controls.Add(this.txtTIPsSaveTime);
            this.grbHistory.Controls.Add(this.lblMeetingOrReportTIPsSaveTime);
            this.grbHistory.Controls.Add(this.txtMeetingOrReportTIPs);
            this.grbHistory.Controls.Add(this.lblMeetingOrReportTIPs);
            this.grbHistory.Controls.Add(this.dateTimePicker1);
            this.grbHistory.Location = new System.Drawing.Point(21, 92);
            this.grbHistory.Name = "grbHistory";
            this.grbHistory.Size = new System.Drawing.Size(687, 63);
            this.grbHistory.TabIndex = 1;
            this.grbHistory.TabStop = false;
            this.grbHistory.Text = "History Data";
            // 
            // btnSaveHistoryData
            // 
            this.btnSaveHistoryData.Location = new System.Drawing.Point(592, 20);
            this.btnSaveHistoryData.Name = "btnSaveHistoryData";
            this.btnSaveHistoryData.Size = new System.Drawing.Size(82, 33);
            this.btnSaveHistoryData.TabIndex = 7;
            this.btnSaveHistoryData.Text = "Save";
            this.btnSaveHistoryData.UseVisualStyleBackColor = true;
            // 
            // btnModifyHistory
            // 
            this.btnModifyHistory.Location = new System.Drawing.Point(487, 20);
            this.btnModifyHistory.Name = "btnModifyHistory";
            this.btnModifyHistory.Size = new System.Drawing.Size(87, 33);
            this.btnModifyHistory.TabIndex = 6;
            this.btnModifyHistory.Text = "Modify";
            this.btnModifyHistory.UseVisualStyleBackColor = true;
            // 
            // txtTIPsSaveTime
            // 
            this.txtTIPsSaveTime.Location = new System.Drawing.Point(401, 26);
            this.txtTIPsSaveTime.Name = "txtTIPsSaveTime";
            this.txtTIPsSaveTime.Size = new System.Drawing.Size(80, 22);
            this.txtTIPsSaveTime.TabIndex = 6;
            this.txtTIPsSaveTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTIPsSaveTime.TextChanged += new System.EventHandler(this.txtTIPsSaveTime_TextChanged);
            // 
            // lblMeetingOrReportTIPsSaveTime
            // 
            this.lblMeetingOrReportTIPsSaveTime.AutoSize = true;
            this.lblMeetingOrReportTIPsSaveTime.Location = new System.Drawing.Point(304, 29);
            this.lblMeetingOrReportTIPsSaveTime.Name = "lblMeetingOrReportTIPsSaveTime";
            this.lblMeetingOrReportTIPsSaveTime.Size = new System.Drawing.Size(90, 14);
            this.lblMeetingOrReportTIPsSaveTime.TabIndex = 6;
            this.lblMeetingOrReportTIPsSaveTime.Text = "TIPs Save Time:";
            // 
            // txtMeetingOrReportTIPs
            // 
            this.txtMeetingOrReportTIPs.Location = new System.Drawing.Point(226, 26);
            this.txtMeetingOrReportTIPs.Name = "txtMeetingOrReportTIPs";
            this.txtMeetingOrReportTIPs.Size = new System.Drawing.Size(59, 22);
            this.txtMeetingOrReportTIPs.TabIndex = 6;
            this.txtMeetingOrReportTIPs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMeetingOrReportTIPs.TextChanged += new System.EventHandler(this.txtMeetingOrReportTIPs_TextChanged);
            // 
            // lblMeetingOrReportTIPs
            // 
            this.lblMeetingOrReportTIPs.AutoSize = true;
            this.lblMeetingOrReportTIPs.Location = new System.Drawing.Point(147, 29);
            this.lblMeetingOrReportTIPs.Name = "lblMeetingOrReportTIPs";
            this.lblMeetingOrReportTIPs.Size = new System.Drawing.Size(73, 14);
            this.lblMeetingOrReportTIPs.TabIndex = 6;
            this.lblMeetingOrReportTIPs.Text = "MeetingTIPs";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimePicker1.Location = new System.Drawing.Point(16, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(123, 22);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // grbBaseline
            // 
            this.grbBaseline.Controls.Add(this.btnSaveData);
            this.grbBaseline.Controls.Add(this.btnModify);
            this.grbBaseline.Controls.Add(this.txtMeetingOrReportTime);
            this.grbBaseline.Controls.Add(this.lblMeetingorReportWorkingTime);
            this.grbBaseline.Controls.Add(this.txtTotaWorkingTime);
            this.grbBaseline.Controls.Add(this.lblTotalWorkingTime);
            this.grbBaseline.Location = new System.Drawing.Point(21, 12);
            this.grbBaseline.Name = "grbBaseline";
            this.grbBaseline.Size = new System.Drawing.Size(687, 60);
            this.grbBaseline.TabIndex = 0;
            this.grbBaseline.TabStop = false;
            this.grbBaseline.Text = "BaseLine";
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(571, 20);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(103, 33);
            this.btnSaveData.TabIndex = 5;
            this.btnSaveData.Text = "Save";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(462, 20);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(103, 33);
            this.btnModify.TabIndex = 4;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // txtMeetingOrReportTime
            // 
            this.txtMeetingOrReportTime.Location = new System.Drawing.Point(342, 26);
            this.txtMeetingOrReportTime.Name = "txtMeetingOrReportTime";
            this.txtMeetingOrReportTime.Size = new System.Drawing.Size(100, 22);
            this.txtMeetingOrReportTime.TabIndex = 3;
            this.txtMeetingOrReportTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMeetingOrReportTime.TextChanged += new System.EventHandler(this.txtMeetingOrReportTime_TextChanged);
            // 
            // lblMeetingorReportWorkingTime
            // 
            this.lblMeetingorReportWorkingTime.AutoSize = true;
            this.lblMeetingorReportWorkingTime.Location = new System.Drawing.Point(255, 30);
            this.lblMeetingorReportWorkingTime.Name = "lblMeetingorReportWorkingTime";
            this.lblMeetingorReportWorkingTime.Size = new System.Drawing.Size(81, 14);
            this.lblMeetingorReportWorkingTime.TabIndex = 2;
            this.lblMeetingorReportWorkingTime.Text = "MeetingTime:";
            // 
            // txtTotaWorkingTime
            // 
            this.txtTotaWorkingTime.Location = new System.Drawing.Point(128, 26);
            this.txtTotaWorkingTime.Name = "txtTotaWorkingTime";
            this.txtTotaWorkingTime.Size = new System.Drawing.Size(100, 22);
            this.txtTotaWorkingTime.TabIndex = 1;
            this.txtTotaWorkingTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotaWorkingTime.TextChanged += new System.EventHandler(this.txtTotaWorkingTime_TextChanged);
            // 
            // lblTotalWorkingTime
            // 
            this.lblTotalWorkingTime.AutoSize = true;
            this.lblTotalWorkingTime.Location = new System.Drawing.Point(14, 30);
            this.lblTotalWorkingTime.Name = "lblTotalWorkingTime";
            this.lblTotalWorkingTime.Size = new System.Drawing.Size(108, 14);
            this.lblTotalWorkingTime.TabIndex = 0;
            this.lblTotalWorkingTime.Text = "TotalWorkingTime:";
            // 
            // frmMeetingReportDailyData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 185);
            this.Controls.Add(this.grbBaseline);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grbHistory);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMeetingReportDailyData";
            this.Text = "frmMeetingReportDailyData";
            this.Load += new System.EventHandler(this.frmMeetingReportDailyData_Load);
            this.grbHistory.ResumeLayout(false);
            this.grbHistory.PerformLayout();
            this.grbBaseline.ResumeLayout(false);
            this.grbBaseline.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox grbHistory;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox grbBaseline;
        private System.Windows.Forms.Label lblTotalWorkingTime;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.TextBox txtMeetingOrReportTime;
        private System.Windows.Forms.Label lblMeetingorReportWorkingTime;
        private System.Windows.Forms.TextBox txtTotaWorkingTime;
        private System.Windows.Forms.Button btnSaveHistoryData;
        private System.Windows.Forms.Button btnModifyHistory;
        private System.Windows.Forms.TextBox txtTIPsSaveTime;
        private System.Windows.Forms.Label lblMeetingOrReportTIPsSaveTime;
        private System.Windows.Forms.TextBox txtMeetingOrReportTIPs;
        private System.Windows.Forms.Label lblMeetingOrReportTIPs;
    }
}