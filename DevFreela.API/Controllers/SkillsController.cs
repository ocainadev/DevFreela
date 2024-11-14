using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

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
    public IActionResult GetAll(string search = "",int page = 0, int size = 10)
    {
        var skills = _context.Skills
            .Where(s => !s.IsDeleted && (search == "" || s.Description.Contains(search)))
            .Skip(page * size)
            .Take(size)
            .ToList();
        
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