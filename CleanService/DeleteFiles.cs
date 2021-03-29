using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanService
{
    class DeleteFiles
    {
        public static void DeleteFile(ObservableCollection<DirectoryInfos> directoryInfos)
        {
            foreach (var item in directoryInfos)
            {
                DirectoryInfo directory = new DirectoryInfo(item.Root);
                if (Directory.Exists(item.Root) && item.Time == DateTime.Now.ToString("t"))
                {
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ошибка при удалении файлов: {0}", e);
                        }
                    }
                    foreach (DirectoryInfo dir in directory.GetDirectories())
                    {
                        try
                        {
                            dir.Delete();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ошибка при удалении директорий: {0}", e);
                        }
                    }
                }
            }
            Thread.Sleep(60000);
        }
    }
}
