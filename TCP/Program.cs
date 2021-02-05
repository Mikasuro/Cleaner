using Cleaner.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace TCP
{
    class Program
    {
        private const int port = 8887;
        static void Main(string[] args)
        {

            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");
                    NetworkStream stream = client.GetStream();
                    byte[] data = new byte[Int16.MaxValue];
                    int bytes = stream.Read(data, 0, data.Length);
                    string message = Encoding.UTF8.GetString(data, 0, bytes);
                    Console.WriteLine("Отправлено сообщение: {0}", message);
                    var serverMessage = JsonConvert.DeserializeObject<ServerMessage>(message);
                    foreach (var item in serverMessage.Datas)
                    {
                        Console.WriteLine(item.Root + "\\" + item.Name + " " + item.Length);
                    }
                    var dirPath = Assembly.GetExecutingAssembly().Location;
                    dirPath = Path.GetDirectoryName(dirPath);
                    using (StreamReader sr = new StreamReader(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))) 
                        + "\\CleanService\\bin\\Debug\\itemSourse.txt"))
                    {
                        var milliSeconds = (int)Math.Abs(span.TotalMilliseconds);
                        Console.WriteLine(milliSeconds);
                        Console.WriteLine(string.Format("Удаление произайдет через: {0}ч. {1}мин. {2}сек.",
                            TimeSpan.FromMilliseconds(milliSeconds).Hours, TimeSpan.FromMilliseconds(milliSeconds).Minutes, TimeSpan.FromMilliseconds(milliSeconds).Seconds));
                        
                        Thread.Sleep(span);
                        DeleteFiles.DeleteFile(message);
                    }
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
