using GameCatalogue.Application.Dtos;
using MediatR;

namespace GameCatalogue.Application.CQRS.Commands
{
    public record UpdateGameCommand(long Id,
      string Name,
      decimal Price,
      DateTime? LastModified,
      string Platforms,
      Stream? ImageStream,
      string? ImageExtension) : IRequest<GameDto>;

}
