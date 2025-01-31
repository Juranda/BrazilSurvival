using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;

namespace BrazilSurvival.BackEnd.Game;

public class GameService
{
    private readonly IChallengeRepo challengeRepo;

    public GameService(IChallengeRepo challengeRepo)
    {
        this.challengeRepo = challengeRepo;
    }
    public async Task<Tuple<GameStats, List<Challenge>>> StartGame(GameStats? gameStats)
    {
        if (gameStats == null)
        {
            gameStats = new GameStats(10, 10, 10);
        }

        var challenges = await challengeRepo.GetChallengesAsync();

        return new(gameStats, challenges);
    }

    public async Task<AnswerChallengeResult> NextChallenge(GameStats stats, int challengeId, int answerId)
    {

        var challenge = await challengeRepo.GetChallengeAsync(challengeId);
        var selectedOption = challenge.Options.FirstOrDefault(x => x.Id == answerId);

        if (selectedOption is null)
        {
            throw new Exception("Invalid answer");
        }

        GameStats newGameStats = new(
            stats.Health + selectedOption.Health,
            stats.Money + selectedOption.Money,
            stats.Power + selectedOption.Power
        );

        bool isGameOver = newGameStats.Health <= 0 || newGameStats.Money <= 0 || newGameStats.Power <= 0;

        return new AnswerChallengeResult(selectedOption.Answer, selectedOption.Consequence, newGameStats, isGameOver);
    }
}


public record AnswerChallengeResult(
    string answer,
    string consequence,
    GameStats newGameStats,
    bool isGameOver
);