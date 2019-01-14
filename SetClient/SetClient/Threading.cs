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
                host = "192.168.1.135";

                Console.Write("Attempting to connect...");
                TCP_clnt.Connect(host, port);
                Console.WriteLine("Connected to {0}", host);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..."+ e.StackTrace);
            }
        }

        public void ReadFromServer() {
            Thread ThreadRead = new Thread(() =>
            {
                string output = null;
                
                //generate stream with server
                Stream stm = TCP_clnt.GetStream();

                byte[] bb = new byte[100];

                int k = stm.Read(bb, 0, 100);

                for (int i = 0; i < k; ++i) 
                    output += Convert.ToChar(bb[i]);

                MainForm form = new MainForm();
                AddToListBox.UpdateListBox(output);
                
            });
            ThreadRead.Start();
            ThreadRead.Join();
            
        }

        public void WriteToServer(string str) {
            Thread ThreadWrite = new Thread(() => {

                Stream stm = TCP_clnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();

                byte[] ba = asen.GetBytes(str);

                stm.Write(ba, 0, ba.Length);
            });
            
            ThreadWrite.Start();
            ThreadWrite.Join();

        }
    }
}
