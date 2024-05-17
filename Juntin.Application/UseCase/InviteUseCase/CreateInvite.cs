using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.InviteJuntinPlay;
using Domain.Dtos.InviteJuntinPlay.Validator;
using Domain.Dtos.JuntinMovie.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces.InviteJuntinPLay;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.InviteUseCase;

public class CreateInvite : ICreateInviteJuntinPlayUseCase
{
    private readonly CreateInviteJuntinPlayDtoValidator _createInviteJuntinDtoValidator;
    private readonly SessionManager _sessionManager;
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly IInviteJuntinPlayRepository _inviteJuntinPlayRepository; 
    public CreateInvite(SessionManager sessionManager, IJuntinPlayRepository juntinPlayRepository, IInviteJuntinPlayRepository inviteJuntinPlayRepository)
    {_sessionManager = sessionManager;
             _juntinPlayRepository = juntinPlayRepository;
             _createInviteJuntinDtoValidator = new CreateInviteJuntinPlayDtoValidator();
             _inviteJuntinPlayRepository = inviteJuntinPlayRepository;
        
    }
  
    
    public async Task<BasicResult<string>> Execute(CreateInviteJuntinPlayDto input)
    {
        try
        {
            var validations = await _createInviteJuntinDtoValidator.ValidateAsync(input);
            if (!validations.IsValid) return DefaultValidator<string>.ReturnError(validations);

            var ownerId = await _sessionManager.GetUserLoggedId();

            var juntinPlay = await _juntinPlayRepository.GetById(input.JuntinPlayId);

            if (juntinPlay == null)
                return BasicResult.Failure<string>(new Error(HttpStatusCode.NotFound, "JuntinPlay not found"));

            var code = Guid.NewGuid().ToString();
            var invite = new InviteJuntinPlay()
            {
                Id = Guid.NewGuid(),
                JuntinPlayId = juntinPlay.Id,
                ExpireAt = DateTime.UtcNow.AddDays(3),
                Link = $"juntinplay://invite/{code}",
                Code = code,
            };

            await _inviteJuntinPlayRepository.Add(invite);

            ResultInviteJuntinPlayDto result = invite.Adapt<ResultInviteJuntinPlayDto>();
            
            return result.Link;
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<string>( new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}