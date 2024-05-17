using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IUserViewedJuntinMovieRepository : IBaseRepository<UserViewedJuntinMovie>
{
    Task<UserViewedJuntinMovie?> GetByJuntinUserAndJuntinMovie(Guid userJuntinId, Guid juntinMovieId);
    
    Task<List<UserViewedJuntinMovie>> GetByJuntinUserAndJuntinPlay(Guid userJuntinId, Guid juntinPlayId);
}