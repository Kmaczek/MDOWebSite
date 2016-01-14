using Mdo.Persistence.Entities;

namespace Mdo.Persistence.Cfg
{
    using System.Data.Entity;

    public class MdoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MdoDbContext()
            : base("name=MdoDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
