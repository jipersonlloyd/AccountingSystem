using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
            {
                Application.Exit();
            }
        }
        private void minimizebtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string username = "admin";
            string password = "admin123";

            if (usernamebox.Text == username && passwordbox.Text == password)
            {
                Content content = new Content();
                content.Show();
                Thread.Sleep(500);
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Incorrect Login Details", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usernamebox.Clear();
                passwordbox.Clear();
                usernamebox.Focus();
            }
        }
    }
}
