using Domain.Common;
using Domain.Dtos;
using Domain.Dtos.Auth;

namespace Juntin.Application.Interfaces;

public interface ICreateAuthenticationUseCase : IUseCaseBase<AuthenticationDto, BasicResult<AuthTokenDto>>
{
}