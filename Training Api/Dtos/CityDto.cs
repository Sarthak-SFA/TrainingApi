 namespace Training_Api.Dtos;
    
    public sealed class CityDto
    {
        public CityDto(int id, string name, int stateId)
        {
            Id = id;
            Name = name;
            StateId = stateId;
        }
    
        public int Id { get; }
        public string Name { get; }
        public int StateId { get; }
    };
