using Mdo.Models;

namespace Mdo.Persistence.Interfaces
{
    public interface IUserRepository
    {
        UserModel GetUser(int id);
        UserModel GetUser(string usernameOrEmail);
        UserModel GetByName(string username);
        UserModel GetByEmail(string email);
        void CreateUser(UserModel user);
        void UpdateUser(UserModel user);
    }
}
