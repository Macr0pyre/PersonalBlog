using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class BlogPage
    {
        public BlogPage(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public int Id { get; }

        public string Title { get; private set; }

        public string Description { get; private set; }
    }
}
