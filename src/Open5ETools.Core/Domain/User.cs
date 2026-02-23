using System.ComponentModel.DataAnnotations;
using Open5ETools.Core.Common.Enums;
using Open5ETools.Core.Common.Interfaces;

namespace Open5ETools.Core.Domain;

public class User : AuditableEntity, ISoftDelete
{
    [StringLength(short.MaxValue)] public required string Username { get; set; }
    [StringLength(short.MaxValue)] public required string FirstName { get; set; }
    [StringLength(short.MaxValue)] public required string LastName { get; set; }
    [StringLength(short.MaxValue)] public required string Email { get; set; }
    [StringLength(short.MaxValue)] public required string Password { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
}