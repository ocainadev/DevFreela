using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositorys;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace DevFreela.Application.Commands.UserCommands;

public class LoginUserCommand : IRequest<ResultViewModel<LoginViewModel>>
{
    public string Email { get; set; }
    public string Password { get; set; }

}

public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginViewModel>>
{
    public LoginUserHandler(IUserRepository repository, IAuthService authsevice)
    {
        _repository = repository;
        _authService = authsevice;
    }
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;
    public async Task<ResultViewModel<LoginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var hash = _authService.ComputeHash(request.Password);
        var user = await _repository.Login(request.Email, hash);

        var token = _authService.GenerateToken(user.Email, user.Role);
        var viewModel = new LoginViewModel(token);
        return ResultViewModel<LoginViewModel>.Success(viewModel); 
    }
}
