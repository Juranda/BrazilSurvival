using BrazilSurvival.BackEnd.Models.Domain;
using BrazilSurvival.BackEnd.Models.DTO;
using BrazilSurvival.BackEnd.Repos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

IChallengeRepo challengeRepo = new StaticChallengeRepo();
IPlayerScoreRepo playerScoreRepo = new StaticPlayersRepo();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.MapGet("/challenges", async () => {
    return await challengeRepo.GetChallengesAsync();
});

app.MapGet("/players", async () => {
    return await playerScoreRepo.GetPlayerScoresAsync();
});

app.MapPost("/players", async ([FromBody] PlayerScorePostRequest playerScorePostRequest) => {
    var playerScore = new PlayerScore() 
    {
        Name = playerScorePostRequest.Name,
        Score = playerScorePostRequest.Score
    };

    playerScore = await playerScoreRepo.PostPlayerScoreAsync(playerScore);

    return playerScore;
});

app.Run();