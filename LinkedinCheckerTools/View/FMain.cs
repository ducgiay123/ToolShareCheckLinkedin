using Leaf.xNet;
using LinkedinCheckerTools.AdvcSelenium;
using LinkedinCheckerTools.API;
using LinkedinCheckerTools.Automation;
using LinkedinCheckerTools.Config;
using LinkedinCheckerTools.Models;
using LinkedinCheckerTools.Request;
using LinkedinCheckerTools.Singleton;
using LinkedinCheckerTools.Utils;
using LinkedinCheckerTools.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LinkedinCheckerTools.Models.LinkedinAPIExecuteResult;

namespace LinkedinCheckerTools.View
{
    public partial class FMain : Form
    {
        private ConcurrentQueue<string> Dataqueue = new ConcurrentQueue<string>();
        private List<string> ProxyPool = new List<string>();
        private int LinkedC;
        private int DieC;
        private int NotLinkedC;
        private int SuccessC;
        private int FailureC;
        private int RetriesC;
        private CancellationTokenSource cancellationTokenSource;
        private FMainDatagridManager fMainDatagridManager;
        private string SavePath;
        private ProxyType _ProxyType;
        public FMain(AccountLoginResult accountLoginResult)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Text = $"Linkedin AIO Tools v{ProductVersion} | {accountLoginResult.LoginAccountInfo.UserName} / Expired : {accountLoginResult.LoginAccountInfo.ExpiredDate}";
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            //this.Text = $"Linkedin AIO Tools v{ProductVersion}";
            LoadUISettings();
            this.fMainDatagridManager = new FMainDatagridManager(this.dtgrvdata);
            ChromeUtils.getWidthScreen = Screen.PrimaryScreen.Bounds.Width;
            ChromeUtils.getHeightScreen = Screen.PrimaryScreen.Bounds.Height;
            CNT_CaptchaQuestionUtils.LoadAllQuestion();
        }
        private void LoadUISettings()
        {
            numthreads.Value = MainFormUISettings.Threads;
            chkuseproxy.Checked = MainFormUISettings.UseProxy;
            cbbprxtype.SelectedIndex = MainFormUISettings.ProxyType;
            switch (MainFormUISettings.TaskType)
            {
                case 1:
                    rdchecklinked.Checked = true;
                    break;
                case 2:
                    rdcheckdie.Checked = true;
                    break;
                case 3:
                    rdchecklkobj.Checked = true;
                    break;
            }
        }
        private void numthreads_ValueChanged(object sender, EventArgs e)
        {
            MainFormUISettings.Threads = (int)numthreads.Value;
        }

