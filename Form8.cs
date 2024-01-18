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
    public partial class Form8 : Form
    {
        bool btnclicked = false;
        public Form8()
        {
            InitializeComponent();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView1.SelectedRows[0].Index;
            textBox3.Text = dataGridView1.Rows[n].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.Rows[n].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.Rows[n].Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.Rows[n].Cells[3].Value.ToString();
            textBox7.Text = dataGridView1.Rows[n].Cells[4].Value.ToString();

        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            int n = dataGridView2.SelectedRows[0].Index;
            textBox1.Text = dataGridView2.Rows[n].Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridView2.Rows[n].Cells[1].Value.ToString();
            textBox2.Text = dataGridView2.Rows[n].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView2.Rows[n].Cells[3].Value.ToString();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 & ch != 46 && ch != 12)
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 & ch != 46 && ch != 12 && ch != 110)
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 & ch != 46 && ch != 12 && ch != 110)
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
            textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
            textBox7.Clear();
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.ResetText();
            CreateNew();
            dateTimePicker1.Focus();
        }
        void CreateNew()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Proc_New_Purchase", con.ActiveCon());
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            textBox1.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("Proc_New_Book", con.ActiveCon());
            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            textBox3.Text = dt1.Rows[0][0].ToString();
            dateTimePicker1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
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
                //SqlCommand cmdsub = new SqlCommand("SELECT Left(CONVERT(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Value + "',120),103), 10)", con.ActiveCon());
                //string date = cmdsub.ExecuteNonQuery().ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Purchase_Main](Pur_Id,Pur_Date,Pur_From,Pur_Amount,Pur_Status) VALUES('" + textBox1.Text + "','"+dateTimePicker1.Text +"','" + textBox2.Text + "','" + textBox7.Text + "','" + comboBox1.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
                if (btnclicked == false)
                    Add();
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            CreateNew();
            ViewGrid();
        }
        void ViewGrid()
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("Select * FROM Purchase_Main", con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["Pur_Id"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["Pur_Date"].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item["Pur_From"].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item["Pur_Status"].ToString();
            }
            SqlDataAdapter sda1 = new SqlDataAdapter("Select * FROM Purchase_Sub", con.ActiveCon());
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt1.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Book_Id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Book_Name"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Qty"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Rate"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Amount"].ToString();
            }
            label10.Text = "Row Count: " + dt1.Rows.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateRecords();
            ViewGrid();
        }
        void UpdateRecords()
        {
            if(Validate())
            { 
            Class1 con = new Class1();
            //SqlCommand cmdsub = new SqlCommand("SELECT Left(Convert(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Text + "',120),103), 10)",con.ActiveCon());
            //string date = cmdsub.ExecuteNonQuery().ToString();
            SqlCommand cmd = new SqlCommand("UPDATE [librarydb].[dbo].[Purchase_Main] SET [Pur_Id]='" + textBox1.Text + "',[Pur_Date]='" + dateTimePicker1.Text + "',[Pur_From]='" + textBox2.Text + "',[Pur_Amount]='" + textBox7.Text + "',[Pur_Status]='" + comboBox1.Text + "' WHERE [Pur_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("UPDATE [librarydb].[dbo].[Purchase_Sub] SET [Pur_Id]='" + textBox1.Text + "',[Pur_Date]='" + dateTimePicker1.Text + "',[Book_Id]='" + textBox3.Text + "',[Book_Name]='" + textBox4.Text + "',[Qty]='" + textBox5.Text + "',[Rate]='" + textBox6.Text + "',[Amount]='" + textBox7.Text + "',[Status]='" + comboBox1.Text + "' WHERE [Pur_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd1.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("UPDATE [librarydb].[dbo].[Book_Master] SET [Book_Id]='" + textBox3.Text + "',[Book_Name]='" + textBox4.Text + "',[Book_Auth]='',[Book_Edition]='',[Book_Pages]='',[Book_Publish]='',[Sr_Id]='',[Book_Status]='" + comboBox1.Text + "'", con.ActiveCon());
            cmd2.ExecuteNonQuery();
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
            SqlCommand cmd = new SqlCommand("DELETE FROM [librarydb].[dbo].[Purchase_Main] WHERE [Pur_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM [librarydb].[dbo].[Purchase_Sub] WHERE [Pur_Id]='" + textBox1.Text + "'", con.ActiveCon());
            cmd1.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("DELETE FROM [librarydb].[dbo].[Book_Master] WHERE [Book_Id]='" + textBox3.Text + "'", con.ActiveCon());
            cmd2.ExecuteNonQuery();
            MessageBox.Show("Record Deleted Succesfully..!");
        }
        bool Validate()
        {
            bool ReturnVal = true;
            if (textBox2.Text.Length == 0|| textBox4.Text.Length == 0|| textBox5.Text.Length == 0|| textBox6.Text.Length == 0)
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
            textBox7.Text = (float.Parse(textBox5.Text) * float.Parse(textBox6.Text)).ToString();
            if (Validate())
            {
                Class1 con = new Class1();
                //SqlCommand cmdsub = new SqlCommand("SELECT Left(Convert(VARCHAR,Convert(Datetime,'" + dateTimePicker1.Value + "',120),103), 10)",con.ActiveCon());
                //string date = cmdsub.ExecuteNonQuery().ToString();
                SqlCommand cmd = new SqlCommand("INSERT INTO [librarydb].[dbo].[Purchase_Sub](Pur_Id,Pur_Date,Book_Id,Book_Name,Qty,Rate,Amount,Status) VALUES('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + comboBox1.Text + "')", con.ActiveCon());
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("INSERT INTO [librarydb].[dbo].[Book_Master] VALUES('" + textBox3.Text + "','" + textBox4.Text + "','','','','','','" + comboBox1.Text + "')", con.ActiveCon());
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Records Inserted Successfully..!");
            }
            else
                MessageBox.Show("Please Check all the Fields");
        }
    }
}
