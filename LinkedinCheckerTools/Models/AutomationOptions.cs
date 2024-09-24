using LinkedinCheckerTools.ViewModel;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Models
{
    public class AutomationOptions
    {
        public class LinkedinRecoverEmailOptions : BaseAutomationOptions
        {
            public string Email { get; set; }
        }
        public class LinkedinCaptchaSolveOptions : BaseAutomationOptions
        {
            public int MaxTryTimes { get; set; }
        }
        public class BaseAutomationOptions
        {
            public ChromeDriver Chrome { get; set; }
            public FMainDatagridDTO Datagrid { get; set; }
        }
    }
}
