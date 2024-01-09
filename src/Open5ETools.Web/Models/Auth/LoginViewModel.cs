using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.Auth;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}