namespace BrazilSurvival.BackEnd.Auth;

using BrazilSurvival.BackEnd.Auth.Models;
using BrazilSurvival.BackEnd.Auth.Services;
using BrazilSurvival.BackEnd.Errors;
using Microsoft.AspNetCore.Mvc;

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
