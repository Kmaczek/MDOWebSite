using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Mdo.Persistence.Entities;
using Mdo.Persistence.Repositories.Interfaces;

namespace PersistenceMocks
{
    [Serializable]
    public class UserRepositoryMock : IUserRepository
    {
        private readonly UserWarehouse userWarehouse;
        public UserRepositoryMock()
        {
            userWarehouse = UserWarehouse.GetInstance();
        }

        public User GetUser(int id)
        {
            return userWarehouse.GetUser(id);
        }

        public User GetUser(string usernameOrEmail)
        {
            var user = userWarehouse.GetUserByName(usernameOrEmail);
            user = user ?? userWarehouse.GetUserByEmail(usernameOrEmail);
            return user;
        }

        public User GetByName(string username)
        {
            return userWarehouse.GetUserByName(username);
        }

        public User GetByEmail(string email)
        {
            return userWarehouse.GetUserByEmail(email);
        }

        public void CreateUser(User user)
        {
            userWarehouse.AddOrUpdate(user);
        }

        public void UpdateUser(User user)
        {
            userWarehouse.AddOrUpdate(user);
        }
    }
}
