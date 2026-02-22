using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Domain;
using Open5ETools.Core.Domain.DM;
using Open5ETools.Core.Domain.EG;
using Open5ETools.Core.Domain.SM;
using Open5ETools.Infrastructure.Extensions;

namespace Open5ETools.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    protected readonly JsonSerializerOptions JsonSerializerOptions = new();

    public const string DbProvider = "DbProvider";
    public const string Open5ETools = "Open5ETools";
    public const string SqliteContext = "sqlite";
    public const string SqlServerContext = "sqlserver";

    public DbSet<DungeonOption> DungeonOptions { get; set; }
    public DbSet<Dungeon> Dungeons { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<Treasure> Treasures { get; set; }
    public DbSet<Spell> Spells { get; set; }

    public AppDbContext()
    {
    }

    protected AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DungeonOption>()
            .HasIndex(o => new { o.DungeonName, o.UserId })
            .IsUnique();

        modelBuilder.Entity<Option>()
            .HasIndex(o => new { o.Key, o.Name })
            .IsUnique();

        modelBuilder.Entity<Monster>(entity =>
        {
            entity.Property(e => e.JsonMonster)
                .HasColumnType("nvarchar(max)")
                .HasConversion(
                    monster => JsonSerializer.Serialize(monster, JsonSerializerOptions),
                    s => JsonSerializer.Deserialize<Open5ETools.Core.Common.Models.Json.Monster>(s,
                        JsonSerializerOptions)!);
        });

        modelBuilder.Entity<Treasure>().OwnsOne(
            treasure => treasure.TreasureDescription, ownedNavigationBuilder => { ownedNavigationBuilder.ToJson(); });
        modelBuilder.UseEnumStringConverter();
    }
}