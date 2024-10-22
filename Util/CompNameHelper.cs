using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace CustomerMaster.Util
{
    public class CompNameHelper
    {
        public static string DetermineCompName(string IP)
        {
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }
    }
}
