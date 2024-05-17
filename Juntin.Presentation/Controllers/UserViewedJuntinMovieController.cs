using System.Net;
using Domain.Common;
using Domain.Dtos.JuntinMovie.Validator;
using Domain.Dtos.UserViewedJuntinMovie;
using Juntin.Application.Interfaces.UserViewedJuntinMovie;
using Juntin.Middlewares.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
[UseSessionMiddleware]
public class UserViewedJuntinMovieController : BaseController
{
    public readonly ICreateUserViewedJuntinMovie _createUserViewedJuntinMovie;

    public UserViewedJuntinMovieController(ICreateUserViewedJuntinMovie userViewedJuntinMovieRepository)
    {
        _createUserViewedJuntinMovie = userViewedJuntinMovieRepository;
    }

    // GET
    [HttpPost]
    public async Task<ActionResult<BasicResult<ResultViwedJuntinMovieDto>>> Create([FromBody] CreateUserViewedJuntinMovieDto input)
    {
        var result = await _createUserViewedJuntinMovie.Execute(input);

        return ResponseBase(HttpStatusCode.OK, result);
    }
}