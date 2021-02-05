using Cleaner.Model;
using Cleaner.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Navigation;

namespace Cleaner
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<DiskInfo> _diskInfos;
        private List<DirectoryInfo> _directories;
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public List<DirectoryInfo> Directories
        {
            get => _directories;
            set
            {
                _directories = value;
                OnPropertyChanged();
            }
        }

        public List<DiskInfo> DiskInfos
        {
            get => _diskInfos;
            set
            {
                _diskInfos = value;
                OnPropertyChanged();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DiskInfos = DriveInfo.GetDrives().Select(s => new DiskInfo()
            {
                Name = s.Name,
                TotalSize = s.TotalSize,
                TotalFreeSpace = s.TotalFreeSpace,
                DriveType = s.DriveType,
                VolumeLabel = s.VolumeLabel
            } 
            ).ToList();
            DataContext = this;
    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DirectoryWindow directoryWindow = new DirectoryWindow { Owner = this };
            directoryWindow.Show();
        }
    }
}
