using Domain.Helper;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IBookRepository BookRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    IAuthorBookRepository AuthorBookRepository { get; }
    IBookUserRepository BookUserRepository { get; }
    Task SaveAsync();
}