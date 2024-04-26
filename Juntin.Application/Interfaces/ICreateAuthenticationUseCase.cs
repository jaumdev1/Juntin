using Domain.Common;
using Domain.Dtos;

namespace Juntin.Application.Interfaces;

public interface ICreateAuthenticationUseCase : IUseCaseBase<AuthenticationDto, BasicResult<string>>
{
}