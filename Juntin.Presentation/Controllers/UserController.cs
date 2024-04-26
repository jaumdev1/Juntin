using System.Net;
using Domain.Common;
using Domain.Dtos.User;
using Juntin.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly ICreateUserUseCase _createUserUseCase;

    public UserController(ICreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<BasicResult>> Create([FromBody] UserDto userDto)
    {
        var result = await _createUserUseCase.Execute(userDto);

        return ResponseBase(HttpStatusCode.OK, result, "Success!");
    }
}