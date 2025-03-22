using System.Text;
using System.Text.Json.Serialization;
using BrazilSurvival.BackEnd.Auth;
using BrazilSurvival.BackEnd.Auth.Services;
using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Repos;
using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.ExceptionHandlers;
using BrazilSurvival.BackEnd.Game.Repo;
using BrazilSurvival.BackEnd.Game.Services;
using BrazilSurvival.BackEnd.PlayersScores;
using BrazilSurvival.BackEnd.PlayersScores.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .Build();
        });
    });
}

builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            },
            []
        }
    });
});

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<GameDbConext>(options => options.UseFirebird(builder.Configuration.GetConnectionString("BrazilSurvivalConnectionString")));
builder.Services.AddDbContext<UsersDbContext>(options => options.UseFirebird(builder.Configuration.GetConnectionString("BrazilSurvivalUserConnectionString")));

builder.Services.AddScoped<IPlayerScoreRepo, EFContextPlayersScoresRepo>();
builder.Services.AddScoped<IChallengeRepo, EFContextChallengesRepo>();
builder.Services.AddScoped<IGameStateRepo, EFContextGameStatesRepo>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new NullReferenceException("Please provide a jwt:issuer at appsettings"),
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new NullReferenceException("Please provide a jwt:audience at appsettings"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? throw new NullReferenceException("Please provide a jwt:secretkey at appsettings")))
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AuthorizationPolicies.ADMINISTRATOR, policy => policy.RequireRole(AuthorizationPolicies.ADMINISTRATOR))
    .AddPolicy(AuthorizationPolicies.PLAYER, policy => policy.RequireRole(AuthorizationPolicies.PLAYER));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}


app.Run("http://localhost:5000");