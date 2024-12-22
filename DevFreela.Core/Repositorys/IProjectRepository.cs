using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositorys;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project> GetByIdAsync(int id);
    Task<Project> GetDetailsByIdAsync(int id);
    
    Task<int> AddAsync(Project model);
    Task UpdateAsync(Project model);
    Task AddCommentAsync(ProjectComment model);
    Task<bool> ExistByIdAsync(int id);
}