using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos.InviteJuntinPlay;
using Domain.Dtos.JuntinPlay;
using Domain.Dtos.JuntinPlay.Validator;
using Juntin.Application.Interfaces;
using Juntin.Application.Interfaces.InviteJuntinPLay;
using Juntin.Middlewares.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
[UseSessionMiddleware]
public class JuntinPlayController : BaseController
{
    // GET
    private readonly ICreateJuntinPlayUseCase _createJuntinPlayUseCase;
    private readonly IDeleteJuntinPlayUseCase _deleteJuntinPlayUseCase;
    private readonly IGetJuntinPlayUseCase _getJuntinPlayUseCase;
    private readonly IUpdateJuntinPlayUseCase _updateJuntinPlayUseCase;
    private readonly IGetOneJuntinPlayUseCase _getOneJuntinPlayUseCase;
    private readonly ICreateInviteJuntinPlayUseCase _createInviteJuntinPlayUseCase;
    private readonly IInviteJuntinPlayUseCase _inviteJuntinPlayUseCase;

    public JuntinPlayController
    (ICreateJuntinPlayUseCase createJuntinPlayUseCase,
        IUpdateJuntinPlayUseCase updateJuntinPlayUseCase,
        IDeleteJuntinPlayUseCase deleteJuntinPlayUseCase,
        IGetJuntinPlayUseCase getJuntinPlayUseCase, 
        IGetOneJuntinPlayUseCase getOneJuntinPlayUseCase,
        IInviteJuntinPlayUseCase inviteJuntinPlayUseCase,
        ICreateInviteJuntinPlayUseCase createInviteJuntinPlayUseCase)
    {
        _createJuntinPlayUseCase = createJuntinPlayUseCase;
        _updateJuntinPlayUseCase = updateJuntinPlayUseCase;
        _deleteJuntinPlayUseCase = deleteJuntinPlayUseCase;
        _getJuntinPlayUseCase = getJuntinPlayUseCase;
        _getOneJuntinPlayUseCase = getOneJuntinPlayUseCase;
        _createInviteJuntinPlayUseCase = createInviteJuntinPlayUseCase;
        _inviteJuntinPlayUseCase = inviteJuntinPlayUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<BasicResult>> Create([FromBody] JuntinPlayDto juntinPlayDto)
    {
        var result = await _createJuntinPlayUseCase.Execute(juntinPlayDto);

        return ResponseBase(HttpStatusCode.OK, result);
    }

    [HttpPut]
    public async Task<ActionResult<BasicResult>> Update([FromBody] UpdateJuntinPlayDto juntinPlayDto)
    {
        var result = await _updateJuntinPlayUseCase.Execute(juntinPlayDto);

        return ResponseBase(HttpStatusCode.OK, result, "Success!");
    }
    

    [HttpDelete("{id}")]
    public async Task<ActionResult<BasicResult>> Delete(Guid id)
    {
        var deleteJuntinPlayDto = new DeleteJuntinPlayDto { Id = id };
        var result = await _deleteJuntinPlayUseCase.Execute(deleteJuntinPlayDto);

        return ResponseBase(HttpStatusCode.OK, result, "Success!");
    }

    [HttpGet]
    public async Task<ActionResult<BasicResult>> Get([FromQuery] GetJuntinPlayDto juntinPlayDto)
    {
        var result = await _getJuntinPlayUseCase.Execute(juntinPlayDto);

        return ResponseBase(HttpStatusCode.OK, result);
    }

    [HttpPost("invite")]
    public async Task<ActionResult<BasicResult>> Invite([FromBody] CreateInviteJuntinPlayDto createInviteJuntinPlayDto)
    {
       var result = await _createInviteJuntinPlayUseCase.Execute(createInviteJuntinPlayDto);
       
       return ResponseBase(HttpStatusCode.OK, result);
    }
    [HttpPost("invite/accept")]
    public async Task<ActionResult<BasicResult>> Invite([FromBody] InviteJuntinPlayUseCaseDto inviteJuntinPlayDto)
    {
        var result = await _inviteJuntinPlayUseCase.Execute(inviteJuntinPlayDto);
       
        return ResponseBase(HttpStatusCode.OK, result, "Success!");
    }
        
    [HttpGet("{id}")]
    public async Task<ActionResult<BasicResult>> Get(Guid id)
    {
        
        var result = await _getOneJuntinPlayUseCase.Execute(new GetOneJuntinPlayDto(id));

        return ResponseBase(HttpStatusCode.OK, result);
    }
}