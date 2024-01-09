using Open5ETools.Core.Common.Models.Services;

namespace Open5ETools.Core.Common.Interfaces.Services;

public interface IUserService
{
    Task<UserModel> GetAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(UserModel model, CancellationToken cancellationToken);
    Task<int> CreateAsync(UserModel model, CancellationToken cancellationToken);
    Task<IEnumerable<UserModel>> ListAsync(bool? deleted = false, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> RestoreAsync(int id, CancellationToken cancellationToken);
    Task ChangePasswordAsync(ChangePasswordModel model, CancellationToken cancellationToken);
}