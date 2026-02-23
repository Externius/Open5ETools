using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Interfaces.Services.DM.Generator;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Infrastructure;
using Open5ETools.Infrastructure.Data;
using Open5ETools.Web.Services;

namespace Open5ETools.Core.Tests;

public sealed class TestEnvironment : IDisposable
{
    private readonly IServiceScope _scope;
    private SqliteConnection Connection { get; }
    private bool _disposedValue;

    public TestEnvironment()
    {
        var configurationBuilder = new ConfigurationBuilder();
        var configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        configurationBuilder.AddJsonFile(configFile);
        var config = configurationBuilder.Build();
        var connectionString = config.GetConnectionString(AppDbContext.Open5ETools);

        Connection = new SqliteConnection(connectionString);
        Connection.Open();

        var services = new ServiceCollection();
        ConfigureServices(services, config);

        _scope = services.BuildServiceProvider().CreateScope();
        InitDbAsync().Wait();
    }

    private async Task InitDbAsync()
    {
        var initializer = _scope.ServiceProvider
            .GetRequiredService<AppDbContextInitializer>();
        using var source = new CancellationTokenSource();
        var token = source.Token;
        await initializer.UpdateAsync(token);
        await initializer.SeedTestBaseAsync(token);
    }

    public T GetService<T>()
    {
        return _scope.ServiceProvider.GetService<T>() ?? throw new NotImplementedException();
    }

    public IDungeon GetDungeon(DungeonOptionModel? optionModel = null)
    {
        var dungeon = GetService<IDungeon>();
        optionModel ??= new DungeonOptionModel
        (
            "UT Dungeon",
            AppDbContextInitializer.TestAdminUserId,
            25,
            1,
            4,
            4,
            1,
            1,
            10,
            20,
            "any",
            20,
            true,
            false,
            0,
            [],
            1,
            [],
            string.Empty,
            DateTime.UtcNow,
            string.Empty,
            DateTime.UtcNow
        );

        dungeon.Init(optionModel);
        return dungeon;
    }

    public IDungeonNoCorridor GetNcDungeon(DungeonOptionModel? optionModel = null)
    {
        var dungeonNoCorridor = GetService<IDungeonNoCorridor>();
        optionModel ??= new DungeonOptionModel
        (
            "UT Dungeon",
            AppDbContextInitializer.TestAdminUserId,
            15,
            1,
            4,
            4,
            1,
            1,
            10,
            15,
            "any",
            20,
            true,
            false,
            0,
            [],
            1,
            [],
            string.Empty,
            DateTime.UtcNow,
            string.Empty,
            DateTime.UtcNow
        );

        dungeonNoCorridor.Init(optionModel);
        return dungeonNoCorridor;
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions()
            .AddTestInfrastructureServices(configuration, Connection)
            .AddApplicationServices()
            .AddHttpContextAccessor()
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddMemoryCache()
            .AddLogging();
    }

    ~TestEnvironment() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;

        if (disposing)
        {
            Connection.Dispose();
            _scope.Dispose();
        }

        _disposedValue = true;
    }
}