using AutoMapper;
using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Core.Domain.EG;

namespace Open5ETools.Core.Services.Automapper.EG;

public class MonsterProfile : Profile
{
    public MonsterProfile()
    {
        CreateMap<Common.Models.Json.Monster, JsonMonsterModel>(MemberList.None)
            .ForMember(d => d.Hp, opt => opt.MapFrom(s => s.HitPoints))
            .ForMember(d => d.Ac, opt => opt.MapFrom(s => s.ArmorClass));
        CreateMap<Monster, MonsterModel>()
            .ForMember(d => d.JsonMonsterModel, opt => opt.MapFrom(s => s.JsonMonster));
    }
}