using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using LinkedinCheckerTools.Singleton;

namespace LinkedinCheckerTools.Utils
{
    public static class ChromeExecuteUtils
    {
        public static bool TryGotoUrl(this IWebDriver chrome, string url)
        {
            try
            {
                chrome.Navigate().GoToUrl(url);
                chrome.WaitUtil(GlobalVariables.DefaultDriverWaitTime);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool WaitUtil(this IWebDriver chrome, TimeSpan waitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(chrome, waitTime);
                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)chrome).ExecuteScript("return document.readyState").Equals("complete");
                });
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static void AddCookies(this ICookieJar cookie, CookieContainer container, string domain)
        {
            Uri uri = new Uri(domain);
            var cookies = container.GetCookies(uri);
            foreach (System.Net.Cookie ck in cookies)
            {
                // concat your string or do what you want
                OpenQA.Selenium.Cookie ckadd = new OpenQA.Selenium.Cookie(ck.Name, ck.Value, ck.Domain, ck.Path, DateTime.Now.AddDays(10));
                cookie.AddCookie(ckadd);
            }
        }
        public static string ToCookieStr(this ICookieJar cookieJar)
        {
            string cookieStr = "";
            OpenQA.Selenium.Cookie[] arrayCookies = cookieJar.AllCookies.ToArray<OpenQA.Selenium.Cookie>();
            foreach (OpenQA.Selenium.Cookie cookie in arrayCookies)
            {
                cookieStr = string.Concat(new string[]
                    {
                                cookieStr,
                                cookie.Name,
                                "=",
                                cookie.Value,
                                ";"
                    });
            }
            return cookieStr;
        }
        public static bool TrySeeIsDisplay(this IWebDriver chrome, By by)
        {
            try
            {
                if (chrome.FindElement(by).Displayed)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool WaitElementToDisplay(this IWebDriver chrome, By by, TimeSpan time)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(chrome, time);
                return wait.Until((x) =>
                {
                    return chrome.FindElement(by).Displayed;
                });
            }
            catch
            {
                return false;
            }
        }
        public static void ClickWait(this IWebDriver chrome, By by)
        {
            try
            {
                chrome.FindElement(by).Click();
                chrome.WaitUtil(GlobalVariables.DefaultDriverWaitTime);
            }
            catch { }
        }
        public static void SlowSendkeys(this IWebElement element, string text)
        {
            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    element.SendKeys(text[i].ToString());
                    Task.Delay(100).Wait();
                }
            }
            catch { }
        }
    }
}
