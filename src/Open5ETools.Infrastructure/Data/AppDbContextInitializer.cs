using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Open5ETools.Core.Common.Configurations;
using Open5ETools.Core.Common.Enums;
using Open5ETools.Core.Common.Enums.DM;
using Open5ETools.Core.Common.Helpers;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services.DM;
using Open5ETools.Core.Common.Models.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Core.Common.Models.Json;
using Open5ETools.Core.Domain;
using Open5ETools.Core.Domain.DM;
using Open5ETools.Core.Helpers;
using Spell = Open5ETools.Core.Common.Models.Json.Spell;

namespace Open5ETools.Infrastructure.Data;

public class AppDbContextInitializer(
    IMapper mapper,
    IAppDbContext context,
    IDungeonService dungeonService,
    IOptions<AppConfigOptions> config)
{
    private readonly IMapper _mapper = mapper;
    private readonly IAppDbContext _context = context;
    private readonly IDungeonService _dungeonService = dungeonService;

    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        if (_context.Database.IsSqlServer() || _context.Database.IsSqlite())
        {
            await _context.Database.MigrateAsync(cancellationToken);
        }
        else
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }

    public async Task SeedDataAsync(CancellationToken cancellationToken)
    {
        if (!_context.Users.Any())
        {
            await SeedUsersAsync(cancellationToken);
            await SeedMonstersAsync(cancellationToken);
            await SeedTreasuresAsync(cancellationToken);
            await SeedOptionsAsync(cancellationToken);
            await SeedDungeonsAsync(cancellationToken, 1);
            await SeedSpellsAsync(cancellationToken);
        }
    }

    public async Task SeedTestBaseAsync(CancellationToken cancellationToken)
    {
        if (!_context.Users.Any())
        {
            await SeedUsersAsync(cancellationToken);
            await SeedMonstersAsync(cancellationToken);
            await SeedTreasuresAsync(cancellationToken);
            await SeedOptionsAsync(cancellationToken);
            await SeedDungeonsAsync(cancellationToken, 1);
            await SeedDungeonsAsync(cancellationToken, 2);
            await SeedSpellsAsync(cancellationToken);
        }
    }

    private async Task SeedSpellsAsync(CancellationToken cancellationToken)
    {
        var spells = JsonHelper.DeserializeJson<Spell>(JsonHelper.SpellFileName);
        var spellEntities = spells.Select(spell => _mapper.Map<Core.Domain.SM.Spell>(spell)).ToList();

        await _context.Spells.AddRangeAsync(spellEntities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedTreasuresAsync(CancellationToken cancellationToken)
    {
        var treasures = JsonHelper.DeserializeJson<TreasureDescription>(JsonHelper.TreasureFileName);
        var treasureEntities = treasures.Select(treasure => new Treasure { TreasureDescription = treasure }).ToList();

        await _context.Treasures.AddRangeAsync(treasureEntities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedMonstersAsync(CancellationToken cancellationToken)
    {
        var monsters = JsonHelper.DeserializeJson<Monster>(JsonHelper.MonsterFileName);
        var monsterEntities = monsters.Select(monster => new Core.Domain.EG.Monster { JsonMonster = monster }).ToList();

        await _context.Monsters.AddRangeAsync(monsterEntities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedOptionsAsync(CancellationToken token)
    {
        await SeedSizeAsync(token);
        await SeedDifficultyAsync(token);
        await SeedTreasureValueAsync(token);
        await SeedItemsRarityAsync(token);
        await SeedRoomDensityAsync(token);
        await SeedRoomSizeAsync(token);
        await SeedMonsterTypeAsync(token);
        await SeedTrapPercentAsync(token);
        await SeedRoamingPercentAsync(token);
        await SeedThemeAsync(token);
    }

    private async Task SeedThemeAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.Theme,
                Name = Resources.Dungeon.Dark,
                Value = "0"
            },
            new()
            {
                Key = OptionKey.Theme,
                Name = Resources.Dungeon.Light,
                Value = "1"
            },
            new()
            {
                Key = OptionKey.Theme,
                Name = Resources.Dungeon.Minimal,
                Value = "2"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedRoamingPercentAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.RoamingPercent,
                Name = Resources.Common.None,
                Value = "0"
            },
            new()
            {
                Key = OptionKey.RoamingPercent,
                Name = Resources.Common.Few,
                Value = "10"
            },
            new()
            {
                Key = OptionKey.RoamingPercent,
                Name = Resources.Common.More,
                Value = "20"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedTrapPercentAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.TrapPercent,
                Name = Resources.Common.None,
                Value = "0"
            },
            new()
            {
                Key = OptionKey.TrapPercent,
                Name = Resources.Common.Few,
                Value = "15"
            },
            new()
            {
                Key = OptionKey.TrapPercent,
                Name = Resources.Common.More,
                Value = "30"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedMonsterTypeAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Aberrations",
                Value = "aberration"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Beasts",
                Value = "beast"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Celestials",
                Value = "celestial"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Constructs",
                Value = "construct"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Dragons",
                Value = "dragon"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Elementals",
                Value = "elemental"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Fey",
                Value = "fey"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Fiends",
                Value = "fiend"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Giants",
                Value = "giant"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Humanoids",
                Value = "humanoid"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Monstrosities",
                Value = "monstrosity"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Oozes",
                Value = "ooze"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Plants",
                Value = "plant"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Swarm of tiny beasts",
                Value = "swarm of Tiny beasts"
            },
            new()
            {
                Key = OptionKey.MonsterType,
                Name = "Undead",
                Value = "undead"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedRoomSizeAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.RoomSize,
                Name = Resources.Common.Small,
                Value = "20"
            },
            new()
            {
                Key = OptionKey.RoomSize,
                Name = Resources.Common.Medium,
                Value = "35"
            },
            new()
            {
                Key = OptionKey.RoomSize,
                Name = Resources.Common.Large,
                Value = "45"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedRoomDensityAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.RoomDensity,
                Name = Resources.Common.Low,
                Value = "20"
            },
            new()
            {
                Key = OptionKey.RoomDensity,
                Name = Resources.Common.Medium,
                Value = "30"
            },
            new()
            {
                Key = OptionKey.RoomDensity,
                Name = Resources.Common.High,
                Value = "40"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedItemsRarityAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.ItemsRarity,
                Name = Resources.Common.CommonValue,
                Value = "0"
            },
            new()
            {
                Key = OptionKey.ItemsRarity,
                Name = Resources.Common.Uncommon,
                Value = "1"
            },
            new()
            {
                Key = OptionKey.ItemsRarity,
                Name = Resources.Common.Rare,
                Value = "2"
            },
            new()
            {
                Key = OptionKey.ItemsRarity,
                Name = Resources.Common.VeryRare,
                Value = "3"
            },
            new()
            {
                Key = OptionKey.ItemsRarity,
                Name = Resources.Common.Legendary,
                Value = "4"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedTreasureValueAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.TreasureValue,
                Name = Resources.Common.Low,
                Value = "0.5"
            },
            new()
            {
                Key = OptionKey.TreasureValue,
                Name = Resources.Common.Standard,
                Value = "1"
            },
            new()
            {
                Key = OptionKey.TreasureValue,
                Name = Resources.Common.High,
                Value = "1.5"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedDifficultyAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.Difficulty,
                Name = Resources.Common.Easy,
                Value = "0"
            },
            new()
            {
                Key = OptionKey.Difficulty,
                Name = Resources.Common.Medium,
                Value = "1"
            },
            new()
            {
                Key = OptionKey.Difficulty,
                Name = Resources.Common.Hard,
                Value = "2"
            },
            new()
            {
                Key = OptionKey.Difficulty,
                Name = Resources.Common.Deadly,
                Value = "3"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedSizeAsync(CancellationToken token)
    {
        var options = new List<Option>
        {
            new()
            {
                Key = OptionKey.Size,
                Name = Resources.Common.Small,
                Value = "20"
            },
            new()
            {
                Key = OptionKey.Size,
                Name = Resources.Common.Medium,
                Value = "32"
            },
            new()
            {
                Key = OptionKey.Size,
                Name = Resources.Common.Large,
                Value = "44"
            }
        };
        await _context.Options.AddRangeAsync(options, token);
        await _context.SaveChangesAsync(token);
    }

    private async Task SeedDungeonsAsync(CancellationToken token,
        int userId)
    {
        var dungeonOption = new DungeonOption
        {
            UserId = userId,
            DungeonName = "Test 1",
            Created = DateTime.UtcNow,
            ItemsRarity = 1,
            DeadEnd = true,
            DungeonDifficulty = 1,
            DungeonSize = 20,
            MonsterType = "any",
            PartyLevel = 4,
            PartySize = 4,
            TrapPercent = 20,
            RoamingPercent = 10,
            TreasureValue = 1,
            RoomDensity = 10,
            RoomSize = 20,
            Corridor = true
        };

        _context.DungeonOptions.Add(dungeonOption);
        await _context.SaveChangesAsync(token);

        var model = new DungeonOptionModel
        {
            DungeonName = dungeonOption.DungeonName,
            Created = dungeonOption.Created,
            ItemsRarity = dungeonOption.ItemsRarity,
            DeadEnd = dungeonOption.DeadEnd,
            DungeonDifficulty = dungeonOption.DungeonDifficulty,
            DungeonSize = dungeonOption.DungeonSize,
            MonsterType = dungeonOption.MonsterType,
            PartyLevel = dungeonOption.PartyLevel,
            PartySize = dungeonOption.PartySize,
            TrapPercent = dungeonOption.TrapPercent,
            RoamingPercent = dungeonOption.RoamingPercent,
            TreasureValue = dungeonOption.TreasureValue,
            RoomDensity = dungeonOption.RoomDensity,
            RoomSize = dungeonOption.RoomSize,
            Corridor = dungeonOption.Corridor,
            Id = dungeonOption.Id,
            UserId = dungeonOption.UserId
        };

        var sd = await _dungeonService.GenerateDungeonAsync(model);
        sd.Level = 1;
        await _dungeonService.AddDungeonAsync(sd, token);

        dungeonOption = new DungeonOption
        {
            UserId = userId,
            DungeonName = "Test 2",
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
            Corridor = false
        };
        _context.DungeonOptions.Add(dungeonOption);
        await _context.SaveChangesAsync(token);

        model = new DungeonOptionModel
        {
            DungeonName = dungeonOption.DungeonName,
            Created = dungeonOption.Created,
            ItemsRarity = dungeonOption.ItemsRarity,
            DeadEnd = dungeonOption.DeadEnd,
            DungeonDifficulty = dungeonOption.DungeonDifficulty,
            DungeonSize = dungeonOption.DungeonSize,
            MonsterType = dungeonOption.MonsterType,
            PartyLevel = dungeonOption.PartyLevel,
            PartySize = dungeonOption.PartySize,
            TrapPercent = dungeonOption.TrapPercent,
            RoamingPercent = dungeonOption.RoamingPercent,
            TreasureValue = dungeonOption.TreasureValue,
            RoomDensity = dungeonOption.RoomDensity,
            RoomSize = dungeonOption.RoomSize,
            Corridor = dungeonOption.Corridor,
            Id = dungeonOption.Id,
            UserId = dungeonOption.UserId
        };

        sd = await _dungeonService.GenerateDungeonAsync(model);
        sd.Level = 1;
        await _dungeonService.AddDungeonAsync(sd, token);
    }

    private async Task SeedUsersAsync(CancellationToken token)
    {
        _context.Users.Add(new User
        {
            Username = "TestAdmin",
            Password = PasswordHelper.EncryptPassword(config.Value.DefaultAdminPassword),
            FirstName = "Test",
            LastName = "Admin",
            Email = "admin@admin.com",
            Role = Role.Admin
        });

        _context.Users.Add(new User
        {
            Username = "TestUser",
            Password = PasswordHelper.EncryptPassword(config.Value.DefaultUserPassword),
            FirstName = "Test",
            LastName = "User",
            Email = "user@user.com",
            Role = Role.User
        });
        await _context.SaveChangesAsync(token);
    }
}