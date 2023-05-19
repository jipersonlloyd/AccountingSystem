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
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-RC1RFKK\SQLEXPRESS01;Initial Catalog=AccountingSystem;Integrated Security=True");
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO UserAccounts(Username, Password) VALUES('" + this.username.Text + "', '" + this.password.Text + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Account has been added successfully!", "Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            username.Clear();
            password.Clear();
            Refresh1();
        }
        private void Refresh1()
        {
            conn.Open();
            string query = "SELECT * FROM UserAccounts";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView2.DataSource = dataset.Tables[0];
            conn.Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            Refresh1();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControl1.SelectTab(2);
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView2.CurrentRow.Selected = true;
                userupdate.Text = dataGridView2.Rows[e.RowIndex].Cells["Username"].FormattedValue.ToString();
                newpass.Text = dataGridView2.Rows[e.RowIndex].Cells["Password"].FormattedValue.ToString();
                idbox.Text = dataGridView2.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            string query = "UPDATE UserAccounts SET Username = '" + this.userupdate.Text + "', Password = '" + this.newpass.Text + "' WHERE id = '"+this.idbox.Text+"'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Updated Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Refresh1();
            userupdate.Clear();
            newpass.Clear();
            idbox.Clear();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string query = "DELETE FROM UserAccounts WHERE id = '" + this.idbox.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr;
                conn.Open();
                dr = cmd.ExecuteReader();
                conn.Close();
            }
            Refresh1();
            userupdate.Clear();
            newpass.Clear();
            idbox.Clear();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchuser = searchbox.Text.ToString();
            SearchItem(searchuser);
        }
        private void SearchItem(string username)
        {
            string query = "SELECT * FROM UserAccounts WHERE Username LIKE '%" + username + "%'";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataAdapter dataadapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataadapter.Fill(datatable);
            dataGridView2.DataSource = datatable;
        }
    }
}
