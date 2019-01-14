using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetServer
{
    //test commit
    class Threading
    {
        public Socket s;
        public string ip = "192.168.1.135";
        public IPAddress ipAd;
        public int port = 8001;
        public void Connect() {
            try
            {
                

                IPAddress ipAd = IPAddress.Parse(ip);
                TcpListener listener = new TcpListener(ipAd, port);
                listener.Start();

                Console.WriteLine("Server is running on ip {0} at port {1}", ip, port);
                Console.WriteLine("Local endpoint is: {0}", listener.LocalEndpoint);
                Console.WriteLine("Waiting for connection...");

                s = listener.AcceptSocket();
                Console.WriteLine("Connection accepted from {0}", s.RemoteEndPoint);
                ASCIIEncoding msg = new ASCIIEncoding();
                s.Send(msg.GetBytes("maybe this"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..." + e.StackTrace);
            }
        }
        public void ReadFromClient() {
            Thread ThreadRead = new Thread(() => {
                string output = null;
                int k = 0;
                byte[] b = new byte[100];
                k = s.Receive(b);

                for (int i = 0; i < k; ++i)
                    output += Convert.ToChar(b[i]);

                AddToListBox.UpdateListBox(output);
            });
            ThreadRead.Start();
            ThreadRead.Join();
        }
        public void WriteToClient()
        {
            try
            {
                Thread ThreadWrite = new Thread(() =>
                {
                    TcpListener listener = new TcpListener(IPAddress.Parse("192.168.1.135"), port);
                    listener.Start();
                    s = listener.AcceptSocket();
                    ASCIIEncoding msg = new ASCIIEncoding();
                    TextBox t = Application.OpenForms["MainForm"].Controls["txtSend"] as TextBox;
                    s.Send(msg.GetBytes("maybe this"));
                    //ISSUES WITH NULL VALUE FROM S.SEND
                    
                });

                ThreadWrite.Start();
                ThreadWrite.Join();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..." + e.StackTrace);
            }
        }

    }
}
