using System.Net;
using Domain.Common;
using Domain.Dtos;
using Domain.Dtos.Auth;
using Juntin.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
public class RefreshAuthenticationController  : BaseController
{
    private readonly IRefreshAuthenticationUseCase _createRefreshAuthentication;

    public RefreshAuthenticationController(IRefreshAuthenticationUseCase refreshAuthenticationUseCase)
    {
        _createRefreshAuthentication = refreshAuthenticationUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<BasicResult<AuthTokenDto>>> Create()
    {
        var result = await  _createRefreshAuthentication.Execute();

        return CookieAuthResponseBase<AuthTokenDto>(HttpStatusCode.OK, result, "Sucess!", HttpContext);
    }
    
}