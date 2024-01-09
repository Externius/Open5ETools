using Microsoft.EntityFrameworkCore;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Domain;
using Open5ETools.Core.Domain.DM;
using Open5ETools.Core.Domain.EG;
using Open5ETools.Infrastructure.Extensions;

namespace Open5ETools.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
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

        modelBuilder.Entity<Monster>().OwnsOne(
                monster => monster.JsonMonster, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                    ownedNavigationBuilder.OwnsMany(m => m.SpecialAbilities);
                    ownedNavigationBuilder.OwnsMany(m => m.Actions);
                    ownedNavigationBuilder.OwnsMany(m => m.LegendaryActions);
                    ownedNavigationBuilder.OwnsMany(m => m.Reactions);
                });

        modelBuilder.Entity<Treasure>().OwnsOne(
                treasure => treasure.TreasureDescription, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                });
        modelBuilder.UseEnumStringConverter();
    }
}