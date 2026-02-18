using Microsoft.AspNetCore.Mvc;
using Training_Api.Dtos;
using Training_Api.Models;
using Training_Api.Services;

namespace Training_Api.Controllers;

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
    public IActionResult Delete(int id)
    {
        bool deleted = _stateService.DeleteState(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch]
    [Route("{id:int}")]
    public IActionResult UpdateStatus(int id, [FromBody] PatchRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        StateDto? state = _stateService.PatchRequest(id, request.IsActive);

        if (state is null)
        {
            return NotFound("State not found.");
        }

        return Ok(state);
    }
}
