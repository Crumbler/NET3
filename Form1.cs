using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Socket tcpAccepter;
        private List<Member> members;
        private List<IPAddress> pendingConnections;

        private void LogChat(string s)
        {
            lock (RchtxtChat)
            {
                RchtxtChat.AppendText(s + Environment.NewLine);

                RchtxtChat.SelectionStart = RchtxtChat.Text.Length;
                RchtxtChat.ScrollToCaret();
            }
        }

        private void Log(string s)
        {
            var now = DateTime.Now;
            string currTime = now.Hour.ToString() + ':' +
                              now.Minute.ToString() + ':' +
                              now.Second.ToString() + ": ";

            lock (RchtxtLog)
            {
                RchtxtLog.AppendText(currTime + s + Environment.NewLine);

                RchtxtLog.SelectionStart = RchtxtLog.Text.Length;
                RchtxtLog.ScrollToCaret();
            }
        }

        private void SendMessage(string s)
        {
            foreach (Member memb in members)
                memb.SendMessage(s, MessageType.ChatMessage);

            Log("Sent \"" + s + '"');
        }

        private void TxtboxSend_KeyDown(object sender, KeyEventArgs e)
        {
            // Don't send empty messages
            if (TxtboxSend.Text.Equals(string.Empty))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                SendMessage(TxtboxSend.Text);

                LogChat(Global.myNick + ": " + TxtboxSend.Text);
                TxtboxSend.Text = string.Empty;
            }
        }

        private bool MemberAbsent(IPAddress addr)
        {
            lock (pendingConnections)
            {
                for (int i = 0; i < pendingConnections.Count; ++i)
                    if (pendingConnections[i].Equals(addr))
                        return false;
            }

            lock (members)
            {
                for (int i = 0; i < members.Count; ++i)
                {
                    var membAddr = members[i].sock.RemoteEndPoint.IPAddress();
                    if (membAddr.Equals(addr))
                        return false;
                }
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PanelChat.BringToFront();
            RchtxtChat.BringToFront();
            RchtxtLog.BringToFront();

            members = new List<Member>();
            pendingConnections = new List<IPAddress>();

            Global.Init();
            Broadcaster.Init();

            Log("My IP: " + Global.lanIp);
            Log("Mask: " + Global.lanMask);
            Log("Broad address: " + Broadcaster.broadAddr);

            Broadcaster.OnNewBroadcast = OnBroadcastReceived;
            Broadcaster.MemberCheck = MemberAbsent;

            // Create tcp receiver socket
            tcpAccepter = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpAccepter.Bind(Global.localEp);
        }

        private async void BeginAccepting()
        {
            Socket membSock;
            Member memb;

            tcpAccepter.Listen(5);

            while (true)
            {
                membSock = await tcpAccepter.AcceptAsync();

                // If not connected
                if (MemberAbsent(membSock.RemoteEndPoint.IPAddress()))
                {
                    memb = new Member(membSock);
                    lock (members)
                    {
                        members.Add(memb);
                    }

                    Log("Accepted from " + memb.sock.RemoteEndPoint);

                    BeginReceiving(memb);
                }
                else
                {
                    // Already connected to member
                    membSock.Shutdown(SocketShutdown.Both);
                    membSock.Close();
                }
            }
        }

        private void OnBroadcastReceived(IPEndPoint addr, string newNick)
        {
            Log("Received broadcast from " + newNick + '(' + addr + ')');

            BeginConnecting(new Member(newNick), addr);
        }

        private async void BeginConnecting(Member memb, IPEndPoint addr)
        {
            lock (pendingConnections)
            {
                pendingConnections.Add(addr.Address);
            }

            try 
            {
                await memb.sock.ConnectAsync(addr);
            }
            catch(Exception)
            {
                Log("Failed to connect to " + memb.name + '(' + addr + ')');
                return;
            }
            finally
            {
                lock (pendingConnections)
                {
                    pendingConnections.Remove(addr.Address);
                }
            }

            if (MemberAbsent(addr.Address))
            {
                Log("Connected to " + memb.name + '(' + addr + ')');

                lock (members)
                {
                    members.Add(memb);
                }

                try
                {
                    memb.SendMessage(Global.myNick, MessageType.NickMessage);

                    BeginReceiving(memb);
                }
                catch (Exception)
                {   // Connection is a duplicate
                    lock (members)
                    {
                        members.Remove(memb);
                    }
                }
            }
            else
            {
                // Already accepted connection to this member
                memb.sock.Shutdown(SocketShutdown.Both);
                memb.sock.Close();
            }
        }

        private async void BeginReceiving(Member memb)
        {
            while(true)
            {
                try
                {
                    int cnt = await memb.sock.ReceiveAsync(memb.buf, SocketFlags.None);

                    // Connection closed gracefully
                    if (cnt == 0)
                        throw new SocketException();

                    (MessageType, string) res = memb.ProcessMessage();

                    switch(res.Item1)
                    {
                        case MessageType.ChatMessage:
                            LogChat(memb.name + ": " + res.Item2);
                            Log("Received \"" + res.Item2 + "\" from " + 
                                memb.name + '(' + memb.sock.RemoteEndPoint + ')');
                            break;

                        case MessageType.NickMessage:
                            memb.name = res.Item2;
                            Log("Received nickname " + res.Item2 + " from " + memb.sock.RemoteEndPoint);
                            break;
                    }
                }
                catch (SocketException)
                {
                    Log("Lost connection with " + memb.name + '(' + memb.sock.RemoteEndPoint + ')');

                    // Remove the disconnected member
                    lock (members)
                    {
                        for (int i = 0; i < members.Count; ++i)
                            if (members[i] == memb)
                            {
                                members.RemoveAt(i);
                                return;
                            }
                    }
                }
            }
        }

        private void BtnBroadcast_Click(object sender, EventArgs e)
        {
            DisableBroadcast();

            Global.myNick = TxtboxName.Text;

            Log("Sent broadcast");
            Broadcaster.Broadcast();

            BeginAccepting();
        }

        private void DisableBroadcast()
        {
            BtnBroadcast.Enabled = false;
            TxtboxName.Enabled = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            lock (members)
            {
                foreach (Member memb in members)
                {
                    memb.sock.Shutdown(SocketShutdown.Both);
                    memb.sock.Close();
                }
            }
        }
    }
}
