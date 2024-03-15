using Microsoft.Extensions.DependencyInjection;
using Open5ETools.Core.Common.Helpers;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Interfaces.Services.DM;
using Open5ETools.Core.Common.Interfaces.Services.DM.Generator;
using Open5ETools.Core.Common.Interfaces.Services.EG;
using Open5ETools.Core.Common.Interfaces.Services.SM;
using Open5ETools.Core.Services;
using Open5ETools.Core.Services.DM;
using Open5ETools.Core.Services.DM.Generator;
using Open5ETools.Core.Services.EG;
using Open5ETools.Core.Services.SM;

namespace Open5ETools.Core;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services
    )
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IEncounterService, EncounterService>()
            .AddScoped<IDungeonHelper, DungeonHelper>()
            .AddScoped<IOptionService, OptionService>()
            .AddScoped<IDungeon, Dungeon>()
            .AddScoped<IDungeonNoCorridor, DungeonNoCorridor>()
            .AddScoped<IDungeonService, DungeonService>()
            .AddScoped<ISpellService, SpellService>();

        services.AddAutoMapper(cfg =>
            {
                cfg.AllowNullCollections = true;
            }
            , AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}