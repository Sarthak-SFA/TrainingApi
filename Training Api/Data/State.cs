using System.ComponentModel.DataAnnotations;

namespace Training_Api.Data;

public sealed class State
{
    [Key] public int Id { get; set; }
    [StringLength(100)] public required string Name { get; set; }
    [StringLength(2)] public required string Code { get; set; }
    public required bool IsActive { get; set; }
}