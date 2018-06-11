using System.Data.SqlClient;

namespace Shelf_Share.Data
{
    public class SqlConnect
    {
        public static string ConnectionString
        {
            get
            {
                string connStr = "Server=L-SRATANAS;Database=ShelfShare;Trusted_Connection=True;MultipleActiveResultSets=true";


                //Allows to parse automatically a connection string and manage the individual properties
                //of a class. Makes it easy to manipulate a connection string
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connStr);

                return sb.ToString();
            }
        }
        /// <summary>
        /// returns an open connection that can be used elsewhere
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection()
        {
            
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
    

  
            return conn;
        }

    }
}
