using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services.DM;
using Open5ETools.Core.Common.Interfaces.Services.DM.Generator;
using Open5ETools.Core.Common.Interfaces.Services.EG;
using Open5ETools.Core.Common.Interfaces.Services.SM;

namespace Open5ETools.Core.Tests;
public class TestFixture : IDisposable
{
    public readonly IDungeonService DungeonService;
    public readonly IOptionService OptionService;
    public readonly IAppDbContext Context;
    public readonly IDungeonNoCorridor DungeonNoCorridor;
    public readonly IEncounterService EncounterService;
    public readonly ISpellService SpellService;
    private readonly TestEnvironment _env = new();
    private bool _disposedValue;

    public TestFixture()
    {
        DungeonService = _env.GetService<IDungeonService>();
        OptionService = _env.GetService<IOptionService>();
        EncounterService = _env.GetService<IEncounterService>();
        SpellService = _env.GetService<ISpellService>();
        Context = _env.GetService<IAppDbContext>();
        DungeonNoCorridor = _env.GetNcDungeon();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;
        if (disposing)
        {
            _env.Dispose();
        }

        _disposedValue = true;
    }

    ~TestFixture()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}