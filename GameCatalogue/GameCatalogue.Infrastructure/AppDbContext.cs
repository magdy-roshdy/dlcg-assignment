using GameCatalogue.Domain.Entities;
using GameCatalogue.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogue.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
