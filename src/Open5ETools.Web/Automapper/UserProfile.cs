using AutoMapper;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Web.Models.User;

namespace Open5ETools.Web.Automapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserModel, UserEditViewModel>().ReverseMap();
        CreateMap<UserModel, UserCreateViewModel>().ReverseMap();
    }
}