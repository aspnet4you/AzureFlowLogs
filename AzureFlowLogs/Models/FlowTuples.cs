using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspnet4you.AzureFlowLogs.Models
{
    public class FlowTuples
    {
        string timeStampUnixEpoch;
        string timeStamp;
        string sourceIP;
        string sourcePort;
        string destinationIP;
        string destinationPort;
        string protocol;
        string trafficFlow;
        string trafficDecision;

        public FlowTuples() { }

        public FlowTuples(string flowTuples)
        {
            string[] tuples = flowTuples.Split(",");
            this.timeStampUnixEpoch = tuples[0];
            this.timeStamp = epoch2string(int.Parse(tuples[0]));
            this.sourceIP = tuples[1];
            this.destinationIP = tuples[2];
            this.sourcePort = tuples[3];
            this.destinationPort = tuples[4];
            this.protocol = tuples[5];
            this.trafficFlow = tuples[6];
            this.trafficDecision = tuples[7];
        }

        public string TimeStampUnixEpoch { get => timeStampUnixEpoch; set => timeStampUnixEpoch = value; }
        public string TimeStamp { get => timeStamp; set => timeStamp = value; }
        public string SourceIP { get => sourceIP; set => sourceIP = value; }
        public string SourcePort { get => sourcePort; set => sourcePort = value; }
        public string DestinationIP { get => destinationIP; set => destinationIP = value; }
        public string DestinationPort { get => destinationPort; set => destinationPort = value; }
        public string Protocol { get => protocol; set => protocol = value; }
        public string TrafficFlow { get => trafficFlow; set => trafficFlow = value; }
        public string TrafficDecision { get => trafficDecision; set => trafficDecision = value; }

        private string epoch2string(int epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToString("MM/dd/yyyy HH:mm:ss tt");
        }
    }
}
