using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaner.Model
{
    public class DirectoryList
    {
        public Directories[] Directories { get; set; }
    }
    public class Directories
    {
        public string Name { get; set; }
        public string Time { get; set; }
    }
}
