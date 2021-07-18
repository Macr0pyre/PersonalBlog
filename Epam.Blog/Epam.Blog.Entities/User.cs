using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class User
    {
        public User(int id, string name, string login, string password, BlogPage page)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
            Page = page;
        }

        public int Id { get;}

        public string Name { get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public BlogPage Page {get; private set;}
    }
}
