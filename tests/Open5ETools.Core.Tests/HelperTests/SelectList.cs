using Open5ETools.Core.Common.Helpers;
using Shouldly;

namespace Open5ETools.Core.Tests.HelperTests;

public class SelectList
{
    [Theory]
    [InlineData(1, 12, 12)]
    [InlineData(10, 20, 11)]
    [InlineData(132, 242, 111)]
    [InlineData(0, 256, 257)]
    public void GenerateIntSelectList_ReturnsCorrectAmount(int from, int to, int expectedCount)
    {
        var count = SelectListHelper.GenerateIntSelectList(from, to).Length;
        count.ShouldBe(expectedCount);
    }
}