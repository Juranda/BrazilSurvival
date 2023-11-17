using BrazilSurvival.BackEnd.Models.Domain;

namespace BrazilSurvival.BackEnd.Repos;

interface IChallengeRepo
{
    Task<List<Challenge>> GetChallengesAsync(int quantity = 10);
}
