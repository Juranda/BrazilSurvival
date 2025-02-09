using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Errors;

namespace BrazilSurvival.BackEnd.Challenges;

public interface IChallengeRepo
{
    Task<List<Challenge>> GetChallengesAsync(int quantity = 10);
    Task<Result<Challenge>> GetChallengeAsync(int id);
    Task<Result<Challenge>> PostChallengeAsync(Challenge challenge);
}
