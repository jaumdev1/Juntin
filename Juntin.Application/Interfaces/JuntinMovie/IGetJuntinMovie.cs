using Domain.Common;
using Domain.Dtos.JuntinMovie;

namespace Juntin.Application.Interfaces.JuntinMovie;

public interface IGetJuntinMovieUseCase : IUseCaseBase<GetJuntinMovieDto, BasicResult<List<ResultJuntinMovieDto>>>
{
}