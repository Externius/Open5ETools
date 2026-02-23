using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Core.Domain.DM;

namespace Open5ETools.Core.Common.Mappers;

public static class OptionMapper
{
    public static OptionModel ToModel(this Option option)
    {
        return new OptionModel
        (
            option.Key,
            option.Name,
            option.Value
        );
    }
}