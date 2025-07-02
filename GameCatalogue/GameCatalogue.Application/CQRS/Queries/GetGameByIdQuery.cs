using MediatR;
using GameCatalogue.Application.Dtos;

namespace GameCatalogue.Application.CQRS.Queries
{
    public record GetGameByIdQuery(long Id) : IRequest<GameDto?>;
}
