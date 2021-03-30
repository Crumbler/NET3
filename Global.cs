using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace NET3
{
    public class Lan
    {
        public IPAddress addr;
        public IPAddress mask;

        public Lan(IPAddress Addr, IPAddress Mask)
        {
            addr = Addr;
            mask = Mask;
        }
    }

    public static class Global
    {
        public static IPAddress lanIp, lanMask;
        public static IPEndPoint localEp;
        public static string myNick;

        public static void Init()
        {
            List<Lan> lans = GetLans();

            if (lans.Count == 1)
            {
                lanIp = lans[0].addr;
                lanMask = lans[0].mask;
                return;
            }

            var frm = new FormLan();

            DialogResult res = frm.ShowDialog(lans);

            if (res == DialogResult.OK)
            {
                lanIp = frm.chosenLan.addr;
                lanMask = frm.chosenLan.mask;
                return;
            }

            Application.Exit();
        }

        public static IPAddress IPAddress(this EndPoint ep)
        {
            return (ep as IPEndPoint).Address;
        }

        public static List<Lan> GetLans()
        {
            var lans = new List<Lan>();

            // For all IPv4 lans, get their subnet mask and my ip address
            foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
                foreach (var unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                        lans.Add(new Lan(unicastIPAddressInformation.Address,
                                         unicastIPAddressInformation.IPv4Mask));

            return lans;
        }
    }
}
