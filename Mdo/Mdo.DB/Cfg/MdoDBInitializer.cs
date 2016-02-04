using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Mdo.Core;
using Mdo.DB.Entities;

namespace Mdo.DB.Cfg
{
    public class MdoDBInitializer
    {
        private readonly IList<RoleEntity> Roles = new List<RoleEntity>()
        {
            new RoleEntity() { RoleId = 1, Name = RoleNames.Std },
            new RoleEntity() { RoleId = 2, Name = RoleNames.Admin},
            new RoleEntity() { RoleId = 3, Name = RoleNames.Mod }
        }; 

        private readonly IList<UserEntity> Users = new List<UserEntity>()
        {
            new UserEntity() { Email = "wes@wes.pl", Password = MdoSecurity.CreateHashedPassword("wes"), Username = "wes", Secret = MdoSecurity.CreateSecret()},
            new UserEntity() { Email = "zxx@zxx.pl", Password = MdoSecurity.CreateHashedPassword("zxx"), Username = "zxx", Secret = MdoSecurity.CreateSecret()},
            new UserEntity() { Email = "zcc@zcc.pl", Password = MdoSecurity.CreateHashedPassword("zcc"), Username = "zcc", Secret = MdoSecurity.CreateSecret()}
        }; 

        public void Seed(MdoDbContext context)
        {
            foreach (var role in Roles)
                context.Roles.AddOrUpdate(role);
            context.SaveChanges();

            foreach (var user in Users)
                context.Users.AddOrUpdate(x => new {x.Username, x.Email}, user);
            context.SaveChanges();

            AddRoles(context);
            context.SaveChanges();
        }

        private void AddRoles(MdoDbContext context)
        {
            var std = context.Roles.First(x => x.Name == RoleNames.Std);
            var admin = context.Roles.First(x => x.Name == RoleNames.Admin);
            var mod = context.Roles.First(x => x.Name == RoleNames.Mod);

            var wes = context.Users.First(x => x.Username == "wes");
            var zxx = context.Users.First(x => x.Username == "zxx");
            var zcc = context.Users.First(x => x.Username == "zcc");

            wes.Roles.Add(std);
            wes.Roles.Add(admin);
            wes.Roles.Add(mod);

            zxx.Roles.Add(std);

            zcc.Roles.Add(admin);
        }

        private static class RoleNames
        {
            public static string Std { get; } = "Standard";
            public static string Admin { get; } = "Administrator";
            public static string Mod { get; } = "Moderator";
        }
    }
}
