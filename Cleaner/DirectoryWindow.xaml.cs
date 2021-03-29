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
            if (search.Text != string.Empty)
            {
                DirectoryInfo dirInf = new DirectoryInfo(search.Text);
                var directoryInfos = new DirectoryInfos()
                {
                    Name = dirInf.Name,
                    Root = search.Text,
                    Time = Convert.ToDateTime(timePicker.Value).ToString("t")
                };
                DirectoryInf.Add(directoryInfos);
            }
            else
            {
                MessageBox.Show("Выберите путь");
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
                    Root = _path,
                    Time = Convert.ToDateTime(timePicker.Value).ToString("t")
                };

                if (!DirectoryInf.Contains(directoryInfos))
                {
                    DirectoryInf.Add(directoryInfos);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid1.Columns[2].Visibility = Visibility.Hidden;
            dataGrid1.Columns[3].Header = "Расположение каталога";
            dataGrid1.Columns[4].Header = "Время";
            timePicker.Value = DateTime.Now;
            Server server = new Server();
            Thread thread = new Thread(new ThreadStart(() =>
                {
                    string result = string.Empty;
                    try
                    {
                        result = server.SendString("1", DirectoryInf);
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(string.Format("Ошибка подключения {0}", f));
                        
                    }
                    Dispatcher.Invoke(() => 
                    {
                        var mes = JsonConvert.DeserializeObject<DirectoryInfos[]>(result);
                        foreach(var item in mes)
                        {
                            var directory = new DirectoryInfos()
                            {
                                Name = item.Name,
                                Time = item.Time,
                                Root = item.Root
                            };
                            DirectoryInf.Add(directory);
                        }
                    });
                }));
            thread.Start();
        }

        
    }
}
