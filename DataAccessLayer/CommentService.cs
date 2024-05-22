using DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CommentService
    {
        public List<Comment> GetBlogComments(int blog_id)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "select * from comments where blog_id = @blog_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@blog_id", blog_id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Comment> comments = new List<Comment>();
            while (reader.Read())
            {
                   Comment comment = new Comment();
                comment.comment_id = Convert.ToInt32(reader["comment_id"]);
                comment.blog_id = Convert.ToInt32(reader["blog_id"]);
                comment.user_id = Convert.ToInt32(reader["user_id"]);
                comment.content = reader["content"].ToString()!;
                comment.publish_date = reader["date"].ToString()!;
                comments.Add(comment);
            }
            return comments;
        }

        public void AddComment(Comment comment)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "insert into comments(blog_id, user_id, content, date) values (@blog_id, @user_id, @content, @publish_date)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@blog_id", comment.blog_id);
            cmd.Parameters.AddWithValue("@user_id", comment.user_id);
            cmd.Parameters.AddWithValue("@content", comment.content);
            cmd.Parameters.AddWithValue("@publish_date", comment.publish_date);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
