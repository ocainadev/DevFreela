using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositorys;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<int> AddAsync(User user);
}