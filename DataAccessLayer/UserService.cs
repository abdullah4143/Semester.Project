using DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserService
    {
        public List<User> GetUsers()
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT * FROM users";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                User user = new User();
                user.user_id = Convert.ToInt32(reader["user_id"]);
                user.username = reader["username"].ToString();
                user.Email = reader["Email"].ToString();
                user.password = reader["password"].ToString();
                user.Name = reader["Name"].ToString();
                users.Add(user);
            }
            conn.Close();

            return users;
        }

        public string GetUsernameByEmail(string email)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT username FROM users WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();
            string result = (string)cmd.ExecuteScalar();
            conn.Close();

            return result;
        }
        public string GetUsernameById(int id)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT username FROM users WHERE user_id = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            string username = cmd.ExecuteScalar().ToString()!;
            conn.Close();

            return username;
        }
        public User GetUserByEmail(string email)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT * FROM users WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            User user = new User();
            while (reader.Read())
            {
                user.user_id = Convert.ToInt32(reader["user_id"]);
                user.username = reader["username"].ToString();
                user.Email = reader["Email"].ToString();
                user.password = reader["password"].ToString();
                user.Name = reader["Name"].ToString();
            }
            conn.Close();

            return user;
        }
        public User GetUserByUsername(string username)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT * FROM users WHERE username = @username";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            User user = new User();
            while (reader.Read())
            {
                user.user_id = Convert.ToInt32(reader["user_id"]);
                user.username = reader["username"].ToString();
                user.Email = reader["Email"].ToString();
                user.password = reader["password"].ToString();
                user.Name = reader["Name"].ToString();
            }
            conn.Close();

            return user;
        }
        public bool UpdateUser(User u)
        {
            using (SqlConnection conn = DbConnectionString.GetConnection())
            {
                conn.Open();

                // Check if the username or email already exists for a different user
                string checkQuery = "SELECT COUNT(*) FROM users WHERE (username = @username OR Email = @Email) AND user_id != @user_id";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", u.username);
                checkCmd.Parameters.AddWithValue("@Email", u.Email);
                checkCmd.Parameters.AddWithValue("@user_id", u.user_id);

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return false;
                }

                string hashedPassword = HashPassword(u.password);

                // Proceed with the update
                string query = "UPDATE users SET username = @username, Email = @Email, password = @password, Name = @Name WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@Name", u.Name);
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                cmd.ExecuteNonQuery();
                return true;
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public int GetUseridByUsername(string username)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT user_id FROM users WHERE username = @username";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            conn.Open();
            int user_id = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            return user_id;
        }

    }
}
