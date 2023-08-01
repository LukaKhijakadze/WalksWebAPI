using AutoMapper;
using WalksAPI.Database.DomainModel;
using WalksAPI.Database.DTO;
using WalksAPI.Database.DTOModel;
using WalksAPI.DataModels.Domain;

namespace WalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

            CreateMap<Walk, AddWalksRequestDto>().ReverseMap();
            CreateMap<Walk, UpdateWalksRequestDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
