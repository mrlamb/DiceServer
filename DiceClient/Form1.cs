using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Microsoft.VisualBasic;


delegate void AddMessage(string newstring);
delegate void ToggleButton(Button button);
delegate void ToggleConnectionInfo();
delegate void SetTextName();

namespace DiceClient
{
    public partial class DiceForm : Form
    {
        private event AddMessage m_addMessage;
        private event ToggleButton m_toggleButton;
        private event ToggleConnectionInfo m_toggleConnectionInfo;
        private event SetTextName m_setTextName;
        string Host;
        Socket socket;
        Random rnd = new Random();
        public string Identity { get; private set; }

        //Heartbeat timer to check connection
        System.Timers.Timer timer = new System.Timers.Timer(30000);

        public DiceForm()
        {
            InitializeComponent();
            m_addMessage = new AddMessage(OnAddMessage);
            m_toggleButton = new ToggleButton(OnToggleButton);
            m_setTextName = new SetTextName(OnSetTextName);
            m_toggleConnectionInfo = new ToggleConnectionInfo(OnToggleConnectionInfo);

            timer.Elapsed += new ElapsedEventHandler(HeartbeatCheck);
            timer.Enabled = false;
            timer.AutoReset = true;
        }

        private void OnSetTextName()
        {
            txtName.Text = Identity;
        }

        private void HeartbeatCheck(object source, ElapsedEventArgs e)
        {
            if (!socket.Connected)
                ConnectSocket();
        }

        private void OnToggleConnectionInfo()
        {
            if (lblConnectionString.Text.Equals("Connected"))
                lblConnectionString.Text = "Disconnected";
            else
                lblConnectionString.Text = "Connected";

            if (lblConnectionString.Text.Equals("Connected"))
                lblStatusIcon.Image = Properties.Resources.transp_green;
            else
                lblStatusIcon.Image = Properties.Resources.transp_red;
        }



        private void OnToggleButton(Button button)
        {
            button.Enabled = !button.Enabled;
        }

        private void OnAddMessage(string newstring)
        {
            lbOutput.Items.Add(newstring);
            lbOutput.TopIndex = lbOutput.Items.Count - 1;
        }

        private void DiceForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\config.txt"))
            {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.txt");
                if (lines.Length > 0)
                {
                    foreach (string line in lines)
                    {
                        if (line.Contains("host"))
                        {
                            Host = line.Substring(line.IndexOf(":") + 1).Trim();
                            txtHost.Text = Host;
                        }
                    }
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ConnectSocket();

                if (socket.Connected)
                {
                    SetupReceiveCallback(socket);
                    ToggleButtons();
                    Invoke(m_toggleConnectionInfo);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = cursor;
        }

        private void ConnectSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(txtHost.Text), 11000);
            socket.Connect(ipe);
        }

        byte[] buffer = new byte[256];
        

        private void SetupReceiveCallback(Socket socket)
        {
            try
            {
                AsyncCallback receiveData = new AsyncCallback(OnReceiveData);
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, receiveData, socket);

            }
            catch (SocketException se)
            {
                MessageBox.Show("Socket Exception: " + se.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetupReceiveCallBack Exception: " + ex.Message);
            }
        }

        private void OnReceiveData(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;

            try
            {
                int bytesRec = socket.EndReceive(ar);
                if (bytesRec > 0)
                {
                    string recString = Encoding.ASCII.GetString(buffer, 0, bytesRec);
                    if (recString.Contains("IDENTIFY"))
                    {
                        string message = string.Empty;
                        if (string.IsNullOrEmpty(Identity))
                        {
                            do { Identity = Interaction.InputBox("Enter your identifier"); }
                            while (string.IsNullOrEmpty(Identity));
                        }
                        Invoke(m_setTextName);
                        message = "IDENTITY " + Identity;
                        socket.Send(Encoding.ASCII.GetBytes(message));
                        timer.Enabled = true;
                    }
                    else
                        Invoke(m_addMessage, new string[] { recString });

                    SetupReceiveCallback(socket);
                }
            }
            catch (SocketException se)
            {
                ToggleButtons();
                Invoke(m_toggleConnectionInfo);


                MessageBox.Show("A connection error has occurred.", "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnReceiveData Exception: " + ex.Message);
            }
        }

        
        private void ToggleButtons()
        {
            Invoke(m_toggleButton, btnConnect);
            Invoke(m_toggleButton, btnD20);
            Invoke(m_toggleButton, btnD10);
            Invoke(m_toggleButton, btnD8);
            Invoke(m_toggleButton, btnD6);
            Invoke(m_toggleButton, btnD4);
            Invoke(m_toggleButton, btnD100);
        }

        private void btnD20_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter a character name to roll");
                return;
            }
            if (socket.Connected)
            {
                string Message = "ROLL " + txtName.Text + " (D20): " + (rnd.Next(20) + 1).ToString();
                socket.Send(Encoding.ASCII.GetBytes(Message));
            }
        }

        private void btnD10_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter a character name to roll");
                return;
            }
            if (socket.Connected)
            {
                string Message = "ROLL " + txtName.Text + " (D10): " + (rnd.Next(10) + 1).ToString();
                socket.Send(Encoding.ASCII.GetBytes(Message));
            }
        }

        private void btnD8_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter a character name to roll");
                return;
            }
            if (socket.Connected)
            {
                string Message = "ROLL " + txtName.Text + " (D8): " + (rnd.Next(8) + 1).ToString();
                socket.Send(Encoding.ASCII.GetBytes(Message));
            }
        }

        private void btnD6_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter a character name to roll");
                return;
            }
            if (socket.Connected)
            {
                string Message = "ROLL " + txtName.Text + " (D6): " + (rnd.Next(6) + 1).ToString();
                socket.Send(Encoding.ASCII.GetBytes(Message));
            }
        }

        private void btnD4_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter a character name to roll");
                return;
            }
            if (socket.Connected)
            {
                string Message = "ROLL " + txtName.Text + " (D4): " + (rnd.Next(4) + 1).ToString();
                socket.Send(Encoding.ASCII.GetBytes(Message));
            }
        }

        private void btnD100_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(String.Empty))
            {
                MessageBox.Show("Enter a character name to roll");
                return;
            }
            if (socket.Connected)
            {
                string Message = "ROLL " + txtName.Text + " (D100): " + (rnd.Next(100) + 1).ToString();
                socket.Send(Encoding.ASCII.GetBytes(Message));
            }
        }
    }
}
