using DevFreela.Application.Commands;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/projects")]
[Authorize]

public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET api/projects
    [HttpGet]
    [Authorize(Roles = "freelancer, client")]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id));
        if (!result.IsSuccess) BadRequest(result.Message);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Post(CreateProjectCommand model)
    {
        var result = await _mediator.Send(model);
        if (!result.IsSuccess) BadRequest(result.Message);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateProjectCommand model)
    {
        var result = await _mediator.Send(model);
        if (!result.IsSuccess) BadRequest(result.Message);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id));
        if (!result.IsSuccess) BadRequest(result.Message);
        return NoContent();
    }

    [HttpPut("{id:int}/start")]
    public async Task<IActionResult> Start(int id)
    {
        var result = await _mediator.Send(new StartProjectCommand(id));
        if (!result.IsSuccess) BadRequest(result.Message);
        return NoContent();
    }
    
    [HttpPut("{id:int}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var result = await _mediator.Send(new CompleteProjectCommand(id));
        if (!result.IsSuccess) BadRequest(result.Message);
        return NoContent();
    }
    
    [HttpPost("{id:int}/comments")]
    public async Task<IActionResult> PostComment(int id, PostCommentCommand model)
    {
        var result = await _mediator.Send(model);
        if (!result.IsSuccess) BadRequest(result.Message);
        return NoContent();
    }
}
