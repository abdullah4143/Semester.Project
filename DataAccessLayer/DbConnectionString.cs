 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer
{
    public class DbConnectionString
    {
        public static SqlConnection GetConnection()
        {
            // Use a SqlConnectionStringBuilder for better connection string management.
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource= "BUGBUD\\SQLEXPRESS",
                InitialCatalog = "Club",
                IntegratedSecurity = true,
                ConnectTimeout = 30,
                Encrypt = true,
                TrustServerCertificate = true,
                ApplicationIntent = ApplicationIntent.ReadWrite,
                MultiSubnetFailover = false
            };

            // Create and return the SqlConnection object.
            SqlConnection con = new SqlConnection(builder.ConnectionString);
            return con;
        }
    }
}
