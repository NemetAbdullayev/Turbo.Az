using EntityLayer.Tables;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Server=NEMET;Database=TurboAz;Integrated Security=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            modelBuilder.Entity<Car>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<User>()
           .HasIndex(p => p.UserName).IsUnique();
            modelBuilder.Entity<EntityLayer.Tables.File>().HasQueryFilter(m => !m.IsDeleted);



        }
        public DbSet<User> Users { get; set; }
        public DbSet<BanType> BanTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
         public DbSet<Car> Cars { get; set; }
        public DbSet<EntityLayer.Tables.Color> Colors { get; set; }
        public DbSet<EntityLayer.Tables.File> Files { get; set; }
    }
}