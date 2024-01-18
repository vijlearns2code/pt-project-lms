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
namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            CreateNew();
            textBox2.Focus();
        }
        void CreateNew()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Proc_New_Role",con.ActiveCon());
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            textBox1.Text = dt.Rows[0][0].ToString();
            textBox2.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddRecords();
            CreateNew();
            ViewGrid();
        }
        void AddRecords()
        {
            if (Validate())
            {
                Class1 con = new Class1();
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Role_Master](Role_Id,Role,Role_Status) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CreateNew();
            ViewGrid();
        }

        void ViewGrid()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Role_Master", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach(DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Role_Id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Role"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Role_Status"].ToString();
            }
            label5.Text = "Row Count: " + dt.Rows.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateRecords();
            ViewGrid();
        }

        void UpdateRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Role_Master] SET [Role_Id]='" + textBox1.Text+"',[Role]='" + textBox2.Text + "',[Role_Status]='" + comboBox1.Text + "' WHERE [Role_Id]='" + textBox1.Text+"'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }

        private void dataGridView1_MouseClick_1(object sender, MouseEventArgs e)
        {
            int n = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells[2].Value.ToString();
        }
        void DeleteRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("DELETE [librarydb].[dbo].[Role_Master] WHERE [Role_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteRecords();
            ViewGrid();
        }
        bool Validate()
        {
            bool ReturnVal = true;
            if (textBox2.Text.Length == 0)
                ReturnVal = false;
            if (comboBox1.SelectedIndex == -1)
                ReturnVal = false;
            return ReturnVal;
        }
    }
}
