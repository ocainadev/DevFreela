using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using MediatR;

namespace DevFreela.Application.Commands;

public class CompleteProjectCommand : IRequest<ResultViewModel>
{
    public CompleteProjectCommand(int id)
        => Id = id;
    public int Id { get; set; }
}

public class CompleteProjectHandler :IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    public CompleteProjectHandler(IProjectRepository repository, IMediator mediator)
    {
        _repository = repository;
    }
    private readonly IProjectRepository _repository;
    
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        if (project == null) return ResultViewModel.Error("Projeto nao existe");
        
        project.Complete();
        await _repository.UpdateAsync(project);
        return ResultViewModel.Success();
    }
}
