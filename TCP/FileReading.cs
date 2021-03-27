using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TCP
{
    public class FileReading : INotifyPropertyChanged
    {
        private ObservableCollection<DirectoryInfos> _directoryInfos = new ObservableCollection<DirectoryInfos>();

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
        public string _path;
        public ObservableCollection<DirectoryInfos> DirectoryInf
        {
            get => _directoryInfos;
            set
            {
                _directoryInfos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DirectoryInfos> ReadFileList()
        {
            string fileName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\note.txt";
            byte[] array = null;
            try
            {
                using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    fstream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при чтении из файла: {0}", e);
            }

            try
            {
                var s = Encoding.UTF8.GetString(array);
                var mes = JsonConvert.DeserializeObject<DirectoryInfos[]>(s);
                foreach (var item in mes)
                {
                    var directory = new DirectoryInfos()
                    {
                        Name = item.Name,
                        Time = item.Time,
                        Root = item.Root
                    };
                    _directoryInfos.Add(directory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка десериализации: {0}", e);
            }
            return _directoryInfos;
        }
    }
}
