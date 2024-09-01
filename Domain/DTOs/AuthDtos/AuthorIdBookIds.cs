using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AuthDtos;

public class AuthorIdBookIds
{
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    public IEnumerable<Guid>? BookIds { get; set; }    
}