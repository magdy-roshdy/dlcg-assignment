using GameCatalogue.Application.Dtos;
using GameCatalogue.Domain.Entities;
using AutoMapper;

namespace GameCatalogue.Application.Mapping
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Game, GameDto>();
        }
    }
}
