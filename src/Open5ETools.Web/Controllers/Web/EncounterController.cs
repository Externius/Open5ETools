using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Open5ETools.Core.Common.Enums.EG;
using Open5ETools.Core.Common.Exceptions;
using Open5ETools.Core.Common.Extensions;
using Open5ETools.Core.Common.Helpers;
using Open5ETools.Core.Common.Interfaces.Services.EG;
using Open5ETools.Core.Common.Models.EG;
using Open5ETools.Web.Models.Encounter;

namespace Open5ETools.Web.Controllers.Web;

[Authorize]
public class EncounterController(
    IEncounterService encounterService,
    ILogger<EncounterController> logger,
    IMapper mapper) : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<EncounterController> _logger = logger;
    private readonly IEncounterService _encounterService = encounterService;

    public async Task<IActionResult> Index()
    {
        var model = new EncounterOptionViewModel
        {
            PartyLevel = 1,
            PartyLevels = SelectListHelper.GenerateIntSelectList(1, 20),
            PartySize = 4,
            PartySizes = SelectListHelper.GenerateIntSelectList(1, 10),
            Difficulties = (await _encounterService.GetEnumListAsync<Difficulty>())
            .Select(k => new SelectListItem { Text = k.Key, Value = k.Value.ToString() })
            .AddFirstItem(),
            SelectedMonsterTypes = [.. Enum.GetValues<MonsterType>()],
            MonsterTypes = (await _encounterService.GetEnumListAsync<MonsterType>())
                .Select(k => new SelectListItem
                {
                    Text = k.Key,
                    Value = k.Value.ToString(),
                    Selected = true
                }),
            SelectedSizes = [.. Enum.GetValues<Size>()],
            Sizes = (await _encounterService.GetEnumListAsync<Size>())
                .Select(k => new SelectListItem
                {
                    Text = k.Key,
                    Value = k.Value.ToString()
                })
        };
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Generate(EncounterOptionViewModel optionModel)
    {
        var option = _mapper.Map<EncounterOption>(optionModel);
        try
        {
            var encounters = await _encounterService.GenerateAsync(option);
            var model = new EncounterViewModel()
            {
                Details = encounters.Monsters.Select(_mapper.Map<MonsterViewModel>),
                SumXp = encounters.SumXp
            };
            return PartialView("_Details", model);
        }
        catch (Exception ex)
        {
            return Problem(
                title: ex.Message,
                detail: ex.LocalizedMessage(),
                statusCode: StatusCodes.Status500InternalServerError
                );
        }
    }

    public async Task<IActionResult> MonsterDetail(int id)
    {
        var monster = await _encounterService.GetMonsterByIdAsync(id);
        var model = _mapper.Map<MonsterViewModel>(monster);
        return PartialView("_MonsterDetail", model);
    }
}