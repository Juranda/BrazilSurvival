using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Challenges.Models.DTO;

public class PostChallengeOption
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string Action { get; set; }

    [Required]
    [MinLength(1)]
    public required List<PostOptionConsequence> consequences { get; set; }
}
