using Epam.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.DAL.Interfaces
{
    public interface IBlogPageDAO
    {
        BlogPage AddBlogPage(string title, string description);

        BlogPage GetBlogPage(int id);

        void EditBlogPage(int id, string title, string description);

        void RemoveBlogPage(int id);
    }
}
