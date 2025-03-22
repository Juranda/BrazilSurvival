using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;

namespace BrazilSurvival.BackEnd.Game.Repo;

public interface IGameStateRepo
{
    Task<GameState> PostGameState(GameState gameState);
    Task<Result<GameState>> GetGameState(Guid token);
    Task<Result<GameState>> UpdateGameState(GameState gameState);
}
