using Epam.Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Blog.DAL.Interfaces
{
    public interface IUserDAO
    {
        User AddUser(string name, string login, string password);

        User GetUser(int id);

        void EditUser(int id, string newName, string newLogin, string newPassword);

        void RemoveUser(int id);

        IEnumerable<User> GetUsers();
    }
}
