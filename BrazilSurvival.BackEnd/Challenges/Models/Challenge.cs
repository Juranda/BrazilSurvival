namespace BrazilSurvival.BackEnd.Challenges.Models;

public class Challenge
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public ChallengeOption[] Options { get; set; } = [];
}
