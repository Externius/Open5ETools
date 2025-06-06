﻿using Open5ETools.Core.Common;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Services.DM;
using Open5ETools.Core.Common.Models.DM.Services;
using Shouldly;

namespace Open5ETools.Core.Tests.DungeonServiceTests;

public class Create(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IDungeonService _dungeonService = fixture.DungeonService;

    [Fact]
    public async Task CreateDungeonOptionAsync_WithOptionModel_ReturnsNewEntityId()
    {
        var optionsModel = new DungeonOptionModel
        {
            DungeonName = "UT Dungeon 1",
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
        };
        var result = await _dungeonService.CreateDungeonOptionAsync(optionsModel, TestContext.Current.CancellationToken);
        result.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task AddDungeonAsync_WithDungeonModel_ReturnsNewEntityId()
    {
        var optionsModel =
            (await _dungeonService.GetAllDungeonOptionsForUserAsync(1, TestContext.Current.CancellationToken)).First();
        var dungeon = await _dungeonService.GenerateDungeonAsync(optionsModel);

        var result = await _dungeonService.AddDungeonAsync(dungeon, TestContext.Current.CancellationToken);
        result.ShouldBeGreaterThan(1);
    }

    [Fact]
    public async Task CreateOrUpdateDungeonAsync_WithInvalidModel_ReturnsServiceAggregateException()
    {
        var optionsModel = new DungeonOptionModel
        {
            DungeonName = string.Empty,
            UserId = 0
        };
        var expectedErrors = new List<string>
        {
            string.Format(Resources.Error.RequiredValidation, nameof(DungeonOptionModel.DungeonName)),
            string.Format(Resources.Error.RequiredValidation, nameof(DungeonOptionModel.UserId))
        };

        var act = async () =>
        {
            await _dungeonService.CreateOrUpdateDungeonAsync(optionsModel, false, 1, TestContext.Current.CancellationToken);
        };

        var result = await act.ShouldThrowAsync<ServiceAggregateException>();
        result.GetInnerExceptions()
            .Select(se => se.Message)
            .OrderBy(s => s)
            .SequenceEqual(expectedErrors.OrderBy(s => s))
            .ShouldBeTrue();
    }

    [Fact]
    public async Task CreateOrUpdateDungeonAsync_WithValidNewModel_CreateOptionAndReturnsDungeonModel()
    {
        var optionsModel = new DungeonOptionModel
        {
            DungeonName = "UT Dungeon 2",
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
        };
        var result =
            await _dungeonService.CreateOrUpdateDungeonAsync(optionsModel, false, 1, TestContext.Current.CancellationToken);
        result.ShouldNotBeNull();
        result.RoamingMonsterDescription.ShouldBe(Constants.Empty);
    }

    [Fact]
    public async Task
        CreateOrUpdateDungeonAsync_WithValidExistingModel_AddsDungeonToExistingOptionAndReturnsDungeonModel()
    {
        const int userId = 1;
        const int levelNumber = 2;
        var existingDungeonOption =
            (await _dungeonService.GetAllDungeonOptionsForUserAsync(userId, TestContext.Current.CancellationToken)).First();
        var currentDungeonCount =
            (await _dungeonService.ListUserDungeonsByNameAsync(existingDungeonOption.DungeonName, userId,
                TestContext.Current.CancellationToken)).Count();
        var result = await _dungeonService.CreateOrUpdateDungeonAsync(existingDungeonOption, true, levelNumber,
            TestContext.Current.CancellationToken);
        var newDungeonCount =
            (await _dungeonService.ListUserDungeonsByNameAsync(existingDungeonOption.DungeonName, userId,
                TestContext.Current.CancellationToken)).Count();
        result.ShouldNotBeNull();
        result.Level.ShouldBe(levelNumber);
        newDungeonCount.ShouldBeGreaterThan(currentDungeonCount);
    }
}