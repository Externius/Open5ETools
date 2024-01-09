using Microsoft.EntityFrameworkCore;

namespace Open5ETools.Infrastructure.Data;
public class SqlServerContext : AppDbContext
{
    public SqlServerContext()
    {

    }
    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
    {

    }

#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();
#endif
}