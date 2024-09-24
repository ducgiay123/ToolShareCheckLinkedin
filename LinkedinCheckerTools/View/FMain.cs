using Leaf.xNet;
using LinkedinCheckerTools.API;
using LinkedinCheckerTools.Config;
using LinkedinCheckerTools.Models;
using LinkedinCheckerTools.Request;
using LinkedinCheckerTools.Singleton;
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
        public FMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            LoadUISettings();
            this.fMainDatagridManager = new FMainDatagridManager(this.dtgrvdata);
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
                            //int indexPos = ChromeUtils.GetIndexOfPossitionApp();
                            //Start(indexPos);
                            Start();
                            //ChromeUtils.FillIndexPossition(indexPos);
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
        private void Start()
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
                        // do chrome task
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
    }
}
