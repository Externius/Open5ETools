using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Web.Models.Profile;

namespace Open5ETools.Web.Mappers;

public static class ProfileMapper
{
    public static ProfileViewModel ToViewModel(this UserModel user)
    {
        return new ProfileViewModel
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Id = user.Id,
            Created = user.Created,
            CreatedBy = user.CreatedBy,
            LastModified = user.LastModified,
            LastModifiedBy = user.LastModifiedBy,
            Timestamp = user.Timestamp
        };
    }

    public static ChangePasswordModel ToPasswordModel(this ProfileChangePasswordModel model)
    {
        return new ChangePasswordModel
        (
            model.Id,
            model.CurrentPassword,
            model.NewPassword
        );
    }
}