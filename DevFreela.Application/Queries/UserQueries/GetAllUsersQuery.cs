using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Identity.Client;

namespace DevFreela.Application.Queries.UserQueries;

public class GetAllUsersQuery : IRequest<ResultViewModel<List<UserViewModel>>>
{
    
}

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserViewModel>>>
{
    public GetAllUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    private readonly IUserRepository _repository;


    public async Task<ResultViewModel<List<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync();
        var model = users.Select(UserViewModel.FromEntity).ToList();
        return ResultViewModel<List<UserViewModel>>.Success(model);
    }
}