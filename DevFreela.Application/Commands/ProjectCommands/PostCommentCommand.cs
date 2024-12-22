using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using MediatR;

namespace DevFreela.Application.Commands;

public class PostCommentCommand : IRequest<ResultViewModel>
{
    public string Content { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }
}

public class PostCommentHandler : IRequestHandler<PostCommentCommand, ResultViewModel>
{
    public PostCommentHandler(IProjectRepository repository)
        => _repository = repository;
    private readonly IProjectRepository _repository;
    
    public async Task<ResultViewModel> Handle(PostCommentCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistByIdAsync(request.IdProject);
        if (!exists) return ResultViewModel.Error("Projeto nao existe");
        
        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
        await _repository.AddCommentAsync(comment);
        return ResultViewModel.Success();
    }
}