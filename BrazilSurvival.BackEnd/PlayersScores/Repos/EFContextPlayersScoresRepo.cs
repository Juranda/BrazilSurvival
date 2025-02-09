using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using Microsoft.EntityFrameworkCore;

namespace BrazilSurvival.BackEnd.PlayersScores.Repos;

public class EFContextPlayersScoresRepo : IPlayerScoreRepo
{
    private readonly GameDbConext context;

    public EFContextPlayersScoresRepo(GameDbConext context)
    {
        this.context = context;
    }

    public async Task<Result<PlayerScore>> GetPlayerScoreAsync(int id)
    {
        var playerScore = await context.PlayerScores.Where(x => x.Id == id).SingleOrDefaultAsync();

        if (playerScore is null)
        {
            return Error.NotFound();
        }

        return await Task.FromResult(playerScore);
    }

    public async Task<List<PlayerScore>> GetPlayerScoresAsync(int quantity = 10)
    {
        return await context.PlayerScores.OrderByDescending(x => x.Score).ToListAsync();
    }   

    public async Task<Result<PlayerScore>> PostPlayerScoreAsync(PlayerScore playerScore)
    {
        var playerScoreInDB = context.PlayerScores.Where(x => x.Name == playerScore.Name);

        context.PlayerScores.Add(playerScore);

        await context.SaveChangesAsync();

        return await Task.FromResult(playerScore);
    }
}