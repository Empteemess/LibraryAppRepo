using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookUserRepository : IRepository<BookUser>
{
    Task<BookUser?> GetByUserBookIdAsync(Guid userId, Guid? bookId);
    Task ReturnBookByIds(Guid userId, Guid bookId);
}