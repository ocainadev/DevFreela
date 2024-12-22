using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DevFreela.Application.Queries.UserQueries;

public class GetUserByIdQuery(int id) : IRequest<ResultViewModel<UserViewModel>>
{
    public int Id { get; set; } = id;
}

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    public GetUserByIdHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    private readonly IUserRepository _repository;
    
    public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);
        var model = UserViewModel.FromEntity(user);
        return ResultViewModel<UserViewModel>.Success(model);
    }
}