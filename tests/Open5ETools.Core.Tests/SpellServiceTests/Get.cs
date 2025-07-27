using Open5ETools.Core.Common.Interfaces.Services.SM;
using Shouldly;

namespace Open5ETools.Core.Tests.SpellServiceTests;

public class Get(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly ISpellService _spellService = fixture.SpellService;

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(132)]
    [InlineData(256)]
    public async Task GetAsync_WithValidId_ReturnsSpell(int id)
    {
        var result = await _spellService.GetAsync(id, TestContext.Current.CancellationToken);
        result.ShouldNotBeNull();
        result.Id.ShouldBe(id);
    }
}