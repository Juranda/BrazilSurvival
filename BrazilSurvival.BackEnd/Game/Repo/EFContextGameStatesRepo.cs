using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;
using Microsoft.EntityFrameworkCore;

namespace BrazilSurvival.BackEnd.Game.Repo;

public class EFContextGameStatesRepo : IGameStateRepo
{
    private readonly GameDbConext gameDbContext;

    public EFContextGameStatesRepo(GameDbConext gameDbContext)
    {
        this.gameDbContext = gameDbContext;
    }

    public async Task<GameState> PostGameState(GameState gameState)
    {
        gameState.Token = Guid.NewGuid();
        gameState.CreatedAt = DateTime.UtcNow;

        await gameDbContext.AddAsync(gameState);
        await gameDbContext.SaveChangesAsync();

        return gameState;
    }

    public async Task<Result<GameState>> GetGameState(Guid token)
    {
        GameState? gameState = await gameDbContext.GameStates.FirstOrDefaultAsync(g => g.Token == token);

        if (gameState is null)
        {
            return Error.NotFound("Game does not exist in the server");
        }

        return gameState;
    }

    public async Task<Result<GameState>> UpdateGameState(GameState newGameState)
    {
        GameState? gameState = await gameDbContext.GameStates.FirstOrDefaultAsync(g => g.Token == newGameState.Token);

        if (gameState is null)
        {
            return Error.NotFound("Game does not exist in the server");
        }

        if (gameState.IsOver)
        {
            return Error.NotFound("Can not update game after it's ending");
        }

        gameState.Health = newGameState.Health;
        gameState.Money = newGameState.Money;
        gameState.Power = newGameState.Power;
        gameState.Score = newGameState.Score;

        if (newGameState.Health <= 0 || newGameState.Money <= 0 || newGameState.Power <= 0)
        {
            gameState.EndedAt = DateTime.UtcNow;
        }

        await gameDbContext.SaveChangesAsync();

        return gameState;
    }
}