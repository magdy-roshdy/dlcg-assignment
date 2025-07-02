using GameCatalogue.Domain.Entities;
using GameCatalogue.Domain.Seeding;

namespace GameCatalogue.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(AppDbContext ctx)
        {
            if (ctx.Games.Any()) return;

            ctx.Games.AddRange(GameSeedData.InitialGames);
            ctx.SaveChanges();
        }
    }
}
