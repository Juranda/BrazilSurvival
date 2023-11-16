using BrazilSurvival.BackEnd.Models.Domain;

namespace BrazilSurvival.BackEnd.Repos;

public class StaticPlayersRepo : IPlayerScoreRepo
{
    List<PlayerScore> playersScores = new()
    {
        new PlayerScore() {
            Id = 0,
            Name = "",
            Score = 25
        }
        ,
        new PlayerScore() {
            Id = 0,
            Name = "",
            Score = 50
        }
        ,new PlayerScore() {
            Id = 0,
            Name = "",
            Score = 75
        },
        new PlayerScore() {
            Id = 0,
            Name = "",
            Score = 100
        }
    };
    
    public async Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10)
    {
        return await Task.FromResult(playersScores.OrderByDescending(x => x.Score).ToList());
    }

    public async Task<PlayerScore> PostPlayerScoreAsync(PlayerScore playerScore)
    {
        playersScores.Add(playerScore);

        return await Task.FromResult(playerScore);
    }
}