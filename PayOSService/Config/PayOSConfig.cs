using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayOSService.Config
{
    public class PayOSConfig
    {
        public static string ConfigName => "PayOS";
        public string ClientId { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ChecksumKey { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
        public string ClientRedirectUrl { get; set; } = string.Empty;
    }
}
