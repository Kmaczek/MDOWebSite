using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdo.Persistence.Entities;

namespace Mdo.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string usernameOrEmail);
        User GetByName(string username);
        User GetByEmail(string email);
        void CreateUser(User user);
        void UpdateUser(User user);
    }
}
