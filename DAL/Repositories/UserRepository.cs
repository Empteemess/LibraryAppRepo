using DAL.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private DbSet<User> User { get; set; }

    public UserRepository(LibraryDbContext context)
    {
        User = context.Set<User>();
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Task.FromResult(User.Include(x => x.BookUsers)!.ThenInclude(b => b.Book));
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        return (await User
            .Include(x => x.BookUsers)!
            .ThenInclude(t => t.Book)
            .FirstOrDefaultAsync(x => x.Id == userId))!;
    }

    public async Task AddAsync(User user)
    {
        await User.AddAsync(user);
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        await Task.FromResult(User.Remove(user));
    }

    public async Task UpdateAsync(User user)
    {
        await Task.FromResult(User.Update(user));
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await User.FirstOrDefaultAsync(x => x.Email == email);
    }
}