using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using Microsoft.EntityFrameworkCore;

namespace BrazilSurvival.BackEnd.PlayersScores.Repos;

public class EFContextPlayersScoresRepo : IPlayerScoreRepo
{
    private readonly GameDbConext context;
    private readonly ILogger<EFContextPlayersScoresRepo> logger;

    public EFContextPlayersScoresRepo(GameDbConext context, ILogger<EFContextPlayersScoresRepo> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task<Result<PlayerScore>> GetPlayerScoreAsync(int id)
    {
        var playerScore = await context.PlayerScores.FirstOrDefaultAsync(x => x.Id == id);

        if (playerScore is null)
        {
            return Error.NotFound();
        }

        return await Task.FromResult(playerScore);
    }

    public async Task<List<PlayerScore>> GetPlayerScoresAsync(int page = 1, int pageSize = 10)
    {
        return await context.PlayerScores
            .OrderByDescending(x => x.Score)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Result<PlayerScore>> PostPlayerScoreAsync(Guid token, string name)
    {
        GameState? gameState = await context.GameStates.FirstOrDefaultAsync(x => x.Token == token);

        if (gameState is null)
        {
            return Error.NotFound($"GameState with token {token} does not exist in the server");
        }

        PlayerScore playerScore = new PlayerScore()
        {
            Id = 0,
            Name = name,
            Score = gameState.Score,
            GameState = gameState,
            GameStateToken = token
        };

        context.PlayerScores.Add(playerScore);

        await context.SaveChangesAsync();

        return await Task.FromResult(playerScore);
    }
}