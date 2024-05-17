using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinPlay;
using Domain.Dtos.JuntinPlay.Validator;
using Juntin.Application.Interfaces;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinPlayUseCase;

public class GetOneJuntinPlay : IGetOneJuntinPlayUseCase
{
    private readonly GetOneJuntinPlayValidator _getJuntinPlayValidator;
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly SessionManager _sessionManager;
    private readonly IUserJuntinRepository _userJuntinRepository;

    public GetOneJuntinPlay(IJuntinPlayRepository juntinPlayRepository, SessionManager sessionManager, IUserJuntinRepository userJuntinRepository)
    {
        _juntinPlayRepository = juntinPlayRepository;
        _getJuntinPlayValidator = new GetOneJuntinPlayValidator();
        _sessionManager = sessionManager;
        _userJuntinRepository = userJuntinRepository;
    }

    public async Task<BasicResult<JuntinPlayResult>> Execute(GetOneJuntinPlayDto input)
    {
        try
        {
            var validations = await _getJuntinPlayValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<JuntinPlayResult>.ReturnError(validations);

            Guid ownerId = await _sessionManager.GetUserLoggedId();

            bool isUserJuntin = await _userJuntinRepository.IsUserJuntin(input.JuntinPlayId, ownerId);

            if (!isUserJuntin)
                return BasicResult.Failure<JuntinPlayResult>(new Error(HttpStatusCode.Forbidden,
                    "You are not allowed to access this resource"));
            
            var JuntinPlay = await _juntinPlayRepository.GetByIdAndUserAndMovieCount(input.JuntinPlayId);
            
            if (JuntinPlay == null)
                return BasicResult.Failure<JuntinPlayResult>(
                    new Error(HttpStatusCode.NotFound, "JuntinPlay not found"));
       
            return BasicResult.Success(JuntinPlay);
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<JuntinPlayResult>(
                new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}