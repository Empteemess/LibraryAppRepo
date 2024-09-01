using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Domain.DTOs.BookDtos;

public class BookDto : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Range(0,int.MaxValue)]
    public int TotalCount { get; set; }
    [Range(0,int.MaxValue)]
    public int CurrentCount { get; set; }

    public string? Image { get; set; }
    
    public string? UniqImageString { get; set; }
}