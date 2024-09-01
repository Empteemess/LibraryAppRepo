namespace Domain.Entities;

public class Book : BaseEntity
{
    public required string Title { get; set; }
    public required string Descrption { get; set; }
    public int TotalCount { get; set; }
    public int CurrentCount { get; set; }
    
    public string? Image { get; set; }
    public string? UniqImageString { get; set; }

    public ICollection<AuthorBook>? AuthorBooks { get; set; }
    public ICollection<BookUser>? BookUsers { get; set; }
}