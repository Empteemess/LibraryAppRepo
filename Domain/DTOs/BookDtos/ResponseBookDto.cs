using Domain.DTOs.AuthDtos;

namespace Domain.DTOs.BookDtos;

public class ResponseBookDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Descrption { get; set; }
    public int TotalCount { get; set; }
    public int CurrentCount { get; set; }
    public string? Image { get; set; }
    

    public IEnumerable<AllBookAuthorDto>? Authors { get; set; }
}