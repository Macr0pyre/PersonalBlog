using System;
using System.Collections.Generic;
using System.Linq;
using Epam.Blog.Entities;
using Epam.Blog.DAL.Interfaces;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

namespace Epam.Blog.SqlDAL
{
    public class UserSqlDAO : IUserDAO
    {
        IBlogPageDAO blogDAO = new BlogPagesSqlDAO();

        public string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        public User AddUser(User user)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO dbo.Users(Login, PasswordHash, PageID)" +
                    "VALUES(@Login, @PasswordHash, @PageID); SELECT CAST(scope_identity() AS INT) AS ID";
                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@Login", user.Login);
                command.Parameters.AddWithValue("@PasswordHash", Hash(user.PasswordHash));
                command.Parameters.AddWithValue("@PageID", user.Page.Id);

                _connection.Open();

                var result = command.ExecuteScalar();

                new RoleManager().AddUserToRole(user.Login, "users");

                if (result != null)
                    return new User(
                        id: (int)result,
                        login: user.Login,
                        password: user.PasswordHash,
                        page: user.Page);

                throw new InvalidOperationException("Cannot add User");
            }
        }

        public User GetUserById(int id)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Users " + $"WHERE ID='{id}'";

                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@id", id);

                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new User(
                        id: (int)reader["Id"],
                        login: reader["Login"] as string,
                        password: reader["PasswordHash"] as string,
                        page: blogDAO.GetBlogPage((int)reader["PageID"]));
                }
                _connection.Close();

                throw new InvalidOperationException("Cannot find User with ID = " + id);
            }
        }

        public User GetUserByPage(BlogPage page)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT * FROM Users WHERE PageID='{page.Id}'";

                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@PageID", page.Id);

                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new User(
                        id: (int)reader["Id"],
                        login: reader["Login"] as string,
                        password: reader["PasswordHash"] as string,
                        page: blogDAO.GetBlogPage((int)reader["PageID"]));
                }
                _connection.Close();

                throw new InvalidOperationException("Cannot find User with BlogPage");
            }
        }

        public User GetUserByName(string login)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Users " + $"WHERE Login='{login}'";
                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@Login", login);

                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new User(
                        id: (int)reader["Id"],
                        login: reader["Login"] as string,
                        password: reader["PasswordHash"] as string,
                        page: blogDAO.GetBlogPage((int)reader["PageID"]));
                }

                _connection.Close();

                return null;
            }
        }

        public void RemoveUser(int id)
        {
            string sql = $"Delete From Users Where Id='{id}'";
            using (var _connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(sql, _connection);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void EditUser(int id, string newLogin)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = $"UPDATE dbo.Users SET Login='{newLogin}'" +
                    $"WHERE Id = '{id}'";
                var command = new SqlCommand(query, _connection);

                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public string Hash(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            for (int i = 0; i < buffer3.Length; i++)
            {
                if (buffer3[i] != buffer4[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool SignIn(string login, string password)
        {
            User user = GetUserByName(login);

            bool check = false;
            if (user != null)
            {
                check = VerifyHashedPassword(user.PasswordHash, password);
            }
            return check;
        }

    }
}


