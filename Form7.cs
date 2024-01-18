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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length != 0)
            {
                Class1 con = new Class1();
                SqlCommand cmd = new SqlCommand("Update [librarydb].[dbo].[Profile_Master] SET Pro_Password='" + textBox3.Text + "' WHERE Pro_User_Id='" + textBox1.Text + "'AND Pro_Password='" + textBox2.Text + "'", con.ActiveCon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Password Successfully Changed..!");
                this.Close();
                MessageBox.Show("Login With Your New Password");
            }
            else
                MessageBox.Show("Enter Valid Values", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
            {
                Class1 con = new Class1();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Profile_Master WHERE Pro_User_Id='" + textBox1.Text + "' and Pro_Password='" + textBox2.Text + "'", con.ActiveCon());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 1)
                   MessageBox.Show("Enter Valid Username and Password", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
