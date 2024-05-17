using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinMovie;
using Domain.Dtos.JuntinMovie.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinMovieUseCase;

public class CreateJuntinMovie : ICreateJuntinMovieUseCase
{
    private readonly CreateJuntinMovieValidator _createJuntinMovieValidator;
    private readonly IJuntinMovieRepository _juntinMovieRepository;
    private readonly SessionManager _sessionManager;
    private readonly IUserJuntinRepository _userJuntinRepository;
    public CreateJuntinMovie(IJuntinMovieRepository juntinMovieRepository, SessionManager sessionManager,   IUserJuntinRepository userJuntinRepository)
    {
        _juntinMovieRepository = juntinMovieRepository;
        _createJuntinMovieValidator = new CreateJuntinMovieValidator();
        _sessionManager = sessionManager;
        _userJuntinRepository = userJuntinRepository;
    }

    public async Task<BasicResult<Guid>> Execute(JuntinMovieDto input)
    {
        try
        {
            var validations = await _createJuntinMovieValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<Guid>.ReturnError(validations);
            var juntinMovieMapped = input.Adapt<JuntinMovie>();

            juntinMovieMapped.Id = Guid.NewGuid();

            var ownerId = await _sessionManager.GetUserLoggedId();

            var isUserJuntin = await _userJuntinRepository.IsUserJuntin(juntinMovieMapped.JuntinPlayId, ownerId);

            if (!isUserJuntin)
                return BasicResult.Failure<Guid>(new Error(HttpStatusCode.Forbidden,
                    "You are not allowed to access this resource"));
            
            juntinMovieMapped.UserId = ownerId;

            await _juntinMovieRepository.Add(juntinMovieMapped);

            return juntinMovieMapped.Id;
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<Guid>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}