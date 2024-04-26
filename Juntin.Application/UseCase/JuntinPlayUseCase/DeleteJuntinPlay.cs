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

public class DeleteJuntinPlay : IDeleteJuntinPlayUseCase
{
       
 
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly DeleteJuntinPlayValidator _deleteJuntinPlayValidator;
    private readonly SessionManager _sessionManager;

    public DeleteJuntinPlay(IJuntinPlayRepository juntinPlayRepository, SessionManager sessionManager)
    {
        _juntinPlayRepository = juntinPlayRepository;
        _deleteJuntinPlayValidator = new DeleteJuntinPlayValidator();
        _sessionManager = sessionManager;
        
    }
    public async Task<BasicResult> Execute(DeleteJuntinPlayDto input)
    {
        try
        {
            var validations = await _deleteJuntinPlayValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<BasicResult>.ReturnError(validations);

            Guid ownerId =  await _sessionManager.GetUserLoggedId();

            var juntin = await _juntinPlayRepository.GetById(input.Id);

            if(juntin.OwnerId != ownerId)
                return BasicResult.Failure(new Error(HttpStatusCode.Forbidden, "You are not the owner of this JuntinPlay"));
            
            input.Adapt(juntin);

            await _juntinPlayRepository.DeleteById(input.Id);

            return BasicResult.Success();
        }
        catch (Exception ex)
        {
            return BasicResult.Failure(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    
    
    
}