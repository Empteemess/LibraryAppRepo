using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
   Task<User> GetUserByEmail(string email);
}