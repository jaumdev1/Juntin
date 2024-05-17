using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IUserJuntinRepository : IBaseRepository<UserJuntin>
{

    Task<bool> IsUserJuntin(Guid JuntinPlayId, Guid userId);
    
    Task<UserJuntin> GetByJuntinPlayAndUser(Guid JuntinPlayId, Guid userId);
}