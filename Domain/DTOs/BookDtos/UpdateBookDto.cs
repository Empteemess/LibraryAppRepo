using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.BookDtos;

public class UpdateBookDto
{
    public Guid BookId { get; set; }
    public string? Title { get; set; }
    public string? Descrption { get; set; }
    [Range(0,int.MaxValue)]
    public int TotalCount { get; set; }
    [Range(0,int.MaxValue)]
    public int CurrentCount { get; set; }

    public string? Image { get; set; }
}