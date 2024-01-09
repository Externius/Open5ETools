using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Infrastructure.Data;
using Open5ETools.Web.Services;
using Serilog;
using System.Globalization;

namespace Open5ETools.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor()
            .AddScoped<ICurrentUserService, CurrentUserService>();

        services.Configure<RequestLocalizationOptions>(
            opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new("hu"),
                    new("en"),
                };
                opts.DefaultRequestCulture = new RequestCulture("en");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
                opts.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
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

        services.AddMemoryCache();

        services.AddMvc()
#if DEBUG
            .AddRazorRuntimeCompilation()
#endif
            ;

        services.AddHealthChecks();

        return services;
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