using Open5ETools.Core.Common.Enums.DM;
using Open5ETools.Core.Common.Models.DM.Services;

namespace Open5ETools.Core.Common.Interfaces.Services.DM;

public interface IOptionService
{
    Task<OptionModel[]> ListOptionsAsync(OptionKey? filter = null, CancellationToken cancellationToken = default);
}