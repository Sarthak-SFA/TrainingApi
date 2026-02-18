using Training_Api.Data;
using Training_Api.Dtos;

namespace Training_Api.Services;

public sealed class CityService
{
    private readonly AppDbContext _dbContext;

    public CityService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    //createrequest
    public void AddCity(int stateId, CreateCityRequest request)
    {
        State? state = _dbContext.State.Find(stateId);
        if (state == null)
        {
            throw new KeyNotFoundException($"State with id {stateId} not found");
        }

        City city = new() { Id = request.ID, Name = request.Name, StateId = stateId };

        _dbContext.City.Add(city);
        _dbContext.SaveChanges();
    }

    //get all
    public IEnumerable<CityDto> GetAll()
    {
        return _dbContext.City.Select(city => new CityDto(city.Id, city.Name, city.StateId))
            .ToList();
    }

    public IEnumerable<CityDto> GetAllByState(int stateId)
    {
        return _dbContext.City
            .Where(city => city.StateId == stateId)
            .Select(city => new CityDto(city.Id, city.Name, city.StateId))
            .ToList();
    }
}
