using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext : DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options) { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
            });
        builder
            .Entity<Project>(e =>
            { 
                e.HasKey(p => p.Id);
                
                e.HasOne(p => p.Freelancer)
                    .WithMany(f => f.FreelanceProjects)
                    .HasForeignKey(f => f.IdFreelancer)
                    .OnDelete(DeleteBehavior.Restrict);
                
                e.HasOne(p => p.Client)
                    .WithMany(f => f.OwnedProjects)
                    .HasForeignKey(f => f.IdClient)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        base.OnModelCreating(builder);
    }
}
