using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using DevFreela.Infrastructure.Auth;
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
    public string Password { get;  set; }
    public string Role { get;  set; }

    public User ToEntity(string hash)
        => new (FullName, Email, BirthDate,hash, Role);
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    public CreateUserHandler(IUserRepository repository, IAuthService authsevice)
    {
        _repository = repository;
        _authService = authsevice;
    }
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;
    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hash = _authService.ComputeHash(request.Password);
        var user = request.ToEntity(hash);
        await _repository.AddAsync(user);
        
        return ResultViewModel<int>.Success(user.Id);
    }
}
