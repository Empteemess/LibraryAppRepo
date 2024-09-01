using Domain.Helper;
using Domain.Interfaces;

namespace DAL.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IAuthorBookRepository _authorBookRepository;
    private readonly IBookUserRepository _bookUserRepository;
    private readonly LibraryDbContext _context;

    public UnitOfWork(IUserRepository userRepository,
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IAuthorBookRepository authorBookRepository,
        IBookUserRepository bookUserRepository,
        LibraryDbContext context)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _authorBookRepository = authorBookRepository;
        _bookUserRepository = bookUserRepository;
        _context = context;
    }

    public IUserRepository UserRepository => _userRepository;
    public IBookRepository BookRepository => _bookRepository;
    public IAuthorRepository AuthorRepository => _authorRepository;
    public IAuthorBookRepository AuthorBookRepository => _authorBookRepository;
    public IBookUserRepository BookUserRepository => _bookUserRepository;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}