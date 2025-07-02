using AutoMapper;
using GameCatalogue.Application.CQRS.Commands;
using GameCatalogue.Application.Dtos;
using GameCatalogue.Application.Interfaces;
using GameCatalogue.Domain.Interfaces;
using MediatR;

namespace GameCatalogue.Application.CQRS.CommandHandlers
{
    public class UpdateGameHandler : IRequestHandler<UpdateGameCommand, GameDto>
    {
        private readonly IGameRepository _repo;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper _mapper;

        public UpdateGameHandler(IGameRepository repo, IFileStorageService fileStorage, IMapper mapper)
        {
            _repo = repo;
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        public async Task<GameDto> Handle(UpdateGameCommand cmd, CancellationToken cancellationToken)
        {
            var game = await _repo.GetByIdAsync(cmd.Id)
                ?? throw new KeyNotFoundException($"Game {cmd.Id} not found.");

            game.Name = cmd.Name;
            game.Price = cmd.Price;
            game.LastModified = cmd.LastModified ?? DateTime.UtcNow;
            game.Platforms = cmd.Platforms;
            await _repo.UpdateAsync(game);

            if (cmd.ImageStream != null && !string.IsNullOrEmpty(cmd.ImageExtension))
            {
                if (!string.IsNullOrEmpty(game.ImagePath))
                    await _fileStorage.DeleteFileAsync(game.ImagePath);
                
                var newPath = await _fileStorage.SaveImageAsync(game.Id, cmd.ImageStream, cmd.ImageExtension);

                game.ImagePath = newPath;
                await _repo.UpdateAsync(game);
            }

            return _mapper.Map<GameDto>(game);
        }
    }
}
