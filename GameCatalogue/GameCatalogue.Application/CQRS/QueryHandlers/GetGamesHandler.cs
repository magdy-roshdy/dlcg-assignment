using AutoMapper;
using GameCatalogue.Application.CQRS.Queries;
using GameCatalogue.Application.Dtos;
using GameCatalogue.Domain.Interfaces;
using MediatR;

namespace GameCatalogue.Application.CQRS.QueryHandlers
{
    public class GetGamesHandler : IRequestHandler<GetGamesQuery, PagedResult<GameDto>>
    {
        private readonly IGameRepository _repo;
        private readonly IMapper _mapper;
        public GetGamesHandler(IGameRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedResult<GameDto>> Handle(GetGamesQuery q, CancellationToken ct)
        {
            var skip = (q.Page - 1) * q.PageSize;
            var (games, total) = await _repo.GetFilteredPagedAsync(
                q.Platforms,
                q.Prices,
                skip,
                q.PageSize);

            var dtos = _mapper.Map<List<GameDto>>(games);
            return new PagedResult<GameDto>(dtos, total);
        }
    }
}
