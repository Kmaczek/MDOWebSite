using System.Data.Entity.Migrations;
using System.Linq;
using Mdo.Persistence.Cfg;
using Mdo.Persistence.Entities;
using Mdo.Persistence.Repositories.Interfaces;

namespace Mdo.Persistence.Repositories
{
    public class UserRepository: IUserRepository
    {
        public UserRepository()
        {
        }

        public User GetUser(int id)
        {
            User user;

            using (var context = new MdoDbContext())
            {
                user = context.Users.Find(id);
            }

            return user;
        }

        public User GetByName(string username)
        {
            User user;

            using (var context = new MdoDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.Username == username);
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            User user;

            using (var context = new MdoDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.Email == email);
            }

            return user;
        }

        public User GetUser(string usernameOrEmail)
        {
            User user = GetByName(usernameOrEmail) ?? GetByEmail(usernameOrEmail);

            return user;
        }

        public void CreateUser(User user)
        {
            using (var context = new MdoDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            using (var context = new MdoDbContext())
            {
                context.Users.AddOrUpdate(user);
                context.SaveChanges();
            }
        }
    }
}
