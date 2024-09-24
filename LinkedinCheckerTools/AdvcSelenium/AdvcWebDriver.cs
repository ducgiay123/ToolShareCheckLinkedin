using LinkedinCheckerTools.Singleton;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using LinkedinCheckerTools.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedinCheckerTools.Models;

namespace LinkedinCheckerTools.AdvcSelenium
{
    public class AdvcWebDriver
    {
        public static string ChromeX86AppFolder = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        public static string ChromeAppFolder = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        private object LockErrorHandle = new object();
        public CreateChromeDriverResult CreateNewDriver(AdvcWebdriverConfig driverconfig)
        {
            ChromeDriver driver = null;
            CreateChromeDriverResult result = new CreateChromeDriverResult();
            result.Chrome = driver;
            if (string.IsNullOrEmpty(driverconfig.DriverPath))
            {
                throw new FileNotFoundException("could not find a chrome.exe");
            }
            try
            {
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;
                chromeDriverService.SuppressInitialDiagnosticInformation = true;
                ChromeOptions chromeOptions = new ChromeOptions();
                if (driverconfig.UseChromenium)
                {
                    chromeOptions.BinaryLocation = PathSingleton.DefaultChromeniumExecutablePath;
                }
                else
                {
                    if (File.Exists(ChromeX86AppFolder))
                    {
                        chromeOptions.BinaryLocation = ChromeX86AppFolder;
                    }
                    else
                    {
                        if (File.Exists(ChromeAppFolder))
                        {
                            chromeOptions.BinaryLocation = ChromeAppFolder;
                        }
                        else
                        {
                            chromeOptions.BinaryLocation = driverconfig.DriverPath;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(driverconfig.ProfilePath))
                {
                    chromeOptions.AddArgument("--user-data-dir=" + driverconfig.ProfilePath);
                    chromeOptions.AddArgument("--profile-directory=Default");
                }
                Point location = new Point();
                location = ChromeUtils.GetPointFromIndexPosition(driverconfig.Position);
                chromeOptions.AddArgument($"--window-position={location.X},{location.Y}");
                chromeOptions.AddArgument($"--window-size={ChromeUtils.getHeightChrome},{ChromeUtils.getWidthChrome}");
                chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                chromeOptions.AddArgument("--primary_language=en-US");
                chromeOptions.AddArgument("--languages=en-US,en");
                chromeOptions.AddArgument("--accept_language=en-US,en;q=0.9");
                chromeOptions.AddArgument("--dnt=0");
                chromeOptions.AddArgument("--flag-switches-begin");
                chromeOptions.AddArgument("--flag-switches-end");
                chromeOptions.AddArgument("--enable-experimental-web-platform-features");
                //chromeOptions.AddArgument("--font-masking-mode=" + Utils.AppUtils.RandomNumber(2, 6).ToString());
                chromeOptions.AddArgument("--use-fake-device-for-media-stream");
                chromeOptions.AddArgument("--disable-features=ExtensionsToolbarMenu,ChromeLabs,ReadLater");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                chromeOptions.AddArgument("--disable-notifications");
                chromeOptions.AddArgument("--disable-save-password-bubble");
                chromeOptions.AddUserProfilePreference("useAutomationExtension", true);
                chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                //chromeOptions.AddExtension(Directory.GetCurrentDirectory() + "\\Extensions\\Captcha\\NopeCHA-CAPTCHA-Solver.crx");
                //chromeOptions.AddExtension(Directory.GetCurrentDirectory() + "\\Extensions\\Captcha\\Buster-Captcha-Solver-for-Humans.crx");
                if (driverconfig.Extensions != null)
                {
                    foreach (var extension in driverconfig.Extensions)
                    {
                        if (File.Exists(extension))
                        {
                            chromeOptions.AddExtension(extension);
                        }
                    }
                }
                if (driverconfig.ChromeOptions != null)
                {
                    foreach (var option in driverconfig.ChromeOptions)
                    {
                        if (!chromeOptions.Arguments.Contains(option))
                        {
                            chromeOptions.AddArgument(option);
                        }
                    }
                }
                if (driverconfig.ExtensionPaths != null)
                {
                    foreach (string path in driverconfig.ExtensionPaths)
                    {
                        if (Directory.Exists(path))
                        {
                            chromeOptions.AddArgument("--load-extension=" + path);
                        }
                    }
                }
                chromeOptions.AddExcludedArguments(new List<string>
                {
                    "enable-automation"
                });
                if (driverconfig.IsHeadless)
                {
                    chromeOptions.AddArgument("--blink-settings=imagesEnabled=false");
                    //chromeOptions.AddArgument("--headless");
                    chromeOptions.AddArgument("--headless=new");
                }
                if (driverconfig.IsDisableImageLoading)
                {
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                }
                if (!string.IsNullOrEmpty(driverconfig.MobileDeviceFake) && driverconfig.FakeMobileDevice) // nếu cái user agent ko rỗng
                {
                    OpenQA.Selenium.Chromium.ChromiumMobileEmulationDeviceSettings chromiumMobileEmulationDeviceSettings = new OpenQA.Selenium.Chromium.ChromiumMobileEmulationDeviceSettings();
                    chromiumMobileEmulationDeviceSettings.UserAgent = driverconfig.MobileDeviceFake;
                    chromiumMobileEmulationDeviceSettings.EnableTouchEvents = true;
                    //chromiumMobileEmulationDeviceSettings.Width = 500; // chiều rộng của khung
                    //chromiumMobileEmulationDeviceSettings.Height = 500; // chiều dài của khung
                    chromiumMobileEmulationDeviceSettings.PixelRatio = 5.0; // tỉ lệ pixel
                    chromeOptions.EnableMobileEmulation(chromiumMobileEmulationDeviceSettings); // thêm setting mobile device
                    chromeOptions.AddUserProfilePreference("mobile.enable", true); // bật setting mobile device
                }
                WebDriverProxyConfig.Proxy proxy = null;
                if (!string.IsNullOrEmpty(driverconfig.ProxyConfig.ProxyUrl) && driverconfig.ProxyConfig.UseProxy)
                {
                    proxy = WebDriverProxyConfig.Proxy.Parse(driverconfig.ProxyConfig.ProxyUrl);
                    if (driverconfig.ProxyConfig.TypeProxy == WebDriverProxyConfig.ProxyType.ProxyServer)
                    {
                        chromeOptions.AddArgument($"--proxy-server={proxy.Server}:{proxy.Port}");
                    }
                    else if (driverconfig.ProxyConfig.TypeProxy == WebDriverProxyConfig.ProxyType.Socks)
                    {
                        chromeOptions.AddArgument("--proxy-server=socks5://" + proxy.ToString());
                    }
                    else if (driverconfig.ProxyConfig.TypeProxy == WebDriverProxyConfig.ProxyType.Http)
                    {
                        chromeOptions.Proxy = new Proxy
                        {
                            Kind = ProxyKind.Manual,
                            IsAutoDetect = false,
                            HttpProxy = proxy.ToString(),
                            SslProxy = proxy.ToString(),
                            FtpProxy = proxy.ToString()
                        };
                        chromeOptions.AddArgument("--proxy-server=" + proxy.ToString());
                    }
                    else
                    {
                        chromeOptions.AddArgument("--proxy-server=http://127.0.0.1:" + proxy.Port.ToString());
                    }
                }
                try
                {
                    chromeOptions.ToCapabilities();
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    if (driverconfig.ProxyConfig.TypeProxy == WebDriverProxyConfig.ProxyType.ProxyServer)
                    {
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                        driver.FindElement(By.Id("login")).SendKeys(proxy.UserName);
                        driver.FindElement(By.Id("password")).SendKeys(proxy.Password);
                        driver.FindElement(By.Id("save")).Click();
                        Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                        driver.Close();
                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                    }
                    result.Chrome = (ChromeDriver)driver;
                    result.ProcessId = chromeDriverService.ProcessId;
                }
                catch (Exception ex)
                {
                    lock (LockErrorHandle)
                    {
                        File.AppendAllText("Log.txt", ex.ToString() + Environment.NewLine);
                    }
                    if (driver != null)
                    {
                        driver.Close();
                        driver.Quit();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                lock (LockErrorHandle)
                {
                    File.AppendAllText("Log.txt", ex.ToString() + Environment.NewLine);
                }
                return result;
            }
            return result;
        }
    }
}
