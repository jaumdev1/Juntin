using System.Net;
using Domain.Common;
using Domain.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

/// <summary>
///     Classe base para ser herdadas em todos os controller, para termos agilidades de codigos e melhoria em
///     funcionalidades
/// </summary>
[ApiController]
[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public abstract class BaseController : Controller
{
    /// <summary>
    ///     Este método serve para que evitemos repetir muito o código, entao fazemos a chamada dele e ele decidira qual
    ///     retorno deve voltar
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="basicResult"></param>
    /// <param name="responseMessage"></param>
    /// <param name="statusCodeError">Opcional HttpStatusCode.NotFound</param>
    /// <returns>ActionResult</returns>
    protected ActionResult ResponseBase(
        HttpStatusCode statusCode,
        BasicResult basicResult,
        string responseMessage,
        HttpStatusCode statusCodeError = HttpStatusCode.NotFound)
    {
        if (basicResult.IsFailure)
            return StatusCode(basicResult.Error.StatusCode, new BaseResponse<Error>(basicResult.Error));

        return StatusCode((int)statusCode, responseMessage);
    }

    protected ActionResult CookieAuthResponseBase<T>(
        HttpStatusCode statusCode,
        BasicResult<AuthTokenDto> basicResult,
        string responseMessage,
        HttpContext context,
        int cookieExpirationMinutes = 30,
        HttpStatusCode statusCodeError = HttpStatusCode.NotFound)
    {
        if (basicResult.IsFailure)
            return StatusCode(basicResult.Error.StatusCode, new BaseResponse<Error>(basicResult.Error));

        StatusCode((int)statusCode, responseMessage);
        string cookieName = "Authorization";
        string refreshCookieName = "RefreshAuthorization";
        
            
        var authCookieValue = basicResult.Value.AuthToken;
        var refreshTokenValue = basicResult.Value.RefreshToken;
        
        context.Response.Cookies.Append(cookieName, authCookieValue, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(cookieExpirationMinutes)
        });
        context.Response.Cookies.Append(refreshCookieName, refreshTokenValue, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(cookieExpirationMinutes)
        });
      
        context.Response.Headers.Authorization = authCookieValue;
        context.Response.Headers.Add("RefreshAuthorization", refreshTokenValue);
        
        return StatusCode((int)statusCode, responseMessage);
    }

    /// <summary>
    ///     Este método serve para que evitemos repetir muito o código, entao fazemos a chamada dele e ele decidira qual
    ///     retorno deve voltar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statusCode"></param>
    /// <param name="basicResult"></param>
    /// <param name="statusCodeError">Opcional HttpStatusCode.NotFound</param>
    /// <returns>ActionResult</returns>
    protected ActionResult ResponseBase<T>(
        HttpStatusCode statusCode,
        BasicResult<T> basicResult,
        HttpStatusCode statusCodeError = HttpStatusCode.NotFound)
    {
        if (basicResult.IsFailure)
        {
            basicResult.Error.StatusCode = (int)statusCodeError;
            return StatusCode(basicResult.Error.StatusCode, basicResult.Error);
        }

        return StatusCode((int)statusCode, new BaseResponse<T>(basicResult.Value));
    }
}