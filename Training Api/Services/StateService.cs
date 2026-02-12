using Training_Api.Web.Models;
using Training_Api.Web.Data;
using Training_Api.Web.Dtos;

namespace Training_Api.Web.Services;

public sealed class StateService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<StateService> _logger;

    public StateService(AppDbContext dbContext, ILogger<StateService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger;
    }

    public IEnumerable<StateViewModel> GetStates()
    {
        IReadOnlyList<StateViewModel> states = _dbContext.State
            .Select(state => new StateViewModel { Id = state.Id, StateName = state.Name, Code = state.Code })
            .ToArray();

        return states;
    }

    public IEnumerable<StateDto> GetStateList()
    {
        IReadOnlyList<StateDto> states = _dbContext.State
            .Select(state => new StateDto(state.Id, state.Name, state.Code))
            .ToArray();

        return states;
    }

    public bool CreateState(StateViewModel model)
    {
        State state = new() { Name = model.StateName, Code = model.Code };

        _dbContext.State.Add(state);
        _dbContext.SaveChanges();

        return true;
    }

    public bool CreateState(CreateStateRequest request)
    {
        try
        {
            State? state = _dbContext.State.FirstOrDefault(s => s.Code == request.Code);
            if (state is not null)
            {
                throw new Exception($"State with code {request.Code} already exists.");
            }

            state = new State { Name = request.Name, Code = request.Code };

            _dbContext.State.Add(state);
            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            // _logger.LogError(e, "Error while creating a state with name {stateName} {code}.", request.Name,
            //     request.Code);
            _logger.LogError(e, "Error while creating a state with name {@state}.", request);
            return false;
        }
    }

    public StateDto? UpdateState(int id, CreateStateRequest request)
    {
        try
        {
            State? state = _dbContext.State.Find(id);

            if (state is null)
            {
                return null;
            }

            state.Name = request.Name;
            state.Code = request.Code;

            _dbContext.SaveChanges();

            return new StateDto(state.Id, state.Name, state.Code);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while creating a state with name {stateName} {code}.", request.Name,
                request.Code);
            // _logger.LogError(e, "Error while updating a state {@state}.", request);
            return null;
        }
    }

    public StateDto? GetState(int id)
    {
        State? state = _dbContext.State.Find(id);
        if (state is null)
        {
            return null;
        }

        return new StateDto(state.Id, state.Name, state.Code);
    }
}