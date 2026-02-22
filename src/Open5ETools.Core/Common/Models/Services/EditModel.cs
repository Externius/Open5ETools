namespace Open5ETools.Core.Common.Models.Services;

public record EditModel(
    int Id,
    byte[] Timestamp,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified
);