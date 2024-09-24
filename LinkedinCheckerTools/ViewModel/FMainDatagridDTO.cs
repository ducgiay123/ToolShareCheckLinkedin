using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.ViewModel
{
    public class FMainDatagridDTO
    {
        private FMainDatagridManager _datagridViewFmainManager;
        public FMainDatagridManager Manager
        {
            get
            {
                return _datagridViewFmainManager;
            }
            set
            {
                _datagridViewFmainManager = value;
            }
        }
        private int _index;
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                Manager.SafeSetValueCell(_index, FMainDatagridViewCellName.Cellstt, _number);
            }
        }
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                Manager.SafeSetValueCell(_index, FMainDatagridViewCellName.Cellemail, _email);
            }
        }
        private string _proxy;
        public string Proxy
        {
            get
            {
                return _proxy;
            }
            set
            {
                _proxy = value;
                Manager.SafeSetValueCell(_index, FMainDatagridViewCellName.CellProxy, _proxy);
            }
        }
        private string _cllinked;
        public string LinkedObj
        {
            get
            {
                return _cllinked;
            }
            set
            {
                _cllinked = value;
                Manager.SafeSetValueCell(_index, FMainDatagridViewCellName.CellLinkedObj, _cllinked);
            }
        }
        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                Manager.SafeSetValueCell(_index, FMainDatagridViewCellName.CellStatus, _status);
            }
        }
    }
}
