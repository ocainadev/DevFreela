using DevFreela.Application.Models;
using DevFreela.Core.Repositorys;
using DevFreela.Infrastructure.Auth;
using MediatR;

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
        var user = await _repository.LoginAsync(request.Email, hash);
        if (user is null)
             return ResultViewModel<LoginViewModel>.Error("Email or password is incorrect");
        
        var token = _authService.GenerateToken(user.Email, user.Role);
        var viewModel = new LoginViewModel(token);
        return ResultViewModel<LoginViewModel>.Success(viewModel); 
    }
}
