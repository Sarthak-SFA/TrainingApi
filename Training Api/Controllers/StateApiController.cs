using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training_Api.Web.Dtos;
using Training_Api.Web.Models;
using Training_Api.Web.Services;

namespace Training_Api.Web.Controllers;

[Route("api/master/state")]
public sealed class StateApiController : ControllerBase
{
    private readonly StateService _stateService;

    public StateApiController(StateService stateService)
    {
        _stateService = stateService;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Get()
    {
        IEnumerable<StateViewModel> states = _stateService.GetStates();

        return Ok(states);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get(int id)
    {
        StateDto? state = _stateService.GetState(id);
        return state == null ? NotFound("State not found.") : Ok(state);
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] CreateStateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = _stateService.CreateState(request);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update([FromBody] CreateStateRequest request, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        StateDto? state = _stateService.UpdateState(id, request);
        return state is null ? NotFound() : Ok(state);
    }


    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult  Delete(int id)
    {
        var deleted = _stateService.DeleteState(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}





