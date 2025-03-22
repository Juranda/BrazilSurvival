using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.Game.Models;

namespace BrazilSurvival.BackEnd.Game.Services;

public interface IGameService
{
    public Task<(Guid, PlayerStats, List<Challenge>)> StartGame(PlayerStats? playerStats);
    public Task<Result<AnswerChallengeResult>> AnswerChallenge(Guid token, int challengeId, int optionId, bool requestNewChallenges);
}