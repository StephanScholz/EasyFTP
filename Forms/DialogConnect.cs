using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyFTP
{    public partial class DialogConnect : Form
    {
        public string[] credentials = new string[3];

        public DialogConnect()
        {
            InitializeComponent();
        }

        // TODO Work on saving Userdata
        private void buttonDialogConfirm_Click(object sender, EventArgs e)
        {
            if (tbIP.Text == "")
            {
                MessageBox.Show("Please enter a valid IP-Adress", "Missing IP", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                credentials[0] = tbIP.Text;
                credentials[1] = tbUser.Text;
                credentials[2] = tbPassword.Text;
            }
        }

        private void buttonDialogCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
