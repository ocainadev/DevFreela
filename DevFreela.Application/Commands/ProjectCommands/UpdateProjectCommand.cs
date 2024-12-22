using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using MediatR;

namespace DevFreela.Application.Commands;

public class UpdateProjectCommand: IRequest<ResultViewModel>
{
    public UpdateProjectCommand(int id)
         => IdProject = id;
    
    public int IdProject { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float TotalCost { get; set; }
}

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    public UpdateProjectHandler(IProjectRepository repository)
        => _repository = repository;
    private readonly IProjectRepository _repository;
    
    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.IdProject);
        if (project is null) ResultViewModel.Error("Projeto nao existe");

        await _repository.UpdateAsync(project);
        return ResultViewModel.Success();
    }
}