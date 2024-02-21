using BrazilSurvival.BackEnd.Models.Domain;
using BrazilSurvival.BackEnd.Models.DTO;
using BrazilSurvival.BackEnd.Repos;
using Microsoft.AspNetCore.Mvc;

IChallengeRepo challengeRepo = new StaticChallengeRepo();
IPlayerScoreRepo playerScoreRepo = new StaticPlayersRepo();

string[] allowedOrigins =
{
    "https://app.brazilsurvival:3535",
    "http://localhost:5173"
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy => policy
        .AllowAnyOrigin()
        .WithMethods("DELETE", "UPDATE")
        .AllowAnyHeader()));

var app = builder.Build();

app.UseCors();

app.MapGet("/challenges", async () =>
{
    return await challengeRepo.GetChallengesAsync();
});

app.MapGet("/players", async () =>
{
    var playersScores = await playerScoreRepo.GetPlayerScoresAsync();

    return Results.Ok(playersScores);
});

app.MapPost("/players", async ([FromBody] PlayerScorePostRequest playerScorePostRequest) =>
{
    var playerScore = new PlayerScore()
    {
        Name = playerScorePostRequest.Name,
        Score = playerScorePostRequest.Score
    };

    playerScore = await playerScoreRepo.PostPlayerScoreAsync(playerScore);

    return Results.Created("/players", playerScore);
});

app.Run();