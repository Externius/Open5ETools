using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Core.Common.Configurations;

public class AppConfigOptions
{
    public const string AppConfig = "Config";
    [Required]
    public string DefaultAdminPassword { get; set; } = string.Empty;
    [Required]
    public string DefaultUserPassword { get; set; } = string.Empty;
}