using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1.UserControls
{
    public partial class Customer : UserControl
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-RC1RFKK\SQLEXPRESS01;Initial Catalog=AccountingSystem;Integrated Security=True");
        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO CustomerInformation(Name, PhoneNumber, Address) VALUES('" + this.cusnamebox.Text + "', '" + this.phonenumberbox.Text + "', '" + this.addressbox.Text + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Customer Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearTextboxes();
            Refresh();
        }
        private void ClearTextboxes()
        {
            cusnamebox.Clear();
            phonenumberbox.Clear();
            addressbox.Clear();
            cusnamebox.Focus();
        }
        private void ClearTextboxes1() 
        {
            idbox.Clear();
            cusnamebox1.Clear();
            phonenumberbox1.Clear();
            addressbox1.Clear();
            cusnamebox1.Focus();
        }
        private void Refresh()
        {
            conn.Open();
            string query = "SELECT * FROM CustomerInformation";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Customer_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControl1.SelectTab(2);
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                cusnamebox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                phonenumberbox1.Text = dataGridView1.Rows[e.RowIndex].Cells["PhoneNumber"].FormattedValue.ToString();
                addressbox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].FormattedValue.ToString();
                idbox.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string query = "UPDATE CustomerInformation SET Name = '"+this.cusnamebox1.Text+"', PhoneNumber = '"+this.phonenumberbox1.Text+"', Address = '"+this.addressbox1.Text+"' WHERE id = '"+this.idbox.Text+"'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Updated Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Refresh();
            ClearTextboxes1();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
            {
                string query = "DELETE FROM CustomerInformation WHERE id = '" + this.idbox.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr;
                conn.Open();
                dr = cmd.ExecuteReader();
                conn.Close();
            }
            Refresh();
            ClearTextboxes1();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchcustomer = searchbox.Text.ToString();
            SearchItem(searchcustomer);
        }
        private void SearchItem(string customername)
        {
            string query = "SELECT * FROM CustomerInformation WHERE Name LIKE '%" + customername + "%'";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataadapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
        }
    }
}
