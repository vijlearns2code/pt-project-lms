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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        bool btnclicked = false;
        int flag = 0;

        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            CreateNew();
            ViewGrid();
        }

        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView3.SelectedRows[0].Index;
            textBox3.Text = dataGridView3.Rows[n].Cells[0].Value.ToString();
            textBox4.Text = dataGridView3.Rows[n].Cells[1].Value.ToString();
            dateTimePicker1.Text = dataGridView3.Rows[n].Cells[2].Value.ToString();
            textBox5.Text = dataGridView3.Rows[n].Cells[3].Value.ToString();
        }

       void FineAmount()
        {
            DateTime outdate = dateTimePicker2.Value;
            DateTime indate = dateTimePicker1.Value;
            TimeSpan borroweddays = indate - outdate;
            if (borroweddays.TotalDays > 14)
                textBox6.Text = "500";
            else
                textBox6.Text = "0";
            FineChanged();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 & ch != 46 && ch != 12)
                e.Handled = true;
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView2.SelectedRows[0].Index;
            textBox2.Text = dataGridView2.Rows[n].Cells[0].Value.ToString();
            textBox1.Text = dataGridView2.Rows[n].Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.Rows[n].Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.Rows[n].Cells[3].Value.ToString();
            dateTimePicker2.Text = dataGridView2.Rows[n].Cells[4].Value.ToString();
            textBox5.Text = dataGridView2.Rows[n].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.ResetText(); dateTimePicker2.ResetText();
            CreateNew();
            textBox1.Focus();
        }
        void CreateNew()
        {
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Book_Register_Sub WHERE User_Id='"+textBox1.Text+"'", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["Reg_Id"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["User_Id"].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item["Book_Id"].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item["Book_Name"].ToString();
                dataGridView2.Rows[n].Cells[4].Value = item["Br_Out_Date"].ToString();
                dataGridView2.Rows[n].Cells[5].Value = item["Br_Qty"].ToString();
            }
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
                FineAmount();
                Class1 con = new Class1();
                //SqlCommand cmdsub = new SqlCommand("SELECT Left(Convert(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Value + "',120),103), 10)",con.ActiveCon());
                //string date = cmdsub.ExecuteNonQuery().ToString();
                SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Register_Sub] SET [Reg_Id]='" + textBox2.Text + "',[User_Id]='" + textBox1.Text + "',[Book_Id]='" + textBox3.Text + "',[Book_Name]='" + textBox4.Text + "',[Br_Out_Date]='" + dateTimePicker2.Text + "',[Br_In_Date]='" + dateTimePicker2.Text + "',[Br_Qty]='" + textBox5.Text + "',[Br_Fine]='" + textBox6.Text + "' WHERE [Reg_Id]='" + textBox2.Text + "'", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added Successfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }
        void ViewGrid()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Book_Register_Sub", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["Reg_Id"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["User_Id"].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item["Book_Id"].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item["Book_Name"].ToString();
                dataGridView2.Rows[n].Cells[4].Value = item["Br_Out_Date"].ToString();
                dataGridView2.Rows[n].Cells[5].Value = item["Br_Qty"].ToString();
                int m = dataGridView3.Rows.Add();
                dataGridView3.Rows[m].Cells[0].Value = item["Book_Id"].ToString();
                dataGridView3.Rows[m].Cells[1].Value = item["Book_Name"].ToString();
                dataGridView3.Rows[m].Cells[2].Value = item["Br_In_Date"].ToString();
                dataGridView3.Rows[m].Cells[3].Value = item["Br_Qty"].ToString();
                dataGridView3.Rows[m].Cells[4].Value = item["Br_Fine"].ToString();
                dataGridView3.Rows[m].Cells[5].Value = item["Br_Status"].ToString();
            }
            label9.Text = "Item Count: " + dt.Rows.Count.ToString();
        }
        bool Validate()
        {
            bool ReturnVal = true;
            if (textBox1.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0)
                ReturnVal = false;
            return ReturnVal;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (btnclicked == false)
                Add();
            MessageBox.Show("Records Saved Successfully..!");
            CreateNew();
            ViewGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateRecords();
            ViewGrid();
        }
        void UpdateRecords()
        {
            if (Validate())
            {
                FineAmount();
                Class1 con = new Class1();
                //SqlCommand cmdsub = new SqlCommand("SELECT Left(Convert(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Text + "',120),103), 10)",con.ActiveCon());
                //string date = cmdsub.ExecuteNonQuery().ToString();
                SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Register_Main] SET [Reg_Id]='" + textBox2.Text + "',[User_Id]='" + textBox1.Text + "' WHERE [Reg_Id]='" + textBox2.Text + "'", con.ActiveCon());
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Register_Sub] SET [Reg_Id]='" + textBox2.Text + "',[User_Id]='" + textBox1.Text + "',[Book_Id]='" + textBox3.Text + "',[Book_Name]='" + textBox4.Text + "',[Br_Out_Date]='" + dateTimePicker2.Text + "',[Br_In_Date]='" + dateTimePicker1.Text + "',[Br_Qty]='" + textBox5.Text + "',[Br_Fine]='" + textBox6.Text + "' WHERE [Reg_Id]='" + textBox2.Text + "'", con.ActiveCon());
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
            if (Validate())
            {
                Class1 con = new Class1();
                SqlCommand cmd = new SqlCommand("DELETE FROM [librarydb].[dbo].[Book_Register_Main] WHERE [Reg_Id]='" + textBox2.Text + "'", con.ActiveCon());
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM [librarydb].[dbo].[Book_Register_Sub] WHERE [Reg_Id]='" + textBox2.Text + "'", con.ActiveCon());
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Succesfully..!");
            }
        }

        void FineChanged()
        {
            if (textBox6.Text == "0")
            {
                Class1 con = new Class1();
                using (SqlCommand cmd2 = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Register_Sub] SET [Br_Status]='Inactive' WHERE [Reg_Id]='" + textBox2.Text + "' AND [Br_Fine]='0'", con.ActiveCon()))
                {
                    cmd2.ExecuteNonQuery();
                    flag = 1;
                }
                if (flag == 1)
                {
                    SqlCommand cmd1 = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Register_Main] SET [Reg_Status]='Inactive' WHERE [Reg_Id]='" + textBox2.Text + "'", con.ActiveCon());
                    cmd1.ExecuteNonQuery();
                }
            }
        }
    }
}
