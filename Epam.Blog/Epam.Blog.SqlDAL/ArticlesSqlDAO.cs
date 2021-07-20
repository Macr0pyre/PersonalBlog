using Epam.Blog.Entities;
using Epam.Blog.DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Epam.Blog.SqlDAL
{
    public class ArticlesSqlDAO : IArticleDAO
    {
        public static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        IBlogPageDAO blogDAO = new BlogPagesSqlDAO();

        public Article AddArticle(Article article)
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO dbo.Articles(Title, Text, CreationDate, PageID)" +
                    "VALUES(@Title, @Text, @CreationDate, @PageID); SELECT CAST(scope_identity() AS INT) AS NewID";
                var command = new SqlCommand(query, _connection);

                command.Parameters.AddWithValue("@Title", article.Title);
                command.Parameters.AddWithValue("@Text", article.Text);
                command.Parameters.AddWithValue("@CreationDate", article.CreationDate);
                command.Parameters.AddWithValue("@PageID", article.Page.Id);

                _connection.Open();

                var result = command.ExecuteScalar();

                if (result != null)
                    return new Article(
                        id: (int)result,
                        title: article.Title,
                        text: article.Text,
                        creationDate: article.CreationDate,
                        page: article.Page);

                throw new InvalidOperationException(string.Format("Cannot add Article"));
            }
        }

        public List<Article> GetArticlesByPage(BlogPage page)
        {
            List<Article> articles = new List<Article>();
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT ID, Title, Text, CreationDate, PageID FROM dbo.Articles WHERE PageID='{page.Id}'";


                var command = new SqlCommand(query, _connection);

                _connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Article article = new Article(
                        id: (int)reader["ID"],
                        title: reader["Title"] as string,
                        text: reader["Text"] as string,
                        creationDate: (DateTime)reader["CreationDate"],
                        page: blogDAO.GetBlogPage((int)reader["PageID"]));

                    articles.Add(article);
                }

                _connection.Close();
            }
            return articles;
        }

        public List<Article> GetAllArticles()
        {
            List<Article> articles = new List<Article>();
            using (var _connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT ID, Title, Text, CreationDate, PageID FROM dbo.Articles ORDER BY CreationDate DESC";


                var command = new SqlCommand(query, _connection);

                _connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Article article = new Article(
                        id: (int)reader["ID"],
                        title: reader["Title"] as string,
                        text: reader["Text"] as string,
                        creationDate: (DateTime)reader["CreationDate"],
                        page: blogDAO.GetBlogPage((int)reader["PageID"]));

                    articles.Add(article);
                }

                _connection.Close();
            }
            return articles;
        }

        public void EditArticle(int id, string newText, string newTitle, List<Tag> tags)
        {
            throw new NotImplementedException();
        }

        public Article GetArticle(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveArticle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
