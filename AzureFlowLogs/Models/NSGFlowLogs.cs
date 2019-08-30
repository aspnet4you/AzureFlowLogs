using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspnet4you.AzureFlowLogs.Models
{

    public class NSGFlowLogs
    {
        public Record[] records { get; set; }
    }

    public class Record
    {
        public DateTime time { get; set; }
        public string systemId { get; set; }
        public string macAddress { get; set; }
        public string category { get; set; }
        public string resourceId { get; set; }
        public string operationName { get; set; }
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public int Version { get; set; }
        public Flow[] flows { get; set; }
    }

    public class Flow
    {
        public string rule { get; set; }
        public Flow1[] flows { get; set; }
    }

    public class Flow1
    {
        public string mac { get; set; }
        public string[] flowTuples { get; set; }
    }
}


