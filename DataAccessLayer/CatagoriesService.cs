using DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CatagoriesService
    {
        public List<Catagory> GetCatagories()
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "select * from catagories";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Catagory> catagories = new List<Catagory>();

            while (reader.Read())
            {
                Catagory catagory = new Catagory();
                catagory.catagory_id =  Convert.ToInt32(reader["catagory_id"]);
                catagory.catagory_name = reader["catagory_name"].ToString()!;
                catagories.Add(catagory);
            }


            return catagories;
        }

        public Catagory GetCatagory(int catagory_id)
        {
            SqlConnection conn = DbConnectionString.GetConnection();
            string query = "select * from catagories where catagory_id = @catagory_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@catagory_id", catagory_id);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Catagory catagory = new Catagory();

            while (reader.Read())
            {
                catagory.catagory_id = Convert.ToInt32(reader["catagory_id"]);
                catagory.catagory_name = reader["catagory_name"].ToString()!;
            }


            return catagory;
        }

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
    }
}
