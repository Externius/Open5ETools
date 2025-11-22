using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Open5ETools.Core.Common.Enums.SM;
using Open5ETools.Core.Common.Helpers;
using Open5ETools.Core.Common.Interfaces.Services;
using Open5ETools.Core.Common.Interfaces.Services.DM;
using Open5ETools.Core.Common.Interfaces.Services.DM.Generator;
using Open5ETools.Core.Common.Interfaces.Services.EG;
using Open5ETools.Core.Common.Interfaces.Services.SM;
using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Core.Common.Models.Json;
using Open5ETools.Core.Services;
using Open5ETools.Core.Services.DM;
using Open5ETools.Core.Services.DM.Generator;
using Open5ETools.Core.Services.EG;
using Open5ETools.Core.Services.SM;

namespace Open5ETools.Core;

public static class ConfigureServices
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
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

            services.ConfigureMapster();
            return services;
        }

        private IServiceCollection ConfigureMapster()
        {
            services.AddMapster();
            TypeAdapterConfig<Spell, Open5ETools.Core.Domain.SM.Spell>
                .NewConfig()
                .Map(dest => dest.Concentration,
                    src => string.IsNullOrWhiteSpace(src.Concentration) ||
                           !src.Concentration.Equals("no", StringComparison.InvariantCultureIgnoreCase))
                .Map(dest => dest.Ritual,
                    src => string.IsNullOrWhiteSpace(src.Ritual) ||
                           !src.Ritual.Equals("no", StringComparison.InvariantCultureIgnoreCase))
                .Map(dest => dest.School,
                    src => Enum.Parse<School>(src.School ?? string.Empty));

            TypeAdapterConfig<Monster, JsonMonsterModel>
                .NewConfig()
                .Map(dest => dest.Hp, src => src.HitPoints)
                .Map(dest => dest.Ac, src => src.ArmorClass);

            TypeAdapterConfig<Open5ETools.Core.Domain.EG.Monster, MonsterModel>
                .NewConfig()
                .Map(dest => dest.JsonMonsterModel, src => src.JsonMonster);

            return services;
        }
    }
}