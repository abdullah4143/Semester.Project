using DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class NewsLetterService
    {
        public List<string> GetSubscribers()
        {
            throw new NotImplementedException();
        }

        public bool AddSubscriber(NewsLetter n)
        {
            using (SqlConnection conn = DbConnectionString.GetConnection())
            {
                // Query to check if the email already exists
                string checkQuery = "SELECT COUNT(*) FROM newsletter WHERE email = @Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", n.email);

                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    conn.Close();
                    return false;
                }

                // Email does not exist proceed to insert
                string insertQuery = "INSERT INTO newsletter (email, name) VALUES (@Email, @Name)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@Email", n.email);
                insertCmd.Parameters.AddWithValue("@Name", n.name);

                insertCmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }


        public void RemoveSubscriber(string email)
        {
            throw new NotImplementedException();
        }
    }
}
