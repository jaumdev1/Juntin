using System.Net;
using Domain.Common;
using Domain.Dtos.Movie;
using Juntin.Application.Interfaces.Movie;
using Juntin.Middlewares.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
[UseSessionMiddleware]
public class MovieController : BaseController
{
    private readonly IMovieUseCase _movieUseCase;

    public MovieController
        (IMovieUseCase movieUseCase)
    {
        _movieUseCase = movieUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<BasicResult>> Get([FromQuery] MovieDto movieDto)
    {
        var result = await _movieUseCase.Execute(movieDto);

        return ResponseBase(HttpStatusCode.OK, result);
    }
}