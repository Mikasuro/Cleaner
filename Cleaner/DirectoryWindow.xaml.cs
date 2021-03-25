using Cleaner.Model;
using Cleaner.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace Cleaner
{
    /// <summary>
    /// Логика взаимодействия для DirectoryWindow.xaml
    /// </summary>
    public partial class DirectoryWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<DirectoryInfos> _directoryInfos = new ObservableCollection<DirectoryInfos>();

        private List<string> _listDirectory = new List<string>();

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

        public DirectoryWindow()
        {
            InitializeComponent();
            search.Focus();
            DataContext = this;
        }
        
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            

        }
        ServerMessage SerializeData()
        {
            int count = 0;
            string path = search.Text;
            try
            {
                DirectoryInfo dirInf = new DirectoryInfo(path);
                ServerMessage serverMessage = new ServerMessage();
                    //if (Directory.Exists(path))
                    //{
                    //    foreach (var item in dirInf.GetDirectories())
                    //    {
                    //        count++;
                    //        var d = new DirectoryInfos()
                    //        {
                    //            Name = item.Name,
                    //            Root = item.Root + item.Parent.ToString()
                    //        };

                    //        DirectoryInf.Add(d);
                    //    }
                    //    serverMessage.Datas = new Data[count];
                    //    foreach (var item in dirInf.GetFiles())
                    //    {
                    //        count++;
                    //        var f = new DirectoryInfos()
                    //        {
                    //            Name = item.Name,
                    //            Length = item.Length,
                    //            Root = item.DirectoryName

                    //        };
                    //        DirectoryInf.Add(f);

                    //    }
                    //    serverMessage.Datas = new Data[count];
                    //    for (int i = 0; i < count; i++)
                    //    {
                    //        serverMessage.Datas[i] = new Data()
                    //        {
                    //            Name = DirectoryInf[i].Name,
                    //            Length = DirectoryInf[i].Length,
                    //            Root = DirectoryInf[i].Root
                    //        };
                    //    }
                    //}
                    return serverMessage;
            }
            catch
            {
                MessageBox.Show("Выберите путь!");
                return null;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var deleted = (sender as Button).DataContext as DirectoryInfos;
            DirectoryInf.Remove(deleted);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Server server = new Server();
            Thread thread = new Thread(new ThreadStart(() =>
            {
                server.SendString("2", DirectoryInf);
            }));
            thread.Start();
            MessageBox.Show("Успешно сохранено");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dslg = new System.Windows.Forms.FolderBrowserDialog();
            dslg.ShowDialog();
            _path = dslg.SelectedPath;
            if (_path != string.Empty)
            {
                DirectoryInfo dirInf = new DirectoryInfo(_path);
                var directoryInfos = new DirectoryInfos()
                {
                    Name = dirInf.Name,
                    Time = Convert.ToDateTime(timePicker.Value).ToString("t")
                };
                DirectoryInf.Add(directoryInfos);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid1.Columns[3].Visibility = Visibility.Hidden;
            dataGrid1.Columns[4].Header = "Время";
            timePicker.Value = DateTime.Now;
            Server server = new Server();
            Thread thread = new Thread(new ThreadStart(() =>
                {
                    string result = server.SendString("1",DirectoryInf);
                    Dispatcher.Invoke(() => 
                    {
                        var mes = JsonConvert.DeserializeObject<DirectoryInfos[]>(result);
                        foreach(var item in mes)
                        {
                            var f = new DirectoryInfos()
                            {
                                Name = item.Name,
                                Time = item.Time
                            };
                            DirectoryInf.Add(f);
                        }
                    });
                }));
            thread.Start();
        }
    }
}
