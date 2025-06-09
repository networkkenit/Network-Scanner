using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace NetworkScannerAndSniffer
{


    public partial class NetworkSniffer : Form
    {

        #region --- Properties -------------------------------
        LibPcapLiveDevice Adapter { get; set; }

        ActiveDevice ActiveDevice { get; set; }

        SpoofARP ArpSpoofer { get; set; }

        List<ActiveDevice> Gatways { get; set; }

        ActiveDevice Gatway { get; set; }

        List<DNS_ID> DNS_ID_Color = new List<DNS_ID>();

        #endregion

        #region --- Constructor ------------------------------
        public NetworkSniffer(string deviceAdapter, ActiveDevice activeDevice, List<ActiveDevice> _Gatways)
        {
            InitializeComponent();
            this.form_title.Text = "Watching :" + activeDevice.HostName + " , " + activeDevice.IPV4Adresse;
            init_Adapter_CB(deviceAdapter);
            ActiveDevice = activeDevice;
            Gatways = _Gatways.Distinct().ToList();
            Init_Gatways_CB();

        }
        #endregion
      
        #region --- Methods ---------------------------
         void init_Adapter_CB(string selectedadapter)
        {
            List<string> AdaptersList = new List<string>();

            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {

                    AdaptersList.Add(ip.ToString());

                }
            }

            for (int i = 0;i< LibPcapLiveDeviceList.Instance.Count;i++)
            {
                string adrre = String.Empty;
                foreach (var address in LibPcapLiveDeviceList.Instance[i].Addresses)
                {
                    if (address.Addr.ipAddress != null)
                        if (address.Addr.ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {

                            adrre = address.Addr.ipAddress.ToString();

                        }
                }
         
                if (AdaptersList.Contains(adrre)) {
                    
                        ComboboxItem item = new ComboboxItem();
                        item.Text = adrre;
                        item.Index = i;
                        adapter_comboBox.Items.Add(item);
                    }
            }
            
            if (adapter_comboBox.Items.Contains(selectedadapter)&&!string.IsNullOrEmpty(selectedadapter)) adapter_comboBox.SelectedIndex = adapter_comboBox.FindStringExact(selectedadapter);
            else adapter_comboBox.SelectedIndex = 0;


        }

        void Init_Gatways_CB()
        {
            if (Gatways != null && Gatways.Any())
            {
                foreach (ActiveDevice gw in Gatways)
                    gatways_compobox.Items.Add(gw.IPV4Adresse);
                gatways_compobox.SelectedIndex = 0;

                this.Gatway = this.Gatways[0];
            }
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            if (this.Adapter != null)
            {
                if (!((bool)this.Start_button.Tag))
                {
                    if (!this.Adapter.Opened)
                    {


                        if (Adapter_on())
                        {

                            this.Start_button.Text = "Stop";
                            this.Start_button.BackColor = Color.Red;
                            this.Start_button.Tag = true;
                            this.adapter_comboBox.Enabled = false;
                            this.spoofarp_toggle.Enabled = false;
                            this.gatways_compobox.Enabled = false;
                            groupBox3.Enabled = false;

                        }


                    }

                    else
                    {

                        MessageBox.Show("Driver Allready Open!!");

                    }
                }
                else
                {

                    Adapter_off();
                    this.Start_button.Text = "Start";
                    this.Start_button.BackColor = Color.LimeGreen;
                    this.Start_button.Tag = false;
                    this.adapter_comboBox.Enabled = true;
                    this.spoofarp_toggle.Enabled = true;
                    this.gatways_compobox.Enabled = true;
                     groupBox3.Enabled = true;



                }
            }


        }
        bool Adapter_on()
        {

            try
            {

                if (this.adapter_comboBox.SelectedIndex != -1)
                {

                    if ((bool)spoofarp_toggle.Tag)
                    {

                        Adapter.Open();

                        if (ActiveDevice != null && Gatway != null)
                        {


                            IPAddress target = IPAddress.Parse(ActiveDevice.IPV4Adresse);

                            PhysicalAddress targetMac = PhysicalAddress.Parse(ActiveDevice.MACAdresse);

                            IPAddress gatway = IPAddress.Parse(Gatway.IPV4Adresse);

                            PhysicalAddress gatwayMac = PhysicalAddress.Parse(Gatway.MACAdresse);


                            if (target == null || gatway == null || targetMac == null || gatwayMac == null)
                                throw new Exception("target device or gatway must be not null");

                            ArpSpoofer = new SpoofARP(Adapter, target, targetMac, gatway, gatwayMac);
                            if (ArpSpoofer != null)
                            {

                                try
                                {
                                    ArpSpoofer.SendArpResponsesAsync();
                                    if (ArpSpoofer.Error)
                                        throw new Exception("error while running the Arp Spoofing Thread");

                                    Adapter.OnPacketArrival += Adapter_OnPacketArrival;

                                    Adapter.StartCapture();

                                }
                                catch (Exception ex)
                                {
                                    Adapter.Close();
                                    throw new Exception(ex.Message);

                                }
                            }
                            else
                            {

                                Adapter.Close();
                                throw new Exception("Arp spoofer must be not null");

                            }
                        }
                        else
                        {
                            Adapter.Close();
                            throw new Exception("target device or gatway must be not null");
                        }




                    }
                    else
                    {

                        Adapter.Open();

                        Adapter.OnPacketArrival += Adapter_OnPacketArrival;

                        Adapter.StartCapture();

                    }


                }
                else
                {

                    throw new Exception("Pease Select an Adapter First and then Try again !");
                }

                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                Adapter.Close();
                return false;

            }

        }
        void getPacketInfoINTreeView(Packet packet)
        {

            this.analyse_treeView.Nodes.Clear();
            this.analyse_treeView.Nodes.Add("Ethernet", "Ethernet");
            var ethernetPacket = (PacketDotNet.EthernetPacket)packet;
            this.analyse_treeView.Nodes["Ethernet"].Nodes.Add("SHA", "Source Hardware Address");
            this.analyse_treeView.Nodes["Ethernet"].Nodes["SHA"].Nodes.Add(ethernetPacket.SourceHardwareAddress.ToString(), ethernetPacket.SourceHardwareAddress.ToString());
            this.analyse_treeView.Nodes["Ethernet"].Nodes.Add("DHA", "Destination Hardware Address");
            this.analyse_treeView.Nodes["Ethernet"].Nodes["DHA"].Nodes.Add(ethernetPacket.DestinationHardwareAddress.ToString(), ethernetPacket.DestinationHardwareAddress.ToString());
            if (ethernetPacket.Type == PacketDotNet.EthernetType.IPv4 || ethernetPacket.Type == PacketDotNet.EthernetType.IPv6)
            {
                var ipPacket = packet.Extract<PacketDotNet.IPPacket>();


                if (ipPacket.Version == IPVersion.IPv4)
                {

                    var ipv4Packet = packet.Extract<PacketDotNet.IPv4Packet>();

                    this.analyse_treeView.Nodes.Add("IPV4", "IPV4");
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("TOS", "Type OF Service");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["TOS"].Nodes.Add(ipv4Packet.TypeOfService.ToString(), ipv4Packet.TypeOfService.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("TL", "Total Length");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["TL"].Nodes.Add(ipv4Packet.TotalLength.ToString(), ipv4Packet.TotalLength.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("ID", "Identification");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["ID"].Nodes.Add(ipv4Packet.Id.ToString(), ipv4Packet.Id.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("Flags", "Flags");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["Flags"].Nodes.Add(ipv4Packet.FragmentFlags.ToString(), ipv4Packet.FragmentFlags.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("FO", "Fragment Offset");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["FO"].Nodes.Add(ipv4Packet.FragmentOffset.ToString(), ipv4Packet.FragmentOffset.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("TTL", "Time To Live");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["TTL"].Nodes.Add(ipv4Packet.TimeToLive.ToString(), ipv4Packet.TimeToLive.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("Protocol", "Protocol");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["Protocol"].Nodes.Add(ipv4Packet.Protocol.ToString(), ipv4Packet.Protocol.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("Check_Sum", "Check Sum");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["Check_Sum"].Nodes.Add(ipv4Packet.Checksum.ToString(), ipv4Packet.Checksum.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("Source_Addresse", "Source Addresse");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["Source_Addresse"].Nodes.Add(ipv4Packet.SourceAddress.ToString(), ipv4Packet.SourceAddress.ToString());
                    this.analyse_treeView.Nodes["IPV4"].Nodes.Add("Destination_Addresse", "Destination Addresse");
                    this.analyse_treeView.Nodes["IPV4"].Nodes["Destination_Addresse"].Nodes.Add(ipv4Packet.DestinationAddress.ToString(), ipv4Packet.DestinationAddress.ToString());

                    if (ipPacket.Protocol == PacketDotNet.ProtocolType.Udp)
                    {

                        var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();
                        this.analyse_treeView.Nodes.Add("UDP", "UDP");
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Source_Port", "Source Port");
                        this.analyse_treeView.Nodes["UDP"].Nodes["Source_Port"].Nodes.Add(udpPacket.SourcePort.ToString(), udpPacket.SourcePort.ToString());
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Destination_Port", "Destionation Port");
                        this.analyse_treeView.Nodes["UDP"].Nodes["Destination_Port"].Nodes.Add(udpPacket.DestinationPort.ToString(), udpPacket.DestinationPort.ToString());
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Length", "Lenfth");
                        this.analyse_treeView.Nodes["UDP"].Nodes["length"].Nodes.Add(udpPacket.Length.ToString(), udpPacket.Length.ToString());
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Check_Sum", "Check Sum");
                        this.analyse_treeView.Nodes["UDP"].Nodes["Check_Sum"].Nodes.Add(udpPacket.Checksum.ToString(), udpPacket.Checksum.ToString());

                        if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                        {

                            var DNSPacket = new DnsPacket(udpPacket.PayloadData);
                            this.analyse_treeView.Nodes.Add("DNS", "DNS");
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("ID", "Identification");
                            this.analyse_treeView.Nodes["DNS"].Nodes["ID"].Nodes.Add(DNSPacket.ID, DNSPacket.ID);
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("QR", "QR");
                            this.analyse_treeView.Nodes["DNS"].Nodes["QR"].Nodes.Add(DNSPacket.QR.ToString(), DNSPacket.QR.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("OP_Code", "OP Code");
                            this.analyse_treeView.Nodes["DNS"].Nodes["OP_Code"].Nodes.Add(DNSPacket.OP_Code.ToString(), DNSPacket.OP_Code.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("AA", "Authoritative Answer");
                            this.analyse_treeView.Nodes["DNS"].Nodes["AA"].Nodes.Add(DNSPacket.Authoritative_Answer.ToString(), DNSPacket.Authoritative_Answer.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("TC", "Truncation");
                            this.analyse_treeView.Nodes["DNS"].Nodes["TC"].Nodes.Add(DNSPacket.Truncation.ToString(), DNSPacket.Truncation.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("RD", "Recursion Desired");
                            this.analyse_treeView.Nodes["DNS"].Nodes["RD"].Nodes.Add(DNSPacket.Recursion_Desired.ToString(), DNSPacket.Recursion_Desired.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("RA", "Recursion Available");
                            this.analyse_treeView.Nodes["DNS"].Nodes["RA"].Nodes.Add(DNSPacket.Recursion_Available.ToString(), DNSPacket.Recursion_Available.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("Zero", "Zero");
                            this.analyse_treeView.Nodes["DNS"].Nodes["Zero"].Nodes.Add(DNSPacket.Zero.ToString(), DNSPacket.Zero.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("RCode", "Response Code");
                            this.analyse_treeView.Nodes["DNS"].Nodes["RCode"].Nodes.Add(DNSPacket.Response_Code.ToString(), DNSPacket.Response_Code.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOQ", "Number of Questions");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOQ"].Nodes.Add(DNSPacket.Number_of_Questions.ToString(), DNSPacket.Number_of_Questions.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOAn", "Number of Answer");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOAn"].Nodes.Add(DNSPacket.Number_of_Answer.ToString(), DNSPacket.Number_of_Answer.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOAu", "Number of Authority");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOAu"].Nodes.Add(DNSPacket.Number_of_Authority.ToString(), DNSPacket.Number_of_Authority.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOAd", "Number of Additional");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOAd"].Nodes.Add(DNSPacket.Number_of_Additional.ToString(), DNSPacket.Number_of_Additional.ToString());


                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("Info", "DNS DATA");
                            for (int i = 0; i < DNSPacket.DNS_INFO.Count; i++)
                            {

                                IDNSINFO info = DNSPacket.DNS_INFO[i];
                                if (info.DNSType == DNSType.Question)
                                {
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes.Add(i.ToString(), DNSPacket.DNS_INFO[i].DNSType.ToString() + " " + i.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Name", "Name");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Name"].Nodes.Add(((Question)DNSPacket.DNS_INFO[i]).Name, ((Question)DNSPacket.DNS_INFO[i]).Name);
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Type", "Type");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Type"].Nodes.Add(((Question)DNSPacket.DNS_INFO[i]).Type.ToString(), ((Question)DNSPacket.DNS_INFO[i]).Type.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Class", "Class");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Class"].Nodes.Add(((Question)DNSPacket.DNS_INFO[i]).Class.ToString(), ((Question)DNSPacket.DNS_INFO[i]).Class.ToString());

                                }
                                else if (info.DNSType == DNSType.ResourceRecords)
                                {

                                    ResourceRecords resourceRecords = (ResourceRecords)info;

                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes.Add(i.ToString(), DNSPacket.DNS_INFO[i].DNSType.ToString() + " " + i.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Name", "Name");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Name"].Nodes.Add(((ResourceRecords)DNSPacket.DNS_INFO[i]).Name, ((ResourceRecords)DNSPacket.DNS_INFO[i]).Name);
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Type", "Type");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Type"].Nodes.Add(((ResourceDataType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Type)).ToString(), ((ResourceDataType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Type)).ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Class", "Class");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Class"].Nodes.Add(((ClassType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Class)).ToString(), ((ClassType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Class)).ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("TTL", "TTL");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["TTL"].Nodes.Add(((ResourceRecords)DNSPacket.DNS_INFO[i]).TTL.ToString(), ((ResourceRecords)DNSPacket.DNS_INFO[i]).Class.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Data", "Data");

                                    switch (resourceRecords.RData.Type)
                                    {
                                        case ResourceDataType.A:
                                            ResourceDataTypeA resourceDataTypeA = ((ResourceDataTypeA)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("IPV4", "IPV4");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["IPV4"].Nodes.Add(resourceDataTypeA.IPV4, resourceDataTypeA.IPV4);

                                            break;
                                        case ResourceDataType.AAAA:
                                            ResourceDataTypeAAAA resourceDataTypeAAAA = (ResourceDataTypeAAAA)resourceRecords.RData;
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("IPV6", "IPV6");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["IPV6"].Nodes.Add(resourceDataTypeAAAA.IPV6, resourceDataTypeAAAA.IPV6);
                                            break;
                                        case ResourceDataType.MX:
                                            ResourceDataTypeMX ResourceDataTypeMX = ((ResourceDataTypeMX)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Preference", "Preference");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Preference"].Nodes.Add(ResourceDataTypeMX.Preference.ToString(), ResourceDataTypeMX.Preference.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Mail", "Mail");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Mail"].Nodes.Add(ResourceDataTypeMX.mail, ResourceDataTypeMX.mail);
                                            break;
                                        case ResourceDataType.CNAME:
                                            ResourceDataTypeCNAME ResourceDataTypeCNAME = ((ResourceDataTypeCNAME)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypeCNAME.DomainName, ResourceDataTypeCNAME.DomainName);
                                            break;
                                        case ResourceDataType.NS:
                                            ResourceDataTypeNS ResourceDataTypeNS = ((ResourceDataTypeNS)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypeNS.DomainName, ResourceDataTypeNS.DomainName);
                                            break;
                                        case ResourceDataType.PTR:
                                            ResourceDataTypePTR ResourceDataTypePTR = ((ResourceDataTypePTR)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypePTR.DomainName, ResourceDataTypePTR.DomainName);
                                            break;
                                        case ResourceDataType.SOA:
                                            ResourceDataTypeSOA ResourceDataTypeSOA = ((ResourceDataTypeSOA)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypeSOA.DomainName, ResourceDataTypeSOA.DomainName);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Mail", "Mail");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Mail"].Nodes.Add(ResourceDataTypeSOA.mail, ResourceDataTypeSOA.mail);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Serial", "Serial");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Serial"].Nodes.Add(ResourceDataTypeSOA.serial.ToString(), ResourceDataTypeSOA.serial.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Refresh", "Refresh");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Refresh"].Nodes.Add(ResourceDataTypeSOA.Refresh.ToString(), ResourceDataTypeSOA.Refresh.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Retry", "Retry");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Retry"].Nodes.Add(ResourceDataTypeSOA.Retry.ToString(), ResourceDataTypeSOA.Retry.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("TTL", "TTL");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["TTL"].Nodes.Add(ResourceDataTypeSOA.TTL.ToString(), ResourceDataTypeSOA.TTL.ToString());
                                            break;
                                        default:
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("UNDEFINE", "UNDEFINE");
                                            break;

                                    }


                                }

                            }

                        }

                    }


                }

                if (ipPacket.Version == IPVersion.IPv6)
                {

                    var ipv6Packet = packet.Extract<PacketDotNet.IPv6Packet>();
                    this.analyse_treeView.Nodes.Add("IPV6", "IPV6");
                    this.analyse_treeView.Nodes["IPV6"].Nodes.Add("Tc", "Traffic Class");
                    this.analyse_treeView.Nodes["IPV6"].Nodes["TC"].Nodes.Add(ipv6Packet.TrafficClass.ToString(), ipv6Packet.TrafficClass.ToString());
                    this.analyse_treeView.Nodes["IPV6"].Nodes.Add("FL", "Flow Label");
                    this.analyse_treeView.Nodes["IPV6"].Nodes["FL"].Nodes.Add(ipv6Packet.FlowLabel.ToString(), ipv6Packet.FlowLabel.ToString());
                    this.analyse_treeView.Nodes["IPV6"].Nodes.Add("PL", "Payload Length");
                    this.analyse_treeView.Nodes["IPV6"].Nodes["PL"].Nodes.Add(ipv6Packet.PayloadLength.ToString(), ipv6Packet.PayloadLength.ToString());
                    this.analyse_treeView.Nodes["IPV6"].Nodes.Add("NH", "Next Header");
                    this.analyse_treeView.Nodes["IPV6"].Nodes["NH"].Nodes.Add(ipv6Packet.NextHeader.ToString(), ipv6Packet.NextHeader.ToString());
                    this.analyse_treeView.Nodes["IPV6"].Nodes.Add("HL", "Hope Limit");
                    this.analyse_treeView.Nodes["IPV6"].Nodes["HL"].Nodes.Add(ipv6Packet.HopLimit.ToString(), ipv6Packet.HopLimit.ToString());
                    this.analyse_treeView.Nodes["IPV6"].Nodes.Add("SA", "Source Addresse");
                    this.analyse_treeView.Nodes["IPV6"].Nodes["SA"].Nodes.Add(ipv6Packet.SourceAddress.ToString(), ipv6Packet.SourceAddress.ToString());
                    this.analyse_treeView.Nodes["IPV6"].Nodes.Add("DA", "Destination Addresse");
                    this.analyse_treeView.Nodes["IPV6"].Nodes["DA"].Nodes.Add(ipv6Packet.DestinationAddress.ToString(), ipv6Packet.DestinationAddress.ToString());

                    if (ipPacket.Protocol == PacketDotNet.ProtocolType.Udp)
                    {

                        var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();
                        this.analyse_treeView.Nodes.Add("UDP", "UDP");
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Source_Port", "Source Port");
                        this.analyse_treeView.Nodes["UDP"].Nodes["Source_Port"].Nodes.Add(udpPacket.SourcePort.ToString(), udpPacket.SourcePort.ToString());
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Destination_Port", "Destionation Port");
                        this.analyse_treeView.Nodes["UDP"].Nodes["Destination_Port"].Nodes.Add(udpPacket.DestinationPort.ToString(), udpPacket.DestinationPort.ToString());
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Length", "Lenfth");
                        this.analyse_treeView.Nodes["UDP"].Nodes["length"].Nodes.Add(udpPacket.Length.ToString(), udpPacket.Length.ToString());
                        this.analyse_treeView.Nodes["UDP"].Nodes.Add("Check_Sum", "Check Sum");
                        this.analyse_treeView.Nodes["UDP"].Nodes["Check_Sum"].Nodes.Add(udpPacket.Checksum.ToString(), udpPacket.Checksum.ToString());

                        if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                        {

                            var DNSPacket = new DnsPacket(udpPacket.PayloadData);
                            this.analyse_treeView.Nodes.Add("DNS", "DNS");
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("ID", "Identification");
                            this.analyse_treeView.Nodes["DNS"].Nodes["ID"].Nodes.Add(DNSPacket.ID, DNSPacket.ID);
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("QR", "QR");
                            this.analyse_treeView.Nodes["DNS"].Nodes["QR"].Nodes.Add(DNSPacket.QR.ToString(), DNSPacket.QR.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("OP_Code", "OP Code");
                            this.analyse_treeView.Nodes["DNS"].Nodes["OP_Code"].Nodes.Add(DNSPacket.OP_Code.ToString(), DNSPacket.OP_Code.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("AA", "Authoritative Answer");
                            this.analyse_treeView.Nodes["DNS"].Nodes["AA"].Nodes.Add(DNSPacket.Authoritative_Answer.ToString(), DNSPacket.Authoritative_Answer.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("TC", "Truncation");
                            this.analyse_treeView.Nodes["DNS"].Nodes["TC"].Nodes.Add(DNSPacket.Truncation.ToString(), DNSPacket.Truncation.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("RD", "Recursion Desired");
                            this.analyse_treeView.Nodes["DNS"].Nodes["RD"].Nodes.Add(DNSPacket.Recursion_Desired.ToString(), DNSPacket.Recursion_Desired.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("RA", "Recursion Available");
                            this.analyse_treeView.Nodes["DNS"].Nodes["RA"].Nodes.Add(DNSPacket.Recursion_Available.ToString(), DNSPacket.Recursion_Available.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("Zero", "Zero");
                            this.analyse_treeView.Nodes["DNS"].Nodes["Zero"].Nodes.Add(DNSPacket.Zero.ToString(), DNSPacket.Zero.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("RCode", "Response Code");
                            this.analyse_treeView.Nodes["DNS"].Nodes["RCode"].Nodes.Add(DNSPacket.Response_Code.ToString(), DNSPacket.Response_Code.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOQ", "Number of Questions");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOQ"].Nodes.Add(DNSPacket.Number_of_Questions.ToString(), DNSPacket.Number_of_Questions.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOAn", "Number of Answer");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOAn"].Nodes.Add(DNSPacket.Number_of_Answer.ToString(), DNSPacket.Number_of_Answer.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOAu", "Number of Authority");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOAu"].Nodes.Add(DNSPacket.Number_of_Authority.ToString(), DNSPacket.Number_of_Authority.ToString());
                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("NOAd", "Number of Additional");
                            this.analyse_treeView.Nodes["DNS"].Nodes["NOAd"].Nodes.Add(DNSPacket.Number_of_Additional.ToString(), DNSPacket.Number_of_Additional.ToString());

                            this.analyse_treeView.Nodes["DNS"].Nodes.Add("Info", "DNS DATA");
                            for (int i = 0; i < DNSPacket.DNS_INFO.Count; i++)
                            {

                                IDNSINFO info = DNSPacket.DNS_INFO[i];
                                if (info.DNSType == DNSType.Question)
                                {
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes.Add(i.ToString(), DNSPacket.DNS_INFO[i].DNSType.ToString() + " " + i.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Name", "Name");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Name"].Nodes.Add(((Question)DNSPacket.DNS_INFO[i]).Name, ((Question)DNSPacket.DNS_INFO[i]).Name);
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Type", "Type");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Type"].Nodes.Add(((Question)DNSPacket.DNS_INFO[i]).Type.ToString(), ((Question)DNSPacket.DNS_INFO[i]).Type.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Class", "Class");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Class"].Nodes.Add(((Question)DNSPacket.DNS_INFO[i]).Class.ToString(), ((Question)DNSPacket.DNS_INFO[i]).Class.ToString());

                                }
                                else if (info.DNSType == DNSType.ResourceRecords)
                                {

                                    ResourceRecords resourceRecords = (ResourceRecords)info;

                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes.Add(i.ToString(), DNSPacket.DNS_INFO[i].DNSType.ToString() + " " + i.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Name", "Name");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Name"].Nodes.Add(((ResourceRecords)DNSPacket.DNS_INFO[i]).Name, ((ResourceRecords)DNSPacket.DNS_INFO[i]).Name);
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Type", "Type");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Type"].Nodes.Add(((ResourceDataType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Type)).ToString(), ((ResourceDataType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Type)).ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Class", "Class");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Class"].Nodes.Add(((ClassType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Class)).ToString(), ((ClassType)int.Parse(((ResourceRecords)DNSPacket.DNS_INFO[i]).Class)).ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("TTL", "TTL");
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["TTL"].Nodes.Add(((ResourceRecords)DNSPacket.DNS_INFO[i]).TTL.ToString(), ((ResourceRecords)DNSPacket.DNS_INFO[i]).Class.ToString());
                                    this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("Data", "Data");

                                    switch (resourceRecords.RData.Type)
                                    {
                                        case ResourceDataType.A:
                                            ResourceDataTypeA resourceDataTypeA = ((ResourceDataTypeA)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("IPV4", "IPV4");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["IPV4"].Nodes.Add(resourceDataTypeA.IPV4, resourceDataTypeA.IPV4);

                                            break;
                                        case ResourceDataType.AAAA:
                                            ResourceDataTypeAAAA resourceDataTypeAAAA = (ResourceDataTypeAAAA)resourceRecords.RData;
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("IPV6", "IPV6");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["IPV6"].Nodes.Add(resourceDataTypeAAAA.IPV6, resourceDataTypeAAAA.IPV6);
                                            break;
                                        case ResourceDataType.MX:
                                            ResourceDataTypeMX ResourceDataTypeMX = ((ResourceDataTypeMX)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Preference", "Preference");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Preference"].Nodes.Add(ResourceDataTypeMX.Preference.ToString(), ResourceDataTypeMX.Preference.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Mail", "Mail");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Mail"].Nodes.Add(ResourceDataTypeMX.mail, ResourceDataTypeMX.mail);
                                            break;
                                        case ResourceDataType.CNAME:
                                            ResourceDataTypeCNAME ResourceDataTypeCNAME = ((ResourceDataTypeCNAME)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypeCNAME.DomainName, ResourceDataTypeCNAME.DomainName);
                                            break;
                                        case ResourceDataType.NS:
                                            ResourceDataTypeNS ResourceDataTypeNS = ((ResourceDataTypeNS)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypeNS.DomainName, ResourceDataTypeNS.DomainName);
                                            break;
                                        case ResourceDataType.PTR:
                                            ResourceDataTypePTR ResourceDataTypePTR = ((ResourceDataTypePTR)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypePTR.DomainName, ResourceDataTypePTR.DomainName);
                                            break;
                                        case ResourceDataType.SOA:
                                            ResourceDataTypeSOA ResourceDataTypeSOA = ((ResourceDataTypeSOA)resourceRecords.RData);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("DomainName", "DomainName");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["DomainName"].Nodes.Add(ResourceDataTypeSOA.DomainName, ResourceDataTypeSOA.DomainName);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Mail", "Mail");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Mail"].Nodes.Add(ResourceDataTypeSOA.mail, ResourceDataTypeSOA.mail);
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Serial", "Serial");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Serial"].Nodes.Add(ResourceDataTypeSOA.serial.ToString(), ResourceDataTypeSOA.serial.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Refresh", "Refresh");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Refresh"].Nodes.Add(ResourceDataTypeSOA.Refresh.ToString(), ResourceDataTypeSOA.Refresh.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("Retry", "Retry");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["Retry"].Nodes.Add(ResourceDataTypeSOA.Retry.ToString(), ResourceDataTypeSOA.Retry.ToString());
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes.Add("TTL", "TTL");
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes["Data"].Nodes["TTL"].Nodes.Add(ResourceDataTypeSOA.TTL.ToString(), ResourceDataTypeSOA.TTL.ToString());
                                            break;
                                        default:
                                            this.analyse_treeView.Nodes["DNS"].Nodes["Info"].Nodes[i.ToString()].Nodes.Add("UNDEFINE", "UNDEFINE");
                                            break;

                                    }


                                }

                            }
                        }

                    }
                }

            }

        }

        void GetDomainName(List<IDNSINFO> dNSINFOs, out string Domainname)
        {

            Domainname = string.Empty;
            foreach (var info in dNSINFOs)
                Domainname += (string.IsNullOrEmpty(info.Name) ? "UNDEFINE" : info.Name) + " ,";

            Domainname.TrimEnd(',');

            string[] DataArr = Domainname.Split(',');

            bool containDomain = false;

            foreach (string domain in DataArr)
            {

                if (!Domainname.Contains("UNDEFINE") || !Domainname.Contains(string.Empty))
                {

                    containDomain = true;
                    break;

                }

            }
            if (containDomain)
                Domainname = Domainname.Replace("UNDEFINE", string.Empty);

            Domainname = FilterRepeatedStr(Domainname);


        }
        void GetDnsData(List<IDNSINFO> dNSINFOs, out string Data)
        {

            Data = string.Empty;
            foreach (var info in dNSINFOs)
            {

                if (info.DNSType == DNSType.ResourceRecords)
                {

                    ResourceRecords resourceRecords = (ResourceRecords)info;

                    IResourceData resourceData;


                    switch (resourceRecords.RData.Type)
                    {
                        case ResourceDataType.A:
                            Data += ((ResourceDataTypeA)resourceRecords.RData).IPV4 + " , ";
                            break;
                        case ResourceDataType.AAAA:
                            Data += ((ResourceDataTypeAAAA)resourceRecords.RData).IPV6 + " , ";
                            break;
                        case ResourceDataType.MX:
                            Data += ((ResourceDataTypeMX)resourceRecords.RData).Preference.ToString() + " , " + ((ResourceDataTypeMX)resourceRecords.RData).mail + " ,";
                            break;
                        case ResourceDataType.CNAME:
                            Data += ((ResourceDataTypeCNAME)resourceRecords.RData).DomainName + " , ";
                            break;
                        case ResourceDataType.NS:
                            Data += ((ResourceDataTypeNS)resourceRecords.RData).DomainName + " , ";
                            break;
                        case ResourceDataType.PTR:
                            Data += ((ResourceDataTypePTR)resourceRecords.RData).DomainName + " , ";
                            break;
                        case ResourceDataType.SOA:
                            Data += ((ResourceDataTypeSOA)resourceRecords.RData).DomainName + " , ";
                            break;
                        default:
                            Data += "UNDEFINE" + " , ";
                            break;

                    }

                }
                else
                {

                    Data += "UNDEFINE" + " , ";
                }

            }


            Data.TrimEnd(',');


            string[] DataArr = Data.Split(',');

            bool containData = false;

            foreach (string Datastr in DataArr)
            {

                if (!Datastr.Contains("UNDEFINE") || !Datastr.Contains(string.Empty))
                {

                    containData = true;
                    break;

                }

            }
            if (containData)
                Data = Data.Replace("UNDEFINE", string.Empty);

            Data = FilterRepeatedStr(Data);

        }
        string FilterRepeatedStr(string target)
        {

            string[] DataArr = target.Split(',');

            return String.Join(",", DataArr.ToList()
                  .GroupBy(x => x)
                  .Select(x => x.First())
                  .ToArray());

        }
        public void SafeInvoke(Control uiElement, Action updater)
        {
            if (uiElement == null)
            {
                throw new ArgumentNullException("uiElement");
            }

            if (uiElement.InvokeRequired)
            {


                uiElement.BeginInvoke((Action)delegate { SafeInvoke(uiElement, updater); });

            }
            else
            {
                if (uiElement.IsDisposed)
                {
                    throw new ObjectDisposedException("Control is already disposed.");
                }

                updater();
            }
        }
        void Adapter_off()
        {

            try
            {
                if (this.Adapter != null)
                {
                    if (this.Adapter.Opened)
                    {
                        if ((bool)spoofarp_toggle.Tag)
                        {

                            ArpSpoofer.StopAsync();
                        }
                        this.Adapter.StopCapture();
                        this.Adapter.Close();

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }


        Color GenerateRandomColor()
        {
            Color randomColor;
            do
            {
                Random rnd = new Random();
                randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            } while (DNS_ID_Color.Select(x => x.color).ToList().Contains(randomColor));

            return randomColor;
        }
        #endregion

        #region --- Events ---------------------------
        void Adapter_OnPacketArrival(object s, PacketCapture e)
        {
           
            var rawCapture = e.GetPacket();

            var packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);

            if (packet != null)
            {
                var ethernetPacket = (PacketDotNet.EthernetPacket)packet;

                if (ethernetPacket.Type == PacketDotNet.EthernetType.IPv4 || ethernetPacket.Type == PacketDotNet.EthernetType.IPv6)
                {

                    var ipPacket = packet.Extract<PacketDotNet.IPPacket>();

                    if (ipPacket != null)
                    {
                        //IpV4
                        if (ipPacket.Version == IPVersion.IPv4)
                        {
                            if (IP_V4_radioButton.Checked || both.Checked)
                            {
                                ipPacket = packet.Extract<PacketDotNet.IPv4Packet>();

                                if (!string.IsNullOrEmpty(this.ActiveDevice.IPV4Adresse))
                                {
                                    if (ipPacket.SourceAddress.ToString() == ActiveDevice.IPV4Adresse || ipPacket.DestinationAddress.ToString() == ActiveDevice.IPV4Adresse)
                                    {


                                        if (ipPacket.Protocol == PacketDotNet.ProtocolType.Udp)
                                        {

                                            var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                                            if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                                            {

                                                DnsPacket DnsPacket = new DnsPacket(udpPacket.PayloadData);



                                                if (DnsPacket.QR == DnsFieldsEnums.QR_Type.request)
                                                {
                                                    SafeInvoke(this.packets_dgv, new Action(() =>
                                                    {

                                                        string domainname;
                                                        GetDomainName(DnsPacket.DNS_INFO, out domainname);
                                                        string Data;
                                                        GetDnsData(DnsPacket.DNS_INFO, out Data);
                                                        domainname = domainname.Substring(0, Math.Min(domainname.Length, 75));
                                                        domainname = domainname.Length < 75 ? domainname : domainname + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        Data = Data.Substring(0, Math.Min(Data.Length, 75));
                                                        Data = Data.Length < 75 ? Data : Data + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        List<DNS_ID> DNS_IDs = DNS_ID_Color.Where(x => (x.Id == DnsPacket.ID)).ToList();
                                                        if (DNS_IDs.Any())
                                                        {
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_herunterladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;
                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Tag = packet;
                                                        }
                                                        else
                                                        {
                                                            this.DNS_ID_Color.Add(new DNS_ID(DnsPacket.ID, GenerateRandomColor()));
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_herunterladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;

                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount-1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount-1].Tag = packet;
                                                        }
                                                     

                                                    }));

                                                }
                                                else if (DnsPacket.QR == DnsFieldsEnums.QR_Type.response)
                                                {

                                                    SafeInvoke(this.packets_dgv, new Action(() =>
                                                    {
                                                        string domainname;
                                                        GetDomainName(DnsPacket.DNS_INFO, out domainname);
                                                        string Data;
                                                        GetDnsData(DnsPacket.DNS_INFO, out Data);
                                                        domainname = domainname.Substring(0, Math.Min(domainname.Length, 75));
                                                        domainname = domainname.Length < 75 ? domainname : domainname + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        Data = Data.Substring(0, Math.Min(Data.Length, 75));
                                                        Data = Data.Length < 75 ? Data : Data + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        List<DNS_ID> DNS_IDs = DNS_ID_Color.Where(x => (x.Id == DnsPacket.ID)).ToList();
                                                        if (DNS_IDs.Any())
                                                        {
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_hochladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;
                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Tag = packet;
                                                        }
                                                        else
                                                        {
                                                            this.DNS_ID_Color.Add(new DNS_ID(DnsPacket.ID, GenerateRandomColor()));
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_hochladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;

                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Tag = packet;
                                                        }
                                                       

                                                    }));
                                                }


                                            }
                                        }
                                    }
                                }
                            }

                        }
                        //IpV6
                        
                        else if (ipPacket.Version == IPVersion.IPv6)
                        {
                            if (IP_V6_radioButton.Checked || both.Checked)
                            { 
                            ipPacket = packet.Extract<PacketDotNet.IPv6Packet>();

                            if (this.ActiveDevice.IPV6Adresse.Any())
                            {
                                if (this.ActiveDevice.IPV6Adresse.Contains(ipPacket.SourceAddress.ToString()) || this.ActiveDevice.IPV6Adresse.Contains(ipPacket.DestinationAddress.ToString()))
                                {

                                    if (ipPacket.Protocol == PacketDotNet.ProtocolType.Udp)
                                    {

                                        var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                                            if (udpPacket.SourcePort == 53 || udpPacket.DestinationPort == 53)
                                            {

                                                DnsPacket DnsPacket = new DnsPacket(udpPacket.PayloadData);

                                                if (DnsPacket.QR == DnsFieldsEnums.QR_Type.request)
                                                {
                                                    SafeInvoke(this.packets_dgv, new Action(() =>
                                                    {

                                                        string domainname;
                                                        GetDomainName(DnsPacket.DNS_INFO, out domainname);
                                                        string Data;
                                                        GetDnsData(DnsPacket.DNS_INFO, out Data);
                                                        domainname = domainname.Substring(0, Math.Min(domainname.Length, 75));
                                                        domainname = domainname.Length < 75 ? domainname : domainname + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        Data = Data.Substring(0, Math.Min(Data.Length, 75));
                                                        Data = Data.Length < 75 ? Data : Data + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        List<DNS_ID> DNS_IDs = DNS_ID_Color.Where(x => (x.Id == DnsPacket.ID)).ToList();
                                                        if (DNS_IDs.Any())
                                                        {
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_herunterladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;
                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Tag = packet;
                                                        }
                                                        else
                                                        {
                                                            this.DNS_ID_Color.Add(new DNS_ID(DnsPacket.ID, GenerateRandomColor()));
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_herunterladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;

                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Tag = packet;
                                                        }
                                                      

                                                    }));

                                                }
                                                else if (DnsPacket.QR == DnsFieldsEnums.QR_Type.response)
                                                {

                                                    SafeInvoke(this.packets_dgv, new Action(() =>
                                                    {
                                                        string domainname;
                                                        GetDomainName(DnsPacket.DNS_INFO, out domainname);
                                                        string Data;
                                                        GetDnsData(DnsPacket.DNS_INFO, out Data);
                                                        domainname = domainname.Substring(0, Math.Min(domainname.Length, 75));
                                                        domainname = domainname.Length < 75 ? domainname : domainname + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        Data = Data.Substring(0, Math.Min(Data.Length, 75));
                                                        Data = Data.Length < 75 ? Data : Data + "...";
                                                        Data = string.IsNullOrEmpty(Data) ? "UNDEFINE" : Data;
                                                        List<DNS_ID> DNS_IDs = DNS_ID_Color.Where(x => (x.Id == DnsPacket.ID)).ToList();
                                                        if (DNS_IDs.Any())
                                                        {
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_hochladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;
                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Tag = packet;
                                                        }
                                                        else
                                                        {
                                                            this.DNS_ID_Color.Add(new DNS_ID(DnsPacket.ID, GenerateRandomColor()));
                                                            Image image3 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_analyse_des_finanziellen_wachstums_32;
                                                            Image image2 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_hochladen_32;
                                                            Image image1 = global::NetworkScannerAndSniffer.Properties.Resources.icons8_internet_30;

                                                            packets_dgv.Rows.Add(image1, domainname, Data, this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].Id, image2, image3);
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Cells[3].Style.BackColor = this.DNS_ID_Color[this.DNS_ID_Color.Count - 1].color;
                                                            packets_dgv.Rows[packets_dgv.RowCount - 1].Tag = packet;
                                                        }
                                                        

                                                    }));
                                                }


                                            }
                                        }
                                    }

                                }
                            }
                        }
                        
                    }
                }
            }

        }

      
        private void NetworkSniffer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Adapter_off();
        }

        private void adapter_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            Adapter = LibPcapLiveDeviceList.Instance[(this.adapter_comboBox.SelectedItem as ComboboxItem).Index];
           
        }

        private void spoofarp_toggle_Click(object sender, EventArgs e)
        {

            if (!((bool)this.spoofarp_toggle.Tag))
            {

                this.spoofarp_toggle.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_schalter_an_40;
                this.spoofarp_toggle.Tag = true;

            }
            else
            {

                this.spoofarp_toggle.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_schalter_aus_40;
                this.spoofarp_toggle.Tag = false;

            }


        }

        private void packets_dgv_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (packets_dgv.Rows.Count > 0 && e.RowIndex >= 0)
            {
                try
                {
                    packets_dgv.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.LightGreen;
                    packets_dgv.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.LightGreen;
                    packets_dgv.Rows[e.RowIndex].Cells[2].Style.BackColor = Color.LightGreen;
                    packets_dgv.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.LightGreen;
                    packets_dgv.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.LightGreen;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void packets_dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (packets_dgv.Rows.Count > 0 && e.RowIndex >= 0)
            {
                try
                {
                    packets_dgv.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.White;
                    packets_dgv.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.White;
                    packets_dgv.Rows[e.RowIndex].Cells[2].Style.BackColor = Color.White;
                    packets_dgv.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.White;
                    packets_dgv.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.White;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void packets_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if ( e.RowIndex >= 0)
            {

                

                Packet packet = (Packet)packets_dgv.Rows[e.RowIndex].Tag;

                analyse_textBox.Text = (packet.PrintHex() + Environment.NewLine); 
                 getPacketInfoINTreeView(packet);

            }


        }

        private void packets_dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void choose_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (choose_rb.Checked)
            {

                this.gatways_compobox.Enabled = true;
                this.Gatway_tb.Text = String.Empty;
                this.Gatway_tb.Enabled = false;
                this.loading_btn.Visible = false;
                if (Gatways != null && Gatways.Any())
                {
                    gatways_compobox.SelectedIndex = 0;
                    this.Gatway = this.Gatways[0];                                        
                }

            }
            else
            {

                this.gatways_compobox.Enabled = false;
                this.Gatway_tb.Enabled = true;
                this.loading_btn.Image = global::NetworkScannerAndSniffer.Properties.Resources.Rolling_1s_16px;
                this.loading_btn.Visible = true;


            }
        }

        private void Gatway_tb_TextChanged(object sender, EventArgs e)
        {
            if (!GetGatwayBGW.IsBusy)
            {
                GetGatwayBGW.RunWorkerAsync();
                this.loading_btn.Image = global::NetworkScannerAndSniffer.Properties.Resources.Rolling_1s_16px;

            }
        }

        private void GetGatwayBGW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while(true && Gatway_tb.Enabled)
            try
            {

                ActiveDevice activeDevice;

                IPAddress.Parse(Gatway_tb.Text);
                this.status.Text = "Getting Gatway MAC Address form the Giveing IP-v4 Address";
                string mac = DeviceInfo.getMACAddresse(Gatway_tb.Text);
                if (mac == "not detected")
                {
                    this.status.Text = "Can´t Get the MAC Address form the Target Gatway";
                    throw new Exception("Can´t Get the MAC Address form the Target Gatway");
                }
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Gatway_tb.Text);
                string host = ipEntry.HostName;
                List<string> foundIPs = new List<string>();
                foreach (IPAddress iPAddress in ipEntry.AddressList)
                {
                    if (iPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        foundIPs.Add(iPAddress.ToString());

                    }

                }

                this.Gatway = new ActiveDevice(Gatway_tb.Text, foundIPs, mac, host);
                this.loading_btn.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_check_mark_16;
                this.status.Text = "successfully got the mac address";
                break;

            }
            catch (Exception ex)
            {

            }
        }

        private void gatways_compobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Gatways != null && Gatways.Any())
            {

                gatways_compobox.SelectedIndex = gatways_compobox.SelectedIndex ;

                this.Gatway = this.Gatways[gatways_compobox.SelectedIndex];
            }
        }

        #endregion
   
    
    }


}
