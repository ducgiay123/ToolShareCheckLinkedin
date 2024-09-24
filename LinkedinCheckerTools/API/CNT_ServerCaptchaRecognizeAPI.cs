using Leaf.xNet;
using LinkedinCheckerTools.Models;
using LinkedinCheckerTools.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinkedinCheckerTools.API
{
    public class CNT_ServerCaptchaRecognizeAPI
    {
        private const string DefaultAccessKey = "chaunghieptin";
        private const string ServerUrl = "http://chaunghieptin.ddns.net:2003";
        public CNT_CaptchaServerModel.CreateTaskResult CreateTask(string base64img, string captchatype, int tryRequestCount)
        {
            CNT_CaptchaServerModel.CreateTaskResult result = new CNT_CaptchaServerModel.CreateTaskResult();
            result.IsSuccess = false;
            result.IsRequestTimeout = false;
            HttpRequest httpRequest = HttpFactory.NewClient(HttpConfig.Default);
            HttpResponse httpResponse = null;
            CNT_CaptchaServerModel.CreateTaskJsonModel createTaskJsonModel = new CNT_CaptchaServerModel.CreateTaskJsonModel
            {
                clientKey = DefaultAccessKey,
                task = new CNT_CaptchaServerModel.CreateTaskJsonModel.Task
                {
                    image = base64img,
                    question = captchatype
                }
            };
            string json = JsonConvert.SerializeObject(createTaskJsonModel);
            int tryrqtime = 0;
            string ResponseStr = String.Empty;
            while (tryrqtime < tryRequestCount)
            {
                try
                {
                    httpResponse = httpRequest.Post(ServerUrl + "/createTask", json, HttpGlobal.ContentTypes.JsonContentType);
                    ResponseStr = httpResponse.ToString();
                    if (!string.IsNullOrEmpty(ResponseStr))
                    {
                        break;
                    }
                }
                catch
                {

                }
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                tryrqtime++;
            }
            if (tryrqtime >= tryRequestCount)
            {
                result.IsRequestTimeout = true;
            }
            if (!string.IsNullOrEmpty(ResponseStr) && ResponseStr.Contains("taskId\":"))
            {
                result.TaskId = JObject.Parse(ResponseStr)["taskId"].ToString();
                result.IsSuccess = !string.IsNullOrEmpty(result.TaskId);
            }
            httpRequest?.Close();
            httpRequest?.Dispose();
            httpResponse = null;
            json = String.Empty;
            ResponseStr = String.Empty;
            createTaskJsonModel = null;
            return result;
        }
        public CNT_CaptchaServerModel.GetTaskResult GetTaskResult(string taskid, int waitcount)
        {
            CNT_CaptchaServerModel.GetTaskResult result = new CNT_CaptchaServerModel.GetTaskResult();
            result.IsSuccess = false;
            HttpRequest httpRequest = HttpFactory.NewClient(HttpConfig.Default);
            HttpResponse httpResponse = null;
            CNT_CaptchaServerModel.GetTaskResultJsonModel getTaskResultJsonModel = new CNT_CaptchaServerModel.GetTaskResultJsonModel
            {
                key = DefaultAccessKey,
                taskId = taskid
            };
            string json = JsonConvert.SerializeObject(getTaskResultJsonModel);
            string ResponseStr = String.Empty;
            int count = 0;
            while (count < waitcount)
            {
                httpResponse = httpRequest.Post(ServerUrl + "/getTaskResult", json, HttpGlobal.ContentTypes.JsonContentType);
                ResponseStr = httpResponse.ToString();
                try
                {
                    result.Solution = JObject.Parse(ResponseStr)["solution"]["gRecaptchaResponse"].ToString();
                    result.IsSuccess = !string.IsNullOrEmpty(result.Solution);
                    if (result.IsSuccess)
                    {
                        break;
                    }
                }
                catch
                {

                }
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                count++;
            }
            httpRequest?.Close();
            httpRequest?.Dispose();
            httpResponse = null;
            json = String.Empty;
            ResponseStr = String.Empty;
            getTaskResultJsonModel = null;
            return result;
        }
    }
}
