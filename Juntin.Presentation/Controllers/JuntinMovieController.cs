using System.Net;
using Domain.Common;
using Domain.Dtos.JuntinMovie;
using Domain.Dtos.JuntinPlay;
using Juntin.Application.Interfaces;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Middlewares.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;
[Route("api/[controller]")]
public class JuntinMovieController : BaseController
{
    private readonly ICreateJuntinMovieUseCase _createJuntinMovieUseCase;
    
    public JuntinMovieController(ICreateJuntinMovieUseCase createJuntinMovieUseCase)
    {
        _createJuntinMovieUseCase = createJuntinMovieUseCase;
    }
   

    [HttpPost]
    public async Task<ActionResult<BasicResult>> Create([FromBody] JuntinMovieDto  juntinMovieDto )
    {
        var result = await _createJuntinMovieUseCase.Execute(juntinMovieDto);

        return ResponseBase<Guid>(HttpStatusCode.OK, result);
    }
}