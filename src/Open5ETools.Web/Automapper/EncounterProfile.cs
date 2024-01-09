using AutoMapper;
using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Web.Models.Encounter;

namespace Open5ETools.Web.Automapper;

public class EncounterProfile : Profile
{
    public EncounterProfile()
    {
        CreateMap<MonsterViewModel, MonsterModel>().ReverseMap();
        CreateMap<JsonMonsterViewModel, JsonMonsterModel>().ReverseMap();
        CreateMap<EncounterViewModel, EncounterModel>().ReverseMap();
        CreateMap<EncounterOptionViewModel, EncounterOption>()
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(s => s.SelectedSizes))
            .ForMember(dest => dest.MonsterTypes, opt => opt.MapFrom(s => s.SelectedMonsterTypes));
    }
}