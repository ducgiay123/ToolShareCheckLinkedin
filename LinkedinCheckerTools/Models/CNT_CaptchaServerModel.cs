using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Models
{
    public class CNT_CaptchaServerModel
    {
        public class CreateTaskResult : BaseRecognizeApiResult
        {
            public string TaskId { get; set; }
        }
        public class GetTaskResult : BaseRecognizeApiResult
        {
            public string Solution { get; set; }
        }
        public class BaseRecognizeApiResult
        {
            public bool IsSuccess
            {
                get; set;
            }
            public bool IsRequestTimeout { get; set; }
        }
        public class GetTaskResultJsonModel
        {
            public string key { get; set; }
            public string taskId { get; set; }
        }
        public class CreateTaskJsonModel
        {
            public string clientKey { get; set; }
            public Task task { get; set; }
            public class Task
            {
                public string type { get; set; } = "FunCaptchaClassification";
                public string image { get; set; }
                public string question { get; set; } = "rotated";
                public string body { get; set; } = "BASE64 image";
            }
        }
    }
}
