using System.Net;
using Domain.Common;
using Domain.Dtos.JuntinMovie;
using Juntin.Application.Interfaces;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Middlewares.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
[UseSessionMiddleware]
public class JuntinMovieController : BaseController
{
    private readonly ICreateJuntinMovieUseCase _createJuntinMovieUseCase;
    private readonly IGetJuntinMovieUseCase _getJuntinMovieUseCase;
    private readonly IGetHistoricJuntinMovie _getHistoricJuntinMovie;
    private readonly IUpdateJuntinMovieUseCase _updateJuntinMovieUseCase;
    private readonly IDeleteJuntinMovieUseCase _deleteJuntinMovieUseCase;
    public JuntinMovieController(ICreateJuntinMovieUseCase createJuntinMovieUseCase,
        IGetJuntinMovieUseCase getJuntinMovieUseCase,
        IUpdateJuntinMovieUseCase updateJuntinMovieUseCase,
        IDeleteJuntinMovieUseCase deleteJuntinMovieUseCase,
        IGetHistoricJuntinMovie getHistoricJuntinMovie
        
        )
    {
        _createJuntinMovieUseCase = createJuntinMovieUseCase;
        _getJuntinMovieUseCase = getJuntinMovieUseCase;
        _updateJuntinMovieUseCase = updateJuntinMovieUseCase;
        _deleteJuntinMovieUseCase = deleteJuntinMovieUseCase;
        _getHistoricJuntinMovie = getHistoricJuntinMovie;
    }


    [HttpPost]
    public async Task<ActionResult<BasicResult>> Create([FromBody] JuntinMovieDto juntinMovieDto)
    {
        var result = await _createJuntinMovieUseCase.Execute(juntinMovieDto);

        return ResponseBase(HttpStatusCode.OK, result);
    }

    [HttpGet]
    public async Task<ActionResult<BasicResult>> Get([FromQuery] GetJuntinMovieDto getJuntinMovieDto)
    {
        var result = await _getJuntinMovieUseCase.Execute(getJuntinMovieDto);

        return ResponseBase(HttpStatusCode.OK, result);
    }
    [HttpGet("historic")]
    public async Task<ActionResult<BasicResult>> GetHistoric([FromQuery] GetJuntinMovieDto getJuntinMovieDto)
    {
        var result = await _getHistoricJuntinMovie.Execute(getJuntinMovieDto);

        return ResponseBase(HttpStatusCode.OK, result);
    }
    [HttpPut]
    public async Task<ActionResult<BasicResult>> Update([FromBody] UpdateJuntinMovieDto updateJuntinMovieDto)
    {
        var result = await _updateJuntinMovieUseCase.Execute(updateJuntinMovieDto);

        return ResponseBase(HttpStatusCode.OK, result, "Success!");;
    }

    [HttpDelete]
    public async Task<ActionResult<BasicResult>> Delete([FromBody] DeleteJuntinMovieDto deleteJuntinMovieDto)
    {
        var result = await _deleteJuntinMovieUseCase.Execute(deleteJuntinMovieDto);
        return ResponseBase(HttpStatusCode.OK, result, "Success!");
    }
    
}