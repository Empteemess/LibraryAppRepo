using Domain.DTOs.AuthDtos;

namespace Domain.DTOs.BookDtos;

public class AddBookWithAuthorsDto
{
    public required BookDto BookDto { get; set; }
    public IEnumerable<AddAuthorDto>? AddAuthorDtos { get; set; }
}