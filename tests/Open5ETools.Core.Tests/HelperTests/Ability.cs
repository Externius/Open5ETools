using Open5ETools.Core.Common.Helpers;
using Shouldly;

namespace Open5ETools.Core.Tests.HelperTests;

public class Ability
{
    [Theory]
    [InlineData(21, "+5")]
    [InlineData(19, "+4")]
    [InlineData(17, "+3")]
    [InlineData(14, "+2")]
    [InlineData(13, "+1")]
    [InlineData(10, "0")]
    [InlineData(9, "-1")]
    [InlineData(6, "-2")]
    [InlineData(5, "-3")]
    [InlineData(3, "-4")]
    public void CalcMod_ReturnsCorrectMod(int abilityScore, string expectedMod)
    {
        var mod = AbilityHelper.CalcMod(abilityScore);
        mod.ShouldBeEquivalentTo(expectedMod);
    }
}