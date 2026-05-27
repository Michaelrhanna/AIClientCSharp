using System;
using System.Collections.Generic;
using System.Text;

namespace AIClient.Model
{
    internal class AppSettings
    {
        public string ShimmyApiKey { get; set; } = string.Empty;
        public string ShimmyBaseUrl { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
    }
}
