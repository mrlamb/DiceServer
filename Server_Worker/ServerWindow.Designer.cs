namespace Server_Worker
{
    partial class ServerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblConnectedClients = new System.Windows.Forms.Label();
            this.dgvClientList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConnectedClients
            // 
            this.lblConnectedClients.AutoSize = true;
            this.lblConnectedClients.Location = new System.Drawing.Point(12, 34);
            this.lblConnectedClients.Name = "lblConnectedClients";
            this.lblConnectedClients.Size = new System.Drawing.Size(93, 13);
            this.lblConnectedClients.TabIndex = 1;
            this.lblConnectedClients.Text = "Connected Clients";
            // 
            // dgvClientList
            // 
            this.dgvClientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientList.Location = new System.Drawing.Point(13, 67);
            this.dgvClientList.Name = "dgvClientList";
            this.dgvClientList.Size = new System.Drawing.Size(305, 284);
            this.dgvClientList.TabIndex = 2;
            // 
            // ServerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 363);
            this.Controls.Add(this.dgvClientList);
            this.Controls.Add(this.lblConnectedClients);
            this.Name = "ServerWindow";
            this.Text = "ServerWindow";
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConnectedClients;
        private System.Windows.Forms.DataGridView dgvClientList;
    }
}