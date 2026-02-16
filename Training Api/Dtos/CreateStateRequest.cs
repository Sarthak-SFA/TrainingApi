using System.ComponentModel.DataAnnotations;

namespace Training_Api.Dtos;

public sealed class CreateStateRequest
{
    [Required] public required string Name { get; init; }

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public required string Code { get; init; }

   [Required] public bool IsActive { get; set; }
}