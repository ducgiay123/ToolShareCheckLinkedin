using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedinCheckerTools.Singleton;

namespace LinkedinCheckerTools.Utils
{
    public class CNT_CaptchaQuestionUtils
    {
        /// <summary>
        /// 0 is content , 1 is question id
        /// </summary>
        public static Dictionary<string,string> QuestionMap = new Dictionary<string,string>();
        public static void LoadAllQuestion()
        {
            string[] content = File.ReadAllLines(PathSingleton.CaptchaQuestionMapFile);
            foreach(string line in content)
            {
                if (!string.IsNullOrEmpty(line) && line.Contains("|"))
                {
                    string[] arr = line.Split('|');
                    QuestionMap.Add(arr[0], arr[1]);
                }
            }
        }
        public static string GetQuestioIdnByContent(string content)
        {
            if (QuestionMap.ContainsKey(content))
            {
                return QuestionMap[content];
            }
            else
            {
                return null;
            }
        }
    }
}
