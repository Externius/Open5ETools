using Open5ETools.Core.Common.Interfaces.Services.DM;
using Shouldly;

namespace Open5ETools.Core.Tests.DungeonServiceTests;

public class Export(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IDungeonService _dungeonService = fixture.DungeonService;

    [Fact]
    public async Task ExportToJsonAsync_WithValidId_ReturnsDungeonJson()
    {
        var model = (await _dungeonService.GetAllDungeonOptionsAsync(TestContext.Current.CancellationToken)).First();
        var result =
            await _dungeonService.ExportToJsonAsync(model.Dungeons.First().Id, TestContext.Current.CancellationToken);
        result.ShouldNotBeEmpty();
    }
}