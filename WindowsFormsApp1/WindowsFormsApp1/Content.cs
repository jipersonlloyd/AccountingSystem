using Bunifu.UI.WinForms.BunifuButton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.UserControls;

namespace WindowsFormsApp1
{
    public partial class Content : Form
    {
        public Content()
        {
            InitializeComponent();
        }
        
        private void Content_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();
        }
        private void add_UControls(UserControl userControl) 
        {
            userControl.Dock = DockStyle.Fill;
            mainpnl.Controls.Clear();
            mainpnl.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void btn_Click(object sender, EventArgs e) 
        {
            

            foreach (var pnl in tableLayoutPanel1.Controls.OfType<Panel>())
            {
                pnl.BackColor = Color.SteelBlue;
            }
            BunifuButton btn = (BunifuButton)sender;

            switch (btn.Name) 
            {
                case "dashboardbtn":
                    add_UControls(new Dashboard());
                    dashboardpnl.BackColor = Color.White;
                    break;
                case "customerbtn":
                    add_UControls(new Customer());
                    customerpnl.BackColor = Color.White;
                    break;
                case "vendorbtn":
                    add_UControls(new Vendor());
                    vendorpnl.BackColor = Color.White;
                    break;
                case "productbtn":
                    add_UControls(new Product());
                    productpnl.BackColor = Color.White;
                    break;
                case "purchasebtn":
                    add_UControls(new Purchase());
                    purchasepnl.BackColor = Color.White;
                    break;
                case "salebtn":
                    add_UControls(new Sale());
                    salepnl.BackColor = Color.White;
                    break;
                case "settingbtn":
                    add_UControls(new Setting());
                    settingpnl.BackColor = Color.White;
                    break;
            }
        }

        private void mainpnl_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void minimizebtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit ?", "Exit message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
            {
                Application.Exit();
            }            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LoginPage loginpage = new LoginPage();

            loginpage.Show();
            Thread.Sleep(500);
            this.Hide();
        }
    }
}
