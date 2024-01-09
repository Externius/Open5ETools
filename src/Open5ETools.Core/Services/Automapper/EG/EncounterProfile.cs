using AutoMapper;
using Open5ETools.Core.Common.Models.EG;

namespace Open5ETools.Core.Services.Automapper.EG;

public class EncounterProfile : Profile
{
    public EncounterProfile()
    {
        CreateMap<Common.Models.Json.Monster, EncounterDetail>(MemberList.None)
            .ForMember(d => d.Hp, opt => opt.MapFrom(s => s.HitPoints))
            .ForMember(d => d.Ac, opt => opt.MapFrom(s => s.ArmorClass));
        CreateMap<MonsterModel, EncounterDetail>(MemberList.None).ReverseMap();
    }
}