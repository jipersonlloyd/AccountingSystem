﻿using System;
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
    public partial class Vendor : UserControl
    {
        public Vendor()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-RC1RFKK\SQLEXPRESS01;Initial Catalog=AccountingSystem;Integrated Security=True");
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO VendorInformation1(Name, PhoneNumber, Address) VALUES('" + this.vendorbox.Text + "', '" + this.phonenumberbox.Text + "', '" + this.addressbox.Text + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Vendor Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Refresh();
            ClearTextboxes();
        }
        private void ClearTextboxes()
        {
            vendorbox.Clear();
            addressbox.Clear();
            phonenumberbox.Clear();
            vendorbox.Focus();
        }
        private void Refresh()
        {
            conn.Open();
            string query = "SELECT * FROM VendorInformation1";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];
            conn.Close();
        }

        private void Vendor_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControl1.SelectTab(2);
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                vendorbox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                phonenumberbox1.Text = dataGridView1.Rows[e.RowIndex].Cells["PhoneNumber"].FormattedValue.ToString();
                addressbox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].FormattedValue.ToString();
                idbox.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string query = "UPDATE VendorInformation1 SET Name = '" + this.vendorbox1.Text + "', PhoneNumber = '" + this.phonenumberbox1.Text + "', Address = '" + this.addressbox1.Text + "' WHERE id = '" + this.idbox.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Updated Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Refresh();
            ClearTextboxes1();
        }
        private void ClearTextboxes1() 
        {
            vendorbox1.Clear();
            phonenumberbox1.Clear();
            addressbox1.Clear();
            idbox.Clear();
            vendorbox1.Focus();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this vendor ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "DELETE FROM VendorInformation1 WHERE id = '" + this.idbox.Text + "'";
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
            string searchvendor = searchbox.Text.ToString();
            SearchItem(searchvendor);
        }
        private void SearchItem(string vendorname)
        {
            string query = "SELECT * FROM VendorInformation1 WHERE Name LIKE '%" + vendorname + "%'";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataadapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
        }
    }
}
