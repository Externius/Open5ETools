using Open5ETools.Core.Common.Enums.DM;

namespace Open5ETools.Core.Common.Models.DM.Services;

public class OptionModel
{
    public OptionKey Key { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}