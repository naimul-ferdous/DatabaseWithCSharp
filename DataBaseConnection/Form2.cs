using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataBaseConnection
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static string conString = @"Data Source=NAIMUL-PC\SQLEXPRESS;Initial Catalog=myDB;Integrated Security=True";
        SqlConnection connection = new SqlConnection(conString);

        private void Form2_Load(object sender, EventArgs e)
        {

            populateDGV();
        }

        public void populateDGV()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("select * from students", connection);
            command.ExecuteNonQuery();
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
            connection.Close();
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into students(fname, lname, age) values('"+textBox2.Text+"', '"+textBox3.Text+ "', '"+textBox4.Text+"')";
            connection.Open();
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Student Inserted");
            connection.Close();
            populateDGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE [dbo].[students] SET[fname] = '" + textBox2.Text + "' ,[lname] = '" + textBox3.Text + "' ,[age] ='" + textBox4.Text + "' WHERE [id]='" +int.Parse(textBox1.Text) + "'";
            connection.Open();
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Student Updated");
            connection.Close();
            populateDGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM [dbo].[students] WHERE [id]='" + int.Parse(textBox1.Text) + "' ";
            connection.Open();
            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Student Deleted");
            connection.Close();
            populateDGV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string select = "SELECT * FROM[dbo].[students] WHERE [id]= " + textBox1.Text;
            SqlCommand command = new SqlCommand(select, connection);
            connection.Open();
            SqlDataReader sdr;
            sdr=command.ExecuteReader();
            
            if(sdr.Read())
            {
                textBox2.Text = sdr[1].ToString();
                textBox3.Text = sdr[2].ToString();
                textBox4.Text = sdr[3].ToString();
            }
            else
            {
                MessageBox.Show("User not found");
            }

            connection.Close();
            populateDGV();
        }
    }
}
