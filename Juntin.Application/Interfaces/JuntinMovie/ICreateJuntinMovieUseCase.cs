using Domain.Common;
using Domain.Dtos.JuntinMovie;

namespace Juntin.Application.Interfaces.JuntinMovie;

public interface ICreateJuntinMovieUseCase : IUseCaseBase<JuntinMovieDto, BasicResult<Guid>>
{
}