using System;
using System.Collections.Generic;
using Epam.Blog.Entities;

namespace Epam.Blog.BLL.Interfaces
{
    public interface IArticlesLogic
    {
        Article AddArticle(Article article);

        List<Article> GetArticlesByPage(BlogPage page);

        List<Article> GetAllArticles();

        Article GetArticle(int id);

        void RemoveArticle(int id);

        void EditArticle(int id, string newText, string newTitle, List<Tag> tags);
    }
}
