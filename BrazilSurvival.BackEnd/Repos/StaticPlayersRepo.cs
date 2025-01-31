using BrazilSurvival.BackEnd.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrazilSurvival.BackEnd.Repos;

public class StaticPlayersRepo : IPlayerScoreRepo
{
    List<PlayerScore> playersScores = new()
    {
        new PlayerScore() {
            Id = 0,
            Name = "FELIPE",
            Score = 25
        }
        ,
        new PlayerScore() {
            Id = 1,
            Name = "SPACED",
            Score = 50
        }
        ,new PlayerScore() {
            Id = 2,
            Name = "VINICS",
            Score = 75
        },
        new PlayerScore() {
            Id = 3,
            Name = "SOFIS2",
            Score = 100
        }
    };

    public async Task<PlayerScore?> GetPlayerScoreAsync(int id)
    {
        var playerScores = playersScores.Where(x => x.Id == id);

        PlayerScore? playerScore;

        try {
            playerScore = playerScores.Single();
        } catch {
            playerScore = null;
        }

        return await Task.FromResult(playerScore);
    }

    public async Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10)
    {
        return await Task.FromResult(playersScores.OrderByDescending(x => x.Score).ToList());
    }

    public async Task<PlayerScore> PostPlayerScoreAsync(PlayerScore playerScore)
    {

        playerScore.Id = playersScores.Count();

        playersScores.Add(playerScore);

        return await Task.FromResult(playerScore);
    }
}