namespace DiceClient
{
    partial class DiceForm
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
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lbOutput = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnD20 = new System.Windows.Forms.Button();
            this.btnD10 = new System.Windows.Forms.Button();
            this.btnD8 = new System.Windows.Forms.Button();
            this.btnD6 = new System.Windows.Forms.Button();
            this.btnD4 = new System.Windows.Forms.Button();
            this.btnD100 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(13, 13);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(122, 20);
            this.txtHost.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(142, 11);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lbOutput
            // 
            this.lbOutput.FormattingEnabled = true;
            this.lbOutput.Location = new System.Drawing.Point(13, 40);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(204, 212);
            this.lbOutput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Character Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(227, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 4;
            // 
            // btnD20
            // 
            this.btnD20.Enabled = false;
            this.btnD20.Location = new System.Drawing.Point(227, 57);
            this.btnD20.Name = "btnD20";
            this.btnD20.Size = new System.Drawing.Size(38, 23);
            this.btnD20.TabIndex = 5;
            this.btnD20.Text = "D20";
            this.btnD20.UseVisualStyleBackColor = true;
            this.btnD20.Click += new System.EventHandler(this.btnD20_Click);
            // 
            // btnD10
            // 
            this.btnD10.Enabled = false;
            this.btnD10.Location = new System.Drawing.Point(271, 56);
            this.btnD10.Name = "btnD10";
            this.btnD10.Size = new System.Drawing.Size(38, 23);
            this.btnD10.TabIndex = 6;
            this.btnD10.Text = "D10";
            this.btnD10.UseVisualStyleBackColor = true;
            this.btnD10.Click += new System.EventHandler(this.btnD10_Click);
            // 
            // btnD8
            // 
            this.btnD8.Enabled = false;
            this.btnD8.Location = new System.Drawing.Point(227, 86);
            this.btnD8.Name = "btnD8";
            this.btnD8.Size = new System.Drawing.Size(38, 23);
            this.btnD8.TabIndex = 7;
            this.btnD8.Text = "D8";
            this.btnD8.UseVisualStyleBackColor = true;
            this.btnD8.Click += new System.EventHandler(this.btnD8_Click);
            // 
            // btnD6
            // 
            this.btnD6.Enabled = false;
            this.btnD6.Location = new System.Drawing.Point(271, 86);
            this.btnD6.Name = "btnD6";
            this.btnD6.Size = new System.Drawing.Size(38, 23);
            this.btnD6.TabIndex = 8;
            this.btnD6.Text = "D6";
            this.btnD6.UseVisualStyleBackColor = true;
            this.btnD6.Click += new System.EventHandler(this.btnD6_Click);
            // 
            // btnD4
            // 
            this.btnD4.Enabled = false;
            this.btnD4.Location = new System.Drawing.Point(227, 115);
            this.btnD4.Name = "btnD4";
            this.btnD4.Size = new System.Drawing.Size(38, 23);
            this.btnD4.TabIndex = 9;
            this.btnD4.Text = "D4";
            this.btnD4.UseVisualStyleBackColor = true;
            this.btnD4.Click += new System.EventHandler(this.btnD4_Click);
            // 
            // btnD100
            // 
            this.btnD100.Enabled = false;
            this.btnD100.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnD100.Location = new System.Drawing.Point(271, 115);
            this.btnD100.Name = "btnD100";
            this.btnD100.Size = new System.Drawing.Size(38, 23);
            this.btnD100.TabIndex = 10;
            this.btnD100.Text = "D100";
            this.btnD100.UseVisualStyleBackColor = true;
            this.btnD100.Click += new System.EventHandler(this.btnD100_Click);
            // 
            // DiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 261);
            this.Controls.Add(this.btnD100);
            this.Controls.Add(this.btnD4);
            this.Controls.Add(this.btnD6);
            this.Controls.Add(this.btnD8);
            this.Controls.Add(this.btnD10);
            this.Controls.Add(this.btnD20);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbOutput);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtHost);
            this.Name = "DiceForm";
            this.Text = "Dice Roller";
            this.Load += new System.EventHandler(this.DiceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox lbOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnD20;
        private System.Windows.Forms.Button btnD10;
        private System.Windows.Forms.Button btnD8;
        private System.Windows.Forms.Button btnD6;
        private System.Windows.Forms.Button btnD4;
        private System.Windows.Forms.Button btnD100;
    }
}

