using Open5ETools.Core.Common.Enums;
using Open5ETools.Core.Common.Interfaces;

namespace Open5ETools.Core.Domain;

public class User : AuditableEntity, ISoftDelete
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
}