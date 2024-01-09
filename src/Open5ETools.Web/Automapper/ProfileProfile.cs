using AutoMapper;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Web.Models.Profile;

namespace Open5ETools.Web.Automapper;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<UserModel, ProfileViewModel>().ReverseMap();
        CreateMap<ChangePasswordModel, ProfileChangePasswordModel>().ReverseMap();
    }
}