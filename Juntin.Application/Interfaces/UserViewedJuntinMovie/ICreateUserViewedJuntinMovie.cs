using Domain.Common;
using Domain.Dtos.JuntinMovie.Validator;
using Domain.Dtos.Movie;
using Domain.Dtos.UserViewedJuntinMovie;

namespace Juntin.Application.Interfaces.UserViewedJuntinMovie;

public interface ICreateUserViewedJuntinMovie : IUseCaseBase<CreateUserViewedJuntinMovieDto, BasicResult<ResultViwedJuntinMovieDto>>
{
  
}