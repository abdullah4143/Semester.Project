using DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    public class AuthServices
    {
        public bool VerifyAdmin(string email, string password)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT count(*) FROM Admin WHERE Email = @Email AND Password = @Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool VerifyUser(string email, string password)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT COUNT(*) FROM [users] WHERE Email = @email and password = @password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddUser(User user)
        {
            using (SqlConnection conn = DbConnectionString.GetConnection())
            {
                // Check if the email already exists
                string checkQuery = "SELECT COUNT(*) FROM users WHERE email = @email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@email", user.Email);

                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return false;
                }

                // Email does not exist, proceed with insertion
                string insertQuery = "INSERT INTO users (name, email, username, password) VALUES (@name, @email, @username, @password)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@name", user.Name);
                insertCmd.Parameters.AddWithValue("@email", user.Email);
                insertCmd.Parameters.AddWithValue("@username", user.username);
                insertCmd.Parameters.AddWithValue("@password", user.password);
                insertCmd.ExecuteNonQuery();

                int user_id = GetUseridByEmail(user.Email!);

                insertQuery = "INSERT INTO role (user_id, admin_id, role) VALUES (@user_id, @admin_id, @role)";
                SqlCommand insertCmd1 = new SqlCommand(insertQuery, conn);
                insertCmd1.Parameters.AddWithValue("@user_id", user_id);
                insertCmd1.Parameters.AddWithValue("@admin_id", 0);
                insertCmd1.Parameters.AddWithValue("@role", "Editor");
                insertCmd1.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }



        public int GetUseridByEmail(string email)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT user_id FROM users WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();
            int user_id = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            return user_id;
        }

    }
}
 