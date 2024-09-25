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
        public static string DefaultChromeniumExecutablePath
        {
            get
            {
                return Environment.CurrentDirectory + "\\chromenium\\chrome-win\\chrome.exe";
            }
        }
        public static string DefaultDriverChromeniumExecutablePath
        {
            get
            {
                return Environment.CurrentDirectory + "\\Bin_chromenium";
            }
        }
        public static string DefaultChromeExecutablePath
        {
            get
            {
                return @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            }
        }
        public static string DefaultDriverExecutablePath
        {
            get
            {
                return Environment.CurrentDirectory + "\\Bin";
            }
        }
        public static string ExtensionsFolder = Environment.CurrentDirectory + "\\Extensions";
        public static string AuthProxyExtensionPath = ExtensionsFolder + "\\LoginProxy\\Proxy-Auto-Auth.crx";
        public static string CaptchaQuestionMapFile = ConfigPath + "\\captcha_question_map.txt";
    }
}
