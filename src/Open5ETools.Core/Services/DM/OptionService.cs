using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Open5ETools.Core.Common.Enums.DM;
using Open5ETools.Core.Common.Interfaces.Data;
using Open5ETools.Core.Common.Interfaces.DM.Services;
using Open5ETools.Core.Common.Models.DM.Services;

namespace Open5ETools.Core.Services.DM;

public class OptionService(IMapper mapper,
    IAppDbContext context,
    IMemoryCache memoryCache,
    ILogger<OptionService> logger) : IOptionService
{
    private readonly IAppDbContext _context = context;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IEnumerable<OptionModel>> ListOptionsAsync(OptionKey? filter = null, CancellationToken cancellationToken = default)
    {
        try
        {
            if (_memoryCache.TryGetValue(nameof(ListOptionsAsync), out List<OptionModel>? cacheEntry))
                return filter.HasValue ? cacheEntry?.Where(o => o.Key == filter.Value) ?? [] : cacheEntry ?? [];

            var options = await _context.Options
                                            .AsNoTracking()
                                            .ToListAsync(cancellationToken);

            cacheEntry = options.Select(_mapper.Map<OptionModel>).ToList();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            _memoryCache.Set(nameof(ListOptionsAsync), cacheEntry, cacheEntryOptions);

            return filter.HasValue ? cacheEntry.Where(o => o.Key == filter.Value) : cacheEntry;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "List options failed.");
            throw;
        }
    }
}