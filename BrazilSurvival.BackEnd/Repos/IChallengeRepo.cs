using BrazilSurvival.BackEnd.Models.Domain;

namespace BrazilSurvival.BackEnd.Repos;

public interface IChallengeRepo
{
    Task<List<Challenge>> GetChallengesAsync(int quantity = 10);
}
