using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos.InviteJuntinPlay;
using Domain.Entities;
using Juntin.Application.Interfaces.InviteJuntinPLay;
using Juntin.Application.Security;

namespace Juntin.Application.UseCase.InviteUseCase;

public class InviteJuntinPlayUseCase : IInviteJuntinPlayUseCase
{
    private readonly SessionManager _sessionManager;
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    private readonly IInviteJuntinPlayRepository _inviteJuntinPlayRepository; 
    private readonly IUserJuntinRepository _userJuntinRepository;
    public InviteJuntinPlayUseCase(SessionManager sessionManager, IJuntinPlayRepository juntinPlayRepository, 
            IInviteJuntinPlayRepository inviteJuntinPlayRepository, IUserJuntinRepository userJuntinRepository)
    {
        _sessionManager = sessionManager;
        _juntinPlayRepository = juntinPlayRepository;
        _inviteJuntinPlayRepository = inviteJuntinPlayRepository;
        _userJuntinRepository = userJuntinRepository;
    }


    public async Task<BasicResult> Execute(InviteJuntinPlayUseCaseDto input)
    {

        try
        {
            var invite = await _inviteJuntinPlayRepository.GetByCode(input.Code);

            if(invite == null)
                return BasicResult.Failure(new Error(HttpStatusCode.NotFound, "Invite not found"));

            var ownerId = await _sessionManager.GetUserLoggedId();
            
            if(ownerId == null)
                return BasicResult.Failure(new Error(HttpStatusCode.Unauthorized, "User not logged"));

            if (invite.ExpireAt < DateTime.Now)
                return BasicResult.Failure(new Error(HttpStatusCode.BadRequest, "Invite expired"));
            
            
            if(input.IsAccepted)
            {
                var juntinPlay = await _juntinPlayRepository.GetById(invite.JuntinPlayId);
                if(juntinPlay == null)
                    return BasicResult.Failure(new Error(HttpStatusCode.NotFound, "JuntinPlay not found"));


                var userJuntin =  await _userJuntinRepository.GetByJuntinPlayAndUser(juntinPlay.Id, ownerId);
                if(userJuntin != null)
                  return BasicResult.Failure(new Error(HttpStatusCode.BadRequest, "User is already in this Juntin"));
                _userJuntinRepository.Add(new UserJuntin()
                {
                    Id = Guid.NewGuid(),
                    JuntinPlayId = juntinPlay.Id,
                    UserId = ownerId
                });
                await _juntinPlayRepository.Update(juntinPlay);
            }
            return BasicResult.Success();
        }
        catch (Exception ex)
        {
            return BasicResult.Failure(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}