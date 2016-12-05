using System;
using System.Security;
using System.Windows.Forms;

namespace MSG_CLIENT
{
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void radioServ_CheckedChanged(object sender, EventArgs e)
        {
            if (radioServ.Checked == true)
            {
                groupServ.Enabled = true;
                groupPeer.Enabled = false;
            }
            else
            {
                groupServ.Enabled = false;
                groupPeer.Enabled = true;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (radioServ.Checked == true)
            {
                if (!string.IsNullOrWhiteSpace(textSIP.Text) && !string.IsNullOrWhiteSpace(textSName.Text) && !string.IsNullOrWhiteSpace(textSPort.Text))
                {
                    SecureString s = new SecureString();
                    if (checkPass.Checked == true && !string.IsNullOrWhiteSpace(passWord.Text))
                        foreach (var c in passWord.Text.ToCharArray())
                            s.AppendChar(c);
                    else
                        foreach (var c in "EN-CON_12345678910?!".ToCharArray())
                            s.AppendChar(c);
                    Main m = new Main(textSIP.Text, Convert.ToInt32(textSPort.Text), textSName.Text, s, radioPeer.Checked);
                    Hide();
                    m.ShowDialog(this);
                    Show();
                    m.Dispose();
                }
                else
                    MessageBox.Show("Please fill in the missing fields correctly.", "ERR_INCOMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
        }

        private void checkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPass.Checked == true)
                passWord.Enabled = true;
            else
                passWord.Enabled = false;
        }

        private void Startup_Load(object sender, EventArgs e)
        {
            passWord.Register(this);
        }

        private void Startup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}