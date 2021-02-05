using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanService
{
    class ServerMessage
    {
        public Data[] Datas { get; set; }
    }
    class Data
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public string Root { get; set; }
    }
}
