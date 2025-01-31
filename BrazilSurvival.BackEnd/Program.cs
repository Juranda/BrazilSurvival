using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Repos;
using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Game;
using BrazilSurvival.BackEnd.PlayersScores;
using BrazilSurvival.BackEnd.PlayersScores.Repos;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<GameService>();
builder.Services.AddDbContext<GameDbConext>(options => options.UseFirebird(builder.Configuration.GetConnectionString("BrazilSurvivalConnectionString")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run("http://localhost:5000");