using Training_Api.Data;
using Training_Api.Dtos;
using Training_Api.Models;

namespace Training_Api.Services;

public sealed class StateService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<StateService> _logger;
    private object _context;

    public StateService(AppDbContext dbContext, ILogger<StateService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger;
    }

    // Get all states (ViewModel)
    public IEnumerable<StateViewModel> GetStates()
    {
        return _dbContext.State
            .Select(state => new StateViewModel
            {
                Id = state.Id,
                StateName = state.Name,
                Code = state.Code,
                IsActive = state.IsActive,
            })
            .ToArray();
    }

    // Get all states (DTO)
    public IEnumerable<StateDto> GetStateList()
    {
        return _dbContext.State
            .Select(state => new StateDto(state.Id, state.Name, state.Code, state.IsActive))
            .ToArray();
    }

    // Get single state by id
    public StateDto? GetState(int id)
    {
        var state = _dbContext.State.Find(id);
        if (state == null) return null;

        return new StateDto(state.Id, state.Name, state.Code , state.IsActive);
    }

    //Solution 
    public bool CreateState(CreateStateRequest request)
    {
        try
        {
            var isDuplicate = _dbContext.State
                .Any(s => s.Name.ToLower().Trim() == request.Name.ToLower().Trim()
                          || s.Code.ToLower().Trim() == request.Code.ToLower().Trim());

            if (isDuplicate)
                throw new Exception($"State with Name '{request.Name}' or Code '{request.Code}' already exists.");

            var state = new State
            {
                Name = request.Name,
                Code = request.Code,
                IsActive = request.IsActive
            };

            _dbContext.State.Add(state);
            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating state {@StateRequest}", request);
            return false;
        }
    }

    //for checking duplicate (excluding current record)
    public StateDto? UpdateState(int id, CreateStateRequest request)
    {
        try
        {
            var state = _dbContext.State.Find(id);
            if (state == null) return null;


            var isDuplicate = _dbContext.State
                .Any(s => (s.Name.ToLower().Trim() == request.Name.ToLower().Trim()
                           || s.Code.ToLower().Trim() == request.Code.ToLower().Trim())
                          && s.Id != id);

            if (isDuplicate) throw new Exception("State Name or Code already exists in another record.");


            state.Name = request.Name;
            state.Code = request.Code;

            _dbContext.SaveChanges();

            return new StateDto(state.Id, state.Name, state.Code , state.IsActive);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating state with Id {Id} and request {@StateRequest}", id, request);
            return null;
        }
    }

    internal bool CreateState(StateViewModel state)
    {
        throw new NotImplementedException();
    }


    //delete 
    public bool DeleteState(int id)
    {
        try
        {
            var state = _dbContext.State.Find(id);
            if (state == null) return false;

            _dbContext.State.Remove(state);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting state with Id {Id}", id);
            return false;
        }
    }
    
    //isActive Patch Request
    public StateDto? PatchRequest(int id, bool IsActive)
    {
        var state = _dbContext.State.FirstOrDefault(x => x.Id == id);

        if (state == null)
            return null;

        state.IsActive = IsActive;   

        _dbContext.SaveChanges();

        return new StateDto(
            state.Id,
            state.Name,
            state.Code ,
            state.IsActive
        );
 
    }

}