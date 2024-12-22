using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace DevFreela.Application.Commands.UserCommands;

public class CreateUserCommand : IRequest<ResultViewModel<int>>
{
    public string FullName { get;  set; }
    public string Email { get;  set; }
    public DateTime BirthDate { get;  set; }

    public User ToEntity()
        => new (FullName, Email, BirthDate);
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    private readonly IUserRepository _repository;
    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();
        await _repository.AddAsync(user);
        
        return ResultViewModel<int>.Success(user.Id);
    }
}
