using Epam.Blog.BLL.Interfaces;
using Epam.Blog.Entities;
using Epam.Blog.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace Epam.Blog.BLL
{
    public class ArticlesLogic : IArticlesLogic
    {
        public IArticleDAO _articlesDAO;

        public ArticlesLogic(IArticleDAO articlesDao)
        {
            _articlesDAO = articlesDao;
        }

        public Article AddArticle(Article article) => _articlesDAO.AddArticle(article);

        public void EditArticle(int id, string newText, string newTitle, List<Tag> tags)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetAllArticles() => _articlesDAO.GetAllArticles();

        public Article GetArticle(int id)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetArticlesByPage(BlogPage page) => _articlesDAO.GetArticlesByPage(page);

        public void RemoveArticle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
