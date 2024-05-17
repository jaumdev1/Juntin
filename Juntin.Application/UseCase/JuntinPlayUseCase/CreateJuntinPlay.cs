using System.Net;
using Domain;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinPlay;
using Domain.Dtos.JuntinPlay.Validator;
using Domain.Dtos.UserJutin;
using Domain.Entities;
using Juntin.Application.Interfaces;
using Juntin.Application.Interfaces.UserJutin;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinPlayUseCase;

public class CreateJuntinPlay : ICreateJuntinPlayUseCase
{
    private readonly CreateJuntinPlayValidator _createJuntinPlayValidator;
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly SessionManager _sessionManager;
    private readonly ICreateUserJutin  _createUserJuntin;
    public CreateJuntinPlay(IJuntinPlayRepository juntinPlayRepository, SessionManager sessionManager, ICreateUserJutin createUserJutin)
    {
        _juntinPlayRepository = juntinPlayRepository;
        _createJuntinPlayValidator = new CreateJuntinPlayValidator();
        _sessionManager = sessionManager;
        _createUserJuntin = createUserJutin;
    }

    public async Task<BasicResult<Guid>> Execute(JuntinPlayDto input)
    {
        try
        {
            var validations = await _createJuntinPlayValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<Guid>.ReturnError(validations);
            var juntinPlayMapped = input.Adapt<JuntinPlay>();

            juntinPlayMapped.Id = Guid.NewGuid();

            var ownerId = await _sessionManager.GetUserLoggedId();

            juntinPlayMapped.OwnerId = ownerId;
            
            await _juntinPlayRepository.Add(juntinPlayMapped);
            
            var userJuntinDto = new CreateUserJutinDto(ownerId, UserRole.Admin, juntinPlayMapped.Id);
            var createUserJuntinResult = await _createUserJuntin.Execute(userJuntinDto);
            if (!createUserJuntinResult.IsSuccess)
            {
                return BasicResult.Failure<Guid>(createUserJuntinResult.Error);
            }

            return juntinPlayMapped.Id;
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<Guid>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}