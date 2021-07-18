using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class Article
    {
        public Article(int id, string title, string text, DateTime creationDate, IList<Tag> tags)
        {
            Id = id;
            Title = title;
            Text = text;
            CreationDate = creationDate;
            Tags = tags;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Text { get; private set; }

        public DateTime CreationDate { get; private set; }

        public IList<Tag> Tags { get; private set; }

        public void Edit(string newText, string newTitle, IList<Tag> newTags)
        {
            Text = newText;
            Title = newTitle;
            Tags = newTags;
        }
    }
}
