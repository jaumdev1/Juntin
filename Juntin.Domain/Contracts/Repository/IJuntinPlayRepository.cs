using Domain.Dtos.JuntinPlay;
using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IJuntinPlayRepository : IBaseRepository<JuntinPlay>
{
    Task<List<JuntinPlayResult>> GetPage(int page, Guid OwnerId);
    Task<JuntinPlay> GetByIdAndListUserJuntin(Guid Id);
    Task<JuntinPlayResult?> GetByIdAndUserAndMovieCount(Guid Id);
}