using Microsoft.EntityFrameworkCore;
using Open5ETools.Core.Domain;
using Open5ETools.Core.Domain.DM;
using Open5ETools.Core.Domain.EG;
using Open5ETools.Core.Domain.SM;

namespace Open5ETools.Infrastructure.Data;
public class SqliteContext : AppDbContext
{
    private const string CurrentTimestamp = "CURRENT_TIMESTAMP";
    public const string HomeToken = "%HOME%";
    public SqliteContext()
    {

    }
    public SqliteContext(DbContextOptions<SqliteContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dungeon>().Metadata.UseSqlReturningClause(false);
        modelBuilder.Entity<Dungeon>()
            .Property(p => p.Timestamp)
            .IsRowVersion()
            .HasDefaultValueSql(CurrentTimestamp);
        modelBuilder.Entity<DungeonOption>().Metadata.UseSqlReturningClause(false);
        modelBuilder.Entity<DungeonOption>()
            .Property(p => p.Timestamp)
            .IsRowVersion()
            .HasDefaultValueSql(CurrentTimestamp);
        modelBuilder.Entity<Option>().Metadata.UseSqlReturningClause(false);
        modelBuilder.Entity<Option>()
            .Property(p => p.Timestamp)
            .IsRowVersion()
            .HasDefaultValueSql(CurrentTimestamp);
        modelBuilder.Entity<User>().Metadata.UseSqlReturningClause(false);
        modelBuilder.Entity<User>()
            .Property(p => p.Timestamp)
            .IsRowVersion()
            .HasDefaultValueSql(CurrentTimestamp);

        modelBuilder.Entity<Monster>().Metadata.UseSqlReturningClause(false);
        modelBuilder.Entity<Monster>()
             .Property(p => p.Timestamp)
            .IsRowVersion()
            .HasDefaultValueSql(CurrentTimestamp);
        modelBuilder.Entity<Monster>()
            .OwnsOne(
            monster => monster.JsonMonster, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsMany(m => m.SpecialAbilities);
                ownedNavigationBuilder.OwnsMany(m => m.Actions);
                ownedNavigationBuilder.OwnsMany(m => m.LegendaryActions);
                ownedNavigationBuilder.OwnsMany(m => m.Reactions);
            });

        modelBuilder.Entity<Treasure>().Metadata.UseSqlReturningClause(false);
        modelBuilder.Entity<Treasure>()
             .Property(p => p.Timestamp)
            .IsRowVersion()
            .HasDefaultValueSql(CurrentTimestamp);
        modelBuilder.Entity<Treasure>().OwnsOne(
            treasure => treasure.TreasureDescription, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

        modelBuilder.Entity<Spell>().Metadata.UseSqlReturningClause(false);
        modelBuilder.Entity<Spell>()
            .Property(p => p.Timestamp)
            .IsRowVersion()
            .HasDefaultValueSql(CurrentTimestamp);            
    }

#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite();
#endif
}