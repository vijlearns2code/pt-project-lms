using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1
{
    public class Class1
    {
        SqlConnection con = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = librarydb; Integrated Security = True");
        public SqlConnection ActiveCon()
        { if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;

        }
        public SqlConnection DeactiveCon()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            return null;
        }
    }
}
