using BrazilSurvival.BackEnd.Models.Domain;

namespace BrazilSurvival.BackEnd.Repos;

public interface IPlayerScoreRepo
{
    Task<PlayerScore?> GetPlayerScoreAsync(int id);
    Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10);
    Task<PlayerScore> PostPlayerScoreAsync(PlayerScore playerScore);
}
