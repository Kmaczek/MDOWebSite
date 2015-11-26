using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdo.Persistence.Domain;

namespace Mdo.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string email);
        void CreateUser(User user);
        void UpdateUser(User user);
    }
}
