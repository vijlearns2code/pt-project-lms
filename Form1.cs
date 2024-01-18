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
using System.Runtime.Remoting.Contexts;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class1 con = new Class1();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Profile_Master WHERE Pro_User_Id='"+textBox1.Text+"' and Pro_Password='"+textBox2.Text+"'",con.ActiveCon());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count==1)
            {
                MDIParent1 obj = new MDIParent1();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password","Alert",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form7 obj = new Form7();
            obj.Show();
        }
    }
}
