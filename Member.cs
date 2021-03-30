using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NET3
{
    public class Member
    {
        public Socket sock;
        public string name;

        public byte[] buf = new byte[256];

        public Member(Socket s)
        {
            sock = s;
            name = null;
        }

        public Member(string s)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            name = s;
        }

        public (MessageType, string) ProcessMessage()
        {
            var msgType = (MessageType)buf[0];
            byte msgLength = buf[1];

            string msg = Encoding.ASCII.GetString(buf, 2, msgLength);

            return (msgType, msg);
        }

        public void SendMessage(string s, MessageType msgType)
        {
            byte[] msg = Encoding.ASCII.GetBytes(s);
            // Message type + string length + one byte per char
            var sendBuf = new byte[msg.Length + 2];

            sendBuf[0] = (byte)msgType;
            sendBuf[1] = (byte)msg.Length;
            Array.Copy(msg, 0, sendBuf, 2, msg.Length);

            sock.Send(sendBuf);
        }
    }
}
