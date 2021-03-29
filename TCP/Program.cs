using Cleaner.Model;
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
                Thread thread1 = new Thread(new ThreadStart(() => Read()));
                thread1.Start();
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread thread = new Thread(new ThreadStart(() => WorkWithClient(client)));
                    thread.Start();
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
        public static void Read()
        {
            FileReading file = new FileReading();
            while (true)
            {
                DeleteFiles.DeleteFile(file.ReadFileList());
            }
        }
        public static void WorkWithClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[Int16.MaxValue];
            int bytes = stream.Read(data, 0, data.Length);
            string message = Encoding.UTF8.GetString(data, 0, bytes);
            byte[] array;
            if (message == "1")
            {
                string fileName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\note.txt";
                using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    fstream.Close();
                }
                stream.Write(array, 0, array.Length);
            }
            if (message == "2")
            {
                string request = "3";
                string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\note.txt";
                var array1 = Encoding.UTF8.GetBytes(request);
                stream.Write(array1, 0, array1.Length);
                byte[] dataRead = new byte[Int16.MaxValue];
                int dataBytes = stream.Read(dataRead, 0, dataRead.Length);
                string directoriesList = Encoding.UTF8.GetString(dataRead, 0, dataBytes);
                if (File.Exists(path))
                {
                    File.WriteAllText(path, directoriesList);
                }
            }
        }
    }
}
