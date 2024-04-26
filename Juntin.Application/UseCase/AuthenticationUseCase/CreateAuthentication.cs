using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Juntin.Application.Interfaces;
using Juntin.Application.Security;

namespace Juntin.Application.UseCase.AuthenticationUseCase;

public class CreateAuthentication : ICreateAuthenticationUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateAuthentication(SessionManager _sessionManager, IUserRepository userRepository)
    {
        sessionManager = _sessionManager;
        _userRepository = userRepository;
    }

    private SessionManager sessionManager { get; }

    public async Task<BasicResult<string>> Execute(AuthenticationDto input)
    {
        try
        {
            var user  = await _userRepository.GetByUsername(input.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
                return BasicResult.Failure<string>(new Error(HttpStatusCode.Unauthorized, "Credenciais inválidas"));
                    
            var sessionId = sessionManager.CreateSession(user.Id,user.Username, TimeSpan.FromMinutes(30));

            return sessionId;
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<string>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}