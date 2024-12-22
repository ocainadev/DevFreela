using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery(int id) : IRequest<ResultViewModel<ProjectViewModel>>
{
    public int Id { get; set; } = id;
}

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    public GetProjectByIdHandler(IProjectRepository repository)
        => _repository = repository;
    private readonly IProjectRepository _repository; 
    
    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetDetailsByIdAsync(request.Id);
        
        if (project == null) return ResultViewModel<ProjectViewModel>.Error("Projeto nao existe");
        var model = ProjectViewModel.FromEntity(project);
        return ResultViewModel<ProjectViewModel>.Success(model);
    }
}