using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public class ActiveDevice
    {

       public string IPV4Adresse { get; set; }
       public List<string> IPV6Adresse { get; set; }
       public string MACAdresse { get; set; }
       public string HostName { get; set; }

        public ActiveDevice(string ipv4,List<string> ipv6,string mac,string host) {
            
            this.IPV4Adresse = ipv4;
            this.IPV6Adresse = ipv6;
            this.MACAdresse = mac;
            this.HostName = host;
        
        }

        public ActiveDevice(string ipv4)
        {
            this.IPV4Adresse = ipv4;
            DeviceInfo deviceInfo = new DeviceInfo(ipv4);
            this.IPV6Adresse = deviceInfo.IPV6;
            this.MACAdresse = deviceInfo.MAC;
            this.HostName = deviceInfo.HostName;

        }


    }
}
