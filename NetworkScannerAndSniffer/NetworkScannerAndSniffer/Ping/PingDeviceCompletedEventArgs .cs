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
    public class PingDeviceCompletedEventArgs : EventArgs
    {

        public PingDeviceStatus Status;

        public string IP;


    }
}
