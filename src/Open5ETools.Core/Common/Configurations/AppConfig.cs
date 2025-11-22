using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Core.Common.Configurations;

public class AppConfigOptions
{
    public const string AppConfig = "Config";
    [Required] public required string DefaultAdminPassword { get; set; }
    [Required] public required string DefaultUserPassword { get; set; }
}