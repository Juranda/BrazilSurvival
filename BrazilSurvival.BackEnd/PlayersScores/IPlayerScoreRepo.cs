using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.PlayersScores.Models;

namespace BrazilSurvival.BackEnd.PlayersScores;

public interface IPlayerScoreRepo
{
    Task<Result<PlayerScore>> GetPlayerScoreAsync(int id);
    Task<List<PlayerScore>> GetPlayerScoresAsync(int page = 1, int pageSize = 10);
    Task<Result<PlayerScore>> PostPlayerScoreAsync(Guid token, string name);
}
