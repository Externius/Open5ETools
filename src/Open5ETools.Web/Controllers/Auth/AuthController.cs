using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Models.Services;
using Open5ETools.Web.Models.Auth;
using System.Security.Claims;

namespace Open5ETools.Web.Controllers.Auth;

public class AuthController(IAuthService authService, IMapper mapper) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;

    public IActionResult Login()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            return RedirectToAction("Index", "Dungeon");
        }
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model, string? returnUrl, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var user = await _authService.LoginAsync(_mapper.Map<UserModel>(model), cancellationToken);

            if (user is null)
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return View();
            }

            var claims = new List<Claim>
            {
                new(JwtClaimTypes.Name, user.Username),
                new(JwtClaimTypes.Id, user.Id.ToString()),
                new(JwtClaimTypes.Email, user.Email),
                new(JwtClaimTypes.Role, user.Role)
            };

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, JwtClaimTypes.Subject, JwtClaimTypes.Role);

            var authProperties = (await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme)).Properties;
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(userIdentity),
                authProperties);

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Dungeon");
            return Redirect(returnUrl);
        }
        return View();
    }
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Forbidden()
    {
        return View();
    }
}