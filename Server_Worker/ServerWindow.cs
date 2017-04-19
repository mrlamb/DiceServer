using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Worker
{
    public partial class ServerWindow : Form
    {
        List<Client> clients = new List<Client>();
        public ServerWindow()
        {
            InitializeComponent();
            
        }

        public void AddToDataGridView(Client client)
        {
            clients.Add(client);
            dgvClientList.DataSource = null;
            dgvClientList.DataSource = clients;

           
        }

        public void RemoveFromDataGridView(Client client)
        {
            dgvClientList.DataSource = null;

            clients.Remove(client);
            dgvClientList.DataSource = clients;
            
            
        }
    }
}
