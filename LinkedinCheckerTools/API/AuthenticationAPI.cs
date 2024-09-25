using Leaf.xNet;
using LinkedinCheckerTools.Models;
using LinkedinCheckerTools.Request;
using LinkedinCheckerTools.Singleton;
using LinkedinCheckerTools.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DeviceId;

namespace LinkedinCheckerTools.API
{
    public class AuthenticationAPI
    {
        public static string GetKey()
        {
            string text = new DeviceIdBuilder().AddMachineName().AddOsVersion().OnWindows(delegate (WindowsDeviceIdBuilder windows)
            {
                _ = windows.AddMotherboardSerialNumber().AddSystemSerialDriveNumber();
            }).ToString();
            if (text != null && text.Length > 0)
            {
                text += "LINKEDIN_AIO_TOOLS";
                string result;
                using (MD5 md = MD5.Create())
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(text);
                    byte[] array = md.ComputeHash(bytes);
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < array.Length; i++)
                    {
                        stringBuilder.Append(array[i].ToString("X2"));
                    }
                    stringBuilder[stringBuilder.Length - 1] = 'N';
                    stringBuilder[stringBuilder.Length - 2] = 'D';
                    stringBuilder[stringBuilder.Length - 3] = 'T';
                    result = "LINKEDIN_AIO_TOOLS_" +stringBuilder.ToString();
                }
                return result;
            }
            return null;
        }
        public static bool GetAccess()
        {
            HttpRequest httpRequest = HttpFactory.NewClient(HttpConfig.Default);
            try
            {
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36";
                string response = httpRequest.Get("https://tienichmmo.net/api/tools_state?cod=linkedin_aio_tools").ToString();
                return response.Contains("state\":\"ok\",");
            }
            catch
            {
                return false;
            }
        }
        public static string GetSecuredKey()
        {
            string[] byteValues = GlobalVariables.Lk.Split('|');
            byte[] byteArray = byteValues.Select(byteStr => Convert.ToByte(byteStr)).ToArray();
            string restoredString = Encoding.UTF8.GetString(byteArray);
            return restoredString;
        }
        public static AccountLoginResult Login(string username, string password, string softcode, string systemid)
        {
            HttpRequest httpRequest = HttpFactory.NewClient(HttpConfig.Default);
            HttpResponse httpResponse = null;
            AccountLoginResult result = new AccountLoginResult();
            string apiurl = "https://tienichmmo.net/api/authapi/login.php";
            string payload = $"user={username}&password={password}&device_code={systemid}&software_code={softcode}";
            try
            {
                httpResponse = httpRequest.Post(apiurl, payload, HttpGlobal.ContentTypes.FormUrlEncodedUtf8);
                if (!string.IsNullOrEmpty(payload))
                {
                    string decryptedmessage = CryptorUtils.DecryptStringFromBytes_AesECB(httpResponse.ToString(), GetSecuredKey());
                    JObject json = JObject.Parse(decryptedmessage);
                    string status = json["status"].ToString();
                    if (status != "success")
                    {
                        result.Message = AccountLoginResult.GetErrorMessage(status);
                    }
                    else
                    {
                        result.IsSuccess = true;
                        result.LoginAccountInfo = new LoginAccountInfo();
                        result.LoginAccountInfo.UserName = username;
                        string expdate = json["expired_time"].ToString();
                        expdate = Regex.Unescape(expdate);
                        expdate = DateTime.Parse(expdate).ToString("dd-MM-yyyy");
                        result.LoginAccountInfo.ExpiredDate = expdate;
                    }
                }
                else
                {
                    result.Message = AccountLoginResult.GetErrorMessage("error_connect");
                }
            }
            catch
            {
                result.Message = AccountLoginResult.GetErrorMessage("error_connect");
            }
            finally
            {
                httpRequest.Close();
                httpRequest.Dispose();
                httpResponse = null;
            }
            return result;
        }
    }
}
