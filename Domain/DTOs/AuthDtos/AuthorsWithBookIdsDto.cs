using Domain.DTOs.BookDtos;

namespace Domain.DTOs.AuthDtos;

public class AuthorsWithBookIdsDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public IEnumerable<BookNamesDto>? Books { get; set; }    
}