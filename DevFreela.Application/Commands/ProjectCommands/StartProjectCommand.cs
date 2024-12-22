using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands;

public class StartProjectCommand : IRequest<ResultViewModel>
{
    public StartProjectCommand(int id)
        => Id = id;
    public int Id { get; set; } 
}

public class StartProjectHandler :IRequestHandler<StartProjectCommand, ResultViewModel>
{
    public StartProjectHandler(IProjectRepository repository)
        => _repository = repository;
    
    private readonly IProjectRepository _repository;
    
    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        if (project is null) return ResultViewModel.Error("Projeto nao existe");
        
        project.Start();
        await _repository.UpdateAsync(project);
        return ResultViewModel.Success();
    }
}