using Leaf.xNet;
using LinkedinCheckerTools.Models;
using LinkedinCheckerTools.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.API
{
    public class LinkedinAPI
    {
        public LinkedinAPIExecuteResult.CheckpointEmailSubmitResult CheckpointEmailSubmit(LinkedinAPIOptions.CheckpointEmailOptions options)
        {
            var result = new LinkedinAPIExecuteResult.CheckpointEmailSubmitResult();
            result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Success;
            result.CheckpointEmailStatus = Enums.CheckpointEmailStatusCode.Nothing;
            HttpRequest httpRequest = HttpFactory.NewClient(options.HttpConfig);
            HttpResponse httpResponse = null;
            try
            {
                string urlcheckpointsubmit = "https://www.linkedin.com/checkpoint/lg/login-submit";
                httpRequest.AddHeader("Accept", "*/*");
                httpRequest.AddHeader("Pragma", "no-cache");
                httpRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpResponse = httpRequest.Post(urlcheckpointsubmit);
                if (string.IsNullOrEmpty(httpResponse.ToString()))
                {
                    result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
                    goto EndPoint;
                }
                string csrfToken = Regex.Match(httpResponse.ToString(), "name=\"csrfToken\" value=\"(.*?)\"").Groups[1].Value;
                string ac = Regex.Match(httpResponse.ToString(), "name=\"ac\" value=\"(.*?)\"").Groups[1].Value;
                string loginCsrfParam = Regex.Match(httpResponse.ToString(), "name=\"loginCsrfParam\" value=\"(.*?)\"").Groups[1].Value;
                string apfc = Regex.Match(httpResponse.ToString(), "name=\"apfc\" value=\"(.*?)\"").Groups[1].Value;
                string _d = Regex.Match(httpResponse.ToString(), "name=\"_d\" value=\"(.*?)\"").Groups[1].Value;
                string payload = $"csrfToken={csrfToken}&session_key={options.Email}&ac={ac}&sIdString=<sIdString>&parentPageKey=d_checkpoint_lg_consumerLogin&pageInstance=urn%3Ali%3Apage%3Ad_checkpoint_lg_consumerLogin%3BH4kX%2FmdGQjaHdiYTYRYkwg%3D%3D&trk=&authUUID=&session_redirect=&loginCsrfParam={loginCsrfParam}&fp_data=default&apfc={apfc}&_d={_d}&showGoogleOneTapLogin=true&controlId=d_checkpoint_lg_consumerLogin-login_submit_button&session_password=<PASS>";
                BuildPerfectHeaderLoginCheckpointSubmit(httpRequest, httpResponse.Address.ToString(), payload.Length.ToString());
                httpResponse = httpRequest.Post(urlcheckpointsubmit, payload, HttpGlobal.ContentTypes.FormUrlEncoded);
                if (string.IsNullOrEmpty(httpResponse.ToString()))
                {
                    result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
                    goto EndPoint;
                }
                if(httpResponse.ToString().Contains("Wrong email or password. Try again or <span data-tracking-control-name=\\\"login_error_create_account\\"))
                {
                    result.CheckpointEmailStatus = Enums.CheckpointEmailStatusCode.Success;
                }
                else if (httpResponse.ToString().Contains("checkpoint/challenge/funCaptchaInternal"))
                {
                    result.CheckpointEmailStatus = Enums.CheckpointEmailStatusCode.Captcha;
                }
                else
                {
                    result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
                    result.CheckpointEmailStatus = Enums.CheckpointEmailStatusCode.Nothing;
                }
            EndPoint:;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
            }
            finally
            {
                httpRequest?.Close();
                httpRequest?.Dispose();
                httpResponse = null;
            }
            return result;
        }
        private void BuildPerfectHeaderLoginCheckpointSubmit(HttpRequest httpRequest, string referer, string contentlenght)
        {
            httpRequest.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            httpRequest.AddHeader("accept-language", "en-US,en;q=0.9");
            httpRequest.AddHeader("cache-control", "max-age=0");
            httpRequest.AddHeader("origin", "https://www.linkedin.com");
            httpRequest.AddHeader("referer", referer);
            httpRequest.AddHeader("sec-ch-ua", "\"Microsoft Edge\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"");
            httpRequest.AddHeader("sec-ch-ua-mobile", "?0");
            httpRequest.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            httpRequest.AddHeader("sec-fetch-dest", "document");
            httpRequest.AddHeader("sec-fetch-mode", "navigate");
            httpRequest.AddHeader("sec-fetch-site", "same-origin");
            httpRequest.AddHeader("sec-fetch-user", "?1");
            httpRequest.AddHeader("upgrade-insecure-requests", "1");
            httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36 Edg/93.0.961.52");
        }
        public LinkedinAPIExecuteResult.VerifyEmailResult Verify(LinkedinAPIOptions.VerifyEmailOptions options)
        {
            var result = new LinkedinAPIExecuteResult.VerifyEmailResult();
            result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Success;
            result.EmailStatusCode = Enums.VerifyEmailStatusCode.Nothing;
            HttpRequest httpRequest = HttpFactory.NewClient(options.HttpConfig);
            HttpResponse httpResponse = null;
            try
            {
                string urlVerify = $"https://www.linkedin.com/jobs-guest/jobs/api/verifyEmailAddress?emailAddress={options.Email}&flowType=SAVE_JOB";
                httpResponse = httpRequest.Get(urlVerify);
                if(httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    result.EmailStatusCode = Enums.VerifyEmailStatusCode.Linked;
                }
                else if(httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    result.EmailStatusCode = Enums.VerifyEmailStatusCode.NotLinked;
                }
                else
                {
                    result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
                }
            }
            catch(Exception ex)
            {
                result.Exception = ex;
                result.StatusCode = Enums.LinkedinAPIExecuteStatusCode.Error;
            }
            finally
            {
                httpRequest?.Close();
                httpRequest?.Dispose();
                httpResponse = null;
            }
            return result;
        }
    }
}
