using System.Text.Json.Serialization;
using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Repos;
using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Game;
using BrazilSurvival.BackEnd.Game.Exceptions;
using BrazilSurvival.BackEnd.PlayersScores;
using BrazilSurvival.BackEnd.PlayersScores.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<GameDbConext>(options => options.UseFirebird(builder.Configuration.GetConnectionString("BrazilSurvivalConnectionString")));

builder.Services.AddScoped<IPlayerScoreRepo, EFContextPlayersScoresRepo>();
builder.Services.AddScoped<IChallengeRepo, StaticChallengeRepo>();
builder.Services.AddScoped<GameService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

if (builder.Environment.IsProduction())
{
    builder.Services.AddExceptionHandler<ProcessErrorExceptionHandler>();
    builder.Services.AddProblemDetails();
}

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}


app.Run("http://localhost:5000/api");