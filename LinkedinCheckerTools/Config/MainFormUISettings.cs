using LinkedinCheckerTools.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Config
{
    public class MainFormUISettings
    {
        public static IniFile MainFormUISettingsProvider = new IniFile(PathSingleton.MainFormUISettingPath);
        public static int Threads
        {
            get
            {
                string val = MainFormUISettingsProvider.Read("threads");
                if (string.IsNullOrEmpty(val))
                {
                    return 0;
                }
                return int.Parse(val);
            }
            set
            {
                MainFormUISettingsProvider.Write("threads", value.ToString());
            }
        }
        public static int ProxyType
        {
            get
            {
                string val = MainFormUISettingsProvider.Read("proxytype");
                if (string.IsNullOrEmpty(val))
                {
                    return 0;
                }
                return int.Parse(val);
            }
            set
            {
                MainFormUISettingsProvider.Write("proxytype", value.ToString());
            }
        }
        /// <summary>
        /// 1 : Checklinked
        /// <br></br>
        /// 2 : Check Die
        /// <br></br>
        /// 3 : Check linked Obj
        /// </summary>
        public static int TaskType
        {
            get
            {
                string val = MainFormUISettingsProvider.Read("tasktype");
                if (string.IsNullOrEmpty(val))
                {
                    return 1;
                }
                return int.Parse(val);
            }
            set
            {
                MainFormUISettingsProvider.Write("tasktype", value.ToString());
            }
        }
        public static bool UseProxy
        {
            get
            {
                string val = MainFormUISettingsProvider.Read("useproxy");
                if (string.IsNullOrEmpty(val))
                {
                    return false;
                }
                return bool.Parse(val);
            }
            set
            {
                MainFormUISettingsProvider.Write("useproxy", value.ToString());
            }
        }
    }
}
