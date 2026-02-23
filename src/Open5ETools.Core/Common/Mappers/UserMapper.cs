using Open5ETools.Core.Common.Enums;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Core.Domain;

namespace Open5ETools.Core.Common.Mappers;

public static class UserMapper
{
    public static UserModel ToModel(this User user)
    {
        return new UserModel
        (
            user.Username,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Password,
            user.IsDeleted,
            user.Role.ToString(),
            user.Id,
            user.Timestamp,
            user.CreatedBy,
            user.Created,
            user.LastModifiedBy,
            user.LastModified
        );
    }

    public static User ToEntity(this UserModel user)
    {
        return new User
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            IsDeleted = user.IsDeleted,
            Role = Enum.Parse<Role>(user.Role),
            Id = user.Id,
            Timestamp = user.Timestamp,
            CreatedBy = user.CreatedBy,
            Created = user.Created,
            LastModifiedBy = user.LastModifiedBy,
            LastModified = user.LastModified
        };
    }
}