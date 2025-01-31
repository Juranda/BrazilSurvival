using BrazilSurvival.BackEnd.Models;

namespace BrazilSurvival.BackEnd.Challenges;

public interface IChallengeRepo
{
    Task<List<Challenge>> GetChallengesAsync(int quantity = 10);
}
