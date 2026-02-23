using System.ComponentModel.DataAnnotations;

namespace Open5ETools.Web.Models.User;

public class UserCreateViewModel : EditViewModel
{
    [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "RequiredValidation")]
    [Display(ResourceType = typeof(Resources.User), Name = "Username")]
    public required string Username { get; set; }

    [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "RequiredValidation")]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resources.User), Name = "Password")]
    public required string Password { get; set; }

    [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources.Error))]
    [Compare("Password", ErrorMessageResourceName = "ConfirmPassword",
        ErrorMessageResourceType = typeof(Resources.Error))]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resources.User), Name = "ConfirmPassword")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "RequiredValidation")]
    [Display(ResourceType = typeof(Resources.User), Name = "FirstName")]
    public required string FirstName { get; set; }

    [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "RequiredValidation")]
    [Display(ResourceType = typeof(Resources.User), Name = "LastName")]
    public required string LastName { get; set; }

    [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "RequiredValidation")]
    [Display(ResourceType = typeof(Resources.User), Name = "Email")]
    public required string Email { get; set; }

    [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "RequiredValidation")]
    [Display(ResourceType = typeof(Resources.User), Name = "Role")]
    public required string Role { get; set; }

    public bool IsDeleted { get; set; }
}