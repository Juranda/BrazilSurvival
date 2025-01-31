namespace BrazilSurvival.BackEnd.Challenges.Models;

public class ChallengeDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public ChallengeOptionDTO[] Options { get; set; } = [];
}