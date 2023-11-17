using BrazilSurvival.BackEnd.Models.Domain;

namespace BrazilSurvival.BackEnd.Repos;

public class StaticChallengeRepo : IChallengeRepo
{
    private List<Challenge> challenges = new List<Challenge> {
        new Challenge {
            Id = 0,
            Title = "",
            Options = new ChallengeOption[] {
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
            }
        },
        new Challenge {
            Id = 0,
            Title = "",
            Options = new ChallengeOption[] {
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
            }
        },
        new Challenge {
            Id = 0,
            Title = "",
            Options = new ChallengeOption[] {
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
            }
        },
        new Challenge {
            Id = 0,
            Title = "",
            Options = new ChallengeOption[] {
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
                new() {
                    Id = 0,
                    Action = "",
                    Answer = "",
                    Consequence = "",
                    Health = 0,
                    Money = 0,
                    Power = 0
                },
            }
        }
    };

    public async Task<List<Challenge>> GetChallengesAsync(int quantity = 10)
    {
        return await Task.FromResult(challenges);
    }  
}
