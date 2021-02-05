using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanService
{
    public partial class Service1 : ServiceBase
    {
        Server server;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            server = new Server();
            Thread server1Thread = new Thread(new ThreadStart(server.ServerStart));
            server1Thread.Start();
        }

        protected override void OnStop()
        {
            
        }
    }

    
}
