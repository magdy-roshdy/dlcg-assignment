using AutoMapper;
using GameCatalogue.Application.CQRS.Queries;
using GameCatalogue.Application.Dtos;
using GameCatalogue.Domain.Interfaces;
using MediatR;

public class GetGameByIdHandler : IRequestHandler<GetGameByIdQuery, GameDto?>
{
    private readonly IGameRepository _repo;
    private readonly IMapper _mapper;
    public GetGameByIdHandler(IGameRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<GameDto?> Handle(GetGameByIdQuery q, CancellationToken ct)
    {
        var game = await _repo.GetByIdAsync(q.Id);
        if (game is null)
            return null;
        
        return _mapper.Map<GameDto>(game);
    }
}
