using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AuthorBookRepository : IAuthorBookRepository
{
    private DbSet<AuthorBook> _authBook;

    public AuthorBookRepository(LibraryDbContext context)
    {
        _authBook = context.Set<AuthorBook>();
    }

    public async Task<IEnumerable<AuthorBook>> GetAllAsync()
    {
        return await Task.FromResult(_authBook);
    }

    public async Task<AuthorBook> GetByIdAsync(Guid userId)
    {
        return (await _authBook.FirstOrDefaultAsync(x => x.Id == userId))!;
    }

    public async Task AddAsync(AuthorBook entity)
    {
        await _authBook.AddAsync(entity);
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var author = await GetByIdAsync(id);
        await Task.FromResult(_authBook.Remove(author));
    }

    public async Task UpdateAsync(AuthorBook entity)
    {
        await Task.FromResult(_authBook.Update(entity));
    }
}