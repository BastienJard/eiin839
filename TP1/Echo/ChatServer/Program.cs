using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Echo
{

    class EchoServer
    {

        [Obsolete]
        static void Main(string[] args)
        {

            Console.CancelKeyPress += delegate
            {
                System.Environment.Exit(0);
            };

            TcpListener ServerSocket = new TcpListener(5000);
            ServerSocket.Start();

            Console.WriteLine("Server started.");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                handleClient client = new handleClient();
                client.startClient(clientSocket);
            }


        }
    }

    public class handleClient
    {
        TcpClient clientSocket;
        string HTTP_ROOT = "F:/SI4/SOC/eiin839/TP1/Echo/ChatServer/www/pub";
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Echo);
            ctThread.Start();
        }


        private void Echo()
        {
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            while (true)
            {
                string str = reader.ReadString();
                Console.WriteLine(str);
                if (str.Split(" ")[0] == "GET")
                {
                    this.get(str, writer);
                }
            }

        }

        private void get(String str, BinaryWriter writer)
        {
            try
            {
                String value = str.Split(" ")[1];
                String path = HTTP_ROOT + value;
                string reponse = "";
                if (!File.Exists(path))
                {
                    reponse ="http / 1.0 404 Not found";
                }
                else
                {
                    reponse = "http / 1.0 200 OK \n \n";
                    using (StreamReader sr = File.OpenText(path))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            reponse = reponse + "\n" + s;
                        }
                    }
                }
                writer.Write(reponse);
            }
            catch (IndexOutOfRangeException e)
            {
                writer.Write("http / 1.0 400 Bad request");
            }
 
        }

    }
}