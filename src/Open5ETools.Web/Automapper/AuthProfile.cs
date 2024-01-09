using AutoMapper;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Web.Models.Auth;

namespace Open5ETools.Web.Automapper;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<UserModel, LoginViewModel>().ReverseMap();
    }
}