using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class Article
    {
        public Article(string title, string text, DateTime creationDate, BlogPage page, int id=-1)
        {
            Id = id;
            Title = title;
            Text = text;
            CreationDate = creationDate;
            Page = page;
        }

        public int Id { get; private set; }

        public string Title { get;  private set; }

        public string Text { get; private set; }

        //public IList<Tag> Tags { get; private set; }

        public DateTime CreationDate { get; private set; }

        public BlogPage Page { get; private set; }
    }
}
