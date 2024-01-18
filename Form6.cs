using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[n].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[n].Cells[1].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[n].Cells[4].Value.ToString();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 & ch != 46 && ch != 12)
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            CreateNew();
            textBox3.Focus();
        }
        void CreateNew()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Proc_New_Sec_Rack", con.ActiveCon());
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            textBox1.Text = dt.Rows[0][0].ToString();
            comboBox1.Focus();
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
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Sec_Rack_Map](Sr_Id,Sec_Name,Rack_Name,Sr_Print,Sr_Status) VALUES('" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + comboBox3.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CreateNew();
            ViewGrid();
        }
        void ViewGrid()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Sec_Rack_Map", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Sr_Id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Sec_Name"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Rack_Name"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Sr_Print"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Sr_Status"].ToString();
            }
            label6.Text = "Row Count: " + dt.Rows.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateRecords();
            ViewGrid();
        }
        void UpdateRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Sec_Rack_Map] SET [Sr_Id]='" + textBox1.Text + "',[Sec_Name]='" + comboBox1.Text + "',[Rack_Name]='" + comboBox2.Text + "',[Sr_Print]='" + textBox3.Text + "',[Sr_Status]='" + comboBox3.Text + "' WHERE [Sr_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRecords();
            ViewGrid();
        }
        void DeleteRecords()
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("DELETE [librarydb].[dbo].[Sec_Rack_Map] WHERE [Sr_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }
        bool Validate()
        {
            bool ReturnVal = true;
            if (textBox3.Text.Length == 0)
                ReturnVal = false;
            if (comboBox1.SelectedIndex == -1|| comboBox2.SelectedIndex == -1|| comboBox3.SelectedIndex == -1)
                ReturnVal = false;
            return ReturnVal;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("Select Rack_Name FROM Rack_Master", con.ActiveCon());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetString(0));
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Class1 con = new Class1();
            SqlCommand cmd = new SqlCommand("SELECT Sec_Name FROM Section_Master", con.ActiveCon());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr.GetString(0));
            }
        }
    }
}
