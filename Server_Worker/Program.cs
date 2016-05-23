using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_Worker
{
    class Server
    {
        Socket[] Listeners = new Socket[10];
        int Clients = 0;

        static void Main(string[] args)
        {
            Server diceServer = new Server();
            diceServer.Start();
        }

        private void Start()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] localAddress = ipEntry.AddressList;

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(Array.Find(localAddress, a => a.AddressFamily == AddressFamily.InterNetwork), 11000));
            listener.Listen(100);
            Console.WriteLine("IP Address: {0}", hostName);

            Console.WriteLine("Server started. Awaiting connections..");

            listener.BeginAccept(new AsyncCallback(OnConnectCallback), listener);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private void OnConnectCallback(IAsyncResult ar)
        {

            Socket listener = (Socket)ar.AsyncState;
            Socket client = listener.EndAccept(ar);
            Listeners[Clients] = client;
            Console.WriteLine("Client #{0} connected on {1}", Clients, client.RemoteEndPoint);
            WaitForData(Listeners[Clients]);

            ++Clients;


            listener.BeginAccept(new AsyncCallback(OnConnectCallback), listener);

        }

        private byte[] buffer = new byte[256];
        private void WaitForData(Socket socket)
        {

            if (socket.Connected)
            {
                AsyncCallback receiveData = new AsyncCallback(OnReceive);
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveData, socket);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try {
                int bytesRec = socket.EndReceive(ar);
                if (bytesRec > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRec);
                    Propagate(message);

                    AsyncCallback receiveData = new AsyncCallback(OnReceive);
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveData, socket);
                }
            }
            catch(SocketException ex)
            {
                socket = null;
            }
        }

        private void Propagate(string message)
        {
            foreach (Socket sock in Listeners)
            {
                if (sock != null && sock.Connected)
                {
                    sock.Send(Encoding.ASCII.GetBytes(message));
                }
            }
        }
    }
}
