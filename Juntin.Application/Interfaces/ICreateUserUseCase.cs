using Domain.Common;
using Domain.Dtos.User;

namespace Juntin.Application.Interfaces;

public interface ICreateUserUseCase : IUseCaseBase<UserDto, BasicResult>
{
  
}