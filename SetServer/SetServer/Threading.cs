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
        public string ip = "127.0.0.1";
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

                Thread ThreadRead = ReadFromClient();
                ThreadRead.Start();

                ASCIIEncoding msg = new ASCIIEncoding();
                s.Send(msg.GetBytes("Welcome to the server\n"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..." + e.StackTrace);
            }
        }

        public Thread ReadFromClient() {
            return new Thread(() =>
            {
                byte[] bb = new byte[100];

                while (true)
                {
                    string output = "";

                    int k = s.Receive(bb, 100, SocketFlags.None);
                    for (int i = 0; i < k; ++i)
                    {
                        char c = Convert.ToChar(bb[i]);
                        if (c.Equals('\n'))
                        {
                            //AddToListBox.UpdateListBox(output);
                            Console.WriteLine("Message: " + output);
                            output = "";
                        }
                        else
                        {
                            output += c;
                        }
                    }
                }
            });
        }
        public void WriteToClient(string text)
        {
            ASCIIEncoding msg = new ASCIIEncoding();
            s.Send(msg.GetBytes(text + "\n"));
        }

    }
}
