using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdo.Persistence.Cfg;
using Mdo.Persistence.Domain;
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

        public User GetUser(string email)
        {
            User user;

            using (var context = new MdoDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.Email == email);
            }

            return user;
        }

        public async void CreateUser(User user)
        {
            using (var context = new MdoDbContext())
            {
                context.Users.Add(user);
                var result = await context.SaveChangesAsync();
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
