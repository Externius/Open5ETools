﻿using Open5ETools.Core.Common.Interfaces.Services.DM;
using Shouldly;

namespace Open5ETools.Core.Tests.DungeonServiceTests;

public class List(TestFixture fixture) : IClassFixture<TestFixture>
{
    private const int UserId = 1;
    private readonly IDungeonService _dungeonService = fixture.DungeonService;

    [Fact]
    public async Task ListUserDungeonsAsync_WithValidUserIdWithDungeons_ReturnsDungeonModelList()
    {
        using var source = new CancellationTokenSource();
        var token = source.Token;
        const int expectedCount = 2;
        var result = await _dungeonService.ListUserDungeonsAsync(UserId, token);
        result.Count().ShouldBe(expectedCount);
    }

    [Fact]
    public async Task ListUserDungeonsByNameAsync_WithValidNameAndUserId_ReturnsDungeonModelList()
    {
        using var source = new CancellationTokenSource();
        var token = source.Token;
        const int expectedCount = 1;
        var dungeonName = (await _dungeonService.GetAllDungeonOptionsAsync(token)).First().DungeonName;
        var result = await _dungeonService.ListUserDungeonsByNameAsync(dungeonName, UserId, token);
        result.Count().ShouldBe(expectedCount);
    }
}