using System.Net;
using Domain.Common;
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
        BasicResult<T> basicResult,
        string responseMessage,
        HttpContext context,
        string cookieName = "Authorization",
        int cookieExpirationMinutes = 30,
        HttpStatusCode statusCodeError = HttpStatusCode.NotFound)
    {
        if (basicResult.IsFailure)
            return StatusCode(basicResult.Error.StatusCode, new BaseResponse<Error>(basicResult.Error));

        // Se o resultado básico for um sucesso, retorne um StatusCode com a mensagem de resposta
        StatusCode((int)statusCode, responseMessage);

        // Se o resultado da autenticação for um sucesso, armazene-o em um cookie
        context.Response.Cookies.Append(cookieName, basicResult.Value.ToString(), new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(cookieExpirationMinutes)
        });

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