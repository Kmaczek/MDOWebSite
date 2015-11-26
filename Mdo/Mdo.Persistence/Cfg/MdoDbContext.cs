using Mdo.Persistence.Domain;

namespace Mdo.Persistence.Cfg
{
    using System.Data.Entity;

    public partial class MdoDbContext : DbContext
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
