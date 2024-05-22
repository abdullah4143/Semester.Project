using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class AdminService
	{
        

        // Admin Control Over Users

        public bool deleteUser(int id)
		{
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "delete from users where user_id = @user_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user_id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        
		}

		// Admin Control Over Catagories
        public void AddCatagory(string cat_name)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "insert into catagories(catagory_name) values (@catagory_name)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@catagory_name", cat_name);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void RemoveCatagory(int id)
		{
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "delete from catagories where catagory_id = @catagory_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@catagory_id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

		// Admin Control Over Comments

		public bool RemoveComment(int id)
		{
			throw new NotImplementedException();
		}

        // Admin Control Over Blogs

        public void deleteBlog(int id)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "delete from blogs where blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@blog_id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

       
	}
}
