using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinMovie;
using Domain.Dtos.JuntinMovie.Validator;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Application.Security;

namespace Juntin.Application.UseCase.JuntinMovieUseCase;

public class DeleteJuntinMovie : IDeleteJuntinMovieUseCase
{
   
    private readonly DeleteJuntinMovieValidator _deleteJuntinMovieValidator;
    private readonly IJuntinMovieRepository _juntinMovieRepository;
    private readonly SessionManager _sessionManager;
    private readonly IUserJuntinRepository _userJuntinRepository;
    public DeleteJuntinMovie(IJuntinMovieRepository juntinMovieRepository, SessionManager sessionManager,   IUserJuntinRepository userJuntinRepository)
    {
        _juntinMovieRepository = juntinMovieRepository;
        _deleteJuntinMovieValidator = new DeleteJuntinMovieValidator();
        _sessionManager = sessionManager;
        _userJuntinRepository = userJuntinRepository;
    }
    public async Task<BasicResult> Execute(DeleteJuntinMovieDto input)
    {
        try
        {
            var validations = await _deleteJuntinMovieValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<BasicResult>.ReturnError(validations);
         
            
            var ownerId = await _sessionManager.GetUserLoggedId();
            
            var juntin = await _juntinMovieRepository.GetById(input.Id);

            var isUserJuntin = await _userJuntinRepository.IsUserJuntin(juntin.JuntinPlayId, ownerId);

            if (!isUserJuntin)
                return BasicResult.Failure(new Error(HttpStatusCode.Forbidden,
                    "You are not allowed to access this resource"));
            
            
            await _juntinMovieRepository.DeleteById(input.Id);

           return BasicResult.Success();
        }
        catch (Exception ex)
        {
            return BasicResult.Failure(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}