using Microsoft.AspNetCore.Mvc;
using Training_Api.Models;
using Training_Api.Services;

namespace Training_Api.Controllers;

public sealed class StateController : Controller
{
    private readonly StateService _stateService;

    public StateController(StateService stateService)
    {
        _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
    }

    public IActionResult Index()
    {
        var states = _stateService.GetStates();

        return View(states);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm] StateViewModel state)
    {
        if (!ModelState.IsValid) return View(state);

        var result = _stateService.CreateState(state);
        if (result) return RedirectToAction(nameof(Index));

        return View();
    }
}