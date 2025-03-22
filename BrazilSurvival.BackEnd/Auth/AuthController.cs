namespace BrazilSurvival.BackEnd.Auth;
using System.Security.Cryptography;
using System.Text;
using BrazilSurvival.BackEnd.Auth.Models;
using BrazilSurvival.BackEnd.Auth.Services;
using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        Result<string> result = await authService.Register(request);

        if (result.HasError)
        {
            return ErrorResponse.InvalidArgument(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Result<string> userResult = await authService.Login(request);

        if (userResult.HasError)
        {
            return BadRequest(userResult.Error);
        }

        return Ok(userResult.Value);
    }
}

public class AuthService : IAuthService
{
    private readonly UsersDbContext dbContext;
    private readonly JwtService jwtService;

    public AuthService(UsersDbContext dbContext, JwtService jwtService)
    {
        this.dbContext = dbContext;
        this.jwtService = jwtService;
    }

    public async Task<Result<string>> Register(RegisterRequest request)
    {
        if (request.Role != AuthorizationPolicies.ADMINISTRATOR && request.Role != AuthorizationPolicies.PLAYER)
        {
            return Error.InvalidArgument($"Invalid role. Only \"{AuthorizationPolicies.ADMINISTRATOR}\" or \"{AuthorizationPolicies.PLAYER}\" are valid");
        }

        var user = new User(
            Id: 0L,
            Name: request.Name,
            Email: request.Email,
            ProfilePicture: null,
            Password: HashPassword(request.Password),
            Role: request.Role,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return jwtService.GenerateToken(user);
    }

    public async Task<Result<string>> Login(LoginRequest request)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
        {
            return Error.NotFound("User not found");
        }

        if (!user.Password.Equals(HashPassword(request.Password)))
        {
            return Error.InvalidArgument("Invalid password");
        }

        return jwtService.GenerateToken(user);
    }

    private string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }
}

public interface IAuthService
{
    Task<Result<string>> Register(RegisterRequest request);
    Task<Result<string>> Login(LoginRequest request);
}

public record RegisterRequest(string Name, string Email, string Password, string Role);
public record LoginRequest(string Email, string Password);