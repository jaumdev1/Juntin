using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinPlay;
using Domain.Dtos.JuntinPlay.Validator;
using Domain.Dtos.User.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinPlayUseCase;

public class CreateJuntinPlay : ICreateJuntinPlayUseCase
{
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly CreateJuntinPlayValidator _createJuntinPlayValidator;
    private readonly SessionManager _sessionManager;
    
    public CreateJuntinPlay(IJuntinPlayRepository juntinPlayRepository, SessionManager sessionManager)
    {
        _juntinPlayRepository = juntinPlayRepository;
        _createJuntinPlayValidator = new CreateJuntinPlayValidator();
        _sessionManager = sessionManager;
    }
    
    public async Task<BasicResult<Guid>> Execute(JuntinPlayDto input)
    {
        try
        {
            var validations = await _createJuntinPlayValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<Guid>.ReturnError(validations);
            var juntinPlayMapped = input.Adapt<JuntinPlay>();
            
            juntinPlayMapped.Id = Guid.NewGuid();

            Guid ownerId =  await _sessionManager.GetUserLoggedId();
            
           juntinPlayMapped.OwnerId = ownerId;
            
            await _juntinPlayRepository.Add(juntinPlayMapped);
            
            return juntinPlayMapped.Id;
            
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<Guid>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }


}