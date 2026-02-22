using System.ComponentModel.DataAnnotations;
using Open5ETools.Core.Common.Enums.DM;

namespace Open5ETools.Core.Domain.DM;

public class Option : BaseEntity
{
    public OptionKey Key { get; set; }
    [StringLength(short.MaxValue)] public required string Name { get; set; }
    [StringLength(short.MaxValue)] public required string Value { get; set; }
}