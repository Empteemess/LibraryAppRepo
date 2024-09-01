using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private DbSet<Author> _auth;

    public AuthorRepository(LibraryDbContext context)
    {
        _auth = context.Set<Author>();
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await Task.FromResult(_auth
            .Include(auth => auth.AuthorBooks)!
            .ThenInclude(x => x.Book));
    }

    public async Task<Author> GetByIdAsync(Guid userId)
    {
        return (await _auth
            .Include(x => x.AuthorBooks)!
            .ThenInclude(b => b.Book)
            .FirstOrDefaultAsync(x => x.Id == userId))!;
    }

    public async Task AddAsync(Author entity)
    {
        await _auth.AddAsync(entity);
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var author = await GetByIdAsync(id);
        await Task.FromResult(_auth.Remove(author));
    }

    public async Task UpdateAsync(Author entity)
    {
        await Task.FromResult(_auth.Update(entity));
    }
}