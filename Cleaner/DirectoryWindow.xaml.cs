using Cleaner.Model;
using Cleaner.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
            comboTime.ItemsSource = Enumerable.Range(0, 24).Select(s => string.Format("{0}.00", s));
            TcpIp server = new TcpIp();
            
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TcpIp server = new TcpIp();
            string path = search.Text;
            DirectoryInfo dirInf = new DirectoryInfo(path);
            if (Directory.Exists(path))
            {
                foreach (var item in dirInf.GetDirectories())
                {
                    var d = new DirectoryInfos()
                    {
                        Name = item.Name
                    };
                    DirectoryInf.Add(d);
                }
                foreach ( var item in dirInf.GetFiles())
                {
                    var f = new DirectoryInfos()
                    {
                        Name = item.Name,
                        Length = item.Length
                    };
                    DirectoryInf.Add(f);
                }
                server.ServerTCP(JsonConvert.SerializeObject(DirectoryInf));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var deleted = (sender as Button).DataContext as DirectoryInfos;
            DirectoryInfo directoryInfo = new DirectoryInfo(search.Text);
            
            //var deleted = (sender as Button).DataContext as FileInfos;
            //string path = search.Text;
            //FileInfo fileInf = new FileInfo(path);
            //if (fileInf.Exists)
            //{
            //    FileInfoss.Remove(deleted);
            //}
        }
    }
}
