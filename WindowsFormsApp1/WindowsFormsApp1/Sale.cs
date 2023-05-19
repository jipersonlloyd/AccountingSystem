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
    public partial class Sale : UserControl
    {
        public Sale()
        {
            InitializeComponent();
            FillCustomerNameBox();
            FillProductBox();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-RC1RFKK\SQLEXPRESS01;Initial Catalog=AccountingSystem;Integrated Security=True");
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Checkbox();
            PhoneNumber();

            string query1 = "INSERT INTO TemporarySale (Date, CustomerName, PhoneNumber, TotalProduct, GrandTotal, Cash) VALUES('" + this.datebox.Text + "', '" + this.customerbox.Text + "', '" + PhoneNumber() + "', '" + this.totalproductbox.Text + "', '" + this.gtotalbox.Text + "', '" + Checkbox() + "')";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlDataReader dr1;
            conn.Open();
            dr1 = cmd1.ExecuteReader();
            conn.Close();

            string query2 = "Delete AddSale";
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2;
            conn.Open();
            dr2 = cmd2.ExecuteReader();
            conn.Close();
            Refresh1();
            SavedSale();
            ManageSale();
            ClearTextboxes();
            MessageBox.Show("Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int databasequantity = ProductQuantityDatabase();
            gtotalbox.Text = "0";
            int price = Convert.ToInt32(this.pricebox.Text);
            int quantity = Convert.ToInt32(this.quantitybox.Text);
            int result = price * quantity;
            string query = "INSERT INTO AddSale(Name, Quantity, Price, Total) VALUES('" + this.customerbox.Text + "', '" + this.quantitybox.Text + "', '" + this.pricebox.Text + "', '" + result + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();


            int quantitybox = Convert.ToInt32(this.quantitybox.Text);
            int databaseresult = databasequantity - quantitybox;

            string query3 = "UPDATE ProductInformation SET Quantity = '" + databaseresult + "' WHERE ProductName = '" + this.productbox.Text + "' ";
            SqlCommand cmd3 = new SqlCommand(query3, conn);
            SqlDataReader dr3;
            conn.Open();
            dr3 = cmd3.ExecuteReader();
            conn.Close();

            gtotalbox.Text = result.ToString();
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    gtotalbox.Text = Convert.ToString(double.Parse(gtotalbox.Text) + double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            conn.Open();
            string query1 = "Select Count(*) from AddSale";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            var count1 = cmd1.ExecuteScalar();
            totalproductbox.Text = count1.ToString();
            conn.Close();
            Refresh1();
        }
        private void FillCustomerNameBox()
        {
            string query = "SELECT Name FROM CustomerInformation";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string vname = dr.GetString(0);
                    customerbox.Items.Add(vname);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillProductBox()
        {
            string query = "SELECT ProductName FROM ProductInformation";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string vname = dr.GetString(0);
                    productbox.Items.Add(vname);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Refresh1()
        {
            conn.Open();
            string query = "SELECT * FROM AddSale";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];
            conn.Close();
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            Refresh1();
            SavedSale();
            ManageSale();
        }
        private void ManageSale()
        {
            conn.Open();
            string query = "SELECT * FROM TemporarySale";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView3.DataSource = dataset.Tables[0];
            conn.Close();
        }
        private void ClearTextboxes()
        {
            totalproductbox.Clear();
            quantitybox.Clear();
            gtotalbox.Clear();
        }
        private int ProductQuantityDatabase()
        {
            int databasequantity = 0;

            string query4 = "SELECT * FROM ProductInformation where ProductName = '" + this.productbox.Text + "'";
            SqlCommand cmd4 = new SqlCommand(query4, conn);
            SqlDataReader dr4;
            try
            {
                conn.Open();
                dr4 = cmd4.ExecuteReader();
                while (dr4.Read())
                {
                    databasequantity = dr4.GetInt32(2);
                }
                conn.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return databasequantity;
        }
        private string PhoneNumber()
        {
            string phonenumber = "";

            string query = "SELECT * FROM CustomerInformation where Name = '" + this.customerbox.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    phonenumber = dr.GetString(2).ToString();
                }
                conn.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return phonenumber;
        }
        private string Checkbox()
        {
            string paid = "";

            if (paidcheckbox.Checked)
            {
                paid = "Paid";
            }
            else { paid = "Not Paid"; }

            return paid;
        }
        private void SavedSale()
        {
            conn.Open();
            string query = "SELECT * FROM TemporarySale";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView2.DataSource = dataset.Tables[0];
            conn.Close();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM ProductInformation where ProductName = '" + this.productbox.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string price = dr.GetInt32(3).ToString();
                    pricebox.Text = price;
                }
                conn.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string paid = "";
            if (paid1.Checked)
            {
                paid = "Paid";
            }
            else { paid = "Not Paid"; }

            string query = "UPDATE TemporarySale SET Cash = '" + paid + "' WHERE CustomerName = '" + this.namebox.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SavedSale();
            ManageSale();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this item ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "DELETE FROM TemporarySale WHERE CustomerName = '" + this.namebox.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr;
                conn.Open();
                dr = cmd.ExecuteReader();
                conn.Close();
            }
            namebox.Clear();
            ManageSale();
            SavedSale();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControl1.SelectTab(2);
            if (dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView3.CurrentRow.Selected = true;
                namebox.Text = dataGridView3.Rows[e.RowIndex].Cells["CustomerName"].FormattedValue.ToString();
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchcustomer = searchbox.Text.ToString();
            SearchItem(searchcustomer);
        }
        private void SearchItem(string customername)
        {
            string query = "SELECT * FROM TemporarySale WHERE CustomerName LIKE '%" + customername + "%'";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataadapter.Fill(datatable);
            dataGridView2.DataSource = datatable;
        }
    }
}
