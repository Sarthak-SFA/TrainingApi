using Training_Api.Data;
using Training_Api.Web.Data;

namespace Training_Api.Services;

public sealed class CityService
{
    private readonly AppDbContext _dbContext;

    public CityService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void AddCity()
    {
        _dbContext.State.Add(new State
        {
            Name = "New City",
            Code = "NC",
            IsActive = false
        });
        _dbContext.SaveChanges();
    }
}