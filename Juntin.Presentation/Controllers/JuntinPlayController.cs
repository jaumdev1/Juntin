using System.Net;
using Domain.Common;
using Domain.Dtos.JuntinPlay;
using Juntin.Application.Interfaces;
using Juntin.Middlewares.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
[UseSessionMiddleware]
public class JuntinPlayController : BaseController
{
    // GET
    private readonly ICreateJuntinPlayUseCase _createJuntinPlayUseCase;
    private readonly IUpdateJuntinPlayUseCase  _updateJuntinPlayUseCase;
    private readonly IDeleteJuntinPlayUseCase  _deleteJuntinPlayUseCase;
    private readonly IGetJuntinPlayUseCase _getJuntinPlayUseCase;
    

    public JuntinPlayController
        (ICreateJuntinPlayUseCase createJuntinPlayUseCase,
        IUpdateJuntinPlayUseCase updateJuntinPlayUseCase, 
        IDeleteJuntinPlayUseCase  deleteJuntinPlayUseCase,
        IGetJuntinPlayUseCase getJuntinPlayUseCase)
    {
        _createJuntinPlayUseCase = createJuntinPlayUseCase;
        _updateJuntinPlayUseCase = updateJuntinPlayUseCase;
        _deleteJuntinPlayUseCase = deleteJuntinPlayUseCase;
        _getJuntinPlayUseCase = getJuntinPlayUseCase;
    }
    [HttpPost]
    public async Task<ActionResult<BasicResult>> Create([FromBody] JuntinPlayDto juntinPlayDto)
    {
        var result = await _createJuntinPlayUseCase.Execute(juntinPlayDto);

        return ResponseBase<Guid>(HttpStatusCode.OK, result);
    }
    [HttpPut]
    public async Task<ActionResult<BasicResult>> Update([FromBody] UpdateJuntinPlayDto juntinPlayDto)
    {
        var result = await _updateJuntinPlayUseCase.Execute(juntinPlayDto);

        return ResponseBase(HttpStatusCode.OK, result, "Success!");
    }
    [HttpDelete]
    public async Task<ActionResult<BasicResult>> Delete([FromBody] DeleteJuntinPlayDto deleteJuntinPlayDto)
    {
        var result = await _deleteJuntinPlayUseCase.Execute(deleteJuntinPlayDto);

        return ResponseBase(HttpStatusCode.OK, result, "Success!");
    }
    
    [HttpGet]
    public async Task<ActionResult<BasicResult>> Get([FromBody] GetJuntinPlayDto juntinPlayDto)
    {
        var result = await _getJuntinPlayUseCase.Execute(juntinPlayDto);
        
        return ResponseBase<List<JuntinPlayResult>>(HttpStatusCode.OK,result);
    }
 
}