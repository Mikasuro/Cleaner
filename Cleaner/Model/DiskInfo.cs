using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cleaner.Model
{
    public class DiskInfo
    {
        public string Name { get; set; }
      
        public DriveType DriveType { get; set; }
        
        public long TotalFreeSpace { get; set; }
     
        public int TotalSize { get; set; }

        public int TotalSizeGb { get { return TotalSize / (1024 * 1024 * 1024); } }
        
        public string VolumeLabel { get; set; }
    }
}
