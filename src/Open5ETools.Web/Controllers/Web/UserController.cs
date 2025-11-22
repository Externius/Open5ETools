using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Core.Domain;
using Open5ETools.Web.Extensions;
using Open5ETools.Web.Models.User;

namespace Open5ETools.Web.Controllers.Web;

[Authorize(Roles = Roles.Admin)]
public class UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IActionResult> Index()
    {
        var list = await _userService.ListAsync(null);

        return View(new UserListViewModel
        {
            Details = list.Select(_mapper.Map<UserEditViewModel>)
        });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new UserCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.CreateAsync(_mapper.Map<UserModel>(model), cancellationToken);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.HandleException(ex, _logger, "Error creating user.");
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<UserEditViewModel>(await _userService.GetAsync(id, cancellationToken));
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserEditViewModel model, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var user = new UserModel();
                _mapper.Map(model, user);
                await _userService.UpdateAsync(user, cancellationToken);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.HandleException(ex, _logger, "Error editing user.");
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            this.HandleException(ex, _logger, "Error deleting user.");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _userService.RestoreAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            this.HandleException(ex, _logger, "Error restoring user.");
        }

        return RedirectToAction("Index");
    }
}