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

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch)&&ch!=8&ch!=46&&ch!=12)
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsLetter(ch) && ch != 8 & ch != 46&&ch!=20&&ch!=12&&ch!=32)
                e.Handled = true;
        }

        void CreateNew()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Proc_New_Profile", con.ActiveCon());
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            textBox1.Text = dt.Rows[0][0].ToString();
            textBox2.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
            textBox4.Clear(); textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            CreateNew();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateRecords();
            ViewGrid();
        }

        private void button3_Click(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Profile_Master](Pro_User_Id,Pro_Name,Pro_Email,Pro_Mobile,Pro_Password,Pro_Role,Pro_Status) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }
        void ViewGrid()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Profile_Master", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Pro_User_Id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Pro_Name"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Pro_Mobile"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Pro_Email"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Pro_Password"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Pro_Role"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Pro_Status"].ToString();
            }
            label8.Text = "Row Count: " + dt.Rows.Count.ToString();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            CreateNew();
            ViewGrid();
        }
        void UpdateRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Profile_Master] SET [Pro_User_Id]='" + textBox1.Text + "',[Pro_Name]='" + textBox2.Text + "',[Pro_Email]='" + textBox5.Text + "',[Pro_Mobile]='" + textBox3.Text + "',[Pro_Password]='" + textBox4.Text + "',[Pro_Role]='" + comboBox1.Text + "',[Pro_Status]='" + comboBox2.Text + "' WHERE [Pro_User_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteRecords();
            ViewGrid();
        }
        void DeleteRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("DELETE [librarydb].[dbo].[Profile_Master] WHERE [Pro_User_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }
        bool Validate()
        {
            bool ReturnVal = true;
            if (textBox2.Text.Length == 0|| textBox3.Text.Length == 0|| textBox4.Text.Length == 0)
                ReturnVal = false;
            if (comboBox1.SelectedIndex == -1|| comboBox2.SelectedIndex == -1)
                ReturnVal = false;
            return ReturnVal;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[n].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.Rows[n].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[n].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[n].Cells[6].Value.ToString();
        }
    }
}
