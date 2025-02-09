using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.PlayersScores.Models;

namespace BrazilSurvival.BackEnd.PlayersScores.Repos;

public class StaticPlayersRepo : IPlayerScoreRepo
{
    readonly List<PlayerScore> playersScores =
    [
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
    ];

    public async Task<Result<PlayerScore>> GetPlayerScoreAsync(int id)
    {
        var playerScore = playersScores.Where(x => x.Id == id).SingleOrDefault();

        if (playerScore is null)
        {
            return Error.NotFound();
        }

        return await Task.FromResult(playerScore);
    }

    public async Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10)
    {
        return await Task.FromResult(playersScores.OrderByDescending(x => x.Score).ToList());
    }

    public async Task<Result<PlayerScore>> PostPlayerScoreAsync(PlayerScore playerScore)
    {
        playerScore.Id = playersScores.Count;
        playersScores.Add(playerScore);

        return await Task.FromResult(playerScore);
    }
}