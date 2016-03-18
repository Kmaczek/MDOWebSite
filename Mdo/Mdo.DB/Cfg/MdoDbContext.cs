using System.Data.Entity;
using Mdo.DB.Entities;

namespace Mdo.DB.Cfg
{
    public class MdoDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<ExpansionEntity> Expansions { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<TypeEntity> Types { get; set; }

        public MdoDbContext()
            : base("name=MdoDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRole");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });

            modelBuilder.Entity<CardEntity>()
                .HasMany(u => u.Tags)
                .WithMany(r => r.Cards)
                .Map(m =>
                {
                    m.ToTable("CardTags");
                    m.MapLeftKey("CardId");
                    m.MapRightKey("TagId");
                });

            modelBuilder.Entity<CardEntity>()
                .HasMany(u => u.Types)
                .WithMany(r => r.Cards)
                .Map(m =>
                {
                    m.ToTable("CardTypes");
                    m.MapLeftKey("CardId");
                    m.MapRightKey("TypeId");
                });
        }
    }
}
