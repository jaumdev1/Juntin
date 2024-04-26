using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IJuntinPlayRepository : IBaseRepository<JuntinPlay>
{
  Task<List<JuntinPlay>> GetPage(int page, Guid OwnerId);
}