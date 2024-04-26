using System.Net;
using Domain.Common;
using Domain.Dtos;
using Juntin.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
public class AuthenticationController : BaseController

{
    private readonly ICreateAuthenticationUseCase _createAuthentication;

    public AuthenticationController(ICreateAuthenticationUseCase authenticationUseCase)
    {
        _createAuthentication = authenticationUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<BasicResult<string>>> Create([FromBody] AuthenticationDto authenticationDto)
    {
        var result = await _createAuthentication.Execute(authenticationDto);

        return CookieAuthResponseBase(HttpStatusCode.OK, result, "Success!", HttpContext);
    }
}