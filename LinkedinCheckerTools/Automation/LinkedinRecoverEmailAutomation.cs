using LinkedinCheckerTools.Models;
using LinkedinCheckerTools.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Automation
{
    public class LinkedinRecoverEmailAutomation
    {
        public AutomationResult.LinkedinRecoverEmailResult Recover(AutomationOptions.LinkedinRecoverEmailOptions options)
        {
            LinkedinCaptchaSolveAutomation linkedinCaptchaSolveAutomation = new LinkedinCaptchaSolveAutomation();
            AutomationResult.LinkedinCaptchaSolveResult linkedinCaptchaSolveResult = new AutomationResult.LinkedinCaptchaSolveResult();
            AutomationResult.LinkedinRecoverEmailResult result = new AutomationResult.LinkedinRecoverEmailResult();
            try
            {
                options.Datagrid.Status = "going to recover password page...";
                options.Chrome.TryGotoUrl("https://www.linkedin.com/checkpoint/rp/request-password-reset");
                if (options.Chrome.Url.Contains("checkpoint/challenge"))
                {
                    options.Datagrid.Status = "solving captcha...";
                    AutomationOptions.LinkedinCaptchaSolveOptions linkedinCaptchaSolveOptions = new AutomationOptions.LinkedinCaptchaSolveOptions
                    {
                        Chrome = options.Chrome,
                        MaxTryTimes = 12
                    };
                    linkedinCaptchaSolveResult = linkedinCaptchaSolveAutomation.Solve(linkedinCaptchaSolveOptions);
                    if (linkedinCaptchaSolveResult.StatusCode != Enums.SolveCaptchaStatusCode.Success)
                    {
                        Recover(options);
                    }
                }
                options.Datagrid.Status = "fill info...";
                options.Chrome.FindElement(By.Id("username")).SendKeys(options.Email);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                options.Chrome.ClickWait(By.Id("reset-password-submit-button"));
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                if (options.Chrome.Url.Contains("checkpoint/challenge") && !options.Chrome.TrySeeIsDisplay(By.Name("pin"))) // captcha
                {
                    options.Datagrid.Status = "solving captcha...";
                    AutomationOptions.LinkedinCaptchaSolveOptions linkedinCaptchaSolveOptions = new AutomationOptions.LinkedinCaptchaSolveOptions
                    {
                        Chrome = options.Chrome,
                        MaxTryTimes = 12
                    };
                    linkedinCaptchaSolveResult = linkedinCaptchaSolveAutomation.Solve(linkedinCaptchaSolveOptions);
                    if (linkedinCaptchaSolveResult.StatusCode != Enums.SolveCaptchaStatusCode.Success)
                    {
                        Recover(options);
                    }
                }
                string morerecovermethodpath = Regex.Match(options.Chrome.PageSource, "href=\"\\/checkpoint\\/rp\\/id-verify-create(.*?)\"").Groups[1].Value;
                string MoreMethodRcv = string.Empty;
                if (!string.IsNullOrEmpty(morerecovermethodpath))
                {
                    MoreMethodRcv = "https://www.linkedin.com/checkpoint/rp/id-verify-create" + morerecovermethodpath.Replace("amp;", "");
                }
                if (!string.IsNullOrEmpty(MoreMethodRcv))
                {
                    options.Datagrid.Status = "checking all adresses...";
                    options.Chrome.TryGotoUrl(MoreMethodRcv);
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    if (!options.Chrome.PageSource.Contains("name=\"maskedValue\""))
                    {
                        result.RecoverEmailStatusCode = Enums.LinkedinRecoverEmailStatusCode.Failed;
                        return result;
                    }
                    result.LinkedObjs.Add(options.Email);
                    var matches_maskeditem = Regex.Matches(options.Chrome.PageSource, "<input name=\"maskedValue\" value=\"(.*?)\"");
                    foreach (Match match in matches_maskeditem)
                    {
                        if (!string.IsNullOrEmpty(match.Groups[1].Value))
                        {
                            result.LinkedObjs.Add(match.Groups[1].Value);
                        }
                    }
                    result.RecoverEmailStatusCode = Enums.LinkedinRecoverEmailStatusCode.Success;
                }
                else
                {
                    result.RecoverEmailStatusCode = Enums.LinkedinRecoverEmailStatusCode.Failed;
                }
            }
            catch(Exception ex)
            {
                result.Exception = ex;
                result.RecoverEmailStatusCode = Enums.LinkedinRecoverEmailStatusCode.Error;
            }
            return result;
        }
    }
}
