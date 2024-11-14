using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/skills")]

public class SkillsController : ControllerBase
{
    private readonly DevFreelaDbContext _context;
    public SkillsController(DevFreelaDbContext context)
    {
        _context = context;
    }
    
    // GET api/skills
    [HttpGet]
    public IActionResult GetAll()
    {
        var skills = _context.Skills.ToList();
        
        var model = skills.Select(SkillViewModel.FromEntity).ToList();
        return Ok(model);
    }
    
    // POST api/skills
    [HttpPost]
    public IActionResult Post(CreateSkillInputModel model)
    {
        var skill = new Skill(model.Description);
        
        _context.Skills.Add(skill);
        _context.SaveChanges();
        return NoContent();
    }
}