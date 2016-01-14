using Mdo.Persistence.Entities;

namespace Mdo.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Mdo.Persistence.Cfg.MdoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Mdo.Persistence.Cfg.MdoDbContext context)
        {
            context.Users.AddOrUpdate(x => new { x.Username, x.Password, x.Email },
            new User()
            {
                Username = "T1",
                Password = "hash",
                Email = "e@s.com"
            }, new User()
            {
                Username = "T2",
                Password = "hash2",
                Email = "e@s.com2"
            });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
