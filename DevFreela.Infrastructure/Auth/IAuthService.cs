namespace DevFreela.Infrastructure.Auth;

public interface IAuthService
{
    string ComputeHash(string password);
    string GenerateToken(string email, string role);
}