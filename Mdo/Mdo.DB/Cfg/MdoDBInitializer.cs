using System;
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

        private readonly IList<ExpansionEntity> Expansions = new List<ExpansionEntity>()
        {
            new ExpansionEntity() {ExpansionId = 1, ImagePath = @"mdo_images/expansions/origins.png", Name = "First", Started = DateTime.Today.AddDays(-30)},
            new ExpansionEntity() {ExpansionId = 2, ImagePath = @"mdo_images/expansions/origins.png", Name = "Second", Started = DateTime.Today}
        };

        private readonly IList<UserEntity> Users = new List<UserEntity>()
        {
            new UserEntity() { Email = "wes@wes.pl", Password = MdoSecurity.CreateHashedPassword("wes"), Username = "wes", Secret = MdoSecurity.CreateSecret()},
            new UserEntity() { Email = "zxx@zxx.pl", Password = MdoSecurity.CreateHashedPassword("zxx"), Username = "zxx", Secret = MdoSecurity.CreateSecret()},
            new UserEntity() { Email = "zcc@zcc.pl", Password = MdoSecurity.CreateHashedPassword("zcc"), Username = "zcc", Secret = MdoSecurity.CreateSecret()}
        };

        private readonly IList<CardEntity> Cards = new List<CardEntity>()
        {
            new CardEntity() { Name = "qwe", BlackMana = 1, BlueMana = 1, ColorlessMana = 2, CardType = "creature", ExpansionId = 1},
            new CardEntity() { Name = "qwe", RedMana = 1, BlueMana = 1, ColorlessMana = 1, CardType = "spell", ExpansionId = 2}
        };

        public void Seed(MdoDbContext context)
        {
            // independent tables
            foreach (var role in Roles)
                context.Roles.AddOrUpdate(role);

            foreach (var expansion in Expansions)
                context.Expansions.AddOrUpdate(expansion);

            context.SaveChanges();

            // dependent tables
            foreach (var user in Users)
                context.Users.AddOrUpdate(x => new { x.Username, x.Email }, user);
            foreach (var card in Cards)
                context.Cards.AddOrUpdate(card);

            context.SaveChanges();

            // relations
            AddRolesToUsers(context);

            context.SaveChanges();
        }

        private void AddRolesToUsers(MdoDbContext context)
        {
            var std = context.Roles.First(x => x.Name == RoleNames.Std);
            var admin = context.Roles.First(x => x.Name == RoleNames.Admin);
            var mod = context.Roles.First(x => x.Name == RoleNames.Mod);

            var wes = context.Users.First(x => x.Username == "wes");
            var zxx = context.Users.First(x => x.Username == "zxx");
            var zcc = context.Users.First(x => x.Username == "zcc");

            wes.Roles = new List<RoleEntity> {std, admin, mod};

            zxx.Roles = new List<RoleEntity> {std};

            zcc.Roles = new List<RoleEntity> {admin};
        }

        private void AddExpansionsToCards(MdoDbContext context)
        {
            
        }

        private static class RoleNames
        {
            public static string Std { get; } = "standard";
            public static string Admin { get; } = "administrator";
            public static string Mod { get; } = "moderator";
        }
    }
}
