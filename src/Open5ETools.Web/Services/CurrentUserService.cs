using IdentityModel;
using Open5ETools.Core.Common.Interfaces.Services;
using System.Security.Claims;

namespace Open5ETools.Web.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public int GetUserIdAsInt()
    {
        return int.TryParse(UserId, out var result) ? result : -1;
    }

    public string UserId => _httpContextAccessor.HttpContext?
        .User
        .FindFirstValue(JwtClaimTypes.Id) ?? string.Empty;

    public string UserName => _httpContextAccessor.HttpContext?
        .User
        .FindFirstValue(JwtClaimTypes.PreferredUserName) ?? string.Empty;
}