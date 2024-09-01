using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class BookRepository : IBookRepository
{
    private DbSet<Book> _book;

    public BookRepository(LibraryDbContext libraryDbContext)
    {
        _book = libraryDbContext.Set<Book>();
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await Task.FromResult(_book.Include(x => x.AuthorBooks)!.ThenInclude(a => a.Author));
    }

    public async Task<Book> GetByIdAsync(Guid userId)
    {
        return (await _book
            .Include(x => x.AuthorBooks)!
            .ThenInclude(au => au.Author)
            .FirstOrDefaultAsync(x => x.Id == userId))!;
    }

    public async Task AddAsync(Book entity)
    {
        await _book.AddAsync(entity);
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var book = await GetByIdAsync(id);
        await Task.FromResult(_book.Remove(book));
    }

    public async Task UpdateAsync(Book entity)
    {
        await Task.FromResult(_book.Update(entity));
    }
}