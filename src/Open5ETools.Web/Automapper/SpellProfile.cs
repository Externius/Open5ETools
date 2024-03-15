using AutoMapper;
using Open5ETools.Core.Common.Models.SM;
using Open5ETools.Web.Models.Spell;

namespace Open5ETools.Web.Automapper;

public class SpellProfile : Profile
{
    public SpellProfile()
    {
        CreateMap<SpellModel, SpellViewModel>().ReverseMap();
    }
}