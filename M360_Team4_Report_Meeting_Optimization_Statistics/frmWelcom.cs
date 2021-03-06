﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Edward;
using System.IO;

namespace M360_Team4_Report_Meeting_Optimization_Statistics
{
    public partial class frmWelcom : Form
    {
        public frmWelcom()
        {
            InitializeComponent();
        }

        private void frmWelcom_Load(object sender, EventArgs e)
        {
            this.Text = "First run or not set your department...";

            p.checkFolder();
            if (!File.Exists(p.iniFilePath))
                p.createIniFile(p.iniFilePath);
            
        }

        private void comboDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.myDepartment = this.comboDep.SelectedItem.ToString();
            IniFile.IniWriteValue(p.IniSection.SysConfig.ToString(), "MyDepartment", DES.DesEncrypt ( p.myDepartment,"Edward86"), p.iniFilePath);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.comboDep.SelectedIndex == -1)
            {
                MessageBox.Show("You don't select department,pls retry...");
                this.comboDep.Focus();
                return;

            }
            else
            {
                Form f = new frmMain();
                f.Show();
                this.Hide();

            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "Administrator" && txtPwd.Text == "administrator")
            {

                Form f = new frmMain();
                p.isAdmin = true;
                f.Show();
                this.Hide();
              
            }
            else
            {
                MessageBox.Show("Invalid ID or Passowrd...", "Admin Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtID.SelectAll();
                txtID.Focus();
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtID.Text == "Administrator" && txtPwd.Text == "administrator")
                {

                    Form f = new frmMain();
                    p.isAdmin = true;
                    f.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid ID or Passowrd...", "Admin Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtID.SelectAll();
                    txtID.Focus();
                }
            }
        }
    }
}
