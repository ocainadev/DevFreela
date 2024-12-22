using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
{
    
}

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    public GetAllProjectsQueryHandler(IProjectRepository repository)
        => _repository = repository;
    private readonly IProjectRepository _repository; 
    
    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetAllAsync();
        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
}