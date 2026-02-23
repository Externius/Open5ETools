namespace Open5ETools.Core.Common.Models.Services;

public record ChangePasswordModel(
    int Id,
    string CurrentPassword,
    string NewPassword
);