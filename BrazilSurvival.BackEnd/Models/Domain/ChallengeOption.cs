namespace BrazilSurvival.BackEnd.Models.Domain;

public class ChallengeOption
{
    public int Id { get; set; }
    public string Action { get; set; }
    public string Answer { get; set; }
    public string Consequence { get; set; }
    public int Health { get; set; }
    public int Money { get; set; }
    public int Power { get; set; }
}
