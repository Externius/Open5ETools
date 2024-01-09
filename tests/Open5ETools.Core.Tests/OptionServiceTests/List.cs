using Microsoft.EntityFrameworkCore;
using Open5ETools.Core.Common.Enums.DM;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.DM.Services;
using Shouldly;

namespace Open5ETools.Core.Tests.OptionServiceTests;

public class List(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IOptionService _optionService = fixture.OptionService;
    private readonly IAppDbContext _context = fixture.Context;

    [Fact]
    public async Task ListOptionsAsync_WithNoFilter_ReturnsAllOptions()
    {
        var expectedCount = _context.Options.AsNoTracking().Count();
        var result = await _optionService.ListOptionsAsync();
        result.Count().ShouldBe(expectedCount);
    }

    public static TheoryData<OptionKey> GetOptionKeys()
    {
        var result = new TheoryData<OptionKey>();
        foreach (OptionKey key in Enum.GetValues(typeof(OptionKey)))
        {
            result.Add(key);
        }
        return result;
    }

    [Theory]
    [MemberData(nameof(GetOptionKeys), MemberType = typeof(List))]
    public async Task ListOptionsAsync_WithFilter_ReturnsFilteredOptions(OptionKey filter)
    {
        var expectedCount = (await _context.Options.Where(o => o.Key == filter).ToListAsync()).Count;
        var result = await _optionService.ListOptionsAsync(filter);
        result.Count().ShouldBe(expectedCount);
    }
}