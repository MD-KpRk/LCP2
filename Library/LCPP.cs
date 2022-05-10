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
        public string Message { get; set; } = "";

        public LCPP(int SourcePort, int DestPort, IPAddress SourceIP, string Message)
        {
            this.SourcePort = SourcePort; this.DestPort = DestPort; this.SourceIP = SourceIP; this.Message = Message; 
        }
    }
}
