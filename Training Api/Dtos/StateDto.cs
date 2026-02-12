
namespace Training_Api.Web.Dtos;

public sealed class StateDto(int id, string name, string code)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Code { get; } = code;
}
