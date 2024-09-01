using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class BookUserRepository : IBookUserRepository
{
    private DbSet<BookUser> _bookUsers;

    public BookUserRepository(LibraryDbContext context)
    {
        _bookUsers = context.Set<BookUser>();
    }

    public async Task<IEnumerable<BookUser>> GetAllAsync()
    {
        var bookUsers = await Task.FromResult(_bookUsers.Include(x => x.User));
        return bookUsers;
    }

    public async Task<BookUser> GetByIdAsync(Guid userId)
    {
        var bookUser = await _bookUsers
            .FirstOrDefaultAsync(i => i.UserId == userId);
        return bookUser!;
    }

    public async Task<BookUser?> GetByUserBookIdAsync(Guid userId, Guid? bookId)
    {
        var bookUser = await _bookUsers
            .Include(b => b.Book)
            .FirstOrDefaultAsync(i => i.UserId == userId
                                      && i.BookId == bookId);
        return bookUser;
    }

    public async Task ReturnBookByIds(Guid userId, Guid bookId)
    {
        var user = await GetByUserBookIdAsync(userId, bookId);
        await Task.FromResult(_bookUsers.Remove(user!));
    }

    public async Task AddAsync(BookUser? entity)
    {
        await _bookUsers.AddAsync(entity!);
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        await Task.FromResult(_bookUsers.Remove(user));
    }

    public async Task UpdateAsync(BookUser? entity)
    {
        await Task.FromResult(_bookUsers.Update(entity!));
    }
}