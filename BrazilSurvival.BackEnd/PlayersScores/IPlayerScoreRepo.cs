using BrazilSurvival.BackEnd.PlayersScores.Models;

namespace BrazilSurvival.BackEnd.PlayersScores;

public interface IPlayerScoreRepo
{
    Task<PlayerScore?> GetPlayerScoreAsync(int id);
    Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10);
    Task<PlayerScore> PostPlayerScoreAsync(PlayerScore playerScore);
}
