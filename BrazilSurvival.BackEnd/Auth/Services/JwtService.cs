using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BrazilSurvival.BackEnd.Auth.Models;
using BrazilSurvival.BackEnd.Errors;

namespace BrazilSurvival.BackEnd.Auth.Services
{
    public class JwtService
    {
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;

        public JwtService(IConfiguration configuration)
        {
            secretKey = configuration["Jwt:SecretKey"] ?? throw new NullReferenceException("Please provide a jwt:secretkey at appsettings");
            issuer = configuration["Jwt:Issuer"] ?? throw new NullReferenceException("Please provide a jwt:issuer at appsettings");
            audience = configuration["Jwt:Audience"] ?? throw new NullReferenceException("Please provide a jwt:audience at appsettings");
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public Result<ClaimsPrincipal> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch(Exception ex)
            {
                return Error.Unauthorized(ex.Message);
            }
        }
    }
}