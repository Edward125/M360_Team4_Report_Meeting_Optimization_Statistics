namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    partial class frmWelcom
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboDep = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboDep
            // 
            this.comboDep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDep.FormattingEnabled = true;
            this.comboDep.Items.AddRange(new object[] {
            "1KC900",
            "1KCD00",
            "KD0B00",
            "KD1100",
            "KD1200",
            "KD1300",
            "KD1500",
            "KD1600",
            "KD1700",
            "KD1C00",
            "KD1E00",
            "KD1M00",
            "KD1P00",
            "KD1Q00",
            "KD1S00",
            "KD1T00",
            "KD1W00"});
            this.comboDep.Location = new System.Drawing.Point(91, 119);
            this.comboDep.Name = "comboDep";
            this.comboDep.Size = new System.Drawing.Size(121, 22);
            this.comboDep.Sorted = true;
            this.comboDep.TabIndex = 0;
            // 
            // frmWelcom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 362);
            this.Controls.Add(this.comboDep);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmWelcom";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmWelcom_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboDep;
    }
}

