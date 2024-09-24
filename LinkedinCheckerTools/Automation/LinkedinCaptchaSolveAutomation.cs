using LinkedinCheckerTools.API;
using LinkedinCheckerTools.Enums;
using LinkedinCheckerTools.Models;
using LinkedinCheckerTools.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Automation
{
    public class LinkedinCaptchaSolveAutomation
    {
        public AutomationResult.LinkedinCaptchaSolveResult Solve(AutomationOptions.LinkedinCaptchaSolveOptions options)
        {
            AutomationResult.LinkedinCaptchaSolveResult result = new AutomationResult.LinkedinCaptchaSolveResult();
            int trycount = 0;
            while (trycount < options.MaxTryTimes)
            {
                options.Chrome.SwitchTo().DefaultContent();
                Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();
                try
                {
                    options.Chrome.SwitchTo().Frame("captcha-internal");
                    Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();
                }
                catch
                {
                    result.StatusCode = Enums.SolveCaptchaStatusCode.Success;
                    break;
                }
                string base64 = String.Empty;
                //LinkedinCaptchaType captchaType = LinkedinCaptchaType.Rotated;
                string text_captcha = String.Empty;
                try
                {
                    options.Chrome.SwitchTo().Frame("arkoseframe");
                    Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();
                    options.Chrome.SwitchTo().Frame(options.Chrome.FindElement(By.XPath("//*[contains(@title,'Verification challenge')]")));
                    Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();
                    options.Chrome.SwitchTo().Frame("fc-iframe-wrap");
                    Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();
                    options.Chrome.SwitchTo().Frame("CaptchaFrame");
                    Task.Delay(TimeSpan.FromMilliseconds(200)).Wait();
                    try
                    {
                        options.Chrome.FindElement(By.Id("home_children_button")).Click();
                        Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    }
                    catch { }
                    text_captcha = options.Chrome.FindElement(By.Id("game_children_text")).Text;
                    if (string.IsNullOrEmpty(text_captcha))
                    {
                        result.StatusCode = SolveCaptchaStatusCode.Error;
                        return result;
                    }
                    #region Un Used
                    //if (text_captcha.Contains("is the correct way up"))
                    //{
                    //    captchaType = LinkedinCaptchaType.Rotated;
                    //}
                    //else if (text_captcha.Contains("shows two identical objects"))
                    //{
                    //    captchaType = LinkedinCaptchaType.Identical_two;
                    //}
                    //else if(text_captcha.Contains("the same icon facing up"))
                    //{
                    //    // the same icon facing up
                    //}
                    //else
                    //{
                    //    captchaType = LinkedinCaptchaType.Identical_three;
                    //}
                    #endregion
                    base64 = options.Chrome.FindElement(By.Id("game_challengeItem_image")).GetAttribute("src");
                    if (string.IsNullOrEmpty(base64) || !base64.Contains("data:image"))
                    {
                        Task.Delay(TimeSpan.FromMilliseconds(700)).Wait();
                        trycount++;
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    trycount++;
                    continue;
                }
                CNT_ServerCaptchaRecognizeAPI CNT_serverCaptchaRecognizeAPI = new CNT_ServerCaptchaRecognizeAPI();
                CNT_CaptchaServerModel.GetTaskResult getTaskResult = new CNT_CaptchaServerModel.GetTaskResult();
                int countt = 0;
                while (countt < 60)
                {
                    #region UnUsed
                    //string captcha_typestr = captchaType == LinkedinCaptchaType.Rotated ? "rotated"
                    //    : captchaType == LinkedinCaptchaType.Identical_two ? "ident"
                    //    : "ident";
                    //if (captchaType == LinkedinCaptchaType.Identical_two || captchaType == LinkedinCaptchaType.Identical_three)
                    //{

                    //}
                    #endregion
                    if (text_captcha.EndsWith("."))
                    {
                        text_captcha = text_captcha.Replace(".", "");
                    }
                    string captcha_typestr = CNT_CaptchaQuestionUtils.GetQuestioIdnByContent(text_captcha);
                    if(captcha_typestr == null)
                    {
                        result.StatusCode = SolveCaptchaStatusCode.Error;
                        return result;
                    }
                    CNT_CaptchaServerModel.CreateTaskResult createTaskResult = CNT_serverCaptchaRecognizeAPI.CreateTask(base64, captcha_typestr, 5);
                    if (!createTaskResult.IsSuccess)
                    {
                        Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                        countt++;
                        continue;
                    }
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                    getTaskResult = CNT_serverCaptchaRecognizeAPI.GetTaskResult(createTaskResult.TaskId, 3);
                    if (!getTaskResult.IsSuccess)
                    {
                        Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                        countt++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                if (!getTaskResult.IsSuccess)
                {
                    options.Chrome.TryGotoUrl(options.Chrome.Url);
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    trycount++;
                    continue;
                }
                options.Chrome.ClickWait(By.XPath($"//*[contains(@aria-label,'Image {getTaskResult.Solution}.')]"));
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                if (options.Chrome.TrySeeIsDisplay(By.Id("wrong_children_button")))
                {
                    options.Chrome.TryGotoUrl(options.Chrome.Url);
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    trycount++;
                }
            }
            if (trycount >= options.MaxTryTimes)
            {
                result.StatusCode = Enums.SolveCaptchaStatusCode.TimeTryExceeded;
            }
            return result;
        }
    }
}
