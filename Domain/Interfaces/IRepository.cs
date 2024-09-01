namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid userId);
    Task AddAsync(T entity);
    Task RemoveByIdAsync(Guid id);
    Task UpdateAsync(T entity);
}