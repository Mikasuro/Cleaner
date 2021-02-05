using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService
{
    class DeleteFiles
    {
        public static void DeleteFile(string message)
        {
            var serverMessage = JsonConvert.DeserializeObject<ServerMessage>(message);
            foreach (var item in serverMessage.Datas)
            {
                try
                {
                    if (Directory.Exists(item.Root + "\\" + item.Name))
                    {
                        Directory.Delete(item.Root + "\\" + item.Name, true);
                        Console.WriteLine("Каталог удален");
                    }
                    if (File.Exists(item.Root + "\\" + item.Name))
                    {
                        File.Delete(item.Root + "\\" + item.Name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
