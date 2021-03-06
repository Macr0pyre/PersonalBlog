using System.Collections.Generic;

namespace Epam.Blog.Entities
{
    public class Tag
    {
        public Tag(string name, int id)
        {
            Id = id;
            Name = name;
            Articles = new List<Article>();
        }

        public int Id
        { get; set; }

        public string Name
        { get; set; }

        public IList<Article> Articles
        { get; set; }

        public override string ToString()
        {
            return " #" + Name;
        }
    }
}
