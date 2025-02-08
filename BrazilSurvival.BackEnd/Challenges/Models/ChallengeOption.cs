using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Challenges.Models;

public class ChallengeOption
{
    public int Id { get; set; }
    [StringLength(50)]
    public required string Action { get; set; }
    public int ChallengeId { get; set; }

    public required Challenge Challenge { get; set; }
    public required List<ChallengeOptionConsequence> Consequences { get; set; }
}
