using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.AdvcSelenium
{
    public class AdvcWebdriverConfig
    {
        /// <summary>
        /// nếu chỉ khởi tạo 1 chrome
        /// </summary>
        public bool IsSingleThread { get; set; } = false;
        /// <summary>
        /// các đường dẫn đến thư mục extension chưa được pack
        /// </summary>
        public List<string> ExtensionPaths { get; set; }
        public bool UseChromenium { get; set; }
        public string UserAgent { get; set; }
        /// <summary>
        /// đường dẫn đến file chromedriver.exe
        /// </summary>
        public string DriverPath { get; set; }
        /// <summary>
        /// đường dẫn đến profile
        /// </summary>
        public string ProfilePath { get; set; }
        /// <summary>
        /// danh sách các file tiện ích cần cài đặt ( dạng crx )
        /// </summary>
        public List<string> Extensions { get; set; }
        /// <summary>
        ///  có ẩn trình duyệt đi hay không
        /// </summary>
        public bool IsHeadless { get; set; }
        /// <summary>
        /// có tắt hình ảnh đi hay không
        /// </summary>
        public bool IsDisableImageLoading { get; set; }
        /// <summary>
        /// user agent của thiết bị mobile cần fake
        /// </summary>
        public string MobileDeviceFake { get; set; }
        public bool FakeMobileDevice { get; set; }
        public int Position { get; set; }
        public List<string> ChromeOptions { get; set; }
        public WebDriverProxyConfig ProxyConfig { get; set; }
    }
}
