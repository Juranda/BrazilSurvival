using System.Text.Json;
using BrazilSurvival.BackEnd.Models.Domain;

namespace BrazilSurvival.BackEnd.Repos;

public class StaticChallengeRepo : IChallengeRepo
{
    private List<Challenge> challenges;

    public StaticChallengeRepo() {

        string jsonArray = File.ReadAllText("staticChallenges.json");
        challenges = JsonSerializer.Deserialize<Challenge[]>(jsonArray, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }).ToList();
    }

    public async Task<List<Challenge>> GetChallengesAsync(int quantity = 10)
    {
        return await Task.FromResult(challenges);
    }  
}
