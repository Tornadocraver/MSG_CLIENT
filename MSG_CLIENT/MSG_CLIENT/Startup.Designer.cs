namespace MSG_CLIENT
{
    partial class Startup
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.groupPeer = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioPeer = new System.Windows.Forms.RadioButton();
            this.radioServ = new System.Windows.Forms.RadioButton();
            this.groupServ = new System.Windows.Forms.GroupBox();
            this.textSPort = new System.Windows.Forms.TextBox();
            this.textSIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textSName = new System.Windows.Forms.TextBox();
            this.checkPass = new System.Windows.Forms.CheckBox();
            this.passWord = new Micahz.Controls.PassBox();
            this.groupPeer.SuspendLayout();
            this.groupServ.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(319, 79);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(105, 78);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // groupPeer
            // 
            this.groupPeer.Controls.Add(this.textBox3);
            this.groupPeer.Controls.Add(this.textBox2);
            this.groupPeer.Controls.Add(this.label3);
            this.groupPeer.Controls.Add(this.label2);
            this.groupPeer.Controls.Add(this.label1);
            this.groupPeer.Controls.Add(this.textBox1);
            this.groupPeer.Enabled = false;
            this.groupPeer.Location = new System.Drawing.Point(36, 5);
            this.groupPeer.Name = "groupPeer";
            this.groupPeer.Size = new System.Drawing.Size(253, 80);
            this.groupPeer.TabIndex = 1;
            this.groupPeer.TabStop = false;
            this.groupPeer.Text = "Peer-to-Peer";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(87, 51);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(160, 20);
            this.textBox3.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(87, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Peer Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Peer IP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Screen Name:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 20);
            this.textBox1.TabIndex = 0;
            // 
            // radioPeer
            // 
            this.radioPeer.AutoSize = true;
            this.radioPeer.Location = new System.Drawing.Point(12, 39);
            this.radioPeer.Name = "radioPeer";
            this.radioPeer.Size = new System.Drawing.Size(14, 13);
            this.radioPeer.TabIndex = 3;
            this.radioPeer.UseVisualStyleBackColor = true;
            // 
            // radioServ
            // 
            this.radioServ.AutoSize = true;
            this.radioServ.Checked = true;
            this.radioServ.Location = new System.Drawing.Point(13, 125);
            this.radioServ.Name = "radioServ";
            this.radioServ.Size = new System.Drawing.Size(14, 13);
            this.radioServ.TabIndex = 4;
            this.radioServ.TabStop = true;
            this.radioServ.UseVisualStyleBackColor = true;
            this.radioServ.CheckedChanged += new System.EventHandler(this.radioServ_CheckedChanged);
            // 
            // groupServ
            // 
            this.groupServ.Controls.Add(this.textSPort);
            this.groupServ.Controls.Add(this.textSIP);
            this.groupServ.Controls.Add(this.label4);
            this.groupServ.Controls.Add(this.label5);
            this.groupServ.Controls.Add(this.label6);
            this.groupServ.Controls.Add(this.textSName);
            this.groupServ.Location = new System.Drawing.Point(36, 91);
            this.groupServ.Name = "groupServ";
            this.groupServ.Size = new System.Drawing.Size(253, 80);
            this.groupServ.TabIndex = 6;
            this.groupServ.TabStop = false;
            this.groupServ.Text = "Client-to-Server";
            // 
            // textSPort
            // 
            this.textSPort.Location = new System.Drawing.Point(87, 51);
            this.textSPort.Name = "textSPort";
            this.textSPort.Size = new System.Drawing.Size(160, 20);
            this.textSPort.TabIndex = 5;
            this.textSPort.Text = "8023";
            // 
            // textSIP
            // 
            this.textSIP.Location = new System.Drawing.Point(87, 32);
            this.textSIP.Name = "textSIP";
            this.textSIP.Size = new System.Drawing.Size(160, 20);
            this.textSIP.TabIndex = 4;
            this.textSIP.Text = "104.231.90.168";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Server Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Server IP:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Screen Name:";
            // 
            // textSName
            // 
            this.textSName.Location = new System.Drawing.Point(87, 13);
            this.textSName.Name = "textSName";
            this.textSName.Size = new System.Drawing.Size(160, 20);
            this.textSName.TabIndex = 0;
            // 
            // checkPass
            // 
            this.checkPass.AutoSize = true;
            this.checkPass.Location = new System.Drawing.Point(310, 21);
            this.checkPass.Name = "checkPass";
            this.checkPass.Size = new System.Drawing.Size(123, 17);
            this.checkPass.TabIndex = 7;
            this.checkPass.Text = "Password Encrypted";
            this.checkPass.UseVisualStyleBackColor = true;
            this.checkPass.CheckedChanged += new System.EventHandler(this.checkPass_CheckedChanged);
            // 
            // passWord
            // 
            this.passWord.Enabled = false;
            this.passWord.Location = new System.Drawing.Point(310, 40);
            this.passWord.Name = "passWord";
            this.passWord.PasswordChar = '*';
            this.passWord.Size = new System.Drawing.Size(123, 20);
            this.passWord.TabIndex = 8;
            this.passWord.Visible = false;
            // 
            // Startup
            // 
            this.AcceptButton = this.buttonConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 182);
            this.Controls.Add(this.passWord);
            this.Controls.Add(this.checkPass);
            this.Controls.Add(this.groupServ);
            this.Controls.Add(this.radioServ);
            this.Controls.Add(this.radioPeer);
            this.Controls.Add(this.groupPeer);
            this.Controls.Add(this.buttonConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Startup";
            this.Text = "Msg Client Connector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Startup_FormClosed);
            this.Load += new System.EventHandler(this.Startup_Load);
            this.groupPeer.ResumeLayout(false);
            this.groupPeer.PerformLayout();
            this.groupServ.ResumeLayout(false);
            this.groupServ.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.GroupBox groupPeer;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioPeer;
        private System.Windows.Forms.RadioButton radioServ;
        private System.Windows.Forms.GroupBox groupServ;
        private System.Windows.Forms.TextBox textSPort;
        private System.Windows.Forms.TextBox textSIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textSName;
        private System.Windows.Forms.CheckBox checkPass;
        private Micahz.Controls.PassBox passWord;
    }
}