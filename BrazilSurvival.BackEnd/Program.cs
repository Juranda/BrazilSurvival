using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Repos;
using Microsoft.EntityFrameworkCore;

IChallengeRepo challengeRepo = new StaticChallengeRepo();
IPlayerScoreRepo playerScoreRepo = new StaticPlayersRepo();

string[] allowedOrigins =
{
    "https://app.brazilsurvival:3535",
    "http://localhost:5173"
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy => policy
        .AllowAnyOrigin()
        .WithMethods("DELETE", "UPDATE")
        .AllowAnyHeader()));


builder.Services.AddScoped<IPlayerScoreRepo, EFContextPlayersScoresRepo>();
builder.Services.AddScoped<IChallengeRepo, StaticChallengeRepo>();
builder.Services.AddDbContext<GameDbConext>(options =>
options.UseFirebird(builder.Configuration.GetConnectionString("BrazilSurvivalConnectionString")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapControllers();

app.Run("http://localhost:5000");