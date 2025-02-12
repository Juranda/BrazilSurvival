using System.Text;
using System.Text.Json.Serialization;
using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Repos;
using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.ExceptionHandlers;
using BrazilSurvival.BackEnd.Game.Services;
using BrazilSurvival.BackEnd.PlayersScores;
using BrazilSurvival.BackEnd.PlayersScores.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<GameDbConext>(options => options.UseFirebird(builder.Configuration.GetConnectionString("BrazilSurvivalConnectionString")));

builder.Services.AddScoped<IPlayerScoreRepo, EFContextPlayersScoresRepo>();
builder.Services.AddScoped<IChallengeRepo, EFContextChallengesRepo>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<GameService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//     });

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors();
app.MapControllers();

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