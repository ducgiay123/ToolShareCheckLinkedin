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
    }
}
