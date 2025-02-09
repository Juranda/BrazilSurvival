using System.Security.Cryptography;
using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Services;

namespace BrazilSurvival.BackEnd.Game;

public class GameService : IGameService
{
    private readonly IChallengeRepo challengeRepo;

    public GameService(IChallengeRepo challengeRepo)
    {
        this.challengeRepo = challengeRepo;
    }
    public async Task<(PlayerStats, List<Challenge>)> StartGame(PlayerStats? playerStats)
    {
        if (playerStats == null)
        {
            playerStats = new PlayerStats(10, 10, 10);
        }

        var challenges = await challengeRepo.GetChallengesAsync();

        return (playerStats, challenges);
    }

    public async Task<Result<AnswerChallengeResult>> AnswerChallenge(PlayerStats playerStats, int challengeId, int optionId, bool requestNewChallenges)
    {
        List<Challenge> newChallenges = [];

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

        int randomNumber = RandomNumberGenerator.GetInt32(selectedOption.Consequences.Count);
        ChallengeOptionConsequence consequence = selectedOption.Consequences.ElementAt(randomNumber);

        PlayerStats newPlayerStats = new()
        {
            Health = playerStats.Health + consequence.Health ?? 0,
            Money = playerStats.Money + consequence.Money ?? 0,
            Power = playerStats.Power + consequence.Power ?? 0
        };

        AnswerEffect effect = new()
        {
            Health = consequence.Health ?? 0,
            Money = consequence.Money ?? 0,
            Power = consequence.Power ?? 0
        };

        bool isGameOver = newPlayerStats.Health <= 0 || newPlayerStats.Money <= 0 || newPlayerStats.Power <= 0;

        if (requestNewChallenges && isGameOver == false)
        {
            newChallenges = await challengeRepo.GetChallengesAsync();
        }

        return new AnswerChallengeResult(consequence.Answer, consequence.Consequence, effect, newPlayerStats, isGameOver, newChallenges);
    }
}