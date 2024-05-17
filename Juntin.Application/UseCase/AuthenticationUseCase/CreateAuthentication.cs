using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.Auth;
using Domain.Dtos.Auth.Validator;
using Juntin.Application.Interfaces;
using Juntin.Application.Security;

namespace Juntin.Application.UseCase.AuthenticationUseCase;

public class CreateAuthentication : ICreateAuthenticationUseCase
{
    private readonly IUserRepository _userRepository;
    private SessionManager sessionManager { get; }
    private readonly AuthenticationValidator _authenticationValidator;
    public CreateAuthentication(SessionManager _sessionManager, IUserRepository userRepository)
    {
        sessionManager = _sessionManager;
        _userRepository = userRepository;
        _authenticationValidator = new AuthenticationValidator();
    }

 

    public async Task<BasicResult<AuthTokenDto>> Execute(AuthenticationDto input)
    {
        try
        {
                
            var validationResult = _authenticationValidator.Validate(input);
            if (!validationResult.IsValid)
                return BasicResult.Failure<AuthTokenDto>(new Error(HttpStatusCode.BadRequest, validationResult.Errors.First().ErrorMessage));
            
            var user = await _userRepository.GetByUsername(input.Username);
            
      
            if (user == null || !BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
                return BasicResult.Failure<AuthTokenDto>(new Error(HttpStatusCode.Unauthorized, "Invalid credentials"));
            // if(!user.ConfirmedEmail)
            //     return BasicResult.Failure<AuthTokenDto>(new Error(HttpStatusCode.Unauthorized, "Unconfirmed Email address, please confirm your e-mail address"));
            
            var sessionId = sessionManager.CreateSession(user.Id, user.Username, TimeSpan.FromMinutes(1));
            
            var refreshToken = sessionManager.GenerateRefreshToken();
            sessionManager.SaveRefreshToken(user.Id, refreshToken);
            var auth = new AuthTokenDto(sessionId, refreshToken);
            
            return auth;
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<AuthTokenDto>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}