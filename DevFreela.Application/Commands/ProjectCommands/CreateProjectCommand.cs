using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using MediatR;

namespace DevFreela.Application.Commands;

public class CreateProjectCommand : IRequest<ResultViewModel<int>>
{ 
    public string Title { get; set; }
    public string Description { get; set; }
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public float TotalCost { get; set; }

    public Project ToEntity()
        => new (Title, Description, IdClient, IdFreelancer, TotalCost);
}

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ResultViewModel<int>>
{
    public CreateProjectHandler(IProjectRepository repository)
    {
       _repository = repository;
    }
    
    private readonly IProjectRepository _repository;
    
    public async Task<ResultViewModel<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();
        await _repository.AddAsync(project);
        
        return ResultViewModel<int>.Success(project.Id);
    }
}