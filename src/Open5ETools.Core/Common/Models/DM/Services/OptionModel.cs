using Open5ETools.Core.Common.Enums.DM;

namespace Open5ETools.Core.Common.Models.DM.Services;

public record OptionModel(
    OptionKey Key,
    string Name,
    string Value
);