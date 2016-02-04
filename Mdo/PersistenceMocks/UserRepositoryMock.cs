using System;
using Mdo.DB.Entities;
using Mdo.Models;
using Mdo.Persistence.Adapters;
using Mdo.Persistence.Interfaces;

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

        public UserModel GetUser(int id)
        {
            var adapter = new UserAdapter(userWarehouse.GetUser(id));
            return adapter.UserModel;
        }

        public UserModel GetUser(string usernameOrEmail)
        {
            var user = userWarehouse.GetUserByName(usernameOrEmail);
            user = user ?? userWarehouse.GetUserByEmail(usernameOrEmail);
            var adapter = new UserAdapter(user);

            return adapter.UserModel;
        }

        public UserModel GetByName(string username)
        {
            var adapter = new UserAdapter(userWarehouse.GetUserByName(username));
            return adapter.UserModel;
        }

        public UserModel GetByEmail(string email)
        {
            var adapter = new UserAdapter(userWarehouse.GetUserByEmail(email));
            return adapter.UserModel;
        }

        public void CreateUser(UserModel user)
        {
            userWarehouse.AddOrUpdate(new UserAdapter(user).UserEntity);
        }

        public void UpdateUser(UserModel user)
        {
            userWarehouse.AddOrUpdate(new UserAdapter(user).UserEntity);
        }
    }
}
