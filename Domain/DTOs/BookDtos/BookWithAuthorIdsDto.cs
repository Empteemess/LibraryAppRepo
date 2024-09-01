namespace Domain.DTOs.BookDtos;

public class BookWithAuthorIdsDto
{
    public BookDto? BookDto { get; set; }
    
    public IEnumerable<Guid>? AuthorIds { get; set; }
}