using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Exceptions;
using BrazilSurvival.BackEnd.Game.Models;

namespace BrazilSurvival.BackEnd.Game;

public class GameService
{
    private readonly IChallengeRepo challengeRepo;

    public GameService(IChallengeRepo challengeRepo)
    {
        this.challengeRepo = challengeRepo;
    }
    public async Task<Tuple<PlayerStats, List<Challenge>>> StartGame(PlayerStats? playerStats)
    {
        if (playerStats == null)
        {
            playerStats = new PlayerStats(10, 10, 10);
        }

        var challenges = await challengeRepo.GetChallengesAsync();

        return new(playerStats, challenges);
    }

    public async Task<AnswerChallengeResult> NextChallenge(PlayerStats stats, int challengeId, int optionId, bool requestNewChallenges)
    {
        List<Challenge>? challenges = null;
        var challenge = await challengeRepo.GetChallengeAsync(challengeId);
        var selectedOption = challenge.Options.First(x => x.Id == optionId);

        if (selectedOption is null)
        {
            throw new NotFoundException("Invalid answer id");
        }

        PlayerStats newPlayerStats = new(
            stats.Health + selectedOption.Health,
            stats.Money + selectedOption.Money,
            stats.Power + selectedOption.Power
        );

        bool isGameOver = newPlayerStats.Health <= 0 || newPlayerStats.Money <= 0 || newPlayerStats.Power <= 0;

        if (requestNewChallenges && isGameOver == false)
        {
            challenges = await challengeRepo.GetChallengesAsync();
        }

        return new AnswerChallengeResult(selectedOption.Answer, selectedOption.Consequence, newPlayerStats, isGameOver, challenges);
    }

    public async Task<List<Challenge>> GetRandomChallenges()
    {
        return await challengeRepo.GetChallengesAsync();
    }
}
