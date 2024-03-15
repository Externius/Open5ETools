using Microsoft.EntityFrameworkCore;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services.SM;
using Shouldly;

namespace Open5ETools.Core.Tests.SpellServiceTests;

public class List(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly ISpellService _spellService = fixture.SpellService;
    private readonly IAppDbContext _context = fixture.Context;

    [Fact]
    public async Task ListAsync_WithoutSearchParameter_ReturnsAllSpells()
    {
        var expectedCount = _context.Spells.AsNoTracking().Count();
        var result = await _spellService.ListAsync();
        result.Count().ShouldBe(expectedCount);
    }

    [Theory]
    [InlineData("Wish", 1)]
    [InlineData("Healing", 3)]
    [InlineData("Light", 7)]
    [InlineData("Acid", 2)]
    public async Task ListAsync_WithSearchParameter_ReturnsFilteredSpells(string search, int expectedCount)
    {
        var result = await _spellService.ListAsync(search);
        result.Count().ShouldBe(expectedCount);
    }
}