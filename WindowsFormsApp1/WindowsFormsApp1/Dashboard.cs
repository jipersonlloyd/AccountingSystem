using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.UserControls
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-RC1RFKK\SQLEXPRESS01;Initial Catalog=AccountingSystem;Integrated Security=True");
        private void Dashboard_Load(object sender, EventArgs e)
        {
            CustomerTotal();
            VendorTotal();
            ProductTotal();
            UserAccountsTotal();
        }
        private string CustomerTotal()
        {
            conn.Open();
            string query1 = "Select Count(Name) from CustomerInformation";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            var count1 = cmd1.ExecuteScalar();
            customertotal.Text = count1.ToString();
            conn.Close();
            return customertotal.Text;
        }
        private string VendorTotal()
        {
            conn.Open();
            string query1 = "Select Count(Name) from VendorInformation1";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            var count1 = cmd1.ExecuteScalar();
            vendortotal.Text = count1.ToString();
            conn.Close();
            return vendortotal.Text;
        }
        private string ProductTotal()
        {
            conn.Open();
            string query1 = "Select Count(ProductName) from ProductInformation";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            var count1 = cmd1.ExecuteScalar();
            producttotal.Text = count1.ToString();
            conn.Close();
            return producttotal.Text;
        }
        private string UserAccountsTotal()
        {
            conn.Open();
            string query1 = "Select Count(Username) from UserAccounts";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            var count1 = cmd1.ExecuteScalar();
            useraccountstotal.Text = count1.ToString();
            conn.Close();
            return useraccountstotal.Text;
        }
    }
}
