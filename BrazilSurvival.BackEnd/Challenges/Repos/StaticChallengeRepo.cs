using System.Text.Json;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Exceptions;

namespace BrazilSurvival.BackEnd.Challenges.Repos;

public class StaticChallengeRepo : IChallengeRepo
{
    private List<Challenge> challenges;

    public StaticChallengeRepo() {
        string jsonArray = File.ReadAllText("staticChallenges.json");
        
        Challenge[]? possiblyNullChallenges = JsonSerializer.Deserialize<Challenge[]>(jsonArray, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        });

        if(possiblyNullChallenges == null) {
            throw new Exception("Para utilizar a classe 'StaticChallengeRepo' é preciso configurar o arquivo 'staticChallenges.json' na pasta raiz da API.");
        }

        challenges = possiblyNullChallenges.ToList();
    }

    public Task<Challenge> GetChallengeAsync(int id)
    {
        var challenge = challenges.Find(x => x.Id == id);

        if(challenge is null) {
            throw new NotFoundException("Invalid answer");
        }

        return Task.FromResult(challenge);
    }

    public async Task<List<Challenge>> GetChallengesAsync(int quantity = 10)
    {
        return await Task.FromResult(challenges);
    }  
}
