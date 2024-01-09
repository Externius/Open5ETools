namespace Open5ETools.Core.Common.Models.Services;

public class ChangePasswordModel
{
    public int Id { get; set; }
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}