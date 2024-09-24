using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.AdvcSelenium
{
    public class WebDriverProxyConfig
    {
        /// <summary>
        /// kiểu proxy
        /// </summary>
        public ProxyType TypeProxy { get; set; }
        /// <summary>
        /// full đường dẫn proxy, ví dụ : 127.0.0.1:8080 hoặc 127.0.0.1:8080:user:password
        /// </summary>
        public string ProxyUrl { get; set; }
        /// <summary>
        /// có dùng proxy hay không
        /// </summary>
        public bool UseProxy { get; set; }
        /// <summary>
        /// các loại proxy
        /// </summary>
        public enum ProxyType
        {
            Http,
            Socks,
            ProxyServer,
            ForwardedPort
        }
        /// <summary>
        /// proxy class
        /// </summary>
        public class Proxy
        {
            public static Proxy Parse(string proxy)
            {
                Proxy prx = new Proxy();
                string[] proxyarry = proxy.Split(':');
                if (proxyarry.Length < 0)
                {
                    throw new FormatException("invalid proxy format !");
                }
                prx.Server = proxyarry[0];
                prx.Port = Convert.ToInt32(proxyarry[1]);
                if (proxyarry.Length >= 3)
                {
                    prx.UserName = proxyarry[2];
                    prx.Password = proxyarry[3];
                }
                return prx;
            }
            public override string ToString()
            {
                string result = $"{Server}:{Port}";
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    result += $":{UserName}:{Password}";
                }
                return result;
            }
            /// <summary>
            /// server chính của proxy
            /// </summary>
            public string Server { get; set; }
            /// <summary>
            /// cổng port
            /// </summary>
            public int Port { get; set; }
            /// <summary>
            /// username
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// mật khẩu
            /// </summary>
            public string Password { get; set; }
        }
    }
}
