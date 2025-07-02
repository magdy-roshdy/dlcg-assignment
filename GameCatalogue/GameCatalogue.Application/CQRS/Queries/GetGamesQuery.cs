using GameCatalogue.Application.Dtos;
using MediatR;

namespace GameCatalogue.Application.CQRS.Queries
{
    public record GetGamesQuery(int Page,
        int PageSize,
        string[] Platforms,
        string[] Prices) : IRequest<PagedResult<GameDto>>;
}
