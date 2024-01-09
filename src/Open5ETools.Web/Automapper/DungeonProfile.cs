using AutoMapper;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Web.Models.Dungeon;

namespace Open5ETools.Web.Automapper;

public class DungeonProfile : Profile
{
    public DungeonProfile()
    {
        CreateMap<DungeonModel, DungeonViewModel>().ReverseMap();
    }
}