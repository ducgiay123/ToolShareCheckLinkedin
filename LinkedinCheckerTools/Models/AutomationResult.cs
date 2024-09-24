using LinkedinCheckerTools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Models
{
    public class AutomationResult
    {
        public class LinkedinRecoverEmailResult : BaseAutomationResult
        {
            public LinkedinRecoverEmailStatusCode RecoverEmailStatusCode { get; set; }
            public List<string> LinkedObjs { get; set; } = new List<string>();
        }
        public class LinkedinCaptchaSolveResult : BaseAutomationResult
        {
            public SolveCaptchaStatusCode StatusCode { get; set; }
        }
        public class BaseAutomationResult
        {
            public Exception Exception { get; set; }
        }
    }
}
