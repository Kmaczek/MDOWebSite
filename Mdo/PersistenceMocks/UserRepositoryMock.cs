using Mdo.Persistence.Entities;
using Mdo.Persistence.Repositories.Interfaces;

namespace PersistenceMocks
{
    public class UserRepositoryMock : IUserRepository
    {
        public User GetUser(int id)
        {
            return UserWarehouse.GetUser(id);
        }

        public User GetUser(string usernameOrEmail)
        {
            return UserWarehouse.GetUser(u => u.Email == usernameOrEmail || u.Username == usernameOrEmail);
        }

        public User GetByName(string username)
        {
            return UserWarehouse.GetUser(u => u.Username == username);
        }

        public User GetByEmail(string email)
        {
            return UserWarehouse.GetUser(u => u.Email == email);
        }

        public void CreateUser(User user)
        {
            UserWarehouse.AddOrUpdate(user);
        }

        public void UpdateUser(User user)
        {
            UserWarehouse.AddOrUpdate(user);
        }
    }
}
