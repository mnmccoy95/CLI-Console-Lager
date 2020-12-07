using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }      

        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Blog(Title, URL) VALUES(@title, @url)";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {

        }

        public void Update(Blog blog)
        {

        }

        public List<Blog> GetAll()
        {
            List<Blog> blogList = new List<Blog>();
            return blogList;
        }

        public Blog Get(int id)
        {
            Blog blog = new Blog();
            return blog;
        }

    }
}
