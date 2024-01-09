using Open5ETools.Core.Common.Interfaces.DM.Services;
using Open5ETools.Core.Common.Models.DM.Services;
using Shouldly;

namespace Open5ETools.Core.Tests.DungeonServiceTests;

public class Generate(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IDungeonService _dungeonService = fixture.DungeonService;

    [Fact]
    public async Task GenerateDungeonAsync_WithValidOptionModel_ReturnsDungeonModel()
    {
        var result = await _dungeonService.GenerateDungeonAsync(new DungeonOptionModel
        {
            DungeonName = "UT Dungeon",
            Created = DateTime.UtcNow,
            ItemsRarity = 1,
            DeadEnd = true,
            DungeonDifficulty = 1,
            DungeonSize = 25,
            MonsterType = "any",
            PartyLevel = 4,
            PartySize = 4,
            TrapPercent = 20,
            RoamingPercent = 0,
            TreasureValue = 1,
            RoomDensity = 10,
            RoomSize = 20,
            Corridor = false,
            UserId = 1
        });

        result.DungeonTiles.ShouldNotBeNull();
    }
}