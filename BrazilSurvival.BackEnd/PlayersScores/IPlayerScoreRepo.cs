using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.PlayersScores.Models;

namespace BrazilSurvival.BackEnd.PlayersScores;

public interface IPlayerScoreRepo
{
    Task<Result<PlayerScore>> GetPlayerScoreAsync(int id);
    Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10);
    Task<Result<PlayerScore>> PostPlayerScoreAsync(PlayerScore playerScore);
}
