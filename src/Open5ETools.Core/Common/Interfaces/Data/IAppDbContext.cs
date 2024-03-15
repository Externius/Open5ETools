using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Open5ETools.Core.Domain;
using Open5ETools.Core.Domain.DM;
using Open5ETools.Core.Domain.EG;
using Open5ETools.Core.Domain.SM;

namespace Open5ETools.Core.Common.Interfaces.Data;

public interface IAppDbContext
{
    public DbSet<DungeonOption> DungeonOptions { get; }
    public DbSet<Dungeon> Dungeons { get; }
    public DbSet<User> Users { get; }
    public DbSet<Option> Options { get; }
    public DbSet<Monster> Monsters { get; }
    public DbSet<Treasure> Treasures { get; }
    public DbSet<Spell> Spells { get; }
    public DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}