using DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BlogsService
    {
        public List<Blog> GetBlogs()
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT * FROM blogs";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Blog> blogs = new List<Blog>();
            while (reader.Read())
            {
                Blog blog = new Blog();

                blog.blog_id = Convert.ToInt32(reader["blog_id"]);
                blog.catagory_id = Convert.ToInt32(reader["catagory_id"]);
                blog.title = reader["title"].ToString();
                blog.content = reader["content"].ToString();
                blog.publish_date = reader["publish_date"].ToString();
                blogs.Add(blog);
            }

            return blogs;
        }
        public void deleteBlog(int id)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "delete from blogs where blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@blog_id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            query = "delete from User_Blog where blog_id = @blog_id";
            SqlCommand cmd1 = new SqlCommand(query, conn);
            cmd1.Parameters.AddWithValue("@blog_id", id);
            conn.Open();
            cmd1.ExecuteNonQuery();
            conn.Close();
        }

        public bool CreateBlog(Blog blog, int userId)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "insert into blogs(user_id,catagory_id, title, content, publish_date) values(@user_id,@category_id, @title, @content, @publish_date)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user_id", userId);
            cmd.Parameters.AddWithValue("@category_id", blog.catagory_id);
            cmd.Parameters.AddWithValue("@title", blog.title);
            cmd.Parameters.AddWithValue("@content", blog.content);
            cmd.Parameters.AddWithValue("@publish_date", blog.publish_date);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


            return true;
        }
       public List<Blog> GetUsersBlogs(string email)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT * FROM blogs WHERE user_id IN (SELECT user_id FROM users WHERE email = @email)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Blog> blogs = new List<Blog>();
            while (reader.Read())
            {
                Blog blog = new Blog();
                blog.user_id = Convert.ToInt32(reader["user_id"]);
                blog.blog_id = Convert.ToInt32(reader["blog_id"]);
                blog.catagory_id = Convert.ToInt32(reader["catagory_id"]);
                blog.title = reader["title"].ToString();
                blog.content = reader["content"].ToString();
                blog.publish_date = reader["publish_date"].ToString();
                blogs.Add(blog);
            }
            return blogs;
        }

        public Blog GetBlogById(int id)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT * FROM blogs WHERE blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@blog_id", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Blog blog = new Blog();
            while (reader.Read())
            {
                blog.user_id = Convert.ToInt32(reader["user_id"]);
                blog.blog_id = Convert.ToInt32(reader["blog_id"]);
                blog.catagory_id = Convert.ToInt32(reader["catagory_id"]);
                blog.title = reader["title"].ToString();
                blog.content = reader["content"].ToString();
                blog.publish_date = reader["publish_date"].ToString();
            }
            conn.Close();

            return blog;
        }

        public List<Blog> GetBlogByCat(int id)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT * FROM blogs WHERE catagory_id = @catagory_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@catagory_id", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Blog> blogs = new List<Blog>();
            while (reader.Read())
            {
                Blog blog = new Blog();
                blog.user_id = Convert.ToInt32(reader["user_id"]);
                blog.blog_id = Convert.ToInt32(reader["blog_id"]);
                blog.title = reader["title"].ToString();
                blog.content = reader["content"].ToString();
                blog.publish_date = reader["publish_date"].ToString();
                blogs.Add(blog);
            }
            conn.Close();

            return blogs;
        }


        public List<Blog> GetRecentBlogs()
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "SELECT TOP 5 * FROM blogs ORDER BY publish_date DESC";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Blog> blogs = new List<Blog>();
            while (reader.Read())
            {
                Blog blog = new Blog();
                blog.user_id = Convert.ToInt32(reader["user_id"]);
                blog.blog_id = Convert.ToInt32(reader["blog_id"]);
                blog.catagory_id = Convert.ToInt32(reader["catagory_id"]);
                blog.title = reader["title"].ToString();
                blog.content = reader["content"].ToString();
                blog.publish_date = reader["publish_date"].ToString();
                blogs.Add(blog);
            }
            conn.Close();

            return blogs;
        }

    }

}

