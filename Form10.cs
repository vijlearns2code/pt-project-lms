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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        bool btnclicked = false;
        bool btndgv2 = false;
        bool btndgv1 = false;
        public Form10()
        {
            InitializeComponent();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 & ch != 46 && ch != 12)
                e.Handled = true;
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView2.SelectedRows[0].Index;
            textBox1.Text = dataGridView2.Rows[n].Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.Rows[n].Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.Rows[n].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView2.Rows[n].Cells[3].Value.ToString();
            textBox4.Text = dataGridView2.Rows[n].Cells[4].Value.ToString(); 
            btndgv2 = true;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = dataGridView1.Rows[n].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.Rows[n].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[n].Cells[2].Value.ToString();
            btndgv1 = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear(); textBox3.Clear();
            textBox4.Clear(); textBox1.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.ResetText();
            CreateNew();
            textBox5.Focus();
        }
        void CreateNew()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Proc_New_Book_Register", con.ActiveCon());
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            textBox1.Text = dt.Rows[0][0].ToString();
            textBox5.Focus();
        }
        void AddRecords()
        {
            if (Validate())
            {
                Class1 con = new Class1();
                //SqlCommand cmdsub = new SqlCommand("SELECT Left(CONVERT(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Value + "',120),103), 10)", con.ActiveCon());
                //string date = cmdsub.ExecuteNonQuery().ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Book_Register_Main](Reg_Id,User_Id,Reg_Status) VALUES('" + textBox1.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
                if(btnclicked==false)
                Add();
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddRecords();
            CreateNew();
            ViewGrid();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            CreateNew();
            ViewGrid();
        }
        void ViewGrid()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Book_Register_Main", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Reg_Id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["User_Id"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Reg_Status"].ToString();
            }
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * FROM Book_Register_Sub", con.ActiveCon());
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            dataGridView2.Rows.Clear();
            foreach (DataRow item in dt1.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["Reg_Id"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["Book_Id"].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item["Book_Name"].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item["Br_Out_Date"].ToString();
                dataGridView2.Rows[n].Cells[4].Value = item["Br_Qty"].ToString();
            }
            label1.Text = "Row Count: " + dt1.Rows.Count.ToString();
        }
        bool Validate()
        {
            bool ReturnVal = true;
            if (textBox4.Text.Length == 0 || textBox1.Text.Length == 0)
                ReturnVal = false;
            if (comboBox1.SelectedIndex == -1)
                ReturnVal = false;
            return ReturnVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add();
            btnclicked = true;
            ViewGrid();
        }

        void Add()
        {
            if (Validate())
            {
                Class1 con = new Class1();
                SqlCommand cmd1 = new SqlCommand("SELECT Book_Id FROM [librarydb].[dbo].[Book_Master] WHERE Book_Name='" + textBox3.Text + "'", con.ActiveCon());
                using (SqlDataReader dr = cmd1.ExecuteReader())
                {
                    if (dr.Read())
                        textBox2.Text = dr.GetString(0);
                }
                //SqlCommand cmdsub = new SqlCommand("SELECT Left(Convert(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Value + "',120),103), 10)",con.ActiveCon());
                //string date = cmdsub.ExecuteNonQuery().ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Book_Register_Sub] VALUES('" + textBox1.Text + "','" + textBox5.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Text + "','','" + textBox4.Text + "','','" + comboBox1.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateRecords();
            ViewGrid();
        }
        void UpdateRecords()
        {
            if (Validate()) { 
            Class1 con = new Class1();
            //SqlCommand cmdsub = new SqlCommand("SELECT Left(Convert(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Text + "',120),103), 10)",con.ActiveCon());
            //string date = cmdsub.ExecuteNonQuery().ToString();
            SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Register_Main] SET [Reg_Id]='" + textBox1.Text + "',[User_Id]='" + textBox5.Text + "',[Reg_Status]='" + comboBox1.Text + "' WHERE [Reg_Id]='" + textBox2.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Register_Sub] SET [Reg_Id]='" + textBox1.Text + "',[User_Id]='" + textBox5.Text + "',[Book_Id]='" + textBox2.Text + "',[Book_Name]='" + textBox3.Text + "',[Br_Out_Date]='" + dateTimePicker1.Text + "',[Br_In_Date]='',[Br_Qty]='" + textBox4.Text + "',[Br_Fine]='',[Br_Status]='" + comboBox1.Text + "' WHERE [Reg_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Records Updated Succesfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteRecords();
            ViewGrid();
        }
        void DeleteRecords()
        {
            Class1 con = new Class1();
            if (btndgv1 == true) { 
            SqlCommand cmd = new SqlCommand("DELETE FROM [librarydb].[dbo].[Book_Register_Main] WHERE [Reg_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
        }
            if (btndgv2==true)
            {
                SqlCommand cmd1 = new SqlCommand("DELETE FROM [librarydb].[dbo].[Book_Register_Sub] WHERE [Reg_Id]='" + textBox1.Text + "'", con.ActiveCon());
                cmd1.ExecuteNonQuery();
            }
            MessageBox.Show("Record Deleted Succesfully..!");
        }
    }
}
