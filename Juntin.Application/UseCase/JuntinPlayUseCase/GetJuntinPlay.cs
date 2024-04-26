using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinPlay;
using Domain.Dtos.JuntinPlay.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinPlayUseCase;

public class GetJuntinPlay : IGetJuntinPlayUseCase
{
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly GetJuntinPlayValidator _getJuntinPlayValidator;
    private readonly SessionManager _sessionManager;

    public GetJuntinPlay(IJuntinPlayRepository juntinPlayRepository, SessionManager sessionManager)
    {
        _juntinPlayRepository = juntinPlayRepository;
        _getJuntinPlayValidator = new GetJuntinPlayValidator();
        _sessionManager = sessionManager;
    }

    public async Task<BasicResult<List<JuntinPlayResult>>> Execute(GetJuntinPlayDto input)
    {
        try
        {
            var validations = await _getJuntinPlayValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<List<JuntinPlayResult>>.ReturnError(validations);

            Guid ownerId = await _sessionManager.GetUserLoggedId();

            var JuntinPlay = await _juntinPlayRepository.GetPage(input.Page, ownerId);
            var JuntinPlayDto = JuntinPlay.Adapt<List<JuntinPlayResult>>();
            
            return BasicResult.Success(JuntinPlayDto);
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<List<JuntinPlayResult>>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}

