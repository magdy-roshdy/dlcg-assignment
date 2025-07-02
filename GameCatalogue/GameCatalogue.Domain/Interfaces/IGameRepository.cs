using GameCatalogue.Domain.Entities;

namespace GameCatalogue.Domain.Interfaces
{
    public interface IGameRepository
    {
        Task<(List<Game> Items, int TotalCount)> GetFilteredPagedAsync(IEnumerable<string> platforms, IEnumerable<string> priceFilters, int skip, int take);
        Task<Game?> GetByIdAsync(long id);
        Task UpdateAsync(Game game);
    }
}
