using System.Security.Cryptography;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models;

namespace BrazilSurvival.BackEnd.PlayersScores.Repos;

public class StaticPlayersRepo : IPlayerScoreRepo
{
    readonly List<PlayerScore> playersScores =
    [
        new PlayerScore() {
            Id = 0,
            Name = "FELIPE",
            Score = 25,
            GameStateToken = Guid.NewGuid(),
            GameState = new GameState {
            }
        }
        ,
        new PlayerScore() {
            Id = 1,
            Name = "SPACED",
            Score = 50,
            GameStateToken = Guid.NewGuid(),
            GameState = new GameState {
            }
        }
        ,new PlayerScore() {
            Id = 2,
            Name = "VINICS",
            Score = 75,
            GameStateToken = Guid.NewGuid(),
            GameState = new GameState {
            }
        },
        new PlayerScore() {
            Id = 3,
            Name = "SOFIS2",
            Score = 100,
            GameStateToken = Guid.NewGuid(),
            GameState = new GameState {
            }
        }
    ];

    public async Task<Result<PlayerScore>> GetPlayerScoreAsync(int id)
    {
        var playerScore = playersScores.Where(x => x.Id == id).SingleOrDefault();

        if (playerScore is null)
        {
            return Error.NotFound();
        }

        return await Task.FromResult(playerScore);
    }

    public async Task<List<PlayerScore>> GetPlayerScoresAsync(int page = 1, int pageSize = 10)
    {
        List<PlayerScore> playerScores = playersScores
            .OrderByDescending(x => x.Score)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return await Task.FromResult(playerScores);
    }

    public async Task<Result<PlayerScore>> PostPlayerScoreAsync(Guid token, string name)
    {
        PlayerScore playerScore = new()
        {
            Id = playersScores.Count,
            Name = name,
            GameState = new(),
            GameStateToken = token,
            Score = RandomNumberGenerator.GetInt32(100)
        };

        playersScores.Add(playerScore);

        return await Task.FromResult(playerScore);
    }
}