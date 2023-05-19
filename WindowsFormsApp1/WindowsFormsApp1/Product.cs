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
    public partial class Product : UserControl
    {
        public Product()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-RC1RFKK\SQLEXPRESS01;Initial Catalog=AccountingSystem;Integrated Security=True");
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO ProductInformation(ProductName, Quantity, Price, Description) VALUES('" +this.prodnamebox.Text + "', '" + this.quantitybox.Text + "', '" + this.pricebox.Text + "', '"+this.descriptionbox.Text+"')";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Product Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearTextboxes();
            Refresh();
        }
        private void ClearTextboxes() 
        {
            prodnamebox.Clear();
            quantitybox.Clear();
            pricebox.Clear();
            descriptionbox.Clear();
            prodnamebox.Focus();
        }
        private void Refresh() 
        {
            conn.Open();
            string query = "SELECT * FROM ProductInformation";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];
            conn.Close();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string query = "UPDATE ProductInformation SET ProductName = '" + this.prodnamebox1.Text + "', Quantity = '" + this.quantitybox1.Text + "', Price = '" + this.pricebox1.Text + "', Description = '"+this.descriptionbox1.Text+"' WHERE id = '" + this.idbox.Text + "'";
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
            prodnamebox1.Clear();
            quantitybox1.Clear();
            pricebox1.Clear();
            descriptionbox1.Clear();
            idbox.Clear();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControl1.SelectTab(2);
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                prodnamebox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].FormattedValue.ToString();
                quantitybox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Quantity"].FormattedValue.ToString();
                pricebox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Price"].FormattedValue.ToString();
                idbox.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                descriptionbox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Description"].FormattedValue.ToString();
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this product ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "DELETE FROM ProductInformation WHERE id = '" + this.idbox.Text + "'";
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
            string searchproduct = searchbox.Text.ToString();
            SearchItem(searchproduct);
        }
        private void SearchItem(string productname)
        {
            string query = "SELECT * FROM ProductInformation WHERE ProductName LIKE '%" + productname + "%'";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataadapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
        }
    }
}
