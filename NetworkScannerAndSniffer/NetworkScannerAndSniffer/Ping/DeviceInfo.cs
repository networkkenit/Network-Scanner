using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{
    public class DeviceInfo
    {

        public string IPV4 { get; set; }       
        public List<string> IPV6 { get; set; }
        public string MAC { get; set; }

        public string HostName { get; set; }

        public DeviceInfo(string ipv4)
        {

            IPV4 = ipv4;
            try
            {
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(ipv4);
                HostName = ipEntry.HostName;
                IPAddress[] addr = ipEntry.AddressList;
                List<string> _IPv6 = new List<string>();
                foreach (IPAddress iPAddress in addr)
                {
                    if (iPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        _IPv6.Add(iPAddress.ToString());

                    }

                }
                IPV6 = _IPv6;
                MAC = getMACAddresse(ipv4);
            }
            catch (Exception ex) { 
            


            }

           


        }
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] MacAddr, ref int MacLen);

        public static string getMACAddresse(string Ipaddress)
        {

            IPAddress address = IPAddress.Parse(Ipaddress);
            try
            {
                byte[] MACByte = new byte[6];
                int MACLength = MACByte.Length;
                SendARP((int)address.Address, 0, MACByte, ref MACLength);
                string MACSSTR = BitConverter.ToString(MACByte, 0, 6);
                if (MACSSTR != "00-00-00-00-00-00")
                    return PhysicalAddress.Parse(MACSSTR).ToString();
            }
            catch (Exception ex) { return "not detected"; }
            return "not detected";

        }

    }

}
