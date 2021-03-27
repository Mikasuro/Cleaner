using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP
{
    class DeleteFiles
    {
        public static void DeleteFile(ObservableCollection<DirectoryInfos> directoryInfos)
        {
            foreach (var item in directoryInfos)
            {
                DirectoryInfo directory = new DirectoryInfo(item.Root);
                if (Directory.Exists(item.Root))
                {
                    foreach(FileInfo file in directory.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Ошибка при удалении: {0}", e);
                        }
                    }
                }
            }
        }
    }
}
