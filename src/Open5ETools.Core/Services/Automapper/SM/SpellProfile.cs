using AutoMapper;
using Open5ETools.Core.Common.Enums.SM;
using Open5ETools.Core.Common.Models.SM;
using Open5ETools.Core.Domain.SM;

namespace Open5ETools.Core.Services.Automapper.SM;

public class SpellProfile : Profile
{
    public SpellProfile()
    {
        CreateMap<Common.Models.Json.Spell, Spell>(MemberList.None)
            .ForMember(d => d.Concentration, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Concentration)
                        || !s.Concentration.Equals("no", StringComparison.InvariantCultureIgnoreCase)))
            .ForMember(d => d.Ritual, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Ritual)
                        || !s.Ritual.Equals("no", StringComparison.InvariantCultureIgnoreCase)))
            .ForMember(d => d.School, opt => opt.MapFrom(s => Enum.Parse<School>(s.School ?? string.Empty)));
        CreateMap<SpellModel, Spell>(MemberList.None).ReverseMap();
    }
}