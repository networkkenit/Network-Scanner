using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkScannerAndSniffer
{

    public enum PingDeviceStatus
    {
        Pending,
        Completed,
        InvalidHost,
        Timeout
    }

    public delegate void PingDeviceCompletedEventHandler(Object sender, PingDeviceCompletedEventArgs e);

    public class PingDevice
    {


        public event PingDeviceCompletedEventHandler PingCompleted;
        public PingDevice()  { }


        public async void SendAsync(string hostNameOrAddress, int millisecond_time_out)
        {
            
                new Thread(async delegate ()
                {

                    PingDeviceCompletedEventArgs args = new PingDeviceCompletedEventArgs();
                    PingDeviceCompletedEventHandler handler = this.PingCompleted;
                    try
                    {
                        args.Status = PingDeviceStatus.Pending;
                        var result = Task.Run(() => Dns.GetHostEntryAsync(hostNameOrAddress)).Wait(millisecond_time_out);
                        if (!result)
                        {

                            args.Status = PingDeviceStatus.Timeout;
                            args.IP = null;
                            if (handler != null)
                                handler(this, args);

                        }
                        else
                        {

                            args.Status = PingDeviceStatus.Completed;
                            args.IP = hostNameOrAddress;
                            if (handler != null)
                                handler(this, args);

                        }
                    }
                    catch (Exception ex)
                    {

                        args.Status = PingDeviceStatus.InvalidHost;
                        args.IP = null;
                        if (handler != null)
                            handler(this, args);


                    }


                }).Start();


            }

            public async void SendAsync(string hostNameOrAddress)
            {
                {
                    new Thread(async delegate ()
                    {

                        PingDeviceCompletedEventArgs args = new PingDeviceCompletedEventArgs();
                        PingDeviceCompletedEventHandler handler = this.PingCompleted;
                        try
                        {
                            args.Status = PingDeviceStatus.Pending;
                            var result = await Dns.GetHostEntryAsync(hostNameOrAddress);
                           
                            if (result == null)
                            {

                                args.Status = PingDeviceStatus.Timeout;
                                args.IP = null;
                                if (handler != null)
                                    handler(this, args);

                            }
                            else
                            {

                                args.Status = PingDeviceStatus.Completed;
                                args.IP = hostNameOrAddress;
                                if (handler != null)
                                    handler(this, args);

                            }
                        }
                        catch (Exception ex)
                        {

                            args.Status = PingDeviceStatus.InvalidHost;
                            args.IP = null;
                            if (handler != null)
                                handler(this, args);


                        }


                    }).Start();


                }


            }
    }
}

