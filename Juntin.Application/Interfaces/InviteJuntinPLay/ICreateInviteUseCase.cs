using Domain.Common;
using Domain.Dtos.InviteJuntinPlay;
using Domain.Dtos.JuntinPlay;

namespace Juntin.Application.Interfaces.InviteJuntinPLay;

public interface ICreateInviteJuntinPlayUseCase : IUseCaseBase<CreateInviteJuntinPlayDto, BasicResult<string>>
{
    
}