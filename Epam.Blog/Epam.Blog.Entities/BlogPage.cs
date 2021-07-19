using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class BlogPage
    {
        public BlogPage(int iD, string title)
        {
            ID = iD;
            Title = title;
        }

        public int ID { get; private set; }

        public string Title { get; private set; }
    }
}
