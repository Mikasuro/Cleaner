using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cleaner.Service
{
    public class Server
    {
        private const int port = 8887;
        private const string server = "127.0.0.1";

        public string SendString(object str, object json)
        {
            string message = str as string;
            string serverMessage = null;
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);
                NetworkStream stream = client.GetStream();
                do
                {
                    if (message == "1")
                    {
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        byte[] dataRead = new byte[Int16.MaxValue];
                        int bytes = stream.Read(dataRead, 0, dataRead.Length);
                        serverMessage = Encoding.UTF8.GetString(dataRead, 0, bytes);
                    }
                    if(message == "2")
                    {
                        byte[] data1 = Encoding.UTF8.GetBytes(message);
                        stream.Write(data1, 0, data1.Length);
                        byte[] dataRead1 = new byte[Int16.MaxValue];
                        int bytes1 = stream.Read(dataRead1, 0, dataRead1.Length);
                        if(Encoding.UTF8.GetString(dataRead1, 0, bytes1) == "3")
                        {
                            byte[] directories = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));
                            stream.Write(directories, 0, directories.Length);
                        }
                    }
                }
                while (stream.DataAvailable);
                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            return serverMessage;
        }
    }
}
