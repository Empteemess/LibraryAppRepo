namespace Domain.Entities;

public class Author : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public ICollection<AuthorBook>? AuthorBooks { get; set; }
}