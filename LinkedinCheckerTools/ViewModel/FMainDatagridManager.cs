using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkedinCheckerTools.ViewModel
{
    public class FMainDatagridManager
    {
        public DataGridView Maingridview { get; set; }
        private List<FMainDatagridDTO> _mails;
        public List<FMainDatagridDTO> Mails
        {
            get
            {
                return _mails;
            }
            set
            {
                _mails = value;
            }
        }
        public FMainDatagridManager(DataGridView maingridview)
        {
            Maingridview = maingridview;
            _mails = new List<FMainDatagridDTO>();
        }
        public FMainDatagridDTO AddNewRow()
        {
            int row = 0;
            this.Maingridview.Invoke(new Action(() =>
            {
                row = this.Maingridview.Rows.Add();
            }));
            int number = row + 1;
            FMainDatagridDTO datagridViewFbAccountDTO = new FMainDatagridDTO();
            datagridViewFbAccountDTO.Manager = this;
            datagridViewFbAccountDTO.Index = row;
            datagridViewFbAccountDTO.Number = number;
            _mails.Add(datagridViewFbAccountDTO);
            return datagridViewFbAccountDTO;
        }
        public void SafeSetValueCell(int row, string cellname, object value)
        {
            try
            {
                this.Maingridview.Invoke(new Action(() =>
                {
                    Maingridview.Rows[row].Cells[cellname].Value = value;
                }));
                //Maingridview.Rows[row].Cells[cellname].Style.ForeColor = GetBackgroundColorByStatus(value.ToString());
                //Maingridview.Rows[row].DefaultCellStyle.ce = GetForceColorByStatus(value.ToString());
            }
            catch
            {

            }
        }
        public Color GetForceColorByStatus(string status)
        {
            if (status.Contains("Success"))
            {
                return Color.Green;
            }
            if (status.Contains("Blocked") || status.Contains("ErrorCreateMail") || status.Contains("EmailCantReg") || status.Contains("CodeNotRecievied") || status.Contains("SignupCodeFail"))
            {
                return Color.Red;
            }
            if (status.Contains("Error") || status.Contains("Unknown"))
            {
                return Color.OrangeRed;
            }
            return Color.Black;
        }
    }
}
