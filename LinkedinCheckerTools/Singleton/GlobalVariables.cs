using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Singleton
{
    public class GlobalVariables
    {
        public static TimeSpan DefaultDriverWaitTime = TimeSpan.FromSeconds(30);
        public static string Lk = "78|111|107|65|125|122|71|68|35|64|70|73|36|114|71|65";
        public static Dictionary<string,ChromeDriver> Drivers = new Dictionary<string, ChromeDriver>();
    }
}
