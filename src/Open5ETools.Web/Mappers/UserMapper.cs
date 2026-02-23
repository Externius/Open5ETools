using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Web.Models.User;

namespace Open5ETools.Web.Mappers;

public static class UserMapper
{
    public static UserEditViewModel ToEditViewModel(this UserModel user)
    {
        return new UserEditViewModel
        {
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            IsDeleted = user.IsDeleted,
            Created = user.Created,
            CreatedBy = user.CreatedBy,
            Id = user.Id,
            LastModified = user.LastModified,
            LastModifiedBy = user.LastModifiedBy,
            Timestamp = user.Timestamp
        };
    }

    public static UserModel ToModel(this UserCreateViewModel user)
    {
        return new UserModel
        (
            user.Username,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Password,
            user.IsDeleted,
            user.Role,
            user.Id,
            user.Timestamp,
            user.CreatedBy,
            user.Created,
            user.LastModifiedBy,
            user.LastModified
        );
    }

    public static UserModel ToModel(this UserEditViewModel user)
    {
        return new UserModel
        (
            user.Username,
            user.FirstName,
            user.LastName,
            user.Email,
            string.Empty,
            user.IsDeleted,
            user.Role,
            user.Id,
            user.Timestamp,
            user.CreatedBy,
            user.Created,
            user.LastModifiedBy,
            user.LastModified
        );
    }
}