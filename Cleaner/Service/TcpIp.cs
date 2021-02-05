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
    public class TcpIp
    {
        private const int port = 8887;
        private const string server = "127.0.0.1";

        public string ServerTCP(string message)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);
                NetworkStream stream = client.GetStream();
                do
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
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

            Console.WriteLine("Запрос завершен...");
            Console.Read();
            return null;
        }
    }
}
