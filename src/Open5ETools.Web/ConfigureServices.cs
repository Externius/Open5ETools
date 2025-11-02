using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Infrastructure.Data;
using Open5ETools.Web.Services;
using Serilog;
using System.Globalization;
using Mapster;
using Open5ETools.Core.Common.Models.DM.Services;
using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Web.Models.Dungeon;
using Open5ETools.Web.Models.Encounter;

namespace Open5ETools.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor()
            .AddScoped<ICurrentUserService, CurrentUserService>();

        services.Configure<RequestLocalizationOptions>(opts =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new("hu"),
                new("en")
            };
            opts.DefaultRequestCulture = new RequestCulture("en");
            opts.SupportedCultures = supportedCultures;
            opts.SupportedUICultures = supportedCultures;
            opts.RequestCultureProviders =
            [
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
            ];
        });

        services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
                options.AccessDeniedPath = new PathString("/Auth/Forbidden/");
            });

        services.ConfigureMapster()
            .AddMemoryCache();

        services.AddMvc()
#if DEBUG
            .AddRazorRuntimeCompilation()
#endif
            ;
        services.AddWebOptimizer(pipeline =>
        {
            pipeline.AddJavaScriptBundle("js/site.min.js", "js/site.js", "js/password.js");
            pipeline.AddCssBundle("css/site.min.css", "css/site.css");
        });
        services.AddHealthChecks();

        return services;
    }

    private static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        services.AddMapster();

        TypeAdapterConfig<EncounterOptionViewModel, EncounterOption>
            .NewConfig()
            .Map(dest => dest.Sizes, src => src.SelectedSizes)
            .Map(dest => dest.MonsterTypes, src => src.SelectedMonsterTypes);

        TypeAdapterConfig<DungeonOptionCreateViewModel, DungeonOptionModel>
            .NewConfig()
            .Map(dest => dest.TreasureValue, src => Convert.ToDouble(src.TreasureValue, CultureInfo.InvariantCulture))
            .Ignore(dest => dest.MonsterType);

        TypeAdapterConfig<DungeonOptionModel, DungeonOptionCreateViewModel>
            .NewConfig()
            .Map(dest => dest.TreasureValue, src => src.TreasureValue.ToString(CultureInfo.InvariantCulture))
            .Map(dest => dest.MonsterType, src => GetMonsters(src));

        return services;
    }

    private static string[] GetMonsters(DungeonOptionModel model)
    {
        return model.MonsterType.Split(',');
    }

    public static IHostBuilder AddSerilog(this IHostBuilder host,
        IConfiguration configuration)
    {
        switch (configuration.GetConnectionString(AppDbContext.DbProvider)?.ToLower())
        {
            case AppDbContext.SqlServerContext:
                host.UseSerilog((ctx, lc) => lc
                    .ReadFrom.Configuration(ctx.Configuration));
                break;
            case AppDbContext.SqliteContext:
                host.UseSerilog(new LoggerConfiguration()
                    .WriteTo.File($"Logs{Path.DirectorySeparatorChar}log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger());
                break;
            default:
                throw new ServiceException(
                    string.Format(Resources.Error.DbProviderError,
                        configuration.GetConnectionString(AppDbContext.DbProvider)));
        }

        return host;
    }
}