namespace Training_Api.Dtos;

public sealed class CreateCityRequest
{
    public required string Name  { get; init; }
    public int ID { get; set; }
}