using Epam.Blog.DAL.Interfaces;
using Epam.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.SqlDAL
{
    public class BlogPagesSqlDAO : IBlogPageDAO
    {
        public string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public BlogPage AddBlogPage(BlogPage page)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO dbo.Pages(Name)" +
                    "VALUES(@Name); SELECT CAST(scope_identity() AS INT) AS NewID";

                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@Name", page.Title);

                _connection.Open();

                var result = command.ExecuteScalar();

                if (result != null) 
                    return new BlogPage(
                        id: (int)result,
                        title: page.Title);

                throw new InvalidOperationException("Cannot add BlogPage");
            }
        }

        public void EditBlogPage(int id, string newName)
        {
            throw new NotImplementedException();
        }

        public BlogPage GetBlogPage(int id)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Pages " + $"WHERE ID='{id}'";

                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@id", id);

                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new BlogPage(id: (int)reader["Id"],
                        title: reader["Name"] as string);
                }
                _connection.Close();

                throw new InvalidOperationException("Cannot find BlogPage with ID = " + id);
            }
        }

        public void RemoveBlogPage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
