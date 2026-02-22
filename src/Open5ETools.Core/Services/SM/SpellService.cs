using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.Services.SM;
using Open5ETools.Core.Common.Mappers;
using Open5ETools.Core.Common.Models.SM;
using Open5ETools.Resources;

namespace Open5ETools.Core.Services.SM;

public class SpellService(
    IAppDbContext context,
    ILogger<SpellService> logger
) : ISpellService
{
    private readonly IAppDbContext _context = context;
    private readonly ILogger _logger = logger;

    public async Task<SpellModel> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var spell = await _context.Spells
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken) ??
                        throw new ServiceException(Error.NotFound);

            return spell.ToModel();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Name} failed.", nameof(GetAsync));
            throw;
        }
    }

    public async Task<SpellModel[]> ListAsync(string? search = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _context.Spells.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(s => EF.Functions.Like(s.Name, $"%{search}%")
                                         || EF.Functions.Like(s.Class, $"%{search}%")
                                         || EF.Functions.Like(s.Level, $"%{search}%")
                                         || EF.Functions.Like(s.CastingTime, $"%{search}%")
                                         || EF.Functions.Like(s.Range, $"%{search}%")
                                         || EF.Functions.Like(s.Components, $"%{search}%")
                );

            var items = await query.ToArrayAsync(cancellationToken);
            return [.. items.Select(s => s.ToModel())];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Name} failed.", nameof(ListAsync));
            throw;
        }
    }
}