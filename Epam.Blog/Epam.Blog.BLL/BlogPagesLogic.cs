using Epam.Blog.BLL.Interfaces;
using Epam.Blog.DAL.Interfaces;
using Epam.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.BLL
{
    public class BlogPagesLogic : IBlogPagesLogic
    {
        public IBlogPageDAO _pageDAO;

        public BlogPagesLogic(IBlogPageDAO pageDAO)
        {
            _pageDAO = pageDAO;
        }

        public BlogPage AddBlogPage(BlogPage page)
        {
            return _pageDAO.AddBlogPage(page);
        }

        public void EditBlogPage(int id, string newName)
        {
            _pageDAO.EditBlogPage(id, newName);
        }

        public BlogPage GetBlogPage(int id)
        {
            return _pageDAO.GetBlogPage(id);
        }

        public void RemoveBlogPage(int id)
        {
            _pageDAO.RemoveBlogPage(id);
        }
    }
}
