using Domain.Common;
using Domain.Dtos.Movie;

namespace Juntin.Application.Interfaces.Movie;

public interface IMovieUseCase : IUseCaseBase<MovieDto, BasicResult<List<MovieResult>>>
{
}