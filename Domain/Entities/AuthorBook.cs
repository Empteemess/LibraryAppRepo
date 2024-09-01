namespace Domain.Entities;

public class AuthorBook : BaseEntity
{
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }
    
    public Author? Author { get; set; }
    public Book? Book { get; set; }
}