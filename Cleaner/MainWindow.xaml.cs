using Cleaner.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Cleaner
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<DiskInfo> _diskInfos;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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
            DiskInfos = DriveInfo.GetDrives().Select(s => new DiskInfo()).ToList();
            DataContext = this;
        }
    }
}
