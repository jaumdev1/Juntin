using Domain.Dtos.JuntinMovie;
using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IJuntinMovieRepository : IBaseRepository<JuntinMovie>
{
    Task<List<ResultJuntinMovieDto>> GetPage(int page, Guid JuntinPlayId);
    
    Task<List<ResultHistoricJuntinMovieDto>> GetHistoric(int page, Guid JuntinPlayId);
}