using System.Linq.Expressions;
using GameCatalogue.Domain.Entities;
using GameCatalogue.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogue.Infrastructure
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _ctx;
        public GameRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<(List<Game> Items, int TotalCount)> GetFilteredPagedAsync(
            IEnumerable<string> platforms,
            IEnumerable<string> priceFilters,
            int skip,
            int take)
        {
            var query = _ctx.Games.AsNoTracking();

            var pf = platforms?
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim().ToLower())
                .ToArray()
              ?? [];

            if (pf.Length > 0 && !pf.Contains("all"))
            {
                query = query.Where(g =>
                    pf.Any(pl => g.Platforms.ToLower().Contains(pl))
                );
            }

            var tokens = priceFilters?
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim().ToLower())
                .ToArray()
              ?? [];

            if (tokens.Length > 0)
            {
                var param = Expression.Parameter(typeof(Game), "g");
                Expression? exp = null;
                var priceProp = Expression.Property(param, nameof(Game.Price));

                foreach (var token in tokens)
                {
                    Expression? predicate = null;

                    if (token == "free")
                    {
                        predicate = Expression.Equal(priceProp, Expression.Constant(0m));
                    }
                    else if (token.StartsWith("lt") &&
                        decimal.TryParse(token.Substring(2), out var max))
                    {
                        predicate = Expression.LessThan(priceProp, Expression.Constant(max));
                    }
                    else if (token.StartsWith("gt") &&
                             decimal.TryParse(token.Substring(2), out var min))
                    {
                        predicate = Expression.GreaterThan(priceProp, Expression.Constant(min));
                    }
                    else if (token.Contains('-'))
                    {
                        var parts = token.Split('-', 2);
                        if (decimal.TryParse(parts[0], out var lo) &&
                            decimal.TryParse(parts[1], out var hi))
                        {
                            var lower = Expression.GreaterThanOrEqual(priceProp, Expression.Constant(lo));
                            var upper = Expression.LessThanOrEqual(priceProp, Expression.Constant(hi));
                            predicate = Expression.AndAlso(lower, upper);
                        }
                    }

                    if (predicate != null)
                    {
                        exp = exp == null ? predicate : Expression.OrElse(exp, predicate);
                    }
                }

                if (exp != null)
                {
                    var lambda = Expression.Lambda<Func<Game, bool>>(exp, param);
                    query = query.Where(lambda);
                }
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderBy(g => g.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return (items, total);
        }

        public Task<Game?> GetByIdAsync(long id)
          => _ctx.Games.FindAsync(id).AsTask();

        public async Task UpdateAsync(Game game)
        {
            _ctx.Games.Update(game);
            await _ctx.SaveChangesAsync();
        }
    }
}
