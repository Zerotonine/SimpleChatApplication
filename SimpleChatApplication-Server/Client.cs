using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SimpleChatApplication_Server
{
    class Client
    {
        internal TcpClient client { get; set; }
        internal StreamWriter sw { get; set; }
        internal StreamReader sr { get; set; }

        public Client(TcpClient client)
        {
            this.client = client;
            this.sw = new StreamWriter(client.GetStream());
            this.sr = new StreamReader(client.GetStream());
        }
    }
}
