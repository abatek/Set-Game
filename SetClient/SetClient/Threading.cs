using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;

namespace SetClient
{
    class Threading
    {
        public static TcpClient TCP_clnt = new TcpClient();
        public string host;
        public int port = 8001;

        public void Connect() {
            try
            {
                host = "127.0.0.1";

                Console.Write("Attempting to connect...");
                TCP_clnt.Connect(host, port);
                Console.WriteLine("Connected to {0}", host);

                Thread ThreadRead = ReadFromServer();
                ThreadRead.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..."+ e.StackTrace);
            }
        }

        public Thread ReadFromServer() {
            return new Thread(() =>
            {
                //generate stream with server
                Stream stm = TCP_clnt.GetStream();

                byte[] bb = new byte[100];

                while (true) {
                    string output = "";

                    int k = stm.Read(bb, 0, 100);
                    for (int i = 0; i < k; ++i)
                    {
                        char c = Convert.ToChar(bb[i]);
                        if (c.Equals('\n'))
                        {
                            //AddToListBox.UpdateListBox(output);
                            Console.WriteLine("Message: " + output);
                            output = "";
                        }
                        else {
                            output += c;
                        }
                    }
                }
            });
        }

        public void WriteToServer(string str) {
            Stream stm = TCP_clnt.GetStream();

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str + "\n");

            stm.Write(ba, 0, ba.Length);
        }
    }
}
