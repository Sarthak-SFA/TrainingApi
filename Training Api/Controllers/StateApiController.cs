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



//[HttpPut]
//    [Route("{id:int}")]
//    public IActionResult UpdateStateName([FromBody] CreateStateRequest request, int id)
//    {
//        try
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            StateDto? state = _stateService.UpdateState(id, request);
//            return state is null ? NotFound() : Ok(state);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, "Something went wrong: " + ex.Message);
//        }
//    }

public StateDto? UpdateState(int id, CreateStateRequest request)
    {
        try
        {
            var existingState = _stateService.UpdateState
                                        .FirstOrDefault(x => x.Id == id);

            if (existingState == null)
                return null;

            // 🔎 Duplicate check (ignore same record)
            bool isDuplicate = _context.States
                                       .Any(x => x.StateName.ToLower().Trim() == request.StateName.ToLower().Trim()
                                                 && x.Id != id);

            if (isDuplicate)
            {
                throw new Exception("State name already exists.");
            }

            // ✅ Update allowed
            existingState.StateName = request.Name;
            existingState.StateCode = request.Code;

            _DbContext.SaveChanges();

            return new StateDto
            {
                Id = existingState.Id,
                StateName = existingState.StateName,
                StateCode = existingState.StateCode
            };
        }
        catch (Exception)
        {
            throw; // controller me handle hoga
        }
    }
