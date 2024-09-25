using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Models
{
    public class AccountLoginResult
    {
        public bool IsSuccess { get; set; } = false;
        public LoginAccountInfo LoginAccountInfo { get; set; }
        public string Message { get; set; }
        public static string GetErrorMessage(string errorid)
        {
            if (ErrorMessagesMap().ContainsKey(errorid))
            {
                return ErrorMessagesMap()[errorid];
            }
            else
            {
                return "UNKNOWN_ERROR";
            }
        }
        public static Dictionary<string, string> ErrorMessagesMap()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("account_not_exits", "Account is not Exits");
            map.Add("password_is_incorrect", "Password is incorrect");
            map.Add("this_software_is_not_active_on_this_account", "This software is not active on your account");
            map.Add("this_software_is_expired", "this software is expired");
            map.Add("max_devices_reached", "max devices reached, please contact admin grant a new device");
            map.Add("error_connect", "Error Connect To Server");
            return map;
        }
    }
}
