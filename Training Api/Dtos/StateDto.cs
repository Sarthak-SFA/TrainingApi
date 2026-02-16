namespace Training_Api.Dtos;

public sealed class StateDto(int id, string name, string code, bool IsActive)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Code { get; } = code;
    public bool IsActive { get; } = IsActive;
}