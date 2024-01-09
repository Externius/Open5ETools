using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Common.Interfaces.Services;

public interface IAuthService
{
    Task<UserModel?> LoginAsync(UserModel model, CancellationToken cancellationToken);
}