using AutoMapper;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Web.Models.Dungeon;
using System.Globalization;

namespace Open5ETools.Web.Automapper;

public class DungeonOptionProfile : Profile
{
    public DungeonOptionProfile()
    {
        CreateMap<DungeonOptionCreateViewModel, DungeonOptionModel>()
            .ForMember(dest => dest.TreasureValue, opt => opt.MapFrom(src => Convert.ToDouble(src.TreasureValue, CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.MonsterType, opt => opt.Ignore());
        CreateMap<DungeonOptionModel, DungeonOptionCreateViewModel>()
            .ForMember(dest => dest.TreasureValue, opt => opt.MapFrom(src => src.TreasureValue.ToString()))
            .ForMember(dest => dest.MonsterType, opt => opt.MapFrom(src => GetMonsters(src)));
        CreateMap<DungeonOptionModel, DungeonOptionViewModel>().ReverseMap();
    }

    private static string[] GetMonsters(DungeonOptionModel model)
    {
        return model.MonsterType.Split(',');
    }
}