using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Open5ETools.Core.Common;
using Open5ETools.Core.Common.Enums.EG;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Extensions;
using Open5ETools.Core.Common.Helpers;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services.EG;
using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Core.Domain.EG;
using Open5ETools.Resources;
using Enum = System.Enum;

namespace Open5ETools.Core.Services.EG;

public class EncounterService(
    IMapper mapper,
    IAppDbContext context,
    ILogger<EncounterService> logger
) : IEncounterService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAppDbContext _context = context;
    private readonly ILogger _logger = logger;
    private List<Monster> _monsters = [];
    private int _partyLevel;
    private int _partySize;
    private ICollection<KeyValuePair<Difficulty, int>> _xpList = [];

    public async Task<ICollection<KeyValuePair<string, int>>> GetEnumListAsync<T>() where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new InvalidOperationException("Type parameter must be Enum.");

        return await Task.Run(() =>
            (from object e in Enum.GetValues(typeof(T))
                select new KeyValuePair<string, int>(((Enum)e)
                    .GetName(Resources.Enum.ResourceManager), (int)e))
            .ToList());
    }

    public async Task<EncounterModel> GenerateAsync(EncounterOption option)
    {
        ValidateOption(option);
        _partyLevel = option.PartyLevel;
        _partySize = option.PartySize;
        _xpList =
        [
            new KeyValuePair<Difficulty, int>(Difficulty.Easy, Constants.Thresholds[_partyLevel, 0] * _partySize),
            new KeyValuePair<Difficulty, int>(Difficulty.Medium, Constants.Thresholds[_partyLevel, 1] * _partySize),
            new KeyValuePair<Difficulty, int>(Difficulty.Hard, Constants.Thresholds[_partyLevel, 2] * _partySize),
            new KeyValuePair<Difficulty, int>(Difficulty.Deadly, Constants.Thresholds[_partyLevel, 3] * _partySize)
        ];
        try
        {
            _monsters = [.. _context.Monsters.AsNoTracking()];
            var sumXp = CalcSumXp((int)Difficulty.Deadly);
            var monsterXps = new SortedSet<int>();

            if (option.MonsterTypes.Any())
            {
                var selectedMonsters = option.MonsterTypes.Select(m => m.ToString().ToLower());
                _monsters = [.. _monsters.Where(m => selectedMonsters.Any(m.JsonMonster.Type.ToLower().Equals))];
            }

            if (option.Sizes.Any())
            {
                var selectedSizes = option.Sizes.Select(s => s.ToString().ToLower());
                _monsters = [.. _monsters.Where(m => selectedSizes.Any(m.JsonMonster.Size.ToLower().Equals))];
            }

            if (option.Difficulty.HasValue)
                sumXp = Constants.Thresholds[option.PartyLevel, (int)option.Difficulty.Value] * option.PartySize;

            foreach (var monster in _monsters)
            {
                monsterXps.Add(
                    Constants.ChallengeRatingXp[
                        Constants.ChallengeRating.IndexOf(monster.JsonMonster.ChallengeRating)]);
            }

            CheckPossible(sumXp, monsterXps);

            var result = new EncounterModel();
            var maxTryNumber = 5000;
            await Task.Run(() =>
            {
                while (result.Monsters.Count < option.Count && maxTryNumber > 0)
                {
                    try
                    {
                        var monster = GetMonster(option.Difficulty);
                        if (monster is not null)
                            result.Monsters.Add(monster);
                        maxTryNumber--;
                    }
                    catch (ServiceException)
                    {
                        maxTryNumber--;
                    }
                }
            });

            result.SumXp = result.Monsters.Sum(mm => mm.JsonMonsterModel.Xp);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Generate encounter failed.");
            throw new ServiceException(ex.Message, ex);
        }
    }

    private int CalcSumXp(int difficulty)
    {
        return Constants.Thresholds[_partyLevel, difficulty] * _partySize;
    }

    private static void ValidateOption(EncounterOption? option)
    {
        var exceptions = new List<ServiceException>();

        if (option is null)
            exceptions.Add(new ServiceException(ServiceException.EntityNotFoundException));
        if (option?.PartyLevel == 0)
            exceptions.Add(new ServiceException(ServiceException.RequiredValidation, "partyLevel"));
        if (option?.PartySize == 0)
            exceptions.Add(new ServiceException(ServiceException.RequiredValidation, "partySize"));

        if (exceptions.Count != 0)
            throw new ServiceAggregateException(exceptions);
    }

    private static void CheckPossible(int sumXp, IReadOnlyCollection<int> monsterXps)
    {
        if (monsterXps.Count != 0 && sumXp > monsterXps.First())
            return;
        throw new ServiceException(ServiceException.EncounterNotPossible);
    }

    private MonsterModel? GetMonster(Difficulty? difficulty = null)
    {
        var monsterCount = _monsters.Count;
        var monster = 0;
        var indexes = new List<int>(Enumerable.Range(0, Constants.Multipliers.GetLength(0)));

        while (monster < monsterCount)
        {
            var currentMonster = _monsters.ElementAt(DungeonHelper.GetRandomInt(0, _monsters.Count));
            _monsters.Remove(currentMonster);
            var monsterXp = DungeonHelper.GetMonsterXp(currentMonster.JsonMonster);
            indexes.Shuffle();

            if (difficulty.HasValue)
            {
                var difficultyXp = _xpList.First(l => l.Key == difficulty.Value).Value;
                foreach (var i in indexes)
                {
                    var count = (int)Constants.Multipliers[i, 0];
                    var allXp = monsterXp * count * Constants.Multipliers[i, 1];
                    if (allXp < difficultyXp ||
                        _xpList.OrderByDescending(l => l.Value).First(l => allXp >= l.Value).Key != difficulty)
                        continue;

                    return GetEncounterDetail(difficulty.Value, currentMonster, (int)allXp, count);
                }
            }
            else
            {
                foreach (var i in indexes)
                {
                    var count = (int)Constants.Multipliers[i, 0];
                    var allXp = monsterXp * count * Constants.Multipliers[i, 1];
                    var difficulties = _xpList.Where(xp => allXp <= xp.Value).Select(xp => xp.Key).AsQueryable();
                    if (difficulties.Any())
                        return GetEncounterDetail(difficulties.First(), currentMonster, (int)allXp, count);
                }
            }

            monster++;
        }

        return null;
    }

    private MonsterModel GetEncounterDetail(Difficulty difficulty, Monster currentMonster, int allXp, int count)
    {
        var monsterModel = _mapper.Map<MonsterModel>(currentMonster);
        monsterModel.JsonMonsterModel.Xp = allXp;
        monsterModel.JsonMonsterModel.Count = count;
        monsterModel.JsonMonsterModel.Difficulty = difficulty.GetName(Resources.Enum.ResourceManager);
        monsterModel.JsonMonsterModel.Size =
            Enum.Parse<Size>(currentMonster.JsonMonster.Size).GetName(Resources.Enum.ResourceManager);

        if (Enum.TryParse(monsterModel.JsonMonsterModel.Type, out MonsterType type))
            monsterModel.JsonMonsterModel.Type = type.GetName(Resources.Enum.ResourceManager);

        return monsterModel;
    }

    public async Task<MonsterModel> GetMonsterByIdAsync(int id)
    {
        var monster = await _context.Monsters
                          .AsNoTracking()
                          .FirstOrDefaultAsync(m => m.Id == id) ??
                      throw new ServiceException(Error.NotFound);

        return _mapper.Map<MonsterModel>(monster);
    }
}