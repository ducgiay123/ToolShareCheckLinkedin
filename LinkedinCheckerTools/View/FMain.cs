using LinkedinCheckerTools.Config;
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
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            LoadUISettings();
        }
        private void LoadUISettings()
        {
            numthreads.Value = MainFormUISettings.Threads;
            chkuseproxy.Checked = MainFormUISettings.UseProxy;
            cbbprxtype.SelectedIndex = MainFormUISettings.ProxyType;
            switch (MainFormUISettings.TaskType)
            {
                case 1:
                    rdchecklinked.Checked = true;
                    break;
                case 2:
                    rdcheckdie.Checked = true;
                    break;
                case 3:
                    rdchecklkobj.Checked = true;
                    break;
            }
        }
        private void numthreads_ValueChanged(object sender, EventArgs e)
        {
            MainFormUISettings.Threads = (int)numthreads.Value;
        }

        private void chkuseproxy_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.UseProxy = chkuseproxy.Checked;
        }

        private void cbbprxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainFormUISettings.ProxyType = cbbprxtype.SelectedIndex;
        }

        private void rdchecklinked_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.TaskType = 1;
        }

        private void rdcheckdie_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.TaskType = 2;
        }

        private void rdchecklkobj_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.TaskType = 3;
        }
    }
}
