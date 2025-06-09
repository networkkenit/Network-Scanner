using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
namespace NetworkScannerAndSniffer
{
    public partial class NetworkScanner : Form
    {

        #region --- Constructor ------------------------------
        public NetworkScanner()
        {
            InitializeComponent();
            GetLocalIPAddress();

        }
        #endregion
     
        #region --- class info ------------------------------
        int PingCompletedCount = 0;

        List<Thread> Ping_Threads = new List<Thread>();
        #endregion
     
        #region --- Properties -------------------------------
        List<string> AllGatwas { 
        
            get
            {
              List<string> list = new List<string>();

                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                foreach (GatewayIPAddressInformation gw in ni.GetIPProperties().GatewayAddresses)
                    if (gw.Address != null)
                        if (gw.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                list.Add(gw.Address.ToString());

                return list;
            }
        
        }
        List<ActiveDevice> Gatways = new List<ActiveDevice>();


        string IP
        {
            get
            {

                if (Enter_ip_radioButton.Checked)
                    return Ip_textBox.Text;
                else
                    if (Ip_comboBox.SelectedIndex != -1)
                    return (Ip_comboBox.SelectedItem.ToString());
                else return string.Empty;
            }
        }

        #endregion  
     
        #region ---  Methods --------------------------
         void GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {

                    this.Ip_comboBox.Items.Add(ip.ToString());

                }
            }

        }

         string getSubnetMask(string mask_bits)
        {

            string subnetmaskstr = string.Empty;
            int len = mask_bits.Length / 8;

            int currentpies = 0;
            for (int i = 0; i < len; i++)
            {

                string targetstr = mask_bits.Substring(currentpies, 8);

                string result = Convert.ToInt32(targetstr, 2).ToString();

                if (i == len - 1)
                {

                    if (result == string.Empty)
                        subnetmaskstr += "0";
                    else subnetmaskstr += result;

                }

                else
                {

                    if (result == string.Empty)
                        subnetmaskstr += "0.";
                    else subnetmaskstr += result + ".";
                }
                this.mask_textBox.Text = subnetmaskstr;
                currentpies += 8;

            }
            return subnetmaskstr;
        }
       
      public enum IPClass { A, B, C, D, E, notDetected }

        IPClass getIPClass(string IP)
        {

            if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
            {

                string ipclassstr = IP.Split('.').First();

                int ipclasssnum = int.Parse(ipclassstr);

                if (0 <= ipclasssnum && ipclasssnum <= 126)
                {

                    return IPClass.A;

                }

                if (128 <= ipclasssnum && ipclasssnum <= 191)
                {

                    return IPClass.B;

                }

                if (192 <= ipclasssnum && ipclasssnum <= 223)
                {

                    return IPClass.C;

                }

                if (224 <= ipclasssnum && ipclasssnum <= 239)
                {

                    return IPClass.D;

                }
                if (240 <= ipclasssnum && ipclasssnum <= 255)
                {

                    return IPClass.E;

                }

            }
            else return IPClass.notDetected;

            return IPClass.notDetected;
        }

        int getSubnetMaskBitsCount(string ip)
        {

            IPClass iPClass = getIPClass(ip);

            if (iPClass == IPClass.A)
                return 8;
            if (iPClass == IPClass.B)
                return 16;
            if (iPClass == IPClass.C)
                return 24;
            if (iPClass == IPClass.D)
                return 31;
            if (iPClass == IPClass.E)
                return 32;

            return -1;

        }

         string getNetID(string ipadresse, string subnetmask)
        {
            StringBuilder sb = new StringBuilder();

            string[] subMaskValues = subnetmask.Split('.');

            string[] subIpValues = ipadresse.Split('.');

            for (int i = 0; i < subMaskValues.Length; i++)
            {
                if (i == subMaskValues.Length - 1)
                    sb.Append((int.Parse(subMaskValues[i]) & int.Parse(subIpValues[i])).ToString());
                else
                {
                    int maskvalue = int.Parse(subMaskValues[i]);
                    int ipvalue = int.Parse(subIpValues[i]);
                    string str = ((maskvalue & ipvalue).ToString());
                    str += ".";
                    sb.Append(str);
                }

            }
            return sb.ToString();
        }

       int getHostcount(int Current_Subnetmask_Bits_Count)
        {

            double resultbits = Math.Max(Math.Pow(2, (32 - (Current_Subnetmask_Bits_Count))) - 2, 0);

            return (int)resultbits;
        }

