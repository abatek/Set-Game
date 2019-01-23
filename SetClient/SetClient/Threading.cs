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
        public List<Card> dealtCards = new List<Card>();
        public int threadServerScore = 0;
        public int threadClientScore = 0;
        public string endGameString = null;

        public void Connect(string ip) {
            try
            {
                Console.Write("Attempting to connect...");
                TCP_clnt.Connect(ip, port);
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
                    string[] rawScores = new string[2];

                    int k = stm.Read(bb, 0, 100); // number of characters in 100 byte segment
                    for (int i = 0; i < k; ++i)
                    {
                        char c = Convert.ToChar(bb[i]);
                        if (c.Equals('\n')) // newline character signals end of message
                        {
                            if (output.Substring(0, 1) == "*")
                            {
                                Console.WriteLine(output);

                            }
                            else if (output.Substring(0, 1) == "~")
                            {
                                output = output.Substring(1);
                                rawScores = output.Split(' ');
                                threadServerScore = Convert.ToInt32(rawScores[0]);
                                threadClientScore = Convert.ToInt32(rawScores[1]);
                            }
                            else if (output.Substring(0, 1) == "#")
                            {
                                output = output.Substring(1);
                                endGameString = output;
                            }
                            else
                            {
                                SetLogic convert = new SetLogic();
                                dealtCards = convert.convertToCards(output);
                            }
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
            // sends encoded string followed by newline 
            byte[] ba = asen.GetBytes(str + "\n"); 
            stm.Write(ba, 0, ba.Length);

            Console.WriteLine("Sent:{0}", str);
        }
    }
}
