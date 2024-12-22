using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using MediatR;

namespace DevFreela.Application.Commands;

public class DeleteProjectCommand : IRequest<ResultViewModel>
{
    public DeleteProjectCommand(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    public DeleteProjectHandler(IProjectRepository repository)
    {
        _repository = repository;
    }
    private readonly IProjectRepository _repository;
    
    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        if (project is null) return ResultViewModel.Error("Projeto nao existe");
        
        project.SetIsDeleted();
        await _repository.UpdateAsync(project);
        return ResultViewModel.Success();
    }
}