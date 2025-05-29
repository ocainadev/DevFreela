using DevFreela.Application.Commands.UserCommands;
using DevFreela.Application.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController : ControllerBase
{
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    private readonly IMediator _mediator;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        if (!result.IsSuccess) BadRequest(result.Message);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        if (!result.IsSuccess) BadRequest(result.Message);
        return Ok(result);
    }
    
    // POST api/users
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(CreateUserCommand model)
    {
        var result = await _mediator.Send(model);
        if (!result.IsSuccess) BadRequest(result.Message);
        return NoContent();
    }
    

    [HttpPut("{id:int}/profile-picture")]
    public async Task<IActionResult> PostProfilePicture(IFormFile file, int id)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";
        
        return Ok(description);
    }

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserCommand model)
    {
        var result = await _mediator.Send(model);
        if (!result.IsSuccess) BadRequest(result.Message);
        return Ok(result);
    }
    
}