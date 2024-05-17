using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinMovie;
using Domain.Dtos.Movie.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinMovieUseCase;

public class GetHistoricJuntinMovie : IGetHistoricJuntinMovie
{
    private readonly GetJuntinMovieValidator _getJuntinMovieValidator;

    private readonly IJuntinMovieRepository _juntinMovieRepository;
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly SessionManager _sessionManager;
    private readonly IUserJuntinRepository _userJuntinRepository;
    private readonly IUserViewedJuntinMovieRepository _viewedJuntinMovieRepository;
    public GetHistoricJuntinMovie(IJuntinMovieRepository juntinMovieRepository, IJuntinPlayRepository juntinPlayRepository,
        IUserJuntinRepository userJuntinRepository, SessionManager sessionManager, IUserViewedJuntinMovieRepository viewedJuntinMovieRepository)
    {
        _juntinMovieRepository = juntinMovieRepository;
        _sessionManager = sessionManager;
        _juntinPlayRepository = juntinPlayRepository;
        _getJuntinMovieValidator = new GetJuntinMovieValidator();
        _userJuntinRepository = userJuntinRepository;
        _viewedJuntinMovieRepository = viewedJuntinMovieRepository;
        
    }

    public async Task<BasicResult<List<ResultHistoricJuntinMovieDto>>> Execute(GetJuntinMovieDto input)
    {
        try
        {
            var validations = await _getJuntinMovieValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<List<ResultHistoricJuntinMovieDto>>.ReturnError(validations);
            var juntinMovieMapped = input.Adapt<JuntinMovie>();

            

            Guid ownerId = await _sessionManager.GetUserLoggedId();

            bool isUserJuntin = await _userJuntinRepository.IsUserJuntin(juntinMovieMapped.JuntinPlayId, ownerId);

            if (!isUserJuntin)
                return BasicResult.Failure<List<ResultHistoricJuntinMovieDto>>(new Error(HttpStatusCode.Forbidden,
                    "You are not allowed to access this resource"));
            
            var juntinsMovies = await _juntinMovieRepository.GetHistoric(input.Page, input.JuntinPlayId);
            
          var userJuntin = await _userJuntinRepository.GetByJuntinPlayAndUser(input.JuntinPlayId, ownerId); 
          
           var moviesViewed= await _viewedJuntinMovieRepository.GetByJuntinUserAndJuntinPlay(userJuntin.Id, input.JuntinPlayId );
            
         
            return juntinsMovies;
        }            
        catch (Exception ex)
        {
            return BasicResult.Failure<List<ResultHistoricJuntinMovieDto>>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}