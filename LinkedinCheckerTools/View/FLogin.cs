using LinkedinCheckerTools.API;
using LinkedinCheckerTools.Config;
using LinkedinCheckerTools.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkedinCheckerTools.View
{
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            if (!AuthenticationAPI.GetAccess())
            {
                Application.Exit();
            }
            txtuser.Text = MainFormUISettings.MainFormUISettingsProvider.Read("username");
            txtpass.Text = MainFormUISettings.MainFormUISettingsProvider.Read("password");
            txtsystemid.Text = AuthenticationAPI.GetKey();
        }

        private void btncopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(txtsystemid.Text);
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MainFormUISettings.MainFormUISettingsProvider.Write("username", txtuser.Text);
            MainFormUISettings.MainFormUISettingsProvider.Write("password", txtpass.Text);
            string softcode = "linkedin_aio_tools";
            AccountLoginResult accountLoginResult = AuthenticationAPI.Login(txtuser.Text, txtpass.Text, softcode, txtsystemid.Text);
            if (!accountLoginResult.IsSuccess)
            {
                MessageBox.Show(this,$"Login failed ! Error :\r\n{accountLoginResult.Message}", "login_failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FMain fMain = new FMain(accountLoginResult);
            fMain.Show();
            this.Hide();
        }
    }
}