        int getNetcount(int IP_CLass_Standart_Subnetmask_Bits_Count, int Current_Subnetmask_Bits_Count)
        {

            double resultbits = Math.Max(
                                 Math.Pow(
                                  2, Current_Subnetmask_Bits_Count -
                                     IP_CLass_Standart_Subnetmask_Bits_Count),
                                     0);

            return (int)resultbits;
        }

         string getBroadcastAdresse(string ipadresse, int standard_subnet_mask_bits_count, int current_subnet_mask_bits_count, string current_subnet_mask_bits)
        {

            int hostlength = 32 - current_subnet_mask_bits_count;

            int subnet_length = current_subnet_mask_bits_count - standard_subnet_mask_bits_count;

            string hostpart = current_subnet_mask_bits.Substring(current_subnet_mask_bits_count, hostlength);

            string ip_bits = ip_to_bit_string(ipadresse);

            string net_id_plus_host = current_subnet_mask_bits.Substring(standard_subnet_mask_bits_count, hostlength).Replace('0', '1');

            if (subnet_length > 0)
            {

                //   string net_id_part = current_subnet_mask_bits.Substring(standard_subnet_mask_bits_count, net_id_length).PadRight(net_id_length + hostlength, '0');
                string subnet_part = current_subnet_mask_bits.Substring(standard_subnet_mask_bits_count, subnet_length);

                string to_operat_ip_bits = ip_bits.Substring(standard_subnet_mask_bits_count, subnet_length);

                //  var And_operation_result = Convert.ToByte(to_operat_ip_bits, 2) & Convert.ToByte(subnet_part, 2);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < subnet_part.Length; i++)
                    sb.Append(Convert.ToUInt64(to_operat_ip_bits[i].ToString(), 2) & Convert.ToUInt64(subnet_part[i].ToString(), 2));

                for (int i = 0; i < hostpart.Length; i++)
                    sb.Append("1");

                net_id_plus_host = sb.ToString();
            }



            //*****************
            int IPPartsLength = net_id_plus_host.Length / 8;

            string[] ipnewparts = new string[IPPartsLength];

            int j = 0;

            for (int i = 0; i < ipnewparts.Length; i++)
            {
                ipnewparts[i] = net_id_plus_host.Substring(j, 8);
                j = j + 8;
            }

            string[] ipparts = ipadresse.Split('.');

            j = ipparts.Length - 1;
            for (int i = ipnewparts.Length - 1; i >= 0; i--)
            {

                ipparts[j] = Convert.ToInt64(ipnewparts[i], 2).ToString();
                j--;

            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < ipparts.Length; i++)
                if (i == ipparts.Length - 1) stringBuilder.Append(ipparts[i]);
                else stringBuilder.Append(ipparts[i] + ".");

            return stringBuilder.ToString();


            //*****************

        }

         string getFirstIpAdresse(string ipadresse, int standard_subnet_mask_bits_count, int current_subnet_mask_bits_count, string current_subnet_mask_bits)
        {

            int hostlength = 32 - current_subnet_mask_bits_count;

            int subnet_length = current_subnet_mask_bits_count - standard_subnet_mask_bits_count;

            string hostpart = current_subnet_mask_bits.Substring(current_subnet_mask_bits_count, hostlength);

            string ip_bits = ip_to_bit_string(ipadresse);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hostpart.Length; i++)
            {
                if (i == hostpart.Length - 1) stringBuilder.Append("1");
                else stringBuilder.Append("0");
            }
            string net_id_plus_host = stringBuilder.ToString();

            if (subnet_length > 0)
            {

                string subnet_part = current_subnet_mask_bits.Substring(standard_subnet_mask_bits_count, subnet_length);

                string to_operat_ip_bits = ip_bits.Substring(standard_subnet_mask_bits_count, subnet_length);

                //   var And_operation_result = Convert.ToByte(to_operat_ip_bits, 2) & Convert.ToByte(subnet_part, 2);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < subnet_part.Length; i++)
                    sb.Append(Convert.ToUInt64(to_operat_ip_bits[i].ToString(), 2) & Convert.ToUInt64(subnet_part[i].ToString(), 2));

                for (int i = 0; i < hostpart.Length; i++)
                    if (i == hostpart.Length - 1) sb.Append("1");
                    else sb.Append("0");

                net_id_plus_host = sb.ToString();
            }



