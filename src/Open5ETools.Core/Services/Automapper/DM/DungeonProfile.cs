using AutoMapper;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Core.Domain.DM;

namespace Open5ETools.Core.Services.Automapper.DM;

public class DungeonProfile : Profile
{
    public DungeonProfile()
    {
        CreateMap<DungeonOption, DungeonOptionModel>().ReverseMap();
        CreateMap<Dungeon, DungeonModel>().ReverseMap();
    }
}