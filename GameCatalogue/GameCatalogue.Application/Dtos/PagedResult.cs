namespace GameCatalogue.Application.Dtos
{
    public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount);
}
