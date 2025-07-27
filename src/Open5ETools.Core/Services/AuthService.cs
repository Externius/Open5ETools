using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Open5ETools.Core.Common.Helpers;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Services;

public class AuthService(IMapper mapper, IAppDbContext context, ILogger<AuthService> logger) : IAuthService
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<UserModel?> LoginAsync(UserModel model, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Username == model.Username, cancellationToken);

            if (user is null)
                return null;

            return PasswordHelper.CheckPassword(user.Password, model.Password) ? _mapper.Map<UserModel>(user) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login User failed.");
            throw;
        }
    }
}