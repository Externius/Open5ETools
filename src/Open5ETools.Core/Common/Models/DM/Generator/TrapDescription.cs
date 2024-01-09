namespace Open5ETools.Core.Common.Models.DM.Generator;

public class TrapDescription(string name, string description)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
}