        private void chkuseproxy_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.UseProxy = chkuseproxy.Checked;
        }

        private void cbbprxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainFormUISettings.ProxyType = cbbprxtype.SelectedIndex;
            _ProxyType = cbbprxtype.SelectedIndex == 0 ? ProxyType.HTTP
                : cbbprxtype.SelectedIndex == 1 ? ProxyType.Socks4
                : ProxyType.Socks5;
        }

        private void rdchecklinked_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.TaskType = 1;
        }

        private void rdcheckdie_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.TaskType = 2;
        }

        private void rdchecklkobj_CheckedChanged(object sender, EventArgs e)
        {
            MainFormUISettings.TaskType = 3;
        }
        private void InitRangeWindow(int threadsCount)
        {
            ChromeUtils.getWidthChrome = (2 * ChromeUtils.getWidthScreen) / 8;
            ChromeUtils.getHeightChrome = ChromeUtils.getHeightScreen / 2;
            int maxThread = Convert.ToInt32(threadsCount);
            for (int i = 0; i < (int)maxThread; i++)
            {
                ChromeUtils.listPossitionApp.Add(0);
            }
        }
        private void btnstart_Click(object sender, EventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            ResetCounter();
            dtgrvdata.Rows.Clear();
            this.fMainDatagridManager.Mails.Clear();
            tmupdatecount.Start();
            Isruning(true);
            string datetimnow = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            SavePath = Application.StartupPath + "\\result\\result_" + datetimnow;
            Directory.CreateDirectory(SavePath);
            int Maxthreads = (int)numthreads.Value;
            InitRangeWindow(Maxthreads);
            new Thread(() =>
            {
                ThreadPool.SetMinThreads(Maxthreads, Maxthreads);
                Thread[] array = new Thread[Maxthreads];
                for (int i = 0; i < Maxthreads; i++)
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                    array[i] = new Thread((ThreadStart)delegate
                    {
                        while (!cancellationTokenSource.IsCancellationRequested && !Dataqueue.IsEmpty)
                        {
                            int indexPos = ChromeUtils.GetIndexOfPossitionApp();
                            Start(indexPos);
                            ChromeUtils.FillIndexPossition(indexPos);
                        }
                    });
                    array[i].Start();
                }
                for (int j = 0; j < Maxthreads; j++)
                {
                    if (array[j] != null)
                    {
                        array[j].Join();
                    }
                }
                CloseAllChrome();
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                tmupdatecount.Stop();
                SaveRemainData();
                Isruning(false);
                cancellationTokenSource.Cancel();
                GC.SuppressFinalize(this);
                GC.WaitForFullGCApproach();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }).Start();
        }
        private void SaveRemainData()
        {
            SaveFile(SavePath + "\\RemainData.txt", string.Join(Environment.NewLine, Dataqueue));
        }
        private void SaveFile(string path, string content)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    File.AppendAllText(path, content + Environment.NewLine);
                }));
            }
            catch { }
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        private void btnresultFolder_Click(object sender, EventArgs e)
        {
            string pathopen = Directory.Exists(SavePath) ? SavePath : Environment.CurrentDirectory;
            Process.Start(pathopen);
            pathopen = string.Empty;
        }

        private void btnimportdata_Click(object sender, EventArgs e)
        {
            FrmImport import = new FrmImport($"Data");
            import.ShowDialog(this);
            if (import.ImportResult == DialogResult.OK && import.ImportedData.Count != 0)
            {
                Dataqueue = new ConcurrentQueue<string>(import.ImportedData);
                lbdataC.Text = $"{import.ImportedData.Count}";
            }
        }

        private void btnimportproxy_Click(object sender, EventArgs e)
        {
            FrmImport import = new FrmImport($"Proxies");
            import.ShowDialog(this);
            if (import.ImportResult == DialogResult.OK && import.ImportedData.Count != 0)
            {
                ProxyPool = new List<string>(import.ImportedData);
                lbprxC.Text = $"{import.ImportedData.Count}";
            }
        }
        private string RandomProxy()
        {
            return ProxyPool.RandomItemInList();
        }
        private void Start(int indexchrome)
        {
            string data = string.Empty;
            string proxy = string.Empty;
            try
            {
                FMainDatagridDTO fMainDatagrid = null;
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    if (Dataqueue.IsEmpty)
                    {
                        break;
                    }
                    Dataqueue.TryDequeue(out data);
                    if (string.IsNullOrEmpty(data))
                    {
                        continue;
                    }
                    if(fMainDatagrid == null)
                    {
                        fMainDatagrid = this.fMainDatagridManager.AddNewRow();
                    }
                    string[] datarr = data.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                    string email = datarr[0];
                    fMainDatagrid.Email = data;
                    proxy = RandomProxy();
                    if(MainFormUISettings.TaskType == 1 || MainFormUISettings.TaskType == 2)
                    {
                        HttpConfig httpConfig = new HttpConfig
                        {
                            UseProxy = false
                        };
                        if(!string.IsNullOrEmpty(proxy) && MainFormUISettings.UseProxy)
                        {
                            fMainDatagrid.Proxy = proxy;
                            httpConfig.UseProxy = true;
                            httpConfig.Proxy = ProxyClient.Parse(_ProxyType, proxy);
                            httpConfig.ConnectTimeOut = 15000;
                        }
                        DoHttpApiTask(httpConfig, email, data, fMainDatagrid);
                    }
                    else
                    {
                        AdvcWebdriverConfig advcWebdriverConfig = new AdvcWebdriverConfig
                        {
                            Extensions = new List<string>(),
                            IsDisableImageLoading = false,
                            IsHeadless = false,
                            MobileDeviceFake = String.Empty,
                            ChromeOptions = null,
                            FakeMobileDevice = false,
                            Position = indexchrome,
                            ProfilePath = String.Empty,
                            ProxyConfig = new WebDriverProxyConfig(),
                            UseChromenium = true,
                            ExtensionPaths = new List<string>(),
                            IsSingleThread = false,
                            DriverPath = PathSingleton.DefaultDriverExecutablePath
                        };
                        if (!string.IsNullOrEmpty(proxy))
                        {
                            fMainDatagrid.Proxy = proxy;
                            advcWebdriverConfig.ProxyConfig.TypeProxy = WebDriverProxyConfig.ProxyType.Http;
                            advcWebdriverConfig.ProxyConfig.ProxyUrl = proxy;
                            advcWebdriverConfig.ProxyConfig.UseProxy = true;
                            WebDriverProxyConfig.Proxy WebProxy = WebDriverProxyConfig.Proxy.Parse(proxy);
                            if (string.IsNullOrEmpty(WebProxy.UserName))
                            {
                                advcWebdriverConfig.ProxyConfig.TypeProxy = WebDriverProxyConfig.ProxyType.Http;
                            }
                            else
                            {
                                advcWebdriverConfig.ProxyConfig.TypeProxy = WebDriverProxyConfig.ProxyType.ProxyServer;
                            }
                        }
                        string guid_chrome = Guid.NewGuid().ToString();
                        advcWebdriverConfig.Extensions.Add(PathSingleton.AuthProxyExtensionPath);
                        AdvcWebDriver advcWebDriver = new AdvcWebDriver();
                        fMainDatagrid.Status = "init chrome...";
                        CreateChromeDriverResult createChromeDriverResult = new CreateChromeDriverResult();
                        createChromeDriverResult = advcWebDriver.CreateNewDriver(advcWebdriverConfig);
                        if (createChromeDriverResult.Chrome == null)
                        {
                            fMainDatagrid.Status = "Chrome error !";
                            Dataqueue.Enqueue(data);
                            goto EndInvoke;
                        }
                        GlobalVariables.Drivers.Add(guid_chrome, createChromeDriverResult.Chrome);
                        if (createChromeDriverResult.Chrome.WindowHandles.Count >= 2)
                        {
                            createChromeDriverResult.Chrome.Close();
                            createChromeDriverResult.Chrome.SwitchTo().Window(createChromeDriverResult.Chrome.WindowHandles.Last());
                        }
                        AutomationOptions.LinkedinRecoverEmailOptions linkedinRecoverEmailOptions = new AutomationOptions.LinkedinRecoverEmailOptions
                        {
                            Chrome = createChromeDriverResult.Chrome,
                            Datagrid = fMainDatagrid,
                            Email = email
                        };
                        LinkedinRecoverEmailAutomation linkedinRecoverEmailAutomation = new LinkedinRecoverEmailAutomation();
                        AutomationResult.LinkedinRecoverEmailResult linkedinRecoverEmailResult = linkedinRecoverEmailAutomation.Recover(linkedinRecoverEmailOptions);
                        if(linkedinRecoverEmailResult.RecoverEmailStatusCode == Enums.LinkedinRecoverEmailStatusCode.Error)
                        {
                            RetriesC++;
                            Dataqueue.Enqueue(data);
                            goto EndInvoke;
                        }
                        fMainDatagrid.Status = $"check linked obj {linkedinRecoverEmailResult.RecoverEmailStatusCode} !";
                        if (linkedinRecoverEmailResult.RecoverEmailStatusCode == Enums.LinkedinRecoverEmailStatusCode.Success)
                        {
                            string linkedobj = string.Empty;
                            if(linkedinRecoverEmailResult.LinkedObjs.Count > 0)
                            {
                                linkedobj = string.Join("|", linkedinRecoverEmailResult.LinkedObjs);
                            }
                            fMainDatagrid.LinkedObj = linkedobj;
                            SuccessC++;
                            string saveformat = $"{data}|{linkedinRecoverEmailResult.LinkedObjs.Count}_LK|{linkedobj}";
                            SaveFile(SavePath + "\\SuccessCheckLinkedObj.txt", saveformat);
                            saveformat = string.Empty;
                            linkedobj = string.Empty;
                        }
                        if(linkedinRecoverEmailResult.RecoverEmailStatusCode == Enums.LinkedinRecoverEmailStatusCode.Failed)
                        {
                            SaveFile(SavePath + "\\FailedCheckLinkedObj.txt", data);
                            FailureC++;
                        }
                    EndInvoke:
                        Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                        try
                        {
                            createChromeDriverResult.Chrome.Quit();
                            createChromeDriverResult.Chrome.Dispose();
                            advcWebdriverConfig = null;
                            if (GlobalVariables.Drivers.ContainsKey(guid_chrome))
                            {
                                GlobalVariables.Drivers.Remove(guid_chrome);
                            }
                        }
                        catch
                        {

                        }
                        try
                        {
                            if (createChromeDriverResult != null)
                            {
                                Process process_kill = Process.Start(new ProcessStartInfo()
                                {
                                    FileName = "taskkill",
                                    Arguments = $"/f /pid {createChromeDriverResult.ProcessId} /t",
                                    CreateNoWindow = true,
                                    WindowStyle = ProcessWindowStyle.Hidden
                                });
                                process_kill.Dispose();
                            }
                        }
                        catch { }
                    }
                }
            }
            catch
            {

            }
        }
        private void DoHttpApiTask(HttpConfig httpConfig, string email, string rawdata, FMainDatagridDTO fMainDatagrid)
        {
            LinkedinAPI linkedinAPI = new LinkedinAPI();
            if(MainFormUISettings.TaskType == 1)
            {
                LinkedinAPIOptions.VerifyEmailOptions verifyEmailOptions = new LinkedinAPIOptions.VerifyEmailOptions
                {
                    Email = email,
                    HttpConfig = httpConfig
                };
                fMainDatagrid.Status = "verifying email...";
                LinkedinAPIExecuteResult.VerifyEmailResult verifyEmailResult = linkedinAPI.Verify(verifyEmailOptions);
                if(verifyEmailResult.StatusCode == Enums.LinkedinAPIExecuteStatusCode.Error)
                {
                    RetriesC++;
                    Dataqueue.Enqueue(rawdata);
                    return;
                }
                fMainDatagrid.Status = $"verify done ! status : {verifyEmailResult.EmailStatusCode}";
                if (verifyEmailResult.EmailStatusCode == Enums.VerifyEmailStatusCode.Linked)
                {
                    LinkedC++;
                }
                else
                {
                    NotLinkedC++;
                }
                SaveFile(SavePath + $"\\{verifyEmailResult.EmailStatusCode}.txt", rawdata);
            }
            if(MainFormUISettings.TaskType == 2)
            {
                LinkedinAPIOptions.CheckpointEmailOptions checkpointEmailOptions = new LinkedinAPIOptions.CheckpointEmailOptions
                {
                    Email = email,
                    HttpConfig = httpConfig
                };
                fMainDatagrid.Status = "checking email...";
                LinkedinAPIExecuteResult.CheckpointEmailSubmitResult checkpointEmailSubmitResult = linkedinAPI.CheckpointEmailSubmit(checkpointEmailOptions);
                if(checkpointEmailSubmitResult.StatusCode == Enums.LinkedinAPIExecuteStatusCode.Error)
                {
                    RetriesC++;
                    Dataqueue.Enqueue(rawdata);
                    return;
                }
                fMainDatagrid.Status = $"check done ! status : {checkpointEmailSubmitResult.CheckpointEmailStatus}";
                string filesavename = string.Empty;
                if(checkpointEmailSubmitResult.CheckpointEmailStatus == Enums.CheckpointEmailStatusCode.Success)
                {
                    filesavename = "Success.txt";
                    SuccessC++;
                }
                else
                {
                    filesavename = "FailedEmail.txt";
                    FailureC++;
                }
                SaveFile(SavePath + $"\\{filesavename}", rawdata);
            }
        }
        private void Isruning(bool status)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                btnstart.Enabled = !status;
                btnstop.Enabled = status;
            });
        }
        private void ResetCounter()
        {
            LinkedC = 0;
            DieC = 0;
            NotLinkedC = 0;
            SuccessC = 0;
            FailureC = 0;
            RetriesC = 0;
        }

        private void tmupdatecount_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    lblinkedC.Text = LinkedC.ToString();
                    lbdieC.Text = DieC.ToString();
                    lbnotlkC.Text = NotLinkedC.ToString();
                    lbsuccessC.Text = SuccessC.ToString();
                    lbfailedC.Text = FailureC.ToString();
                    lbretriesC.Text = RetriesC.ToString();
                }));
            }
            catch { }
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show(this,"Are your want to exit ?", "exit confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Application.Exit();
                cancellationTokenSource.Cancel();
                CloseAllChrome();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void CloseAllChrome()
        {
            if(GlobalVariables.Drivers.Count > 0)
            {
                foreach(var driver in GlobalVariables.Drivers)
                {
                    try
                    {
                        driver.Value.Quit();
                        driver.Value.Dispose();
                    }
                    catch { }
                }
            }
            GlobalVariables.Drivers.Clear();
        }
    }
}
