using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_Worker
{
    public class Client
    {
        public string Identity { get; set; }
        public Socket Socket { get; private set; }
        public Client(Socket socket)
        {
            this.Socket = socket;
        }
    }
}
