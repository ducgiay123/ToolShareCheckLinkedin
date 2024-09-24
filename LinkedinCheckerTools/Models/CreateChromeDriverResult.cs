using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Models
{
    public class CreateChromeDriverResult
    {
        public ChromeDriver Chrome { get; set; }
        public int ProcessId { get; set; } = -1;
    }
}
