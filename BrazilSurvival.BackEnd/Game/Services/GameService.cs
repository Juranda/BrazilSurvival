using System.Security.Cryptography;
using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Repo;

namespace BrazilSurvival.BackEnd.Game.Services;

public class GameService : IGameService
{
    private readonly IChallengeRepo challengeRepo;
    private readonly IGameStateRepo gameStateRepo;

    public GameService(IChallengeRepo challengeRepo, IGameStateRepo gameStateRepo)
    {
        this.challengeRepo = challengeRepo;
        this.gameStateRepo = gameStateRepo;
    }

    public async Task<(Guid, PlayerStats, List<Challenge>)> StartGame(PlayerStats? playerStats)
    {
        if (playerStats == null)
        {
            playerStats = new PlayerStats(10, 10, 10);
        }

        GameState gameState = new()
        {
            Token = Guid.NewGuid(),
            Health = playerStats.Health,
            Money = playerStats.Money,
            Power = playerStats.Power,
        };

        var challenges = await challengeRepo.GetChallengesAsync();

        gameState = await gameStateRepo.PostGameState(gameState);

        return (gameState.Token, playerStats, challenges);
    }

    public async Task<Result<AnswerChallengeResult>> AnswerChallenge(Guid token, int challengeId, int optionId, bool requestNewChallenges)
    {
        Result<GameState> gameStateResult = await gameStateRepo.GetGameState(token);

        if (gameStateResult.HasError)
        {
            return Error.NotFound("Invalid token");
        }

        GameState gameState = gameStateResult.Value;
        if (gameState.IsOver)
        {
            return Error.InvalidArgument("Can not update game after it's ended");
        }

        Result<Challenge> result = await challengeRepo.GetChallengeAsync(challengeId);

        if (result.HasError)
        {
            return Error.NotFound("Invalid challenge id");
        }

        Challenge challenge = result.Value;
        ChallengeOption? selectedOption = challenge.Options.FirstOrDefault(x => x.Id == optionId);

        if (selectedOption is null)
        {
            return Error.NotFound("Invalid answer id");
        }

        var (newGameState, consequence, effect) = await ApplyRandomConsequence(gameState, selectedOption);

        PlayerStats newPlayerStats = new()
        {
            Health = newGameState.Health,
            Money = newGameState.Money,
            Power = newGameState.Power
        };

        List<Challenge> newChallenges = [];

        if (requestNewChallenges && newGameState.IsOver == false)
        {
            newChallenges = await challengeRepo.GetChallengesAsync();
        }

        return new AnswerChallengeResult(gameState.Token, consequence.Answer, consequence.Consequence, effect, newPlayerStats, newGameState.IsOver, newChallenges);
    }

    private async Task<(GameState, ChallengeOptionConsequence, AnswerEffect)> ApplyRandomConsequence(GameState gameState, ChallengeOption option)
    {
        int randomNumber = RandomNumberGenerator.GetInt32(option.Consequences.Count);
        ChallengeOptionConsequence consequence = option.Consequences.ElementAt(randomNumber);

        gameState.Health += consequence.Health ?? 0;
        gameState.Money += consequence.Money ?? 0;
        gameState.Power += consequence.Power ?? 0;

        AnswerEffect effect = new()
        {
            Health = consequence.Health ?? 0,
            Money = consequence.Money ?? 0,
            Power = consequence.Power ?? 0
        };

        if (gameState.Health <= 0 || gameState.Money <= 0 || gameState.Power <= 0)
        {
            gameState.Score++;
        }

        Result<GameState> result = await gameStateRepo.UpdateGameState(gameState);

        if (result.HasError)
        {
            throw new Exception("ApplyRandomConsequence resulted in a error");
        }

        gameState = result.Value;

        return (gameState, consequence, effect);
    }

    private async Task<GameState> EndGame(GameState gameState)
    {
        Result<GameState> result = await gameStateRepo.UpdateGameState(gameState);
        return result.Value;
    }
}