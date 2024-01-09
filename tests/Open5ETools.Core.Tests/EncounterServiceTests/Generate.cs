using Open5ETools.Core.Common.Enums.EG;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Extensions;
using Open5ETools.Core.Common.Interfaces.EG;
using Open5ETools.Core.Common.Models.EG;
using Shouldly;

namespace Open5ETools.Core.Tests.EncounterServiceTests;

public class Generate(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly IEncounterService _encounterService = fixture.EncounterService;

    [Theory]
    [MemberData(nameof(EncounterOptionData.Data), MemberType = typeof(EncounterOptionData))]
    public async Task GenerateAsync_WithDifferentOptions_CanGenerateEncounters(EncounterOption option)
    {
        var result = await _encounterService.GenerateAsync(option);
        result.ShouldNotBeNull();
        result.Monsters.Count.ShouldBe(option.Count);
    }

    [Theory]
    [MemberData(nameof(EncounterOptionData.Data), MemberType = typeof(EncounterOptionData))]
    public async Task GenerateAsync_WithDifferentOptions_CanFilterEncounters(EncounterOption option)
    {
        var encounterModel = await _encounterService.GenerateAsync(option);
        encounterModel.ShouldNotBeNull();
        encounterModel.Monsters.ShouldNotBeNull();
        var resultMonsterTypes = encounterModel.Monsters.Select(mm => mm.JsonMonsterModel.Type.ToLower()).Distinct();
        var resultMonsterSizes = encounterModel.Monsters.Select(mm => mm.JsonMonsterModel.Size.ToLower()).Distinct();
        var optionMonsterTypes = option.MonsterTypes.Select(m => m.GetName(Resources.Enum.ResourceManager).ToLower());
        var optionMonsterSizes = option.Sizes.Select(m => m.GetName(Resources.Enum.ResourceManager).ToLower());
        resultMonsterTypes.Except(optionMonsterTypes).Count().ShouldBe(0);
        resultMonsterSizes.Except(optionMonsterSizes).Count().ShouldBe(0);
    }

    [Theory]
    [MemberData(nameof(EncounterOptionData.FilterWithDifficultyData), MemberType = typeof(EncounterOptionData))]
    public async Task CanFilterWithDifficulty(Difficulty difficulty, int partyLevel, int partySize)
    {
        var option = new EncounterOption
        {
            PartyLevel = partyLevel,
            MonsterTypes = new List<MonsterType> { MonsterType.Beast, MonsterType.Humanoid, MonsterType.SwarmOfTinyBeasts },
            PartySize = partySize,
            Difficulty = difficulty
        };
        var encounterModel = await _encounterService.GenerateAsync(option);
        encounterModel.ShouldNotBeNull();
        encounterModel.Monsters.ShouldNotBeNull();
        encounterModel.Monsters.TrueForAll(e => e.JsonMonsterModel.Difficulty.Equals(difficulty.ToString())).ShouldBeTrue();
    }

    [Fact]
    public async Task CanThrowException()
    {
        var option = new EncounterOption
        {
            PartyLevel = 1,
            PartySize = 1,
            MonsterTypes = new List<MonsterType> { MonsterType.Dragon },
            Difficulty = Difficulty.Easy
        };
        await Should.ThrowAsync<ServiceException>(async () =>
        {
            await _encounterService.GenerateAsync(option);
        });
    }
}