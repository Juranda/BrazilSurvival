namespace BrazilSurvival.BackEnd.Challenges.Models;

public class ChallengeOption
{
    public int Id { get; set; }
    public required string Action { get; set; }
    public required string Answer { get; set; }
    public required string Consequence { get; set; }
    public int Health { get; set; }
    public int Money { get; set; }
    public int Power { get; set; }
}
