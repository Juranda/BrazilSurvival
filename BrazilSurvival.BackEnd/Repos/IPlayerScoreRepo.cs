using BrazilSurvival.BackEnd.Models.Domain;
using BrazilSurvival.BackEnd.Models.DTO;

namespace BrazilSurvival.BackEnd.Repos;

interface IPlayerScoreRepo
{
    Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10);
    Task<PlayerScore> PostPlayerScoreAsync(PlayerScore playerScore);
}
