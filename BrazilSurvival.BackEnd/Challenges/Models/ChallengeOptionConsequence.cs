using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Challenges.Models;

public class ChallengeOptionConsequence
{
    public required int Id { get; set; }
    [StringLength(100)]
    public required string Answer { get; set; }
    [StringLength(255)]
    public required string Consequence { get; set; }
    public int? Health { get; set; }
    public int? Money { get; set; }
    public int? Power { get; set; }


    public int ChallengeOptionId { get; set; }
    public required ChallengeOption ChallengeOption { get; set; }
}