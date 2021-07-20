using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class BlogPage
    {
        public BlogPage(string title, int id=-1)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public IList<Article> Articles { get; private set; }
    }
}
