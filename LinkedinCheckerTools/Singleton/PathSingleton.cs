using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Singleton
{
    public class PathSingleton
    {
        public static string ConfigPath
        {
            get
            {
                return Environment.CurrentDirectory + "\\config";
            }
        }
        public static string MainFormUISettingPath = ConfigPath + "\\MainFormUISettings.ini";
    }
}
