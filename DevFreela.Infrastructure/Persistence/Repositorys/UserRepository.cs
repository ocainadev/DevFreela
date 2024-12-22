using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositorys;

public class UserRepository : IUserRepository
{
    public UserRepository(DevFreelaDbContext context)
    {
        _context = context;
    }
    private readonly DevFreelaDbContext _context;

    public Task<List<User>> GetAllAsync()
    {
        return _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<int> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }
}