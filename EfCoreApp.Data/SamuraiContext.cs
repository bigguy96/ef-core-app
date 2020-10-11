using EfCoreApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApp.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            const string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SamuraiAppData";
            dbContextOptionsBuilder.UseSqlServer(connectionString);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(s => new { s.SamuraiId, s.BattleId });
            modelBuilder.Entity<Horse>().ToTable("Horses");
        }
    }
}