using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Open5ETools.Core.Common.Interfaces.Services.SM;
using Open5ETools.Web.Models;
using Open5ETools.Web.Models.Spell;

namespace Open5ETools.Web.Controllers.Web;

[Authorize]
public class SpellController(
    ISpellService spellService,
    ILogger<SpellController> logger,
    IMapper mapper) : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<SpellController> _logger = logger;
    private readonly ISpellService _spellService = spellService;

    public async Task<IActionResult> Index(
        string search,
        string sort = "Level",
        bool ascending = false,
        int page = 1,
        int pageSize = 10)
    {
        var result = await _spellService.ListAsync(search);
        var items = result.Select(_mapper.Map<SpellViewModel>);
        var model = new XPagedListViewModel<SpellViewModel>(items, page, pageSize, search, sort, ascending);
        return View(model);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var spell = await _spellService.GetAsync(id);
        var model = _mapper.Map<SpellViewModel>(spell);
        return PartialView("_Details", model);
    }
}