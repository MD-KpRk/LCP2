using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class LCPP // LOCAL CONTROL PROJECT PROTOCOL
    {
        public int SourcePort { get; set; }
        public int DestPort { get; set; }
        public IPAddress SourceIP { get; set; }
        public string Command { get; set; } = "";
        public string Args { get; set; } = "";

        public LCPP(int SourcePort, int DestPort, IPAddress SourceIP, string Command, string Args = "")
        {
            this.SourcePort = SourcePort; this.DestPort = DestPort; this.SourceIP = SourceIP; this.Command = Command; this.Args = Args; 
        }
    }
}
