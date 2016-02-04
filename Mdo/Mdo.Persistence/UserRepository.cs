using System.Data.Entity.Migrations;
using System.Linq;
using Mdo.DB.Cfg;
using Mdo.DB.Entities;
using Mdo.Models;
using Mdo.Persistence.Adapters;
using Mdo.Persistence.Interfaces;

namespace Mdo.Persistence
{
    public class UserRepository: IUserRepository
    {
        public UserRepository()
        {
        }

        public UserModel GetUser(int id)
        {
            using (var context = new MdoDbContext())
            {
                var user = context.Users.Find(id);
                return new UserAdapter(user).UserModel;
            }
        }

        public UserModel GetByName(string username)
        {
            using (var context = new MdoDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Username == username);
                return new UserAdapter(user).UserModel;
            }
        }

        public UserModel GetByEmail(string email)
        {
            using (var context = new MdoDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                return new UserAdapter(user).UserModel;
            }
        }

        public UserModel GetUser(string usernameOrEmail)
        {
            UserModel user = GetByName(usernameOrEmail) ?? GetByEmail(usernameOrEmail);

            return user;
        }

        public void CreateUser(UserModel user)
        {
            using (var context = new MdoDbContext())
            {
                context.Users.Add(new UserAdapter(user).UserEntity);
                context.SaveChanges();
            }
        }

        public void UpdateUser(UserModel user)
        {
            using (var context = new MdoDbContext())
            {
                context.Users.AddOrUpdate(new UserAdapter(user).UserEntity);
                context.SaveChanges();
            }
        }
    }
}
