namespace Open5ETools.Core.Common.Models.Services;

public record UserModel(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    bool IsDeleted,
    string Role,
    int Id,
    byte[] Timestamp,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified
) : EditModel(Id, Timestamp, CreatedBy, Created, LastModifiedBy, LastModified);