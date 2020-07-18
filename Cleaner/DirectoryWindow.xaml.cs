using Cleaner.Model;
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
        private ObservableCollection<FileInfos> _fileInfos = new ObservableCollection<FileInfos>();

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public ObservableCollection<FileInfos> FileInfoss
        {
            get => _fileInfos;
            set
            {
                _fileInfos = value;
                OnPropertyChanged();
            }
        }

        public DirectoryWindow()
        {
            InitializeComponent();
            search.Focus();
            DataContext = this;
            comboTime.ItemsSource = Enumerable.Range(0, 24).Select(s => string.Format("{0}.00", s));
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = search.Text;

            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                var a = new FileInfos()
                {
                    Name = fileInf.Name,
                    Length = fileInf.Length
                };

                if (FileInfoss.FirstOrDefault() == null)
                {
                    FileInfoss.Add(a);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var deleted = (sender as Button).DataContext as FileInfos;
            string path = search.Text;
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                FileInfoss.Remove(deleted);
            }
        }
    }
}
