using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Web.Extensions;
using Open5ETools.Web.Mappers;
using Open5ETools.Web.Models.Profile;

namespace Open5ETools.Web.Controllers.Web;

[Authorize]
public class ProfileController(
    IUserService userService,
    ICurrentUserService currentUserService,
    ILogger<ProfileController> logger) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ILogger _logger = logger;

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = await _userService.GetAsync(_currentUserService.GetUserIdAsInt(), cancellationToken);
        return View(model.ToViewModel());
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        ViewData["ReturnUrl"] = Url.Action(nameof(Index));
        return View(new ProfileChangePasswordModel
        {
            CurrentPassword = string.Empty,
            NewPassword = string.Empty,
            ConfirmPassword = string.Empty
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ProfileChangePasswordModel model,
        CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.ChangePasswordAsync(model.ToPasswordModel(), cancellationToken);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.HandleException(ex, _logger, "Error changing password.");
            }
        }

        ViewData["ReturnUrl"] = Url.Action(nameof(Index));
        return View(model);
    }
}