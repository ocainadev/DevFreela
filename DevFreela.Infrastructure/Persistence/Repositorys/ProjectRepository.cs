using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositorys;

public class ProjectRepository : IProjectRepository
{
    public ProjectRepository(DevFreelaDbContext context)
    {
        _context = context;
    }
    private readonly DevFreelaDbContext _context;
    
    public async Task<List<Project>> GetAllAsync()
    {
        var projects = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
        return projects;
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        return project;
    }

    public async Task<Project> GetDetailsByIdAsync(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Include(p => p.Comments)
            .SingleOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        return project;
    }

    public async Task<int> AddAsync(Project model)
    {
        await _context.Projects.AddAsync(model);
        await _context.SaveChangesAsync();

        return model.Id;
    }
    
    public async Task UpdateAsync(Project model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task AddCommentAsync(ProjectComment model)
    {
        await _context.ProjectComments.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistByIdAsync(int id)
    {
        return await _context.Projects.AnyAsync(e => e.Id == id);
    }
}