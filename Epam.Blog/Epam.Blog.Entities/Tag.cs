using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class Tag
    {
        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
            Articles = new List<Article>();
        }

        public int Id { get; }

        public string Name { get; }

        public IList<Article> Articles { get; private set; }
    }
}
