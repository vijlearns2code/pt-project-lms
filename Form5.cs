using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells[2].Value.ToString();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 && ch != 46 && ch != 20 && ch != 12 && ch != 32)
             e.Handled = true;
        }
        void CreateNew()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Proc_New_Section", con.ActiveCon());
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            textBox1.Text = dt.Rows[0][0].ToString();
            textBox2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            CreateNew();
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
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Section_Master](Section_Id,Sec_Name,Sec_Status) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }
        void ViewGrid()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Section_Master", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Section_Id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Sec_Name"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Sec_Status"].ToString();
            }
            label4.Text = "Row Count: " + dt.Rows.Count.ToString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            CreateNew();
            ViewGrid();
        }
        void DeleteRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("DELETE [librarydb].[dbo].[Section_Master] WHERE [Section_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateRecords();
            ViewGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRecords();
            ViewGrid();
        }
        void UpdateRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Section_Master] SET [Section_Id]='" + textBox1.Text + "',[Sec_Name]='" + textBox2.Text + "',[Sec_Status]='" + comboBox1.Text + "' WHERE [Section_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
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
