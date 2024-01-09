using AutoMapper;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Core.Domain;

namespace Open5ETools.Core.Services.Automapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}