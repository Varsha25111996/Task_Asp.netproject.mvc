using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Task_Asp.netproject.mvc.Sql_Connection
{
    public class Db_Connection
    {
        public string ConnectionString = "Data Source=VARSHA_MARATHE\\SQLEXPRESS;Initial catalog=TaskMVC;user id=sa;password=Game@123;trustservercertificate=true;";
        public  SqlConnection Connect()
        {
            SqlConnection sqlconn = new SqlConnection(ConnectionString);
            sqlconn.Close();
            sqlconn.Open();
            return sqlconn;
        }
        public DataTable FillQuery(string Query)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlconn = Connect();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlconn);
            da.Fill(dt);
            return dt;
        }
    }
}