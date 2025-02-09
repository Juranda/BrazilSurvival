using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;

namespace BrazilSurvival.BackEnd.Game.Services;

public interface IGameService
{
    public Task<(PlayerStats, List<Challenge>)> StartGame(PlayerStats? playerStats);
    public Task<Result<AnswerChallengeResult>> AnswerChallenge(PlayerStats stats, int challengeId, int optionId, bool requestNewChallenges);
}