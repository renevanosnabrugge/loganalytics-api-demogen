using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.CustomLogs
{
    public class BuildCompliance
    {
        public string BuildDefinitionName { get; set; }
        public DateTime BuildDateTime { get; set; }
        public string BuildNumber { get; set; }
        public string BuildStatus { get; set; }
        public bool HasFourEyes { get; set; }
    }
}
