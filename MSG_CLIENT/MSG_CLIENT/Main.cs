using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Security;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MSG_CLIENT
{
    public partial class Main : Form
    {
        #region Varialbes
        MsgClient cli;
        int mX = 0;
        int mY = 0;
        int changeMsg = 1;
        ToolTip tt = new ToolTip();
        Thread connector;
        Boolean mouseDown = false;
        Boolean Clients_Right = false;
        Dictionary<Client, Color> clientColors = new Dictionary<Client, Color>();
        int cm_index = -1;
        MenuItem cm_copy = new MenuItem("Copy Message");
        ContextMenu cm_msg = new ContextMenu();
        #endregion

        public Main(string _ip, int _port, string _sName, SecureString _pass, Boolean p2p)
        {
            cli = new MsgClient(_ip, _port, _sName, _pass);
            cli.MessageReceived += cli_MsgReceived;
            cli.NewClient += cli_NewClient;
            cli.NameChanged += cli_NameChanged;
            cli.ClientDisconnected += cli_ClientDis;
            cli.CustomCommand += cli_Custom;
            cm_copy.Click += Right_Copy;
            cm_msg.MenuItems.Add(cm_copy);
            InitializeComponent();
        }

        #region MSG Events
        private void cli_MsgReceived(String _msg, Client _cli)
        {
            if (_cli == null)
            {
                _cli = cli.Clients.Find(c => c.Name.Contains(_msg.Substring(1, _msg.IndexOf(":") - 1)));
                if (_cli == null)
                {
                    if (_msg.StartsWith("You:"))
                    {
                        if (InvokeRequired)
                            Invoke((MethodInvoker)delegate { listMessage.AddColorItem(_msg, clientColors[cli.Clients.Find(b => b.IP.ToString() == cli.ChatIP && b.Name == cli.ScreenName)], listMessage.ForeColor); });
                        else
                            listMessage.AddColorItem(_msg, clientColors[cli.Clients.Find(b => b.IP.ToString() == cli.ChatIP && b.Name == cli.ScreenName)], listMessage.ForeColor);
                        return;
                    }
                    else if (_msg.StartsWith("=>"))
                    {
                        if (InvokeRequired)
                            Invoke((MethodInvoker)delegate { listMessage.AddColorItem(_msg, listMessage.BackColor, listMessage.ForeColor); });
                        else
                            listMessage.Items.Add(_msg);
                        return;
                    }
                    else if (_msg.StartsWith("Server:"))
                    {
                        if (InvokeRequired)
                            Invoke((MethodInvoker)delegate { listMessage.AddColorItem(_msg, Color.FromArgb(255, 179, 149, 0), listMessage.ForeColor); });
                        else
                            listMessage.AddColorItem(_msg, Color.FromArgb(255, 179, 149, 0), listMessage.ForeColor);
                        return;
                    }
                    if (InvokeRequired)
                        Invoke((MethodInvoker)delegate { listMessage.Items.Add(_msg); });
                    else
                        listMessage.Items.Add(_msg);
                    return;
                }
            }
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { if (_msg.StartsWith("=>")) { listMessage.Items.Add(_msg); } else { listMessage.AddColorItem(_msg, clientColors[_cli], listMessage.ForeColor); } });
            else
                if (_msg.StartsWith("=>")) { listMessage.Items.Add(_msg); } else { listMessage.AddColorItem(_msg, clientColors[_cli], listMessage.ForeColor); }
        }
        private void cli_NewClient(Client _cli)
        {
            if (changeMsg == 1)
            {
                UpdateStatus("Connecting to server...");
                changeMsg = 0;
            }
            if (_cli.Name == "Server")
            {
                clientColors.Add(_cli, Color.FromArgb(255, 179, 149, 0));
                if (InvokeRequired)
                    Invoke((MethodInvoker)delegate { listClients.AddColorItem(" Server (" + _cli.IP.ToString() + ")", Color.FromArgb(255, 179, 149, 0), listMessage.ForeColor); });
                else
                    listClients.AddColorItem(" Server (" + _cli.IP.ToString() + ")", Color.FromArgb(255, 179, 149, 0), listMessage.ForeColor);
            }
            else
            {
                clientColors.Add(_cli, UserColor(clientColors.Count + 1));
                string msg = " " + _cli.Name + " (" + _cli.IP.ToString() + ")";
                if (msg.Contains(_cli.IP.ToString()))
                    msg = (msg.Substring(0, msg.IndexOf("("))) + "(You)";
                if (InvokeRequired)
                    Invoke((MethodInvoker)delegate { listClients.AddColorItem(msg, clientColors[_cli], listClients.ForeColor); });
                else
                    listClients.AddColorItem(msg, clientColors[_cli], listClients.ForeColor);
            }
        }
        private void cli_NameChanged(String _old, Client _cli)
        {
            string pat = _old + " (" + _cli.IP.ToString() + ")";
            int index = listClients.Items.IndexOf(pat) - 1;
            clientColors.FirstOrDefault(c => c.Key.Name == _old && c.Key.IP == _cli.IP).Key.Rename(_cli.Name);
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { listClients.Items[index] = _cli.Name + " (" + _cli.IP.ToString() + ")"; });
            else
                listClients.Items[index] = _cli.Name + " (" + _cli.IP.ToString() + ")";
        }
        private void cli_ClientDis(Client _cli)
        {
            Color c = clientColors[_cli];
            clientColors.FirstOrDefault(v => v.Value == c).Key.State = ClientState.Disconnected;
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { listClients.Items.Remove(_cli.Name + " (" + _cli.IP.ToString() + ")"); });
            else
                listClients.Items.Remove(_cli.Name + " (" + _cli.IP.ToString() + ")");
        }
        private void cli_Custom(String _cust)
        {

        }
        #endregion

        private void UpdateStatus(string _status)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { toolConnStat.Text = "Status: " + _status; });
            else
                toolConnStat.Text = "Status: " + _status;
        }

        private Color UserColor(int _index)
        {
            check:
            if (_index > 10)
            {
                _index = _index - 10;
                goto check;
            }
            Color c = Color.Black;
            if (_index == 1)
                c = Color.FromArgb(255, 128, 0, 0);
            else if (_index == 2)
                c = Color.FromArgb(255, 128, 53, 0);
            else if (_index == 3)
                c = Color.FromArgb(255, 128, 106, 0);
            else if (_index == 4)
                c = Color.FromArgb(255, 96, 128, 0);
            else if (_index == 5)
                c = Color.FromArgb(255, 0, 128, 64);
            else if (_index == 6)
                c = Color.FromArgb(255, 0, 128, 117);
            else if (_index == 7)
                c = Color.FromArgb(255, 0, 32, 128);
            else if (_index == 8)
                c = Color.FromArgb(255, 74, 0, 128);
            else if (_index == 9)
                c = Color.FromArgb(255, 128, 0, 127);
            else if (_index == 10)
                c = Color.FromArgb(255, 128, 0, 42);
            return c;
        }

        #region Form Events
        private void Main_Load(object sender, EventArgs e)
        {
            listMessage.Items.Add(Environment.NewLine);
            listClients.BlockedSelections.Clear();
            listClients.BlockedSelections.Add(-1);
            connector = new Thread(() =>
            {
                UpdateStatus("Connecting to server...");
                int conAtt = 0;
                st:
                try
                {
                    cli.Connect();
                    if (cli.Connected)
                    {
                        UpdateStatus("Connected to Server.");
                        CreateGraphics().DrawRectangle(new Pen(Color.Black, 2), ClientRectangle);
                    }
                }
                catch (ConnectionFailed ex) { UpdateStatus("Connection Attempt #" + conAtt++ + " Failed. Trying Again..."); if (cli.Connecting) goto st; }
            }); connector.Start();
            if (cli.Connected)
            {
                Thread t = new Thread(() => { Thread.Sleep(1000); CreateGraphics().DrawRectangle(new Pen(Color.Black, 2), ClientRectangle); });
                t.Start();
                labelTitle.Text = "Message Client - " + Misc.RemoteIP();
            }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateStatus("Disconnecting from Server...");
            if (cli.Connected || cli.Connecting)
                cli.Disconnect();
            cli.MessageReceived -= cli_MsgReceived;
            cli.NewClient -= cli_NewClient;
            cli.NameChanged -= cli_NameChanged;
            cli.ClientDisconnected -= cli_ClientDis;
            cli.CustomCommand -= cli_Custom;
            UpdateStatus("Not Connected");
        }

        private void panelBorder_MouseDown(object sender, MouseEventArgs e)
        {
            mX = MousePosition.X - Bounds.X;
            mY = MousePosition.Y - Bounds.Y;
            mouseDown = true;
        }
        private void panelBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                int x = MousePosition.X - mX;
                int y = MousePosition.Y - mY;
                SetDesktopLocation(x, y);
            }
        }
        private void panelBorder_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void panelBorder_Click(object sender, EventArgs e)
        {
            Main_Click(sender, e);
        }
        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            panelBorder_MouseDown(sender, e);
        }
        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            panelBorder_MouseMove(sender, e);
        }
        private void labelTitle_MouseUp(object sender, MouseEventArgs e)
        {
            panelBorder_MouseUp(sender, e);
        }
        private void labelTitle_Click(object sender, EventArgs e)
        {
            panelBorder_Click(sender, e);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Main_Click(sender, e);
        }
        private void buttonMin_MouseHover(object sender, EventArgs e)
        {
            tt = new ToolTip();
            tt.Show("Minimize to system tray", this, PointToClient(Cursor.Position).X + 16, PointToClient(Cursor.Position).Y + 16, 1000);
        }
        private void buttonClose_MouseHover(object sender, EventArgs e)
        {
            tt = new ToolTip();
            tt.Show("Exit", this, PointToClient(Cursor.Position).X + 16, PointToClient(Cursor.Position).Y + 16, 1000);
        }
        private void Main_Activated(object sender, EventArgs e)
        {
            CreateGraphics().DrawRectangle(new Pen(Color.Black, 2), ClientRectangle);
        }
        private void Main_Deactivate(object sender, EventArgs e)
        {
            CreateGraphics().DrawRectangle(new Pen(Color.LightGreen, 2), ClientRectangle);
        }
        private void Main_Click(object sender, EventArgs e)
        {
            listMessage.SelectedIndex = -1;
            listClients.SelectedIndex = -1;
        }

        private void listClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listClients.SelectedIndex != -1 && listClients.SelectedIndex != 0)
            {
                buttonPM.Enabled = true;
                buttonPing.Enabled = true;
                Clients_Right = true;
            }
            else
            {
                buttonPM.Enabled = false;
                buttonPing.Enabled = false;
                Clients_Right = false;
            }
        }
        private void listMessage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listMessage.IndexFromPoint(e.Location);
                if (index != -1 && index != 0 && index != 1)
                {
                    listMessage.SelectedIndex = index;
                    cm_msg.Show(listMessage, e.Location);
                    cm_index = index;
                }
            }
        }
        private void Right_Copy(object sender, EventArgs e)
        {
            if (cm_index != -1)
                Clipboard.SetText(listMessage.Items[cm_index].ToString());
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            toolConnStat.Text = "Status: Sending Message...";
            try { cli.Write(textMsg.Text); }
            catch (MsgException ex) { MessageBox.Show("An error occured while trying to send message. Details:\r\n\r\n" + ex.Reason, "SYS_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            textMsg.Clear();
            toolConnStat.Text = "Status: Connected & Ready";
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void buttonPM_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedIndex != -1)
            {
                string selected = listClients.SelectedItem.ToString().Trim();
                string tarName = selected.Substring(0, selected.IndexOf(" ("));
                string tarIP = selected.Replace(tarName + " (", "").Replace(")", "");
                Client c = cli.Clients.Find(cl => tarIP.Equals(cl.IP.ToString()) && cl.Name == tarName);
                string pm = Interaction.InputBox("Please type your personal message:", "PM", "");
                cli.PM(pm, c);
            }
        }
        private void buttonPing_Click(object sender, EventArgs e)
        {
            if (listClients.SelectedIndex != -1)
            {
                string selected = listClients.SelectedItem.ToString().Trim();
                string tarName = selected.Substring(0, selected.IndexOf(" ("));
                string tarIP = selected.Replace(tarName + " (", "").Replace(")", "");
                Client c = cli.Clients.Find(cl => tarIP.Equals(cl.IP.ToString()) && cl.Name == tarName);
                string lat;
                if (cli.Ping(c, out lat))
                    MessageBox.Show("Ping successful. Round trip: " + lat + " microseconds.", "PING_SUCCESS_" + lat, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Ping NOT successful. Rount trip: " + lat, "PING_FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}