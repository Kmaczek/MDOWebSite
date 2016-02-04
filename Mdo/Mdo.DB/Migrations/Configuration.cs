using System.Data.Entity.Migrations;
using Mdo.DB.Cfg;

namespace Mdo.DB.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MdoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MdoDbContext context)
        {
            var seeder = new MdoDBInitializer();
            seeder.Seed(context);
        }
    }
}
