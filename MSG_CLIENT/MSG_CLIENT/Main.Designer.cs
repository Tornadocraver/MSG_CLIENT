namespace MSG_CLIENT
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonSend = new System.Windows.Forms.Button();
            this.textMsg = new System.Windows.Forms.TextBox();
            this.panelBorder = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonMin = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolConnStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPM = new System.Windows.Forms.Button();
            this.buttonPing = new System.Windows.Forms.Button();
            this.listClients = new MSG_CLIENT.ColorListBox();
            this.listMessage = new MSG_CLIENT.ColorListBox();
            this.panelBorder.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.Location = new System.Drawing.Point(561, 326);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(86, 40);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textMsg
            // 
            this.textMsg.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textMsg.Location = new System.Drawing.Point(12, 326);
            this.textMsg.Multiline = true;
            this.textMsg.Name = "textMsg";
            this.textMsg.Size = new System.Drawing.Size(543, 40);
            this.textMsg.TabIndex = 1;
            // 
            // panelBorder
            // 
            this.panelBorder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelBorder.Controls.Add(this.labelTitle);
            this.panelBorder.Controls.Add(this.buttonMin);
            this.panelBorder.Controls.Add(this.buttonClose);
            this.panelBorder.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelBorder.Location = new System.Drawing.Point(1, 1);
            this.panelBorder.Name = "panelBorder";
            this.panelBorder.Size = new System.Drawing.Size(656, 25);
            this.panelBorder.TabIndex = 3;
            this.panelBorder.Click += new System.EventHandler(this.panelBorder_Click);
            this.panelBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBorder_MouseDown);
            this.panelBorder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelBorder_MouseMove);
            this.panelBorder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelBorder_MouseUp);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelTitle.Location = new System.Drawing.Point(3, 3);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(135, 19);
            this.labelTitle.TabIndex = 6;
            this.labelTitle.Text = "Message Client";
            this.labelTitle.Click += new System.EventHandler(this.labelTitle_Click);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            this.labelTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseUp);
            // 
            // buttonMin
            // 
            this.buttonMin.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonMin.FlatAppearance.BorderSize = 0;
            this.buttonMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMin.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMin.Location = new System.Drawing.Point(605, 0);
            this.buttonMin.Name = "buttonMin";
            this.buttonMin.Size = new System.Drawing.Size(26, 25);
            this.buttonMin.TabIndex = 5;
            this.buttonMin.Text = "—";
            this.buttonMin.UseVisualStyleBackColor = false;
            this.buttonMin.Click += new System.EventHandler(this.buttonMin_Click);
            this.buttonMin.MouseHover += new System.EventHandler(this.buttonMin_MouseHover);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Red;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(631, 0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(26, 25);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseHover += new System.EventHandler(this.buttonClose_MouseHover);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.OldLace;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolConnStat});
            this.statusStrip1.Location = new System.Drawing.Point(1, 371);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(656, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolConnStat
            // 
            this.toolConnStat.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolConnStat.Name = "toolConnStat";
            this.toolConnStat.Size = new System.Drawing.Size(133, 17);
            this.toolConnStat.Text = "Status: Not Connected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(379, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Session:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonPM
            // 
            this.buttonPM.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonPM.Enabled = false;
            this.buttonPM.FlatAppearance.BorderSize = 0;
            this.buttonPM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPM.Location = new System.Drawing.Point(446, 279);
            this.buttonPM.Name = "buttonPM";
            this.buttonPM.Size = new System.Drawing.Size(98, 23);
            this.buttonPM.TabIndex = 7;
            this.buttonPM.Text = "PM";
            this.buttonPM.UseVisualStyleBackColor = false;
            this.buttonPM.Click += new System.EventHandler(this.buttonPM_Click);
            // 
            // buttonPing
            // 
            this.buttonPing.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonPing.Enabled = false;
            this.buttonPing.FlatAppearance.BorderSize = 0;
            this.buttonPing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPing.Location = new System.Drawing.Point(548, 279);
            this.buttonPing.Name = "buttonPing";
            this.buttonPing.Size = new System.Drawing.Size(98, 23);
            this.buttonPing.TabIndex = 8;
            this.buttonPing.Text = "Ping";
            this.buttonPing.UseVisualStyleBackColor = false;
            this.buttonPing.Click += new System.EventHandler(this.buttonPing_Click);
            // 
            // listClients
            // 
            this.listClients.AutoScroll = true;
            this.listClients.AutoWordWrap = true;
            this.listClients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.listClients.BlockedSelections = ((System.Collections.Generic.List<int>)(resources.GetObject("listClients.BlockedSelections")));
            this.listClients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listClients.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listClients.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listClients.ForeColor = System.Drawing.Color.Cyan;
            this.listClients.FormattingEnabled = true;
            this.listClients.HighlightColor = System.Drawing.Color.Chartreuse;
            this.listClients.ItemHeight = 14;
            this.listClients.Location = new System.Drawing.Point(446, 31);
            this.listClients.Name = "listClients";
            this.listClients.Size = new System.Drawing.Size(200, 242);
            this.listClients.TabIndex = 4;
            this.listClients.SelectedIndexChanged += new System.EventHandler(this.listClients_SelectedIndexChanged);
            // 
            // listMessage
            // 
            this.listMessage.AutoScroll = true;
            this.listMessage.AutoWordWrap = true;
            this.listMessage.BackColor = System.Drawing.SystemColors.MenuText;
            this.listMessage.BlockedSelections = ((System.Collections.Generic.List<int>)(resources.GetObject("listMessage.BlockedSelections")));
            this.listMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listMessage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listMessage.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listMessage.ForeColor = System.Drawing.SystemColors.Window;
            this.listMessage.FormattingEnabled = true;
            this.listMessage.HighlightColor = System.Drawing.Color.GreenYellow;
            this.listMessage.HorizontalScrollbar = true;
            this.listMessage.ItemHeight = 14;
            this.listMessage.Location = new System.Drawing.Point(12, 31);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(361, 280);
            this.listMessage.TabIndex = 2;
            this.listMessage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listMessage_MouseUp);
            // 
            // Main
            // 
            this.AcceptButton = this.buttonSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.ClientSize = new System.Drawing.Size(658, 394);
            this.Controls.Add(this.textMsg);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonPing);
            this.Controls.Add(this.buttonPM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listClients);
            this.Controls.Add(this.panelBorder);
            this.Controls.Add(this.listMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.Text = "Message Client";
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Click += new System.EventHandler(this.Main_Click);
            this.panelBorder.ResumeLayout(false);
            this.panelBorder.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textMsg;
        private ColorListBox listMessage;
        private System.Windows.Forms.Panel panelBorder;
        private System.Windows.Forms.Button buttonMin;
        private System.Windows.Forms.Button buttonClose;
        private ColorListBox listClients;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolConnStat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPM;
        private System.Windows.Forms.Button buttonPing;
        private System.Windows.Forms.Label labelTitle;
    }
}

