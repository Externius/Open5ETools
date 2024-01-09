using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Open5ETools.Core.Common.Configurations;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Infrastructure.Data;
using Open5ETools.Infrastructure.Interceptors;
using System.Reflection;

namespace Open5ETools.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        switch (configuration.GetConnectionString(AppDbContext.DbProvider)?.ToLower())
        {
            case AppDbContext.SqlServerContext:
                services.AddDbContext<SqlServerContext>((sp, options) =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(AppDbContext.Open5ETools),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(SqlServerContext).GetTypeInfo().Assembly.GetName().Name);
                        })
                        .AddInterceptors(
                            ActivatorUtilities.CreateInstance<AuditEntitiesSaveChangesInterceptor>(sp));
                });

                services.AddScoped<IAppDbContext, SqlServerContext>(sp =>
                        sp.GetRequiredService<SqlServerContext>())
                    .AddScoped<AppDbContextInitializer>();
                break;
            case AppDbContext.SqliteContext:
                var home = Environment.GetEnvironmentVariable("HOME") + Path.DirectorySeparatorChar;
                var connString = configuration.GetConnectionString(AppDbContext.Open5ETools);
                services.AddDbContext<SqliteContext>((sp, options) =>
                {
                    options.UseSqlite(connString?.Replace(SqliteContext.HomeToken, home),
                        sqliteOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(SqliteContext).GetTypeInfo().Assembly.GetName().Name);
                        })
                        .AddInterceptors(
                            ActivatorUtilities.CreateInstance<AuditEntitiesSaveChangesInterceptor>(sp));
                });

                services.AddScoped<IAppDbContext, SqliteContext>(sp =>
                        sp.GetRequiredService<SqliteContext>())
                    .AddScoped<AppDbContextInitializer>();
                break;
            default:
                throw new ServiceException(
                    string.Format(Resources.Error.DbProviderError, configuration.GetConnectionString(AppDbContext.DbProvider)));
        }

        AddOptions(services, configuration);

        return services;
    }

    private static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AppConfigOptions>()
                .Bind(configuration.GetSection(AppConfigOptions.AppConfig))
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }

    public static IServiceCollection AddTestInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration,
        SqliteConnection connection)
    {
        services.AddDbContext<SqliteContext>((sp, options) =>
        {
            options.UseSqlite(connection,
                sqliteOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(SqliteContext).GetTypeInfo().Assembly.GetName().Name);
                })
                .AddInterceptors(ActivatorUtilities.CreateInstance<AuditEntitiesSaveChangesInterceptor>(sp));
        });
        services.AddScoped<IAppDbContext, SqliteContext>(sp =>
                sp.GetRequiredService<SqliteContext>())
            .AddScoped<AppDbContextInitializer>();

        AddOptions(services, configuration);

        return services;
    }
}