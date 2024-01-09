using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Web.Models.Profile;

namespace Open5ETools.Web.Controllers.Web;

[Authorize]
public class ProfileController(IUserService userService,
    ICurrentUserService currentUserService,
    IMapper mapper,
    ILogger<ProfileController> logger) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = await _userService.GetAsync(_currentUserService.GetUserIdAsInt(), cancellationToken);
        return View(_mapper.Map<ProfileViewModel>(model));
    }


    [HttpGet]
    public IActionResult ChangePassword()
    {
        ViewData["ReturnUrl"] = Url.Action(nameof(Index));
        return View(new ProfileChangePasswordModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ProfileChangePasswordModel model, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.ChangePasswordAsync(_mapper.Map<ChangePasswordModel>(model), cancellationToken);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password.");
                ModelState.AddModelError("", ex.Message);
            }
        }
        return View(model);
    }
}