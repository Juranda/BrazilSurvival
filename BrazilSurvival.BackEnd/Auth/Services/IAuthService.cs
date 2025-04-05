namespace BrazilSurvival.BackEnd.Auth.Services;

using BrazilSurvival.BackEnd.Auth.Models;
using BrazilSurvival.BackEnd.Errors;

public interface IAuthService
{
    Task<Result<string>> Register(RegisterRequest request);
    Task<Result<string>> Login(LoginRequest request);
}
