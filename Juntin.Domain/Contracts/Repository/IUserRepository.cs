using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUsername(string username);
    Task<bool> UserExists(string email, string username);
}