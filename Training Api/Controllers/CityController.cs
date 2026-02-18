using Microsoft.AspNetCore.Mvc;
using Training_Api.Dtos;
using Training_Api.Services;

namespace Training_Api.Controllers;

public sealed class CityController : ControllerBase
{
    private readonly CityService _cityService;

    public CityController(CityService cityService)
    {
        _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
    }

    [HttpGet]
    [Route("/api/master/city")]
    public IActionResult GetAll()
    {
        IEnumerable<CityDto> result = _cityService.GetAll();
        return Ok(result);
    }

    [HttpGet]
    [Route("/api/master/state/{stateId:int}/city")]
    public IActionResult GetAllByState(int stateId)
    {
        IEnumerable<CityDto> result = _cityService.GetAllByState(stateId);
        return Ok(result);
    }

    [HttpPost]
    [Route("/api/master/state/{stateId:int}/city")]
    public IActionResult Create(int stateId, [FromBody] CreateCityRequest request)
    {
        try
        {
            _cityService.AddCity(stateId, request);

            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}