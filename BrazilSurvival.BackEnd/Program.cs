using BrazilSurvival.BackEnd.Models.Domain;
using BrazilSurvival.BackEnd.Models.DTO;
using BrazilSurvival.BackEnd.Repos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IChallengeRepo challengeRepo = new StaticChallengeRepo();
IPlayerScoreRepo playerScoreRepo = new StaticPlayersRepo();

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


/*
            {
            title: "Você acabou de chegar na rodoviária após um longo dia de trabalho, mas percebe que o ônibus está prestes a sair.",
            options: [
                {
                    action: "Pegar outro ônibus",
                    answer: "Você espera para pegar outro ônibus",
                    consequence: "Dois caras aparecem numa moto e te assaltam",
                    money: -6, 
                    power: -2
                },
                {
                    action: "Pegar outro ônibus que está lotado",
                    answer: "Por sorte você encontra um outro ônibus que vai para o mesmo lugar",
                    consequence: "Você consegue chegar em casa mas percebe que sua carteira foi roubada",
                    health: 2, 
                    money: -3
                },
                {
                    action: "Tentar alcançar o ônibus",
                    answer: "Você corre até suas pernas não aguentarem mais",
                    consequence: "Além de perder o ônibus agora você está muito cansado",
                    health: -2
                },
                {
                    action: "Pedir um uber",
                    answer: "Você tenta chamar um uber no meio da noite",
                    consequence: "Você consegue voltar para casa em segurança",
                    money: -3
                }
            ]
        },
*/