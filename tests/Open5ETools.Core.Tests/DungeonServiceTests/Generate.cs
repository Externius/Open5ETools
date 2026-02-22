using Open5ETools.Core.Common.Interfaces.Services.DM;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Infrastructure.Data;
using Shouldly;

namespace Open5ETools.Core.Tests.DungeonServiceTests;

public class Generate(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IDungeonService _dungeonService = fixture.DungeonService;

    [Fact]
    public async Task GenerateDungeonAsync_WithValidOptionModel_ReturnsDungeonModel()
    {
        var result = await _dungeonService.GenerateDungeonAsync(new DungeonOptionModel
        (
            "UT Dungeon",
            AppDbContextInitializer.TestAdminUserId,
            25,
            1,
            4,
            4,
            1,
            1,
            10,
            20,
            "any",
            20,
            true,
            false,
            0,
            [],
            1,
            [],
            string.Empty,
            DateTime.UtcNow,
            string.Empty,
            DateTime.UtcNow
        ), TestContext.Current.CancellationToken);

        result.DungeonTiles.ShouldNotBeNull();
    }
}