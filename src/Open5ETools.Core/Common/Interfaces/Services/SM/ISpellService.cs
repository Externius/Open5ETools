using Open5ETools.Core.Common.Models.SM;

namespace Open5ETools.Core.Common.Interfaces.Services.SM;

public interface ISpellService
{
    Task<SpellModel> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<SpellModel[]> ListAsync(string? search = null, CancellationToken cancellationToken = default);
}
