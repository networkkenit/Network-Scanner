using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Network_Scanner
{
    public class PingDeviceCompletedEventArgs : EventArgs
    {

        public PingDeviceStatus Status;

        public string IP;

        public string Host => (IP != null) ? GetHostName(IP) : null;

        public List<string> Ipv6 => (IP != null) ? getIPV6Addr(IP) : null;

        public string MAC => (IP != null) ? getMACAddresse(IP) : null;

        List<string> getIPV6Addr(string ipv4)
        {
            try
            {
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(ipv4);
                IPAddress[] addr = ipEntry.AddressList;
                List<string> foundIPs = new List<string>();
                foreach (IPAddress iPAddress in addr)
                {
                    if (iPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        foundIPs.Add(iPAddress.ToString());

                    }

                }
                return foundIPs;
            }
            catch (Exception ex) { return null; }

            return null;

        }
        string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch (SocketException)
            {
                // MessageBox.Show(e.Message.ToString());
            }

            return null;
        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] MacAddr, ref int MacLen);

        public string getMACAddresse(string Ipaddress)
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