            //*****************
            int IPPartsLength = net_id_plus_host.Length / 8;

            string[] ipnewparts = new string[IPPartsLength];

            int j = 0;

            for (int i = 0; i < ipnewparts.Length; i++)
            {
                ipnewparts[i] = net_id_plus_host.Substring(j, 8);
                j = j + 8;
            }

            string[] ipparts = ipadresse.Split('.');

            j = ipparts.Length - 1;
            for (int i = ipnewparts.Length - 1; i >= 0; i--)
            {

                ipparts[j] = Convert.ToInt64(ipnewparts[i], 2).ToString();
                j--;

            }
            StringBuilder stringBuilder1 = new StringBuilder();
            for (int i = 0; i < ipparts.Length; i++)
                if (i == ipparts.Length - 1) stringBuilder1.Append(ipparts[i]);
                else stringBuilder1.Append(ipparts[i] + ".");

            return stringBuilder1.ToString();


            //*****************

        }

         string getLastIpAdresse(string ipadresse, int standard_subnet_mask_bits_count, int current_subnet_mask_bits_count, string current_subnet_mask_bits)
        {

            int hostlength = 32 - current_subnet_mask_bits_count;

            int subnet_length = current_subnet_mask_bits_count - standard_subnet_mask_bits_count;

            string hostpart = current_subnet_mask_bits.Substring(current_subnet_mask_bits_count, hostlength);

            string ip_bits = ip_to_bit_string(ipadresse);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hostpart.Length; i++)
            {
                if (i == hostpart.Length - 1) stringBuilder.Append("0");
                else stringBuilder.Append("1");
            }
            string net_id_plus_host = stringBuilder.ToString();

            if (subnet_length > 0)
            {

                //   string net_id_part = current_subnet_mask_bits.Substring(standard_subnet_mask_bits_count, net_id_length).PadRight(net_id_length + hostlength, '0');
                string subnet_part = current_subnet_mask_bits.Substring(standard_subnet_mask_bits_count, subnet_length);

                string to_operat_ip_bits = ip_bits.Substring(standard_subnet_mask_bits_count, subnet_length);

                //  var And_operation_result = Convert.ToByte(to_operat_ip_bits, 2) & Convert.ToByte(subnet_part, 2);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < subnet_part.Length; i++)
                    sb.Append(Convert.ToUInt64(to_operat_ip_bits[i].ToString(), 2) & Convert.ToUInt64(subnet_part[i].ToString(), 2));

                for (int i = 0; i < hostpart.Length; i++)
                    if (i == hostpart.Length - 1) sb.Append("0");
                    else sb.Append("1");

                net_id_plus_host = sb.ToString();
            }



            //*****************
            int IPPartsLength = net_id_plus_host.Length / 8;

            string[] ipnewparts = new string[IPPartsLength];

            int j = 0;

            for (int i = 0; i < ipnewparts.Length; i++)
            {
                ipnewparts[i] = net_id_plus_host.Substring(j, 8);
                j = j + 8;
            }

            string[] ipparts = ipadresse.Split('.');

            j = ipparts.Length - 1;
            for (int i = ipnewparts.Length - 1; i >= 0; i--)
            {

                ipparts[j] = Convert.ToInt64(ipnewparts[i], 2).ToString();
                j--;

            }
            StringBuilder stringBuilder1 = new StringBuilder();
            for (int i = 0; i < ipparts.Length; i++)
                if (i == ipparts.Length - 1) stringBuilder1.Append(ipparts[i]);
                else stringBuilder1.Append(ipparts[i] + ".");

            return stringBuilder1.ToString();


            //*****************
        }
        string ip_to_bit_string(string ip)
        {

            string[] ip_arr = ip.Split('.');

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string str in ip_arr)
                stringBuilder.Append(Convert.ToString(Int32.Parse(str), 2).PadLeft(8, '0'));

            return stringBuilder.ToString();

        }


        void Ping_Device(string host, int time_out)
        {

            Thread thread = new Thread(() =>
            {
                try
                {
                    PingDevice ping = new PingDevice();
                    ping.PingCompleted += new PingDeviceCompletedEventHandler(Ping_Completed);
                    ping.SendAsync(host, time_out);
                }
                catch
                {

                }
            });

            thread.Start();

            //   Ping_Threads.Add(thread);

        }
        void Ping_Device(string host)
        {

            Thread thread = new Thread(() =>
            {
                try
                {
                    PingDevice ping = new PingDevice();
                    ping.PingCompleted += new PingDeviceCompletedEventHandler(Ping_Completed);
                    ping.SendAsync(host);
                }
                catch
                {

                }
            });

            thread.Start();

            //   Ping_Threads.Add(thread);

        }

        void Ping_ALL_Devices(string Gateway, bool Autotimeout)
        {
            string[] array = Gateway.Split('.');

            IPClass iPClass = getIPClass(Gateway);

            if (iPClass == IPClass.D)
                MessageBox.Show("Cannot Ping Multicast Address");

            if (iPClass == IPClass.E)
                MessageBox.Show("Cannot Ping Address From Class E");



            if (iPClass == IPClass.A)
            {
                SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + 0 + "." + 0 + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                for (int a = 1; a < 255; a++)
                {
                    SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + a + "." + 0 + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                    for (int b = 1; b < 255; b++)
                    {
                        SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + a + "." + b + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                        for (int c = 1; c < 255; c++)
                        {
                            if (Autotimeout)
                            {
                                string ping_var = array[0] + "." + a + "." + b + "." + c;
                                Ping_Device(ping_var);

                            }
                            else
                            {
                                string ping_var = array[0] + "." + a + "." + b + "." + c;
                                Ping_Device(ping_var, (int)this.timeout_numericUpDown.Value);
                            }
                        }
                    }

                }

            }

            if (iPClass == IPClass.B)
            {
                SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + array[1] + "." + 0 + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                for (int a = 1; a < 255; a++)
                {
                    SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + array[1] + "." + a + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                    for (int b = 1; b < 255; b++)
                    {
                        if (Autotimeout)
                        {
                            string ping_var = array[0] + "." + array[1] + "." + a + "." + b;
                            Ping_Device(ping_var);
                        }
                        else
                        {

                            string ping_var = array[0] + "." + array[1] + "." + a + "." + b;
                            Ping_Device(ping_var, (int)this.timeout_numericUpDown.Value);
                        }

                    }

                }

            }

            if (iPClass == IPClass.C)
            {
                SafeInvoke(log_textBox, new Action(() => { log_textBox.AppendText("ping all in " + array[0] + "." + array[1] + "." + array[2] + "." + 0 + " avileable Devices " + Environment.NewLine); }));

                for (int a = 1; a < 255; a++)
                {
                    if (Autotimeout)
                    {
                        string ping_var = array[0] + "." + array[1] + "." + array[2] + "." + a;
                        Ping_Device(ping_var);
                    }
                    else
                    {
                        string ping_var = array[0] + "." + array[1] + "." + array[2] + "." + a;
                        Ping_Device(ping_var, (int)this.timeout_numericUpDown.Value);
                    }

                }

            }

            //time in milliseconds           
            //  Ping_Host(ping_var, 4000);

        }


        #endregion
     
        #region --- Events --------------------------

        private void mask_bits_richTextBox_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = 0;

            int selectionLength = 0;

            for (int i = 0; i < this.mask_bits_richTextBox.Text.Length; i++)
            {

                if (mask_bits_richTextBox.Text[i] == '1')
                {

                    selectionLength++;

                }

            }

            if (!(selectionLength <= 0))
            {

                mask_bits_richTextBox.Select(selectionStart, selectionLength);
                mask_bits_richTextBox.SelectionColor = Color.White;
                mask_bits_richTextBox.SelectionBackColor = Color.Red;


            }


            this.mask_textBox.Text = getSubnetMask(this.mask_bits_richTextBox.Text);
        }

        private void Enter_ip_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Enter_ip_radioButton.Checked)
            {

                this.Ip_textBox.Enabled = true;
                this.Ip_comboBox.Enabled = false;
                this.mask_bits_trackBar.Value = 0;
                if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
                    this.mask_bits_trackBar.Enabled = true;
                else this.mask_bits_trackBar.Enabled = false;
                mask_bits_trackBar_Scroll(sender, e);

            }
            else
            {

                this.Ip_textBox.Enabled = false;
                this.Ip_comboBox.Enabled = true;
                this.mask_bits_trackBar.Value = 0;
                if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
                    this.mask_bits_trackBar.Enabled = true;
                else this.mask_bits_trackBar.Enabled = false;
                mask_bits_trackBar_Scroll(sender, e);

            }
        }

        public void SafeInvoke(Control uiElement, Action updater)
        {

            if (uiElement == null)
            {
                throw new ArgumentNullException("Control was null");
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


        private void Ip_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
            {
                try
                {

                    IPAddress.Parse(IP);

                    this.mask_bits_trackBar.Enabled = true;
                    mask_bits_trackBar.Value = getSubnetMaskBitsCount(IP);
                    mask_bits_trackBar_Scroll(sender, e);
                    IPClass iPClass = getIPClass(IP);
                    this.ip_class_textBox.Text = iPClass.ToString();

                    if (iPClass == IPClass.C)
                    {

                        this.recommend.Text = "Recommended";
                        this.recommend.BackColor = Color.Green;

                    }
                    if (iPClass == IPClass.B)
                    {

                        this.recommend.Text = "Recommended but little Slowe";
                        this.recommend.BackColor = Color.Gold;

                    }

                    if (iPClass == IPClass.A)
                    {

                        this.recommend.Text = "Not Recommended";
                        this.recommend.BackColor = Color.Red;

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }


            }
            else this.mask_bits_trackBar.Enabled = false;
        }

        private void Ip_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(IP) && IP.Split('.').Length == 4 && !string.IsNullOrEmpty(IP.Split('.').Last()))
            {
                this.mask_bits_trackBar.Enabled = true;
                mask_bits_trackBar.Value = getSubnetMaskBitsCount(IP);
                mask_bits_trackBar_Scroll(sender, e);
                IPClass iPClass = getIPClass(IP);
                this.ip_class_textBox.Text = iPClass.ToString();
                if (iPClass == IPClass.C)
                {

                    this.recommend.Text = "Recommended";
                    this.recommend.BackColor = Color.Green;

                }
                if (iPClass == IPClass.B)
                {

                    this.recommend.Text = "Recommended but little Slowe";
                    this.recommend.BackColor = Color.Gold;

                }

                if (iPClass == IPClass.A)
                {

                    this.recommend.Text = "Not Recommended";
                    this.recommend.BackColor = Color.Red;

                }
            }
            else this.mask_bits_trackBar.Enabled = false;

        }

        private void mask_bits_trackBar_Scroll(object sender, EventArgs e)
        {


            int trackbarvalue = mask_bits_trackBar.Value;

            int submask = getSubnetMaskBitsCount(IP);

            if (submask > trackbarvalue)
            {
                mask_bits_trackBar.Value = submask;
            }
            else
            {

                label5.Text = @"/ " + trackbarvalue.ToString();

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < trackbarvalue; i++)
                {

                    sb.Append("1");

                }

                int max = mask_bits_trackBar.Maximum;

                for (int i = trackbarvalue; i < max; i++)
                {

                    sb.Append("0");

                }

                mask_bits_richTextBox.Text = sb.ToString();
                if (mask_bits_trackBar.Enabled)
                {

                    this.net_id_textBox.Text = getNetID(IP, this.mask_textBox.Text).ToString();
                    this.end_ip_textBox.Text = getLastIpAdresse(IP, submask, trackbarvalue, mask_bits_richTextBox.Text).ToString();
                    this.start_ip_textBox.Text = getFirstIpAdresse(IP, submask, trackbarvalue, mask_bits_richTextBox.Text).ToString();
                    this.max_host_textBox.Text = getHostcount(trackbarvalue).ToString();
                    this.net_id_count_textBox.Text = getNetcount(submask, trackbarvalue).ToString();
                    this.Broadcast_tb.Text = getBroadcastAdresse(IP, submask, trackbarvalue, mask_bits_richTextBox.Text).ToString();

                }

            }

        }
        void Ping_Completed(object sender, PingDeviceCompletedEventArgs e)
        {

            SafeInvoke(progressBar, new Action(() => { PingCompletedCount++; }));

            if (e.IP != null)
            {

                string ip = (string)e.IP;
                if (e.Status == PingDeviceStatus.Completed)
                {
                    try
                    {

                        ActiveDevice activeDevice = new ActiveDevice(ip);
                     
                        if (AllGatwas.Contains(activeDevice.IPV4Adresse))
                        {

                            Gatways.Add(activeDevice);
                        
                        }



                        SafeInvoke(found_ip_treeView,
                             new Action(() =>
                             {
                                 if (activeDevice.MACAdresse != "not detected")
                                 {
                                     devices_listView.Items.Add(ip, activeDevice.HostName, 1);
                                     devices_listView.Items[ip].Tag = activeDevice;
                                 }
                                 else
                                 {

                                     devices_listView.Items.Add(ip, activeDevice.HostName, 0);
                                     devices_listView.Items[ip].Tag = activeDevice;

                                 }

                                 found_ip_treeView.Nodes.Add(activeDevice.HostName + ip, activeDevice.HostName, 1);
                                 found_ip_treeView.Nodes[activeDevice.HostName + ip].Tag = activeDevice;

                                 found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes.Add(ip, ip, 0);
                                 found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes[ip].Tag = activeDevice;

                                 found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes[ip].Nodes.Add(activeDevice.HostName ?? ip, activeDevice.HostName ?? ip, 4);
                                 found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes[ip].Nodes[activeDevice.HostName ?? ip].Tag = activeDevice;

                                 found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes[ip].Nodes.Add(activeDevice.MACAdresse, activeDevice.MACAdresse, 2);
                                 found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes[ip].Nodes[activeDevice.MACAdresse].Tag = activeDevice;
                                 if (activeDevice.IPV6Adresse != null)
                                 {
                                     foreach (string ip_v6 in activeDevice.IPV6Adresse)
                                     {
                                         found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes[ip].Nodes.Add(ip_v6, ip_v6, 3);
                                         found_ip_treeView.Nodes[activeDevice.HostName + ip].Nodes[ip].Nodes[ip_v6].Tag = activeDevice;

                                     }
                                 }
                             }));
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }

            }
            else
            {
                // MessageBox.Show(e.Reply.Status.ToString());
            }
        }


      
        void ping_devices_bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                int netcont = int.Parse(this.net_id_count_textBox.Text);
                int hostcont = int.Parse(this.max_host_textBox.Text);
                SafeInvoke(this,
                       new Action(() =>
                       {
                           if ((bool)auto_timeout_btn.Tag)
                           {
                               if (this.Enter_ip_radioButton.Checked)
                                   Ping_ALL_Devices(this.Ip_textBox.Text, true);
                               else Ping_ALL_Devices(this.Ip_comboBox.SelectedItem.ToString(), true);
                           }
                           else
                           {
                               if (this.Enter_ip_radioButton.Checked)
                                   Ping_ALL_Devices(this.Ip_textBox.Text, false);
                               else Ping_ALL_Devices(this.Ip_comboBox.SelectedItem.ToString(), false);

                           }
                       }));

                while (true)
                {

                    int progressmaxvalue = (hostcont * netcont);

                    if (progressmaxvalue > 0)
                    {
                        SafeInvoke(progressBar, new Action(() => { progressBar.Value = ((((PingCompletedCount + 1) * 100) / (hostcont * netcont))); }));
                        if (this.PingCompletedCount == (hostcont * netcont))
                        {
                            SafeInvoke(progressBar, new Action(() => { progressBar.Value = 0; }));
                            PingCompletedCount = 0;
                            break;
                        }
                    }

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            if (!ping_devices_bgw.IsBusy)
            {
                found_ip_treeView.Nodes.Clear();
                devices_listView.Items.Clear();
                ping_devices_bgw.RunWorkerAsync();
            }
        }

        private void auto_timeout_btn_Click(object sender, EventArgs e)
        {
            if (!((bool)this.auto_timeout_btn.Tag))
            {

                this.auto_timeout_btn.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_schalter_an_40;
                this.auto_timeout_btn.Tag = true;
                this.timeout_numericUpDown.Enabled = false;

            }
            else
            {

                this.auto_timeout_btn.Image = global::NetworkScannerAndSniffer.Properties.Resources.icons8_schalter_aus_40;
                this.auto_timeout_btn.Tag = false;
                this.timeout_numericUpDown.Enabled = true;

            }

        }

        private void devices_listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = devices_listView.SelectedItems[0];

            if (listViewItem != null)
            {
                if (listViewItem.ImageIndex == 1)
                {
                    string deviceAdapter = ((Enter_ip_radioButton.Checked) ? Ip_textBox.Text : Ip_comboBox.SelectedItem.ToString());
                    NetworkSniffer deviceSniffer = new NetworkSniffer(deviceAdapter, (ActiveDevice)listViewItem.Tag, Gatways);
                    deviceSniffer.Show();
                }

            }
        }

        #endregion


    }
}