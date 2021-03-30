using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NET3
{
    public static class Broadcaster
    {
        public static Action<IPEndPoint, string> OnNewBroadcast;
        public static Func<IPAddress, bool> MemberCheck;

        public static IPEndPoint broadAddr;
        private static EndPoint senderEp;
        private static Socket udpSock;
        private static byte[] sendBuf, receiveBuf;

        public static void Init()
        {
            broadAddr = CalcBroadAddr();

            sendBuf = new byte[256];
            receiveBuf = new byte[256];

            Global.localEp = new IPEndPoint(Global.lanIp, 25565);
        }

        private static async void StartReceiving()
        {
            SocketReceiveFromResult res;

            while (true)
            {
                res = await udpSock.ReceiveFromAsync(receiveBuf, SocketFlags.None, senderEp);

                var senderAddress = (res.RemoteEndPoint as IPEndPoint).Address;
                
                // If this isn't my broadcast and not broadcast of existing member
                if (!senderAddress.Equals(Global.localEp.Address) &&
                    MemberCheck(senderAddress))
                {
                    string senderName = Encoding.ASCII.GetString(receiveBuf, 1, receiveBuf[0]);
                    OnNewBroadcast(res.RemoteEndPoint as IPEndPoint, senderName);
                }
            }
        }

        public static void Broadcast()
        {
            // Create udp receiver socket
            udpSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                EnableBroadcast = true
            };
            udpSock.Bind(Global.localEp);

            // Prepare broadcast message
            sendBuf[0] = (byte)Global.myNick.Length;
            byte[] nickBytes = Encoding.ASCII.GetBytes(Global.myNick);
            Array.Copy(nickBytes, 0, sendBuf, 1, nickBytes.Length);

            udpSock.SendTo(sendBuf, Global.myNick.Length + 1, SocketFlags.None, broadAddr);

            senderEp = new IPEndPoint(0, 0);

            StartReceiving();
        }

        private static IPEndPoint CalcBroadAddr()
        {
            byte[] addrBytes = Global.lanIp.GetAddressBytes(),
                   maskBytes = Global.lanMask.GetAddressBytes();

            long brAddr = 0;
            for (int i = 3; i >= 0; --i)
            {
                brAddr <<= 8;
                brAddr |= (byte)(addrBytes[i] | ~maskBytes[i]);
            }

            return new IPEndPoint(brAddr, 25565);
        }
    }
}
