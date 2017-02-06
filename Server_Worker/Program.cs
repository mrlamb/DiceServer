using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MRLamb.StringUtils;

namespace Server_Worker
{
    class Server
    {
        List<Client> Listeners = new List<Client>();
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
            Console.WriteLine("Enter LIST to view connections or EXIT to exit.");
            do
            {
                
                string command = Console.ReadLine();
                if (command.Equals("LIST"))
                {
                    foreach(Client client in Listeners)
                    {
                        Console.WriteLine(client.Identity);
                    }
                }
                else if (command.Equals("EXIT"))
                {
                    break;
                }
                
            } while (true);
        }

        private void OnConnectCallback(IAsyncResult ar)
        {

            Socket listener = (Socket)ar.AsyncState;
            Socket socket = listener.EndAccept(ar);
            Client client = new Client(socket);
            
            Listeners.Add(client);
            Console.WriteLine("Client #{0} connected on {1}", Clients, client.Socket.RemoteEndPoint);
            WaitForData(client.Socket);
            string request = "IDENTIFY";
            client.Socket.Send(Encoding.ASCII.GetBytes(request));

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
            Client client = Listeners.Find(a => a.Socket.Equals(socket));
            try
            {
                int bytesRec = socket.EndReceive(ar);
                if (bytesRec > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRec);
                    if (message.Contains("ROLL"))
                        Propagate(message);
                    else if (message.Contains("IDENTITY"))
                        client.Identity = message.Substring(message.IndexOf(' ') + 1);
                        

                    AsyncCallback receiveData = new AsyncCallback(OnReceive);
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveData, socket);
                }
            }
            catch (SocketException ex)
            {
                socket = null;
            }
        }

        private void Propagate(string message)
        {
            foreach (Client client in Listeners)
            {
                if (client.Socket != null && client.Socket.Connected)
                {
                    message = message.TrimInnerWhitespace();
                    client.Socket.Send(Encoding.ASCII.GetBytes(message.Substring(message.IndexOf(' ')+1)));
                }
            }
        }
    }
}
