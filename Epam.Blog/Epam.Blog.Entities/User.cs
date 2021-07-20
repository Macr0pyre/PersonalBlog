using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.Entities
{
    public class User
    {
        public User(string login, string password, BlogPage page, int id = -1)
        {
            Id = id;
            Login = login;
            PasswordHash = password;
            Page = page;
        }

        public int Id { get; private set; }

        public string Login { get; private set; }

        public string PasswordHash { get; private set; }

        public BlogPage Page { get; private set; }

    }
}
