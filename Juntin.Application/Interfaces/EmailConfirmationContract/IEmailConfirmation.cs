using Domain.Common;
using Domain.Dtos.User;
using Domain.Entities;

namespace Juntin.Application.Interfaces.EmailConfirmationContract;


public interface IEmailConfirmation: IUseCaseBase<User, BasicResult<bool>>
{
    
}