using Microsoft.AspNetCore.Mvc;
using Training_Api.Web.Models;
using Training_Api.Web.Services;

namespace Training_Api.Web.Controllers;

public sealed class StateController : Controller
{
    private readonly StateService _stateService;

    public StateController(StateService stateService)
    {
        _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
    }

    public IActionResult Index()
    {
        IEnumerable<StateViewModel> states = _stateService.GetStates();

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
        if (!ModelState.IsValid)
        {
            return View(state);
        }

        bool result = _stateService.CreateState(state);
        if (result)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}