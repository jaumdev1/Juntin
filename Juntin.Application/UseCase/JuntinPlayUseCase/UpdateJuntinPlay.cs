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

public class UpdateJuntinPlay : IUpdateJuntinPlayUseCase
{
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly SessionManager _sessionManager;
    private readonly UpdateJuntinPlayValidator _updateJuntinPlayValidator;

    public UpdateJuntinPlay(IJuntinPlayRepository juntinPlayRepository, SessionManager sessionManager)
    {
        _juntinPlayRepository = juntinPlayRepository;
        _updateJuntinPlayValidator = new UpdateJuntinPlayValidator();
        _sessionManager = sessionManager;
    }

    public async Task<BasicResult> Execute(UpdateJuntinPlayDto input)
    {
        try
        {
            var validations = await _updateJuntinPlayValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<BasicResult>.ReturnError(validations);

            var ownerId = await _sessionManager.GetUserLoggedId();

            var juntin = await _juntinPlayRepository.GetById(input.Id);

            if (juntin.OwnerId != ownerId)
                return BasicResult.Failure(new Error(HttpStatusCode.Forbidden,
                    "You are not the owner of this JuntinPlay"));

            input.Adapt(juntin);

            await _juntinPlayRepository.Update(juntin);

            return BasicResult.Success();
        }
        catch (Exception ex)
        {
            return BasicResult.Failure(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}