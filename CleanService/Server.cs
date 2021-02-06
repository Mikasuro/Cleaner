using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanService
{
    class Server
    {
        private const int port = 8888;

        public void ServerStart()
        {
            Debugger.Launch();
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");//
                server = new TcpListener(localAddr, port);
                server.Start();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    byte[] data = new byte[Int16.MaxValue];
                    int bytes = stream.Read(data, 0, data.Length);
                    string message = Encoding.UTF8.GetString(data, 0, bytes);
                    var serverMessage = JsonConvert.DeserializeObject<ServerMessage>(message);
                    var path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\itemSourse.txt";
                    using (StreamReader sr = new StreamReader(path))
                    { 
                        TimeSpan span = Convert.ToDateTime(sr.ReadToEnd()).Subtract(DateTime.Now);
                        Thread.Sleep(span);
                        DeleteFiles.DeleteFile(message);
                        sr.Close();
                    }
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}
