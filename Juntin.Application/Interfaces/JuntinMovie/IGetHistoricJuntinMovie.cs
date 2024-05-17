using Domain.Common;
using Domain.Dtos.JuntinMovie;

namespace Juntin.Application.Interfaces.JuntinMovie;

public interface IGetHistoricJuntinMovie : IUseCaseBase<GetJuntinMovieDto, BasicResult<List<ResultHistoricJuntinMovieDto>>>
{
}