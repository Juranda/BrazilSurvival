using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Challenges;

public interface IChallengeRepo
{
    Task<List<Challenge>> GetChallengesAsync(int quantity = 10);
    Task<Challenge> GetChallengeAsync(int id);
}
