using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MRLamb.StringUtils;
using System.Timers;
using System.Windows.Forms;

namespace Server_Worker
{
    class Server
    {
        List<Client> Listeners = new List<Client>();
        static ServerWindow serverWindow;
        int Clients = 0;

        System.Timers.Timer timer = new System.Timers.Timer(10000);
        
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Server diceServer = new Server();
            diceServer.Start();

            Application.Run(serverWindow);
        }


        private void Start()
        {

            serverWindow = new ServerWindow();

            timer.Elapsed += new ElapsedEventHandler(HeartbeatCheck);
            timer.Enabled = false;
            timer.AutoReset = true;

            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] localAddress = ipEntry.AddressList;
            IPEndPoint ipEndPoint = new IPEndPoint(Array.Find(localAddress, a => a.AddressFamily == AddressFamily.InterNetwork), 11000);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEndPoint);
            listener.Listen(100);
            Console.WriteLine("IP Address: {0}", ipEndPoint.Address.ToString());

            Console.WriteLine("Server started. Awaiting connections..");

            listener.BeginAccept(new AsyncCallback(OnConnectCallback), listener);
            Console.WriteLine("Enter LIST to view connections or EXIT to exit.");

            //do
            //{

            //    string command = Console.ReadLine();
            //    if (command.Equals("LIST"))
            //    {
            //        foreach (Client client in Listeners)
            //        {
            //            Console.WriteLine(client.Identity);
            //        }
            //    }
            //    else if (command.Equals("EXIT"))
            //    {
            //        break;
            //    }

            //} while (true);
        }

        private void HeartbeatCheck(object sender, ElapsedEventArgs e)
        {
            List<Client> remove = new List<Client>();
            foreach (Client client in Listeners)
            {
                if (!client.Socket.Connected)
                {
                    serverWindow.BeginInvoke((Action)(() => { serverWindow.RemoveFromDataGridView(client); }));

                    remove.Add(client);
                }
            }
            if (remove.Count > 0)
            {
                foreach (Client client in remove)
                {
                    Clients--;
                    Listeners.Remove(client);
                    Console.WriteLine(client.Identity + " disconnected.");
                }
            }
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
            Clients++;
            timer.Enabled = true;
            serverWindow.BeginInvoke((Action)(() => { serverWindow.AddToDataGridView(client); }));

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
                    client.Socket.Send(Encoding.ASCII.GetBytes(message.Substring(message.IndexOf(' ') + 1)));
                }
            }
        }
    }
}
