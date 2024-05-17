using System.Net;
using Domain.Common;
using Domain.Dtos;
using Domain.Dtos.Auth;
using Juntin.Application.Interfaces;
using Juntin.Application.Security;

namespace Juntin.Application.UseCase.AuthenticationUseCase;

public class RefreshAuthentication : IRefreshAuthenticationUseCase
{
    private SessionManager sessionManager { get; }
    public RefreshAuthentication(SessionManager _sessionManager)
    {
        sessionManager = _sessionManager;
    }
    public async Task<BasicResult<AuthTokenDto>> Execute()
    {
        try{
        var session = sessionManager.RefreshToken();
        if (session == null)
            return BasicResult.Failure<AuthTokenDto>(new Error(HttpStatusCode.Unauthorized, "Invalid Session."));
        
        return await session;
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<AuthTokenDto>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}