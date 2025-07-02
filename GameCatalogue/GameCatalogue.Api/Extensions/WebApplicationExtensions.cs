using GameCatalogue.Infrastructure;
using GameCatalogue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogue.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication ApplyMigrationsAndSeed(this WebApplication app, bool isDevelopment)
        {
            using var scope = app.Services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            ctx.Database.Migrate();

            //seed data if in development environment only
            if (isDevelopment)
                DatabaseSeeder.Seed(ctx);

            return app;
        }
    }
}
